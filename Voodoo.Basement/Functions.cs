using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Security;

namespace Voodoo.Basement
{
    public class Functions
    {
        #region 新闻列表 cmsnewslist
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="ClassID">栏目ID</param>
        /// <param name="TitlePreChar">标题前字符</param>
        /// <param name="count">索取条数</param>
        /// <param name="TitleLength">标题保留长度</param>
        /// <param name="ExtSql">额外的Sql条件</param>
        /// <param name="Order">排序语句</param>
        /// <returns></returns>
        public string cmsnewslist(string ClassID, string TitlePreChar, string count, string TitleLength, string showTime, string ExtSql, string Order)
        {
            Class cls = NewsAction.NewsClass.Where(p => p.ID.ToString() == ClassID).First();

            if (cls.ModelID == 1)
            {

                string str_sql = string.Format("classID in ({0})", GetAllSubClass(ClassID.ToInt32()));
                if (ExtSql.Length > 1)
                {
                    str_sql += " and " + ExtSql;
                }
                str_sql += Order;


                List<News> nlist = NewsView.GetModelList(str_sql, count.ToInt32());
                StringBuilder sb = new StringBuilder();
                foreach (News n in nlist)
                {
                    string title = n.Title;
                    if (TitleLength.ToInt32() > 0)
                    {
                        title = title.CutString(TitleLength.ToInt32());
                    }
                    string timespan = "";
                    if (showTime.ToInt32() > 0)
                    {
                        timespan = string.Format("<span class=\"news_time_span\">{0}</span>", n.NewsTime.ToString("yyyy/MM/dd"));
                    }

                    sb.AppendLine(string.Format("<li>{0}{1}<a href='{2}' title='{3}'>{4}</a></li>", TitlePreChar, timespan, BasePage.GetNewsUrl(n, NewsView.GetNewsClass(n)), n.Title, title));
                }
                return sb.ToS();
            }//Model=1 新闻
            else if (cls.ModelID == 2)
            {
                return "图";
            }
            else if (cls.ModelID == 3)//问答
            {
                string str_sql = string.Format("classID in ({0})", GetAllSubClass(ClassID.ToInt32()));
                if (ExtSql.Length > 1)
                {
                    str_sql += " and " + ExtSql;
                }
                str_sql += Order;
                List<Question> qlist = QuestionView.GetModelList(str_sql, count.ToInt32());
                StringBuilder sb = new StringBuilder();
                foreach (Question q in qlist)
                {
                    string title = q.Title;
                    if (TitleLength.ToInt32() > 0)
                    {
                        title = title.CutString(TitleLength.ToInt32());
                    }
                    string timespan = "";
                    if (showTime.ToInt32() > 0)
                    {
                        timespan = string.Format("<span class=\"news_time_span\">{0}</span>", q.AskTime.ToString("yyyy/MM/dd"));
                    }

                    sb.AppendLine(string.Format("<li>{0}{1}<a href='{2}' title='{3}'>{4}</a></li>", TitlePreChar, timespan, BasePage.GetQuestionUrl(q, q.GetClass()), q.Title, title));
                }
                return sb.ToS();
            }
            else
            {
                return "未知模型";
            }
        }

        /// <summary>
        /// 获取所有子栏目
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        protected string GetAllSubClass(int ClassID)
        {
            var cls = ClassView.GetModelList(string.Format("ParentID={0}", ClassID));
            StringBuilder sb = new StringBuilder();
            foreach (var c in cls)
            {
                sb.Append(c.ID + ",");
            }
            sb.Append(ClassID);
            return sb.ToS();
        }
        #endregion

        #region 通过关键词读取新闻
        /// <summary>
        /// 通过关键词读取新闻
        /// </summary>
        /// <param name="count"></param>
        /// <param name="TitleLength"></param>
        /// <param name="showTime"></param>
        /// <param name="key"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public string getnewsbykeywords(string count, string TitleLength, string showTime, string key, string Order)
        {


            string str_sql = "";
            str_sql += "(";
            string[] keys = Regex.Replace(key, "\\s+", ",").Split(',');
            foreach (string k in keys)
            {
                str_sql += " keywords like '%" + k + "%' or ";
            }
            str_sql += " 1=2)";



            List<News> nlist = NewsView.GetModelList(str_sql + " " + Order, count.ToInt32());
            StringBuilder sb = new StringBuilder();
            foreach (News n in nlist)
            {
                string title = n.Title;
                if (TitleLength.ToInt32() > 0)
                {
                    title = title.CutString(TitleLength.ToInt32());
                }
                string timespan = "";
                if (showTime.ToInt32() > 0)
                {
                    timespan = string.Format("<span class=\"news_time_span\">{0}</span>", n.NewsTime.ToString("yyyy/MM/dd"));
                }

                sb.AppendLine(string.Format("<li>{0}{1}<a href='{2}' title='{3}'>{4}</a></li>", "", timespan, BasePage.GetNewsUrl(n, NewsView.GetNewsClass(n)), n.Title, title));
            }
            return sb.ToS();

        }
        #endregion

        #region 轮播Flash
        /// <summary>
        /// 轮播Flash
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="count"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="showTitle"></param>
        /// <param name="titleLength"></param>
        /// <param name="interval"></param>
        /// <param name="ExtSql"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public static string cmsflashpic(string ClassID, string count, string width, string height, string showTitle, string titleLength, string interval, string ExtSql, string Order)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!--开始FLASH-->");
            sb.AppendLine("<div class=\"flash\">");
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("<!--");

            sb.AppendLine(string.Format("var interval_time={0};", interval));
            sb.AppendLine(string.Format("var focus_width={0};", width));
            sb.AppendLine(string.Format("var focus_height={0};", height));
            if (showTitle.ToInt32() > 0)
            {
                sb.AppendLine("var text_height=20;");
            }
            else
            {
                sb.AppendLine("var text_height=0;");
            }
            sb.AppendLine("var text_align=\"center\";");
            sb.AppendLine("var swf_height = focus_height+text_height;");
            sb.AppendLine("var swfpath=\"/e/data/images/pixviewer.swf\";");
            sb.AppendLine("var swfpatha=\"/e/data/images/pixviewer.swf\";");

            StringBuilder sb_pics = new StringBuilder();
            StringBuilder sb_links = new StringBuilder();
            StringBuilder sb_texts = new StringBuilder();

            #region 图片变量
            sb_pics.Append("var pics=\"");
            sb_links.Append("var links=\"");
            sb_texts.Append("var texts=\"");

            string str_sql = string.Format("(ZtID='{0}' or ClassID='{1}') and len(TitleImage)>0", ClassID, ClassID);
            if (ExtSql.Length > 0)
            {
                str_sql += " and " + ExtSql;
            }
            if (Order.Length > 0)
            {
                str_sql += " order by " + Order;
            }

            List<News> newses = NewsView.GetModelList(str_sql, count.ToInt32());
            newses = newses.Where(p => p.TitleImage.IndexOf(".gif") < 0).ToList();//不支持GIF文件
            foreach (News n in newses)
            {
                sb_pics.Append(n.TitleImage + "|");
                sb_links.Append(BasePage.GetNewsUrl(n, ClassView.GetModelByID(ClassID)) + "|");
                sb_texts.Append(n.Title.CutString(titleLength.ToInt32()) + "|");
            }
            sb_pics = sb_pics.TrimEnd('|');
            sb_links = sb_links.TrimEnd('|');
            sb_texts = sb_texts.TrimEnd('|');

            sb_pics.Append("\";\n");
            sb_links.Append("\";\n");
            sb_texts.Append("\";\n");

            sb.Append(sb_pics.ToString());
            sb.Append(sb_links.ToString());
            sb.Append(sb_texts.ToString());
            #endregion

            #region 输出Flash
            sb.AppendLine("document.write('<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\" width=\"'+ focus_width +'\" height=\"'+ swf_height +'\">'); ");
            sb.AppendLine("document.write('<param name=\"movie\" value=\"'+swfpath+'\"><param name=\"quality\" value=\"high\"><param name=\"bgcolor\" value=\"#ffffff\">'); ");
            sb.AppendLine("document.write('<param name=\"menu\" value=\"false\"><param name=wmode value=\"opaque\">'); ");
            sb.AppendLine("document.write('<param name=\"FlashVars\" value=\"pics='+pics+'&links='+links+'&texts='+texts+'&borderwidth='+focus_width+'&borderheight='+focus_height+'&textheight='+text_height+'&text_align='+text_align+'&interval_time='+interval_time+'\">'); ");
            sb.AppendLine("document.write('<embed src=\"'+swfpath+'\" wmode=\"opaque\" FlashVars=\"pics='+pics+'&links='+links+'&texts='+texts+'&borderwidth='+focus_width+'&borderheight='+focus_height+'&textheight='+text_height+'&text_align='+text_align+'&interval_time='+interval_time+'\" menu=\"false\" bgcolor=\"#ffffff\" quality=\"high\" width=\"'+ focus_width +'\" height=\"'+ swf_height +'\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />');");
            sb.AppendLine("document.write('</object>');");
            #endregion

            sb.AppendLine("//-->");
            sb.AppendLine("</script>");
            sb.AppendLine("</div>");
            return sb.ToString();
        }
        #endregion

        #region 获取栏目列表
        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <param name="TitlePreChar">前置字符串</param>
        /// <param name="TitleLength">栏目名截取长度</param>
        /// <returns></returns>
        public static string cmsclasslist(string TitlePreChar, string TitleLength)
        {
            int cutString = TitleLength.ToInt32(50);
            StringBuilder sb = new StringBuilder();
            List<Class> cls = NewsAction.NewsClass.Where(p => p.IsLeafClass && p.ShowInNav).ToList();
            foreach (Class c in cls)
            {
                sb.AppendLine(string.Format("<li>{0}<a href=\"{1}\">{2}</a></li>",
                    TitlePreChar,
                    BasePage.GetClassUrl(c),
                    c.ClassName.CutString(cutString)
                    ));
            }

            return sb.ToString();
        }
        #endregion

        #region 获取子栏目
        /// <summary>
        /// 获取子栏目
        /// </summary>
        /// <param name="TitlePreChar">前置字符串</param>
        /// <param name="TitleLength"></param>
        /// <param name="ClassID">栏目名截取长度</param>
        /// <returns></returns>
        public static string GetSubClass(string TitlePreChar, string TitleLength, string ClassID)
        {
            int cutString = TitleLength.ToInt32(50);
            StringBuilder sb = new StringBuilder();
            List<Class> cls = ClassView.GetSubClass(ClassView.GetModelByID(ClassID));
            foreach (Class c in cls)
            {
                sb.AppendLine(string.Format("<li>{0}<a href=\"{1}\">{2}</a></li>",
                    TitlePreChar,
                    BasePage.GetClassUrl(c),
                    c.ClassName.CutString(cutString)
                    ));
            }

            return sb.ToString();
        }
        #endregion

        #region  创建菜单
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public static string buildmenustring(string parentID)
        {
            StringBuilder sb = new StringBuilder();

            var cls = NewsAction.NewsClass.Where(p => p.ShowInNav && p.ParentID.ToString() == parentID).ToList();
            if (cls.Count > 0)
            {
                foreach (Class cl in cls)
                {
                    sb.AppendLine("<li>");
                    if (cl.IsLeafClass)
                    {
                        sb.AppendLine(string.Format("<span><a href=\"{0}\">{1}</a></span>", BasePage.GetClassUrl(cl), cl.ClassName));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("<span><a href=\"{0}\">{1}</a></span>", "javascript:void(0)", cl.ClassName));
                    }


                    if (NewsAction.NewsClass.Where(p => p.ParentID == cl.ID).Count() > 0)
                    {
                        sb.AppendLine("<ul>" + buildmenustring(cl.ID.ToString()) + "</ul>");
                    }
                    sb.AppendLine("</li>");

                }
            }

            return sb.ToString();

        }
        #endregion

        #region 友情链接
        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        public static string getlink(string n)
        {
            StringBuilder sb = new StringBuilder("");
            var links = LinkView.GetModelList("id>0 order by [Index]");
            foreach (var l in links)
            {
                sb.AppendLine(string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a> ", l.Url, l.LinkTitle));
            }
            return sb.ToString();
        }
        #endregion

        #region 生成新闻缩略图
        /// <summary>
        /// 生成新闻缩略图
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        public static string getnewsshortimg(string NewsID)
        {
            News news = NewsView.GetModelByID(NewsID);
            Voodoo.Model.File file = FileView.Find(string.Format("FilePath='{0}'", news.TitleImage));

            string result = "<img src='{0}' alt='{1}' />";
            if (!file.SmallPath.IsNullOrEmpty())//有缩略图
            {
                result = string.Format(result, file.SmallPath, news.Title);
            }
            else
            {
                result = "";
            }
            return result;
        }
        #endregion

        #region 获取小说栏目列表
        /// <summary>
        /// 获取小说栏目列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string getallnovelclass(string str)
        {
            List<Class> cls = ClassView.GetModelList("ModelID=4");
            StringBuilder sb = new StringBuilder();
            foreach (Class c in cls)
            {
                sb.Append(string.Format("<a href=\"{0}\">{1}</a> ", BasePage.GetClassUrl(c), c.ClassName));
            }
            return sb.ToS();
        }
        #endregion 获取小说栏目列表

        #region 获取最新更新的书籍
        /// <summary>
        /// 获取最新更新的书籍 Metro风格
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public string getnoveltopmetroupdate(string top)
        {
            int i_top = top.ToInt32();
            List<Book> bs = BookView.GetModelList("Enable=1 order by UpdateTime desc", i_top);
            StringBuilder sb = new StringBuilder();

            foreach (Book b in bs)
            {
                Class c = BookView.GetClass(b);
                sb.AppendLine(string.Format("<li style=\" background-color:{0};\"><div><h1><a href=\"{1}\">{2}</a></h1><div><div class=\"class\"><a href=\"{3}\">{4}</a></div><div class=\"lastchapter\"><a href=\"{5}\" title=\"{6}\">{7}</a></div><div class=\"author\">{8}</div><div class=\"time\">{9}</div></div></div></li>",
                    BasePage.RandomBGColor(),
                    BasePage.GetBookUrl(b, c),
                    b.Title,
                    BasePage.GetClassUrl(c),
                    b.ClassName,
                    BasePage.GetBookChapterUrl(BookChapterView.GetModelByID(b.LastChapterID.ToS()), c),
                    b.LastChapterTitle,
                    b.LastChapterTitle.CutString(12),
                    b.Author,
                    b.UpdateTime.ToString("yyyy-MM-dd HH:mm")
                    ));
            }
            return sb.ToS();
        }

        /// <summary>
        /// 获取最新更新的书籍
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public string getnoveltopupdate(string top)
        {
            int i_top = top.ToInt32();
            List<Book> bs = BookView.GetModelList("Enable=1 order by UpdateTime desc", i_top);
            StringBuilder sb = new StringBuilder();

            foreach (Book b in bs)
            {
                Class c = BookView.GetClass(b);
                sb.AppendLine(string.Format("<tr><td>[<a href=\"{0}\">{1}</a>]</td><td> <a href=\"{2}\">{3}</a></td><td><a href=\"{4}\">{5}</a></td><td>{6}</td><td>[{7}]</td></tr>",
                    BasePage.GetClassUrl(c),
                    b.ClassName,
                    BasePage.GetBookUrl(b, c),
                    b.Title,
                    BasePage.GetBookChapterUrl(BookChapterView.GetModelByID(b.LastChapterID.ToS()), c),
                    b.LastChapterTitle,
                    b.Author,
                    b.UpdateTime.ToString("MM-dd")
                    ));
            }
            return sb.ToS();
        }

        #endregion 获取最新更新的书籍
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            string str_sql = ExtSql + " " + Order;
            if (str_sql.Trim().IsNullOrEmpty())
            {
                str_sql = "1=1";
            }
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
                    timespan = string.Format("<span class=\"news_time_span\">{0}</span>", n.NewsTime.ToString());
                }

                sb.AppendLine(string.Format("<li>{0}<a href='{1}'>{2}</a>{3}</li>", TitlePreChar, BasePage.GetNewsUrl(n, cls), title, timespan));
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
                sb.AppendLine("var text_height=30;");
            }
            else
            {
                sb.AppendLine("var text_height=0;");
            }
            sb.AppendLine("var text_align=\"center\";");
            sb.AppendLine("var swf_height = focus_height+text_height;");
            sb.AppendLine("var swfpath=\"/cms/e/data/images/pixviewer.swf\";");
            sb.AppendLine("var swfpatha=\"/cms/e/data/images/pixviewer.swf\";");

            StringBuilder sb_pics = new StringBuilder();
            StringBuilder sb_links = new StringBuilder();
            StringBuilder sb_texts = new StringBuilder();

            #region 图片变量
            sb_pics.AppendLine("var pics=\"");
            sb_links.AppendLine("var links=\"");
            sb_texts.AppendLine("var texts=\"");

            string str_sql = "ZtID='{0}' or ClassID='{1}'";
            if (ExtSql.Length > 0)
            {
                str_sql += " and " + ExtSql;
            }
            if (Order.Length > 0)
            {
                str_sql += " order by " + Order;
            }

            List<News> newses = NewsView.GetModelList(str_sql, count.ToInt32());
            foreach (News n in newses)
            {
                sb_pics.Append(n.TitleImage + "|");
                sb_links.Append(n.NavUrl.Length > 0 ? n.NavUrl : n.FileForder + n.FileName + "|");
                sb_texts.Append(n.Title.CutString(titleLength.ToInt32()) + "|");
            }
            sb_pics = sb_pics.TrimEnd('|');
            sb_links = sb_links.TrimEnd('|');
            sb_texts = sb_texts.TrimEnd('|');
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

        #region  创建菜单
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public static string buildmenustring(string parentID)
        {
            StringBuilder sb = new StringBuilder();

            var cls = NewsAction.NewsClass.Where(p => p.ShowInNav&&p.ParentID.ToString()==parentID).ToList();
            if (cls.Count > 0)
            {
                foreach (Class cl in cls)
                {
                    sb.AppendLine("<li>");
                    sb.AppendLine(string.Format("<span><a href=\"{0}\">{1}</a></span>", BasePage.GetClassUrl(cl), cl.ClassName));
                    sb.AppendLine("</li>");
                    if(NewsAction.NewsClass.Where(p => p.ParentID==cl.ID).Count()>0)
                    {
                        sb.AppendLine("<ul>" + buildmenustring(cl.ID.ToString()) + "</ul>");
                    }
                    
                }
            }

            return sb.ToString();

        }
        #endregion 
    }
}

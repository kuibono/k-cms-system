using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Security;
using Voodoo.IO;

using System.Reflection;
using System.Text.RegularExpressions;

namespace Voodoo.Basement
{
    public class CreatePage
    {
        #region 默认模板组
        /// <summary>
        /// 默认模板组
        /// </summary>
        public static TemplateGroup DefaultGroup
        {
            get
            {
                return TemplateGroupView.Find("IsDefault=1");
            }
        }
        #endregion

        #region 页面模板类型 enum TempType
        /// <summary>
        /// 页面模板类型 enum TempType
        /// </summary>
        public enum TempType
        {
            首页,
            控制面板,
            全站搜索,
            高级搜索,
            横向搜索JS,
            纵向搜索JS,
            相关信息,
            留言板,
            评论js调用,
            最终下载页,
            下载地址,
            在线播放地址,
            列表分页,
            登陆状态,
            JS调用登陆,
            封面,
            内容,
            列表,
            搜索
        }
        #endregion


        #region  创建首页
        /// <summary>
        /// 创建首页
        /// </summary>
        /// <returns></returns>
        public static string GreateIndexPage()
        {
            SysSetting setting = BasePage.SystemSetting;

            string IndexFile = "~/index" + setting.ExtName;

            string Content = "";
            Content = GetTempateString(1, TempType.首页);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = "首页", UpdateTime = DateTime.Now.ToString() };

            Content = ReplacePageAttribute(Content, pa);

            Content = ReplaceTagContent(Content);

            //BasePage.SystemSetting.ExtName
            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~/Index") + BasePage.SystemSetting.ExtName, Content);


            return Content;
        }
        #endregion

        #region 生成内容页
        /// <summary>
        /// 生成内容页
        /// </summary>
        /// <param name="news"></param>
        /// <param name="cls"></param>
        public static void CreateContentPage(News news, Class cls)
        {
            if (news.NavUrl.Trim().Length > 0)//如果是外部连接新闻 则不需要生成
            {
                return;
            }

            string FileName = BasePage.GetNewsUrl(news, cls);
            string Content = "";

            int tmpid = news.ModelID;

            TemplateContent temp = new TemplateContent();
            if (tmpid <= 0)
            {
                //没有选择模版
                tmpid = TemplateContentView.Find("id>0 order by id desc").ID;
            }
            temp = TemplateContentView.GetModelByID(tmpid.ToS());

            Content = GetTempateString(tmpid, TempType.内容);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = news.Title, UpdateTime = DateTime.Now.ToString(), Description = news.Description, Keyword = news.KeyWords };

            Content = ReplacePageAttribute(Content, pa);

            

            #region 替换新闻内容

            string title = "<font color='#" + news.TitleColor + "'>" + news.Title + "</font>";
            if (news.TitleB)
            {
                title = "<strong>" + title + "</strong>";
            }
            if (news.TitleI)
            {
                title = "<I>" + title + "</I>";
            }
            if (news.TitleS)
            {
                title = "<STRIKE>" + title + "</STRIKE>";
            }
            Content = Content.Replace("[!--class.id--]", cls.ID.ToString());
            Content = Content.Replace("[!--class.name--]", cls.ClassName);

            Content = Content.Replace("[!--news.id--]", news.ID.ToString());
            Content = Content.Replace("[!--news.title--]", title);
            Content = Content.Replace("[!--news.newstime--]", news.NewsTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--news.source--]", news.Source);
            Content = Content.Replace("[!--news.author--]", news.Author);
            Content = Content.Replace("[!--news.content--]", news.Content);
            Content = Content.Replace("[!--news.keywords--]", news.KeyWords);
            Content = Content.Replace("[!--news.description--]", news.Description);
            #endregion

            Content = ReplaceTagContent(Content);

            #region 上一篇  下一篇 链接
            News news_pre = BasePage.GetPreNews(news, cls);
            News news_next = BasePage.GetNextNews(news, cls);

            //上一篇
            string pre_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_pre != null)
            {
                pre_link = string.Format("<a href=\"{0}\">{1}</a>", BasePage.GetNewsUrl(news_pre, cls), news_pre.Title);
            }
            Content = Content.Replace("[!--news.prelink--]", pre_link);

            //下一篇
            string next_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_next != null)
            {
                next_link = string.Format("<a href=\"{0}\">{1}</a>", BasePage.GetNewsUrl(news_next, cls), news_next.Title);
            }
            Content = Content.Replace("[!--news.nextlink--]", next_link);

            #endregion

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(cls));

            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~" + FileName), Content);
        }
        #endregion

        #region 创建列表页面
        /// <summary>
        /// 创建列表页面
        /// </summary>
        /// <param name="c"></param>
        /// <param name="page"></param>
        public static void CreateListPage(Class c, int page)
        {
            int pagecount = 1;
            int recordCount = 0;
            string Content = "";
            int tmpid = 0;
            TemplateList temp = new TemplateList();
            if (c.ModelID <= 0)
            {
                //没有选择模版
                tmpid = TemplateListView.Find("id>0 order by id desc").ID;
            }
            temp = TemplateListView.GetModelByID(tmpid.ToS());

            Content = GetTempateString(tmpid, TempType.列表);

            Content = ReplacePublicTemplate(Content);

            Content = ReplaceSystemSetting(Content);

            PageAttribute pa = new PageAttribute() { Title = c.ClassName, UpdateTime = DateTime.Now.ToString(), Description = c.ClassDescription, Keyword = c.ClassKeywords };

            Content = Content.Replace("[!--class.description--]", c.ClassDescription);
            Content = Content.Replace("[!--class.classname--]", c.ClassName);
            Content = ReplacePageAttribute(Content, pa);

            #region 替换列表

            StringBuilder sb_list = new StringBuilder();
            List<News> ns = NewsView.GetModelList(string.Format("ClassID={0} and Audit=1 order by SetTop desc, id desc", c.ID)).ToList();

            pagecount = (Convert.ToDouble(ns.Count) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();
            recordCount = ns.Count;

            ns = ns.Skip((page - 1) * temp.ShowRecordCount).Take(temp.ShowRecordCount).ToList();
            foreach (News n in ns)
            {
                //<li><span>[!--newstime--]</span><a href="[!--titleurl--]" title="[!--oldtitle--]">[!--title--]</a> </li>

                string _title = "<font color='#" + n.TitleColor + "'>" + n.Title + "</font>";
                if (n.TitleB)
                {
                    _title = "<strong>" + _title + "</strong>";
                }
                if (n.TitleI)
                {
                    _title = "<I>" + _title + "</I>";
                }
                if (n.TitleS)
                {
                    _title = "<STRIKE>" + _title + "</STRIKE>";
                }

                string str_lst = temp.ListVar;
                str_lst = str_lst.Replace("[!--newstime--]", n.NewsTime.ToString(temp.TimeFormat));
                str_lst = str_lst.Replace("[!--titleurl--]", BasePage.GetNewsUrl(n, c));
                str_lst = str_lst.Replace("[!--oldtitle--]", _title);
                str_lst = str_lst.Replace("[!--description--]", n.Description);
                str_lst = str_lst.Replace("[!--author--]", n.Author);
                str_lst = str_lst.Replace("[!--id--]", n.ID.ToS());
                string title = n.Title;
                if (temp.CutTitle > 0)
                {
                    title = title.CutString(temp.CutTitle);
                }
                str_lst = str_lst.Replace("[!--title--]", n.Title);
                sb_list.AppendLine(str_lst);
            }

            Content = Content.Replace("<!--list.var-->", sb_list.ToString());

            #endregion


            //替换标签变量
            Content = ReplaceTagContent(Content);

            #region 替换分页模板

            string tmp_pager = GetTempateString(tmpid, TempType.列表分页);
            tmp_pager = tmp_pager.Replace("[!--thispage--]", page.ToS());
            tmp_pager = tmp_pager.Replace("[!--pagenum--]", pagecount.ToS());
            tmp_pager = tmp_pager.Replace("[!--lencord--]", temp.ShowRecordCount.ToS());
            tmp_pager = tmp_pager.Replace("[!--num--]", recordCount.ToS());
            tmp_pager = tmp_pager.Replace("[!--pagelink--]", BuildPagerLink(c, page));
            tmp_pager = tmp_pager.Replace("[!--options--]", BuidPagerOption(c, page));

            Content = Content.Replace("[!--show.listpage--]", tmp_pager);

            #endregion

            //替换导航条
            Content = Content.Replace("[!--newsnav--]", BuildClassNavString(c));

            string FileName = BasePage.GetClassUrl(c, page);
            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath(FileName), Content);


            //下一页链接
            if (pagecount > page)
            {
                CreateListPage(c, page + 1);
            }
        }
        #endregion

        #region  创建页头和页尾 CreateHeaderString CreateFooterString
        public static string CreateHeaderString(string title)
        {
            string Content = "";
            Content += GetPublicTemplate("header");


            PageAttribute pa = new PageAttribute() { Title = title, UpdateTime = DateTime.Now.ToString() };

            Content = ReplacePageAttribute(Content, pa);

            Content = ReplaceTagContent(Content);

            Content = ReplaceSystemSetting(Content);

            return Content;
        }

        public static string CreateFooterString()
        {
            string Content = "";
            Content += GetPublicTemplate("footer");


   
            Content = ReplaceTagContent(Content);

            Content = ReplaceSystemSetting(Content);

            return Content;
        }
        #endregion 

        #region  创建用户功能菜单
        public static string BuildUserMenuString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("<li><a href=\"#\">会员登陆</a></li>");
            sb.AppendLine("<li><a href=\"/e/member/ChangeRegister.aspx\">注册帐号</a></li>");
            sb.AppendLine("<li><a href=\"/e/post/PostList.aspx\">我的文章</a></li>");
            sb.AppendLine("<li><a href=\"/e/post/PostNews.aspx\">发布投稿</a></li>");

            return sb.ToString();
        }
        #endregion

        #region  创建 上页 下页 首页 尾页链接
        /// <summary>
        /// 创建分页链接
        /// </summary>
        /// <param name="c"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string BuildPagerLink(Class c, int page)
        {
            int tmpid = 0;
            TemplateList temp = new TemplateList();
            if (c.ModelID <= 0)
            {
                //没有选择模版
                tmpid = TemplateListView.Find("id>0 order by id desc").ID;
            }
            temp = TemplateListView.GetModelByID(tmpid.ToS());
            List<News> ns = NewsView.GetModelList(string.Format("ClassID={0} and Audit=1", c.ID)).ToList();

            int pagecount = (Convert.ToDouble(ns.Count) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();
            int recordCount = ns.Count;

            string str_first = string.Format("[<a href=\"{0}\">首页</a>]", page > 1 ? "index" + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_pre = string.Format("[<a href=\"{0}\">上页</a>]", page > 1 ? "index" + (page == 2 ? "" : "_" + (page - 1).ToS()) + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_next = string.Format("[<a href=\"{0}\">下页</a>]", page < pagecount ? "index_" + (page + 1).ToS() + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            string str_end = string.Format("[<a href=\"{0}\">尾页</a>]", page != pagecount ? "index_" + pagecount.ToS() + BasePage.SystemSetting.ExtName : "javascript:void(0)");
            return string.Format("{0} {1} {2} {3}", str_first, str_pre, str_next, str_end);
        }
        #endregion 

        #region 创建跳转下拉菜单
        /// <summary>
        /// 创建跳转下拉菜单
        /// </summary>
        /// <param name="c"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string BuidPagerOption(Class c, int page)
        {
            int tmpid = 0;
            TemplateList temp = new TemplateList();
            if (c.ModelID <= 0)
            {
                //没有选择模版
                tmpid = TemplateListView.Find("id>0 order by id desc").ID;
            }
            temp = TemplateListView.GetModelByID(tmpid.ToS());
            List<News> ns = NewsView.GetModelList(string.Format("ClassID={0} and Audit=1", c.ID)).ToList();

            int pagecount = (Convert.ToDouble(ns.Count) / Convert.ToDouble(temp.ShowRecordCount)).YueShu();
            int recordCount = ns.Count;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<select onchange='location.href=this.value'>");
            for (int i = 1; i <= pagecount; i++)
            {
                if (page == i)
                {
                    sb.AppendLine(string.Format("<option value='index{0}' selected>{1}</option>", (i > 1 ? "_" + i.ToS() : "") + BasePage.SystemSetting.ExtName, i.ToS()));
                }
                else
                {
                    sb.AppendLine(string.Format("<option value='index{0}'>{1}</option>", (i > 1 ? "_" + i.ToS() : "") + BasePage.SystemSetting.ExtName, i.ToS()));
                }
            }
            sb.AppendLine("</select>");
            return sb.ToS();
        }
        #endregion 

        #region  创建导航条
        /// <summary>
        /// 创建类导航
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string BuildClassNavString(Class c)
        {
            string str = "";
            if (c.IsLeafClass)
            {
                str = string.Format("> <a href=\"{0}\">{1}</a>", BasePage.GetClassUrl(c), c.ClassName);
            }
            else
            {
                str = string.Format("> <a href=\"{0}\">{1}</a>", "javascript:void(0);", c.ClassName);
            }
            
            var cls = NewsAction.NewsClass.Where(p => p.ID == c.ParentID && c.ShowInNav).ToList();
            if (cls.Count > 0)
            {
                foreach (Class cl in cls)
                {
                    
                    str = BuildClassNavString(cl) + str;
                }
            }
            //str = "<a href=\"/\">首页</a>" + str;
            return str;

        }
        #endregion


        #region 替换公共模版变量
        /// <summary>
        /// 替换公共模版变量
        /// </summary>
        /// <param name="TmpString">模版内容</param>
        /// <returns></returns>
        public static string ReplacePublicTemplate(string TmpString)
        {
            Match mc_pubic = new Regex("\\[\\!--temp.(?<key>.*?)--\\]", RegexOptions.None).Match(TmpString);
            while (mc_pubic.Success)
            {
                TmpString = Regex.Replace(
                    TmpString,
                    string.Format("\\[\\!--temp\\.{0}--\\]", mc_pubic.Groups["key"].Value),
                    GetPublicTemplate(mc_pubic.Groups["key"].Value)
                    );
                mc_pubic = mc_pubic.NextMatch();
            }

            return TmpString;
        }
        #endregion

        #region 替换系统参数
        /// <summary>
        /// 替换系统参数
        /// </summary>
        /// <param name="TmpString"></param>
        /// <returns></returns>
        public static string ReplaceSystemSetting(string TmpString)
        {
            Match mc_sys = new Regex("\\[\\!--sys.(?<key>.*?)--\\]", RegexOptions.None).Match(TmpString);
            while (mc_sys.Success)
            {
                TmpString = Regex.Replace(
                    TmpString,
                    string.Format("\\[\\!--sys\\.{0}--\\]", mc_sys.Groups["key"].Value),
                    GetSysSettingContent(mc_sys.Groups["key"].Value)
                    );
                mc_sys = mc_sys.NextMatch();
            }

            return TmpString;
        }
        #endregion

        #region 替换标签
        /// <summary>
        /// 替换标签
        /// </summary>
        /// <param name="TmpString"></param>
        /// <returns></returns>
        public static string ReplaceTagContent(string TmpString)
        {
            //Match mc = new Regex("\\[[^\\]]\\][^\\]\\[]*\\[/[^\\[\\]]\\]", RegexOptions.None).Match(TmpString);
            Match mc = new Regex("\\[(?<key>.*?)\\](?<key2>.*?)\\[/(?<key3>.*?)\\]", RegexOptions.None).Match(TmpString);
            while (mc.Success)
            {
                //Match mc_tag = new Regex("\\[(?<key>.*?)\\](?<key2>.*?)\\[/(?<key3>.*?)\\]", RegexOptions.None).Match(mc.Groups[0].Value);

                //if (mc_tag.Success && mc_tag.Groups["key"].Value == mc_tag.Groups["key3"].Value)
                if (mc.Groups["key"].Value == mc.Groups["key3"].Value)
                {
                    TmpString = TmpString.Replace(
                        mc.Groups[0].Value,
                        GetTagContent(string.Format("[{0}]{1}[/{0}]", mc.Groups["key"].Value, mc.Groups["key2"].Value))
                        );

                }

                mc = mc.NextMatch();
            }

            return TmpString;

        }
        #endregion

        #region 替换页面属性 如页面标题等
        /// <summary> 
        /// 替换页面属性
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static string ReplacePageAttribute(string Content, PageAttribute pa)
        {
            Content = Content.Replace("[!--page.title--]", pa.Title);
            Content = Content.Replace("[!--page.updatetime--]", pa.UpdateTime);
            Content = Content.Replace("[!--page.description--]", pa.Description);
            Content = Content.Replace("[!--page.keyword--]", pa.Keyword);
            return Content;
        }
        #endregion







        #region 获取模板内容字符串
        /// <summary>
        /// 获取模板内容字符串
        /// </summary>
        /// <param name="TempID">模板的ID</param>
        /// <param name="PageType">模板页面的类型</param>
        /// <returns></returns>
        public static string GetTempateString(int TempID, TempType PageType)
        {
            TemplatePublic tp = TemplatePublicView.GetModelByID(TempID.ToString());
            if (TempID <= 0)
            {
                tp = TemplatePublicView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
            }

            switch (PageType)
            {
                case TempType.JS调用登陆:
                    return tp.JSLogin;
                    break;
                case TempType.登陆状态:
                    return tp.LoginStatus;
                    break;
                case TempType.封面:
                    TemplateFace tf = TemplateFaceView.GetModelByID(TempID.ToS());
                    if (TempID <= 0)
                    {
                        tf = TemplateFaceView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
                    }
                    return tf.Content;
                    break;
                case TempType.高级搜索:
                    return tp.AdvancedSearch;
                    break;
                case TempType.横向搜索JS:
                    return tp.HorizontaSearch;
                    break;
                case TempType.控制面板:
                    return tp.Controlcontent;
                    break;
                case TempType.列表:
                    TemplateList tl = TemplateListView.GetModelByID(TempID.ToS());
                    if (TempID <= 0)
                    {
                        tl = TemplateListView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
                    }
                    return tl.Content;
                case TempType.列表分页:
                    return tp.ListPager;
                    break;
                case TempType.留言板:
                    return tp.MessageBoard;
                    break;
                case TempType.内容:
                    TemplateContent tc = TemplateContentView.GetModelByID(TempID.ToS());
                    if (TempID <= 0)
                    {
                        tc = TemplateContentView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
                    }
                    return tc.Content;
                    break;
                case TempType.评论js调用:
                    return tp.Reply;
                    break;
                case TempType.全站搜索:
                    return tp.SiteSearchContent;
                    break;
                case TempType.首页:
                    return tp.IndexContent;
                    break;
                case TempType.搜索:
                    TemplateSearch ts = TemplateSearchView.GetModelByID(TempID.ToS());
                    if (TempID <= 0)
                    {
                        ts = TemplateSearchView.Find(string.Format("GroupID={0}", DefaultGroup.ID.ToS()));
                    }
                    return ts.Content;
                    break;
                case TempType.下载地址:
                    return tp.DownAddress;
                    break;
                case TempType.相关信息:
                    return tp.RelationInfo;
                    break;
                case TempType.在线播放地址:
                    return tp.OLPlayaddress;
                    break;
                case TempType.纵向搜索JS:
                    return tp.VerticalSearch;
                    break;
                case TempType.最终下载页:
                    return tp.FinalDown;
                    break;
                default:
                    return "";
                    break;
            }
        }
        #endregion

        #region 获取公共模板变量字符串
        /// <summary>
        /// 获取公共模板变量字符串
        /// </summary>
        /// <param name="VarName"></param>
        /// <returns></returns>
        public static string GetPublicTemplate(string VarName)
        {
            return TemplateVarView.Find(string.Format("VarName='{0}'", VarName)).Content.ToS();
        }
        #endregion

        #region 系统参数
        /// <summary>
        /// 系统参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSysSettingContent(string key)
        {
            SysSetting ss = BasePage.SystemSetting;
            switch (key.ToLower())
            {
                case "siteurl":
                    return ss.SiteUrl;
                    break;
                case "keywords":
                    return ss.KeyWords;
                    break;
                case "description":
                    return ss.Description;
                    break;
                case "copyright":
                    return ss.Copyright;
                    break;
                case "countcode":
                    return ss.CountCode;
                    break;
                case "filedirstring":
                    return ss.FileDirString;
                    break;
                case "extname":
                    return ss.ExtName;
                    break;
                case "siteclosemsg":
                    return ss.SiteCloseMsg;
                    break;
                case "sitename":
                    return ss.SiteName;
                    break;
                default:
                    return "";
            }
        }
        #endregion

        #region 替换标签内容
        /// <summary>
        /// 替换标签内容
        /// </summary>
        /// <param name="tag">标签</param>
        /// <returns>结果</returns>
        public static string GetTagContent(string tag)
        {
            //[方法]xx,xx,xx,xx[/方法]
            string functionName = "";
            object[] pars;

            #region  获取方法名
            Match mc_FunctionName = new Regex("\\[(?<key>.*?)\\]", RegexOptions.None).Match(tag);
            if (mc_FunctionName.Success)
            {
                functionName = mc_FunctionName.Groups["key"].Value;
            }
            Match mc_FunctionNameR = new Regex("\\[/(?<key>.*?)\\]", RegexOptions.None).Match(tag);
            if (mc_FunctionNameR.Success)
            {
                if (mc_FunctionName.Groups["key"].Value != mc_FunctionNameR.Groups["key"].Value)
                {
                    functionName = "ERR";
                }
                else
                {
                    functionName = mc_FunctionName.Groups["key"].Value;
                }
            }
            else
            {
                functionName = "ERR";
            }
            #endregion

            #region 获取参数
            Match mc_pars = new Regex("\\[.*\\](?<key>.*?)\\[/.*\\]", RegexOptions.None).Match(tag);
            if (mc_pars.Success)
            {
                pars = mc_pars.Groups["key"].Value.Split(',');
            }
            else
            {
                pars = new object[] { "" };
            }

            #endregion

            try
            {
                return ExecMethod("Voodoo.Basement.Functions", functionName, pars).ToS();
            }
            catch
            {
                return "标签使用错误";
            }

        }
        #endregion




        #region 执行某个方法
        /// <summary>
        /// 执行某个方法
        /// </summary>
        /// <param name="className">类，包括命名空间</param>
        /// <param name="methodName">方法名</param>
        /// <param name="objParas">参数</param>
        /// <returns></returns>
        protected static object ExecMethod(string className, string methodName, object[] objParas)
        {
            Type t = Type.GetType(className);
            /*实例化这个类*/
            ConstructorInfo constructor = t.GetConstructor(new Type[0]);//将得到的类型传给一个新建的构造器类型变量
            object obj = constructor.Invoke(new object[0]);//使用构造器对象来创建对象
            /*执行Insert方法*/
            MethodInfo m = t.GetMethod(methodName);
            return m.Invoke(obj, objParas);
        }
        #endregion

    }
}

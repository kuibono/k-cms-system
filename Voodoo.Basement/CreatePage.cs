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

            PageAttribute pa = new PageAttribute() { Title = news.Title, UpdateTime = DateTime.Now.ToString(), Description=news.Description, Keyword=news.KeyWords };

            Content = ReplacePageAttribute(Content, pa);

            Content = ReplaceTagContent(Content);

            #region 替换新闻内容

            string title = "<font color='#"+ news.TitleColor +"'>"+news.Title+"</font>";
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

            Content = Content.Replace("[!--news.title--]", title);
            Content = Content.Replace("[!--news.newstime--]", news.NewsTime.ToString(temp.TimeFormat));
            Content = Content.Replace("[!--news.source--]", news.Source);
            Content = Content.Replace("[!--news.author--]", news.Author);
            Content = Content.Replace("[!--news.content--]", news.Content);
            Content = Content.Replace("[!--news.keywords--]", news.KeyWords);
            Content = Content.Replace("[!--news.description--]", news.Description);
            #endregion

            #region 上一篇  下一篇 链接
            News news_pre = BasePage.GetPreNews(news, cls);
            News news_next = BasePage.GetNextNews(news, cls);

            //上一篇
            string pre_link = "<a href=\"javascript:void(0)\">没有了</a>";
            if (news_pre != null)
            {
                pre_link=string.Format("<a href=\"{0}\">{1}</a>",BasePage.GetNewsUrl(news_pre,cls),news_pre.Title);
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

            Voodoo.IO.File.Write(System.Web.HttpContext.Current.Server.MapPath("~"+FileName) , Content);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo;
using Voodoo.Net;
using Newtonsoft.Json;
using Voodoo.Cache;


using System.Text;
using System.Text.RegularExpressions;

namespace Web.e.tool.Collect.Novel
{
    public partial class ServerCollect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetContentList("官雄", "第一百七十七章 巧合吗？");

            //string action = WS.RequestString("a");
            //switch (action)
            //{
            //    case "getcontentlist":

            //}
        }
        public static List<string> GetFilter()
        {
            if (Voodoo.Cache.Cache.GetCache("Filter") == null)
            {
                Voodoo.Cache.Cache.SetCache("Filter",
                     Voodoo.Net.Url.GetHtml(Voodoo.Basement.BasePage.SystemSetting.SiteUrl + "Config/NovelRule/Filter.txt", "utf-8"));
            }
            string[] fi = Voodoo.Cache.Cache.GetCache("Filter").ToString().Split('\n');
            List<string> result = new List<string>();
            foreach (string f in fi)
            {
                result.Add(f.Replace("\r", ""));
            }
            return result;
        }

        protected void GetContentList(string BookTitle, string ChapterTitle)
        {

            List<string> Result = new List<string>();

            #region 1. 从贴吧搜索了

            string chapterListHtml = Url.GetHtml(string.Format("http://tieba.baidu.com/f?kw={0}&tb=on", BookTitle.UrlEncode(Encoding.GetEncoding("gb2312"))), "gbk");

            List<TitleAndUrl> chapters = new List<TitleAndUrl>();
            var match_Chapters = chapterListHtml.GetMatchGroup("<a href=\"(?<url>.*?)\" target=\"_blank\" class=\"th_tit\">(?<title>.*?)</a>|<td class=\"thread_title\">[\\s]*?<a href=\"(?<url>.*?)\" target=\"_blank\">(?<title>.*?)</a>");
            int i = 0;
            while (match_Chapters.Success)
            {
                chapters.Add(new TitleAndUrl()
                {
                    Title = match_Chapters.Groups["title"].Value,
                    Url = match_Chapters.Groups["url"].Value.AppendToDomain("http://tieba.baidu.com/"),
                    Index = i
                });
                i++;
                match_Chapters = match_Chapters.NextMatch();
            }

            var chapter_NeedCollect = (from n in chapters select new { n.Index, n.Url, n.Title, weight = n.Title.GetSimilarityWith(ChapterTitle) }).OrderByDescending(p => p.weight).ToList();
            if (chapter_NeedCollect.First().weight > (0.6).ToDecimal())
            {

                Result.Add(GetContentFromTieba(chapter_NeedCollect.First().Url));
            }
            #endregion

            #region 帖子搜索

            string searchUrl = string.Format("http://www.baidu.com/s?ie=utf-8&f=8&rsv_bp=1&wd={0}++site%3Atieba.baidu.com&rsv_n=2&inputT=6588", BookTitle + " " + ChapterTitle);

            string html_Chapterlist = Url.GetHtml(searchUrl, "utf-8");
            Match m_chapters = html_Chapterlist.GetMatchGroup("<h3 class=\"t\"><a.*?href=\"(?<url>.*?)\".*?>(?<title>.*?)</a>");
            chapters = new List<TitleAndUrl>();
            i = 0;
            while (m_chapters.Success)
            {
                chapters.Add(new TitleAndUrl()
                {
                    Url = m_chapters.Groups["url"].Value,
                    Title = m_chapters.Groups["title"].Value,
                    Index = i
                });
                i++;
                m_chapters = m_chapters.NextMatch();
            }
            chapter_NeedCollect = (from n in chapters select new { n.Index, n.Url, n.Title, weight = n.Title.TrimHTML().Replace(" ", "").GetSimilarityWith(ChapterTitle.TrimHTML().Replace(" ", "")) }).OrderByDescending(p => p.weight).ToList();
            if (chapter_NeedCollect.First().weight > (0.7).ToDecimal())
            {

                Result.Add(GetContentFromTieba(chapter_NeedCollect.First().Url));
            }

            #endregion

            #region 从网上搜索

            searchUrl = string.Format("http://www.baidu.com/s?ie=utf-8&f=8&rsv_bp=1&wd={0}+site%3Awcxiaoshuo.com&rsv_n=2&inputT=6588", BookTitle + " " + ChapterTitle);

            html_Chapterlist = Url.GetHtml(searchUrl, "utf-8");
            m_chapters = html_Chapterlist.GetMatchGroup("<h3 class=\"t\"><a.*?href=\"(?<url>.*?)\".*?>(?<title>.*?)</a>");
            chapters = new List<TitleAndUrl>();
            i = 0;
            while (m_chapters.Success)
            {
                chapters.Add(new TitleAndUrl()
                {
                    Url = m_chapters.Groups["url"].Value,
                    Title = m_chapters.Groups["title"].Value,
                    Index = i
                });
                i++;
                m_chapters = m_chapters.NextMatch();
            }
            chapter_NeedCollect = (from n in chapters select new { n.Index, n.Url, n.Title, weight = n.Title.TrimHTML().Replace(" ", "").GetSimilarityWith(ChapterTitle.TrimHTML().Replace(" ", "")) }).ToList();//.OrderByDescending(p => p.weight).ToList();

            for (int j = 0; j < 3; j++)
            {
                string html_content = Url.GetHtml(chapter_NeedCollect[j].Url, "gbk");

                string c = GetContent(html_content);
                Result.Add(c);
            }


            #endregion


            Response.Clear();
            Response.Write(JsonConvert.SerializeObject(Result));
        }

        #region 从贴吧获取内容
        /// <summary>
        /// 从贴吧获取内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected string GetContentFromTieba(string url)
        {
            string content = "";
            string html_Content = Url.GetHtml(url, "gbk");
            Match m_Content = html_Content.GetMatchGroup("<p id=\".*?\" class=\"d_post_content\">(?<content>[\\s\\S]*?)</p>");
            while (m_Content.Success)
            {
                if (m_Content.Groups["content"].Value.Length > 200)
                {
                    content += Filter(m_Content.Groups["content"].Value);
                }
                m_Content = m_Content.NextMatch();
            }
            return content;
        }
        #endregion

        #region 过滤
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public string Filter(string Content)
        {

            //去除特殊字符

            Content = RegexReplace(Content, "[§№☆★○●◎◇◆□■△▲※→←↑↓〓＃＆＠＼＾＿￣―♂♀‘’々～‖＂〃〔〕〈〉《》「」『』〖〗【】（）［｛｝°＄￡￥‰％℃¤￠]{1,}?", "");
            Content = RegexReplace(Content, "[~@#$%^*()_=\\-\\+\\[\\]]{1,}?", "");



            //全角转半角
            Content = Content.ToDBC();

            //英文转小写
            Content = Content.ToLower();

            //删除脚本
            Content = RegexReplace(Content, "<script [\\s\\S]*?</script>", "");

            //删除链接
            Content = RegexReplace(Content, "<a [\\s\\S]*?</a>", "");

            //删除不需要的HTML
            Content = RegexReplace(Content, "<[/]?table>", "");
            Content = RegexReplace(Content, "<[/]?tr>", "");
            Content = RegexReplace(Content, "<[/]?div>", "");
            Content = RegexReplace(Content, "<[/]?td>", "");
            Content = RegexReplace(Content, "<[/]?span>", "");
            Content = RegexReplace(Content, "<[/]?font>", "");
            Content = RegexReplace(Content, "<[/]?p>", "");
            Content = RegexReplace(Content, "<[/]?cc>", "");

            //删除网址
            Content = RegexReplace(Content, "http://", "");
            Content = RegexReplace(Content, "https://", "");
            Content = RegexReplace(Content, "[\\\\\\w\\./。]{3,20}\\.[com|net|org|cn|co|info|us|cc|xxx|tv|ws|hk|tw]+", "");
            //Content = RegexReplace(Content, "", "");

            var Filter_List = GetFilter();
            foreach (string f in Filter_List)
            {
                string[] pa = f.Split('|');
                if (pa.Length > 1)
                {
                    //Content = Regex.Replace(Content, pa[0], pa[1], RegexOptions.None);
                    //Content = new Regex(pa[0]).Replace(Content, pa[1], 100);
                    Content = RegexReplace(Content, pa[0], pa[1]);
                }
                else
                {
                    //Content = Regex.Replace(Content, pa[0], "", RegexOptions.None);
                    //Content = new Regex(pa[0]).Replace(Content,"", 100);
                    Content = RegexReplace(Content, pa[0], "");
                }

            }

            return Content;
        }

        public static string RegexReplace(string Content, string parrten, string newvalue)
        {
            while (Regex.IsMatch(Content, parrten))
            {
                Content = Regex.Replace(Content, parrten, newvalue, RegexOptions.IgnoreCase);
            }
            return Content;
        }
        #endregion 过滤

        public string GetContent(string html)
        {
            List<TitleAndUrl> chapters = new List<TitleAndUrl>();

            Match m = new Regex("<div.*?>(?<key>[\\s\\S]{1000,}?)</div>", RegexOptions.IgnoreCase).Match(html);
            while (m.Success)
            {
                chapters.Add(new TitleAndUrl()
                {
                    Content = Filter(m.Groups["key"].Value),
                    wg = m.Groups["key"].Value.CountString("<p>")
                    + m.Groups["key"].Value.CountString("</p>")
                    + m.Groups["key"].Value.CountString("<br>")
                    + m.Groups["key"].Value.CountString("&nbsp;")
                    - m.Groups["key"].Value.CountString("</a>")
                    - m.Groups["key"].Value.CountString("<a")
                });
                m = m.NextMatch();
            }

            return chapters.OrderBy(p => p.wg).Last().Content;
        }
    }

    public class TitleAndUrl
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public int Index { get; set; }

        public int wg { get; set; }

        public string Content { get; set; }
    }
}
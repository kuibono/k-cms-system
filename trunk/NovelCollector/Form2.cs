using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;

using Voodoo;
using System.Text.RegularExpressions;
using System.Threading;

namespace NovelCollector
{
    public partial class Form2 : MultiSearch
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            BookInfoRule bi = new BookInfoRule();
            bi.CharSet = "UTF-8";
            bi.SearchRefer = "http://sosu.qidian.com/searchresult.aspx?searchkey={0}&searchtype=%E4%B9%A6%E5%90%8D";
            bi.SearchPageUrl = "http://sosu.qidian.com/ajax/search.ashx?method=getbooksearchlist&searchtype=%E4%B9%A6%E5%90%8D&searchkey={0}";
            bi.BookInfoUrl = "http://www.qidian.com/Book/{0}.aspx";
            bi.BookInfoUrl = "<div class=\"title\">[\\s\\S]*?<h1>(?<title>[\\s\\S]*?)</h1>[\\s]*?<b>小说作者：</b>[\\s\\S]*?\">(?<author>[\\s\\S]*?)</a>[\\s\\S]*?<b>总字数：</b>(?<charcount>[\\s\\S]*?)</td>[\\s\\S]*?<div class=\"txt\">(?<intro>[\\s\\S]*?)<span[\\s\\S]*?<b>小说类别：</b><a href=\"[\\s\\S]*?target=\"_blank\">(?<class>[\\s\\S]*?)</a>[\\s\\S]*?<b>写作进程：</b>(?<status>[\\s\\S]*?)</td>";
            bi.ChapterListUrl = "http://www.qidian.com/BookReader/{0}.aspx";
            bi.ChapterTitle = "<li style='width:.*?%;'><a [\\s\\S]*?>(?<title>.*?)</a>";
            bi.GoogleDomain = "http://www.google.com/";
            bi.GoogleCharSet = "UTF-8";
            bi.ChapterUrl = "<h3 class=\"r\"><a href=\"(?<url>.*?)\" [\\s\\S]*?>[\\s\\S]*?</a></h3>";
            bi.TargetSite = "http://localhost/";

            bi.mDomain = "http://www.shouda8.com/";
            bi.mSearchPageUrl = "http://www.shouda8.com/EBook/index.aspx";
            bi.mSearchPar = "searchkey={0}&SearchClass=1";
            bi.mUrl = "<td><a target=\"_blank\" href=\"(?<url>.*?)\" class=\"bookname\">[\\s\\S]*?</a> <span class=\"booksort\">";
            bi.mChapter = "<div class=\"chapter_list_chapter\"><a href=\"(?<url>.*?)\" [\\s\\S]*?>(?<title>.*?)</a></div>";
            bi.mContent = "<div id=\"chapter_content\">(?<content>[\\s\\S]*?)</div>";
            bi.mCharSet = "gb2312";

            Voodoo.IO.XML.SaveSerialize(bi, "C:\\BookInfoRule.xml");


            //List<ChapterRule> cr = new List<ChapterRule>();

            //cr.Add(new ChapterRule() { 
            //    SiteName="手打吧",
            //    Domain = "shouda8.com",
            //    CharSet = "gb2312",
            //    Content = "<div id=\"chapter_content\">(?<content>[\\s\\S]*?)</div>",
            //    CheckSuccess="[\\s\\S]{1000,}?"
            //});

            //cr.Add(new ChapterRule()
            //{
            //    SiteName = "75小说",
            //    Domain = "75dr.com",
            //    CharSet = "gbk",
            //    Content = "<td id=\"table_container\">(?<content>[\\s\\S]*?)</td>",
            //    CheckSuccess = "[\\s\\S]{1000,}?"
            //});

            //cr.Add(new ChapterRule()
            //{
            //    SiteName = "35小说网",
            //    Domain = "xiaoshuo555.cn",
            //    CharSet = "gbk",
            //    Content = "<div id=\"content\" align=center>(?<content>[\\s\\S]*?)</div>",
            //    CheckSuccess = "[\\s\\S]{1000,}?"
            //});

            //cr.Add(new ChapterRule()
            //{
            //    SiteName = "思路中文网",
            //    Domain = "cilook.cn",
            //    CharSet = "gb2312",
            //    Content = "<div align=\"left\" id=\"content\">(?<content>[\\s\\S]*?)</div>",
            //    CheckSuccess = "[\\s\\S]{1000,}?"
            //});


            //cr.Add(new ChapterRule()
            //{
            //    SiteName = "五五文学",
            //    Domain = "55wx.com",
            //    CharSet = "gb2312",
            //    Content = "<table width=\"100%\"  border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"text\" id=\"fontsize\" style=\"font-size:14px;\">[\\s\\S]*?<tr>[\\s\\S]*?<td>(?<content>[\\s\\S]*?)</td>",
            //    CheckSuccess = "[\\s\\S]{1000,}?"
            //});

            //cr.Add(new ChapterRule()
            //{
            //    SiteName = "思路中文网",
            //    Domain = "slzww.com",
            //    CharSet = "gbk",
            //    Content = "<div id=\"content\">(?<key>[\\s\\S]*?)</div>",
            //    CheckSuccess = "[\\s\\S]{1000,}?"
            //});

            //Voodoo.IO.XML.SaveSerialize(cr, "C:\\ChapterRule.xml");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Thread th = new Thread(Collect);
            th.Start();
        }



        public override void Status_Changed()
        {
            this.Invoke(new MethodInvoker(delegate
                {
                    this.lb_BookTitle.Text = BookTitle;
                    this.lb_ChapterTitle.Text = ChapterTitle;
                    this.lb_Status.Text = Status;
                    this.lb_Class.Text = str_Class;
                    this.lb_Author.Text = Author;
                }));
        }

        #region 根据书名获取书籍信息
        /// <summary>
        /// 根据书名获取书籍信息
        /// </summary>
        /// <param name="BookName">书名</param>
        /// <returns></returns>
        public BookInfo GetChapterList(string BookName)
        {
            BookInfoRule bi = (BookInfoRule)Voodoo.IO.XML.DeSerialize(typeof(BookInfoRule), Voodoo.IO.File.Read(System.Environment.CurrentDirectory + "\\BookInfoRule.xml"));
            string Title = BookName.toUtf8String();

            string QidianSearchUrl = string.Format(bi.SearchPageUrl, Title);
            string QidianRefer = string.Format(bi.SearchRefer, Title);

            string SearchList = Voodoo.Net.Url.Post(new System.Collections.Specialized.NameValueCollection(),
                QidianSearchUrl,
                Encoding.GetEncoding(bi.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                QidianRefer,
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11");


            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.DeserializeObject(SearchList);

            var j = ((System.Collections.Generic.Dictionary<string, object>)((object[])(((object[])(json))[0]))[0]);

            BookInfo book = new BookInfo();

            book.Author = j["AuthorName"].ToS();
            book.Class = j["SubCategoryName"].ToS();
            book.Intro = j["Description"].ToS();
            book.Length = 0;
            book.Title = j["BookName"].ToS().TrimHTML();

            string bookID = j["BookId"].ToS();


            string ChapterListUrl = string.Format(bi.ChapterListUrl, bookID);

            string ListContent = Voodoo.Net.Url.Post(new System.Collections.Specialized.NameValueCollection(),
                ChapterListUrl,
                Encoding.GetEncoding(bi.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                "http://www.qidian.com/Book/" + bookID + ".aspx",
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11");


            Match m_chapterList = new Regex(bi.ChapterTitle, RegexOptions.None).Match(ListContent);

            book.ChapterTitles = new List<string>();

            while (m_chapterList.Success)
            {
                book.ChapterTitles.Add(m_chapterList.Groups["title"].Value);
                m_chapterList = m_chapterList.NextMatch();

            }

            return book;
        }
        #endregion 根据书名获取书籍信息

        #region 根据关键词获取文章内容
        /// <summary>
        /// 根据关键词获取文章内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetChapterContent(string key)
        {
            List<ChapterRule> cr = (List<ChapterRule>)Voodoo.IO.XML.DeSerialize(typeof(List<ChapterRule>), Voodoo.IO.File.Read(System.Environment.CurrentDirectory + "\\ChapterRule.xml"));
            BookInfoRule bi = (BookInfoRule)Voodoo.IO.XML.DeSerialize(typeof(BookInfoRule), Voodoo.IO.File.Read(System.Environment.CurrentDirectory + "\\BookInfoRule.xml"));

            foreach (ChapterRule c in cr)
            {
                //从Google搜索
                string SearchUrl = string.Format("https://www.google.com/search?hl=zh-CN&newwindow=1&safe=strict&q=site%3A{0}+{1}&oq=site%3A{0}+{1}&aq=f&aqi=&aql=&gs_sm=12&gs_upl=6999l11935l0l12589l3l3l0l0l0l0l0l0ll1l0",
                    c.Domain,
                    key.toUtf8String()
                    );

                string ListContent = Voodoo.Net.Url.GetHtml(SearchUrl, "utf-8");
                Match m_Url = new Regex(bi.ChapterUrl, RegexOptions.None).Match(ListContent);
                if (m_Url.Success)
                {
                    string ContentUrl = m_Url.Groups["url"].Value;
                    string ContentHtml = Voodoo.Net.Url.GetHtml(ContentUrl, c.CharSet);
                    //string ContentHtml = Voodoo.Net.Url.Post(new System.Collections.Specialized.NameValueCollection(),
                    //    ContentUrl,
                    //    Encoding.GetEncoding(c.CharSet),
                    //    new System.Net.CookieContainer(),
                    //    "*.*",
                    //    SearchUrl,
                    //    "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11");

                    Match m_Content = new Regex(c.Content, RegexOptions.None).Match(ContentHtml);
                    if (m_Content.Success)
                    {
                        string Content = m_Content.Groups["content"].Value;
                        if (Regex.IsMatch(Content, c.CheckSuccess))
                        {
                            return Content;
                        }

                    }

                    //m_Url = m_Url.NextMatch();
                }
            }



            return "";
        }
        #endregion 根据关键词获取文章内容
    }
}

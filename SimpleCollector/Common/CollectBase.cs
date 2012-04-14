using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using Voodoo;
using Voodoo.Model;
using Voodoo.Basement.Client;
using Voodoo.Net;

namespace SimpleCollector
{
    public class CollectBase
    {
        FormCollecor Parent = new FormCollecor();
        public CollectBase(FormCollecor f)
        {
            this.Parent = f;
        }

        protected void SetStatus(string str)
        {
            Parent.Invoke(new MethodInvoker(delegate
            {

                Parent.lb_stats.Text = str;
            }));
        }


        public void Collect(string ListUrl, string BookTitle, string UrlTitleRule, string ContentRule, string NextPageUrl, string encoding)
        {
            //string enciding = "gb2312";

            BookHelper bh = new BookHelper("http://zuoaiai.net/");
            SetStatus("获取本地书籍");
            Book b = bh.SearchBook(BookTitle, "", "").First();

            SetStatus("打开列表页面");
            string str_ListHtml = Url.GetHtml(ListUrl, encoding);

            Match m_urls = str_ListHtml.GetMatchGroup(UrlTitleRule);
            while (m_urls.Success)
            {
                string url = m_urls.Groups["url"].Value.AppendToDomain(ListUrl);
                string Title = m_urls.Groups["title"].Value.TrimHTML();


                SetStatus("判断章节是否存在-" + Title);
                var chapter = bh.ChapterSearch(b.Title, Title, false);
                if (chapter.Count > 0)
                {
                    m_urls = m_urls.NextMatch();
                    continue;
                }

                SetStatus("打开章节：" + Title);
                string str_HtmlContent = Url.GetHtml(url, encoding);

                string content = "";
                bool isImage = false;
                try
                {
                    content = str_HtmlContent.GetMatch(ContentRule).First();

                    Match m_NextPage = str_HtmlContent.GetMatchGroup(NextPageUrl);
                    while (m_NextPage.Success)
                    {
                        url = m_NextPage.Groups["key"].Value.AppendToDomain(url);
                        str_HtmlContent = Url.GetHtml(url, encoding);
                        content += str_HtmlContent.GetMatch(ContentRule).First();


                        m_NextPage = m_NextPage.NextMatch();
                    }
                }
                catch
                {
                    content = "章节正在处理中，请稍后阅读";
                    isImage = true;
                }
                content = Filter(content);

                SetStatus("保存章节");
                bh.ChapterAdd(b.ID, Title, content, isImage);


                m_urls = m_urls.NextMatch();
            }


        }

        public void CollectBooks(List<string> BookNeedCollect, string ListPageUrl, string NextPageUrl, string BookUrlRule, string BookInfoRule, string ChapterListUrl, string encoding, string urlTitleRule, string ContentRule, string NextContentUrl)
        {
            BookHelper bh = new BookHelper("http://zuoaiai.net/");
        begin:
            SetStatus("打开列表页面");
            string listHtml = Url.GetHtml(ListPageUrl, encoding);
            Match m_books = listHtml.GetMatchGroup(BookUrlRule);
            while (m_books.Success)
            {
                string bookUrl = m_books.Groups["url"].Value.AppendToDomain(ListPageUrl);
                string bookTitle = m_books.Groups["title"].Value.TrimHTML();
                if (BookNeedCollect.Count != 1 || BookNeedCollect.First() != "*")
                {
                    if (BookNeedCollect.Where(p => p == bookTitle).Count() == 0)
                    {
                        m_books = m_books.NextMatch();//不需要采集的书籍
                        continue;
                    }
                }
                SetStatus("验证是否存在");
                string bookInfoHtml = Url.GetHtml(bookUrl, encoding);
                if (bh.SearchBook(bookTitle, "", "").Count > 0)
                {
                    //m_books = m_books.NextMatch();//已经存在的书籍
                    //continue;
                }
                else
                {
                    //不存在
                    SetStatus("打开书籍页面");
                    Match m_bookInfo = bookInfoHtml.GetMatchGroup(BookInfoRule);
                    if (m_bookInfo.Success)
                    {
                        //获取到书籍信息，并且添加到系统
                        string title = m_bookInfo.Groups["title"].Value.TrimHTML();
                        //string author = m_bookInfo.Groups["author"].Value;
                        string author = "robot";
                        string cls = m_bookInfo.Groups["class"].Value;
                        string length = m_bookInfo.Groups["length"].Value;
                        string intro = m_bookInfo.Groups["intro"].Value.TrimHTML();

                        string imageUrl = m_bookInfo.Groups["image"].Value.ToS();

                        bookTitle = title;

                        //处理类别 
                        Class c = bh.GetClass(cls.Length > 0 ? cls : "其他");

                        //添加书籍
                        Book b = bh.BookAdd(title, author, c.ID, intro, length.ToInt64());

                        //if (b.ID > 0 || imageUrl.IsNullOrEmpty() == false)
                        //{
                        //    imageUrl = imageUrl.AppendToDomain(bookUrl);
                        //    Url.DownFile(imageUrl, System.Environment.CurrentDirectory + "\\Face.jpg");
                        //    Voodoo.IO.ImageHelper.MakeThumbnail(System.Environment.CurrentDirectory + "\\Face.jpg",
                        //        System.Environment.CurrentDirectory + "\\stand.jpg",
                        //        120,
                        //        150,
                        //        "Cut");
                        //    bh.SetBookFace(b.ID, System.Environment.CurrentDirectory + "\\stand.jpg");
                        //}


                    }



                }
                //处理章节
                string chapterListUrl = bookInfoHtml.GetMatch(ChapterListUrl).First().AppendToDomain(bookUrl);
                Collect(chapterListUrl, bookTitle, urlTitleRule
                         , ContentRule, NextContentUrl, encoding);
                m_books = m_books.NextMatch();
            }//结束书籍列表书籍采集
            //开始判断是够有下一页
            Match m_NextPage = listHtml.GetMatchGroup(NextPageUrl);
            while (m_NextPage.Success)
            {
                ListPageUrl = m_NextPage.Groups["key"].Value.AppendToDomain(ListPageUrl);
                goto begin; ;
            }
        }


        #region 过滤
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public string Filter(string Content)
        {

            //去除特殊字符
            Content = Regex.Replace(Content, "[§№☆★○●◎◇◆□■△▲※→←↑↓〓＃＆＠＼＾＿￣―♂♀‘’々～‖＂〃〔〕〈〉《》「」『』〖〗【】（）［｛｝°＄￡￥‰％℃¤￠]{1,}?", "");
            Content = Regex.Replace(Content, "[~@#$%^*()_=\\-\\+\\[\\]]{1,}?", "");

            //全角转半角
            Content = Content.ToDBC();

            //英文转小写
            Content = Content.ToLower();

            //删除脚本
            Content = Regex.Replace(Content, "<script [\\s\\S]*?</script>", "", RegexOptions.IgnoreCase);

            //删除链接
            Content = Regex.Replace(Content, "<a [\\s\\S]*?</a>", "", RegexOptions.IgnoreCase);

            //删除不需要的HTML
            Content = Regex.Replace(Content, "<[/]?table>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?tr>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?td>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?div>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?span>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "<[/]?font>", "", RegexOptions.IgnoreCase);
            //Content = Regex.Replace(Content, "<[/]?p>", "", RegexOptions.IgnoreCase);

            //删除网址
            Content = Regex.Replace(Content, "http://", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "https://", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "[\\\\\\w\\./。]{3,20}\\.[com|net|org|cn|co|info|us|cc|xxx|tv|ws|hk|tw]+", "", RegexOptions.IgnoreCase);//有些网址会出现带掺杂特殊符号的行为如 www/.aizr\。net



            return Content;
        }
        #endregion 过滤


    }
}

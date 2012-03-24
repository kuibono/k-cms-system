using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo.Net;
using Voodoo;
using Voodoo.Model;
using Voodoo.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

using System.Threading;
using CookComputing.XmlRpc;

namespace KCMDCollector.Book
{
    public abstract class CollectBaseV2
    {
        #region 属性
        /// <summary>
        /// 起点书籍
        /// </summary>
        protected BookAndChapter QidianBook { get; set; }

        /// <summary>
        /// 需要采集的书籍章节
        /// </summary>
        protected BookAndChapter BookNeedCollect { get; set; }

        /// <summary>
        /// 本地书籍
        /// </summary>
        protected BookAndChapter LocalBook { get; set; }

        /// <summary>
        /// 主站书籍
        /// </summary>
        protected BookAndChapter MainBook { get; set; }

        /// <summary>
        /// 分站书籍
        /// </summary>
        protected BookAndChapter OtherBook { get; set; }

        /// <summary>
        /// 采集状态
        /// </summary>
        protected StatusObject CollectStatus { get; set; }

        /// <summary>
        /// 状态改变
        /// </summary>
        protected abstract void Status_Chage();

        protected Voodoo.Basement.Client.BookHelper BH;

        private IMath googleProxy;

        private IMath baiduProxy;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectBaseV2()
        {
            this.CollectStatus = new StatusObject();
            this.BH = new Voodoo.Basement.Client.BookHelper(RulesOperate.GetSetting().TargetUrl);

            googleProxy = XmlRpcProxyGen.Create<IMath>();
            googleProxy.Url = "http://blogsearch.google.com/ping/RPC2";

            baiduProxy = XmlRpcProxyGen.Create<IMath>();
            baiduProxy.Url = "http://ping.baidu.com/ping/RPC2";

            

        }
        #endregion

        #region 获取本地书籍
        /// <summary>
        /// 获取本地书籍
        /// </summary>
        /// <param name="bac"></param>
        /// <returns></returns>
        protected BookAndChapter GetLocalBook(BookAndChapter bac)
        {
            Setting s = Book.RulesOperate.GetSetting();
            BookAndChapter result = new BookAndChapter();

            //分类
            Class cls = BH.GetClass(bac.Class);

            bool bookExist = BH.BookExist(bac.BookTitle, bac.Author);
            Voodoo.Model.Book book = new Voodoo.Model.Book();
            if (bookExist)
            {
                book = BH.GetBook(bac.BookTitle, bac.Author);

            }
            else
            {
                //添加书籍
                book = BH.BookAdd(bac.BookTitle, bac.Author, cls.ID, bac.Intro, 233999);
                UploadBookFace(book);//设置书籍封面

            }

            result.Url = s.TargetUrl + "Book/" + book.ClassName + "/" + book.Title + "-" + book.Author + "/";

            #region 提交到百度谷歌
            if (!bookExist)
            {
                googleProxy.ping("爱造人小说阅读", "http://www.aizr.net/", result.Url, "http://www.aizr.net/rss.aspx");
                baiduProxy.ping("爱造人小说阅读", "http://www.aizr.net/", result.Url, "http://www.aizr.net/rss.aspx");
            }
            #endregion

            result.BookTitle = book.Title;
            result.Class = book.ClassName;
            result.ClassID = book.ClassID;
            result.ID = book.ID;
            result.Intro = book.Intro;
            result.LastChapter = new Chapter() { Title = book.LastChapterTitle };
            result.Status = book.Status;
            return result;

        }
        #endregion

        #region 获取起点书籍
        /// <summary>
        /// 获取起点书籍
        /// </summary>
        /// <param name="BookTitle"></param>
        /// <returns></returns>
        protected BookAndChapter GetQidianBook(string BookTitle)
        {
            BookAndChapter b = new BookAndChapter();

            //搜索起点，获得json数据
            string Title = BookTitle.toUtf8String();
            QidianRule Rule = Book.RulesOperate.GetQidianRule();

            string QidianSearchUrl = string.Format(Rule.SearchPageUrl, Title);
            string QidianRefer = string.Format(Rule.SearchRefer, Title);

            CollectStatus.Status = "正在搜索"; Status_Chage();
            string SearchList = Voodoo.Net.Url.Post(new System.Collections.Specialized.NameValueCollection(),
                QidianSearchUrl,
                Encoding.GetEncoding(Rule.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                QidianRefer,
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11");


            //解析json数据
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var json = serializer.DeserializeObject(SearchList);

                var j = ((System.Collections.Generic.Dictionary<string, object>)((object[])(((object[])(json))[0]))[0]);

                b.ID = j["BookId"].ToS().ToInt32();
                b.Author = j["AuthorName"].ToS();
                b.Class = j["SubCategoryName"].ToS();
                b.Intro = j["Description"].ToS();
                b.BookTitle = j["BookName"].ToS().TrimHTML();
            }
            catch
            {
                this.CollectStatus.Status = "起点没有这本书";
                this.Status_Chage();

                return new BookAndChapter() { ID = 0, Chapters = new List<Chapter>() };
            }


            //打开章节列表
            CollectStatus.Status = "打开章节列表"; Status_Chage();
            string url_ChapterList = string.Format(Rule.ChapterListUrl, b.ID.ToString());
            string html_ChapterList = Url.Post(new System.Collections.Specialized.NameValueCollection(),
                url_ChapterList,
                Encoding.GetEncoding(Rule.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                "http://www.qidian.com/Book/" + b.ID + ".aspx",
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11");

            //查找章节
            b.Chapters = new List<Chapter>();

            var match_Chapter = html_ChapterList.GetMatchGroup(Rule.ChapterTitle);
            int i = 0;
            while (match_Chapter.Success)
            {
                b.Chapters.Add(new Chapter()
                {
                    Title = match_Chapter.Groups["title"].Value,
                    Url = match_Chapter.Groups["url"].Value.AppendToDomain("http://www.qidian.com/"),
                    IsVip = match_Chapter.Groups["url"].Value.ToLower().Contains("vip"),
                    Index = i
                });
                i++;
                match_Chapter = match_Chapter.NextMatch();
            }

            return b;
        }
        #endregion

        #region 从起点下载普通章节
        /// <summary>
        /// 从起点下载普通章节
        /// </summary>
        /// <param name="ChapterUrl"></param>
        /// <returns></returns>
        protected string GetQidianNormalChapter(string ChapterUrl)
        {
            if (ChapterUrl.Contains("vip"))
            {
                return null;
            }

            QidianRule Rule = Book.RulesOperate.GetQidianRule();
            string HtmlQidianChapter = Url.Post(new NameValueCollection(),
                ChapterUrl,
                Encoding.GetEncoding(Rule.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                "http://www.qidian.com/",
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11"
                );

            string txtUrl = HtmlQidianChapter.GetMatchGroup("<script src='(?<url>.*?)'  charset='GB2312'></script>").Groups["url"].Value;

            if (txtUrl.IsNullOrEmpty())
            {
                return null;
            }

            string txt_Content = Url.GetHtml(txtUrl, "GB2312");


            txt_Content = txt_Content.Replace("document.write('", "");
            txt_Content = txt_Content.Replace("');", "");
            //删除脚本
            txt_Content = Regex.Replace(txt_Content, "<script [\\s\\S]*?</script>", "", RegexOptions.IgnoreCase);

            //删除链接
            txt_Content = Regex.Replace(txt_Content, "<a [\\s\\S]*?</a>", "", RegexOptions.IgnoreCase);

            if (txt_Content.Contains("<img"))
            {
                return "";
            }

            return txt_Content;
        }
        #endregion 从起点下载普通章节

        #region 采集章节
        /// <summary>
        /// 采集章节
        /// </summary>
        /// <param name="b">书籍</param>
        /// <param name="r">采集规则</param>
        protected void CollectChapter(BookAndChapter bc, CollectRule Rule)
        {

            Setting s = Book.RulesOperate.GetSetting();

            BookAndChapter b = new BookAndChapter();
            //搜索书籍
            string html_Search = "";
            if (Rule.SearchMethod.ToLower() == "get")//采集站搜索使用get提交
            {
                html_Search = Url.Post(
                    new NameValueCollection(),
                    Rule.SearchPageUrl + "?" + string.Format(Rule.SearchPars, bc.BookTitle.UrlEncode(Encoding.GetEncoding("gb2312"))),
                    Encoding.GetEncoding(Rule.CharSet),
                    new System.Net.CookieContainer(),
                    "*.*",
                    Rule.Url,
                    "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11"
                    );
            }
            else
            {
                //采集站搜索使用POST提交
                html_Search = Url.Post(
                    string.Format(Rule.SearchPars, bc.BookTitle).ParamToNameValueCollection(),
                    Rule.SearchPageUrl,
                    Encoding.GetEncoding(Rule.CharSet),
                    new System.Net.CookieContainer(),
                    "*.*",
                    Rule.Url,
                    "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11"
                    );
            }

            string html_BookInfo = "";
            if (html_Search.IsMatch(Rule.BookInfoUrl))
            {
                CollectStatus.Status = "打开书籍信息页"; Status_Chage();
                string bookUrl = html_Search.GetMatchGroup(Rule.BookInfoUrl).Groups["url"].Value.AppendToDomain(Rule.Url);
                //打开书籍信息页
                html_BookInfo = Url.GetHtml(bookUrl, Rule.CharSet);
            }
            else
            {
                //系统自动跳转到了书籍信息页
                html_BookInfo = html_Search;
            }


            //获得章节列表页地址
            string chapterListUrl = html_BookInfo.GetMatchGroup(Rule.ChapterListUrl).Groups["url"].Value.AppendToDomain(Rule.Url);


            //打开章节列表
            CollectStatus.Status = "打开章节列表"; Status_Chage();
            string html_ChapterList = Url.GetHtml(chapterListUrl, Rule.CharSet);
            var match_Chapters = html_ChapterList.GetMatchGroup(Rule.ChapterNameAndUrl);

            //获取章节列表
            b.Chapters = new List<Chapter>();
            int i = 0;
            while (match_Chapters.Success)
            {
                b.Chapters.Add(new Chapter()
                {
                    Title = match_Chapters.Groups["title"].Value,
                    Url = match_Chapters.Groups["url"].Value.AppendToDomain(chapterListUrl),
                    Index = i
                });
                i++;
                match_Chapters = match_Chapters.NextMatch();
            }


            foreach (Chapter c in bc.Chapters)
            {
                if (!c.Content.IsNullOrEmpty())
                {
                    //如果章节内容不为空，则不需要采集，继续采集下一章节
                    continue;
                }
                //获取章节在分站点的URL和标题
                //var chapter_NeedCollect = b.Chapters.Where(p => p.Title.Replace(" ", "").Contains(c.Title.Replace(" ", "")));
                var chapter_NeedCollect = (from n in b.Chapters select new { n.Index, n.Url, n.Length, n.Title, n.Content, weight = n.Title.GetSimilarityWith(c.Title) }).OrderByDescending(p => p.weight).ToList();
                if (chapter_NeedCollect.Count() > 0 && chapter_NeedCollect.First().weight > (0.8).ToDecimal())//相似度大于0.8的才进行采集
                {
                    this.CollectStatus.ChapterTitle = c.Title;
                    this.CollectStatus.Status = "正在采集";
                    Status_Chage();
                    //采集章节内容

                    BookNeedCollect.Changed = true;

                    string html_Content = Url.GetHtml(chapter_NeedCollect.First().Url, Rule.CharSet);

                    //过滤
                    string Content = html_Content.GetMatchGroup(Rule.ChapterContent).Groups["content"].Value;
                    Content = Filter(Content);
                    if (Content.ToLower().Contains("<img ") == false)
                    {
                        c.Content = Content;
                    }

                    this.CollectStatus.ChapterleftCout--; Status_Chage();


                }//end of 判断章节在分站中存在

            }//end of 循环采集章节

        }
        #endregion

        #region 从落秋等采集图片章节
        /// <summary>
        /// 没能采集成功的章节在落秋采集图片
        /// </summary>
        /// <param name="bc"></param>
        /// <param name="Rule"></param>
        protected void CollectChapterFromLuoqiu(BookAndChapter bc, CollectRule Rule)
        {
            Setting s = Book.RulesOperate.GetSetting();

            BookAndChapter b = new BookAndChapter();
            //搜索书籍
            string html_Search = "";
            if (Rule.SearchMethod.ToLower() == "get")//采集站搜索使用get提交
            {
                html_Search = Url.Post(
                    new NameValueCollection(),
                    Rule.SearchPageUrl + "?" + string.Format(Rule.SearchPars, bc.BookTitle.UrlEncode(Encoding.GetEncoding("gb2312"))),
                    Encoding.GetEncoding(Rule.CharSet),
                    new System.Net.CookieContainer(),
                    "*.*",
                    Rule.Url,
                    "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11"
                    );
            }
            else
            {
                //采集站搜索使用POST提交
                html_Search = Url.Post(
                    string.Format(Rule.SearchPars, bc.BookTitle).ParamToNameValueCollection(),
                    Rule.SearchPageUrl,
                    Encoding.GetEncoding(Rule.CharSet),
                    new System.Net.CookieContainer(),
                    "*.*",
                    Rule.Url,
                    "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11"
                    );
            }

            string html_BookInfo = "";
            if (html_Search.IsMatch(Rule.BookInfoUrl))
            {
                CollectStatus.Status = "打开书籍信息页"; Status_Chage();
                string bookUrl = html_Search.GetMatchGroup(Rule.BookInfoUrl).Groups["url"].Value.AppendToDomain(Rule.Url);
                //打开书籍信息页
                html_BookInfo = Url.GetHtml(bookUrl, Rule.CharSet);
            }
            else
            {
                //系统自动跳转到了书籍信息页
                html_BookInfo = html_Search;
            }


            //获得章节列表页地址
            string chapterListUrl = html_BookInfo.GetMatchGroup(Rule.ChapterListUrl).Groups["url"].Value.AppendToDomain(Rule.Url);


            //打开章节列表
            CollectStatus.Status = "打开章节列表"; Status_Chage();
            string html_ChapterList = Url.GetHtml(chapterListUrl, Rule.CharSet);
            var match_Chapters = html_ChapterList.GetMatchGroup(Rule.ChapterNameAndUrl);

            //获取章节列表
            b.Chapters = new List<Chapter>();
            int i = 0;
            while (match_Chapters.Success)
            {
                b.Chapters.Add(new Chapter()
                {
                    Title = match_Chapters.Groups["title"].Value,
                    Url = match_Chapters.Groups["url"].Value.AppendToDomain(chapterListUrl),
                    Index = i
                });
                i++;
                match_Chapters = match_Chapters.NextMatch();
            }


            foreach (Chapter c in bc.Chapters)
            {
                if (!c.Content.IsNullOrEmpty())
                {
                    //如果章节内容不为空，则不需要采集，继续采集下一章节
                    continue;
                }
                //获取章节在分站点的URL和标题
                //var chapter_NeedCollect = b.Chapters.Where(p => p.Title.Replace(" ", "").Contains(c.Title.Replace(" ", "")));
                var chapter_NeedCollect = (from n in b.Chapters select new { n.Index, n.Url, n.Length, n.Title, n.Content, weight = n.Title.GetSimilarityWith(c.Title) }).OrderByDescending(p => p.weight).ToList();
                if (chapter_NeedCollect.Count() > 0 && chapter_NeedCollect.First().weight > (0.5).ToDecimal())//相似度大于0.8的才进行采集
                {
                    this.CollectStatus.ChapterTitle = c.Title;
                    this.CollectStatus.Status = "正在采集";
                    Status_Chage();
                    //采集章节内容

                    BookNeedCollect.Changed = true;

                    string html_Content = Url.GetHtml(chapter_NeedCollect.First().Url, Rule.CharSet);

                    bc.Changed = true;

                    if (html_Content.IsMatch(Rule.ImageRule))
                    {
                        c.IsImageChapter = true;
                        Match m_images = html_Content.GetMatchGroup(Rule.ImageRule);
                        while (m_images.Success)
                        {
                            c.Content += string.Format("<img src=\"{0}\" alt=\"{1}\" />", m_images.Groups["key"].Value, c.Title);
                            m_images = m_images.NextMatch();
                        }
                    }
                    else
                    {
                        //过滤
                        string Content = html_Content.GetMatchGroup(Rule.ChapterContent).Groups["content"].Value;
                        Content = Filter(Content);
                        c.Content = Content;
                    }




                    this.CollectStatus.ChapterleftCout--; Status_Chage();


                }//end of 判断章节在分站中存在

            }//end of 循环采集章节
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
            Content = Regex.Replace(Content, "<[/]?p>", "", RegexOptions.IgnoreCase);

            //删除网址
            Content = Regex.Replace(Content, "http://", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "https://", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "[\\\\\\w\\./。]{3,20}\\.[com|net|org|cn|co|info|us|cc|xxx|tv|ws|hk|tw]+", "", RegexOptions.IgnoreCase);//有些网址会出现带掺杂特殊符号的行为如 www/.aizr\。net

            //根据预先指定的规则进行替换
            var Filter_List = Book.RulesOperate.GetFilter();
            foreach (string f in Filter_List)
            {
                string[] pa = f.Split('|');
                if (pa.Length > 1)
                {
                    Content = Regex.Replace(Content, pa[0], pa[1], RegexOptions.None);
                }
                else
                {
                    Content = Regex.Replace(Content, pa[0], "", RegexOptions.None);
                }

            }


            return Content;
        }
        #endregion 过滤

        #region 提交章节内容到目标站点
        /// <summary>
        /// 提交章节内容到目标站点
        /// </summary>
        /// <param name="b"></param>
        public void SubmitBook(BookAndChapter b)
        {
            Setting s = Book.RulesOperate.GetSetting();

            foreach (Chapter c in b.Chapters)
            {
                CollectStatus.ChapterTitle = c.Title;
                CollectStatus.Status = "正在提交";

                BookChapter chapter_Submited;

                Status_Chage();

                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("书籍{0}的章节：{1}正在处理中，请稍后访问阅读，或者百度搜索“{0}{1}”查找本章节。阅读{0}最新章节，尽在<a href={2}>{2}</a>。", b.BookTitle, c.Title, s.TargetUrl));

                if (c.Content.IsNullOrEmpty() || c.Content.Trim().IsNullOrEmpty())
                {
                    this.CollectStatus.Status = "这张没有采集到"; Status_Chage();
                    chapter_Submited = BH.ChapterAdd(b.ID, c.Title, sb.ToS(), true);
                    continue;
                }


                chapter_Submited = BH.ChapterAdd(b.ID, c.Title, c.Content, c.IsImageChapter);

                if (chapter_Submited.ID < 0)
                {
                    this.CollectStatus.Status = "章节保存失败"; Status_Chage();
                    Thread.Sleep(2000);

                }
                else
                {
                    //保存成功 ，更新章节地址 
                    c.Url = b.Url + chapter_Submited.ID + ".htm";
                    b.Changed = true;

                    googleProxy.ping("爱造人小说阅读", "http://www.aizr.net/", c.Url, "http://www.aizr.net/rss.aspx");
                    baiduProxy.ping("爱造人小说阅读", "http://www.aizr.net/", c.Url, "http://www.aizr.net/rss.aspx");
                }
            }
            BookNeedCollect = b;
        }
        #endregion

        #region 发博客
        /// <summary>
        /// 发博客
        /// </summary>
        /// <param name="b"></param>
        public void PublishBlog(BookAndChapter b)
        {
            if (b.Chapters.Where(p => string.IsNullOrEmpty(p.Content) == false).Count() == 0)
            {
                return;
            }

            if (b.Chapters.Where(p => p.Url.IndexOf("qidian.com") > 0).Count() > 0)
            {
                return;
            }

            var lastchapter = b.Chapters.Where(p => string.IsNullOrEmpty(p.Content) == false).Last();
            var blogs = Book.RulesOperate.GetMailBlogRule();

            StringBuilder sb_chapters = new StringBuilder();
            foreach (Chapter c in b.Chapters.Where(p => string.IsNullOrEmpty(p.Content) == false).ToList())
            {
                sb_chapters.AppendLine(string.Format("<a href='{0}'>{1}</a><br/>", c.Url, c.Title));
            }

            string content = string.Format("{0}<br/ ><br/ >继续阅读本次更新的其他章节：{1}<br/><br/>查看章节列表：{2}<br/><br/>回到书籍信息页：{2}",
                lastchapter.Content.CutString(200),
                sb_chapters.ToS(),
                string.Format("<a href='{0}'>{1}</a>", b.Url, b.BookTitle)
                );
            content = Regex.Replace(content, "&.{1,6};", " ").Replace("&", "");

            foreach (var blog in blogs)
            {

                CollectStatus.Status = "发文章到" + blog.BlogName; Status_Chage();

                try
                {
                    Voodoo.Net.Mail.SMTP.SentMail(
                                        blog.Email,
                                        blog.Email,
                                        blog.Password,
                                        blog.RecMail,
                                        "爱造人小说阅读",
                                        b.BookTitle + "-" + b.Chapters.Last().Title,
                                        content,
                                        blog.Smtp,
                                        blog.BlogName);
                }
                catch (System.Exception e)
                {
                    CollectStatus.Status = e.Message;
                    Status_Chage();
                    Thread.Sleep(200);
                }

            }

            var Blogs = Book.RulesOperate.GetBlogModel();
            foreach (var log in Blogs)
            {
                CollectStatus.Status = "发文章到" + log.BlogName; Status_Chage();

                switch (log.type)
                {
                    case BlogType.Neasy:
                        Voodoo.Net.BlogHelper.Neasy n = new Voodoo.Net.BlogHelper.Neasy(log.UserName, log.Password);
                        n.Login();
                        n.Post(b.BookTitle + "-" + b.Chapters.Last().Title, content);
                        break;
                    case BlogType.Sina:
                        Voodoo.Net.BlogHelper.Sina s = new Voodoo.Net.BlogHelper.Sina(log.UserName, log.Password);
                        s.Login();
                        s.Post(b.BookTitle + "-" + b.Chapters.Last().Title, content);
                        break;
                    case BlogType.Sohu:
                        Voodoo.Net.BlogHelper.Sohu so = new Voodoo.Net.BlogHelper.Sohu(log.UserName, log.Password);
                        so.Login();
                        so.Post(b.BookTitle + "-" + b.Chapters.Last().Title, content);
                        break;
                    case BlogType.WordPress:
                        Voodoo.Net.BlogHelper.WordPress wp = new Voodoo.Net.BlogHelper.WordPress(log.BlogUrl, log.UserName, log.Password);
                        wp.Login();
                        wp.Post(b.BookTitle + "-" + b.Chapters.Last().Title, content);
                        break;
                }
            }


        }
        #endregion

        #region 生成页面
        /// <summary>
        /// 生成页面
        /// </summary>
        /// <param name="BookID"></param>
        protected void CreatePage(string BookID)
        {
            Setting s = Book.RulesOperate.GetSetting();

            this.CollectStatus.Status = "生成首页";
            Status_Chage();
            BH.CreateIndex();

            this.CollectStatus.Status = "生成分类页";
            Status_Chage();
            BH.CreateClassPage();

            this.CollectStatus.Status = "生成书籍页";
            Status_Chage();
            BH.CreateBook(BookID.ToInt32());

            this.CollectStatus.Status = "生成章节";
            Status_Chage();
            BH.CreateChapters(BookID.ToInt32());
        }
        #endregion

        #region 根据书籍名称采集
        /// <summary>
        /// 根据书籍名称采集
        /// </summary>
        /// <param name="BookTitle"></param>
        public void CollectBookByTitle(string BookTitle)
        {
            this.CollectStatus.BookTitle = BookTitle; Status_Chage();
            CollectStatus.ChapterTitle = "";
            //1.获取起点书籍
            CollectStatus.Status = "从起点搜索"; Status_Chage();
            this.QidianBook = GetQidianBook(BookTitle);
            if (QidianBook.ID <= 0)
            {
                return;//在起点没有采集到这本书
            }
            if (QidianBook.Chapters.Count == 0)
            {
                CollectStatus.Status = "没能打开章节列表";
                Status_Chage();
                return;
            }

            //2.获取本地书籍
            CollectStatus.Status = "从本地检查"; Status_Chage();
            this.LocalBook = GetLocalBook(QidianBook);

            //3.对比获取需要采集的章节
            var tmp = QidianBook.Chapters.Where(p => p.Title.ReplaceSynonyms() == LocalBook.LastChapter.Title.ReplaceSynonyms());//最后一张在起点中的章节
            if (tmp.Count() == 0 && !LocalBook.LastChapter.Title.IsNullOrEmpty())
            {
                CollectStatus.Status = "最新章节在起点中不存在";
                Status_Chage();

                return;
            }
            if (LocalBook.LastChapter.Title.IsNullOrEmpty())
            {
                //本地书籍没有任何章节
                BookNeedCollect = QidianBook;
            }
            else
            {
                var localLastBook = tmp.First();
                BookNeedCollect = QidianBook;
                BookNeedCollect.Url = LocalBook.Url;
                BookNeedCollect.Chapters = BookNeedCollect.Chapters.Where(p => p.Index > localLastBook.Index).ToList();
            }

            BookNeedCollect.ID = LocalBook.ID;
            BookNeedCollect.Class = LocalBook.Class;
            BookNeedCollect.ClassID = LocalBook.ClassID;

            CollectStatus.ChapterCount = BookNeedCollect.Chapters.Count(); CollectStatus.ChapterleftCout = BookNeedCollect.Chapters.Count(); Status_Chage();//剩余章节数量

            //可以从起点采集的章节
            if (BookNeedCollect.Chapters.Where(p => p.IsVip == false && p.Content.IsNullOrEmpty()).Count() > 0)
            {
                foreach (Chapter c in BookNeedCollect.Chapters.Where(p => p.IsVip == false).ToList())
                {
                    CollectStatus.ChapterTitle = c.Title; CollectStatus.Status = "正在从起点采集"; Status_Chage();
                    c.Content = GetQidianNormalChapter(c.Url);
                }
            }

            //4.循环采集书籍
            var Rules = RulesOperate.GetBookRules();
            foreach (CollectRule rule in Rules.Where(p => p.IsImageSite == false))
            {
                //如果没有任何章节需要采集，则直接退出章节
                CollectStatus.Status = "开始采集-" + rule.SiteName; Status_Chage();
                if (BookNeedCollect.Chapters.Count == 0)
                {
                    CollectStatus.Status = "没有任何章节需要采集"; Status_Chage(); Thread.Sleep(500);
                    break;
                }



                //需要采集的章节没有空内容的，也就是说需要采集的已经全都采集完成了
                if (BookNeedCollect.Chapters.Where(p => p.Content.IsNullOrEmpty()).Count() == 0)
                {
                    CollectStatus.Status = "章节全部采集完成"; Status_Chage(); Thread.Sleep(100);
                    break;
                }

                CollectChapter(BookNeedCollect, rule);
            }

            foreach (CollectRule rule in Rules.Where(p => p.IsImageSite == true))
            {
                //如果没有任何章节需要采集，则直接退出章节
                CollectStatus.Status = "开始采集-" + rule.SiteName; Status_Chage();
                if (BookNeedCollect.Chapters.Count == 0)
                {
                    CollectStatus.Status = "没有任何章节需要采集"; Status_Chage(); Thread.Sleep(500);
                    break;
                }



                //需要采集的章节没有空内容的，也就是说需要采集的已经全都采集完成了
                if (BookNeedCollect.Chapters.Where(p => p.Content.IsNullOrEmpty()).Count() == 0)
                {
                    CollectStatus.Status = "章节全部采集完成"; Status_Chage(); Thread.Sleep(100);
                    break;
                }

                CollectChapterFromLuoqiu(BookNeedCollect, rule);
            }


            //5.提交到目标站点
            CollectStatus.Status = "保存到目标站点"; Status_Chage();
            SubmitBook(BookNeedCollect);


            //6. 发博客
            PublishBlog(BookNeedCollect);

            //7.采集完成 生成书籍
            CollectStatus.Status = "采集完成，正在生成"; Status_Chage();
            if (BookNeedCollect.Changed)
            {
                CreatePage(BookNeedCollect.ID.ToS());
            }
            CollectStatus.Status = string.Format("书籍《{0}》完成", BookTitle); Status_Chage();
        }
        #endregion

        

        #region 多本采集
        /// <summary>
        /// 多本采集
        /// </summary>
        public void Collect()
        {
            string[] books = Book.RulesOperate.GetBooks();
            CollectStatus.BookCount = books.Length; CollectStatus.BookLeftCount = books.Length; Status_Chage();

            foreach (string b in books)
            {
                try
                {
                    CollectBookByTitle(b.Trim());
                }
                catch (Exception e)
                {
                    //采集某本书的时候出现异常
                    CollectStatus.Status = "ERR:" + e.Message;
                    Status_Chage();
                }
                CollectStatus.BookLeftCount--; Status_Chage();
            }
           
        }
        #endregion

        #region 替换图片章节为文本章节
        /// <summary>
        /// 替换图片章节为文本章节
        /// </summary>
        public void CollectText()
        {
            Setting s = Book.RulesOperate.GetSetting();

            this.CollectStatus.Status = "正在获取系统书籍列表"; Status_Chage();
            var books = BH.SearchBook("", "", "");
            foreach (var book in books)
            {
                this.CollectStatus.BookTitle = book.Title; Status_Chage();
                #region 获取书籍信息
                BookAndChapter bc = new BookAndChapter();
                bc.Author = book.Author;
                bc.BookTitle = book.Title;
                bc.Class = book.ClassName;
                bc.ClassID = book.ClassID;
                bc.ID = book.ID;
                bc.Intro = book.Intro;
                bc.Status = book.Status;
                bc.Chapters = new List<Chapter>();
                #endregion

                #region 获取图片章节
                this.CollectStatus.Status = "正在获取需要处理的章节"; Status_Chage();
                var chapters = BH.ChapterSearch(book.Title, "", true);//获取所有图片章节

                if (chapters.Count == 0)
                {
                    continue;
                }

                foreach (var chapter in chapters)
                {
                    bc.Chapters.Add(new Chapter()
                    {
                        IsImageChapter = true,
                        IsVip = true,
                        Title = chapter.Title,
                        id = chapter.ID

                    });
                }//获得书籍待采集章节结束
                #endregion


                //获得文本采集规则
                var Rules = RulesOperate.GetBookRules().Where(p => p.IsImageSite == false);

                #region 循环规则，开始采集
                foreach (var Rule in Rules)
                {
                    BookAndChapter b = new BookAndChapter();

                    #region 搜索书籍
                    this.CollectStatus.Status = string.Format("正在从{0}搜索书籍", Rule.SiteName); Status_Chage();
                    //搜索书籍
                    string html_Search = "";
                    if (Rule.SearchMethod.ToLower() == "get")//采集站搜索使用get提交
                    {
                        html_Search = Url.Post(
                            new NameValueCollection(),
                            Rule.SearchPageUrl + "?" + string.Format(Rule.SearchPars, bc.BookTitle.UrlEncode(Encoding.GetEncoding("gb2312"))),
                            Encoding.GetEncoding(Rule.CharSet),
                            new System.Net.CookieContainer(),
                            "*.*",
                            Rule.Url,
                            "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11"
                            );
                    }
                    else
                    {
                        //采集站搜索使用POST提交
                        html_Search = Url.Post(
                            string.Format(Rule.SearchPars, bc.BookTitle).ParamToNameValueCollection(),
                            Rule.SearchPageUrl,
                            Encoding.GetEncoding(Rule.CharSet),
                            new System.Net.CookieContainer(),
                            "*.*",
                            Rule.Url,
                            "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11"
                            );
                    }
                    #endregion

                    #region 打开书籍信息页

                    this.CollectStatus.Status = string.Format("正在从{0}打开书籍", Rule.SiteName); Status_Chage();
                    string html_BookInfo = "";
                    if (html_Search.IsMatch(Rule.BookInfoUrl))
                    {
                        CollectStatus.Status = "打开书籍信息页"; Status_Chage();
                        string bookUrl = html_Search.GetMatchGroup(Rule.BookInfoUrl).Groups["url"].Value.AppendToDomain(Rule.Url);
                        //打开书籍信息页
                        html_BookInfo = Url.GetHtml(bookUrl, Rule.CharSet);
                    }
                    else
                    {
                        //系统自动跳转到了书籍信息页
                        html_BookInfo = html_Search;
                    }
                    #endregion

                    #region 获取章节列表
                    //获得章节列表页地址
                    this.CollectStatus.Status = string.Format("正在从{0}打开章节列表", Rule.SiteName); Status_Chage();
                    string chapterListUrl = html_BookInfo.GetMatchGroup(Rule.ChapterListUrl).Groups["url"].Value.AppendToDomain(Rule.Url);


                    //打开章节列表
                    CollectStatus.Status = "打开章节列表"; Status_Chage();
                    string html_ChapterList = Url.GetHtml(chapterListUrl, Rule.CharSet);
                    var match_Chapters = html_ChapterList.GetMatchGroup(Rule.ChapterNameAndUrl);

                    //获取章节列表
                    b.Chapters = new List<Chapter>();
                    int i = 0;
                    while (match_Chapters.Success)
                    {
                        b.Chapters.Add(new Chapter()
                        {
                            Title = match_Chapters.Groups["title"].Value,
                            Url = match_Chapters.Groups["url"].Value.AppendToDomain(chapterListUrl),
                            Index = i
                        });
                        i++;
                        match_Chapters = match_Chapters.NextMatch();
                    }
                    #endregion

                    #region 循环获取待采集图片章节，替换成文本
                    this.CollectStatus.Status = string.Format("正在从{0}处理章节", Rule.SiteName); Status_Chage();
                    foreach (Chapter c in bc.Chapters)
                    {
                        this.CollectStatus.ChapterTitle = c.Title; Status_Chage();
                        if (!c.Content.IsNullOrEmpty())
                        {
                            //如果章节内容不为空，则不需要采集，继续采集下一章节
                            continue;
                        }
                        //获取章节在分站点的URL和标题
                        //var chapter_NeedCollect = b.Chapters.Where(p => p.Title.Replace(" ", "").Contains(c.Title.Replace(" ", "")));
                        var chapter_NeedCollect = (from n in b.Chapters select new { n.Index, n.Url, n.Length, n.Title, n.Content, weight = n.Title.GetSimilarityWith(c.Title) }).OrderByDescending(p => p.weight).ToList();
                        if (chapter_NeedCollect.Count() > 0 && chapter_NeedCollect.First().weight > (0.8).ToDecimal())//相似度大于0.8的才进行采集
                        {
                            this.CollectStatus.ChapterTitle = c.Title;
                            this.CollectStatus.Status = "正在采集";
                            Status_Chage();
                            //采集章节内容


                            string html_Content = Url.GetHtml(chapter_NeedCollect.First().Url, Rule.CharSet);

                            //过滤
                            string Content = html_Content.GetMatchGroup(Rule.ChapterContent).Groups["content"].Value;
                            Content = Filter(Content);
                            if (Content.ToLower().Contains("<img ") == false)
                            {
                                c.Content = Content;
                                bc.Changed = true;
                                //编辑章节
                                this.CollectStatus.Status = "章节保存到系统"; Status_Chage();
                                BH.ChapterEdit(c.id, c.Title, c.Content, false);

                                //完成之后将本章节去掉
                                bc.Chapters = bc.Chapters.Where(p => p.id != c.id).ToList();

                            }

                            this.CollectStatus.ChapterleftCout--; Status_Chage();


                        }//end of 判断章节在分站中存在

                    }//end of 循环采集章节
                    #endregion 循环采集章节
                }
                #endregion 循环规则

                #region 重新生成章节
                if (bc.Changed)
                {
                    CollectStatus.Status = "正在生成章节"; Status_Chage();
                    BH.CreateChapters(bc.ID);
                }

                #endregion
            }//书籍循环结束
        }
        #endregion

        #region 为书籍设置封面
        /// <summary>
        /// 为书籍设置封面
        /// </summary>
        /// <param name="book"></param>
        public void UploadBookFace(Voodoo.Model.Book book)
        {
            string faceUrl = "";

            string Title = book.Title.toUtf8String();
            QidianRule Rule = Book.RulesOperate.GetQidianRule();

            string QidianSearchUrl = string.Format(Rule.SearchPageUrl, Title);
            string QidianRefer = string.Format(Rule.SearchRefer, Title);

            //CollectStatus.Status = "正在搜索"; Status_Chage();
            string SearchList = Voodoo.Net.Url.Post(new System.Collections.Specialized.NameValueCollection(),
                QidianSearchUrl,
                Encoding.GetEncoding(Rule.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                QidianRefer,
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11");


            //解析json数据
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var json = serializer.DeserializeObject(SearchList);

                var j = ((System.Collections.Generic.Dictionary<string, object>)((object[])(((object[])(json))[0]))[0]);
                faceUrl = string.Format("http://image.cmfu.com/books/{0}/{0}.jpg", j["BookId"].ToS());

                //BH = new Voodoo.Basement.Client.BookHelper("http://localhost/");
                Voodoo.Net.Url.DownFile(faceUrl, System.Environment.CurrentDirectory + "\\face.jpg");

                BH.SetBookFace(book.ID, System.Environment.CurrentDirectory + "\\face.jpg");

            }
            catch
            {
                //this.CollectStatus.Status = "起点没有这本书";
                //this.Status_Chage();
            }

        }
        #endregion
    }

    public interface IMath : IXmlRpcProxy
    {
        [XmlRpcMethod("weblogUpdates.ping")]
        CookComputing.XmlRpc.XmlRpcStruct ping(string a, string b, string c, string d);
    }
}

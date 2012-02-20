using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo.Net;
using Voodoo;
using Voodoo.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

namespace KCMDCollector.Book
{
    public abstract class CollectBase
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectBase()
        {
            this.CollectStatus = new StatusObject();

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
            Web.Class cls = (Web.Class)XML.DeSerialize(typeof(Web.Class), Url.GetHtml(s.TargetUrl + "e/api/getClass.aspx?class=" + bac.Class, "utf-8"));

            //判断书籍是否存在
            bool bookExist = (bool)XML.DeSerialize(typeof(bool), Url.GetHtml(s.TargetUrl + "e/api/BookExist.aspx?title=" + bac.BookTitle + "&author=" + bac.Author, "utf-8"));
            Web.Book book = new Web.Book();
            if (bookExist)
            {
                book = (Web.Book)XML.DeSerialize(typeof(Web.Book), Url.GetHtml(s.TargetUrl + "e/api/getBook.aspx?title=" + bac.BookTitle + "&author=" + bac.Author, "utf-8"));

            }
            else
            {
                //添加书籍
                NameValueCollection nv = new NameValueCollection();
                nv.Add("title", bac.BookTitle);
                nv.Add("author", bac.Author);
                nv.Add("classid", cls.ID.ToS());
                nv.Add("intro", bac.Intro);
                nv.Add("length", "0");

                book = (Web.Book)Voodoo.IO.XML.DeSerialize(typeof(Web.Book), Voodoo.Net.Url.Post(nv, s.TargetUrl + "e/api/BookAdd.aspx", Encoding.UTF8));
            }

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
                    Index = i
                });
                i++;
                match_Chapter = match_Chapter.NextMatch();
            }

            return b;
        }
        #endregion

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
            string html_Search = Url.Post(
                string.Format(Rule.SearchPars, bc.BookTitle).ParamToNameValueCollection(),
                Rule.SearchPageUrl,
                Encoding.GetEncoding(Rule.CharSet),
                new System.Net.CookieContainer(),
                "*.*",
                Rule.Url,
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.2 Safari/535.11"
                );


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
                    Url = chapterListUrl + match_Chapters.Groups["url"].Value,
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
                var chapter_NeedCollect = b.Chapters.Where(p => p.Title.Replace(" ", "").Contains(c.Title.Replace(" ", "")));
                if (chapter_NeedCollect.Count() > 0)
                {
                    this.CollectStatus.ChapterTitle = c.Title;
                    this.CollectStatus.Status = "正在采集";
                    Status_Chage();
                    //采集章节内容
                    string html_Content = Url.GetHtml(chapter_NeedCollect.First().Url, Rule.CharSet);

                    //过滤
                    string Content = html_Content.GetMatchGroup(Rule.ChapterContent).Groups["content"].Value;
                    Content = Filter(Content);
                    c.Content = Content;


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
            Content = Regex.Replace(Content, "[§№☆★○●◎◇◆□■△▲※→←↑↓〓＃＆＠＼＾＿￣―♂♀‘’“”々～‖∶＂〃〔〕〈〉《》「」『』．〖〗【】（）［｛｝°＄￡￥‰％℃¤￠]{1,}?", "");
            Content = Regex.Replace(Content, "[~!@#$%^*()_=\\-\\+\\[\\]]{1,}?", "");

            //全角转半角
            Content = Content.ToDBC();

            //英文转小写
            Content = Content.ToLower();

            //删除脚本
            Content = Regex.Replace(Content, "<script [\\s\\S]*?</script>", "", RegexOptions.IgnoreCase);

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
            Content = Regex.Replace(Content, "[\\w\\.]{3,20}\\.[com|net|org|cn|co|info|us|cc|xxx|tv|ws|hk|tw]+", "", RegexOptions.IgnoreCase);

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
                Status_Chage();
                NameValueCollection nv = new NameValueCollection();
                nv.Add("bookid", b.ID.ToString());
                nv.Add("booktitle", b.BookTitle);
                nv.Add("classid", b.ClassID.ToS());
                nv.Add("classname", b.Class);
                nv.Add("content", c.Content);
                nv.Add("title", c.Title);

                Web.BookChapter chapter_Submited = (Web.BookChapter)XML.DeSerialize(typeof(Web.BookChapter), Url.Post(nv, s.TargetUrl + "e/api/ChapterAdd.aspx", Encoding.UTF8));
                if (chapter_Submited.ID < 0)
                {
                    this.CollectStatus.Status = "章节保存失败"; Status_Chage();

                }
                else
                {
                    //采集成功 清掉这个章节
                    b.Changed = true;
                    b.Chapters = b.Chapters.Where(p => p.Index != c.Index).ToList();

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
            Voodoo.Net.Url.GetHtml(s.TargetUrl + "e/api/CreatePage.aspx?action=createindex");

            this.CollectStatus.Status = "生成分类页";
            Status_Chage();
            Voodoo.Net.Url.GetHtml(s.TargetUrl + "e/api/CreatePage.aspx?action=createclasspage");

            this.CollectStatus.Status = "生成书籍页";
            Status_Chage();
            Voodoo.Net.Url.GetHtml(s.TargetUrl + "e/api/CreatePage.aspx?action=createbook&id=" + BookID);

            this.CollectStatus.Status = "生成章节";
            Status_Chage();
            Voodoo.Net.Url.GetHtml(s.TargetUrl + "e/api/CreatePage.aspx?action=createchapters&id=" + BookID);
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
            //1.获取起点书籍
            CollectStatus.Status = "从起点搜索"; Status_Chage();
            this.QidianBook = GetQidianBook(BookTitle);
            if (QidianBook.ID <= 0)
            {
                return;//在起点没有采集到这本书
            }

            //2.获取本地书籍
            CollectStatus.Status = "从本地检查"; Status_Chage();
            this.LocalBook = GetLocalBook(QidianBook);

            //3.对比获取需要采集的章节
            var tmp = QidianBook.Chapters.Where(p => p.Title.Replace(" ", "") == LocalBook.LastChapter.Title.Replace(" ", ""));//最后一张在起点中的章节
            if (LocalBook.LastChapter.Title.IsNullOrEmpty())
            {
                //本地书籍没有任何章节
                BookNeedCollect = QidianBook;
            }
            else
            {
                var localLastBook = tmp.First();
                BookNeedCollect = QidianBook;
                BookNeedCollect.Chapters = BookNeedCollect.Chapters.Where(p => p.Index > localLastBook.Index).ToList();
            }

            BookNeedCollect.ID = LocalBook.ID;
            BookNeedCollect.Class = LocalBook.Class;
            BookNeedCollect.ClassID = LocalBook.ClassID;

            //4.循环采集书籍
            var Rules = RulesOperate.GetBookRules();
            foreach (CollectRule rule in Rules)
            {
                //如果没有任何章节需要采集，则直接退出章节
                CollectStatus.Status = "开始采集-"+rule.SiteName; Status_Chage();
                if (BookNeedCollect.Chapters.Count == 0)
                {
                    break;
                }
                CollectChapter(BookNeedCollect, rule);
            }

            //5.提交到目标站点
            CollectStatus.Status = "保存到目标站点"; Status_Chage();
            SubmitBook(BookNeedCollect);

            //6.采集完成 生成书籍
            CollectStatus.Status = "采集完成，正在生成"; Status_Chage();
            if (BookNeedCollect.Changed)
            {
                CreatePage(BookNeedCollect.ID.ToS());
            }
            CollectStatus.Status = string.Format("书籍《{0}》完成",BookTitle); Status_Chage();
        }
        #endregion

        #region 多本采集
        /// <summary>
        /// 多本采集
        /// </summary>
        public void Collect()
        {
            string[] books = Book.RulesOperate.GetBooks();
            foreach (string b in books)
            {
                CollectBookByTitle(b.Trim());
            }
        }
        #endregion
    }
}

using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Voodoo;
using Voodoo.IO;
using Voodoo.other.SEO;
using Voodoo.Basement;
using Voodoo.Model;
using Voodoo.DAL;
namespace Web.e.api
{
    /// <summary>
    /// xml-rpc接口，为系统提供一切可能。可以完成客户端的操作
    /// </summary>
    public partial class xmlrpc : System.Web.UI.Page
    {
        #region 处理请求
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WS.RequestString("a").ToLower();
            switch (action)
            {
                case "booksearch":
                    string title = WS.RequestString("title");
                    string author = WS.RequestString("author");
                    string intro = WS.RequestString("intro");
                    SearchBook(title, author, intro);
                    break;
                case "bookexist":
                    title = WS.RequestString("title");
                    author = WS.RequestString("author");
                    BookExist(title, author);
                    break;
                case "getbook":
                    title = WS.RequestString("title");
                    author = WS.RequestString("author");
                    GetBook(title, author);
                    break;
                case "bookadd":
                    title = WS.RequestString("title");
                    author = WS.RequestString("author");
                    int classid = WS.RequestInt("class");
                    intro = WS.RequestString("intro").HtmlDeCode();
                    long length = WS.RequestString("length").ToInt64();
                    BookAdd(title, author, classid, intro, length);
                    break;
                case "bookedit":
                    int id = WS.RequestInt("id");
                    title = WS.RequestString("title");
                    author = WS.RequestString("author");
                    classid = WS.RequestInt("class");
                    intro = WS.RequestString("intro").HtmlDeCode();
                    length = WS.RequestString("length").ToInt64();
                    BookEdit(id, title, author, classid, intro, length);
                    break;
                case "bookdelete":
                    id = WS.RequestInt("id");
                    BookDelete(id);
                    break;
                case "getclass":
                    string ClassName = WS.RequestString("classname");
                    int ModelID = WS.RequestInt("model");
                    GetClass(ClassName, ModelID);
                    break;
                case "editclass":
                    classid = WS.RequestInt("class");
                    ClassName = WS.RequestString("classname");
                    int ParentID = WS.RequestInt("pid");
                    EditClass(classid, ClassName, ParentID);
                    break;
                case "getchapter":
                    string chaptertitle = WS.RequestString("chaptertitle");
                    string booktitle = WS.RequestString("booktitle");
                    GetChapter(booktitle, chaptertitle);
                    break;
                case "chapteradd":
                    int bookid = WS.RequestInt("bid");
                    chaptertitle = WS.RequestString("chaptertitle");
                    string Content = WS.RequestString("content").HtmlDeCode();
                    bool IsImageChapter = WS.RequestString("isimagechapter").ToBoolean();
                    ChapterAdd(bookid, chaptertitle, Content, IsImageChapter);
                    break;
                case "chapteredit":
                    long chapterid = WS.RequestString("chapterid").ToInt64();
                    chaptertitle = WS.RequestString("chaptertitle");
                    Content = WS.RequestString("content").HtmlDeCode();
                    IsImageChapter = WS.RequestString("isimagechapter").ToBoolean();
                    ChapterEdit(chapterid, chaptertitle, Content, IsImageChapter);
                    break;
                case "chapterdelete":
                    chapterid = WS.RequestString("chapterid").ToInt64();
                    ChapterDelete(chapterid);
                    break;
                case "chaptersearch":
                    booktitle = WS.RequestString("booktitle");
                    chaptertitle = WS.RequestString("chaptertitle");
                    IsImageChapter = WS.RequestString("isimagechapter").ToBoolean();
                    ChapterSearch(booktitle, chaptertitle, IsImageChapter);
                    break;
                case "createindex":
                    CreateIndex();
                    break;
                case "createclasspage":
                    CreateClassPage();
                    break;
                case "createbook":
                    bookid = WS.RequestInt("bookid");
                    CreateBook(bookid);
                    break;
                case "createchapters":
                    bookid = WS.RequestInt("bookid");
                    CreateChapters(bookid);
                    break;
                case "createsitemap":
                    CreateSitemap();
                    break;
                default:
                    break;

            }
        }
        #endregion



        #region 书籍搜索
        /// <summary>
        /// 书籍搜索
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Author">作者</param>
        /// <param name="Intro">简介</param>
        protected void SearchBook(string Title, string Author, string Intro)
        {
            var books = BookView.GetModelList(string.Format("Title like N'%{0}%' and Author like N'%{1}%' and Intro like N'%{2}%'", Title, Author, Intro));
            Response.Clear();
            Response.Write(XML.Serialize(books));
        }
        #endregion

        #region 书籍是否存在
        /// <summary>
        /// 书籍是否存在
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Author">作者</param>
        protected void BookExist(string Title, string Author)
        {
            Response.Clear();
            Response.Write(XML.Serialize(BookView.Exist(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author))));

        }
        #endregion

        #region 获取书籍
        /// <summary>
        /// 获取书籍
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Author">作者</param>
        protected void GetBook(string Title, string Author)
        {
            Book b = BookView.Find(string.Format("Title=N'{0}' and Author=N'{1}'",Title,Author));
            Response.Clear();
            Response.Write(XML.Serialize(b));
        }
        #endregion

        #region 添加书籍
        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Author">作者</param>
        /// <param name="ClassID">类别ID</param>
        /// <param name="Intro">简介</param>
        /// <param name="Length">长度</param>
        protected void BookAdd(string Title, string Author, int ClassID, string Intro, long Length)
        {
            string ClassName = ClassView.GetModelByID(ClassID.ToString()).ClassName;

            Book b = new Book();

            if (Title.IsNullOrEmpty() && BookView.Exist(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author)))
            {
                b.ID = int.MinValue;
                Response.Clear();
                Response.Write(Voodoo.IO.XML.Serialize(b));
                return;
            }

            b.Addtime = DateTime.UtcNow.AddHours(8);
            b.Author = Author;
            b.ClassID = ClassID;
            b.ClassName = ClassName;
            b.ClickCount = 0;
            b.CorpusID = 0;
            b.Enable = true;
            b.FaceImage = "";
            b.Intro = Intro;
            b.IsFirstPost = false;
            b.IsVip = false;
            b.LastChapterID = 0;
            b.LastVipChapterID = 0;
            b.Length = Length;
            b.ReplyCount = 0;
            b.SaveCount = 0;
            b.Status = 0;//连载中
            b.Title = Title;
            b.UpdateTime = DateTime.UtcNow.AddHours(8);
            b.VipUpdateTime = DateTime.UtcNow.AddHours(8);
            b.ZtID = 0;

            bool Exist = BookView.Exist(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author));
            if (Exist == false)
            {

                BookView.Insert(b);
            }
            else
            {
                b = BookView.Find(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author));
            }

            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(b));
        }
        #endregion

        #region 书籍编辑
        /// <summary>
        /// 书籍编辑
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="Title">标题</param>
        /// <param name="Author">作者</param>
        /// <param name="ClassID">类别ID</param>
        /// <param name="Intro">简介</param>
        /// <param name="Length">长度</param>
        protected void BookEdit(int id, string Title, string Author, int ClassID, string Intro, long Length)
        {
            Book b = BookView.GetModelByID(id.ToS());
            string ClassName = ClassView.GetModelByID(ClassID.ToString()).ClassName;
            b.Title = Title.IsNull(b.Title);
            b.Author = Author.IsNull(b.Author);
            b.ClassID = ClassID.IsNull(b.ClassID);
            b.ClassName = ClassName.IsNull(b.ClassName);
            b.Intro = Intro.IsNull(b.Intro);
            b.Length = Length == 0 ? b.Length : Length;

            if (b.ID < 0)
            {
                Response.Clear();
                Response.Write(Voodoo.IO.XML.Serialize(false));
            }
            try
            {
                BookView.Update(b);
                Response.Clear();
                Response.Write(Voodoo.IO.XML.Serialize(true));
            }
            catch
            {
                Response.Clear();
                Response.Write(Voodoo.IO.XML.Serialize(false));
            }
        }
        #endregion

        protected void SaveBookFace(int id,HttpFileCollection files)
        {
            try
            {
                Book b = BookView.GetModelByID(id.ToS());
                string ImagePath = Server.MapPath("/Book/BookFace/" + id + ".jpg");
                files[0].SaveAs(ImagePath);
                b.FaceImage = "/Book/BookFace/" + id + ".jpg";
                BookView.Update(b);
                Response.Clear();
                Response.Write(XML.Serialize(true));
            }
            catch
            {
                Response.Clear();
                Response.Write(XML.Serialize(false));
            }


        }

        #region 删除书籍
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id">ID</param>
        protected void BookDelete(int id)
        {
            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(BookView.DelByID(id)));
        }
        #endregion

        #region 获取栏目
        /// <summary>
        /// 获取栏目
        /// </summary>
        /// <param name="ClassName">栏目名称</param>
        /// <param name="ModelID">模型 1新闻 2图片 3问答 4小说</param>
        protected void GetClass(string ClassName, int ModelID)
        {
            Class cls = ClassView.Find(string.Format("ClassName=N'{0}' and ModelID={1}", ClassName, ModelID));
            if (cls.ID <= 0)
            {
                //cls.ClassForder = PinyinHelper.GetPinyin(ClassName);
                cls.ClassForder = ClassName;
                cls.ClassKeywords = ClassName;
                cls.ClassName = ClassName;
                cls.ModelID = ModelID;
                cls.IsLeafClass = true;
                cls.ModelID = ModelID;
                cls.ShowInNav = true;
                ClassView.Insert(cls);
            }
            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(cls));
        }
        #endregion

        #region 编辑类别
        /// <summary>
        /// 编辑类别
        /// </summary>
        /// <param name="ClassID">类别ID</param>
        /// <param name="ClassName">类别名称</param>
        /// <param name="ParentID">父ID</param>
        protected void EditClass(int ClassID, string ClassName, int ParentID)
        {
            Class c = ClassView.GetModelByID(ClassID.ToS());
            if (c.ID < 0)
            {
                Response.Clear();
                Response.Write(XML.Serialize(false));
            }
            c.ParentID = ParentID.IsNull(c.ParentID);
            c.ClassName = ClassName.IsNull(c.ClassName);
            try
            {
                ClassView.Update(c);
                Response.Clear();
                Response.Write(XML.Serialize(true));
            }
            catch (System.Exception e)
            {
                Response.Clear();
                Response.Write(XML.Serialize(false));
            }
        }
        #endregion

        #region 获取章节
        /// <summary>
        /// 获取章节
        /// </summary>
        /// <param name="BookTitle">书籍标题</param>
        /// <param name="ChapterTitle">章节标题</param>
        protected void GetChapter(string BookTitle, string ChapterTitle)
        {
            BookChapter c = BookChapterView.Find(string.Format("BookTitle=N'{0}' and Title=N'{1}'",BookTitle,ChapterTitle));
            Response.Clear();
            Response.Write(XML.Serialize(c));
        }
        #endregion

        #region 添加章节
        /// <summary>
        /// 添加章节
        /// </summary>
        /// <param name="BookID">书籍ID</param>
        /// <param name="Title">标题</param>
        /// <param name="Content">内容</param>
        /// <param name="IsImageChapter">是否图片章节</param>
        protected void ChapterAdd(int BookID, string Title, string Content, bool IsImageChapter)
        {

            Book b = BookView.GetModelByID(BookID.ToS());
            Class cls = BookView.GetClass(b);
            BookChapter c = BookChapterView.Find(string.Format("BookTitle=N'{0}' and Title=N'{1}'", b.Title, Title));
            if (c.ID <= 0)
            {
                c.BookID = b.ID;
                c.BookTitle = b.Title;
                c.ChapterIndex = 0;
                c.ClassID = cls.ID;
                c.ClassName = cls.ClassName;
                c.ClickCount = 0;
                c.Enable = true;
                c.IsFree = true;
                c.IsImageChapter = IsImageChapter;
                c.IsTemp = false;
                c.IsVipChapter = false;
                c.TextLength = Content.Length;
                c.Title = Title;
                c.UpdateTime = DateTime.UtcNow.AddHours(8);//东八区时间

                BookChapterView.Insert(c);

                //创建内容txt
                Voodoo.IO.File.Write(Server.MapPath(BasePage.GetBookChapterTxtUrl(c, cls)), Content);

                b.LastChapterID = c.ID;
                b.LastChapterTitle = c.Title;
                b.UpdateTime = c.UpdateTime;
                BookView.Update(b);
            }
            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(c));
        }

        #endregion

        #region 编辑章节
        /// <summary>
        /// 编辑章节
        /// </summary>
        /// <param name="ChapterID">章节ID</param>
        /// <param name="Title">标题</param>
        /// <param name="Content">内容</param>
        /// <param name="IsImageChapter">是否图片章节</param>
        protected void ChapterEdit(long ChapterID, string Title, string Content, bool IsImageChapter)
        {
            var chapter = BookChapterView.GetModelByID(ChapterID.ToS());
            var cls = ClassView.GetModelByID(chapter.ClassID.ToS());
            chapter.Title = Title.IsNull(chapter.Title);
            chapter.IsImageChapter = IsImageChapter;
            BookChapterView.Update(chapter);
            if (!Content.IsNullOrEmpty())
            {
                Voodoo.IO.File.Write(Server.MapPath(BasePage.GetBookChapterTxtUrl(chapter, cls)), Content);
            }

            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(chapter));

        }
        #endregion

        #region 删除章节
        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="id">章节ID</param>
        protected void ChapterDelete(long id)
        {
            try
            {
                var c = BookChapterView.GetModelByID(id.ToS());
                var b = BookView.GetModelByID(c.BookID.ToS());
                Class cls = BookView.GetClass(b);
                Voodoo.IO.File.Delete(Server.MapPath(BasePage.GetBookChapterTxtUrl(c, cls)));
                BookChapterView.DelByID(id);

                var lastChapter = BookChapterView.Find("BookId={0} order by ChapterIndex,id desc");
                b.UpdateTime = lastChapter.UpdateTime;
                b.LastChapterID = lastChapter.ID;
                b.LastChapterTitle = lastChapter.Title;
                BookView.Update(b);

                Response.Clear();
                Response.Write(Voodoo.IO.XML.Serialize(true));
            }
            catch (System.Exception e)
            {
                Response.Clear();
                Response.Write(Voodoo.IO.XML.Serialize(false));
            }

        }
        #endregion

        #region 搜索章节
        /// <summary>
        /// 搜索章节
        /// </summary>
        /// <param name="BookTitle">书籍标题</param>
        /// <param name="ChapterTitle">章节标题</param>
        /// <param name="IsImagechapter">是否图片章节</param>
        protected void ChapterSearch(string BookTitle, string ChapterTitle, bool IsImagechapter)
        {
            var cs = BookChapterView.GetModelList(string.Format("BookTitle like N'%{0}%' and Title like N'%{1}%' and IsImageChapter={2}", BookTitle, ChapterTitle, IsImagechapter.BoolToShort()));
            Response.Clear();
            Response.Write(XML.Serialize(cs));
        }
        #endregion

        #region 生成首页
        /// <summary>
        /// 生成首页
        /// </summary>
        protected void CreateIndex()
        {
            try
            {
                Voodoo.Basement.CreatePage.GreateIndexPage();
                Response.Clear();
                Response.Write(XML.Serialize(true));
            }
            catch (System.Exception e)
            {
                Response.Clear();
                Response.Write(XML.Serialize(false));
            }

        }
        #endregion

        #region 创建列表页面
        /// <summary>
        /// 创建列表页面
        /// </summary>
        protected void CreateClassPage()
        {
            try
            {
                var cls = ClassView.GetModelList();
                foreach (var c in cls)
                {
                    Voodoo.Basement.CreatePage.CreateListPage(c, 1);
                }
                Response.Clear();
                Response.Write(XML.Serialize(true));
            }
            catch (System.Exception e)
            {
                Response.Clear();
                Response.Write(XML.Serialize(false));
            }


        }
        #endregion

        #region 生成书籍页面
        /// <summary>
        /// 生成书籍页面
        /// </summary>
        /// <param name="bookid">书籍ID</param>
        protected void CreateBook(int bookid)
        {
            try
            {
                Book b = BookView.GetModelByID(bookid.ToS());
                Voodoo.Basement.CreatePage.CreateContentPage(b, BookView.GetClass(b));
                Response.Clear();
                Response.Write(XML.Serialize(true));
            }
            catch (System.Exception e)
            {
                Response.Clear();
                Response.Write(XML.Serialize(false));
            }

        }
        #endregion

        #region  生成章节
        /// <summary>
        /// 生成章节
        /// </summary>
        /// <param name="bookid">书籍ID</param>
        protected void CreateChapters(int bookid)
        {
            try
            {
                var chapters = BookChapterView.GetModelList(string.Format("bookid={0}", bookid));
                foreach (var c in chapters)
                {
                    Voodoo.Basement.CreatePage.CreateBookChapterPage(c, BookView.GetBook(c), BookView.GetClass(c));
                }
                Response.Clear();
                Response.Write(XML.Serialize(true));
            }
            catch (System.Exception e)
            {
                Response.Clear();
                Response.Write(XML.Serialize(false));
            }

        }
        #endregion

        #region 生成站点地图
        /// <summary>
        /// 生成站点地图
        /// </summary>
        protected void CreateSitemap()
        {
            Voodoo.other.SEO.SiteMap sm = new Voodoo.other.SEO.SiteMap();
            sm.url = new List<PageInfo>();

            sm.url.Add(new PageInfo() { changefreq = "always", lastmod = DateTime.Now, loc = Voodoo.Basement.BasePage.SystemSetting.SiteUrl, priority = "1.0" });
            List<Voodoo.Model.Book> bs = BookView.GetModelList("id>0 order by UpdateTime desc", 100);
            foreach (Voodoo.Model.Book b in bs)
            {
                sm.url.Add(new PageInfo()
                {
                    changefreq = "daily",
                    lastmod = b.UpdateTime,
                    loc = (Voodoo.Basement.BasePage.SystemSetting.SiteUrl + BasePage.GetBookUrl(b, BookView.GetClass(b))).Replace("//Book/", "/Book/"),
                    priority = "0.8"
                });
            }

            List<BookChapter> bcs = BookChapterView.GetModelList("id>0 order by UpdateTime desc", 500);
            foreach (BookChapter bc in bcs)
            {
                sm.url.Add(new PageInfo()
                {
                    changefreq = "monthly",
                    lastmod = bc.UpdateTime,
                    loc = (Voodoo.Basement.BasePage.SystemSetting.SiteUrl + BasePage.GetBookChapterUrl(bc, BookView.GetClass(bc))).Replace("//Book/", "/Book/"),
                    priority = "0.7"
                });
            }
            try
            {
                sm.SaveSiteMap(Server.MapPath("~/sitemapxml/index.xml"));
                Response.Clear();
                Response.Write(XML.Serialize(true));
            }
            catch
            {
                Response.Clear();
                Response.Write(XML.Serialize(false));
            }
        }
        #endregion

    }
}

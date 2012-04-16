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
using System.Reflection;

using System.IO;
using Voodoo;
using Voodoo.other;
using Voodoo.IO;
using Voodoo.other.SEO;
using Voodoo.Basement;
using Voodoo.Model;
using Voodoo.DAL;

namespace Web.e.api
{
    public partial class xmlrpcV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            var req = WS.RequestPing();
            List<string> strs = new List<string>();

            for (int i = 0; i < req.@params.Count; i++)
            {
                strs.Add(req.@params[i].value.@string);
            }

            var pars = strs.ToArray();
            string result = ExecMethod("xmlrpcV2", req.methodName, pars).ToS();
            Response.Clear();
            Response.Write(result);
            //}
            //catch (Exception ex)
            //{
            //}

        }

        /// <summary>
        /// 执行某个方法
        /// </summary>
        /// <param name="className">类，包括命名空间</param>
        /// <param name="methodName">方法名</param>
        /// <param name="objParas">参数</param>
        /// <returns></returns>
        protected object ExecMethod(string className, string methodName, object[] objParas)
        {
            Type t = typeof(xmlrpcV2);
            /*实例化这个类*/
            ConstructorInfo constructor = t.GetConstructor(new Type[0]);//将得到的类型传给一个新建的构造器类型变量
            object obj = constructor.Invoke(new object[0]);//使用构造器对象来创建对象
            /*执行Insert方法*/
            MethodInfo m = t.GetMethod(methodName);
            return m.Invoke(obj, objParas);
        }

        #region 书籍搜索
        /// <summary>
        /// 书籍搜索
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Author">作者</param>
        /// <param name="Intro">简介</param>
        public string SearchBook(string Title, string Author, string Intro)
        {
            var books = BookView.GetModelList(string.Format("Title like N'%{0}%' and Author like N'%{1}%' and Intro like N'%{2}%'", Title, Author, Intro));

            return XML.Serialize(books);
        }
        #endregion

        #region 书籍是否存在
        /// <summary>
        /// 书籍是否存在
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Author">作者</param>
        protected string BookExist(string Title, string Author)
        {

            return XML.Serialize(BookView.Exist(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author)));

        }
        #endregion

        #region 获取书籍
        /// <summary>
        /// 获取书籍
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Author">作者</param>
        protected string GetBook(string Title, string Author)
        {
            Book b = BookView.Find(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author));

            return XML.Serialize(b);
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
        protected string BookAdd(string Title, string Author, int ClassID, string Intro, long Length)
        {
            string ClassName = ClassView.GetModelByID(ClassID.ToString()).ClassName;

            Book b = new Book();

            if (Title.IsNullOrEmpty() && BookView.Exist(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author)))
            {
                b.ID = int.MinValue;

                return Voodoo.IO.XML.Serialize(b);
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


            return Voodoo.IO.XML.Serialize(b);
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
        protected string BookEdit(int id, string Title, string Author, int ClassID, string Intro, long Length)
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

                return Voodoo.IO.XML.Serialize(false);
            }
            try
            {
                BookView.Update(b);

                return Voodoo.IO.XML.Serialize(true);
            }
            catch
            {

                return Voodoo.IO.XML.Serialize(false);
            }
        }
        #endregion

        #region 设置书籍封面
        /// <summary>
        /// 设置书籍封面
        /// </summary>
        /// <param name="id">书籍ID</param>
        /// <param name="files">上传文件</param>
        protected string SaveBookFace(int id, HttpFileCollection files)
        {
            try
            {
                Book b = BookView.GetModelByID(id.ToS());
                string ImagePath = Server.MapPath("/Book/BookFace/" + id + ".jpg");
                DirectoryInfo dir = new FileInfo(ImagePath).Directory;
                if (!dir.Exists)
                {
                    dir.Create();
                }
                if (Voodoo.IO.File.Exists(ImagePath))
                {
                    Voodoo.IO.File.Delete(ImagePath);
                }
                files[0].SaveAs(ImagePath);
                b.FaceImage = "/Book/BookFace/" + id + ".jpg";
                BookView.Update(b);

                return XML.Serialize(true);
            }
            catch
            {

                return XML.Serialize(false);
            }


        }
        #endregion

        #region 删除书籍
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id">ID</param>
        protected string BookDelete(int id)
        {

            return Voodoo.IO.XML.Serialize(BookView.DelByID(id));
        }
        #endregion

        #region 获取栏目
        /// <summary>
        /// 获取栏目
        /// </summary>
        /// <param name="ClassName">栏目名称</param>
        /// <param name="ModelID">模型 1新闻 2图片 3问答 4小说</param>
        protected string GetClass(string ClassName, int ModelID)
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

            return Voodoo.IO.XML.Serialize(cls);
        }
        #endregion

        #region 编辑类别
        /// <summary>
        /// 编辑类别
        /// </summary>
        /// <param name="ClassID">类别ID</param>
        /// <param name="ClassName">类别名称</param>
        /// <param name="ParentID">父ID</param>
        protected string EditClass(int ClassID, string ClassName, int ParentID)
        {
            Class c = ClassView.GetModelByID(ClassID.ToS());
            if (c.ID < 0)
            {

                return XML.Serialize(false);
            }
            c.ParentID = ParentID.IsNull(c.ParentID);
            c.ClassName = ClassName.IsNull(c.ClassName);
            try
            {
                ClassView.Update(c);

                return XML.Serialize(true);
            }
            catch (System.Exception e)
            {

                return XML.Serialize(false);
            }
        }
        #endregion

        #region 获取章节
        /// <summary>
        /// 获取章节
        /// </summary>
        /// <param name="BookTitle">书籍标题</param>
        /// <param name="ChapterTitle">章节标题</param>
        protected string GetChapter(string BookTitle, string ChapterTitle)
        {
            BookChapter c = BookChapterView.Find(string.Format("BookTitle=N'{0}' and Title=N'{1}'", BookTitle, ChapterTitle));

            return XML.Serialize(c);
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
        protected string ChapterAdd(int BookID, string Title, string Content, bool IsImageChapter)
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

            return Voodoo.IO.XML.Serialize(c);
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
        protected string ChapterEdit(long ChapterID, string Title, string Content, bool IsImageChapter)
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


            return Voodoo.IO.XML.Serialize(chapter);

        }
        #endregion

        #region 删除章节
        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="id">章节ID</param>
        protected string ChapterDelete(long id)
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


                return Voodoo.IO.XML.Serialize(true);
            }
            catch (System.Exception e)
            {

                return Voodoo.IO.XML.Serialize(false);
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
        protected string ChapterSearch(string BookTitle, string ChapterTitle, bool IsImagechapter)
        {
            var cs = BookChapterView.GetModelList(string.Format("BookTitle like N'%{0}%' and Title like N'%{1}%' and IsImageChapter={2}", BookTitle, ChapterTitle, IsImagechapter.BoolToShort()));

            return XML.Serialize(cs);
        }
        #endregion

        #region  获取章节内容
        /// <summary>
        /// 获取章节内容
        /// </summary>
        /// <param name="chapterID">章节id</param>
        protected string GetChapterContent(long chapterID)
        {
            BookChapter chapter = BookChapterView.GetModelByID(chapterID.ToS());

            string path = BasePage.GetBookChapterTxtUrl(chapter, BookView.GetClass(chapter));

            string content = Voodoo.IO.File.Read(Server.MapPath(path));

            return XML.Serialize(content);
        }
        #endregion

        #region 生成首页
        /// <summary>
        /// 生成首页
        /// </summary>
        protected string CreateIndex()
        {
            try
            {
                Voodoo.Basement.CreatePage.GreateIndexPage();

                return XML.Serialize(true);
            }
            catch (System.Exception e)
            {

                return XML.Serialize(false);
            }

        }
        #endregion

        #region 创建列表页面
        /// <summary>
        /// 创建列表页面
        /// </summary>
        protected string CreateClassPage()
        {
            try
            {
                var cls = ClassView.GetModelList();
                foreach (var c in cls)
                {
                    Voodoo.Basement.CreatePage.CreateListPage(c, 1);
                }

                return XML.Serialize(true);
            }
            catch (System.Exception e)
            {

                return XML.Serialize(false);
            }


        }
        #endregion

        #region 生成书籍页面
        /// <summary>
        /// 生成书籍页面
        /// </summary>
        /// <param name="bookid">书籍ID</param>
        protected string CreateBook(int bookid)
        {
            try
            {
                Book b = BookView.GetModelByID(bookid.ToS());
                Voodoo.Basement.CreatePage.CreateContentPage(b, BookView.GetClass(b));

                return XML.Serialize(true);
            }
            catch (System.Exception e)
            {

                return XML.Serialize(false);
            }

        }
        #endregion

        #region  生成章节
        /// <summary>
        /// 生成章节
        /// </summary>
        /// <param name="bookid">书籍ID</param>
        protected string CreateChapters(int bookid)
        {
            try
            {
                var chapters = BookChapterView.GetModelList(string.Format("bookid={0}", bookid));
                foreach (var c in chapters)
                {
                    Voodoo.Basement.CreatePage.CreateBookChapterPage(c, BookView.GetBook(c), BookView.GetClass(c));
                }

                return XML.Serialize(true);
            }
            catch (System.Exception e)
            {

                return XML.Serialize(false);
            }

        }
        #endregion

        #region 生成站点地图
        /// <summary>
        /// 生成站点地图
        /// </summary>
        protected string CreateSitemap()
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

                return XML.Serialize(true);
            }
            catch
            {

                return XML.Serialize(false);
            }
        }
        #endregion
    }
}
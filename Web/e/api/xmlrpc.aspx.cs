using System;
using System.Collections;
using System.Configuration;
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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

            if (Title.IsNullOrEmpty())
            {
                b.ID = int.MinValue;
                Response.Clear();
                Response.Write(Voodoo.IO.XML.Serialize(b));
                return;
            }

            b.Addtime = DateTime.Now;
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
            b.UpdateTime = DateTime.Now;
            b.VipUpdateTime = DateTime.Now;
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
            b.Title = Title;
            b.Author = Author;
            b.ClassID = ClassID;
            b.ClassName = ClassName;
            b.Intro = Intro;
            b.Length = Length;

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

        #region 删除书籍
        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id">ID</param>
        protected void DeleteBook(int id)
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
            c.ParentID = ParentID;
            c.ClassName = ClassName;
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

        #region 添加章节
        /// <summary>
        /// 添加章节
        /// </summary>
        /// <param name="BookID">书籍ID</param>
        /// <param name="Title">标题</param>
        /// <param name="Content">内容</param>
        /// <param name="IsImageChapter">是否图片章节</param>
        protected void AddChapter(int BookID, string Title, string Content, bool IsImageChapter)
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
        protected void EditChapter(int ChapterID, string Title, string Content, bool IsImageChapter)
        {
            var chapter = BookChapterView.GetModelByID(ChapterID.ToS());
            var cls = ClassView.GetModelByID(chapter.ClassID.ToS());
            chapter.Title = Title;
            chapter.IsImageChapter = IsImageChapter;
            BookChapterView.Update(chapter);

            Voodoo.IO.File.Write(Server.MapPath(BasePage.GetBookChapterTxtUrl(chapter, cls)), Content);

            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(chapter));

        }
        #endregion

        #region 删除章节
        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="id">章节ID</param>
        protected void DeleteChapter(int id)
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
        protected void SeachChapter(string BookTitle, string ChapterTitle, bool IsImagechapter)
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo;
using Voodoo.Data;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;
using Voodoo.Setting;


namespace Web.e.admin.Book
{
    public partial class ChapterList : AdminBase
    {
        protected int id = WS.RequestInt("bookid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
            if (WS.RequestString("action") == "del")
            {
                Button1_Click(sender, e);
            }
        }

        protected void BindList()
        {
            var cpl = BookChapterView.GetModelList(string.Format("BookId={0}", id));
            rp_List.DataSource = cpl;
            rp_List.DataBind();
        }

        protected void btn_disable_Click(object sender, EventArgs e)
        {

        }

        protected void btn_enable_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            var chapters = BookChapterView.GetModelList(string.Format("id in({0})", ids));
            foreach (var chapter in chapters)
            {
                string FilePath = Server.MapPath(GetBookChapterUrl(chapter, BookView.GetClass(chapter)));
                Voodoo.IO.File.Delete(FilePath);
            }

            BookChapterView.Del(string.Format("id in ({0})", ids));

            var book = BookView.GetModelByID(id.ToS());
            var cls = BookView.GetClass(book);

            //更新书籍的最新章节
            var lastChapter = BookChapterView.Find(string.Format("bookid={0} order by ChapterIndex,ID desc",id));
            book.LastChapterID = lastChapter.ID;
            book.LastChapterTitle = lastChapter.Title;
            BookView.Update(book);

            
            chapters = BookChapterView.GetModelList(string.Format("bookid={0}", id));

            foreach (var chapter in chapters)
            {
                CreatePage.CreateBookChapterPage(chapter, book, cls);
            }

            CreatePage.CreateContentPage(book, cls);

            CreatePage.CreateListPage(cls, 0);

            CreatePage.GreateIndexPage();

            Response.Redirect(string.Format("ChapterList.aspx?bookid={0}", id));
        }
    }
}

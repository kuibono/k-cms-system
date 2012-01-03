using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo;
using Voodoo.Basement;
using Voodoo.Model;
using Voodoo.DAL;
using System.IO;


namespace Web.e.api
{
    public partial class ChapterAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int BookID = WS.RequestInt("bookid");
            string BookTitle = WS.RequestString("booktitle");
            int ClassID = WS.RequestInt("classid");
            string ClassName = WS.RequestString("classname");
            string Content = WS.RequestString("content").HtmlDeCode();
            string Title=WS.RequestString("title");

            

            BookChapter c = new BookChapter();

            if (BookChapterView.Exist(string.Format("BookTitle=N'{0}' and Title=N'{1}'", BookTitle, Title)) )
            {
                Response.Clear();
                Response.Write(Voodoo.IO.XML.Serialize(c));
                return;
            }


            c.BookID = BookID;
            c.BookTitle = BookTitle;
            c.ChapterIndex = 0;
            c.ClassID = ClassID;
            c.ClassName = ClassName;
            c.ClickCount = 0;
            c.Content = Content;
            c.Enable = true;
            c.IsFree = true;
            c.IsImageChapter = false;
            c.IsTemp = false;
            c.IsVipChapter = false;
            c.TextLength = Content.TrimHTML().Length;
            c.Title = Title;
            c.UpdateTime = DateTime.Now;
            c.ValumeID = 0;

            BookChapterView.Insert(c);

            Book b = BookView.GetModelByID(BookID.ToString());
            b.LastChapterID = c.ID;
            b.LastChapterTitle = c.Title;
            b.UpdateTime = c.UpdateTime;
            BookView.Update(b);

            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(c));
        }
    }
}

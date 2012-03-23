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
using Voodoo.IO;

namespace Web.e.admin.Book
{
    public partial class ChapterEdit : AdminBase
    {
        /// <summary>
        /// 章节ID
        /// </summary>
        long id = WS.RequestString("id").ToInt64();

        int bookid = WS.RequestInt("bookid");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }

        protected void LoadInfo()
        {
            if (id > 0)
            {
                var chapter = BookChapterView.GetModelByID(id.ToS());
                lb_BookTitle.Text = chapter.BookTitle;
                lb_ValumeName.Text = chapter.ValumeName;
                txt_Title.Text = chapter.Title;
                string Content = Voodoo.IO.File.Read(Server.MapPath(GetBookChapterTxtUrl(chapter, BookView.GetClass(chapter))));
                txt_Content.Text = Content;
                chk_IsVip.Checked = chapter.IsVipChapter;
                chk_IsEnable.Checked = chapter.Enable;
                chk_IsTemp.Checked = chapter.IsTemp;
                chk_IsFree.Checked = chapter.IsFree;
                chk_IsImageChapter.Checked = chapter.IsImageChapter;

            }

            if (bookid > 0)
            {
                var book = BookView.GetModelByID(bookid.ToS());
                lb_BookTitle.Text = book.Title;

            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            var chapter = BookChapterView.GetModelByID(id.ToS());
            chapter.Title = txt_Title.Text;
            chapter.IsVipChapter = chk_IsVip.Checked;
            chapter.Enable = chk_IsEnable.Checked;
            chapter.IsTemp = chk_IsTemp.Checked;
            chapter.IsFree = chk_IsFree.Checked;
            chapter.IsImageChapter = chk_IsImageChapter.Checked;
            if (id > 0)
            {
                BookChapterView.Update(chapter);
            }
            else
            {
                Voodoo.Model.Book book = BookView.GetModelByID(bookid.ToS());
                chapter.BookID = book.ID;
                chapter.BookTitle = book.Title;
                chapter.ClassID = book.ClassID;
                chapter.ClassName = book.ClassName;
                chapter.UpdateTime = DateTime.UtcNow.AddHours(8);

                BookChapterView.Insert(chapter);

                //update Book's Last chapter

                book.LastChapterID = chapter.ID;
                book.LastChapterTitle = chapter.Title;
                book.UpdateTime = chapter.UpdateTime;

                BookView.Update(book);

                //如果是添加，则同时生成书籍信息页面
                CreatePage.CreateContentPage(book, BookView.GetClass(book));
            }

            Voodoo.IO.File.Write(
                Server.MapPath(GetBookChapterTxtUrl(chapter, BookView.GetClass(chapter))),
                txt_Content.Text);
            //生成章节页面
            CreatePage.CreateBookChapterPage(chapter,BookView.GetModelByID(chapter.BookID.ToS()), BookView.GetClass(chapter));

            Response.Redirect(string.Format("ChapterList.aspx?bookid={0}",chapter.BookID));
        }
    }
}

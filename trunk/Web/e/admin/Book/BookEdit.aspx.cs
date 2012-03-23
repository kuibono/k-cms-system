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

namespace Web.e.admin.Book
{
    public partial class BookEdit : AdminBase
    {
        int cls = WS.RequestInt("class");
        int id = WS.RequestInt("id");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }


        #region  Load Book Info
        /// <summary>
        /// Load Book Info
        /// </summary>
        protected void LoadInfo()
        {
            #region 数据绑定

            var classes = NewsAction.NewsClass.Where(p => p.ModelID == 4&&p.IsLeafClass==true);
            ddl_CLass.DataSource = classes;
            ddl_CLass.DataTextField = "ClassName";
            ddl_CLass.DataValueField = "id";
            ddl_CLass.DataBind();

            if (cls > 0)
            {
                ddl_CLass.SelectedValue = cls.ToS();
            }

            var book = BookView.GetModelByID(id.ToS());
            if (book.ID > 0)
            {
                txt_Title.Text = book.Title;
                txt_Author.Text = book.Author;
                ddl_Status.SelectedValue = book.Status.ToS();
                txt_Intro.Text = book.Intro;
                txt_Length.Text = book.Length.ToS();
                txt_ClickCount.Text = book.ClickCount.ToS();
                txt_SaveCount.Text = book.SaveCount.ToS();
                txt_Replycount.Text = book.ReplyCount.ToS();
                txt_TjCount.Text = book.TjCount.ToS();
                chk_IsVip.Checked = book.IsVip;
                chk_IsFirstpost.Checked = book.IsFirstPost;
                chk_Enable.Checked = book.Enable;
                img_Bookface.ImageUrl = book.FaceImage;
            }


            #endregion
        }

        #endregion

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            var book = BookView.GetModelByID(id.ToS());
            book.Title = txt_Title.Text;
            book.Author = txt_Author.Text;
            book.ClassID = ddl_CLass.SelectedValue.ToInt32();
            book.ClassName = ddl_CLass.SelectedItem.Text;
            book.ClickCount = txt_ClickCount.Text.ToInt32();
            book.CorpusID = 0;
            book.CorpusTitle = "";
            book.Enable = chk_Enable.Checked;
            //book.FaceImage=
            book.Intro = txt_Intro.Text;
            book.IsFirstPost = chk_IsFirstpost.Checked;
            book.IsVip = chk_IsVip.Checked;
            book.Length = txt_Length.Text.ToInt32();
            book.ReplyCount = txt_Replycount.Text.ToInt32();
            book.SaveCount = txt_SaveCount.Text.ToInt32();
            book.Status = ddl_Status.SelectedValue.ToByte();
            book.TjCount = txt_TjCount.Text.ToInt32();
            book.ZtID = 0;
            book.ZtName = "";

            

            if (book.ID > 0)
            {
                BookView.Update(book);
            }
            else
            {
                book.Addtime = DateTime.UtcNow.AddHours(8);
                book.UpdateTime = DateTime.UtcNow.AddHours(8);
                book.VipUpdateTime = DateTime.UtcNow.AddHours(8);
                BookView.Insert(book);
            }

            //Deal Book face image
            if (file_Bookfacefile.FileName.IsNullOrEmpty() == false)
            {
                file_Bookfacefile.SaveAs(Server.MapPath("/Book/BookFace/" + book.ID + ".jpg"));
                book.FaceImage = "/Book/BookFace/" + book.ID + ".jpg";
                BookView.Update(book);
            }

            CreatePage.CreateContentPage(book, BookView.GetClass(book));
            Response.Redirect("BookList.aspx?class="+cls.ToS());
        }
    }
}
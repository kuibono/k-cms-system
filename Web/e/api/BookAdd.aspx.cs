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
    public partial class BookAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Title = WS.RequestString("title");
            string Author = WS.RequestString("author");
            int ClassID = WS.RequestInt("classid");
            string ClassName = ClassView.GetModelByID(ClassID.ToString()).ClassName;
            string Intro = WS.RequestString("intro");
            int Length = WS.RequestInt("length", 0);

            Book b = new Book();
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
            b.Status = 1;
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
                b=BookView.Find(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author));
            }

            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(b));
        }
    }
}

﻿using System;
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
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;

using System.Text;

namespace Web.e
{
    public partial class BookDown : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateTxt(WS.RequestInt("id"));
        }

        protected void CreateTxt(int Bookid)
        {
            Book b = BookView.GetModelByID(Bookid.ToS());
            Class cls=BookView.GetClass(b);
            var cs = BookChapterView.GetModelList(string.Format("bookid={0} order by ChapterIndex,id",b.ID));

            StringBuilder sb = new StringBuilder();
            foreach (var c in cs)
            {
                sb.AppendLine(c.Title);
                sb.AppendLine(Voodoo.IO.File.Read(Server.MapPath(BasePage.GetBookChapterTxtUrl(c,cls)),Voodoo.IO.File.EnCode.UTF8).TrimHTML());
            }

            HttpContext.Current.Response.ContentType = "application/text";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.txt", b.Title));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BinaryWrite(sb.ToS().ToByteArray(Encoding.UTF8));
            HttpContext.Current.Response.End();
        }
    }
}

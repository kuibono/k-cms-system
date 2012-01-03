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
    public partial class BookExist : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Title = WS.RequestString("title");
            string Author = WS.RequestString("author");

            bool Exist= BookView.Exist(string.Format("Title=N'{0}' and Author=N'{1}'", Title, Author));

            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(Exist));
        }
    }
}

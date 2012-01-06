using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo;
using Voodoo.Data;
using Voodoo.Data.DbHelper;
using System.IO;


namespace Web
{
    public partial class Search : Voodoo.Basement.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Key = WS.RequestString("key");
            int Model = WS.RequestInt("m", 4);
            int page = WS.RequestInt("p", 1);

            Response.Clear();
            Response.Write(Voodoo.Basement.CreatePage.GetSearchResult(Model, page, Key));
        }
    }
}

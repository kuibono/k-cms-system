using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo.Model;
using Voodoo.DAL;

namespace Web.e.admin
{
    public partial class Test : Voodoo.Basement.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Voodoo.Basement.CreatePage.GreateIndexPage());

            //Class c = ClassView.Find("ID>0");
            //Voodoo.Basement.CreatePage.CreateListPage(c, 1);
            Response.Write(Voodoo.Basement.Functions.buildmenustring(0));
        }
    }
}

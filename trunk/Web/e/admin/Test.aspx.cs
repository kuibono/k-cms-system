using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.e.admin
{
    public partial class Test : Voodoo.Basement.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Voodoo.Basement.CreatePage.GreateIndexPage());
        }
    }
}

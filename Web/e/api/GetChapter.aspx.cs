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
    public partial class GetChapter : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            BookChapter c = BookChapterView.GetModelByID(id.ToS());
            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(c));

        }
    }
}

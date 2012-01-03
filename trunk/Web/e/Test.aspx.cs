using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;
using Voodoo.Html;
namespace Web.e
{
    public partial class Test : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Class cls = ClassView.GetModelList().First();
            //Voodoo.Html.Class.ClassPage cp = new Voodoo.Html.Class.ClassPage();
            //string path = cp.GetHtml(cls);
            //Response.Write(path);

            Class cls = ClassView.GetModelList().Where(p => p.ModelID == 4).First();

            CreatePage.CreateListPage(cls, 1);

            Book b = BookView.GetModelList().First();
            CreatePage.CreateContentPage(b, cls);

            var cps = BookChapterView.GetModelList();
            foreach (var cp in cps)
            {
                CreatePage.CreateBookChapterPage(cp, b, cls);
            }
        }
    }
}

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
    public partial class CreatePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WS.RequestString("action");
            int id = WS.RequestInt("id");

            switch (action)
            {
                case "createindex":
                    CreateIndex();
                    break;
                case "createclasspage":
                    CreateClassPage();
                    break;
                case "createbook":
                    CreateBook(id);
                    break;
                case "createchapters":
                    CreateChapters(id);
                    break;
            }
        }

        protected void CreateIndex()
        {
            Voodoo.Basement.CreatePage.GreateIndexPage();
        }

        protected void CreateClassPage()
        {
            var cls = ClassView.GetModelList();
            foreach (var c in cls)
            {
                Voodoo.Basement.CreatePage.CreateListPage(c, 1);
            }

        }

        protected void CreateBook(int bookid)
        {
            Book b = BookView.GetModelByID(bookid.ToS());
            Voodoo.Basement.CreatePage.CreateContentPage(b, BookView.GetClass(b));
        }

        protected void CreateChapters(int bookid)
        {
            var chapters = BookChapterView.GetModelList(string.Format("bookid={0}",bookid));
            foreach (var c in chapters)
            {
                Voodoo.Basement.CreatePage.CreateBookChapterPage(c, BookView.GetBook(c), BookView.GetClass(c));
            }
        }
    }
}

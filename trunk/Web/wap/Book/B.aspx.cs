using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;

namespace Web.wap.Book
{
    public partial class B : Voodoo.Basement.BasePage
    {
        public Voodoo.Model.Book b;
        public List<BookChapter> cs;
        public Class cls;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            GetBook(id);
        }

        protected void GetBook(int id)
        {
            if (id > 0)
            {
                b = BookView.GetModelByID(id.ToS());
            }
            else
            {
                b = BookView.Find("id>0 order by id desc");
            }

            b.ClickCount++;
            BookView.Update(b);

            cls = BookView.GetClass(b);
            cs = BookChapterView.GetModelList(string.Format("bookid={0} order by ChapterIndex,id desc", b.ID));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Voodoo;
using Voodoo.Data;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;
using Voodoo.Setting;
using System.Text;



namespace Web.e.tool
{
    public partial class GetReadHistory : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string chapters = "";
            if (Voodoo.Cookies.Cookies.GetCookie("history") != null)
            {
                chapters = Voodoo.Cookies.Cookies.GetCookie("history").Value;
            }
            string[] cs = chapters.Split(',');

            List<Cook> cookie = new List<Cook>();

            string ids = "";
            foreach (string chapter in cs)
            {
                string[] Arr_chapter = chapter.Split('|');
                cookie.Add(new Cook() { id = Arr_chapter[0].ToInt64(), time = Arr_chapter[1].ToDateTime() });
                ids += Arr_chapter[0] + ",";
            }

            ids = ids.TrimEnd(',');

            List<BookChapter> list_chapter = BookChapterView.GetModelList(string.Format("id in({0})",ids));

            StringBuilder sb = new StringBuilder();
            sb.Append("document.write('");
            foreach (BookChapter bc in list_chapter)
            {
                Book b=BookView.GetModelByID(bc.BookID.ToString());
                Class c =ClassView.GetModelByID(bc.ClassID.ToString());

                BookChapter new_Chapter=BookChapterView.GetModelByID(b.LastChapterID.ToString());

                sb.Append(string.Format("书籍：<a href=\"{0}\">{1}</a>&nbsp; 您的章节：<a href=\"{2}\">{3}</a>&nbsp; 最新：<a href=\"{4}\">{5}</a><br />", 
                    BasePage.GetBookUrl(b,c),
                    bc.BookTitle,
                    BasePage.GetBookChapterUrl(bc,c),
                    bc.Title,
                    BasePage.GetBookChapterUrl(new_Chapter,c),
                    b.LastChapterTitle
                    ));
            }
            sb.Append("');");

            Response.Clear();
            Response.Write(sb.ToS());
        }
    }
}

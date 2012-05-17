using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;

namespace Web.e.tool
{
    public partial class BookOperate : System.Web.UI.Page
    {
        protected string id = WS.RequestString("id");
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WS.RequestString("action");
            switch (action)
            {
                case "tj":
                    BookTj(id);
                    break;
                case "err":
                    ChapterError(id);
                    break;
            }
        }

        /// <summary>
        /// 推荐票
        /// </summary>
        /// <param name="BookID"></param>
        protected void BookTj(string BookID)
        {
            Book b=BookView.GetModelByID(BookID);
            b.TjCount++;
            BookView.Update(b);

            Response.Clear();
            Response.Write(b.TjCount.ToString().StringToJson());
        }

        protected void ChapterError(string ChapterID)
        {
            BookChapter chapter = BookChapterView.GetModelByID(ChapterID);
            chapter.IsTemp = true;
            chapter.IsImageChapter = true;
            BookChapterView.Update(chapter);

            Response.Clear();
            Response.Write(chapter.ToJsonStr());
        }
    }
}
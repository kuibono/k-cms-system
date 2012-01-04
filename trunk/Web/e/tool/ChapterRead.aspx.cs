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
    public class Cook
    {
        public long id { get; set; }

        public DateTime time { get; set; }
    }

    public partial class ChapterRead : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int chapterid = WS.RequestInt("cid");
            BookChapter bc = BookChapterView.GetModelByID(chapterid.ToS());
            bc.ClickCount=bc.ClickCount > 0 ? bc.ClickCount + 1 : 1;
            BookChapterView.Update(bc);

            //写入Cookie

            List<Cook> cookies = new List<Cook>();
            if (Voodoo.Cookies.Cookies.GetCookie("history") != null)
            {
                string[] chapters = Voodoo.Cookies.Cookies.GetCookie("history").Value.Split(',');
                foreach (string chapter in chapters)
                {
                    string[] arr_chapter=chapter.Split('|');
                    cookies.Add(new Cook(){ id=arr_chapter[0].ToInt64(), time=arr_chapter[1].ToDateTime()});
                }
            }

            cookies = cookies.Where(p => p.id != chapterid).OrderByDescending(p=>p.time).Take(4).ToList();
            cookies.Add(new Cook() { id = chapterid, time = DateTime.Now });

            StringBuilder sb = new StringBuilder();

            foreach (Cook c in cookies)
            {
                sb.Append(string.Format("{0}|{1},",c.id,c.time.ToString()));
            }
            sb = sb.TrimEnd(',');

            HttpCookie _cookie = new HttpCookie("history", sb.ToString());
            _cookie.Expires = DateTime.Now.AddYears(1);

            Voodoo.Cookies.Cookies.SetCookie(_cookie);
        }
    }
}

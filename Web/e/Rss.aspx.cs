using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;

namespace Web.e
{
    public partial class Rss : Voodoo.Basement.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var chapters = BookChapterView.GetModelList("enable=1 order by UpdateTime desc", 500);
            var items = new List<Voodoo.other.SEO.RssItem>();
            foreach (var chapter in chapters)
            {
                items.Add(new Voodoo.other.SEO.RssItem()
                {
                    Title = chapter.BookTitle + "-" + chapter.Title,
                    PutTime = chapter.UpdateTime,
                    Link = SystemSetting.SiteUrl+GetBookChapterUrl(chapter, BookView.GetClass(chapter)),
                    Description = chapter.BookTitle + "Update to chapter:" + chapter.Title + ", from chanel" + chapter.ClassName
                });
            }

            Response.Clear();
            Voodoo.other.SEO.Rss.GetRss(items, SystemSetting.SiteName, SystemSetting.SiteUrl, SystemSetting.Description, SystemSetting.Copyright);
        }
    }
}
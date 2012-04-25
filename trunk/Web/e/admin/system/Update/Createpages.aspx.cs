using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using Voodoo;
using Voodoo.Basement;
using Voodoo.Model;
using Voodoo.DAL;
using System.IO;
using System.Data;
using Voodoo.other.SEO;

namespace Web.e.admin.system.Update
{
    public partial class Createpages : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Index_Click(object sender, EventArgs e)
        {
            CreatePage.GreateIndexPage();
            Js.AlertAndGoback("成功！");
        }

        protected void btn_List_Click(object sender, EventArgs e)
        {
            var cls = ClassView.GetModelList();
            //cls = cls.Where(p => p.IsLeafClass).ToList();
            foreach (var c in cls)
            {
                CreatePage.CreateListPage(c, 1);
            }

            Js.AlertAndGoback("成功！");
            
        }

        protected void btn_Content_Click(object sender, EventArgs e)
        {
            var newses = NewsView.GetModelList();
            newses = newses.Where(p => p.Audit).ToList();
            foreach (var n in newses)
            {
                CreatePage.CreateContentPage(n, NewsView.GetNewsClass(n));
            }

            var imgs = ImageAlbumView.GetModelList();
            foreach (var img in imgs)
            {
                CreatePage.CreateContentPage(img, img.GetClass());
            }

            var ques = QuestionView.GetModelList();
            foreach (var q in ques)
            {
                CreatePage.CreateContentPage(q, q.GetClass());
            }

            var books = BookView.GetModelList();
            foreach (var b in books)
            {
                CreatePage.CreateContentPage(b, BookView.GetClass(b));
            }

            var movies = MovieInfoView.GetModelList();
            foreach (var m in movies)
            {
                CreatePage.CreateContentPage(m, MovieInfoView.GetClass(m));
            }

            Js.AlertAndGoback("成功！");
        }

        protected void btn_ClearAll_Click(object sender, EventArgs e)
        {
            //var cls = NewsAction.NewsClass;
            //cls = cls.Where(p => p.ParentID == 0).ToList();
            //foreach (var c in cls)
            //{
            //    FileInfo file = new FileInfo(Server.MapPath(GetClassUrl(c)));
            //    if (file.Directory.Exists)
            //    {
            //        file.Directory.Delete(true);
            //    }
            //}

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Book/"));
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    file.Delete();
                }

                DirectoryInfo[]  subdirs=dir.GetDirectories();
                foreach (DirectoryInfo subdir in subdirs)
                {
                    subdir.Delete(true);
                }


            }

            Js.AlertAndGoback("成功！");
        }

        protected void btn_Chapter_Click(object sender, EventArgs e)
        {
            var chapters = BookChapterView.GetModelList();
            foreach (var c in chapters)
            {
                CreatePage.CreateBookChapterPage(c, BookView.GetBook(c), BookView.GetClass(c));
            }

            Js.AlertAndGoback("成功！");
        }

        protected void btn_Excute_Click(object sender, EventArgs e)
        {
            Voodoo.Data.IDbHelper Helper = Voodoo.Setting.DataBase.GetHelper();
            GridView1.DataSource = Helper.ExecuteDataTable(CommandType.Text, txt_SQL.Text);
            GridView1.DataBind();


        }

        protected void btn_GenSitreMap_Click(object sender, EventArgs e)
        {
            

            Voodoo.other.SEO.SiteMap sm = new Voodoo.other.SEO.SiteMap();
            sm.url = new List<PageInfo>();

            sm.url.Add(new PageInfo() { changefreq = "always", lastmod = DateTime.Now, loc = SystemSetting.SiteUrl, priority = "1.0" });
            List<Voodoo.Model.Book> bs = BookView.GetModelList("id>0 order by UpdateTime desc", 100);
            foreach (Voodoo.Model.Book b in bs)
            {
                sm.url.Add(new PageInfo() { 
                    changefreq = "daily", 
                    lastmod = b.UpdateTime, 
                    loc = (SystemSetting.SiteUrl + BasePage.GetBookUrl(b, BookView.GetClass(b))).Replace("//Book/", "/Book/"), 
                    priority = "0.8" 
                });
            }

            List<BookChapter> bcs = BookChapterView.GetModelList("id>0 order by UpdateTime desc", 500);
            foreach (BookChapter bc in bcs)
            {
                sm.url.Add(new PageInfo() { 
                    changefreq = "monthly", 
                    lastmod = bc.UpdateTime,
                    loc = (SystemSetting.SiteUrl + BasePage.GetBookChapterUrl(bc, BookView.GetClass(bc))).Replace("//Book/", "/Book/"),
                    priority = "0.7" 
                });
            }

            sm.SaveSiteMap(Server.MapPath("~/sitemapxml/index.xml"));

        }

        protected void btn_Drama_Click(object sender, EventArgs e)
        {
            List<MovieUrlKuaib> ks = MovieUrlKuaibView.GetModelList();
            foreach (var k in ks)
            {
                CreatePage.CreateDramapage(k, MovieInfoView.GetClass(k));
            }

            List<MovieUrlBaidu> bs = MovieUrlBaiduView.GetModelList();
            foreach(var b in bs)
            {
                CreatePage.CreateDramapage(b, MovieInfoView.GetClass(b));
            }
        }
    }
}

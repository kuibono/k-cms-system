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
            Response.Buffer = false;
            foreach (var c in cls)
            {
                try
                {
                    Response.Write(string.Format("正在生成列表页：{0}<br/>", c.ClassName));
                    CreatePage.CreateListPage(c, 1);
                }
                catch (Exception ex)
                {
                    Response.Write(string.Format("{0}<br/>", ex.Message));
                }
            }

            Js.AlertAndGoback("成功！");

        }

        protected void btn_Content_Click(object sender, EventArgs e)
        {
            Response.Buffer = false;
            Js.ScrollEnd();
            var newses = NewsView.GetModelList();
            newses = newses.Where(p => p.Audit).ToList();
            foreach (var n in newses)
            {
                Response.Write(string.Format("正在生成内容页：{0}<br/>", n.Title));
                try
                {
                    CreatePage.CreateContentPage(n, NewsView.GetNewsClass(n));
                }
                catch (Exception ex)
                {
                    Response.Write(string.Format("{0}<br/>", ex.Message));
                }
            }

            var imgs = ImageAlbumView.GetModelList();
            foreach (var img in imgs)
            {
                try
                {
                    Response.Write(string.Format("正在生成内容页：{0}<br/>", img.Title));
                    CreatePage.CreateContentPage(img, img.GetClass());
                }
                catch (Exception ex)
                {
                    Response.Write(string.Format("{0}<br/>", ex.Message));
                }
            }

            var ques = QuestionView.GetModelList();
            foreach (var q in ques)
            {
                try
                {
                    Response.Write(string.Format("正在生成内容页：{0}<br/>", q.Title));
                    CreatePage.CreateContentPage(q, q.GetClass());
                }
                catch (Exception ex)
                {
                    Response.Write(string.Format("{0}<br/>", ex.Message));
                }
            }

            var books = BookView.GetModelList();
            foreach (var b in books)
            {
                try
                {
                    Response.Write(string.Format("正在生成内容页：{0}<br/>", b.Title));
                    CreatePage.CreateContentPage(b, BookView.GetClass(b));
                    Js.ScrollEndStart();
                }
                catch (Exception ex)
                {
                    Response.Write(string.Format("{0}<br/>", ex.Message));
                }
            }

            //try
            //{
            //    var movies = MovieInfoView.GetModelList();
            //    foreach (var m in movies)
            //    {
            //        try
            //        {
            //            Response.Write(string.Format("正在生成内容页：{0}<br/>", m.Title));
            //            CreatePage.CreateContentPage(m, MovieInfoView.GetClass(m));
            //            Js.ScrollEnd();
            //        }
            //        catch (Exception ex)
            //        {
            //            Response.Write(string.Format("{0}<br/>", ex.Message));
            //        }
            //    }
            //}
            //catch { }

            Js.AlertAndGoback("成功！");
        }

        protected void btn_ClearAll_Click(object sender, EventArgs e)
        {
            Response.Buffer = false;

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Book/"));
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    Response.Write(string.Format("正在删除文件：{0}<br/>", file.Name));
                    file.Delete();
                }

                DirectoryInfo[] subdirs = dir.GetDirectories();
                foreach (DirectoryInfo subdir in subdirs)
                {
                    Response.Write(string.Format("正在删除目录：{0}<br/>", subdir.Name));
                    subdir.Delete(true);
                }


            }

            dir = new DirectoryInfo(Server.MapPath(SystemSetting.ClassFolder));
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    Response.Write(string.Format("正在删除文件：{0}<br/>", file.Name));
                    file.Delete();
                }

                DirectoryInfo[] subdirs = dir.GetDirectories();
                foreach (DirectoryInfo subdir in subdirs)
                {
                    Response.Write(string.Format("正在删除目录：{0}<br/>", subdir.Name));
                    subdir.Delete(true);
                }


            }

            Js.AlertAndGoback("成功！");
        }

        protected void btn_Chapter_Click(object sender, EventArgs e)
        {
            Response.Buffer = false;
            var chapters = BookChapterView.GetModelList();
            foreach (var c in chapters)
            {
                try
                {
                    Response.Write(string.Format("正在生成章节：{0}<br/>", c.Title));
                    CreatePage.CreateBookChapterPage(c, BookView.GetBook(c), BookView.GetClass(c));
                    Js.ScrollEndStart();
                }
                catch (Exception ex)
                {
                    Response.Write(string.Format("{0}<br/>", ex.Message));
                }

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
                sm.url.Add(new PageInfo()
                {
                    changefreq = "daily",
                    lastmod = b.UpdateTime,
                    loc = (SystemSetting.SiteUrl + BasePage.GetBookUrl(b, BookView.GetClass(b))).Replace("//Book/", "/Book/"),
                    priority = "0.8"
                });
            }

            List<BookChapter> bcs = BookChapterView.GetModelList("id>0 order by UpdateTime desc", 500);
            foreach (BookChapter bc in bcs)
            {
                sm.url.Add(new PageInfo()
                {
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
            //Response.Buffer = false;
            //List<MovieUrlKuaib> ks = MovieUrlKuaibView.GetModelList();
            //foreach (var k in ks)
            //{
            //    Response.Write(string.Format("正在生成快播页面：{0}<br/>", k.Title));
            //    CreatePage.CreateDramapage(k, MovieInfoView.GetClass(k));
            //}

            //List<MovieUrlBaidu> bs = MovieUrlBaiduView.GetModelList();
            //foreach (var b in bs)
            //{
            //    Response.Write(string.Format("正在生成百度页面：{0}<br/>", b.Title));
            //    CreatePage.CreateDramapage(b, MovieInfoView.GetClass(b));
            //}

            //List<MovieDrama> ds = MovieDramaView.GetModelList();
            //foreach (var d in ds)
            //{
            //    Response.Write(string.Format("正在生成剧集列表页面：{0}<br/>", d.Title));
            //    CreatePage.CreateDramapage(d, MovieInfoView.GetClass(d));
            //}
        }

        protected void btn_CreatePage_Click(object sender, EventArgs e)
        {
            //Response.Buffer = false;
            //var pages = TemplatePageView.GetModelList();
            //foreach (var page in pages)
            //{
            //    Response.Write(string.Format("正在生成剧集列表页面：{0}<br/>", page.PageName));
            //    CreatePage.CreatePages(page);
            //}
        }
    }
}

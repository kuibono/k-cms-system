using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;
using Voodoo.Data;
using Voodoo.Data.DbHelper;

namespace Web.e.tool.Create
{
    public partial class CreatePage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string action = WS.RequestString("a").ToLower();
            string id = WS.RequestString("id");
            switch (action)
            {
                case "createindex":
                    CreateIndex();
                    break;
                case "createbook":

                    CreateBook(id.ToInt32());
                    break;
                case "createmoviepage":
                    CreateMoviePage(id.ToInt32());
                    break;
                case "createchapter":
                    CreateChapter(id.ToInt64());
                    break;
                case "createkuaibopage":
                    CreateKuaiboPage(id.ToInt64());
                    break;
                case "createbaidupage":
                    CreateBaiduPage(id.ToInt64());
                    break;
                case "createdramapage":
                    CreateDramaPage(id.ToInt64());
                    break;
                case "createclasspage":
                    CreateClassPage(id.ToInt32(),WS.RequestString("page").ToInt32(1));
                    break;
                case "getmaxid":
                    GetMaxID(WS.RequestString("table"));
                    break;
                case "getclasspagecount":
                    GetClassPageCount(id.ToInt32());
                    break;

            }

        }

        #region 生成首页
        /// <summary>
        /// 生成首页
        /// </summary>
        protected void CreateIndex()
        {
            Voodoo.Basement.CreatePage.GreateIndexPage();
            Response.Write("生成首页完成");
        }
        #endregion

        #region 生成书籍页
        /// <summary>
        /// 生成书籍页
        /// </summary>
        /// <param name="id"></param>
        protected void CreateBook(int id)
        {
            Book b = BookView.GetModelByID(id.ToS());
            if (b.ID > 0)
            {
                Class c = BookView.GetClass(b);
                Voodoo.Basement.CreatePage.CreateContentPage(b, c);
                Response.Write("小说" + b.Title + "完成");
            }
            else
            {
                Response.Write("不存在");
            }
        }
        #endregion

        #region 生成电影页面
        /// <summary>
        /// 生成电影页面
        /// </summary>
        /// <param name="id"></param>
        protected void CreateMoviePage(int id)
        {
            MovieInfo m = MovieInfoView.GetModelByID(id.ToS());
            if (m.Id > 0)
            {
                Class c = MovieInfoView.GetClass(m);
                Voodoo.Basement.CreatePage.CreateContentPage(m, c);

                var Dramas = MovieDramaView.GetModelList(string.Format("movieid={0}", id));
                var Baidus = MovieUrlBaiduView.GetModelList(string.Format("movieid={0}", id));
                var Kuaibos = MovieUrlKuaibView.GetModelList(string.Format("movieid={0}", id));

                foreach (var d in Dramas)
                {
                    Voodoo.Basement.CreatePage.CreateDramapage(d, c);
                }
                foreach (var d in Baidus)
                {
                    Voodoo.Basement.CreatePage.CreateDramapage(d, c);
                }
                foreach (var d in Kuaibos)
                {
                    Voodoo.Basement.CreatePage.CreateDramapage(d, c);
                }

                Response.Write("电影" + m.Title + "完成");
            }
            else
            {
                Response.Write("不存在");
            }
        }
        #endregion

        #region 生成章节
        /// <summary>
        /// 生成章节
        /// </summary>
        /// <param name="id"></param>
        protected void CreateChapter(long id)
        {
            BookChapter cp = BookChapterView.GetModelByID(id.ToS());
            if (cp.ID > 0)
            {
                Class c = BookView.GetClass(cp);
                Book b = BookView.GetModelByID(cp.BookID.ToS());
                Voodoo.Basement.CreatePage.CreateBookChapterPage(cp, b, c);
                Response.Write(string.Format("书籍《{0}》-章节《{1}》完成", cp.BookTitle, cp.Title));
            }
            else
            {
                Response.Write("不存在");
            }
        }
        #endregion

        #region 生成快播页面
        /// <summary>
        /// 生成快播页面
        /// </summary>
        /// <param name="id"></param>
        protected void CreateKuaiboPage(long id)
        {
            MovieUrlKuaib k = MovieUrlKuaibView.GetModelByID(id.ToS());
            if (k.Id > 0)
            {
                Class c = MovieInfoView.GetClass(k);
                Voodoo.Basement.CreatePage.CreateDramapage(k, c);
                Response.Write(string.Format("《{0}》-快播剧集《{1}》完成", k.MovieTitle, k.Title));
            }
            else
            {
                Response.Write("不存在");
            }
        }
        #endregion

        #region 生成百度影音剧集
        /// <summary>
        /// 生成百度影音剧集
        /// </summary>
        /// <param name="id"></param>
        protected void CreateBaiduPage(long id)
        {
            MovieUrlBaidu b = MovieUrlBaiduView.GetModelByID(id.ToS());
            if (b.Id > 0)
            {
                Class c = MovieInfoView.GetClass(b);
                Voodoo.Basement.CreatePage.CreateDramapage(b, c);
                Response.Write(string.Format("《{0}》-百度影音剧集《{1}》完成", b.MovieTitle, b.Title));
            }
            else
            {
                Response.Write("不存在");
            }
        }
        #endregion

        #region 生成搜索剧集
        /// <summary>
        /// 生成搜索剧集
        /// </summary>
        /// <param name="id"></param>
        protected void CreateDramaPage(long id)
        {
            MovieDrama d = MovieDramaView.GetModelByID(id.ToS());
            if (d.Id > 0)
            {
                Class c = MovieInfoView.GetClass(d);
                Voodoo.Basement.CreatePage.CreateDramapage(d, c);
                Response.Write(string.Format("《{0}》-剧集列表《{1}》完成", d.MovieTitle, d.Title));
            }
        }
        #endregion

        #region 获取列表页面的页数
        /// <summary>
        /// 获取列表页面的页数
        /// </summary>
        /// <param name="id"></param>
        protected void GetClassPageCount(int id)
        {
            Class c = ClassView.GetModelByID(id.ToS());
            TemplateList t = TemplateListView.Find(string.Format("SysModel={0}", c.ModelID.ToS()));
            
            int pagecount = 0;
            switch (c.ModelID)
            {
                case 1:
                    //新闻
                    pagecount = NewsView.Count(string.Format("classid in(select {0} union select id from Class where parentID={0})", id)).GetPageCount(t.ShowRecordCount);
                    break;
                case 2:
                    //图片
                    pagecount = ImageAlbumView.Count(string.Format("classid in(select {0} union select id from Class where parentID={0})", id)).GetPageCount(t.ShowRecordCount);
                    break;
                case 3:
                    //问答
                    pagecount = QuestionView.Count(string.Format("classid in(select {0} union select id from Class where parentID={0})", id)).GetPageCount(t.ShowRecordCount);
                    break;
                case 4:
                    //小说
                    pagecount = BookView.Count(string.Format("classid in(select {0} union select id from Class where parentID={0})", id)).GetPageCount(t.ShowRecordCount);
                    break;
                case 5:
                    //分类
                    pagecount = InfoView.Count(string.Format("classid in(select {0} union select id from Class where parentID={0})", id)).GetPageCount(t.ShowRecordCount);
                    break;
                case 6:
                    //影视
                    pagecount = MovieInfoView.Count(string.Format("classid in(select {0} union select id from Class where parentID={0})", id)).GetPageCount(t.ShowRecordCount);
                    break;
                default:
                    pagecount = 0;
                    break;
            }

            Response.Clear();
            Response.Write(pagecount);
        }
        #endregion

        #region 生成列表页面
        /// <summary>
        /// 生成列表页面
        /// </summary>
        /// <param name="id"></param>
        protected void CreateClassPage(int id, int page)
        {
            Class c = ClassView.GetModelByID(id.ToS());
            Voodoo.Basement.CreatePage.CreateListPage(c, page, false);
            Response.Write(string.Format("创建分类“{0}”第{1}页完成", c.ClassName,page));
        }
        #endregion

        #region 获取最大ID
        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <param name="TableName"></param>
        protected void GetMaxID(string TableName)
        {
            try
            {
                var Sql = Voodoo.Setting.DataBase.GetHelper();
                long mid = Sql.ExecuteScalar(CommandType.Text, string.Format("select max(id) from {0}", TableName)).ToInt64(0);
                Response.Write(mid);
            }
            catch
            {
                Response.Write("0");
            }
        }
        #endregion
    }
}
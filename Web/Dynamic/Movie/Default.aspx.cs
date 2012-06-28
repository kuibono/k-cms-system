using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Voodoo.Basement;
using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;

namespace Web.Dynamic.Movie
{
    public partial class Default : TemplateHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = WS.RequestString("a");
            switch (action)
            {
                case "class":
                    GetClass(WS.RequestString("name"), WS.RequestInt("page", 1));
                    break;
                case "content":
                    Content(WS.RequestString("name"), WS.RequestString("class"));
                    break;
                case "kuaib":
                    Kuaib(WS.RequestInt("id", 1));
                    break;
                case "baidu":
                    Baidu(WS.RequestInt("id", 1));
                    break;
                case "drama":
                    Drama(WS.RequestInt("id", 1));
                    break;
                case "index":
                default:
                    Index();
                    break;
            }
        }

        #region 分类页面
        /// <summary>
        /// 分类页面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        protected void GetClass(string Name, int page)
        {
            Class cls = ClassView.Find(string.Format("ClassName=N'{0}'", Name));
            Response.Clear();
            Response.Write(CreateListPage(cls, page));
        }
        #endregion

        #region 首页
        /// <summary>
        /// 首页
        /// </summary>
        protected void Index()
        {
            Response.Clear();
            Response.Write(GetIndex());
        }
        #endregion

        #region 内容页
        /// <summary>
        /// 内容页
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Class"></param>
        protected void Content(string Name, string Class)
        {
            MovieInfo mv = MovieInfoView.Find(string.Format("StandardTitle=N'{0}' and ClassName=N'{1}'", Name, Class));
            Response.Clear();
            Response.Write(CreateContentPage(mv, MovieInfoView.GetClass(mv)));
        }
        #endregion

        #region 快播
        /// <summary>
        /// 快播
        /// </summary>
        /// <param name="id"></param>
        protected void Kuaib(int id)
        {
            MovieUrlKuaib k = MovieUrlKuaibView.GetModelByID(id.ToS());
            Response.Clear();
            Response.Write(CreateDramapage(k,MovieInfoView.GetClass(k)));
        }
        #endregion

        #region  百度
        /// <summary>
        /// 百度
        /// </summary>
        /// <param name="id"></param>
        protected void Baidu(int id)
        {
            MovieUrlBaidu b = MovieUrlBaiduView.GetModelByID(id.ToS());
            Response.Clear();
            Response.Write(CreateDramapage(b, MovieInfoView.GetClass(b)));
        }
        #endregion

        #region 单集列表
        /// <summary>
        /// 单集列表
        /// </summary>
        /// <param name="id"></param>
        protected void Drama(int id)
        {
            MovieDrama d = MovieDramaView.GetModelByID(id.ToS());
            Response.Clear();
            Response.Write(CreateDramapage(d,MovieInfoView.GetClass(d)));
        }
        #endregion
    }
}
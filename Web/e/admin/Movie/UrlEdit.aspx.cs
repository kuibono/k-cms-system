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


namespace Web.e.admin.Movie
{
    public partial class UrlEdit : AdminBase
    {
        protected string type = WS.RequestString("type");
        protected long id = WS.RequestString("id").ToInt64();
        protected int movieID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }

        protected void LoadInfo()
        {
            switch (type)
            {
                case "kuaib":
                    var kuaibUrl = MovieUrlKuaibView.GetModelByID(id.ToS());
                    txt_BookTitle.Text = kuaibUrl.MovieTitle;
                    txt_Title.Text = kuaibUrl.Title;
                    txt_Url.Text = kuaibUrl.Url;
                    movieID = kuaibUrl.MovieID;
                    break;
                case "baidu":
                    var baiduUrl = MovieUrlBaiduView.GetModelByID(id.ToS());
                    txt_BookTitle.Text = baiduUrl.MovieTitle;
                    txt_Title.Text = baiduUrl.Title;
                    txt_Url.Text = baiduUrl.Url;
                    movieID = baiduUrl.MovieID;
                    break;
                case "mag":
                    var magUrl = MovieUrlMagView.GetModelByID(id.ToS());
                    txt_BookTitle.Text = magUrl.MovieTitle;
                    txt_Title.Text = magUrl.Title;
                    txt_Url.Text = magUrl.Url;
                    movieID = magUrl.MovieID;
                    break;
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            movieID = WS.RequestInt("movieid", 0);
            MovieInfo movie = MovieInfoView.GetModelByID(movieID.ToS());
            #region deal
            switch (type)
            {
                case "kuaib":
                    var kuaibUrl = MovieUrlKuaibView.GetModelByID(id.ToS());
                    kuaibUrl.MovieTitle = movie.Title;
                    kuaibUrl.Title = txt_Title.Text;
                    kuaibUrl.Url = txt_Url.Text;
                    kuaibUrl.MovieID = movie.Id;
                    kuaibUrl.UpdateTime = DateTime.Now;
                    if (kuaibUrl.Id > 0)
                    {
                        MovieUrlKuaibView.Update(kuaibUrl);
                    }
                    else
                    {
                        MovieUrlKuaibView.Insert(kuaibUrl);
                    }
                    break;
                case "baidu":
                    var baiduUrl = MovieUrlBaiduView.GetModelByID(id.ToS());
                    baiduUrl.MovieTitle = movie.Title;
                    baiduUrl.Title = txt_Title.Text;
                    baiduUrl.Url = txt_Url.Text;
                    baiduUrl.MovieID = movie.Id;
                    baiduUrl.UpdateTime = DateTime.Now;
                    if (baiduUrl.Id > 0)
                    {
                        MovieUrlBaiduView.Update(baiduUrl);
                    }
                    else
                    {
                        MovieUrlBaiduView.Insert(baiduUrl);
                    }
                    break;
                case "mag":
                    var magUrl = MovieUrlMagView.GetModelByID(id.ToS());
                    magUrl.MovieTitle =  movie.Title;
                    magUrl.Title = txt_Title.Text;
                    magUrl.Url = txt_Url.Text;
                    magUrl.MovieID = movie.Id;
                    magUrl.UpdateTime = DateTime.Now;
                    if (magUrl.Id > 0)
                    {
                        MovieUrlMagView.Update(magUrl);
                    }
                    else
                    {
                        MovieUrlMagView.Insert(magUrl);
                    }
                    break;
            }
            #endregion 

            Response.Redirect("UrlList.aspx?type=" + type + "&movieid="+movieID);
        }
    }
}
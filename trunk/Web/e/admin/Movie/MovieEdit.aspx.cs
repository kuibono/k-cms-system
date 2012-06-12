﻿using System;
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
    public partial class MovieEdit : AdminBase
    {
        protected int cls = WS.RequestInt("class", 0);
        protected int zt = WS.RequestInt("zt", 0);
        protected string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            url = string.Format("MovieList.aspx?class={0}&zt={1}", cls, zt);
            if (!IsPostBack)
            {

                LoadInfo();
            }
        }

        protected void LoadInfo()
        {
            ddl_Class.DataSource = NewsAction.NewsClass.Where(p => p.ModelID == 6).ToList();
            ddl_Class.DataTextField = "ClassName";
            ddl_Class.DataValueField = "ID";
            ddl_Class.DataBind();

            ddl_Class.SelectedValue = cls.ToS();

            int id = WS.RequestInt("id");

            MovieInfo mi = MovieInfoView.GetModelByID(id.ToS());
            txt_Title.Text = mi.Title;
            txt_Director.Text = mi.Director;
            txt_Actors.Text = mi.Actors;
            txt_Tags.Text = mi.Tags;
            txt_Location.Text = mi.Location;
            txt_PublicYear.Text = mi.PublicYear;
            txt_Intro.Text = mi.Intro;
            chk_IsMovie.Checked = mi.IsMove;
            img_Movieface.ImageUrl = mi.FaceImage;
            rbl_Status.SelectedValue = mi.Status.ToS();
            chk_Enable.Checked = mi.Enable;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            MovieInfo mi = MovieInfoView.GetModelByID(id.ToS());
            mi.ClassID = ddl_Class.SelectedItem.Value.ToInt32();
            mi.ClassName = ddl_Class.SelectedItem.Text;
            mi.Title = txt_Title.Text;
            mi.Director = txt_Director.Text;
            mi.Actors = txt_Actors.Text;
            mi.Tags = txt_Tags.Text;
            mi.Location = txt_Location.Text;
            mi.PublicYear = txt_PublicYear.Text;
            mi.Intro = txt_Intro.Text;
            mi.IsMove = chk_IsMovie.Checked;
            mi.Status = rbl_Status.SelectedValue.ToInt32();
            mi.Enable = chk_Enable.Checked;
            mi.InsertTime = DateTime.UtcNow.AddHours(8);
            mi.UpdateTime = DateTime.UtcNow.AddHours(8);
            if (mi.Id > 0)
            {
                //update
                MovieInfoView.Update(mi);
            }
            else
            {
                MovieInfoView.Insert(mi);
            }

            //Deal Book face image
            if (file_Moviefacefile.FileName.IsNullOrEmpty() == false)
            {
                file_Moviefacefile.SaveAs(Server.MapPath("/u/MoviekFace/" + mi.Id + ".jpg"));
                mi.FaceImage = "/u/MoviekFace/" + mi.Id + ".jpg";
                MovieInfoView.Update(mi);
            }

            Class c=MovieInfoView.GetClass(mi);

            //Create statis pages
            CreatePage.CreateContentPage(mi, c);

            var kuaibos = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0}", mi.Id));
            var baidus = MovieUrlBaiduView.GetModelList(string.Format("MovieID={0}", mi.Id));
            var dramas = MovieDramaView.GetModelList(string.Format("MovieID={0}", mi.Id));
            foreach (var kuaib in kuaibos)
            {
                CreatePage.CreateDramapage(kuaib, c);
            }
            foreach (var baidu in baidus)
            {
                CreatePage.CreateDramapage(baidu, c);
            }
            foreach (var drama in dramas)
            {
                CreatePage.CreateDramapage(drama, c);
            }

            Response.Redirect(url);
        }
    }
}
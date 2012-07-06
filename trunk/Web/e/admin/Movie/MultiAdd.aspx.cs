﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Voodoo;
using Voodoo.Basement;
using Voodoo.Model;
using Voodoo.DAL;

namespace Web.e.admin.Movie
{
    public partial class MultiAdd : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = WS.RequestInt("id");
                MovieInfo mv = MovieInfoView.GetModelByID(id.ToS());
                txt_BookTitle.Text = mv.Title;

            }
        }

        protected void btn_Bind_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = CollectDramas(txt_source.Text, WS.RequestInt("id"));
            GridView1.DataBind();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            var dramas = CollectDramas(txt_source.Text,WS.RequestInt("id"));
            SaveDramas(WS.RequestInt("id"), dramas);
            Response.Redirect("MovieList.aspx");
        }
    }
}
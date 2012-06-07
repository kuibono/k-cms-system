using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Voodoo;
using Voodoo.Data;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;
using Voodoo.Setting;
namespace Web.e.admin.Movie
{
    public partial class MovieList : AdminBase
    {
        protected static int cls = WS.RequestInt("class", 0);
        protected static int zt = WS.RequestInt("zt", 0);
        protected static string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            cls = WS.RequestInt("class", 0);
            zt = WS.RequestInt("zt", 0);
            url = string.Format("?class={0}&zt={1}", cls, zt);
            LoadInfo();
            if (WS.RequestString("action") == "del")
            {
                Button1_Click(sender, e);
            }
        }

        #region 加载列表
        /// <summary>
        /// 加载列表
        /// </summary>
        protected void LoadInfo()
        {
            pager.PageSize = SystemSetting.MagageListSize;

            ddl_Class.DataSource = NewsAction.NewsClass.Where(pa => pa.IsLeafClass && pa.ModelID == 6).ToList();
            ddl_Class.DataTextField = "ClassName";
            ddl_Class.DataValueField = "ID";
            ddl_Class.DataBind();
            ddl_Class.Items.Add(new ListItem("--所有栏目--", ""));
            ddl_Class.SelectedValue = "";



            ddl_Zt.DataSource = NewsAction.NewsZt;
            ddl_Zt.DataTextField = "ZtName";
            ddl_Zt.DataValueField = "ID";
            ddl_Zt.DataBind();
            ddl_Zt.Items.Add(new ListItem("--所有专题--", ""));
            //ddl_Zt.SelectedValue = "";


            ddl_Class_search.DataSource = NewsAction.NewsClass.Where(pa => pa.IsLeafClass && pa.ModelID == 6).ToList();
            ddl_Class_search.DataTextField = "ClassName";
            ddl_Class_search.DataValueField = "ID";
            ddl_Class_search.DataBind();
            ddl_Class_search.Items.Add(new ListItem("--新增请选择栏目--", ""));
            ddl_Class_search.SelectedValue = "";

            if (WS.RequestInt("class") > 0)
            {
                ddl_Class_search.SelectedValue = WS.RequestString("class");
                ddl_Class.SelectedValue = WS.RequestString("class");
            }

            string str_sql = "";
            //if (ddl_Prop.SelectedValue != "")
            //{
            //    switch (ddl_Prop.SelectedValue)
            //    {
            //        case "SetTop":
            //            str_sql += "SetTop=1 ";
            //            break;
            //        case "Tuijian":
            //            str_sql += "Tuijian=1 ";
            //            break;
            //        case "Toutiao":
            //            str_sql += "Toutiao=1 ";
            //            break;
            //        case "Audit":
            //            str_sql += "Audit=1";
            //            break;
            //        case "UnAudit":
            //            str_sql += "Audit=0";
            //            break;
            //    }
            //}

            if (txt_Key.Text.Trim().Length > 0 && txt_Column.SelectedValue != "")
            {
                if (str_sql.Trim().Length > 0)
                {
                    str_sql += " and ";
                }
                str_sql += txt_Column.SelectedValue + " like N'%" + txt_Key.Text + "%'";
            }
            if (txt_Key.Text.Trim().Length > 0 && txt_Column.SelectedValue == "")
            {
                if (str_sql.Trim().Length > 0)
                {
                    str_sql += " and ";
                }
                str_sql +=  string.Format("Title like N'%{0}%' or Director like N'%{0}%' or Tags like N'%{0}%' or Location like N'%{0}%' or Intro like N'%{0}%'",txt_Key.Text.ToSqlEnCode());
            }

            if (ddl_Class.SelectedValue != "")
            {
                if (str_sql.Trim().Length > 0)
                {
                    str_sql += " and ";
                }
                str_sql += "ClassID=" + ddl_Class.SelectedValue; ;
            }

            if (cls > 0)
            {
                if (str_sql.Trim().Length > 0)
                {
                    str_sql += " and ";
                }
                str_sql += "ClassID=" + cls;
                //ddl_Class_search.Visible = false;
                ddl_Class_search.Enabled = false;
                ddl_Class.Enabled = false;
            }
            if (ddl_Class_search.SelectedValue != "")
            {
                if (str_sql.Trim().Length > 0)
                {
                    str_sql += " and ";
                }
                str_sql += "ClassID=" + ddl_Class_search.SelectedValue;
            }

            if (zt > 0)
            {
                if (str_sql.Trim().Length > 0)
                {
                    str_sql += " and ";
                }
                str_sql += "ZtID=" + zt;
            }

            //str_sql += "order by " + ddl_Order.SelectedValue + " " + ddl_Desc.SelectedValue;

            ph p = new ph();
            p.CurrentPage = pager.CurrentPageIndex;
            p.Fields = "*";
            p.Filter = str_sql;
            p.group = "";
            p.PageSize = pager.PageSize;
            p.PrimaryKey = "ID";
            p.Sort = ddl_Order.SelectedValue + " " + ddl_Desc.SelectedValue;
            p.Tables = "MovieInfo";

            rp_list.DataSource = p.GetTable();
            pager.RecordCount = p.Count();
            rp_list.DataBind();


        }
        #endregion

        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_disable_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");

            GetHelper().ExecuteNonQuery(CommandType.Text, string.Format("update MovieInfo set Enable=0 where id in({0})", ids));

            if (cls > 0)
            {
                CreatePage.CreateListPage(ClassView.GetModelByID(cls.ToS()), 1);
            }
            CreatePage.GreateIndexPage();
            Js.Jump(url);
        }

        protected void btn_enable_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            GetHelper().ExecuteNonQuery(CommandType.Text, string.Format("update MovieInfo set Enable=1 where id in({0})", ids));



            if (cls > 0)
            {
                CreatePage.CreateListPage(ClassView.GetModelByID(cls.ToS()), 1);
            }
            CreatePage.GreateIndexPage();
            Js.Jump(url);
        }

 

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            GetHelper().ExecuteNonQuery(CommandType.Text, string.Format("delete from  MovieInfo where id in({0})", ids));
            if (cls > 0)
            {
                CreatePage.CreateListPage(ClassView.GetModelByID(cls.ToS()), 1);
            }
            CreatePage.GreateIndexPage();
            Js.AlertAndChangUrl("删除成功！", url);
        }

        protected void pager_PageChanged(object sender, EventArgs e)
        {
            LoadInfo();
        }

        protected void btn_createPage_Click(object sender, EventArgs e)
        {

            Class c = ClassView.GetModelByID(cls.ToS());
            string[] ids = WS.RequestString("id").Split(',');
            foreach (string id in ids)
            {
                MovieInfo mv = MovieInfoView.GetModelByID(id);
                CreatePage.CreateContentPage(mv,c);
                var kuaibos = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0}",id));
                var baidus = MovieUrlBaiduView.GetModelList(string.Format("MovieID={0}", id));
                var dramas = MovieDramaView.GetModelList(string.Format("MovieID={0}", id));
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

            }

            if (cls > 0)
            {
                try
                {
                    CreatePage.CreateListPage(c, 1);
                }
                catch { }
            }
            CreatePage.GreateIndexPage();
            Js.Jump(url);
        }
    }
}
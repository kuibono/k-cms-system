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


namespace Web.e.admin.images
{
    public partial class ImageList : AdminBase
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

            ddl_Class.DataSource = NewsAction.NewsClass.Where(pa => pa.IsLeafClass&&pa.ModelID==2).ToList();
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
            ddl_Zt.SelectedValue = "";


            ddl_Class_search.DataSource = NewsAction.NewsClass.Where(pa => pa.IsLeafClass && pa.ModelID == 2).ToList();
            ddl_Class_search.DataTextField = "ClassName";
            ddl_Class_search.DataValueField = "ID";
            ddl_Class_search.DataBind();
            ddl_Class_search.Items.Add(new ListItem("--新增请选择栏目--", ""));
            ddl_Class_search.SelectedValue = "";

            if (WS.RequestInt("class") > 0)
            {
                ddl_Class_search.SelectedValue = WS.RequestString("class");
            }

            string str_sql = "";
            if (ddl_Prop.SelectedValue != "")
            {
                switch (ddl_Prop.SelectedValue)
                {
                    case "SetTop":
                        str_sql += "SetTop=1 ";
                        break;
                }
            }

            if (txt_Key.Text.Trim().Length > 0 && txt_Column.SelectedValue != "")
            {
                if (str_sql.Trim().Length > 0)
                {
                    str_sql += " and ";
                }
                str_sql += txt_Column.SelectedValue + " like '%" + txt_Key.Text + "%'";
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
            p.Tables = "ImageAlbum";

            rp_list.DataSource = p.GetTable();
            pager.RecordCount = p.Count();
            rp_list.DataBind();


        }
        #endregion

        protected void pager_PageChanged(object sender, EventArgs e)
        {

        }

        protected void btn_SetTop_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            GetHelper().ExecuteNonQuery(CommandType.Text, string.Format("update ImageAlbum set SetTop=1 where id in({0})", ids));

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

            ImageAction.DeleteImageAlbum(string.Format("id in ({0})",ids));

            if (cls > 0)
            {
                CreatePage.CreateListPage(ClassView.GetModelByID(cls.ToS()), 1);
            }
            CreatePage.GreateIndexPage();
            Js.AlertAndChangUrl("删除成功！", url);
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {

        }
    }
}

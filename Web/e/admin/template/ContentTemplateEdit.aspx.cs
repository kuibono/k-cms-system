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

namespace Web.e.admin.template
{
    public partial class ContentTemplateEdit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }

        protected void LoadInfo()
        {
            ddl_SysModel.DataSource = TemplateAction.AllSysModel;
            ddl_SysModel.DataTextField = "ModelName";
            ddl_SysModel.DataValueField = "ID";
            ddl_SysModel.DataBind();

            int id = WS.RequestInt("id");
            TemplateContent tl = TemplateContentView.GetModelByID(id.ToS());
            txt_TempName.Text = tl.TempName;
            txt_TimeFormat.Text = tl.TimeFormat;
            txt_Content.Text = tl.Content;
            ddl_SysModel.SelectedValue = tl.SysModel.ToS();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            TemplateContent tl = TemplateContentView.GetModelByID(id.ToS());

            tl.TempName = txt_TempName.Text;;
            tl.TimeFormat = txt_TimeFormat.Text;
            tl.Content = txt_Content.Text.Replace("'","''");
            tl.SysModel = ddl_SysModel.SelectedValue.ToInt32();
            if (tl.ID > 0)
            {
                TemplateContentView.Update(tl);
            }
            else
            {
                tl.GroupID = 1;
                tl.SysModel = 1;
                TemplateContentView.Insert(tl);
            }
            Js.AlertAndGoback("保存成功！");
        }
    }
}

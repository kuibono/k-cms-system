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

namespace Web.e.admin.user
{
    public partial class GroupEdit : BasePage
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

            ddl_RegForm.DataSource = UserFormView.GetModelList();
            ddl_RegForm.DataTextField = "FormName";
            ddl_RegForm.DataValueField = "ID";
            ddl_RegForm.DataBind();

            int id = WS.RequestInt("id");
            Voodoo.Model.UserGroup g = UserGroupView.GetModelByID(id.ToS());
            if (g.ID < 0)
            {
                return;
            }
            txt_GroupName.Text = g.GroupName;
            txt_grade.Text = g.Grade.ToS();
            txt_MaxPost.Text = g.MaxPost.ToS();
            chk_PostAotuAudit.Checked = g.PostAotuAudit;
            chk_EnableReg.Checked = g.EnableReg;
            chk_RegAutoAudit.Checked = g.RegAutoAudit;
            ddl_RegForm.SelectedValue = g.RegForm.ToS();

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            Voodoo.Model.UserGroup g = UserGroupView.GetModelByID(id.ToS());

            g.GroupName = txt_GroupName.Text;
            g.Grade = txt_grade.Text.ToInt32();
            g.MaxPost = txt_MaxPost.Text.ToInt32();
            g.PostAotuAudit = chk_PostAotuAudit.Checked;
            g.EnableReg = chk_EnableReg.Checked;
            g.RegAutoAudit = chk_RegAutoAudit.Checked;
            g.RegForm = ddl_RegForm.SelectedValue.ToInt32();

            if (g.ID > 0)
            {
                UserGroupView.Update(g);
            }
            else
            {
                UserGroupView.Insert(g);
            }
            Js.AlertAndChangUrl("保存成功！", "GroupList.aspx");

        }
    }
}

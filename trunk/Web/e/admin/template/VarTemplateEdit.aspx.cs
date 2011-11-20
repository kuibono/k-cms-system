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
    public partial class VarTemplateEdit : AdminBase
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
            int id = WS.RequestInt("id");
            TemplateVar tl = TemplateVarView.GetModelByID(id.ToS());
            txt_VarName.Text = tl.VarName;
            txt_Content.Text = tl.Content;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            TemplateVar tl = TemplateVarView.GetModelByID(id.ToS());

            tl.VarName = txt_VarName.Text; 
            tl.Content = txt_Content.Text.Replace("'", "''");

            if (tl.ID > 0)
            {
                TemplateVarView.Update(tl);
            }
            else
            {
                tl.GroupID = 1;
                TemplateVarView.Insert(tl);
            }
            Js.AlertAndGoback("保存成功！");
        }
    }
}

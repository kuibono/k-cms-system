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
    public partial class UserFormEdit : AdminBase
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
            UserForm f = UserFormView.GetModelByID(id.ToS());
            if (f.ID < 0)
            {
                return;
            }
            txt_FormName.Text = f.FormName;
            txt_Content.Text = f.Content;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            UserForm f = UserFormView.GetModelByID(id.ToS());
            f.FormName = txt_FormName.Text;
            f.Content = txt_Content.Text.TrimDbDangerousChar();

            if (f.ID > 0)
            {
                UserFormView.Update(f);
            }
            else
            {
                UserFormView.Insert(f);
            }

            Js.AlertAndChangUrl("保存成功！", "UserFormList.aspx");
        }
    }
}

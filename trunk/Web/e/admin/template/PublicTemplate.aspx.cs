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

namespace Web.e.admin.template
{
    public partial class PublicTemplate :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }

        public void LoadInfo()
        {
            string action = WS.RequestString("action", "IndexContent");

            txt_Content.Text = GetHelper().ExecuteScalar(CommandType.Text, string.Format("select top 1 {0} from TemplatePublic where id>0", action)).ToString();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string action = WS.RequestString("action", "IndexContent");
            GetHelper().ExecuteNonQuery(CommandType.Text, string.Format("update TemplatePublic set {0}='{1}'",action,txt_Content.Text.TrimDbDangerousChar()));
            Js.AlertAndGoback("保存成功！");
        }
    }
}

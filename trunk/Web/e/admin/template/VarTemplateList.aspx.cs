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
    public partial class VarTemplateList : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WS.RequestString("action") == "del")
            {
                btn_Del_Click(sender, e);
            }
            if (!IsPostBack)
            {
                BindList();
            }
        }

        protected void BindList()
        {
            rp_list.DataSource = TemplateVarView.GetModelList();
            rp_list.DataBind();
        }

        protected void btn_Del_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            TemplateVarView.Del(string.Format("id in ({0})", ids));
        }
    }
}

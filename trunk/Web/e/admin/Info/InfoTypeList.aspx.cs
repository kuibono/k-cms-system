using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using Voodoo;
using Voodoo.Data;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;
using Voodoo.Setting;

namespace Web.e.admin.Info
{
    public partial class InfoTypeList : AdminBase
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

        protected void btn_Del_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            InfoTypeView.Del(string.Format("id in ({0})", ids));
        }
        protected void BindList()
        {
            rp_list.DataSource = InfoTypeView.GetModelList();
            rp_list.DataBind();
        }
    }
}
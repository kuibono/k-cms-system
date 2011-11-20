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
    public partial class GroupList : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WS.RequestString("action") == "del")
            {
                Button1_Click(sender, e);
            }

            if (!IsPostBack)
            {
                BindList();
            }
        }

        protected void BindList()
        {
            rp_list.DataSource = UserGroupView.GetModelList();
            rp_list.DataBind();
        }

        protected void btn_disable_Click(object sender, EventArgs e)
        {

        }

        protected void btn_enable_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            UserGroupView.Del(string.Format("id in ({0})",ids));
            Js.AlertAndChangUrl("删除成功！", "GroupList.aspx");
        }


    }
}

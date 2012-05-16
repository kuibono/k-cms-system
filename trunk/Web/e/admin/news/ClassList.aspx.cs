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

namespace Web.e.admin.news
{
    public partial class ClassList : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WS.RequestString("action") == "del")
            {
                string ids=WS.RequestString("id");
                ClassView.Del(string.Format("id in ({0})",ids));
                Voodoo.Cache.Cache.SetCache("_NewClassList", null);
            }

            if (!IsPostBack)
            {
                LoadList();
            }
        }

        protected void LoadList()
        {
            List<Class> cls = NewsAction.NewsClass;
            rp_list.DataSource = cls;
            rp_list.DataBind();
        }

        protected void btn_order_Click(object sender, EventArgs e)
        {
            //顺序修改
        }


    }
}

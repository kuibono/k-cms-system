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
    public partial class ClassList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

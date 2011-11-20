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

namespace Web.e.post
{
    public partial class PostList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WS.RequestString("action") == "del")
            {
                int id = WS.RequestInt("id");
                if (id > 0)
                {
                    NewsView.Del(string.Format("id={0}", id));
                }
            }

            if (!IsPostBack)
            {
                BindList();
            }
        }

        protected void BindList()
        {
            rp_list.DataSource = NewsView.GetModelList(string.Format("AutorID={0}", UserAction.opuser.ID));
            rp_list.DataBind();
        }
    }
}

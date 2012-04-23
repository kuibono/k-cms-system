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


namespace Web.e.admin.Movie
{
    public partial class UrlList : AdminBase
    {
        protected int id = WS.RequestInt("movieid");
        protected string type = WS.RequestString("type");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
            if (WS.RequestString("action") == "del")
            {
                Button1_Click(sender, e);
            }
        }

        protected void BindList()
        {
            switch (type)
            {
                case "kuaib":
                    var cpl = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0}", id));
                    rp_List.DataSource = cpl;
                    rp_List.DataBind();
                    break;
                case "baidu":
                    var cpl_baidu = MovieUrlBaiduView.GetModelList(string.Format("MovieID={0}", id));
                    rp_List.DataSource = cpl_baidu;
                    rp_List.DataBind();
                    break;
                case "mag":
                    var cpl_mag = MovieUrlMagView.GetModelList(string.Format("MovieID={0}", id));
                    rp_List.DataSource = cpl_mag;
                    rp_List.DataBind();
                    break;
                default:
                    var df = MovieUrlKuaibView.GetModelList(string.Format("MovieID={0}", id));
                    rp_List.DataSource = df;
                    rp_List.DataBind();
                    break;
            }

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
            switch (type)
            {
                case "kuaib":
                    MovieUrlKuaibView.Del(string.Format("id in ({0})", ids));
                    break;
                case "baidu":
                    MovieUrlBaiduView.Del(string.Format("id in ({0})", ids));
                    break;
                case "mag":
                    MovieUrlMagView.Del(string.Format("id in ({0})", ids));
                    break;
            }

            Response.Redirect(string.Format("UrlList.aspx?bookid={0}", id));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Voodoo.Data;
using Voodoo.Model;
using Voodoo.DAL;
using System.Web;
using System.Web.UI.WebControls;

using Voodoo.IO;
using System.IO;
namespace Voodoo.Basement
{
    public class AdminBase:BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["sys_user"] == null || System.Web.HttpContext.Current.Session["sys_user"].ToInt32() <= 0)
            {
                Response.Clear();
                Response.Write("<script type='text/javascript'>parent.parent.location.href='/e/admin/login.aspx'</script>");
                Response.End();
                return;
            }

            base.OnInit(e);
        }
    }
}

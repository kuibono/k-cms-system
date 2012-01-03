using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
//using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Voodoo;
using Voodoo.Data;
using Voodoo.Data.DbHelper;
using System.IO;


namespace Web
{
    public partial class _Default : Voodoo.Basement.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(Server.MapPath("~/Book/Index" + SystemSetting.ExtName));
            if (file.Exists)
            {
                Server.Transfer("~/Book/Index" + SystemSetting.ExtName);
            }
            else
            {
                Response.Write("当前系统还没有首页，请登录<a href='e/admin/'>后台</a>生成！");
            }
            //Response.Write("引子".GetNumberFromTitle());

        }
    }
}

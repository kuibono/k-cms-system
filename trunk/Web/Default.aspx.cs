﻿using System;
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


namespace Web
{
    public partial class _Default : Voodoo.Basement.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.Transfer("~/Index" + SystemSetting.ExtName);
        }
    }
}

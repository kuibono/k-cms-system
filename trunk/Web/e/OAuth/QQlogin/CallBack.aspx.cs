using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QConnectSDK;
using QConnectSDK.Models;
namespace Web.e.OAuth.QQlogin
{
    public partial class CallBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["code"] != null)
            {
                QOpenClient qzone = null;
                User currentUser = null;
                var verifier = Request.Params["code"];
                string state = Session["requeststate"].ToString();
                qzone = new QOpenClient(verifier, state);
                currentUser = qzone.GetCurrentUser();
                if (null != currentUser)
                {
                    
                }
                Session["QzoneOauth"] = qzone;
            }
        }
    }
}
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


namespace Web.e.member
{
    public partial class RegisterForm : BasePage
    {
        protected string formString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int formID = UserGroupView.GetModelByID(WS.RequestString("group")).RegForm;
            formString = UserFormView.GetModelByID(formID.ToS()).Content;
        }
    }
}

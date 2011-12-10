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


namespace Web.e.tool
{
    public partial class click : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int model = WS.RequestInt("m");
            int id = WS.RequestInt("id");

            SysModel sm= SysModelView.GetModelByID(model.ToS());
            if (sm.ID < 0)
            {
                return;
            }
            if (id < 0)
            {
                return;
            }
            string tableName = sm.TableName;
            string str_sql = string.Format("update {0} set ClickCount=ClickCount+1 where ID={1}", tableName, id);
            GetHelper().ExecuteNonQuery(CommandType.Text, str_sql);


        }
    }
}

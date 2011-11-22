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

using Voodoo.Data;
using Voodoo.Data.DbHelper;

namespace Web
{
    public partial class _Default : Voodoo.Basement.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Server.Transfer("~/Index" + SystemSetting.ExtName);

            IDbHelper Helper = new SqlHelper(Voodoo.Setting.DataBase.ConnStr);
            DataTable dt = Helper.ExecuteDataTable(CommandType.Text, "select * from news");


            string str_insert = "SET IDENTITY_INSERT news ON ;insert into [news](";
            for(int i=0;i<dt.Columns.Count;i++)
            {
                str_insert += "["+dt.Columns[i].ColumnName+"]";
                if (i != dt.Columns.Count - 1)
                {
                    str_insert += ",";
                }
            }
            str_insert += ") values(";

            string str_sql = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str_sql += str_insert;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    str_sql += "'" + dt.Rows[i][j].ToString().Replace("'","''") + "'";
                    if (j != dt.Columns.Count - 1)
                    {
                        str_sql += ",";
                    }
                }
                str_sql += ")\n SET IDENTITY_INSERT news OFF ;\n";
            }

            Response.Write(str_sql);
        }
    }
}

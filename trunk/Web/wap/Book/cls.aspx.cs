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


namespace Web.wap.Book
{
    public partial class cls : Voodoo.Basement.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id", 1);
            int page = WS.RequestInt("page", 1);

            IDbHelper Sql = Voodoo.Setting.DataBase.GetHelper();

            string str_sql = string.Format("ClassID in(select id from Class where ParentID={0} union select {0})", id);

            AspNetPager1.RecordCount = Sql.PageCountSort("Book", str_sql, "");

            Sql = Voodoo.Setting.DataBase.GetHelper();
            rp.DataSource = Sql.PageListViewSort("Book", "ID", "id desc", page, AspNetPager1.PageSize, "*", str_sql, "");
            rp.DataBind();
        }
    }
}
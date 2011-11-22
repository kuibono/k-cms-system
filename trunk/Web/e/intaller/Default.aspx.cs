using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using Voodoo;
using Voodoo.Data;
using Voodoo.Data.DbHelper;

namespace Web.e.intaller
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //尝试连接数据库服务器
            string master_conn_str = string.Format("Data Source={0};Initial Catalog=master;Persist Security Info=True;User ID={1};Password={2}", txt_Server.Text, txt_Username.Text, txt_Password.Text);
            string ConnStr = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txt_Server.Text,txt_DbName.Text, txt_Username.Text, txt_Password.Text);
            SqlConnection master_conn = new SqlConnection(master_conn_str);
            try
            {
                master_conn.Open();
            }
            catch
            {
                Js.AlertAndGoback("数据库服务器连接失败，请重试！");
            }
            finally
            {
                if(master_conn.State==ConnectionState.Open)
                {
                    master_conn.Close();
                }
            }


            //判断数据库是否存在
            IDbHelper Helper = new SqlHelper(master_conn_str);
            DbDataReader dr = Helper.ExecuteReader(CommandType.Text, string.Format("select 0 From master.dbo.sysdatabases where name='{0}' ", txt_DbName.Text));
            if (!dr.Read())
            {
                //不存在的话创建
                dr.Close();
                dr.Dispose();
                Helper = new SqlHelper(master_conn_str);
                Helper.ExecuteNonQuery(CommandType.Text, string.Format("CREATE DATABASE {0}",txt_DbName.Text));

            }
            dr.Close();
            dr.Dispose();


            

            //导入表

            string str_sql = Voodoo.IO.File.Read(Server.MapPath("~/e/intaller/tablescript.txt"));
            Helper = new SqlHelper(ConnStr);
            Helper.ExecuteNonQuery(CommandType.Text, str_sql);

            //导入数据
            if (chk_WithData.Checked)
            {
                string str_news = Voodoo.IO.File.Read(Server.MapPath("~/e/intaller/news.txt"));
                Helper = new SqlHelper(ConnStr);
                Helper.ExecuteNonQuery(CommandType.Text, str_news);
            }


            //导入管理员

        }
    }
}

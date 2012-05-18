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

namespace Web.e.admin.user
{
    public partial class UserList : AdminBase
    {
        protected int enable = -1;
        protected int group = -1;
        protected string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WS.RequestString("action") == "del")
            {
                Button1_Click(sender,e);
            }
            if (!IsPostBack)
            {
                BindList();
            }
            
        }

        protected void BindList()
        {
            enable = WS.RequestInt("enable", -1);
            group = WS.RequestInt("group", -1);
            url = string.Format("UserList.aspx?ebable={0}&group={1}", enable.ToString(), group.ToString());


            ddl_Group.DataSource = UserAction.GetUserGroups();
            ddl_Group.DataTextField = "GroupName";
            ddl_Group.DataValueField = "ID";
            ddl_Group.DataBind();
            ddl_Group.Items.Add(new ListItem("--不限--",""));
            ddl_Group.SelectedValue="";

            if (group > -1)
            {
                ddl_Group.SelectedValue = group.ToS();
                ddl_Group.Enabled = false;
            }
            if (enable > -1)
            {
                ddl_Enabled.SelectedValue = enable.ToS();
                ddl_Enabled.Enabled = false;
            }

            pager.PageSize = SystemSetting.MagageListSize;


            string str_sql = "";
            if (!ddl_Group.SelectedValue.IsNullOrEmpty())
            {
                str_sql += "[Group]=" + ddl_Group.SelectedValue;
            }
            if (!ddl_Enabled.SelectedValue.IsNullOrEmpty())
            {
                if (!str_sql.Trim().IsNullOrEmpty())
                {
                    str_sql += " and ";
                }
                str_sql += "Enable=" + ddl_Enabled.SelectedValue;
            }

            ph p = new ph();
            p.CurrentPage = pager.CurrentPageIndex;
            p.Fields = "ID, UserName, UserPass, Email, ChineseName, QQ, MSN, Tel, Mobile, WebSite, Image, Address, ZipCode, Intro, RegTime, RegIP, LoginCount, LastLoginTime, LastLoginIP, Cent, PostCount, GTalk, Twitter, weibo, ICQ, [Group],enable";
            p.Filter = str_sql;
            p.group = "";
            p.PageSize = pager.PageSize;
            p.PrimaryKey = "ID";
            p.Sort = "ID desc";
            p.Tables = "[User]";

            pager.RecordCount = p.Count();

            rp_list.DataSource = p.GetTable();
            rp_list.DataBind();
        }

        protected string GetGroupNameByID(int id)
        {
            var gs=UserAction.GetUserGroups().Where(p => p.ID == id).ToList();
            if (gs.Count > 0)
            {
                return gs.First().GroupName;
            }
            else
            {
                return "";
            }
        }

        protected void btn_disable_Click(object sender, EventArgs e)
        {
            enable = WS.RequestInt("enable", -1);
            group = WS.RequestInt("group", -1);
            url = string.Format("UserList.aspx?ebable={0}&group={1}", enable.ToString(), group.ToString());

            string ids = WS.RequestString("id");
            if (ids.IsNullOrEmpty())
            {
                Js.AlertAndGoback("您没有选择任何项");
                return;
            }
            GetHelper().ExecuteNonQuery(CommandType.Text, string.Format("update [User] set Enable=0 where id in({0})", ids));
            Js.AlertAndChangUrl("设置成功！", url);
        }

        protected void btn_enable_Click(object sender, EventArgs e)
        {
            enable = WS.RequestInt("enable", -1);
            group = WS.RequestInt("group", -1);
            url = string.Format("UserList.aspx?ebable={0}&group={1}", enable.ToString(), group.ToString());

            string ids = WS.RequestString("id");
            if (ids.IsNullOrEmpty())
            {
                Js.AlertAndGoback("您没有选择任何项");
                return;
            }
            GetHelper().ExecuteNonQuery(CommandType.Text, string.Format("update [User] set Enable=1 where id in({0})", ids));
            Js.AlertAndChangUrl("设置成功！", url);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            enable = WS.RequestInt("enable", -1);
            group = WS.RequestInt("group", -1);
            url = string.Format("UserList.aspx?ebable={0}&group={1}", enable.ToString(), group.ToString());

            //删除
            string ids = WS.RequestString("id");
            if (ids.IsNullOrEmpty())
            {
                Js.AlertAndGoback("您没有选择任何项");
                return;
            }
            UserView.Del(string.Format("id in {0}",ids));
            Js.AlertAndChangUrl("删除成功！", url);
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            BindList();
        }
    }
}

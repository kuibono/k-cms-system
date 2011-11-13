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

namespace Web.e.admin.sysuser
{
    public partial class SysUserList : BasePage
    {
        #region  页面加载事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string ids=WS.RequestString("id");
            switch (WS.RequestString("action"))
            {
                case "disable":
                    Disable(ids);
                    break;
                case "enable":
                    Enable(ids);
                    break;
                case "del":
                    Delete(ids);
                    break;
            }

            LoadList();
        }
        #endregion

        #region 加载列表
        /// <summary>
        /// 加载列表
        /// </summary>
        protected void LoadList()
        {
            pager.PageSize = SystemSetting.MagageListSize;

            ph p = new ph()
            {
                Tables = "SysUser",
                PrimaryKey = "ID",
                Sort = "ID desc",
                CurrentPage = pager.CurrentPageIndex,
                PageSize = pager.PageSize,
                Fields = "ID, UserName, UserPass, Logincount, LastLoginTime, LastLoginIP, SafeQuestion, SafeAnswer, Department, ChineseName, UserGroup, Email, TelNumber, Enabled",
                Filter = "",
                group = ""
            };

            rp_list.DataSource = p.GetTable();
            rp_list.DataBind();
            pager.RecordCount = p.Count();
        }
        #endregion

        #region 停用
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="ids"></param>
        protected void Disable(string ids)
        {
            string str_sql = string.Format("update sysuser set Enabled=0 where id in({0})", ids);
            GetHelper().ExecuteNonQuery(CommandType.Text, str_sql);
            Js.AlertAndChangUrl("操作成功！", "SysUserList.aspx");
        }
        #endregion

        #region 启用
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="ids"></param>
        protected void Enable(string ids)
        {
            string str_sql = string.Format("update sysuser set Enabled=1 where id in({0})", ids);
            GetHelper().ExecuteNonQuery(CommandType.Text, str_sql);
            Js.AlertAndChangUrl("操作成功！", "SysUserList.aspx");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        protected void Delete(string ids)
        {
            SysUserView.Del(string.Format("id in({0})", ids));
            Js.AlertAndChangUrl("删除成功！", "SysUserList.aspx");
        }
        #endregion

        #region 按钮事件
        protected void btn_disable_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            Disable(ids);
        }

        protected void btn_enable_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            Enable(ids);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ids = WS.RequestString("id");
            Delete(ids);
        }
        #endregion
    }
}

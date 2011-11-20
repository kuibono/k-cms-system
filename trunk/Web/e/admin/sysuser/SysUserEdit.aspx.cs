﻿using System;
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

namespace Web.e.admin.sysuser
{
    public partial class SysUserEdit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }

        #region 加载信息
        /// <summary>
        /// 加载信息
        /// </summary>
        protected void LoadInfo()
        {
            ddl_Group.DataSource = GetSysGroup();
            ddl_Group.DataTextField = "GroupName";
            ddl_Group.DataValueField = "ID";
            ddl_Group.DataBind();

            ddl_Department.DataSource = GetSysDepartment();
            ddl_Department.DataTextField = "DepartName";
            ddl_Department.DataValueField = "ID";
            ddl_Department.DataBind();

            if (WS.RequestInt("id") > 0)
            {
                txt_Password.EnableNull = true;
                txt_UserName.Enabled = false;

                SysUser user = SysUserView.GetModelByID(WS.RequestInt("id").ToString());
                txt_UserName.Text = user.UserName;
                txt_Password.Text = user.UserPass;
                ddl_Question.SelectedValue = user.SafeQuestion;
                txt_Answer.Text = user.SafeAnswer;
                txt_ChineseName.Text = user.ChineseName;
                txt_Email.Text = user.Email;
                txt_TelNumber.Text = user.TelNumber;
                ddl_Department.SelectedValue = user.Department.ToString();
                ddl_Group.SelectedValue = user.UserGroup.ToString();
                lb_LastLoginTime.Text = user.LastLoginTime.ToString();
                lb_LastLoginIP.Text = user.LastLoginIP;
                lb_LoginCount.Text = user.Logincount.ToString();
                chk_Enable.Checked = user.Enabled;
            }
        }
        #endregion

        #region 保存资料
        /// <summary>
        /// 保存资料
        /// </summary>
        protected void SaveInfo()
        {
            SysUser user = new SysUser();
            if (WS.RequestInt("id") > 0)
            {

                user = SysUserView.GetModelByID(WS.RequestInt("id").ToString());
            }
            else if (txt_Password.Text.Length == 0)
            {
                Js.AlertAndGoback("新增用户时，密码不能为空");
            }

            user.UserName = txt_UserName.Text;
            if (txt_Password.Text.Length > 0)
            {
                user.UserPass = Voodoo.Security.Encrypt.Md5(txt_Password.Text);
            }

            user.SafeQuestion = ddl_Question.SelectedValue;
            user.SafeAnswer = txt_Answer.Text;
            user.Email = txt_Email.Text;
            user.TelNumber = txt_TelNumber.Text;
            user.Department = ddl_Department.SelectedValue.ToInt32();
            user.UserGroup = ddl_Group.SelectedValue.ToInt32();
            user.ChineseName = txt_ChineseName.Text;
            user.Enabled = chk_Enable.Checked;

            if (WS.RequestInt("id") > 0)
            {
                //修改
                SysUserView.Update(user);
                Js.AlertAndChangUrl("修改成功！", "SysUserList.aspx");
            }
            else
            {
                user.LastLoginTime = DateTime.Now;
                user.LastLoginIP = WS.GetIP();
                Result r = SysUserAction.UserAdd(user);

                if (r.Success)
                {
                    Js.AlertAndChangUrl(r.Text, "SysUserList.aspx");
                }
                else
                {
                    Js.AlertAndGoback(r.Text);
                }
            }

        }
        #endregion

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }
    }
}

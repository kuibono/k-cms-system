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

namespace Web.e.admin.system.SystemParameter
{
    public partial class basic : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        protected void LoadInfo()
        {
            SysSetting st = SystemSetting;
            txt_SiteName.Text = st.SiteName;
            txt_SiteUrl.Text = st.SiteUrl;
            chk_SiteOpen.Checked = st.SiteOpen;
            txt_SiteCloseMsg.Text = st.SiteCloseMsg;
            txt_KeyWords.Text = st.KeyWords;
            txt_Description.Text = st.Description;
            txt_Copyright.Text = st.Copyright;
            txt_CountCode.Text = st.CountCode;
            txt_FileDirString.Text = st.FileDirString;
            txt_ExtName.Text = st.ExtName;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SysSetting st = SystemSetting;
            st.SiteName = txt_SiteName.Text;
            st.SiteUrl = txt_SiteUrl.Text;
            st.SiteOpen = chk_SiteOpen.Checked;
            st.SiteCloseMsg = txt_SiteCloseMsg.Text;
            st.KeyWords = txt_KeyWords.Text;
            st.Description = txt_Description.Text;
            st.Copyright = txt_Copyright.Text;
            st.CountCode = txt_CountCode.Text;
            st.FileDirString = txt_FileDirString.Text;
            st.ExtName = txt_ExtName.Text;

            Voodoo.Basement.Setting.SysSettingDAL.SetSetting(st);
            Js.Alert("保存成功！");
        }
    }
}

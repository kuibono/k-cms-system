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

namespace Web.e.admin.template
{
    public partial class ListTemplateEdit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }

        protected void LoadInfo()
        {
            int id = WS.RequestInt("id");
            TemplateList tl = TemplateListView.GetModelByID(id.ToS());
            txt_TempName.Text = tl.TempName;
            txt_CutKeywords.Text = tl.CutKeywords.ToS();
            txt_CutTitle.Text = tl.CutTitle.ToS();
            txt_ShowRecordCount.Text = tl.ShowRecordCount.ToS();
            txt_TimeFormat.Text = tl.TimeFormat;
            txt_Content.Text = tl.Content;
            txt_Listvar.Text = tl.ListVar;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            TemplateList tl = TemplateListView.GetModelByID(id.ToS());

            tl.TempName = txt_TempName.Text;
            tl.CutKeywords = txt_CutKeywords.Text.ToInt32();
            tl.CutTitle = txt_CutTitle.Text.ToInt32();
            tl.ShowRecordCount = txt_ShowRecordCount.Text.ToInt32();
            tl.TimeFormat = txt_TimeFormat.Text;
            tl.Content = txt_Content.Text.Replace("'", "''"); ;
            tl.ListVar = txt_Listvar.Text.Replace("'", "''"); ;

            if (tl.ID > 0)
            {
                TemplateListView.Update(tl);
            }
            else
            {
                tl.GroupID = 1;
                tl.SysModel = 1;
                TemplateListView.Insert(tl);
            }
            Js.AlertAndGoback("保存成功！");
        }
    }
}

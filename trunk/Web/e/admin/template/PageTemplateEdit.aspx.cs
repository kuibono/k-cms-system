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
    public partial class PageTemplateEdit : AdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            if (!IsPostBack)
            {
                LoadInfo(id);
            }
        }

        protected void LoadInfo(int id)
        {
            TemplatePage tp = TemplatePageView.GetModelByID(id.ToS());
            chk_Enable.Checked = tp.Enable;
            txt_pageName.Text = tp.PageName;
            txt_FileName.Text = tp.FileName;
            ddl_CreateWith.SelectedValue = tp.CreateWith.ToS();
            txt_Content.Text = tp.Content;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            TemplatePage tp = TemplatePageView.GetModelByID(WS.RequestString("id"));
            tp.Content = txt_Content.Text.ToSqlEnCode();
            tp.CreateWith = ddl_CreateWith.SelectedValue.ToInt32();
            tp.FileName = txt_FileName.Text;
            tp.PageName = txt_pageName.Text;

            if (tp.Id > 0)
            {
                TemplatePageView.Update(tp);
            }
            else
            {
                TemplatePageView.Insert(tp);
            }

            CreatePage.CreatePages(tp);

            Js.AlertAndChangUrl("保存成功！", "PageTemplateList.aspx");
        }
    }
}
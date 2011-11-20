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


namespace Web.e.admin.system.friendlink
{
    public partial class LinkEdit : AdminBase
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
            Link l = LinkView.GetModelByID(id.ToS());
            txt_Index.Text = l.Index.ToS();
            txt_LinkTitle.Text = l.LinkTitle;
            txt_Url.Text = l.Url;

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            Link l = LinkView.GetModelByID(id.ToS());

            l.Index = txt_Index.Text.ToInt32();
            l.LinkTitle = txt_LinkTitle.Text.TrimDbDangerousChar();
            l.Url = txt_Url.Text.TrimDbDangerousChar();


            if (l.ID > 0)
            {
                LinkView.Update(l);
            }
            else
            {
                LinkView.Insert(l);
            }
            CreatePage.GreateIndexPage();

            Js.AlertAndChangUrl("保存成功！","LinkList.aspx");
        }
    }
}

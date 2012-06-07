using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Voodoo;
using Voodoo.Data;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;
using Voodoo.Setting;

namespace Web.e.admin.template
{
    public partial class PageTemplateList : AdminBase
    {
        protected string[] CREATE_WITH = "不生成,首页,列表,内容,章节/播放/图片".Split(',');
        protected void Page_Load(object sender, EventArgs e)
        {
            if (WS.RequestString("action") == "del")
            {
                btn_Del_Click(sender, e);
            }
            if (!IsPostBack)
            {
                BindList();
            }
        }

        protected void BindList()
        {
            rp_list.DataSource = TemplatePageView.GetModelList();
            rp_list.DataBind();
        }

        protected void btn_Del_Click(object sender, EventArgs e)
        {
            

            string ids = WS.RequestString("id");

            var pages = TemplatePageView.GetModelList(string.Format("id in({0})", ids));
            foreach (var page in pages)
            {
                FileInfo f = new FileInfo(Server.MapPath(page.FileName));
                if (f.Exists)
                {
                    try
                    {
                        f.Delete();
                    }
                    catch { }
                }
            }

            TemplatePageView.Del(string.Format("id in ({0})", ids));
            BindList();
        }
    }
}
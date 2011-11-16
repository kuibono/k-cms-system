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

namespace Web.e.file
{
    public partial class addselect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataList1.DataSource = FileView.GetModelList();
            DataList1.DataBind();
        }

        protected void btn_UpLoad_Click(object sender, EventArgs e)
        {
            HttpPostedFile file=Request.Files["FileUpload1"];
            
            string FileName = file.FileName;
            int FileSize = file.ContentLength / 1024;
            

        }
    }
}

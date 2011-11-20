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
using Voodoo.IO;
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
            if(FileUpload1.FileName.IsNullOrEmpty())
            {
                return;
            }
            Result r= BasePage.UpLoadFile(Request.Files["FileUpload1"], WS.RequestInt("class", 0));
            if (r.Success)
            {
                Js.Jump("?");
            }
            else
            {
                Js.AlertAndGoback(r.Text);
            }

            //SysSetting ss = BasePage.SystemSetting;

            //HttpPostedFile file=Request.Files["FileUpload1"];
            //string FileName=file.FileName.GetFileNameFromPath();//文件名
            //string ExtName=file.FileName.GetFileExtNameFromPath();//扩展名
            //string NewName=@string.GetGuid()+ExtName;//新文件名

            //if(!ExtName.Replace(".","").IsInArray(ss.FileExtNameFilter.Split(',')))
            //{
            //    Js.AlertAndGoback("不允许上传此类文件");
            //    return;
            //}
            //if (file.ContentLength>ss.MaxPostFileSize)
            //{
            //    Js.AlertAndGoback("文件太大");
            //    return;
            //}

            //string Folder=ss.FileDir+"/"+DateTime.Now.ToString("yyyy-MM-dd")+"/";//文件目录
            //string FolderShotCut=Folder+"ShortCut/";//缩略图目录

            //string FilePath=Folder+NewName;//文件路径
            //string FilePath_ShortCut=FolderShotCut+NewName;//缩略图路径

            //file.SaveAs(Server.MapPath(FilePath),true);
            //ImageHelper.MakeThumbnail(Server.MapPath(FilePath),Server.MapPath(FilePath_ShortCut),105,118,"Cut");



            //FileInfo savedFile = new FileInfo(Server.MapPath(FilePath));

            //Voodoo.Model.File f = new Voodoo.Model.File();
            
            //f.FileDirectory = ss.FileDir;
            //f.FileExtName = ExtName;
            //f.FilePath = FilePath;
            //f.FileSize = (savedFile.Length / 1024).ToInt32();
            ////f.FileType=
            //f.SmallPath = FilePath_ShortCut;
            //f.UpTime = DateTime.Now;

            //FileView.Insert(f);
            
            
        }
    }
}

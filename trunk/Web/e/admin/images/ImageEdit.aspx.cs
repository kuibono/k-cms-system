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

namespace Web.e.admin.images
{
    public partial class ImageEdit : AdminBase
    {
        protected int cls = WS.RequestInt("class", 0);
        protected int zt = WS.RequestInt("zt", 0);
        protected int id = WS.RequestInt("id");
        protected string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            url = string.Format("ImageList.aspx?class={0}&zt={1}", cls, zt);

            if (WS.RequestString("action") == "del")
            {
                DeleteImage(WS.RequestInt("imgid"));
            }

            if (!IsPostBack)
            {

                LoadInfo();
            }
        }

        protected void DeleteImage(int imageID)
        {
            Images img = ImagesView.GetModelByID(imageID.ToS());
            Voodoo.IO.File.Delete(Server.MapPath(img.FilePath));
            Voodoo.IO.File.Delete(Server.MapPath(img.SmallPath));

            ImagesView.DelByID(imageID);

            rp_list.DataSource = ImagesView.GetModelList(string.Format("AlbumID={0} order by id", WS.RequestString("id")));
            rp_list.DataBind();
        }

        #region 加载信息
        /// <summary>
        /// 加载信息
        /// </summary>
        protected void LoadInfo()
        {


            ddl_Class.DataSource = NewsAction.NewsClass.Where(p => p.IsLeafClass && p.ModelID == 2);
            ddl_Class.DataTextField = "ClassName";
            ddl_Class.DataValueField = "ID";
            ddl_Class.DataBind();

            ddl_Class.SelectedValue = cls.ToS();

            ddl_Author.DataSource = SysUserView.GetModelList();
            ddl_Author.DataTextField = "UserName";
            ddl_Author.DataValueField = "ID";
            ddl_Author.DataBind();

            ImageAlbum imga = ImageAlbumView.GetModelByID(id.ToS());

            //txt_Title.Text = imga.Title;
            //ddl_Author.SelectedValue = imga.AuthorID.ToS();
            //txt_Folder.Text = imga.Folder;
            //txt_Keyword.Text=imga.

            if (id > 0)
            {

                ddl_Class.SelectedValue = imga.ClassID.ToS();
                txt_Title.Text = imga.Title;
                ddl_Author.Text = imga.AuthorID.ToS();
                //txt_Folder.Text = imga.Folder;
                txt_Keyword.Text = imga.KeyWords;
                txt_ClickCount.Text = imga.ClickCount.ToS();
                txt_ReplyCount.Text = imga.ClickCount.ToS();
                txt_Intro.Text = imga.Intro;

                rp_list.DataSource = ImagesView.GetModelList(string.Format("AlbumID={0} order by id", imga.ID));
                rp_list.DataBind();
            }
            else
            {
                ImageAlbumView.Del("AuthorID=0 and CreateTime<'"+ DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") +"'");
                imga = new ImageAlbum() { ClassID = cls, ClickCount = 0, ReplyCount = 0, ZtID = 0, CreateTime = DateTime.Now, AuthorID=0 , UpdateTime=DateTime.Now};
                ImageAlbumView.Insert(imga);
                Response.Redirect(string.Format("ImageEdit.aspx?class={0}&id={1}", cls, imga.ID));
                
            }


        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Class cls=ClassView.GetModelByID(ddl_Class.SelectedValue);

            ImageAlbum imga = ImageAlbumView.GetModelByID(WS.RequestString("id"));
            imga.ClassID = ddl_Class.SelectedValue.ToInt32();
            imga.Title = txt_Title.Text.TrimDbDangerousChar();
            imga.AuthorID = ddl_Author.SelectedValue.ToInt32();
            imga.Author = ddl_Author.SelectedItem.Text;
            imga.ClickCount = txt_ClickCount.Text.ToInt32(0);
            if (imga.ID <= 0)
            {
                imga.CreateTime = DateTime.Now;
                imga.Folder = DateTime.Now.ToString("yyyy-MM-dd");
            }
            imga.Intro = txt_Intro.Text.TrimDbDangerousChar();
            imga.KeyWords = txt_Keyword.Text.TrimDbDangerousChar();
            imga.ReplyCount = txt_ReplyCount.Text.ToInt32(0);
            imga.SetTop = false;
            //imga.ShotCut = "";
            //imga.Size = 0;
            imga.Title = txt_Title.Text;
            imga.UpdateTime = DateTime.Now;
            imga.ZtID = 0;

            if (imga.ID > 0)
            {
                ImageAlbumView.Update(imga);
            }
            else
            {
                ImageAlbumView.Insert(imga);
            }

            //保存单个图片设置
            string[] tits = WS.RequestString("tit[]").Split(',');
            string[] intrs = WS.RequestString("intro[]").Split(',');

            var imgs = ImagesView.GetModelList(string.Format("AlbumID={0} order by id", imga.ID));
            for (int i = 0; i < imgs.Count; i++)
            {
                try
                {
                    imgs[i].Title = tits[i].ToS();
                    imgs[i].Intro = intrs[i].ToS();
                    ImagesView.Update(imgs[i]);
                }
                catch { }

            }

            //生成页面

            CreatePage.CreateContentPage(imga, cls);

            ImageAlbum pre = GetPreImage(imga, cls);
            if (pre != null)
            {
                CreatePage.CreateContentPage(pre, cls);
            }
            CreatePage.CreateListPage(cls, 1);


            Js.AlertAndChangUrl("保存成功！", url);

        }
        #endregion

        #region 刷新页面
        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Refresh_Click(object sender, EventArgs e)
        {
            rp_list.DataSource = ImagesView.GetModelList(string.Format("AlbumID={0} order by id", WS.RequestString("id")));
            rp_list.DataBind();
        }
        #endregion
    }
}

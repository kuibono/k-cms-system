using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using Voodoo;
using Voodoo.Data;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Basement;
using Voodoo.Setting;

namespace Web.e.admin.news
{
    public partial class NewsEdit : AdminBase
    {
        protected int cls = WS.RequestInt("class", 0);
        protected int zt = WS.RequestInt("zt", 0);
        protected string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            url = string.Format("NewsList.aspx?class={0}&zt={1}", cls, zt);
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
            cp_TitleColor.Value = "000";
            sp_cor.Style["background-color"] = "#"+cp_TitleColor.Value;

            ddl_Class.DataSource = NewsAction.NewsClass;
            ddl_Class.DataTextField = "ClassName";
            ddl_Class.DataValueField = "ID";
            ddl_Class.DataBind();

            ddl_Class.SelectedValue = cls.ToS();

            ddl_contentTemp.DataSource = TemplateContentView.GetModelList();
            ddl_contentTemp.DataTextField = "TempName";
            ddl_contentTemp.DataValueField = "ID";
            ddl_contentTemp.DataBind();
            ddl_contentTemp.Items.Add(new ListItem("--使用默认模板--", "0"));
            ddl_contentTemp.SelectedValue = "0";

            txt_FileForder.Text = DateTime.Now.ToString("yyyy-MM-dd");

            txt_Author.Text = SysUserAction.LocalUser.UserName;

            if (WS.RequestInt("id") <= 0)
            {
                txt_NewsTime.Text = DateTime.Now.ToString();
                return;
            }

            News n = NewsView.GetModelByID(WS.RequestString("id"));

            ddl_Class.SelectedValue = n.ClassID.ToS();

            txt_Title.Text = n.Title;
            cp_TitleColor.Value = n.TitleColor;
            chk_TitleB.Checked = n.TitleB;
            chk_TitleI.Checked = n.TitleI;
            chk_TitleS.Checked = n.TitleS;

            txt_FTitle.Text = n.FTitle;

            chk_Audit.Checked = n.Audit;
            chk_Tuijian.Checked = n.Tuijian;
            chk_Toutiao.Checked = n.Toutiao;

            txt_Key.Text = n.KeyWords;
            txt_NavUrl.Text = n.NavUrl;
            txt_NewsTime.Text = n.NewsTime.ToString();
            txt_TitleImage.Text = n.TitleImage;
            txt_Description.Text = n.Description;
            txt_Author.Text = n.Author;
            txt_Source.Text = n.Source;
            FCKeditor1.Value = n.Content;

            chk_SetTop.Checked = n.SetTop;
            chk_CloseReply.Checked = !n.EnableReply;
            ddl_contentTemp.SelectedValue = n.ModelID.ToString();
            txt_ClickCount.Text = n.ClickCount.ToString();
            txt_DownCount.Text = n.DownCount.ToString();
            txt_FileForder.Text = n.FileForder;
            txt_FileName.Text = n.FileName;
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
            cls = WS.RequestInt("class", 0);
            zt = WS.RequestInt("zt", 0);
            url = string.Format("NewsList.aspx?class={0}&zt={1}", cls, zt);

            //int zt = WS.RequestInt("zt",0);

            News n = new News();

            if (WS.RequestInt("id") > 0)
            {
                n = NewsView.GetModelByID(WS.RequestString("id"));
                cls = n.ClassID;
            }
            else
            {
                n.ClassID = cls;
                n.ZtID = zt;
            }

            Class c = NewsAction.NewsClass.Where(p => p.ID.ToString() == ddl_Class.SelectedValue).First();

            //ddl_Class.SelectedValue = c.ID.ToS();

            //if (cls < 0 && zt < 0)
            //{
            //    Js.AlertAndGoback("栏目或专题获取失败!");
            //    return;
            //}

            n.ClassID = ddl_Class.SelectedValue.ToInt32();
            n.Title = txt_Title.Text.TrimDbDangerousChar();
            n.TitleColor = cp_TitleColor.Value.TrimDbDangerousChar();
            n.TitleB = chk_TitleB.Checked;
            n.TitleI = chk_TitleI.Checked;
            n.TitleS = chk_TitleS.Checked;

            n.FTitle = txt_FTitle.Text.TrimDbDangerousChar();

            n.Audit = chk_Audit.Checked;
            n.Tuijian = chk_Tuijian.Checked;
            n.Toutiao = chk_Toutiao.Checked;

            n.KeyWords = txt_Key.Text.TrimDbDangerousChar();
            //关键词写入系统
            txt_Key.Text = Regex.Replace(txt_Key.Text, "\\s", ",");
            string[] keys = Regex.Replace(txt_Key.Text, "\\s", ",").Split(',');
            foreach (string k in keys)
            {
                InsertKeyWords(1, k);
            }

            n.NavUrl = txt_NavUrl.Text.TrimDbDangerousChar();
            n.NewsTime=txt_NewsTime.Text.ToDateTime();
            n.TitleImage = txt_TitleImage.Text.TrimDbDangerousChar();
            n.Description = txt_Description.Text.TrimDbDangerousChar();
            n.Author = txt_Author.Text.TrimDbDangerousChar();
            n.Source = txt_Source.Text.TrimDbDangerousChar();
            n.Content = FCKeditor1.Value.TrimDbDangerousChar();

            n.SetTop = chk_SetTop.Checked;
            n.EnableReply = !chk_CloseReply.Checked;
            n.ModelID = ddl_contentTemp.SelectedValue.ToInt32();
            n.ClickCount = txt_ClickCount.Text.ToInt32();
            n.DownCount = txt_DownCount.Text.ToInt32();
            n.FileForder = txt_FileForder.Text;
            n.FileName = txt_FileName.Text;
            n.ReplyCount = 0;

            if (WS.RequestInt("id") > 0)
            {
                NewsView.Update(n);
            }
            else
            {
                NewsView.Insert(n);
            }



            //此处进行代码生成

            //nv.Add("不生成", "0");
            //nv.Add("生成当前栏目", "1");
            //nv.Add("生成首页", "2");
            //nv.Add("生成父栏目", "3");
            //nv.Add("生成当前栏目与父栏目", "4");
            //nv.Add("生成父栏目与首页", "5");
            //nv.Add("生成当前栏目、父栏目与首页", "6");

            

            CreatePage.CreateContentPage(n, c);

            News news_pre = GetPreNews(n,c);

            if (news_pre != null)
            {
                CreatePage.CreateContentPage(news_pre, c);
            }
            switch (c.EditcreateList)
            {
                case 0:
                    break;
                case 1:
                    CreatePage.CreateListPage(c, 1);
                    break;
                case 2:
                    CreatePage.GreateIndexPage();
                    break;
                case 3:
                case 4:
                    CreatePage.CreateListPage(c, 1);
                    break;
                case 5:
                    CreatePage.GreateIndexPage();
                    break;
                case 6:
                    CreatePage.CreateListPage(c, 1);
                    CreatePage.GreateIndexPage();
                    break;
            }
            


            Js.AlertAndChangUrl("保存成功！",url);

        }
        #endregion

        #region 获取当前时间
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_setNow_Click(object sender, EventArgs e)
        {
            txt_NewsTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion 
    }
}

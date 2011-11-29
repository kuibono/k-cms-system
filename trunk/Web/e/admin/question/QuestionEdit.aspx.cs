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


namespace Web.e.admin.question
{
    public partial class QuestionEdit : AdminBase
    {
        protected int cls = WS.RequestInt("class", 0);
        protected int zt = WS.RequestInt("zt", 0);
        protected int id = WS.RequestInt("id");
        protected string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            url = string.Format("QuestionList.aspx?class={0}&zt={1}", cls, zt);

            if (WS.RequestString("action") == "del")
            {
                DeleteAnswer(WS.RequestInt("anid"));
            }

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


            ddl_Class.DataSource = NewsAction.NewsClass.Where(p=>p.IsLeafClass&&p.ModelID==3);
            ddl_Class.DataTextField = "ClassName";
            ddl_Class.DataValueField = "ID";
            ddl_Class.DataBind();

            ddl_Class.SelectedValue = cls.ToS();

            ddl_Author.DataSource = UserView.GetModelList();
            ddl_Author.DataTextField = "UserName";
            ddl_Author.DataValueField = "ID";
            ddl_Author.DataBind();

            Question qu = QuestionView.GetModelByID(id.ToS());


            if (id > 0)
            {

                ddl_Class.SelectedValue = qu.ClassID.ToS();
                txt_Title.Text = qu.Title;
                //ddl_Author.Text = qu.UserID.ToS();
                ddl_Author.SelectedValue = qu.UserID.ToS();
                txt_ClickCount.Text = qu.ClickCount.ToS();
                txt_Content.Text = qu.Content;

                rp_list.DataSource = AnswerView.GetModelList(string.Format("QuestionID={0} order by id", qu.ID));
                rp_list.DataBind();
            }


        }
        #endregion

        protected void DeleteAnswer(int id)
        {
            AnswerView.DelByID(id);

            Class cls = ClassView.GetModelByID(WS.RequestString("class"));
            Question qu = QuestionView.GetModelByID(WS.RequestString("id"));
            if (cls.ID > 0 && qu.ID > 0)
            {
                CreatePage.CreateContentPage(qu, cls);
            }
            rp_list.DataSource = AnswerView.GetModelList(string.Format("QuestionID={0} order by id", WS.RequestString("id")));
            rp_list.DataBind();
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Class cls = ClassView.GetModelByID(ddl_Class.SelectedValue);

            Question qu = QuestionView.GetModelByID(WS.RequestString("id"));
            qu.ClassID = ddl_Class.SelectedValue.ToInt32();
            qu.Title = txt_Title.Text.TrimDbDangerousChar();
            qu.UserID = ddl_Author.SelectedValue.ToInt32();
            qu.UserName = ddl_Author.SelectedItem.Text;
            qu.ClickCount = txt_ClickCount.Text.ToInt32(0);
            if (qu.ID <= 0)
            {
                qu.AskTime = DateTime.Now;
            }
            qu.Content = txt_Content.Text.TrimDbDangerousChar();

            qu.Title = txt_Title.Text;
            qu.ZtID = 0;

            if (qu.ID > 0)
            {
                QuestionView.Update(qu);
            }
            else
            {
                QuestionView.Insert(qu);
            }



            //生成页面

            CreatePage.CreateContentPage(qu, cls);

            Question pre = GetPreQuestion(qu, cls);
            if (pre != null)
            {
                CreatePage.CreateContentPage(pre, cls);
            }
            CreatePage.CreateListPage(cls, 1);


            Js.AlertAndChangUrl("保存成功！", url);

        }
        #endregion
    }
}

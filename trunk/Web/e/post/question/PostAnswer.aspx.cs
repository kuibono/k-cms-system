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
using System.IO;
using Voodoo.IO;

namespace Web.e.post.question
{
    public partial class PostAnswer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User u = UserAction.opuser;
            if (u.ID <= 0)
            {
                Js.AlertAndGoback("对不起，您没有登录，请登录后回答！");
                return;
            }

            int qid = WS.RequestInt("qid");
            string content = WS.RequestString("content");
            if (qid <= 0)
            {
                Js.AlertAndGoback("对不起，参数错误，如有疑问，请联系管理员！");
                return;
            }
            Answer a = new Answer();
            a.Agree = 0;
            a.AnswerTime = DateTime.Now;
            a.Content = content;
            a.QuestionID = qid;
            a.UserID = u.ID;
            a.UserName = u.UserName;

            AnswerView.Insert(a);


            Question q = QuestionView.GetModelByID(a.QuestionID.ToS());

            CreatePage.CreateContentPage(q, q.GetClass());//创建内容页



            string url = BasePage.GetQuestionUrl(q, q.GetClass());

            Js.AlertAndChangUrl("回答成功！", url);
        }
    }
}

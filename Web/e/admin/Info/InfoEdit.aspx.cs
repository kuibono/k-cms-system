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

using Voodoo.IO;
namespace Web.e.admin.Info
{
    public partial class InfoEdit : AdminBase
    {

        protected int TypeID = WS.RequestInt("type");
        protected string TypeHtml = "";

        protected string ValueString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (WS.RequestString("submit").Length>0)
            {
                //保存数据
                SaveData();
            }
            else
            {
                LoadInfo();
            }
        }

        #region  保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        protected void SaveData()
        {
            int id = WS.RequestInt("id");
            var i = InfoView.GetModelByID(id.ToS());

            i.Address = WS.RequestString("address");
            i.Bit1 = WS.RequestString("bit1").ToBoolean();
            i.Bit2 = WS.RequestString("bit2").ToBoolean();
            i.Bit3 = WS.RequestString("bit3").ToBoolean();
            i.Bit4 = WS.RequestString("bit4").ToBoolean();
            i.Bit5 = WS.RequestString("bit5").ToBoolean();

            var cls = NewsAction.NewsClass.Where(p => p.ID == WS.RequestInt("class")).First();
            i.ClassID = cls.ID;
            i.ClassName = cls.ClassName;
            i.ClickCount = WS.RequestInt("clickcount",0);
            i.Contact = WS.RequestString("contact");
            i.ContactType = WS.RequestString("contacttype", "个人");

            i.Decimal1 = WS.RequestString("decimal1").ToDecimal(0);
            i.Decimal2 = WS.RequestString("decimal2").ToDecimal(0);
            i.Decimal3 = WS.RequestString("decimal3").ToDecimal(0);
            i.Decimal4 = WS.RequestString("decimal4").ToDecimal(0);
            i.Decimal5 = WS.RequestString("decimal5").ToDecimal(0);

            i.ImageCount = WS.RequestInt("imagecount",0);
            i.InfoTypeID = TypeID.ToS();
            i.Intro = WS.RequestString("intro");
            i.IsSetTop = WS.RequestString("issettop").ToBoolean();

            i.Num1 = WS.RequestInt("num1",0);
            i.Num2 = WS.RequestInt("num2", 0);
            i.Num3 = WS.RequestInt("num3", 0);
            i.Num4 = WS.RequestInt("num4", 0);
            i.Num5 = WS.RequestInt("num5", 0);
            i.Num6 = WS.RequestInt("num6", 0);
            i.Num7 = WS.RequestInt("num7", 0);
            i.Num8 = WS.RequestInt("num8", 0);
            i.Num9 = WS.RequestInt("num9", 0);
            i.Num10 = WS.RequestInt("num10", 0);

            i.Nvarchar1 = WS.RequestString("nvarchar1");
            i.Nvarchar2 = WS.RequestString("nvarchar2");
            i.Nvarchar3 = WS.RequestString("nvarchar3");
            i.Nvarchar4 = WS.RequestString("nvarchar4");
            i.Nvarchar5 = WS.RequestString("nvarchar5");
            i.Nvarchar6 = WS.RequestString("nvarchar6");
            i.Nvarchar7 = WS.RequestString("nvarchar7");
            i.Nvarchar8 = WS.RequestString("nvarchar8");
            i.Nvarchar9 = WS.RequestString("nvarchar9");
            i.Nvarchar10 = WS.RequestString("nvarchar10");
            i.Nvarchar11 = WS.RequestString("nvarchar11");
            i.Nvarchar12 = WS.RequestString("nvarchar12");
            i.Nvarchar13 = WS.RequestString("nvarchar13");
            i.Nvarchar14 = WS.RequestString("nvarchar14");
            i.Nvarchar15 = WS.RequestString("nvarchar15");

            i.OutTime = WS.RequestString("outtime").ToDateTime();
            i.PostTime = WS.RequestString("posttime").ToDateTime();
            i.ReplyCount = WS.RequestInt("replycount",0);
            i.Tel = WS.RequestString("tel");
            i.TelLocation = WS.RequestString("tellocation");

            i.Text1 = WS.RequestString("text1");
            i.Text2 = WS.RequestString("text2");
            i.Text3 = WS.RequestString("text3");
            i.Text4 = WS.RequestString("text4");
            i.Text5 = WS.RequestString("text5");

            i.Title = WS.RequestString("title");
            i.UserID = SysUserAction.LocalUser.ID;
            i.UserName = SysUserAction.LocalUser.UserName;
            i.ZtID = 0;
            i.ZtName = "";

            if (i.Id > 0)
            {
                InfoView.Update(i);
            }
            else
            {
                InfoView.Insert(i);
            }

            Response.Redirect("InfoList.aspx?type=" + TypeID);

        }
        #endregion 

        #region 加载数据
        /// <summary>
        /// 加载数据
        /// </summary>
        protected void LoadInfo()
        {
            InfoType it = InfoTypeView.GetModelByID(TypeID.ToS());
            TypeHtml = it.TemlateAdminForm;

            int id = WS.RequestInt("id");
            var i = InfoView.GetModelByID(id.ToS());
            ValueString = i.ToJson();

            TypeHtml = TypeHtml.Replace("{address}", i.Address);
            TypeHtml = TypeHtml.Replace("{bit1}", i.Bit1.ToS());
            TypeHtml = TypeHtml.Replace("{address}", i.Bit2.ToS());
            TypeHtml = TypeHtml.Replace("{address}", i.Bit3.ToS());
            TypeHtml = TypeHtml.Replace("{address}", i.Bit4.ToS());
            TypeHtml = TypeHtml.Replace("{address}", i.Bit5.ToS());
            TypeHtml = TypeHtml.Replace("{classid}", i.ClassID.ToS());
            TypeHtml = TypeHtml.Replace("{classname}", i.ClassName);
            TypeHtml = TypeHtml.Replace("{clickcount}", i.ClickCount.ToS());
            TypeHtml = TypeHtml.Replace("{contact}", i.Contact);
            TypeHtml = TypeHtml.Replace("{contacttype}", i.ContactType);
            TypeHtml = TypeHtml.Replace("{decimal1}", i.Decimal1.ToS());
            TypeHtml = TypeHtml.Replace("{decimal2}", i.Decimal2.ToS());
            TypeHtml = TypeHtml.Replace("{decimal3}", i.Decimal3.ToS());
            TypeHtml = TypeHtml.Replace("{decimal4}", i.Decimal4.ToS());
            TypeHtml = TypeHtml.Replace("{decimal5}", i.Decimal5.ToS());

            TypeHtml = TypeHtml.Replace("{id}", i.Id.ToS());
            TypeHtml = TypeHtml.Replace("{imagecount}", i.ImageCount.ToS());
            TypeHtml = TypeHtml.Replace("{infotypeid}", i.InfoTypeID.ToS());
            TypeHtml = TypeHtml.Replace("{intro}", i.Intro);
            TypeHtml = TypeHtml.Replace("{issettop}", i.IsSetTop.ToS());
            TypeHtml = TypeHtml.Replace("{num1}", i.Num1.ToS());
            TypeHtml = TypeHtml.Replace("{num2}", i.Num2.ToS());
            TypeHtml = TypeHtml.Replace("{num3}", i.Num3.ToS());
            TypeHtml = TypeHtml.Replace("{num4}", i.Num4.ToS());
            TypeHtml = TypeHtml.Replace("{num5}", i.Num5.ToS());
            TypeHtml = TypeHtml.Replace("{num6}", i.Num6.ToS());
            TypeHtml = TypeHtml.Replace("{num7}", i.Num7.ToS());
            TypeHtml = TypeHtml.Replace("{num8}", i.Num8.ToS());
            TypeHtml = TypeHtml.Replace("{num9}", i.Num9.ToS());
            TypeHtml = TypeHtml.Replace("{num10}", i.Num10.ToS());

            TypeHtml = TypeHtml.Replace("{nvarchar1}", i.Nvarchar1);
            TypeHtml = TypeHtml.Replace("{nvarchar2}", i.Nvarchar2);
            TypeHtml = TypeHtml.Replace("{nvarchar3}", i.Nvarchar3);
            TypeHtml = TypeHtml.Replace("{nvarchar4}", i.Nvarchar4.IsNullOrEmpty()?"随时入住":i.Nvarchar4);
            TypeHtml = TypeHtml.Replace("{nvarchar5}", i.Nvarchar5);
            TypeHtml = TypeHtml.Replace("{nvarchar6}", i.Nvarchar6);
            TypeHtml = TypeHtml.Replace("{nvarchar7}", i.Nvarchar7);
            TypeHtml = TypeHtml.Replace("{nvarchar8}", i.Nvarchar8);
            TypeHtml = TypeHtml.Replace("{nvarchar9}", i.Nvarchar9);
            TypeHtml = TypeHtml.Replace("{nvarchar10}", i.Nvarchar10);
            TypeHtml = TypeHtml.Replace("{nvarchar11}", i.Nvarchar11);
            TypeHtml = TypeHtml.Replace("{nvarchar12}", i.Nvarchar12);
            TypeHtml = TypeHtml.Replace("{nvarchar13}", i.Nvarchar13);
            TypeHtml = TypeHtml.Replace("{nvarchar14}", i.Nvarchar14);
            TypeHtml = TypeHtml.Replace("{nvarchar15}", i.Nvarchar15);

            TypeHtml = TypeHtml.Replace("{outtime}", i.OutTime.ToS());
            TypeHtml = TypeHtml.Replace("{posttime}", i.PostTime.ToS());
            TypeHtml = TypeHtml.Replace("{replycount}", i.ReplyCount.ToS());
            TypeHtml = TypeHtml.Replace("{tel}", i.Tel);
            TypeHtml = TypeHtml.Replace("{tellocation}", i.TelLocation);
            TypeHtml = TypeHtml.Replace("{text1}", i.Text1);
            TypeHtml = TypeHtml.Replace("{text2}", i.Text2);
            TypeHtml = TypeHtml.Replace("{text3}", i.Text3);
            TypeHtml = TypeHtml.Replace("{text4}", i.Text4);
            TypeHtml = TypeHtml.Replace("{text5}", i.Text5);

            TypeHtml = TypeHtml.Replace("{title}", i.Title);
            TypeHtml = TypeHtml.Replace("{userid}", i.UserID.ToS());
            TypeHtml = TypeHtml.Replace("{username}", i.UserName);
            TypeHtml = TypeHtml.Replace("{ztid}", i.ZtID.ToS());
            TypeHtml = TypeHtml.Replace("{ztname}", i.ZtName);
        }
        #endregion
    }
}
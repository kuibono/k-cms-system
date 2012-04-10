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
    public partial class InfoTypeEdit : AdminBase
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
            var i = InfoTypeView.GetModelByID(id.ToS());
            txt_TypeName.Text = i.TypeName;
            txt_TemplateIndex.Text = i.TemplateIndex;
            txt_TemplateList.Text = i.TemplateList;
            txt_TemplateContent.Text = i.TemplateContent;

            txt_TemplateForm.Text = i.TemplateForm;
            txt_TemplateAdminForm.Text = i.TemlateAdminForm;

            txt_Num1.Text = i.Num1;
            txt_Num2.Text = i.Num2;
            txt_Num3.Text = i.Num3;
            txt_Num4.Text = i.Num4;
            txt_Num5.Text = i.Num5;
            txt_Num6.Text = i.Num6;
            txt_Num7.Text = i.Num7;
            txt_Num8.Text = i.Num8;
            txt_Num9.Text = i.Num9;
            txt_Num10.Text = i.Num10;

            txt_Nvarchar1.Text = i.Nvarchar1;
            txt_Nvarchar2.Text = i.Nvarchar2;
            txt_Nvarchar3.Text = i.Nvarchar3;
            txt_Nvarchar4.Text = i.Nvarchar4;
            txt_Nvarchar5.Text = i.Nvarchar5;
            txt_Nvarchar6.Text = i.Nvarchar6;
            txt_Nvarchar7.Text = i.Nvarchar7;
            txt_Nvarchar8.Text = i.Nvarchar8;
            txt_Nvarchar9.Text = i.Nvarchar9;
            txt_Nvarchar10.Text = i.Nvarchar10;
            txt_Nvarchar11.Text = i.Nvarchar11;
            txt_Nvarchar12.Text = i.Nvarchar12;
            txt_Nvarchar13.Text = i.Nvarchar13;
            txt_Nvarchar14.Text = i.Nvarchar14;
            txt_Nvarchar15.Text = i.Nvarchar15;

            txt_Decimal1.Text = i.Decimal1;
            txt_Decimal2.Text = i.Decimal2;
            txt_Decimal3.Text = i.Decimal3;
            txt_Decimal4.Text = i.Decimal4;
            txt_Decimal5.Text = i.Decimal5;

            txt_Text1.Text = i.Text1;
            txt_Text2.Text = i.Text2;
            txt_Text3.Text = i.Text3;
            txt_Text4.Text = i.Text4;
            txt_Text5.Text = i.Text5;

            txt_Bit1.Text = i.Bit1;
            txt_Bit2.Text = i.Bit2;
            txt_Bit3.Text = i.Bit3;
            txt_Bit4.Text = i.Bit4;
            txt_Bit5.Text = i.Bit5;




        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int id = WS.RequestInt("id");
            var i = InfoTypeView.GetModelByID(id.ToS());
            i.TypeName = txt_TypeName.Text.ToSqlEnCode();
            i.TemplateIndex = txt_TemplateIndex.Text.ToSqlEnCode();
            i.TemplateList = txt_TemplateList.Text.ToSqlEnCode();
            i.TemplateContent = txt_TemplateContent.Text.ToSqlEnCode();

            i.TemplateForm = txt_TemplateForm.Text.ToSqlEnCode();
            i.TemlateAdminForm = txt_TemplateAdminForm.Text.ToSqlEnCode();

            i.Num1 = txt_Num1.Text;
            i.Num2 = txt_Num2.Text;
            i.Num3 = txt_Num3.Text;
            i.Num4 = txt_Num4.Text;
            i.Num5 = txt_Num5.Text;
            i.Num6 = txt_Num6.Text;
            i.Num7 = txt_Num7.Text;
            i.Num8 = txt_Num8.Text;
            i.Num9 = txt_Num9.Text;
            i.Num10 = txt_Num10.Text;

            i.Nvarchar1 = txt_Nvarchar1.Text;
            i.Nvarchar2 = txt_Nvarchar2.Text;
            i.Nvarchar3 = txt_Nvarchar3.Text;
            i.Nvarchar4 = txt_Nvarchar4.Text;
            i.Nvarchar5 = txt_Nvarchar5.Text;
            i.Nvarchar6 = txt_Nvarchar6.Text;
            i.Nvarchar7 = txt_Nvarchar7.Text;
            i.Nvarchar8 = txt_Nvarchar8.Text;
            i.Nvarchar9 = txt_Nvarchar9.Text;
            i.Nvarchar10 = txt_Nvarchar10.Text;
            i.Nvarchar11 = txt_Nvarchar11.Text;
            i.Nvarchar12 = txt_Nvarchar12.Text;
            i.Nvarchar13 = txt_Nvarchar13.Text;
            i.Nvarchar14 = txt_Nvarchar14.Text;
            i.Nvarchar15 = txt_Nvarchar15.Text;

            i.Decimal1 = txt_Decimal1.Text;
            i.Decimal2 = txt_Decimal2.Text;
            i.Decimal3 = txt_Decimal3.Text;
            i.Decimal4 = txt_Decimal4.Text;
            i.Decimal5 = txt_Decimal5.Text;

            i.Text1 = txt_Text1.Text;
            i.Text2 = txt_Text2.Text;
            i.Text3 = txt_Text3.Text;
            i.Text4 = txt_Text4.Text;
            i.Text5 = txt_Text5.Text;

            i.Bit1 = txt_Bit1.Text;
            i.Bit2 = txt_Bit2.Text;
            i.Bit3 = txt_Bit3.Text;
            i.Bit4 = txt_Bit4.Text;
            i.Bit5 = txt_Bit5.Text;

            if (i.Id > 0)
            {
                InfoTypeView.Update(i);

            }
            else
            {
                InfoTypeView.Insert(i);
            }

            Response.Redirect("InfoTypeList.aspx");
        }
    }
}
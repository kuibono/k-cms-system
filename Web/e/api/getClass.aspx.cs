using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Voodoo;
using Voodoo.Basement;
using Voodoo.Model;
using Voodoo.DAL;
using System.IO;

namespace Web.e.api
{
    public partial class getClass : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ClassName=WS.RequestString("class");
            int Model=WS.RequestInt("m",4);
            if (ClassName.Length == 0)
            {
                return;
            }

            Class cls = ClassView.Find(string.Format("ClassName=N'{0}' and ModelID={1}", ClassName, Model));
            if (cls.ID <= 0)
            {
                //cls.ClassForder = PinyinHelper.GetPinyin(ClassName);
                cls.ClassForder = ClassName;
                cls.ClassKeywords = ClassName;
                cls.ClassName = ClassName;
                cls.ModelID = Model;
                cls.IsLeafClass = true;
                cls.ModelID = Model;
                cls.ShowInNav = true;
                ClassView.Insert(cls);
            }
            Response.Clear();
            Response.Write(Voodoo.IO.XML.Serialize(cls));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Security;


namespace Voodoo.Basement
{
    public class NewsAction
    {
        /// <summary>
        /// 系统中的所有栏目
        /// </summary>
        public static List<Voodoo.Model.Class> NewsClass
        {
            get
            {
                if (Voodoo.Cache.Cache.GetCache("_NewClassList") == null)
                {
                    Cache.Cache.SetCache("_NewClassList", ClassView.GetModelList(), 10);
                }
                return (List<Voodoo.Model.Class>)Voodoo.Cache.Cache.GetCache("_NewClassList");
            }
        }

        public static List<Voodoo.Model.Zt> NewsZt
        {
            get
            {
                if (Voodoo.Cache.Cache.GetCache("_NewsZtList") == null)
                {
                    Cache.Cache.SetCache("_NewsZtList", ZtView.GetModelList(), 10);
                }
                return (List<Voodoo.Model.Zt>)Voodoo.Cache.Cache.GetCache("_NewsZtList");
            }

        }


    }
}

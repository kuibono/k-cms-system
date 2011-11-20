using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo.Model;

namespace Voodoo.DAL
{
    public partial class ClassView
    {
        public static List<Class> GetSubClass(Class cls)
        {
            return ClassView.GetModelList(string.Format("ParentID={0}", cls.ID));
        }
    }
}

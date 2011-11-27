using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.IO;

using Voodoo;
using Voodoo.IO;
using Voodoo.Model;
using Voodoo.DAL;

namespace Voodoo.Basement
{
    public class ClassAction
    {
        #region 删除栏目
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
        public static Result DeleteClass(List<Class> classes)
        {
            Result r = new Result();
            foreach (var cls in classes)
            {
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath(BasePage.GetClassUrl(cls)));
                if (dir.Exists)
                {
                    dir.Delete(true);
                }
                ClassView.DelByID(cls.ID);
            }

            r.Success = true;
            r.Text = string.Format("成功删除{0}个栏目",classes.Count);
            return r;
        }
        #endregion 
    }
}

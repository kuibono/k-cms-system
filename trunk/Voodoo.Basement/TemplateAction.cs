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
    public class TemplateAction
    {
        #region 系统模型
        /// <summary>
        /// 系统模型
        /// </summary>
        public static List<SysModel> AllSysModel
        {
            get
            {
                if (Voodoo.Cache.Cache.GetCache("_SysModelList") == null)
                {
                    Voodoo.Cache.Cache.SetCache("_SysModelList", SysModelView.GetModelList(),100);
                }
                return (List<SysModel>)Voodoo.Cache.Cache.GetCache("_SysModelList");
            }
        }
        #endregion

        public static string GetNameByID(int id)
        {
            try
            {
                return AllSysModel.Where(p => p.ID == id).First().ModelName;
            }
            catch
            {
                return "错误的系统模型";
            }
        }
    }
}

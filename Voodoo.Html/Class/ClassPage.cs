using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Setting;
using Voodoo.Basement;
using System.Text.RegularExpressions;

namespace Voodoo.Html.Class
{
    public class ClassPage : PageCreaterBase
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ClassPage()
        {
            this.TemplateType = TempType.列表;
        }
        #endregion

        #region 获取指定页的地址
        /// <summary>
        /// 获取指定页的地址
        /// </summary>
        /// <param name="cls"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public string GetWebPath(Voodoo.Model.Class cls, int page)
        {

            string result = "";

            result = string.Format("{0}/{1}{2}/index{3}",
                "",
                cls.ParentClassForder.IsNullOrEmpty() ? "" : cls.ParentClassForder + "/",
                cls.ClassForder,
                page > 1 ? "_" + page.ToS() + BasePage.SystemSetting.ExtName : BasePage.SystemSetting.ExtName
                );

            result = Regex.Replace(result, "[/]{2,}", "/");
            return result;
        }
        #endregion 

        protected string GetClassHtml(Voodoo.Model.Class c, int page)
        {
            return "";
        }

        #region 获取分类页面的物理路径
        /// <summary>
        /// 获取分类页面的物理路径
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public override string GetWebPath(object Model)
        {
            Voodoo.Model.Class cls = Model as Voodoo.Model.Class;
            return System.Web.HttpContext.Current.Server.MapPath(GetWebPath(cls, 1));
        }
        #endregion

        #region 获取页面URL
        /// <summary>
        /// 获取页面URL
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public override string GetWebUrl(object Model)
        {
            Voodoo.Model.Class cls = Model as Voodoo.Model.Class;
            return GetWebPath(cls, 1);
        }
        #endregion

        #region  获取模版内容
        /// <summary>
        /// 获取模版内容
        /// </summary>
        /// <returns></returns>
        protected override string GetTemplateString(int ModelID)
        {
            TemplateList tl = TemplateListView.Find(string.Format("GroupID={0} and SysModel={1}", DefaultGroup.ID.ToS(),ModelID.ToString()));

            return tl.Content;
        }
        #endregion

        /// <summary>
        /// 获取Class的HTML
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public override string GetHtml(object Model)
        {
            Voodoo.Model.Class cls = Model as Voodoo.Model.Class;

            string templateContent = this.GetTemplateString(cls.ModelID);

            return templateContent;
        }

        /// <summary>
        /// 创建静态页面
        /// </summary>
        /// <param name="Model"></param>
        public override void CreatePage(object Model)
        {
            base.CreatePage(Model);
        }
    }
}

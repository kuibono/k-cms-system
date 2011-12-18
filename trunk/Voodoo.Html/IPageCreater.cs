using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voodoo.Html
{
    /// <summary>
    /// 生成静态页面的接口
    /// </summary>
    public interface IPageCreater
    {
        /// <summary>
        /// 获取页面的物理路径
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        string GetWebPath(object Model);

        /// <summary>
        /// 获取页面的URL
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        string GetWebUrl(object Model);

        /// <summary>
        /// 获取页面的HTML
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        string GetHtml(object Model);

        /// <summary>
        /// 创建页面
        /// </summary>
        /// <param name="Model"></param>
        void CreatePage(object Model);
    }
}

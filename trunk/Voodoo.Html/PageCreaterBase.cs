using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo.Model;
using Voodoo.DAL;

namespace Voodoo.Html
{

    public abstract class PageCreaterBase : IPageCreater
    {

        #region 模版类型
        /// <summary>
        /// 模版类型
        /// </summary>
        public TempType TemplateType { get; set; }
        #endregion

        #region 获取模版内容 私有
        /// <summary>
        /// 获取模版内容
        /// </summary>
        /// <returns></returns>
        protected abstract string GetTemplateString();
        #endregion

        #region 默认模板组
        /// <summary>
        /// 默认模板组
        /// </summary>
        public static TemplateGroup DefaultGroup
        {
            get
            {
                return TemplateGroupView.Find("IsDefault=1");
            }
        }
        #endregion

        #region 获取页面的物理路径
        /// <summary>
        /// 获取页面的物理路径
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public virtual string GetWebPath(object Model)
        {
            return "";
        }
        #endregion

        #region 获取页面的URL
        /// <summary>
        /// 获取页面的URL
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public virtual string GetWebUrl(object Model)
        {
            return "";
        }
        #endregion

        #region 获取页面的HTML
        /// <summary>
        /// 获取页面的HTML
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public virtual string GetHtml(object Model)
        {
            return "";
        }
        #endregion 

        #region 创建页面
        /// <summary>
        /// 创建页面
        /// </summary>
        /// <param name="Model"></param>
        public virtual void CreatePage(object Model)
        {

        }
        #endregion
    }

    #region 页面模板类型 enum TempType
    /// <summary>
    /// 页面模板类型 enum TempType
    /// </summary>
    public enum TempType
    {
        首页,
        控制面板,
        全站搜索,
        高级搜索,
        横向搜索JS,
        纵向搜索JS,
        相关信息,
        留言板,
        评论js调用,
        最终下载页,
        下载地址,
        在线播放地址,
        列表分页,
        登陆状态,
        JS调用登陆,
        封面,
        内容,
        列表,
        搜索,
        相册图片列表,
        问答回答列表
    }
    #endregion
}

/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012-6-7 9:53:00
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表TemplatePage的实体类
    ///</summary>
    public partial class TemplatePage
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 0不生成 1首页 2列表 3内容 4章节、播放、图片等内页
        /// </summary>
        public int CreateWith { get; set; }

        ///实体复制
        public TemplatePage Clone()
        {
            return (TemplatePage)this.MemberwiseClone();
        }
    }

}



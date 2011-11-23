/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-23 14:16:23
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表Images的实体类
    ///</summary>
    public partial class Images
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 相册ID
        /// </summary>
        public int AlbumID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public System.DateTime UploadTime { get; set; }

        /// <summary>
        /// 点击数
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string SmallPath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExtName { get; set; }

        ///实体复制
        public Images Clone()
        {
            return (Images)this.MemberwiseClone();
        }
    }

}



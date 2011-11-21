/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-21 8:37:12
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表News的实体类
    ///</summary>
    public partial class News
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime NewsTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool TitleB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool TitleI { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool TitleS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TitleColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Audit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Tuijian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Toutiao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NavUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TitleImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AutorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SetTop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ModelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DownCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileForder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ZtID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableReply { get; set; }

        ///实体复制
        public News Clone()
        {
            return (News)this.MemberwiseClone();
        }
    }

}



/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-29 8:35:20
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表Question的实体类
    ///</summary>
    public partial class Question
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ZtID { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 提问者ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 提问者账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 提问时间
        /// </summary>
        public System.DateTime AskTime { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ClickCount { get; set; }

        ///实体复制
        public Question Clone()
        {
            return (Question)this.MemberwiseClone();
        }
    }

}



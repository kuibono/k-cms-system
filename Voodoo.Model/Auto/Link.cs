/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011/11/19 19:22:50
*生成者：Kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表Link的实体类
    ///</summary>
    public class Link
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 连接文本
        /// </summary>
        public string LinkTitle { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        ///实体复制
        public Link Clone()
        {
            return (Link)this.MemberwiseClone();
        }
    }

}



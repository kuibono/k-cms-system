/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012-4-12 14:34:19
*生成者：Kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表BookRole的实体类
    ///</summary>
    public partial class BookRole
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BookID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Intro { get; set; }

        ///实体复制
        public BookRole Clone()
        {
            return (BookRole)this.MemberwiseClone();
        }
    }

}



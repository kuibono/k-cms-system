/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/3/17 23:00:25
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表SysKeyword的实体类
    ///</summary>
    public partial class SysKeyword
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ModelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ClickCount { get; set; }

        ///实体复制
        public SysKeyword Clone()
        {
            return (SysKeyword)this.MemberwiseClone();
        }
    }

}



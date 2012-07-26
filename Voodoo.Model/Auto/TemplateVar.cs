/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/7/24 13:12:04
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表TemplateVar的实体类
    ///</summary>
    public partial class TemplateVar
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPublic { get; set; }

        ///实体复制
        public TemplateVar Clone()
        {
            return (TemplateVar)this.MemberwiseClone();
        }
    }

}



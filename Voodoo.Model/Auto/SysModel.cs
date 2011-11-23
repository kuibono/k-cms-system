/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-23 10:39:03
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表SysModel的实体类
    ///</summary>
    public partial class SysModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 模型名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 表
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 子类
        /// </summary>
        public string SonClass { get; set; }

        ///实体复制
        public SysModel Clone()
        {
            return (SysModel)this.MemberwiseClone();
        }
    }

}



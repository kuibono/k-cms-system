/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011/11/18 9:23:50
*生成者：Kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表UserForm的实体类
    ///</summary>
    public class UserForm
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 表单内容
        /// </summary>
        public string Content { get; set; }

        ///实体复制
        public UserForm Clone()
        {
            return (UserForm)this.MemberwiseClone();
        }
    }

}



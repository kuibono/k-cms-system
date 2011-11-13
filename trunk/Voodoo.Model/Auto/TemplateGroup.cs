/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-8 15:29:09
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表TemplateGroup的实体类
	///</summary>
	public class TemplateGroup
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }

        /// <summary>
        /// 群组
        /// </summary>
        public int GroupID { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string GroupName{get ; set; }
		
		/// <summary>
		/// 是默认群组
		/// </summary>
		public bool IsDefault{get ; set; }
		
		///实体复制
		public TemplateGroup Clone()
        {
            return (TemplateGroup)this.MemberwiseClone();
        }
	}
	 
}



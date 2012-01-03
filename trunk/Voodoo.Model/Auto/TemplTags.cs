/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:16
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表TemplTags的实体类
	///</summary>
	public partial class TemplTags
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string TagName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string TagCode{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string FunctionName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string TagFormat{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Remark{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool Enable{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int TagIndex{get ; set; }
		
		///实体复制
		public TemplTags Clone()
        {
            return (TemplTags)this.MemberwiseClone();
        }
	}
	 
}



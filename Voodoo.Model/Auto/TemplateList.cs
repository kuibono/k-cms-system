/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:16
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表TemplateList的实体类
	///</summary>
	public partial class TemplateList
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int GroupID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string TempName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int SysModel{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int CutKeywords{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int CutTitle{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ShowRecordCount{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string TimeFormat{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Content{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ListVar{get ; set; }
		
		///实体复制
		public TemplateList Clone()
        {
            return (TemplateList)this.MemberwiseClone();
        }
	}
	 
}



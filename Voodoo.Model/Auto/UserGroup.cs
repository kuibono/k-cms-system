/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:16
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表UserGroup的实体类
	///</summary>
	public partial class UserGroup
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string GroupName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int Grade{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int MaxPost{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool PostAotuAudit{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int RegForm{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool EnableReg{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool RegAutoAudit{get ; set; }
		
		///实体复制
		public UserGroup Clone()
        {
            return (UserGroup)this.MemberwiseClone();
        }
	}
	 
}



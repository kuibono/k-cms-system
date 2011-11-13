/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-8 15:29:09
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表SysUser的实体类
	///</summary>
	public class SysUser
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string UserName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string UserPass{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public long Logincount{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime LastLoginTime{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string LastLoginIP{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string SafeQuestion{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string SafeAnswer{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int Department{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ChineseName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int UserGroup{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Email{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string TelNumber{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool Enabled{get ; set; }
		
		///实体复制
		public SysUser Clone()
        {
            return (SysUser)this.MemberwiseClone();
        }
	}
	 
}



/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:16
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表SysRole的实体类
	///</summary>
	public partial class SysRole
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string RoleName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int RoleGroup{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string URL{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool AddRole{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool EditRole{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool DelRole{get ; set; }
		
		///实体复制
		public SysRole Clone()
        {
            return (SysRole)this.MemberwiseClone();
        }
	}
	 
}



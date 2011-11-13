/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-8 15:29:09
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表UserRegorm的实体类
	///</summary>
	public class UserRegorm
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string FormName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string FormContent{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Remark{get ; set; }
		
		///实体复制
		public UserRegorm Clone()
        {
            return (UserRegorm)this.MemberwiseClone();
        }
	}
	 
}



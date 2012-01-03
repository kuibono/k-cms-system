/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:16
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表UserClassRelation的实体类
	///</summary>
	public partial class UserClassRelation
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int UserID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ClassID{get ; set; }
		
		///实体复制
		public UserClassRelation Clone()
        {
            return (UserClassRelation)this.MemberwiseClone();
        }
	}
	 
}



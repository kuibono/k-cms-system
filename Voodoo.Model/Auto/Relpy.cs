/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/4/9 20:34:17
*生成者：Kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表Relpy的实体类
	///</summary>
	public partial class Relpy
	{
		/// <summary>
		/// 
		/// </summary>
		public int Id{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ModelID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int NewsID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int UserID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string UserName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Ip{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime ReplyTime{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Content{get ; set; }
		
		///实体复制
		public Relpy Clone()
        {
            return (Relpy)this.MemberwiseClone();
        }
	}
	 
}



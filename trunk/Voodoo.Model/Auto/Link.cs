/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:16
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表Link的实体类
	///</summary>
	public partial class Link
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string LinkTitle{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Url{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int Index{get ; set; }
		
		///实体复制
		public Link Clone()
        {
            return (Link)this.MemberwiseClone();
        }
	}
	 
}


/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:15
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表BookVolume的实体类
	///</summary>
	public partial class BookVolume
	{
		/// <summary>
		/// 
		/// </summary>
		public long ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int BookID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Booktitle{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Booknclass{get ; set; }
		
		///实体复制
		public BookVolume Clone()
        {
            return (BookVolume)this.MemberwiseClone();
        }
	}
	 
}



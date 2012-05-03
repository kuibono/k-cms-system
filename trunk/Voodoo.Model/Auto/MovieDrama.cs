/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012-5-3 10:57:39
*生成者：Kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表MovieDrama的实体类
	///</summary>
	public partial class MovieDrama
	{
		/// <summary>
		/// 
		/// </summary>
		public long Id{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int MovieID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string MovieTitle{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Title{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime UpdateTime{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool Enable{get ; set; }
		
		///实体复制
		public MovieDrama Clone()
        {
            return (MovieDrama)this.MemberwiseClone();
        }
	}
	 
}



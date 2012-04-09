/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/4/9 20:34:16
*生成者：Kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表MovieUrl的实体类
	///</summary>
	public partial class MovieUrl
	{
		/// <summary>
		/// 
		/// </summary>
		public long Id{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int MoviewID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string MoviewTitle{get ; set; }
		
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
		
		/// <summary>
		/// 
		/// </summary>
		public string HttpUrl{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string MagUrl{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string KuaibUrl{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string BaiduUrl{get ; set; }
		
		///实体复制
		public MovieUrl Clone()
        {
            return (MovieUrl)this.MemberwiseClone();
        }
	}
	 
}



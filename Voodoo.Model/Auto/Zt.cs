/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:17
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表Zt的实体类
	///</summary>
	public partial class Zt
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ClassID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ZtName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Forder{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ExtName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ICON{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string KeyWords{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Description{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int LtIndex{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool ShowInNav{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int FaceModel{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ListModel{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ListOrder{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ListPageSize{get ; set; }
		
		///实体复制
		public Zt Clone()
        {
            return (Zt)this.MemberwiseClone();
        }
	}
	 
}



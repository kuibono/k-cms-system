/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/4/9 20:34:15
*生成者：Kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表InfoImage的实体类
	///</summary>
	public partial class InfoImage
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
		public int InfoID{get ; set; }
		
		/// <summary>
		/// 图片标题
		/// </summary>
		public string Title{get ; set; }
		
		/// <summary>
		/// 图片地址
		/// </summary>
		public string Url{get ; set; }
		
		///实体复制
		public InfoImage Clone()
        {
            return (InfoImage)this.MemberwiseClone();
        }
	}
	 
}



/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-8 15:29:09
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表File的实体类
	///</summary>
	public class File
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime UpTime{get ; set; }
		
		/// <summary>
		/// 图片
   //Flash
   //多媒体
   //其他
		/// </summary>
		public int FileType{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public long FileSize{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string FileDirectory{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string FileName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string FileExtName{get ; set; }
		
		///实体复制
		public File Clone()
        {
            return (File)this.MemberwiseClone();
        }
	}
	 
}



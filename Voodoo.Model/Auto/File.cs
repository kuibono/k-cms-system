/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:16
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表File的实体类
	///</summary>
	public partial class File
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
		public System.DateTime UpTime{get ; set; }
		
		/// <summary>
		/// 
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
		
		/// <summary>
		/// 
		/// </summary>
		public string FilePath{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string SmallPath{get ; set; }
		
		///实体复制
		public File Clone()
        {
            return (File)this.MemberwiseClone();
        }
	}
	 
}



/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:15
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表Book的实体类
	///</summary>
	public partial class Book
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
		public string ClassName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ZtID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ZtName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Title{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Author{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Intro{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public long Length{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public long ReplyCount{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public long ClickCount{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime Addtime{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public byte Status{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool IsVip{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string FaceImage{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public long SaveCount{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool Enable{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool IsFirstPost{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public long LastChapterID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string LastChapterTitle{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime UpdateTime{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public long LastVipChapterID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string LastVipChapterTitle{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime VipUpdateTime{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int CorpusID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string CorpusTitle{get ; set; }
		
		///实体复制
		public Book Clone()
        {
            return (Book)this.MemberwiseClone();
        }
	}
	 
}



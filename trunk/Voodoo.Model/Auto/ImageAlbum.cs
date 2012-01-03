/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:16
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表ImageAlbum的实体类
	///</summary>
	public partial class ImageAlbum
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 所属栏目
		/// </summary>
		public int ClassID{get ; set; }
		
		/// <summary>
		/// 专题ID
		/// </summary>
		public int ZtID{get ; set; }
		
		/// <summary>
		/// 作者ID
		/// </summary>
		public int AuthorID{get ; set; }
		
		/// <summary>
		/// 作者
		/// </summary>
		public string Author{get ; set; }
		
		/// <summary>
		/// 相册标题
		/// </summary>
		public string Title{get ; set; }
		
		/// <summary>
		/// 创建时间
		/// </summary>
		public System.DateTime CreateTime{get ; set; }
		
		/// <summary>
		/// 更新时间
		/// </summary>
		public System.DateTime UpdateTime{get ; set; }
		
		/// <summary>
		/// 简介
		/// </summary>
		public string Intro{get ; set; }
		
		/// <summary>
		/// 图片数量
		/// </summary>
		public int Size{get ; set; }
		
		/// <summary>
		/// 存放文件夹
		/// </summary>
		public string Folder{get ; set; }
		
		/// <summary>
		/// 点击数
		/// </summary>
		public int ClickCount{get ; set; }
		
		/// <summary>
		/// 评论数量
		/// </summary>
		public int ReplyCount{get ; set; }
		
		/// <summary>
		/// 置顶
		/// </summary>
		public bool SetTop{get ; set; }
		
		/// <summary>
		/// 封面
		/// </summary>
		public string ShotCut{get ; set; }
		
		/// <summary>
		/// 关键词
		/// </summary>
		public string KeyWords{get ; set; }
		
		///实体复制
		public ImageAlbum Clone()
        {
            return (ImageAlbum)this.MemberwiseClone();
        }
	}
	 
}



/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:15
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表Class的实体类
	///</summary>
	public partial class Class
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ClassName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Alter{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ParentID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool IsLeafClass{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ParentClassForder{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ClassForder{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ModelID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ClassKeywords{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ClassDescription{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ClassICON{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool ShowInNav{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int NavIndex{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string VisitRole{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool EnablePost{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool EnableVCode{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string PostRoles{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int PostcreateList{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int PostAddCent{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool NeedAudit{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int PostManagement{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int EditcreateList{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool EnableSameTitle{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool AutoAudt{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool EnableReply{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool ReplyNeedAudit{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ListModel{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ContentModel{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ReplyModel{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int SearchModel{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ClassPageMode{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ContentPageMode{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ManagementOrder{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ListPageOrder{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ClassPageExtName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ListPageSize{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ContentPageForder{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ContentPageExtName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int ContentPageNameMode{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ContentPageDirMode{get ; set; }
		
		///实体复制
		public Class Clone()
        {
            return (Class)this.MemberwiseClone();
        }
	}
	 
}



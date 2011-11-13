/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2011-11-8 15:29:09
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表User的实体类
	///</summary>
	public class User
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string UserName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string UserPass{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Email{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ChineseName{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string QQ{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string MSN{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Tel{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Mobile{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string WebSite{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Image{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Address{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ZipCode{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Intro{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime RegTime{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string RegIP{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int LoginCount{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public System.DateTime LastLoginTime{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string LastLoginIP{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int Cent{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int PostCount{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string GTalk{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Twitter{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string Weibo{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public string ICQ{get ; set; }
		
		/// <summary>
		/// 群组
		/// </summary>
		public int Group{get ; set; }
		
		///实体复制
		public User Clone()
        {
            return (User)this.MemberwiseClone();
        }
	}
	 
}



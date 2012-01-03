/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012/1/3 2:24:15
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
	///<summary>
	///表Answer的实体类
	///</summary>
	public partial class Answer
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID{get ; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public int QuestionID{get ; set; }
		
		/// <summary>
		/// 回答
		/// </summary>
		public string Content{get ; set; }
		
		/// <summary>
		/// 用户Id
		/// </summary>
		public int UserID{get ; set; }
		
		/// <summary>
		/// 用户账号
		/// </summary>
		public string UserName{get ; set; }
		
		/// <summary>
		/// 回答时间
		/// </summary>
		public System.DateTime AnswerTime{get ; set; }
		
		/// <summary>
		/// 赞同
		/// </summary>
		public int Agree{get ; set; }
		
		///实体复制
		public Answer Clone()
        {
            return (Answer)this.MemberwiseClone();
        }
	}
	 
}



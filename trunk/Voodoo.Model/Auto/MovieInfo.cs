/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2012-6-5 8:55:39
*生成者：kuibono
*/
using System;
namespace Voodoo.Model
{
    ///<summary>
    ///表MovieInfo的实体类
    ///</summary>
    public partial class MovieInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Actors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PublicYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMove { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FaceImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime InsertTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastDramaTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long LastDramaID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ReplyCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long TjCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ScoreAvg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ScoreTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 总点击数
        /// </summary>
        public long ClickCount { get; set; }

        /// <summary>
        /// 月点击数
        /// </summary>
        public long MonthClick { get; set; }

        /// <summary>
        /// 周点击数
        /// </summary>
        public long WeekClick { get; set; }

        /// <summary>
        /// 日点击数
        /// </summary>
        public long DayClick { get; set; }

        /// <summary>
        /// 最后点击时间
        /// </summary>
        public System.DateTime LastClickTime { get; set; }

        ///实体复制
        public MovieInfo Clone()
        {
            return (MovieInfo)this.MemberwiseClone();
        }
    }

}



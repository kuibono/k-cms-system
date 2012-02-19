using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;

namespace NovelCollector
{
    public class BookInfo
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Class { get; set; }

        public int Length { get; set; }

        public string Intro { get; set; }

        public string ListUrl { get; set; }

        public List<string> ChapterTitles { get; set; }

        public bool Exist()
        {
            return BookView.Exist(string.Format("Title='{0}' and Author='{1}'",this.Title,this.Author));
        }
    }
}

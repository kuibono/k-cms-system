using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCollector.Common
{
    public class RuleModel
    {
        public string SiteName { get; set; }

        public List<string> BookNeedCollect { get; set; }

        public string Encoding { get; set; }

        public string ListPageUrl { get; set; }

        public string NextPageUrl { get; set; }

        public string BookPageUrl { get; set; }

        public string BookInfo { get; set; }

        public string ChapterListPageUrl { get; set; }

        public string ChapterTitleAndUrl { get; set; }

        public string Content { get; set; }
    }
}

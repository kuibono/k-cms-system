using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Voodoo.Net;
using Voodoo;
using System.Threading;


namespace TextCollector
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string list_url = "http://www.wcxiaoshuo.com/wcxs-9910/";
            //string html_List = Url.GetHtml(list_url, "gb2312");

            //List<string> result = new List<string>();

            //var m_list = html_List.GetMatch("<li.*?><a.*?href=\"(?<key>.*?)\">.+?</a.*?></li.*?>");

            //foreach (string str in m_list)
            //{
            //    string html_content = Url.GetHtml(str.AppendToDomain(list_url), "gb2312");

            //    string content=html_content.GetMatch("<div id=\"htmlContent\" class=\"contentbox\" >[\\s]*?<div align=\"center\"><script type=\"text/javascript\" src=\"/js/nrh.js\"></script></div>(?<key>[\\s\\S]*?)</div>").First();

            //    var matchList = content.GetMatch("<img src=[\"']+?/sss/(?<key>.*?).jpg[\"']+?>");

            //    foreach (string str_img in matchList)
            //    {
            //        result.Add(str_img);
            //    }
               
            //}

            //StringBuilder sb = new StringBuilder();
            //foreach (string str in result)
            //{
            //    sb.AppendLine(str);
            //}

            //Voodoo.IO.File.Write("C:\\replace.txt", sb.ToS());

            ////Common.BookCollector cb = new Common.BookCollector(this);
            //////cb.GetChapterTitleAndUrl("很纯很暧昧");
            ////cb.Main();

            Thread th = new Thread(DoIt);
            th.Start();
            button1.Enabled = false;
           
        }

        public void DoIt()
        {
            Common.BookCollector cb = new Common.BookCollector(this);
            cb.Main();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

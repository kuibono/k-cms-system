using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Voodoo;
using System.Text.RegularExpressions;

namespace KCMDCollector
{
    public partial class FormHouse365 : Form
    {
        public FormHouse365()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("楼盘", typeof(string));


            List<string> a = richTextBox2.Text.Trim().Split('\n').ToList();

            foreach (var str in a)
            {
                dt.Columns.Add(str, typeof(string));
            }
            dt.Columns.Add("统计", typeof(string));

            List<string> b = richTextBox1.Text.Trim().Split('\n').ToList();

            foreach (var str in b)
            {
                dt.Rows.Add(str);
            }

            DataTable dtIndex = dt.Copy();

            foreach (DataRow r in dt.Rows)
            {

                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    string key = r[0] + " " + dt.Columns[i].ToString();

                    status.Text = key;
                    if (dt.Columns[i].ToString() == "统计")
                    {
                        key = r[0].ToS();
                    }
                    string url = string.Format("http://sell.house365.com/selllist_sell.php?keyword={0}", key.UrlEncode("gb2312"));

                    string html = Voodoo.Net.Url.GetHtml(url, "gb2312");

                    string value = html.GetMatch("共找到<span>(?<key>.*?)</span>条房源").First();
                    r[i] = value;
                }
            }

            foreach (DataRow r in dtIndex.Rows)
            {
                string key = r[0].ToS();
                string url = string.Format("http://sell.house365.com/selllist_sell.php?keyword={0}", key.UrlEncode("gb2312"));

                string html = Voodoo.Net.Url.GetHtml(url, "gb2312");
                for (int i = 1; i < dtIndex.Columns.Count; i++)
                {
                    string key2 = dtIndex.Columns[i].ToString();
                    status.Text = key + "覆盖率-" + key2;

                    string parrt = string.Format("<a href='http://zsb.house365.com/main.php\\?agentcode=.*?>{0}.*?</a>", key2);
                    r[i] = html.GetMatch(parrt).Count;


                }
            }

            //Voodoo.IO.ExcelHelper.Export(dt, "c:\\house365.xls", "统计");

            List<DataTableAndString> dts = new List<DataTableAndString>();
            dts.Add(new DataTableAndString()
            {
                Table = dt,
                TableName = "覆盖率"
            });
            dts.Add(new DataTableAndString()
            {
                Table = dtIndex,
                TableName = "首页占比"
            });
            Voodoo.IO.ExcelHelper.Export(dts, "c:\\house365.xls");

            System.Diagnostics.Process.Start("c:\\house365.xls");
        }
    }
}

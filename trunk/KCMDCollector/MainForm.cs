using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using Voodoo;

namespace KCMDCollector
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            SettingForm sf = new SettingForm();
            sf.Show();
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            timer_NovelCollector.Enabled = true;
            timer_NovelCollector.Start();
        }

        protected void Do()
        {
            Book.CollectBook cb = new KCMDCollector.Book.CollectBook(this);
            cb.Collect();
            if (chb_Shutdown.Checked)
            {
                System.Diagnostics.Process.Start("shutdown.exe ", "/s /t 60"); 
            }
        }

        Thread t;
        private void timer_NovelCollector_Tick(object sender, EventArgs e)
        {
            if (t == null || t.IsAlive == false)
            {
                btn_Test.Enabled = false;

                t = new Thread(Do);
                t.Start();
                btn_Test.Enabled = true;
            }
        }

        private void notifyIcon_Sys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }



        private void tool_Exit_Click(object sender, EventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }
            Application.Exit();
        }

        private void tool_Show_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s1 = "第1142章 伪装舒";
            string[] s2 = { "第1114章 于仁的想法", "第0142章 又白来了", "第0124章 找上门来" };
            var r = (from n in s2 select new { n, weight = n.GetSimilarityWith(s1) }).OrderByDescending(p => p.weight).First();


            MessageBox.Show(r.n);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }
            Application.Exit();
        }

    }
}

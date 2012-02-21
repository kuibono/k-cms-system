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

        }

        Thread t;
        private void timer_NovelCollector_Tick(object sender, EventArgs e)
        {
            if (t == null || t.IsAlive == false)
            {
                t = new Thread(Do);
                t.Start();
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
            string s1 = "红豆生南国";
            string s2 = "红豆生外国";

            string s3 = "红豆生鸡毛南国啊";

            MessageBox.Show(s1.GetSimilarityWith(s2).ToS());
            MessageBox.Show(s1.GetSimilarityWith(s3).ToS());
        }

    }
}

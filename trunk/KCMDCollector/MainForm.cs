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

            timer_NovelReplace.Enabled = true;
            timer_NovelReplace.Start();
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
            
            
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Test te = new Test();
            te.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        protected void Rep()
        {
            try
            {
                Book.CollectText ct = new Book.CollectText(this);
                ct.CollectText();
            }
            catch { }
        }

        Thread th;
        private void timer_NovelReplace_Tick(object sender, EventArgs e)
        {
            if (th == null || th.IsAlive == false)
            {
                th = new Thread(Rep);
                th.Start();
                
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //注册热键Ctrl+B，Id号为101。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。   
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.B);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            switch (m.WParam.ToInt32())
            {
                case 101:    //按下的是Ctrl+B   
                    //此处填写快捷键响应代码   
                    //this.Text = "按下的是Ctrl+B";
                    if (notifyIcon_Sys.Visible == false)
                    {
                        notifyIcon_Sys.Visible = true;
                    }
                    else
                    {
                        notifyIcon_Sys.Visible = false;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

    }
}

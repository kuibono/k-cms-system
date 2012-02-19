using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

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
            Thread t = new Thread(Do);
            t.Start();
        }

        protected void Do()
        {
            Book.CollectBook cb = new KCMDCollector.Book.CollectBook(this);
            cb.Collect();

        }
    }
}

namespace KCMDCollector
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_Setting = new System.Windows.Forms.Button();
            this.btn_Test = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer_NovelCollector = new System.Windows.Forms.Timer(this.components);
            this.progress_Chapter = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.progress_Book = new System.Windows.Forms.ProgressBar();
            this.notifyIcon_Sys = new System.Windows.Forms.NotifyIcon(this.components);
            this.strip_Exit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tool_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.chb_Shutdown = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.timer_NovelReplace = new System.Windows.Forms.Timer(this.components);
            this.fuck1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fuck2 = new System.Windows.Forms.ToolStripMenuItem();
            this.split1 = new System.Windows.Forms.ToolStripMenuItem();
            this.split2 = new System.Windows.Forms.ToolStripMenuItem();
            this.fuck3 = new System.Windows.Forms.ToolStripMenuItem();
            this.fuck4 = new System.Windows.Forms.ToolStripMenuItem();
            this.strip_Exit.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Setting
            // 
            this.btn_Setting.Location = new System.Drawing.Point(205, 95);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(38, 23);
            this.btn_Setting.TabIndex = 0;
            this.btn_Setting.Text = "set";
            this.btn_Setting.UseVisualStyleBackColor = true;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(202, 124);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(41, 23);
            this.btn_Test.TabIndex = 1;
            this.btn_Test.Text = "开始";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // timer_NovelCollector
            // 
            this.timer_NovelCollector.Interval = 1000;
            this.timer_NovelCollector.Tick += new System.EventHandler(this.timer_NovelCollector_Tick);
            // 
            // progress_Chapter
            // 
            this.progress_Chapter.Location = new System.Drawing.Point(86, 102);
            this.progress_Chapter.Name = "progress_Chapter";
            this.progress_Chapter.Size = new System.Drawing.Size(100, 16);
            this.progress_Chapter.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "当前进度：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "总进度：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "书名：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "章节：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "状态：";
            // 
            // progress_Book
            // 
            this.progress_Book.Location = new System.Drawing.Point(86, 131);
            this.progress_Book.Name = "progress_Book";
            this.progress_Book.Size = new System.Drawing.Size(100, 16);
            this.progress_Book.TabIndex = 11;
            // 
            // notifyIcon_Sys
            // 
            this.notifyIcon_Sys.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon_Sys.ContextMenuStrip = this.strip_Exit;
            this.notifyIcon_Sys.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_Sys.Icon")));
            this.notifyIcon_Sys.Text = "OUTLOOK Professional";
            this.notifyIcon_Sys.Visible = true;
            this.notifyIcon_Sys.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_Sys_MouseDoubleClick);
            // 
            // strip_Exit
            // 
            this.strip_Exit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fuck4,
            this.fuck3,
            this.tool_Show,
            this.split2,
            this.fuck2,
            this.split1,
            this.fuck1,
            this.tool_Exit});
            this.strip_Exit.Name = "strip_Exit";
            this.strip_Exit.Size = new System.Drawing.Size(259, 202);
            this.strip_Exit.Text = "退出";
            // 
            // tool_Exit
            // 
            this.tool_Exit.Name = "tool_Exit";
            this.tool_Exit.Size = new System.Drawing.Size(258, 22);
            this.tool_Exit.Text = "Exit(&Q)";
            this.tool_Exit.Click += new System.EventHandler(this.tool_Exit_Click);
            // 
            // tool_Show
            // 
            this.tool_Show.Name = "tool_Show";
            this.tool_Show.Size = new System.Drawing.Size(258, 22);
            this.tool_Show.Text = "Auto Sync";
            this.tool_Show.Click += new System.EventHandler(this.tool_Show_Click);
            // 
            // chb_Shutdown
            // 
            this.chb_Shutdown.AutoSize = true;
            this.chb_Shutdown.Location = new System.Drawing.Point(19, 165);
            this.chb_Shutdown.Name = "chb_Shutdown";
            this.chb_Shutdown.Size = new System.Drawing.Size(84, 16);
            this.chb_Shutdown.TabIndex = 12;
            this.chb_Shutdown.Text = "完成后关机";
            this.chb_Shutdown.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "dev";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(212, 161);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "t";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btn_Setting);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btn_Test);
            this.groupBox1.Controls.Add(this.chb_Shutdown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.progress_Book);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.progress_Chapter);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 190);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "书籍采集";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(12, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 96);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "书籍替换";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "书名：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(62, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "label10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(62, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(62, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "label12";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 75);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "状态：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 9;
            this.label14.Text = "章节：";
            // 
            // timer_NovelReplace
            // 
            this.timer_NovelReplace.Interval = 5000;
            this.timer_NovelReplace.Tick += new System.EventHandler(this.timer_NovelReplace_Tick);
            // 
            // fuck1
            // 
            this.fuck1.Name = "fuck1";
            this.fuck1.Size = new System.Drawing.Size(258, 22);
            this.fuck1.Text = "Hide on Minsize(&H)";
            // 
            // fuck2
            // 
            this.fuck2.Name = "fuck2";
            this.fuck2.Size = new System.Drawing.Size(258, 22);
            this.fuck2.Text = "Show Newmail desctopMessage(&S)";
            // 
            // split1
            // 
            this.split1.Name = "split1";
            this.split1.Size = new System.Drawing.Size(258, 22);
            this.split1.Text = "-";
            // 
            // split2
            // 
            this.split2.Name = "split2";
            this.split2.Size = new System.Drawing.Size(258, 22);
            this.split2.Text = "-";
            // 
            // fuck3
            // 
            this.fuck3.Name = "fuck3";
            this.fuck3.Size = new System.Drawing.Size(258, 22);
            this.fuck3.Text = "Show Network change(&N)";
            // 
            // fuck4
            // 
            this.fuck4.Name = "fuck4";
            this.fuck4.Size = new System.Drawing.Size(258, 22);
            this.fuck4.Text = "Show Network Alert(&A)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 315);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "KCMS内容采集系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.strip_Exit.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Setting;
        private System.Windows.Forms.Button btn_Test;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer_NovelCollector;
        public System.Windows.Forms.ProgressBar progress_Chapter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ProgressBar progress_Book;
        private System.Windows.Forms.NotifyIcon notifyIcon_Sys;
        private System.Windows.Forms.ContextMenuStrip strip_Exit;
        private System.Windows.Forms.ToolStripMenuItem tool_Exit;
        private System.Windows.Forms.ToolStripMenuItem tool_Show;
        private System.Windows.Forms.CheckBox chb_Shutdown;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Timer timer_NovelReplace;
        private System.Windows.Forms.ToolStripMenuItem fuck4;
        private System.Windows.Forms.ToolStripMenuItem fuck3;
        private System.Windows.Forms.ToolStripMenuItem split2;
        private System.Windows.Forms.ToolStripMenuItem fuck2;
        private System.Windows.Forms.ToolStripMenuItem split1;
        private System.Windows.Forms.ToolStripMenuItem fuck1;
    }
}


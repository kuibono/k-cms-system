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
            this.strip_Exit.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Setting
            // 
            this.btn_Setting.Location = new System.Drawing.Point(198, 76);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(38, 23);
            this.btn_Setting.TabIndex = 0;
            this.btn_Setting.Text = "set";
            this.btn_Setting.UseVisualStyleBackColor = true;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(195, 105);
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
            this.label1.Location = new System.Drawing.Point(55, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 56);
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
            this.progress_Chapter.Location = new System.Drawing.Point(79, 83);
            this.progress_Chapter.Name = "progress_Chapter";
            this.progress_Chapter.Size = new System.Drawing.Size(100, 16);
            this.progress_Chapter.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "当前进度：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "总进度：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "书名：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "章节：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "状态：";
            // 
            // progress_Book
            // 
            this.progress_Book.Location = new System.Drawing.Point(79, 112);
            this.progress_Book.Name = "progress_Book";
            this.progress_Book.Size = new System.Drawing.Size(100, 16);
            this.progress_Book.TabIndex = 11;
            // 
            // notifyIcon_Sys
            // 
            this.notifyIcon_Sys.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon_Sys.ContextMenuStrip = this.strip_Exit;
            this.notifyIcon_Sys.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_Sys.Icon")));
            this.notifyIcon_Sys.Text = "大脚采集器";
            this.notifyIcon_Sys.Visible = true;
            this.notifyIcon_Sys.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_Sys_MouseDoubleClick);
            // 
            // strip_Exit
            // 
            this.strip_Exit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_Exit,
            this.tool_Show});
            this.strip_Exit.Name = "strip_Exit";
            this.strip_Exit.Size = new System.Drawing.Size(119, 48);
            this.strip_Exit.Text = "退出";
            // 
            // tool_Exit
            // 
            this.tool_Exit.Name = "tool_Exit";
            this.tool_Exit.Size = new System.Drawing.Size(118, 22);
            this.tool_Exit.Text = "退出";
            this.tool_Exit.Click += new System.EventHandler(this.tool_Exit_Click);
            // 
            // tool_Show
            // 
            this.tool_Show.Name = "tool_Show";
            this.tool_Show.Size = new System.Drawing.Size(118, 22);
            this.tool_Show.Text = "显示界面";
            this.tool_Show.Click += new System.EventHandler(this.tool_Show_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 150);
            this.Controls.Add(this.progress_Book);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progress_Chapter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.btn_Setting);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "KCMS内容采集系统";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.strip_Exit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}


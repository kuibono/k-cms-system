namespace SimpleCollector
{
    partial class FormMain
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
            FormMain.CheckForIllegalCrossThreadCalls = false;
            this.txt_ListUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_BookTitle = new System.Windows.Forms.TextBox();
            this.txt_ChapterTitleRule = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_ContentRule = new System.Windows.Forms.RichTextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.lb_Status = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Encoding = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_ListUrl
            // 
            this.txt_ListUrl.Location = new System.Drawing.Point(95, 6);
            this.txt_ListUrl.Name = "txt_ListUrl";
            this.txt_ListUrl.Size = new System.Drawing.Size(477, 21);
            this.txt_ListUrl.TabIndex = 0;
            this.txt_ListUrl.Text = "http://www.wenku.com/htmpage/14/1408/index.htm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "列表页地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "书籍名称：";
            // 
            // txt_BookTitle
            // 
            this.txt_BookTitle.Location = new System.Drawing.Point(95, 34);
            this.txt_BookTitle.Name = "txt_BookTitle";
            this.txt_BookTitle.Size = new System.Drawing.Size(182, 21);
            this.txt_BookTitle.TabIndex = 3;
            this.txt_BookTitle.Text = "少年阿宾";
            // 
            // txt_ChapterTitleRule
            // 
            this.txt_ChapterTitleRule.Location = new System.Drawing.Point(95, 61);
            this.txt_ChapterTitleRule.Name = "txt_ChapterTitleRule";
            this.txt_ChapterTitleRule.Size = new System.Drawing.Size(477, 96);
            this.txt_ChapterTitleRule.TabIndex = 4;
            this.txt_ChapterTitleRule.Text = "<span class=\'middle\'>&nbsp;<a href=\'(?<url>.*?)\'>(?<title>.*?)</a></span>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "地址标题规则：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "内容规则：";
            // 
            // txt_ContentRule
            // 
            this.txt_ContentRule.Location = new System.Drawing.Point(95, 163);
            this.txt_ContentRule.Name = "txt_ContentRule";
            this.txt_ContentRule.Size = new System.Drawing.Size(477, 96);
            this.txt_ContentRule.TabIndex = 6;
            this.txt_ContentRule.Text = "<!--this-is-text-->(?<key>[\\s\\S]*?)<!--this-is-text-->";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(464, 283);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 8;
            this.btn_Start.Text = "开始采集";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // lb_Status
            // 
            this.lb_Status.AutoSize = true;
            this.lb_Status.Location = new System.Drawing.Point(24, 283);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(41, 12);
            this.lb_Status.TabIndex = 9;
            this.lb_Status.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(343, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "编码：";
            // 
            // txt_Encoding
            // 
            this.txt_Encoding.Location = new System.Drawing.Point(390, 33);
            this.txt_Encoding.Name = "txt_Encoding";
            this.txt_Encoding.Size = new System.Drawing.Size(182, 21);
            this.txt_Encoding.TabIndex = 11;
            this.txt_Encoding.Text = "utf-8";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 328);
            this.Controls.Add(this.txt_Encoding);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lb_Status);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_ContentRule);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_ChapterTitleRule);
            this.Controls.Add(this.txt_BookTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_ListUrl);
            this.Name = "FormMain";
            this.Text = "简单采集软件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ListUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_BookTitle;
        private System.Windows.Forms.RichTextBox txt_ChapterTitleRule;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txt_ContentRule;
        private System.Windows.Forms.Button btn_Start;
        public System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Encoding;
    }
}


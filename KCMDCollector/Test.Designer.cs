namespace KCMDCollector
{
    partial class Test
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_sina = new System.Windows.Forms.Button();
            this.btn_Baidu = new System.Windows.Forms.Button();
            this.btn_Neasy = new System.Windows.Forms.Button();
            this.btn_Cnblogs = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_sina
            // 
            this.btn_sina.Location = new System.Drawing.Point(12, 5);
            this.btn_sina.Name = "btn_sina";
            this.btn_sina.Size = new System.Drawing.Size(75, 23);
            this.btn_sina.TabIndex = 5;
            this.btn_sina.Text = "sina";
            this.btn_sina.UseVisualStyleBackColor = true;
            this.btn_sina.Click += new System.EventHandler(this.btn_sina_Click);
            // 
            // btn_Baidu
            // 
            this.btn_Baidu.Location = new System.Drawing.Point(93, 5);
            this.btn_Baidu.Name = "btn_Baidu";
            this.btn_Baidu.Size = new System.Drawing.Size(75, 23);
            this.btn_Baidu.TabIndex = 6;
            this.btn_Baidu.Text = "sohu";
            this.btn_Baidu.UseVisualStyleBackColor = true;
            this.btn_Baidu.Click += new System.EventHandler(this.btn_Baidu_Click);
            // 
            // btn_Neasy
            // 
            this.btn_Neasy.Location = new System.Drawing.Point(174, 5);
            this.btn_Neasy.Name = "btn_Neasy";
            this.btn_Neasy.Size = new System.Drawing.Size(75, 23);
            this.btn_Neasy.TabIndex = 7;
            this.btn_Neasy.Text = "163";
            this.btn_Neasy.UseVisualStyleBackColor = true;
            this.btn_Neasy.Click += new System.EventHandler(this.btn_Neasy_Click);
            // 
            // btn_Cnblogs
            // 
            this.btn_Cnblogs.Location = new System.Drawing.Point(256, 5);
            this.btn_Cnblogs.Name = "btn_Cnblogs";
            this.btn_Cnblogs.Size = new System.Drawing.Size(75, 23);
            this.btn_Cnblogs.TabIndex = 8;
            this.btn_Cnblogs.Text = "博客园";
            this.btn_Cnblogs.UseVisualStyleBackColor = true;
            this.btn_Cnblogs.Click += new System.EventHandler(this.btn_Cnblogs_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(615, 374);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 420);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_Cnblogs);
            this.Controls.Add(this.btn_Neasy);
            this.Controls.Add(this.btn_Baidu);
            this.Controls.Add(this.btn_sina);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_sina;
        private System.Windows.Forms.Button btn_Baidu;
        private System.Windows.Forms.Button btn_Neasy;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_Cnblogs;
    }
}
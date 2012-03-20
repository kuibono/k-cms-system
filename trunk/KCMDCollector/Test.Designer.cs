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

            Test.CheckForIllegalCrossThreadCalls = false;

            this.btn_sina = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_CreateBook = new System.Windows.Forms.Button();
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
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(665, 374);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // btn_CreateBook
            // 
            this.btn_CreateBook.Location = new System.Drawing.Point(103, 4);
            this.btn_CreateBook.Name = "btn_CreateBook";
            this.btn_CreateBook.Size = new System.Drawing.Size(75, 23);
            this.btn_CreateBook.TabIndex = 10;
            this.btn_CreateBook.Text = "生成书";
            this.btn_CreateBook.UseVisualStyleBackColor = true;
            this.btn_CreateBook.Click += new System.EventHandler(this.btn_CreateBook_Click);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 420);
            this.Controls.Add(this.btn_CreateBook);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_sina);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_sina;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_CreateBook;
    }
}
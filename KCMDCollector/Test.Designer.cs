﻿namespace KCMDCollector
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_CreateBook = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Tieba = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "下载封面";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Tieba
            // 
            this.Tieba.Location = new System.Drawing.Point(294, 5);
            this.Tieba.Name = "Tieba";
            this.Tieba.Size = new System.Drawing.Size(75, 23);
            this.Tieba.TabIndex = 12;
            this.Tieba.Text = "贴吧";
            this.Tieba.UseVisualStyleBackColor = true;
            this.Tieba.Click += new System.EventHandler(this.Tieba_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(391, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "xml-rpc";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(483, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 420);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Tieba);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Tieba;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
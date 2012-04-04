namespace SimpleCollector
{
    partial class FormCollecor
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
            this.comb_Rules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_CreateRule = new System.Windows.Forms.Button();
            this.lb_stats = new System.Windows.Forms.Label();
            this.btn_Start = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comb_Rules
            // 
            this.comb_Rules.FormattingEnabled = true;
            this.comb_Rules.Location = new System.Drawing.Point(59, 9);
            this.comb_Rules.Name = "comb_Rules";
            this.comb_Rules.Size = new System.Drawing.Size(121, 20);
            this.comb_Rules.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "规则：";
            // 
            // btn_CreateRule
            // 
            this.btn_CreateRule.Location = new System.Drawing.Point(196, 9);
            this.btn_CreateRule.Name = "btn_CreateRule";
            this.btn_CreateRule.Size = new System.Drawing.Size(39, 23);
            this.btn_CreateRule.TabIndex = 2;
            this.btn_CreateRule.Text = "新建";
            this.btn_CreateRule.UseVisualStyleBackColor = true;
            this.btn_CreateRule.Click += new System.EventHandler(this.btn_CreateRule_Click);
            // 
            // lb_stats
            // 
            this.lb_stats.AutoSize = true;
            this.lb_stats.Location = new System.Drawing.Point(12, 64);
            this.lb_stats.Name = "lb_stats";
            this.lb_stats.Size = new System.Drawing.Size(29, 12);
            this.lb_stats.TabIndex = 3;
            this.lb_stats.Text = "就绪";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(268, 8);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 4;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(268, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormCollecor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 266);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.lb_stats);
            this.Controls.Add(this.btn_CreateRule);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comb_Rules);
            this.Name = "FormCollecor";
            this.Text = "简单的采集器";
            this.Load += new System.EventHandler(this.FormCollecor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comb_Rules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_CreateRule;
        public System.Windows.Forms.Label lb_stats;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button button1;
    }
}
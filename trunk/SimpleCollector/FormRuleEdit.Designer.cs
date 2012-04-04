namespace SimpleCollector
{
    partial class FormRuleEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRuleEdit));
            this.wizardControl1 = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.step_SiteName = new WizardBase.IntermediateStep();
            this.com_Encoding = new System.Windows.Forms.ComboBox();
            this.txt_SiteName = new System.Windows.Forms.TextBox();
            this.step_ListPage = new WizardBase.IntermediateStep();
            this.txt_ListPageUrl = new System.Windows.Forms.TextBox();
            this.step_BookUrl = new WizardBase.IntermediateStep();
            this.txt_BookInfoUrl = new System.Windows.Forms.TextBox();
            this.txt_ListPageHtml = new System.Windows.Forms.RichTextBox();
            this.step_BookInfo = new WizardBase.IntermediateStep();
            this.txt_BookInfoRule = new System.Windows.Forms.RichTextBox();
            this.txt_BookInfoHtml = new System.Windows.Forms.RichTextBox();
            this.step_ChapterListUrl = new WizardBase.IntermediateStep();
            this.txt_ChapterListUrl = new System.Windows.Forms.RichTextBox();
            this.txt_BookInfos = new System.Windows.Forms.RichTextBox();
            this.step_ChapterTitleAndUrl = new WizardBase.IntermediateStep();
            this.txt_ChapterTitleAndUrl = new System.Windows.Forms.RichTextBox();
            this.txt_ChapterListHtml = new System.Windows.Forms.RichTextBox();
            this.step_ChapterContentRule = new WizardBase.IntermediateStep();
            this.txt_ChapterContentRule = new System.Windows.Forms.RichTextBox();
            this.txt_ChapterContentHtml = new System.Windows.Forms.RichTextBox();
            this.step_BookList = new WizardBase.IntermediateStep();
            this.txt_BookList = new System.Windows.Forms.RichTextBox();
            this.finishStep1 = new WizardBase.FinishStep();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Content = new System.Windows.Forms.RichTextBox();
            this.step_SiteName.SuspendLayout();
            this.step_ListPage.SuspendLayout();
            this.step_BookUrl.SuspendLayout();
            this.step_BookInfo.SuspendLayout();
            this.step_ChapterListUrl.SuspendLayout();
            this.step_ChapterTitleAndUrl.SuspendLayout();
            this.step_ChapterContentRule.SuspendLayout();
            this.step_BookList.SuspendLayout();
            this.finishStep1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonEnabled = true;
            this.wizardControl1.BackButtonText = "< 返回";
            this.wizardControl1.BackButtonVisible = true;
            this.wizardControl1.CancelButtonEnabled = true;
            this.wizardControl1.CancelButtonText = "取消";
            this.wizardControl1.CancelButtonVisible = true;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishButtonText = "完成";
            this.wizardControl1.HelpButtonEnabled = true;
            this.wizardControl1.HelpButtonText = "帮助";
            this.wizardControl1.HelpButtonVisible = true;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonEnabled = true;
            this.wizardControl1.NextButtonText = "下一步 >";
            this.wizardControl1.NextButtonVisible = true;
            this.wizardControl1.Size = new System.Drawing.Size(502, 284);
            this.wizardControl1.WizardSteps.Add(this.startStep1);
            this.wizardControl1.WizardSteps.Add(this.step_SiteName);
            this.wizardControl1.WizardSteps.Add(this.step_ListPage);
            this.wizardControl1.WizardSteps.Add(this.step_BookUrl);
            this.wizardControl1.WizardSteps.Add(this.step_BookInfo);
            this.wizardControl1.WizardSteps.Add(this.step_ChapterListUrl);
            this.wizardControl1.WizardSteps.Add(this.step_ChapterTitleAndUrl);
            this.wizardControl1.WizardSteps.Add(this.step_ChapterContentRule);
            this.wizardControl1.WizardSteps.Add(this.step_BookList);
            this.wizardControl1.WizardSteps.Add(this.finishStep1);
            this.wizardControl1.CancelButtonClick += new System.EventHandler(this.wizardControl1_CancelButtonClick);
            this.wizardControl1.FinishButtonClick += new System.EventHandler(this.wizardControl1_FinishButtonClick);
            this.wizardControl1.NextButtonClick += new WizardBase.WizardNextButtonClickEventHandler(this.wizardControl1_NextButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "请根据向导指引一步一步完成制作。";
            this.startStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startStep1.Title = "欢迎使用规则制作向导";
            this.startStep1.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // step_SiteName
            // 
            this.step_SiteName.BindingImage = ((System.Drawing.Image)(resources.GetObject("step_SiteName.BindingImage")));
            this.step_SiteName.Controls.Add(this.com_Encoding);
            this.step_SiteName.Controls.Add(this.txt_SiteName);
            this.step_SiteName.Name = "step_SiteName";
            this.step_SiteName.Subtitle = "请填写采集网站的名称：如“小说520”。网页编码请根据实际情况进行选择。不知道的话一会退回来选择也可以。";
            this.step_SiteName.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.step_SiteName.Title = "网站名称";
            this.step_SiteName.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // com_Encoding
            // 
            this.com_Encoding.FormattingEnabled = true;
            this.com_Encoding.Items.AddRange(new object[] {
            "UTF-8",
            "GB2312",
            "GBK",
            "BIG5"});
            this.com_Encoding.Location = new System.Drawing.Point(12, 76);
            this.com_Encoding.Name = "com_Encoding";
            this.com_Encoding.Size = new System.Drawing.Size(121, 20);
            this.com_Encoding.TabIndex = 2;
            this.com_Encoding.Text = "UTF-8";
            // 
            // txt_SiteName
            // 
            this.txt_SiteName.Location = new System.Drawing.Point(12, 111);
            this.txt_SiteName.Name = "txt_SiteName";
            this.txt_SiteName.Size = new System.Drawing.Size(478, 21);
            this.txt_SiteName.TabIndex = 1;
            // 
            // step_ListPage
            // 
            this.step_ListPage.BindingImage = ((System.Drawing.Image)(resources.GetObject("step_ListPage.BindingImage")));
            this.step_ListPage.Controls.Add(this.txt_ListPageUrl);
            this.step_ListPage.Name = "step_ListPage";
            this.step_ListPage.Subtitle = "需要采集的书籍列表页面的地址。";
            this.step_ListPage.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.step_ListPage.Title = "列表页地址";
            this.step_ListPage.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txt_ListPageUrl
            // 
            this.txt_ListPageUrl.Location = new System.Drawing.Point(12, 130);
            this.txt_ListPageUrl.Name = "txt_ListPageUrl";
            this.txt_ListPageUrl.Size = new System.Drawing.Size(478, 21);
            this.txt_ListPageUrl.TabIndex = 0;
            // 
            // step_BookUrl
            // 
            this.step_BookUrl.BindingImage = ((System.Drawing.Image)(resources.GetObject("step_BookUrl.BindingImage")));
            this.step_BookUrl.Controls.Add(this.txt_BookInfoUrl);
            this.step_BookUrl.Controls.Add(this.txt_ListPageHtml);
            this.step_BookUrl.Name = "step_BookUrl";
            this.step_BookUrl.Subtitle = "title,url必须！";
            this.step_BookUrl.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.step_BookUrl.Title = "书籍信息页地址规则";
            this.step_BookUrl.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txt_BookInfoUrl
            // 
            this.txt_BookInfoUrl.Location = new System.Drawing.Point(12, 194);
            this.txt_BookInfoUrl.Name = "txt_BookInfoUrl";
            this.txt_BookInfoUrl.Size = new System.Drawing.Size(478, 21);
            this.txt_BookInfoUrl.TabIndex = 1;
            // 
            // txt_ListPageHtml
            // 
            this.txt_ListPageHtml.Location = new System.Drawing.Point(12, 66);
            this.txt_ListPageHtml.Name = "txt_ListPageHtml";
            this.txt_ListPageHtml.Size = new System.Drawing.Size(478, 96);
            this.txt_ListPageHtml.TabIndex = 0;
            this.txt_ListPageHtml.Text = "";
            // 
            // step_BookInfo
            // 
            this.step_BookInfo.BindingImage = ((System.Drawing.Image)(resources.GetObject("step_BookInfo.BindingImage")));
            this.step_BookInfo.Controls.Add(this.txt_BookInfoRule);
            this.step_BookInfo.Controls.Add(this.txt_BookInfoHtml);
            this.step_BookInfo.Name = "step_BookInfo";
            this.step_BookInfo.Subtitle = "比较复杂的规则，需要哦后去title,author,class,length,intro几个字段，不能有缺失。";
            this.step_BookInfo.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.step_BookInfo.Title = "书籍信息规则";
            this.step_BookInfo.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txt_BookInfoRule
            // 
            this.txt_BookInfoRule.Location = new System.Drawing.Point(3, 167);
            this.txt_BookInfoRule.Name = "txt_BookInfoRule";
            this.txt_BookInfoRule.Size = new System.Drawing.Size(496, 74);
            this.txt_BookInfoRule.TabIndex = 1;
            this.txt_BookInfoRule.Text = "";
            // 
            // txt_BookInfoHtml
            // 
            this.txt_BookInfoHtml.Location = new System.Drawing.Point(3, 65);
            this.txt_BookInfoHtml.Name = "txt_BookInfoHtml";
            this.txt_BookInfoHtml.Size = new System.Drawing.Size(496, 96);
            this.txt_BookInfoHtml.TabIndex = 0;
            this.txt_BookInfoHtml.Text = "";
            // 
            // step_ChapterListUrl
            // 
            this.step_ChapterListUrl.BindingImage = ((System.Drawing.Image)(resources.GetObject("step_ChapterListUrl.BindingImage")));
            this.step_ChapterListUrl.Controls.Add(this.txt_ChapterListUrl);
            this.step_ChapterListUrl.Controls.Add(this.txt_BookInfos);
            this.step_ChapterListUrl.Name = "step_ChapterListUrl";
            this.step_ChapterListUrl.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.step_ChapterListUrl.Title = "章节列表页地址规则";
            this.step_ChapterListUrl.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txt_ChapterListUrl
            // 
            this.txt_ChapterListUrl.Location = new System.Drawing.Point(3, 166);
            this.txt_ChapterListUrl.Name = "txt_ChapterListUrl";
            this.txt_ChapterListUrl.Size = new System.Drawing.Size(496, 63);
            this.txt_ChapterListUrl.TabIndex = 1;
            this.txt_ChapterListUrl.Text = "";
            // 
            // txt_BookInfos
            // 
            this.txt_BookInfos.Location = new System.Drawing.Point(3, 64);
            this.txt_BookInfos.Name = "txt_BookInfos";
            this.txt_BookInfos.Size = new System.Drawing.Size(496, 96);
            this.txt_BookInfos.TabIndex = 0;
            this.txt_BookInfos.Text = "";
            // 
            // step_ChapterTitleAndUrl
            // 
            this.step_ChapterTitleAndUrl.BindingImage = ((System.Drawing.Image)(resources.GetObject("step_ChapterTitleAndUrl.BindingImage")));
            this.step_ChapterTitleAndUrl.Controls.Add(this.txt_ChapterTitleAndUrl);
            this.step_ChapterTitleAndUrl.Controls.Add(this.txt_ChapterListHtml);
            this.step_ChapterTitleAndUrl.Name = "step_ChapterTitleAndUrl";
            this.step_ChapterTitleAndUrl.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.step_ChapterTitleAndUrl.Title = "章节标题地址规则";
            this.step_ChapterTitleAndUrl.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txt_ChapterTitleAndUrl
            // 
            this.txt_ChapterTitleAndUrl.Location = new System.Drawing.Point(3, 168);
            this.txt_ChapterTitleAndUrl.Name = "txt_ChapterTitleAndUrl";
            this.txt_ChapterTitleAndUrl.Size = new System.Drawing.Size(496, 65);
            this.txt_ChapterTitleAndUrl.TabIndex = 1;
            this.txt_ChapterTitleAndUrl.Text = "";
            // 
            // txt_ChapterListHtml
            // 
            this.txt_ChapterListHtml.Location = new System.Drawing.Point(3, 66);
            this.txt_ChapterListHtml.Name = "txt_ChapterListHtml";
            this.txt_ChapterListHtml.Size = new System.Drawing.Size(496, 96);
            this.txt_ChapterListHtml.TabIndex = 0;
            this.txt_ChapterListHtml.Text = "";
            // 
            // step_ChapterContentRule
            // 
            this.step_ChapterContentRule.BindingImage = ((System.Drawing.Image)(resources.GetObject("step_ChapterContentRule.BindingImage")));
            this.step_ChapterContentRule.Controls.Add(this.txt_ChapterContentRule);
            this.step_ChapterContentRule.Controls.Add(this.txt_ChapterContentHtml);
            this.step_ChapterContentRule.Name = "step_ChapterContentRule";
            this.step_ChapterContentRule.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.step_ChapterContentRule.Title = "章节内容规则";
            this.step_ChapterContentRule.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txt_ChapterContentRule
            // 
            this.txt_ChapterContentRule.Location = new System.Drawing.Point(3, 166);
            this.txt_ChapterContentRule.Name = "txt_ChapterContentRule";
            this.txt_ChapterContentRule.Size = new System.Drawing.Size(496, 75);
            this.txt_ChapterContentRule.TabIndex = 1;
            this.txt_ChapterContentRule.Text = "";
            // 
            // txt_ChapterContentHtml
            // 
            this.txt_ChapterContentHtml.Location = new System.Drawing.Point(3, 64);
            this.txt_ChapterContentHtml.Name = "txt_ChapterContentHtml";
            this.txt_ChapterContentHtml.Size = new System.Drawing.Size(496, 96);
            this.txt_ChapterContentHtml.TabIndex = 0;
            this.txt_ChapterContentHtml.Text = "";
            // 
            // step_BookList
            // 
            this.step_BookList.BindingImage = ((System.Drawing.Image)(resources.GetObject("step_BookList.BindingImage")));
            this.step_BookList.Controls.Add(this.txt_BookList);
            this.step_BookList.Name = "step_BookList";
            this.step_BookList.Subtitle = "本功能是为了避免采集过多的无用书籍。";
            this.step_BookList.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.step_BookList.Title = "需要采集的书籍名称";
            this.step_BookList.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txt_BookList
            // 
            this.txt_BookList.Location = new System.Drawing.Point(3, 65);
            this.txt_BookList.Name = "txt_BookList";
            this.txt_BookList.Size = new System.Drawing.Size(496, 176);
            this.txt_BookList.TabIndex = 0;
            this.txt_BookList.Text = "*";
            // 
            // finishStep1
            // 
            this.finishStep1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BackgroundImage")));
            this.finishStep1.Controls.Add(this.label1);
            this.finishStep1.Controls.Add(this.txt_Content);
            this.finishStep1.Name = "finishStep1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "基本完成！";
            // 
            // txt_Content
            // 
            this.txt_Content.Location = new System.Drawing.Point(3, 76);
            this.txt_Content.Name = "txt_Content";
            this.txt_Content.Size = new System.Drawing.Size(496, 165);
            this.txt_Content.TabIndex = 0;
            this.txt_Content.Text = "";
            // 
            // FormRuleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 284);
            this.Controls.Add(this.wizardControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRuleEdit";
            this.Text = "建立规则";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FormRuleEdit_HelpButtonClicked);
            this.step_SiteName.ResumeLayout(false);
            this.step_SiteName.PerformLayout();
            this.step_ListPage.ResumeLayout(false);
            this.step_ListPage.PerformLayout();
            this.step_BookUrl.ResumeLayout(false);
            this.step_BookUrl.PerformLayout();
            this.step_BookInfo.ResumeLayout(false);
            this.step_ChapterListUrl.ResumeLayout(false);
            this.step_ChapterTitleAndUrl.ResumeLayout(false);
            this.step_ChapterContentRule.ResumeLayout(false);
            this.step_BookList.ResumeLayout(false);
            this.finishStep1.ResumeLayout(false);
            this.finishStep1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.WizardControl wizardControl1;
        private WizardBase.StartStep startStep1;
        private WizardBase.IntermediateStep step_SiteName;
        private WizardBase.FinishStep finishStep1;
        private System.Windows.Forms.TextBox txt_SiteName;
        private WizardBase.IntermediateStep step_ListPage;
        private System.Windows.Forms.TextBox txt_ListPageUrl;
        private System.Windows.Forms.ComboBox com_Encoding;
        private WizardBase.IntermediateStep step_BookUrl;
        private System.Windows.Forms.TextBox txt_BookInfoUrl;
        private System.Windows.Forms.RichTextBox txt_ListPageHtml;
        private WizardBase.IntermediateStep step_BookInfo;
        private System.Windows.Forms.RichTextBox txt_BookInfoRule;
        private System.Windows.Forms.RichTextBox txt_BookInfoHtml;
        private WizardBase.IntermediateStep step_ChapterListUrl;
        private System.Windows.Forms.RichTextBox txt_ChapterListUrl;
        private System.Windows.Forms.RichTextBox txt_BookInfos;
        private WizardBase.IntermediateStep step_ChapterTitleAndUrl;
        private System.Windows.Forms.RichTextBox txt_ChapterListHtml;
        private System.Windows.Forms.RichTextBox txt_ChapterTitleAndUrl;
        private WizardBase.IntermediateStep step_ChapterContentRule;
        private System.Windows.Forms.RichTextBox txt_ChapterContentHtml;
        private System.Windows.Forms.RichTextBox txt_ChapterContentRule;
        private System.Windows.Forms.RichTextBox txt_Content;
        private System.Windows.Forms.Label label1;
        private WizardBase.IntermediateStep step_BookList;
        private System.Windows.Forms.RichTextBox txt_BookList;

    }
}
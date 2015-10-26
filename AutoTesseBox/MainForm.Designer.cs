namespace AutoTesseBox
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.tbBold = new System.Windows.Forms.TextBox();
            this.lbBold = new System.Windows.Forms.Label();
            this.tbItalic = new System.Windows.Forms.TextBox();
            this.lbItalic = new System.Windows.Forms.Label();
            this.tbFontSize = new System.Windows.Forms.TextBox();
            this.lbSize = new System.Windows.Forms.Label();
            this.tbFontName = new System.Windows.Forms.TextBox();
            this.lbFontName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLanguage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTrainFont = new System.Windows.Forms.TextBox();
            this.lbBaseNum = new System.Windows.Forms.Label();
            this.numBaseNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTextFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOutDir = new System.Windows.Forms.TextBox();
            this.btnTextFileSelect = new System.Windows.Forms.Button();
            this.btnOutDirSelect = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseNum)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChange);
            this.groupBox1.Controls.Add(this.tbBold);
            this.groupBox1.Controls.Add(this.lbBold);
            this.groupBox1.Controls.Add(this.tbItalic);
            this.groupBox1.Controls.Add(this.lbItalic);
            this.groupBox1.Controls.Add(this.tbFontSize);
            this.groupBox1.Controls.Add(this.lbSize);
            this.groupBox1.Controls.Add(this.tbFontName);
            this.groupBox1.Controls.Add(this.lbFontName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(233, 16);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 2;
            this.btnChange.Text = "Browse...";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // tbBold
            // 
            this.tbBold.Location = new System.Drawing.Point(152, 70);
            this.tbBold.Name = "tbBold";
            this.tbBold.ReadOnly = true;
            this.tbBold.Size = new System.Drawing.Size(47, 19);
            this.tbBold.TabIndex = 9;
            // 
            // lbBold
            // 
            this.lbBold.AutoSize = true;
            this.lbBold.Location = new System.Drawing.Point(114, 73);
            this.lbBold.Name = "lbBold";
            this.lbBold.Size = new System.Drawing.Size(30, 12);
            this.lbBold.TabIndex = 8;
            this.lbBold.Text = "Bold:";
            // 
            // tbItalic
            // 
            this.tbItalic.Location = new System.Drawing.Point(46, 70);
            this.tbItalic.Name = "tbItalic";
            this.tbItalic.ReadOnly = true;
            this.tbItalic.Size = new System.Drawing.Size(47, 19);
            this.tbItalic.TabIndex = 7;
            // 
            // lbItalic
            // 
            this.lbItalic.AutoSize = true;
            this.lbItalic.Location = new System.Drawing.Point(10, 73);
            this.lbItalic.Name = "lbItalic";
            this.lbItalic.Size = new System.Drawing.Size(32, 12);
            this.lbItalic.TabIndex = 6;
            this.lbItalic.Text = "Italic:";
            // 
            // tbFontSize
            // 
            this.tbFontSize.Location = new System.Drawing.Point(46, 44);
            this.tbFontSize.Name = "tbFontSize";
            this.tbFontSize.ReadOnly = true;
            this.tbFontSize.Size = new System.Drawing.Size(100, 19);
            this.tbFontSize.TabIndex = 5;
            // 
            // lbSize
            // 
            this.lbSize.AutoSize = true;
            this.lbSize.Location = new System.Drawing.Point(12, 47);
            this.lbSize.Name = "lbSize";
            this.lbSize.Size = new System.Drawing.Size(28, 12);
            this.lbSize.TabIndex = 4;
            this.lbSize.Text = "Size:";
            // 
            // tbFontName
            // 
            this.tbFontName.Location = new System.Drawing.Point(46, 18);
            this.tbFontName.Name = "tbFontName";
            this.tbFontName.ReadOnly = true;
            this.tbFontName.Size = new System.Drawing.Size(181, 19);
            this.tbFontName.TabIndex = 3;
            // 
            // lbFontName
            // 
            this.lbFontName.AutoSize = true;
            this.lbFontName.Location = new System.Drawing.Point(6, 21);
            this.lbFontName.Name = "lbFontName";
            this.lbFontName.Size = new System.Drawing.Size(36, 12);
            this.lbFontName.TabIndex = 2;
            this.lbFontName.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOutDirSelect);
            this.groupBox2.Controls.Add(this.btnTextFileSelect);
            this.groupBox2.Controls.Add(this.tbOutDir);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbTextFile);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numBaseNum);
            this.groupBox2.Controls.Add(this.lbBaseNum);
            this.groupBox2.Controls.Add(this.tbTrainFont);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbLanguage);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 157);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Training File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Language:";
            // 
            // tbLanguage
            // 
            this.tbLanguage.Location = new System.Drawing.Point(67, 21);
            this.tbLanguage.Name = "tbLanguage";
            this.tbLanguage.Size = new System.Drawing.Size(79, 19);
            this.tbLanguage.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "Font:";
            // 
            // tbTrainFont
            // 
            this.tbTrainFont.Location = new System.Drawing.Point(67, 46);
            this.tbTrainFont.Name = "tbTrainFont";
            this.tbTrainFont.Size = new System.Drawing.Size(79, 19);
            this.tbTrainFont.TabIndex = 12;
            // 
            // lbBaseNum
            // 
            this.lbBaseNum.AutoSize = true;
            this.lbBaseNum.Location = new System.Drawing.Point(6, 74);
            this.lbBaseNum.Name = "lbBaseNum";
            this.lbBaseNum.Size = new System.Drawing.Size(58, 12);
            this.lbBaseNum.TabIndex = 13;
            this.lbBaseNum.Text = "Base num:";
            // 
            // numBaseNum
            // 
            this.numBaseNum.Location = new System.Drawing.Point(67, 72);
            this.numBaseNum.Name = "numBaseNum";
            this.numBaseNum.Size = new System.Drawing.Size(120, 19);
            this.numBaseNum.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "Text File:";
            // 
            // tbTextFile
            // 
            this.tbTextFile.Location = new System.Drawing.Point(67, 97);
            this.tbTextFile.Name = "tbTextFile";
            this.tbTextFile.ReadOnly = true;
            this.tbTextFile.Size = new System.Drawing.Size(372, 19);
            this.tbTextFile.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "Output Dir:";
            // 
            // tbOutDir
            // 
            this.tbOutDir.Location = new System.Drawing.Point(67, 122);
            this.tbOutDir.Name = "tbOutDir";
            this.tbOutDir.ReadOnly = true;
            this.tbOutDir.Size = new System.Drawing.Size(372, 19);
            this.tbOutDir.TabIndex = 18;
            // 
            // btnTextFileSelect
            // 
            this.btnTextFileSelect.Location = new System.Drawing.Point(446, 95);
            this.btnTextFileSelect.Name = "btnTextFileSelect";
            this.btnTextFileSelect.Size = new System.Drawing.Size(75, 23);
            this.btnTextFileSelect.TabIndex = 10;
            this.btnTextFileSelect.Text = "Browse...";
            this.btnTextFileSelect.UseVisualStyleBackColor = true;
            this.btnTextFileSelect.Click += new System.EventHandler(this.btnTextFileSelect_Click);
            // 
            // btnOutDirSelect
            // 
            this.btnOutDirSelect.Location = new System.Drawing.Point(446, 120);
            this.btnOutDirSelect.Name = "btnOutDirSelect";
            this.btnOutDirSelect.Size = new System.Drawing.Size(75, 23);
            this.btnOutDirSelect.TabIndex = 19;
            this.btnOutDirSelect.Text = "Browse...";
            this.btnOutDirSelect.UseVisualStyleBackColor = true;
            this.btnOutDirSelect.Click += new System.EventHandler(this.btnOutDirSelect_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(480, 333);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 368);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "AutoTesseBox.NET";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbFontSize;
        private System.Windows.Forms.Label lbSize;
        private System.Windows.Forms.TextBox tbFontName;
        private System.Windows.Forms.Label lbFontName;
        private System.Windows.Forms.Label lbItalic;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox tbBold;
        private System.Windows.Forms.Label lbBold;
        private System.Windows.Forms.TextBox tbItalic;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOutDirSelect;
        private System.Windows.Forms.Button btnTextFileSelect;
        private System.Windows.Forms.TextBox tbOutDir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTextFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numBaseNum;
        private System.Windows.Forms.Label lbBaseNum;
        private System.Windows.Forms.TextBox tbTrainFont;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLanguage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRun;
    }
}


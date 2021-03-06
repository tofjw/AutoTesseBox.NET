﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTesseBox
{
    public partial class MainForm : Form
    {
        FontDialog fd;
        Font font;

        void UpdateFont()
        {
            tbFontName.Text = font.Name;
            tbFontSize.Text = font.Size.ToString();
            tbItalic.Text = font.Italic ? "True" : "False";
            tbBold.Text = font.Bold ? "True" : "False";
        }

        public MainForm()
        {
            InitializeComponent();

            fd = new FontDialog();
            font = fd.Font;
            UpdateFont();

            tbOutDir.Text = System.IO.Directory.GetCurrentDirectory();

            tbBatch.KeyPress += tbBatch_KeyPress;
        }

        void tbBatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 1)
            {
                tbBatch.SelectAll();
                e.Handled = true;
            }
        }

        public Font CurrentFont {
            get {
                return font;
            }
        }

        public string TrainFont
        {
            get
            {
                return tbTrainFont.Text;
            }
        }

        public string Lang
        {
            get
            {
                return tbLanguage.Text;
            }
        }

        public int BaseNum {
            get{
                return (int)numBaseNum.Value;
            }
        }

        public string TextFile
        {
            get
            {
                return tbTextFile.Text;
            }
        }

        public string OutDir
        {
            get
            {
                return tbOutDir.Text;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            fd.ShowEffects = false;

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                font = fd.Font;
                UpdateFont();
            }
        }

        private void btnTextFileSelect_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.Multiselect = false;

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbTextFile.Text = fd.FileName;
            }
        }

        private void btnOutDirSelect_Click(object sender, EventArgs e)
        {
            var dd = new FolderBrowserDialog();

            if (dd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbOutDir.Text = dd.SelectedPath;
            }
        }

        string QuoteIfNeeded(string s)
        {
            if (s.Contains(" "))
                return "\"" + s + "\"";

            return s;
        }
        string CreateCommandLine(string outDir, string textFile, Font font, string lang, string trainFont, int baseNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("AutoTesseBox");
            sb.Append(" /batch");
            sb.Append(" /fontname:").Append(QuoteIfNeeded(font.Name));
            sb.Append(" /fontsize:").Append(font.Size.ToString());
            if (font.Italic) sb.Append(" /italic");
            if (font.Bold) sb.Append(" /bold");
            sb.Append(" /textfile:").Append(QuoteIfNeeded(textFile));
            sb.Append(" /outdir:").Append(QuoteIfNeeded(outDir));
            sb.Append(" /tesselang:").Append(QuoteIfNeeded(lang));
            sb.Append(" /tessefont:").Append(QuoteIfNeeded(trainFont));

            return sb.ToString();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (Lang.Length == 0)
            {
                MessageBox.Show("Lang is not specified.");
                return;
            }
            if (TrainFont.Length == 0)
            {
                MessageBox.Show("TrainFont is not specified.");
                return;
            }
            if (TextFile.Length == 0)
            {
                MessageBox.Show("TextFile is not specified.");
                return;
            }
            if (OutDir.Length == 0)
            {
                MessageBox.Show("OutDir is not specified.");
                return;
            }

            var curCursor = Cursor.Current;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Engine.Process(OutDir, TextFile, CurrentFont, Lang, TrainFont, BaseNum);
                MessageBox.Show("Done.");

                string cmdLine = CreateCommandLine(OutDir, TextFile, CurrentFont, Lang, TrainFont, BaseNum);
                tbBatch.AppendText(cmdLine + "\r\n");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Cursor.Current = curCursor;
            }
        }

        new public void Dispose()
        {
            fd.Dispose();
            Parent.Dispose();
        }
    }
}

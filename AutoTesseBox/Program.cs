using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AutoTesseBox
{
    static class Program
    {
        static void usage()
        {
            Console.WriteLine("usage: AutoTessBox [/batch] [other options]");
            Console.WriteLine("  /batch                run as batch");
            Console.WriteLine("options (rendering)");
            Console.WriteLine("  /fontname:FONT_NAME   font name to draw text");
            Console.WriteLine("  /fontsize:FONT_SIZE   font size to draw text: (default: 12.0)");
            Console.WriteLine("  /italic               use italic font");
            Console.WriteLine("  /bold                 use bold font");
            Console.WriteLine("options (intput and output)");
            Console.WriteLine("  /textfile:FILE_NAME   intput text file");
            Console.WriteLine("  /outdir:DIR_NAME      font name for tesserach (default: current directory)");
            Console.WriteLine("  /tesselang:LANG       language name for tesseract (default: eng)");
            Console.WriteLine("  /tessefont:FONT_NAME  font name for tesseract");
            Console.WriteLine("  /basenun:NUM          base number for tiff/box filename (deafult: 0)");
        }
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isBatch = false;

            // font options
            string fontName = null;
            double fontSize = 12.0;
            bool isItalic = false;
            bool isBold = false;

            // output options
            string tesseLang = "eng";
            string tesseFont = null;
            int baseNum = 0;
            string textfile = null;
            string outdir = ".";

//            foreach(string arg in Environment.GetCommandLineArgs())
            string[] args = Environment.GetCommandLineArgs();
            for (int i=1; i< args.Length; i++)
            {
                string arg = args[i];

                if (arg == "/batch")
                {
                    isBatch = true;
                    Console.OpenStandardOutput();

                }
                else if (arg.StartsWith("/fontname:"))
                    fontName = arg.Substring(10);
                else if (arg.StartsWith("/fontsize:"))
                {
                    try
                    {
                        fontSize = double.Parse(arg.Substring(10));
                    }
                    catch
                    {
                        Console.WriteLine("bad fontsize option: " + arg);
                        return;
                    }
                }
                else if (arg == "/italic")
                    isItalic = true;
                else if (arg == "/bold")
                    isBold = true;
                else if (arg.StartsWith("/tesselang:"))
                    tesseLang = arg.Substring(11);
                else if (arg.StartsWith("/tessefont:"))
                    tesseFont = arg.Substring(11);
                else if (arg.StartsWith("/basenum:"))
                {
                    try
                    {
                        baseNum = int.Parse(arg.Substring(9));
                    }
                    catch
                    {
                        Console.WriteLine("bad basenum option: " + arg);
                        return;
                    }
                }
                else if (arg.StartsWith("/textfile:"))
                    textfile = arg.Substring(10);
                else if (arg.StartsWith("/outdir:"))
                    outdir = arg.Substring(8);
                else
                {
                    Console.WriteLine("bad option: " + arg);
                    usage();
                    Environment.ExitCode = 1;
                    return;
                }
            }

            if (isBatch)
            {
                Environment.ExitCode = RunBatch(fontName, fontSize, isItalic, isBold, textfile, tesseLang, tesseFont, baseNum, outdir);
            }
            else
            {
                Console.SetWindowSize(30, 5);
                Console.WriteLine("Running in GUI mode");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var form = new MainForm();
                Application.Run(form);
            }
        }

        static int RunBatch(
            string fontName,
            double fontSize,
            bool isItalic,
            bool isBold,
            string textFile,
            string tesseLang,
            string tesseFont,
            int baseNum,
            string outDir)
        {
            if (fontName == null
                || fontSize == 0.0
                || textFile == null
                || tesseLang == null
                || tesseFont == null
                || outDir == null)
            {
                usage();
                return 1;
            }

            try
            {
                FontStyle fs = 0;
                if (isItalic)
                    fs |= FontStyle.Italic;
                if (isBold)
                    fs |= FontStyle.Bold;

                Font font = new Font(fontName, (float)fontSize, fs);
                Engine.Process(outDir, textFile, font, tesseLang, tesseFont, baseNum);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 1;
            }

            return 0;
        }
    }
}

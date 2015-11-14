using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

namespace AutoTesseBox
{
    class BoxData
    {
        public string Str;
        public Rectangle Rect;

        public BoxData(string s, Rectangle r)
        {
            Str = s;
            Rect = r;
        }
    }

    class LineData
    {
        public Bitmap bitmap;
        public List<BoxData> boxes;

        public LineData(Bitmap bm, IEnumerable<BoxData> b)
        {
            bitmap = bm;
            boxes = new List<BoxData>(b);
        }
    }

    class Engine
    {
        static Rectangle GetBox(Bitmap bm, int x1, int y1, int x2, int y2)
        {
            BitmapData bmpData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height),
                                        ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes  = Math.Abs(bmpData.Stride) * bm.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);            

            int sx = x1;
            int sy = y1;
            int ex = x2;
            int ey = y2;

            for (int x = x1; x <= x2; x++)
            {
                bool found = false;

                for (int y = y1; y <= y2; y++)
                {
                    int index = bmpData.Stride * y + 3 * x;
                    int r = rgbValues[index];
                    int g = rgbValues[index + 1];
                    int b = rgbValues[index + 2];

                    // 0.3R＋0.6G＋0.1B
                    double c = 0.3 * r + 0.6 * g + 0.1 * b;
                    if (c < CharacterMetrics.CThreshold * 255)
                    {
                        sx = x;
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }

            for (int x = x2; x >= x1; x--)
            {
                bool found = false;

                for (int y = y1; y <= y2; y++)
                {
                    int index = bmpData.Stride * y + 3 * x;
                    int r = rgbValues[index];
                    int g = rgbValues[index + 1];
                    int b = rgbValues[index + 2];

                    // 0.3R＋0.6G＋0.1B
                    double c = 0.3 * r + 0.6 * g + 0.1 * b;
                    if (c < CharacterMetrics.CThreshold * 255)
                    {
                        ex = x;
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }

            for (int y = y1; y <= y2; y++)
            {
                bool found = false;

                for (int x = x1; x <= x2; x++)
                {
                    int index = bmpData.Stride * y + 3 * x;
                    int r = rgbValues[index];
                    int g = rgbValues[index + 1];
                    int b = rgbValues[index + 2];

                    // 0.3R＋0.6G＋0.1B
                    double c = 0.3 * r + 0.6 * g + 0.1 * b;
                    if (c < CharacterMetrics.CThreshold * 255)
                    {
                        sy = y;
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }

            for (int y = y2; y >= y1; y--)
            {
                bool found = false;

                for (int x = x1; x <= x2; x++)
                {
                    int index = bmpData.Stride * y + 3 * x;
                    int r = rgbValues[index];
                    int g = rgbValues[index + 1];
                    int b = rgbValues[index + 2];

                    // 0.3R＋0.6G＋0.1B
                    double c = 0.3 * r + 0.6 * g + 0.1 * b;
                    if (c < CharacterMetrics.CThreshold * 255)
                    {
                        ey = y;
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }

            bm.UnlockBits(bmpData);  

            return new Rectangle(sx, sy, ex - sx + 1, ey - sy + 1);
        }

        static LineData CreateLineData(Font font, string s)
        {
            var metrics = new List<CharacterMetrics>();
            var chars = new List<string>();

            SizeF strSize;

            using (var bm = new Bitmap(100, 100))
            using (var g = Graphics.FromImage(bm))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                for (int i = 0; i < s.Length; i++)
                {
                    string cs = s.Substring(i, 1);
                    {
                        g.FillRectangle(Brushes.White, 0, 0, bm.Width, bm.Height);
                        g.DrawString(cs, font, Brushes.Black, new PointF(0, 0));
                        g.Flush();

                        var m = new CharacterMetrics(bm);
                        if (m.Width > 0)
                        {
                            chars.Add(cs);
                            metrics.Add(m);
                        }
                    }
                }

                strSize = g.MeasureString(s, font);
            }

            var masterVFeature = new List<CharacterMetrics.Feature>();
            List<int> labels = new List<int>();
            for (int i = 0; i < metrics.Count; i++)
            {
                for (int x = 0; x < metrics[i].Width; x++)
                {
                    masterVFeature.Add(metrics[i].GetVerticalFeature(x));
                    labels.Add(i);
                }
            }

            CharacterMetrics strMetrics;
            int strWidth = (int)strSize.Width;
            int strHeight = (int)strSize.Height * 2;

            var strBm = new Bitmap(strWidth, strHeight);

            if (chars.Count == 0)
            {
                return new LineData(strBm, new List<BoxData>());
            }

            using (var g = Graphics.FromImage(strBm))
            {
                g.FillRectangle(Brushes.White, 0, 0, strBm.Width, strBm.Height);
                g.DrawString(s, font, Brushes.Black, new PointF(0, 0));
                g.Flush();

                strMetrics = new CharacterMetrics(strBm);
            }

            // match
            int[,] cost = new int[strMetrics.Width, masterVFeature.Count];

            for (int i = 0; i < strMetrics.Width; i++)
            {
                for (int j = 0; j < masterVFeature.Count; j++)
                    cost[i, j] = 10000000;
            }

            var queue = new Queue<KeyValuePair<int, int>>();

            cost[0, 0] = 0;

            queue.Enqueue(new KeyValuePair<int, int>(0, 0));

            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                int baseCost = cost[p.Key, p.Value];

                // strMetrics++, masterVCount+=0
                {
                    int si = p.Key + 1;
                    int mi = p.Value;
                    if (si < strMetrics.Width)
                    {
                        int diff =
                            strMetrics.GetVerticalFeature(si).f1
                            + strMetrics.GetVerticalFeature(si).f2
                            + strMetrics.GetVerticalFeature(si).f3
                            + strMetrics.GetVerticalFeature(si).f4;

                        int c = baseCost + 0 + diff * 10;

                        if (c < cost[si, mi])
                        {
                            cost[si, mi] = c;
                            queue.Enqueue(new KeyValuePair<int, int>(si, mi));
                        }
                    }
                }
                // strMetrics+=0, masterVCount++
                {
                    int si = p.Key;
                    int mi = p.Value + 1;
                    if (mi < masterVFeature.Count)
                    {
                        int diff = masterVFeature[mi].f1
                            + masterVFeature[mi].f2
                            + masterVFeature[mi].f3
                            + masterVFeature[mi].f4;

                        int c = baseCost + 0 + diff * 10;

                        if (c < cost[si, mi])
                        {
                            cost[si, mi] = c;
                            queue.Enqueue(new KeyValuePair<int, int>(si, mi));
                        }
                    }
                }
                // strMetrics++, masterVCount++
                {
                    int si = p.Key + 1;
                    int mi = p.Value + 1;
                    if (si < strMetrics.Width && mi < masterVFeature.Count)
                    {
                        int diff = Math.Abs(masterVFeature[mi].f1 - strMetrics.GetVerticalFeature(si).f1)
                            + Math.Abs(masterVFeature[mi].f2 - strMetrics.GetVerticalFeature(si).f2)
                            + Math.Abs(masterVFeature[mi].f3 - strMetrics.GetVerticalFeature(si).f3)
                            + Math.Abs(masterVFeature[mi].f4 - strMetrics.GetVerticalFeature(si).f4);

                        int c = baseCost + 10 * diff;
                        if (c < cost[si, mi])
                        {
                            cost[si, mi] = c;
                            queue.Enqueue(new KeyValuePair<int, int>(si, mi));
                        }
                    }
                }
            }

            // find lowest cost
            int lcIndex = 0;
            int minValue = cost[strMetrics.Width - 1, lcIndex];
            for (int i = 0; i < masterVFeature.Count; i++)
            {
                int v = cost[strMetrics.Width - 1, i];
                if (minValue > v)
                {
                    minValue = v;
                    lcIndex = i;
                }
            }

            // traverse
            var matchResult = new List<int>();
            for (int i = 0; i < strMetrics.Width; i++)
            {
                matchResult.Add(lcIndex);
            }

            for (int i = strMetrics.Width - 2; i >= 0; i--)
            {
                while (lcIndex > 0)
                {
                    if (lcIndex > 0 && cost[i, lcIndex - 1] <= minValue)
                    {
                        minValue = cost[i, lcIndex - 1];
                        lcIndex--;
                        continue;
                    }

                    break;
                }

                matchResult[i] = lcIndex;
            }

            var matchLabel = new List<int>(matchResult);
            for (int i = 0; i < matchResult.Count; i++)
            {
                matchLabel[i] = labels[matchResult[i]];
            }

            var strBox = GetBox(strBm, 0, 0, strBm.Width - 1, strBm.Height - 1);
            var boxes = new Dictionary<int, Rectangle>();

            for (int i = 0; i < chars.Count; i++)
            {
                int sx = -1;
                int ex = -1;

                for (int j = 0; j < matchLabel.Count; j++)
                {
                    if (matchLabel[j] == i)
                    {
                        if (sx < 0)
                            sx = j;

                        ex = j;
                    }
                    else
                    {
                        if (ex >= 0)
                            break;
                    }
                }

                if (sx >= 0)
                {
                    int offsetX = strBox.Left;
                    sx += offsetX;
                    ex += offsetX;

                    var charBox = GetBox(strBm, sx, 0, ex, strBm.Height - 1);
                    boxes.Add(i, charBox);
                }
            }

            var bd = new List<BoxData>();
            for (int i = 0; i < chars.Count; i++)
            {
                if (boxes.ContainsKey(i))
                {
                    string key = chars[i];

                    // merged?
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (boxes.ContainsKey(j))
                            break;

                        key = chars[j] + key;
                    }

                    bd.Add(new BoxData(key, boxes[i]));
                }
            }

            return new LineData(strBm, bd);
        }

        public static void Process(string outDir, string textFile, Font font, string lang, string trainFont, int baseNum)
        {
            int seq = baseNum;

            using(var file = System.IO.File.OpenText(textFile))
            {
                var lines = new List<string>();
                const int maxLineLen = 60;
                const int maxLines = 60;

                string line = file.ReadLine();
                while (line != null)
                {
                    while (line.Length > 0)
                    {
                        if (line.Length > maxLineLen)
                        {
                            string head = line.Substring(0, maxLineLen);
                            lines.Add(head);

                            line = line.Substring(maxLineLen);
                        }
                        else
                        {
                            lines.Add(line);
                            line = "";
                        }
                    }

                    if (lines.Count > maxLines)
                    {
                        ProcessLines(outDir, lines, font, lang, trainFont, seq++);
                        lines.Clear();
                    }

                    line = file.ReadLine();
                }

                if (lines.Count > 0)
                {
                    ProcessLines(outDir, lines, font, lang, trainFont, seq++);
                }
            }
        }

        static void ProcessLines(string outDir, List<string> lines, Font font, string lang, string trainFont, int seq)
        {
            Console.WriteLine("test");

            var lds = new List<LineData>();
            foreach(var line in lines)
            {
                lds.Add(CreateLineData(font, line));
            }

            int bmHeight = lds.Sum(x => x.bitmap.Height);
            int bmWidth = lds.Max(x => x.bitmap.Width);
            string baseName = System.IO.Path.Combine(outDir, String.Join(".", lang, trainFont, "exp" + seq.ToString()));

            using(var resultBm = new Bitmap(bmWidth, bmHeight))
            using (Graphics g = Graphics.FromImage(resultBm))
            {
                g.FillRectangle(Brushes.White, 0, 0, resultBm.Width, resultBm.Height);

                int posY = 0;
                foreach (var ld in lds)
                {
                    g.DrawImage(ld.bitmap, 0, posY);
                    posY += ld.bitmap.Height;
                }

                resultBm.Save(baseName + ".tif", System.Drawing.Imaging.ImageFormat.Tiff);
            }

            using (var f = System.IO.File.OpenWrite(baseName + ".box"))
            {
                f.SetLength(0);

                var buf = new List<byte>();
                var enc = new System.Text.UTF8Encoding();

                int posY = bmHeight;

                foreach (var ld in lds)
                {
                    foreach (var box in ld.boxes)
                    {
                        buf.AddRange(enc.GetBytes(box.Str));
                        buf.AddRange(enc.GetBytes(" "));

                        int bottom = posY;

                        int x1 = box.Rect.X;
                        int x2 = box.Rect.X + box.Rect.Width;
                        int y1 = bottom - (box.Rect.Y + box.Rect.Height);
                        int y2 = y1 + box.Rect.Height;

                        string coord = string.Join(" ",
                            x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString(), 0);
                        buf.AddRange(enc.GetBytes(coord));
                        buf.Add(0x0a);
                        f.Write(buf.ToArray(), 0, buf.Count);

                        buf.Clear();
                    }

                    posY -= ld.bitmap.Height;
                }
            }

            foreach(var ld in lds)
            {
                ld.bitmap.Dispose();
            }
        }
    }
}

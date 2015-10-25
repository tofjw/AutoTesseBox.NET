using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

namespace AutoBox
{
    class CharacterMetrics
    {
        public const double CThreshold = 0.97;

        public class Feature
        {
            public int f1;
            public int f2;
            public int f3;
            public int f4;

            public Feature(int f1, int f2, int f3, int f4)
            {
                this.f1 = f1;
                this.f2 = f2;
                this.f3 = f3;
                this.f4 = f4;
            }

            public Feature()
            {
                this.f1 = 0;
                this.f2 = 0;
                this.f3 = 0;
                this.f4 = 0;
            }
        }

        private Feature[] vf;

        public int Width {
            get {
                return vf.Length;
            }
        }

        public int Height
        {
            get;
            set;
        }

        public Feature GetVerticalFeature(int x)
        {
            return vf[x];
        }

        public int GetHorizontalFeature(int y)
        {
            throw new NotImplementedException();
            // return hCount[y];
        }

        static Feature[] ComputeVerticalFeature(Bitmap bm)
        {
            BitmapData bmpData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height),
                                        ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bm.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            
            var ret = new List<Feature>();

            bool inChar = false;
            int firstPoint = 0;
            int lastPoint = 0;

            for (int x = 0; x < bm.Width; x++)
            {
                int count = 0;
                int minY = 0;
                int maxY = 0;

                for (int y = 0; y < bm.Height; y++)
                {
                    int index = bmpData.Stride * y + 3 * x;
                    int r = rgbValues[index];
                    int g = rgbValues[index + 1];
                    int b = rgbValues[index + 2];

                    // 0.3R＋0.6G＋0.1B
                    double c = 0.3 * r + 0.6 * g + 0.1 * b;
                    if (c < CThreshold * 255)
                    {
                        minY = Math.Min(minY, y);
                        maxY = y;
                        count += 1;
                    }
                }

                if (count > 0)
                {
                    if (!inChar)
                    {
                        inChar = true;
                        firstPoint = x;
                    }

                    lastPoint = x;
                }

                if (inChar)
                {
                    ret.Add(new Feature(count, 0 * minY, 0 * maxY, 1 * (maxY - minY)));
                }
            }

            int width = lastPoint - firstPoint + 1;
            if (width < ret.Count)
                ret.RemoveRange(width, ret.Count - width);

            bm.UnlockBits(bmpData);  

            return ret.ToArray();
        }

        public CharacterMetrics(Bitmap bm)
        {
            vf = ComputeVerticalFeature(bm);
        }
    }
}

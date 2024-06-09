using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace KlxPiaoAPI
{
    public class 图像
    {
        //参考自DYLIKE

        /// <summary>
        /// 替换图像中所有指定颜色的像素。
        /// </summary>
        /// <param name="要应用替换颜色的图像">要应用颜色替换的图像。</param>
        /// <param name="选定的颜色">要被替换的颜色。</param>
        /// <param name="替换成颜色">替换成的颜色。</param>
        /// <param name="色彩容差百分比">色彩容差百分比，用于判断颜色接近程度的阈值（0到1之间）。</param>
        /// <param name="处理透明通道">是否处理透明通道。</param>
        public static void 替换颜色(ref Bitmap 要应用替换颜色的图像, Color 选定的颜色, Color 替换成颜色, double 色彩容差百分比 = 0, bool 处理透明通道 = false)
        {
            try
            {
                if (色彩容差百分比 < 0) 色彩容差百分比 = 0;
                if (色彩容差百分比 > 1) 色彩容差百分比 = 1;

                BitmapData BmpData1 = 要应用替换颜色的图像.LockBits(new Rectangle(0, 0, 要应用替换颜色的图像.Width, 要应用替换颜色的图像.Height), ImageLockMode.WriteOnly, 要应用替换颜色的图像.PixelFormat);
                byte[] Bts = new byte[BmpData1.Stride * BmpData1.Height];
                Marshal.Copy(BmpData1.Scan0, Bts, 0, Bts.Length);
                int WH = BmpData1.Stride / 要应用替换颜色的图像.Width;
                Color Cl1 = 选定的颜色;
                int B, C, D;

                try
                {
                    for (int I = 0; I < Bts.Length - WH; I += WH)
                    {
                        try
                        {
                            if (要应用替换颜色的图像.PixelFormat == PixelFormat.Format32bppArgb && Bts[I + 3] == 0) continue;

                            B = Bts[I + 2];
                            C = Bts[I + 1];
                            D = Bts[I];
                            Color Cl2 = Color.FromArgb(B, C, D);

                            if (Cl2.R == Cl1.R && Cl2.G == Cl1.G && Cl2.B == Cl1.B)
                            {
                                Bts[I + 2] = 替换成颜色.R;
                                Bts[I + 1] = 替换成颜色.G;
                                Bts[I] = 替换成颜色.B;
                                if (处理透明通道)
                                {
                                    Bts[I + 3] = 替换成颜色.A;
                                }
                            }
                            else if (IsNearColor(Cl1, Cl2, 色彩容差百分比))
                            {
                                Bts[I + 2] = 替换成颜色.R;
                                Bts[I + 1] = 替换成颜色.G;
                                Bts[I] = 替换成颜色.B;
                                if (处理透明通道)
                                {
                                    Bts[I + 3] = 替换成颜色.A;
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                catch { }

                Marshal.Copy(Bts, 0, BmpData1.Scan0, Bts.Length);
                要应用替换颜色的图像.UnlockBits(BmpData1);
            }
            catch { }
        }

        /// <summary>
        /// 判断两个颜色是否接近。
        /// </summary>
        /// <param name="cl1">第一个颜色。</param>
        /// <param name="cl2">第二个颜色。</param>
        /// <param name="per">容差百分比。</param>
        /// <returns>如果颜色接近则返回true，否则返回false。</returns>
        private static bool IsNearColor(Color cl1, Color cl2, double per)
        {
            try
            {
                if ((cl1.R + cl1.R * per) > cl2.R && (cl1.G + cl1.G * per) > cl2.G && (cl1.B + cl1.B * per) > cl2.B &&
                    (cl1.R - cl1.R * per) < cl2.R && (cl1.G - cl1.G * per) < cl2.G && (cl1.B - cl1.B * per) < cl2.B)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
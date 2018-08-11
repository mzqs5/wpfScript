using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace wpfclx
{
    public class BitmapHelper
    {
        public static Bitmap ConvertToFormat(Bitmap image, PixelFormat format)
        {
            try
            {
                var b = new Bitmap(image.Width, image.Height, format);
                b.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (Graphics g = Graphics.FromImage(b))
                {
                    // 用白色清空 
                    g.Clear(Color.White);

                    // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // 指定高质量、低速度呈现。 
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
                    g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

                }
                return b;
            }
            catch (Exception e)
            {
                Log.log("ConvertToFormat", e.Message);
                return image;
            }
        }
        private static int deviationX = 8;//窗口左偏移量
        private static int deviationY = 32;//窗口上偏移量
        public static Bitmap ConvertToFormat(Bitmap image, PixelFormat format, XRECT r)
        {
            var b = new Bitmap(r.Right - r.Left, r.Bottom - r.Top, format);
            b.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics g = Graphics.FromImage(b))
            {
                // 用白色清空 
                g.Clear(Color.White);

                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 指定高质量、低速度呈现。 
                g.SmoothingMode = SmoothingMode.HighQuality;
                // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
                g.DrawImage(image, new Rectangle(0, 0, r.Right - r.Left, r.Bottom - r.Top), new Rectangle(r.Left + deviationX, r.Top + deviationY, r.Right - r.Left, r.Bottom - r.Top), GraphicsUnit.Pixel);

            }
            return b;
        }

        public static string BitmapByteStr(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return System.Text.Encoding.Default.GetString(data);
            }
        }


        public static Bitmap ByteStrToBitmap(string str)
        {
            MemoryStream stream = null;
            try
            {
                byte[] Bytes = System.Text.Encoding.Default.GetBytes(str);
                stream = new MemoryStream(Bytes);
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        /// 颜色替换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="color"></param>
        public static void ColorReplace(Bitmap source, Color color)
        {
            PointBitmap bitmap = new PointBitmap(source);
            bitmap.LockBits();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var pc = bitmap.GetPixel(i, j);
                    if (Math.Abs(pc.R - color.R) < 100 && Math.Abs(pc.G - color.G) < 20 && Math.Abs(pc.B - color.B) < 20)
                        bitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    else
                        bitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                }
            }
            bitmap.UnlockBits();
        }

        /// <summary>
        /// 增强对比度
        /// </summary>
        /// <param name="source"></param>
        /// <param name="color"></param>
        public static void EnhanceContrast(Bitmap source, int degree = 50)
        {
            PointBitmap bitmap = new PointBitmap(source);
            bitmap.LockBits();
            Color curColor;
            int grayR, grayG, grayB;
            double Deg = (100.0 + degree) / 100.0;
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    curColor = bitmap.GetPixel(i, j);
                    grayR = Convert.ToInt32((((curColor.R / 255.0 - 0.5) * Deg + 0.5)) * 255);
                    grayG = Convert.ToInt32((((curColor.G / 255.0 - 0.5) * Deg + 0.5)) * 255);
                    grayB = Convert.ToInt32((((curColor.B / 255.0 - 0.5) * Deg + 0.5)) * 255);
                    bitmap.SetPixel(i, j, Color.FromArgb(setGray(grayR), setGray(grayG), setGray(grayB)));
                }
            }
            bitmap.UnlockBits();
        }


        private static int setGray(int gray)
        {
            if (gray < 0)
                return 0;
            else if (gray > 255)
                return 255;
            else
                return gray;
        }

        #region [颜色：16进制转成RGB]
        /// <summary>
        /// [颜色：16进制转成RGB]
        /// </summary>
        /// <param name="strColor">设置16进制颜色 [返回RGB]</param>
        /// <returns></returns>
        public static Color colorHx16toRGB(string strHxColor)
        {
            try
            {
                if (strHxColor.Length == 0)
                {//如果为空
                    return Color.FromArgb(0, 0, 0);//设为黑色
                }
                else
                {//转换颜色
                    return Color.FromArgb(System.Int32.Parse(strHxColor.Substring(1, 2),
System.Globalization.NumberStyles.AllowHexSpecifier),
System.Int32.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier),
System.Int32.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
                }
            }
            catch
            {//设为黑色
                return Color.FromArgb(0, 0, 0);
            }
        }
        #endregion

        #region [颜色：RGB转成16进制]
        /// <summary>
        /// [颜色：RGB转成16进制]
        /// </summary>
        /// <param name="R">红 int</param>
        /// <param name="G">绿 int</param>
        /// <param name="B">蓝 int</param>
        /// <returns></returns>
        public static string colorRGBtoHx16(Color color)
        {
            if (color.IsEmpty)
                return "#000000";
            return ColorTranslator.ToHtml(color);
        }
        #endregion
    }
}

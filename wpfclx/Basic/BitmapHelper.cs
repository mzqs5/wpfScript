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
            //image.Dispose();
            return b;
        }

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
                g.DrawImage(image, new Rectangle(0, 0, r.Right - r.Left, r.Bottom - r.Top), new Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top), GraphicsUnit.Pixel);

            }
            image.Dispose();
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
        public static void ColorReplace(Bitmap source, string color)
        {
            PointBitmap bitmap = new PointBitmap(source);
            bitmap.LockBits();
            var c = colorHx16toRGB(color.Split('-')[0]);
            var c1 = colorHx16toRGB(color.Split('-')[1]);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var pc = bitmap.GetPixel(i, j);
                    if (c1.R < pc.R && pc.R < c.R && c1.G < pc.G && pc.G < c.G && c1.B < pc.B && pc.B < c.B && Math.Abs(pc.R - pc.G) < 10 && Math.Abs(pc.G - pc.B) < 10)
                    {
                        bitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        bitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            bitmap.UnlockBits();
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

using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace wpfclx
{
    public class aforge
    {
        private static int deviationX = 8;
        private static int deviationY = 32;

        /// <summary>
        /// 模板匹配
        /// </summary>
        /// <param name="source"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static Rectangle ProcessImage(Bitmap source, Bitmap temp, bool debug)
        {
            source = GrayscaleThresholdBlobsFiltering(source, debug);
            temp = GrayscaleThresholdBlobsFiltering(temp);
            ExhaustiveTemplateMatching templateMatching = new ExhaustiveTemplateMatching(0.7f);
            var compare = templateMatching.ProcessImage(source, temp);
            return compare.Count() > 0 ? compare[0].Rectangle : new Rectangle();
        }

        /// <summary>
        /// 灰度 二值 去躁点
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bitmap GrayscaleThresholdBlobsFiltering(Bitmap b, bool debug = false)
        {
            b = new Grayscale(0.2125, 0.7154, 0.0721).Apply(b);
            if (debug)
                b.Save($"C:\\clx\\b1{new Random().Next(100, 200)}.bmp");
            b = new Threshold(85).Apply(b);
            if (debug)
                b.Save($"C:\\clx\\b2{new Random().Next(100, 200)}.bmp");
            b = new BlobsFiltering(1, 1, b.Width, b.Height).Apply(b);
            if (debug)
                b.Save($"C:\\clx\\b3{new Random().Next(100, 200)}.bmp");
            return b;
        }

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
            //var b = image.Clone(new Rectangle(0, 0, image.Width, image.Height), format);
            image.Dispose();
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
                g.DrawImage(image, new Rectangle(0, 0, r.Right - r.Left, r.Bottom - r.Top), new Rectangle(r.Left+ deviationX, r.Top+ deviationY, r.Right - r.Left, r.Bottom - r.Top), GraphicsUnit.Pixel);

            }
            //var b = image.Clone(new Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top), format);
            image.Dispose();
            return b;
        }


    }
}

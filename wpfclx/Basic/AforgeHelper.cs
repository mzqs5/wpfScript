using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace wpfclx
{
    /// <summary>
    /// 图像识别帮助类
    /// </summary>
    public class AforgeHelper
    {
        /// <summary>
        /// 模板匹配
        /// </summary>
        /// <param name="source"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<TemplateMatch> ProcessImage(Bitmap source, Bitmap temp, FindDirection findType = FindDirection.LeftTopToRightDown, float similarity = 0.9f)
        {
            ExhaustiveTemplateMatching templateMatching = new ExhaustiveTemplateMatching(similarity);
            TemplateMatch[] compare = templateMatching.ProcessImage(source, temp);
            if (compare.Count() > 0)
            {
                List<TemplateMatch> list = null;
                switch (findType)
                {
                    case FindDirection.LeftTopToRightDown:
                        list = compare.OrderBy(o => o.Rectangle.Left + o.Rectangle.Top).ToList();
                        break;
                    case FindDirection.RightDownToLeftTop:
                        list = compare.OrderByDescending(o => o.Rectangle.Left + o.Rectangle.Top).ToList();
                        break;
                    case FindDirection.CoreToAround:
                        list = compare.OrderBy(o => Math.Abs(o.Rectangle.Left - source.Width / 2) + Math.Abs(o.Rectangle.Top - source.Height / 2)).ToList();
                        break;
                }
                return list;
            }
            return new List<TemplateMatch>();
        }



        /// <summary>
        /// 灰度 二值 去躁点
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Bitmap GrayscaleThresholdBlobsFiltering(Bitmap b, int threshold = 50)
        {
            b = new Grayscale(0.2125, 0.7154, 0.0721).Apply(b);
            b = new Threshold(threshold).Apply(b);
            b = new BlobsFiltering(1, 1, b.Width, b.Height).Apply(b);
            return b;
        }


    }
}

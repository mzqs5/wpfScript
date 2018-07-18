namespace wpfclx
{
    public class FontLibrary
    {
        /// <summary>
        /// 文字名称
        /// </summary>
        public string TextName { get; set; }
        /// <summary>
        /// 字体点阵16进制数据
        /// </summary>
        public string Byte16 { get; set; }

        /// <summary>
        /// 字体点阵有效像素点数量
        /// </summary>
        public string activeCount { get; set; }

        /// <summary>
        /// 字体点阵有效行数
        /// </summary>
        public int rowCount { get; set; }
    }
}

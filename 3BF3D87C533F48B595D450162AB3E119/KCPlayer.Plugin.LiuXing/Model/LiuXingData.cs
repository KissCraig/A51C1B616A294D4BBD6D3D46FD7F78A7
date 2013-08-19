namespace KCPlayer.Plugin.LiuXing.Model
{
    /// <summary>
    ///     数据原型
    /// </summary>
    public class LiuXingData
    {
        /// <summary>
        /// 电影名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电影网址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 电影封面
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 电影质量
        /// </summary>
        public string HDs { get; set; }
        /// <summary>
        /// 电影年代
        /// </summary>
        public string Tim { get; set; }
        /// <summary>
        /// 电影演员
        /// </summary>
        public string Car { get; set; }
        /// <summary>
        /// 电影类型
        /// </summary>
        public string Typ { get; set; }
        /// <summary>
        /// 电影更新
        /// </summary>
        public string Upt { get; set; }
        /// <summary>
        /// 电影评分
        /// </summary>
        public string Cos { get; set; }
        /// <summary>
        /// 电影地区
        /// </summary>
        public string Loc { get; set; }
        /// <summary>
        /// 影片大类
        /// </summary>
        public string Mpe { get; set; }
        /// <summary>
        /// 电影简介
        /// </summary>
        public string Des { get; set; }
        /// <summary>
        /// 电影资源地址
        /// </summary>
        public System.Collections.Generic.List<string> Drl { get; set; }
    }
}
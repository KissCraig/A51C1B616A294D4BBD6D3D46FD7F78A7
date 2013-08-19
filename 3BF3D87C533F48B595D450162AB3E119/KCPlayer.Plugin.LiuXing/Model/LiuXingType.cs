namespace KCPlayer.Plugin.LiuXing.Model
{
    public class LiuXingType
    {
        /// <summary>
        /// 请求类型
        /// </summary>
        public LiuXingEnum Type { get; set; }
        /// <summary>
        /// 请求编码
        /// </summary>
        public System.Text.Encoding Encoding { get; set; }
        /// <summary>
        /// 请求代理
        /// </summary>
        public System.Net.WebProxy Proxy { get; set; }
        /// <summary>
        /// 请求模型
        /// </summary>
        public LiuXingData Data { get; set; }
        /// <summary>
        /// 请求图片
        /// </summary>
        public System.Drawing.Image Img { get; set; }
        /// <summary>
        /// 请求表示
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 是否复制
        /// </summary>
        public bool IsCopy { get; set; }
        /// <summary>
        /// 主机地址
        /// </summary>
        public System.Collections.Generic.Dictionary<string, string> HostAddress { get; set; }

    }
}
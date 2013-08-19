namespace KCP.Plugin.LiuXing.Model
{
    public class LiuXingType
    {
        public LiuXingEnum Type { get; set; }
        public LiuXingStyle Style { get; set; }
        public System.Text.Encoding Encoding { get; set; }
        public System.Net.WebProxy Proxy { get; set; }
        public LiuXingData Data { get; set; }
        public bool IsCopy { get; set; }
        public System.Collections.Generic.Dictionary<string, string> HostAddress { get; set; }
        public System.Drawing.Image Img { get; set; }
    }
}
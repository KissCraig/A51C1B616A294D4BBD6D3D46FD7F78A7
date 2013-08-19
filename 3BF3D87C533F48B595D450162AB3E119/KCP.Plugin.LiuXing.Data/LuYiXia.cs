using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Url;

namespace KCP.Plugin.LiuXing.Data
{
    public class LuYiXia
    {
        /// <summary>
        /// 撸一下API接口
        /// </summary>
        private const string LuYiXiaApi =
            @"http://api.kcplayer.com:7383/watching/getdata?s=0&e=1&filter=false";

        /// <summary>
        /// 开始撸一下
        /// </summary>
        public static void StartLuYiXia()
        {
            using (
                var ludown = new System.Net.WebClient
                    {
                        Encoding = System.Text.Encoding.UTF8,
                        Proxy = PublicStatic.MyProxy
                    })
            {
                ludown.DownloadStringAsync(new System.Uri(LuYiXiaApi));
                ludown.DownloadStringCompleted += Ludown_DownloadStringCompleted;
            }
        }

        /// <summary>
        /// 完成撸一下地址解析并开始点播
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Ludown_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;
            var urllists = resultstr.GetSingle("\"Url\":\"", "\",\"Gcid\":");
            if (string.IsNullOrEmpty(urllists)) return;
            Pub.StartToVod(urllists);
        }
    }
}
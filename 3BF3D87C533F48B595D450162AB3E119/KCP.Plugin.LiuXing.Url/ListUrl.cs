using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Url
{
    public class ListUrl
    {
        /// <summary>
        /// 开始下载链接地址页面数据
        /// </summary>
        /// <param name="isCopy"></param>
        /// <param name="iType"></param>
        public static void GetThisUrl(bool isCopy, LiuXingType iType)
        {
            if (iType == null) return;
            if (iType.Data == null) return;
            if (string.IsNullOrEmpty(iType.Data.Url)) return;
            using (
                var urldown = new System.Net.WebClient
                    {
                        Encoding = iType.Encoding,
                        Proxy = iType.Proxy
                    })
            {
                var iClass = new LiuXingType
                    {
                        Type = iType.Type,
                        Encoding = iType.Encoding,
                        Proxy = iType.Proxy,
                        Data = iType.Data,
                        IsCopy = isCopy
                    };
                urldown.DownloadStringAsync(new System.Uri(iType.Data.Url), iClass);
                urldown.DownloadStringCompleted += urldown_DownloadStringCompleted;
            }
        }

        /// <summary>
        /// 下载完成链接地址页面数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void urldown_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;
            if (e.UserState == null) return;
            var tag = e.UserState as LiuXingType;
            if (tag == null) return;
            // 进入后续页面数据解析
            ListDrl.GetThisDrl(resultstr, tag);
        }
    }
}
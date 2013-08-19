using System;
using System.Net;
using System.Text;

namespace KCPlayer.Plugin.LiuXing.LoadLiuXing.Piaohua
{
    public class SearchPage
    {
        /// <summary>
        ///     下载影视资料页的数据
        /// </summary>
        /// <param name="eachPage"></param>
        public static void StartSearchPage(string eachPage)
        {
            if (string.IsNullOrEmpty(eachPage)) return;
            using (
                var urldown = new WebClient
                    {
                        Encoding = Encoding.UTF8,
                        Proxy = PublicStatic.MyProxy
                    })
            {
                urldown.DownloadStringAsync(new Uri(eachPage));
                urldown.DownloadStringCompleted += Urldown_DownloadStringCompleted;
            }
        }

        /// <summary>
        ///     下载影视资源页完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Urldown_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            string resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;

            // 解析影视资料页的数据并生成模型
            LiuXingData tag = SearchData.JieXiSearchData(resultstr);
            if (tag == null) return;
            tag.Drl = SearchUrl.GetThisUrl(resultstr);
            if (tag.Drl == null || tag.Drl.Count <= 0) return;
            if (string.IsNullOrEmpty(tag.Img)) return;
            using (
                var imgdown = new WebClient
                    {
                        Encoding = Encoding.UTF8,
                        Proxy = PublicStatic.MyProxy
                    })
            {
                imgdown.DownloadDataAsync(new Uri(tag.Img), tag);
                imgdown.DownloadDataCompleted += SearchImg.Imgdown_DownloadDataCompleted;
            }
        }
    }
}
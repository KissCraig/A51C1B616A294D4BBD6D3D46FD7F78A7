using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;

namespace KCP.Plugin.LiuXing.LoadLiuXing.Dyfm
{
    public class SearchUrl
    {
        public static void Urldown_DownloadDataCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;

            var tag = SearchData.JieXiData(resultstr);
            if (tag == null) return;
            if (!string.IsNullOrEmpty(tag.Img))
            {
                using (
                    var imgdown = new System.Net.WebClient
                        {
                            Encoding = System.Text.Encoding.UTF8,
                            Proxy = PublicStatic.MyProxy
                        })
                {
                    imgdown.DownloadDataAsync(new System.Uri(tag.Img), tag);
                    imgdown.DownloadDataCompleted += SearchImg.Imgdown_DownloadDataCompleted;
                }
            }
        }
    }
}
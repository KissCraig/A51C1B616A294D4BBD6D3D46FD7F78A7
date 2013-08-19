using System.Drawing;
using System.Net;
using KCPlayer.Plugin.LiuXing.LoadLiuXing.Com;

namespace KCPlayer.Plugin.LiuXing.LoadLiuXing.Piaohua
{
    public class SearchImg
    {
        /// <summary>
        ///     下载图片数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Imgdown_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            byte[] resultstr = e.Result;
            if (resultstr == null) return;
            // 图片准备好了后进入显示
            Image img = Pub.ByteToImage(resultstr);
            if (img == null) return;
            var tag = e.UserState as LiuXingData;
            if (tag == null) return;
            SearchDis.DisPlayListItem(tag, img);
        }
    }
}
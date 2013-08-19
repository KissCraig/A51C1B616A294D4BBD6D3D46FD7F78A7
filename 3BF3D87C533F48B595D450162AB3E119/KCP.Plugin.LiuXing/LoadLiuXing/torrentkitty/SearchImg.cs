using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.LoadLiuXing.torrentkitty
{
    public class SearchImg
    {
        public static void Imgdown_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (resultstr == null) return;

            // 图片准备好了后进入显示
            var img = resultstr.GetImageFromByte();
            if (img == null) return;
            var tag = e.UserState as LiuXingData;
            if (tag == null) return;
            SearchDis.DisPlayListItem(tag, img);
        }
    }
}
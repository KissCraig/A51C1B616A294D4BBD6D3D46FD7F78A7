using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCPlayer.Plugin.LiuXing.LoadLiuXing.YYet;

namespace KCP.Plugin.LiuXing.LoadLiuXing.YYet
{
    public class Search
    {
        public Search()
        {
            var temppath = @"http://www.yyets.com/php/search/index?type=resource&keyword=" + PublicStatic.SearchWord;
            if (string.IsNullOrEmpty(temppath)) return;
            // 开始下载地址层
            using (
                var datadown = new System.Net.WebClient
                    {
                        Encoding = System.Text.Encoding.UTF8,
                        Proxy = PublicStatic.MyProxy
                    })
            {
                datadown.DownloadStringAsync(new System.Uri(temppath));
                datadown.DownloadStringCompleted += Datadown_DownloadStringCompleted;
            }
        }

        /// <summary>
        /// 下载第一层的搜索数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Datadown_DownloadStringCompleted(object sender,
                                                             System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;

            #region 得到搜索结果的几个页面地址

            // 得到<ul class=\"allsearch dashed boxPadd6\"> ~ </ul>
            var orignlis = resultstr.GetSingle("<ul class=\"allsearch dashed boxPadd6\">", "</ul>");
            if (string.IsNullOrEmpty(orignlis))
            {
                return;
            }

            // 得到 <li> ~ </li>
            var orignli = orignlis.GetValue("<a href=\"", "\" target=\"_blank\">");
            if (orignli == null || orignli.Count <= 0) return;

            // 解析数据
            for (var i = 0; i < orignli.Count; i++)
            {
                // 依次启动下载
                SearchPage.StartSearchPage(orignli[i]);
            }

            #endregion
        }
    }
}
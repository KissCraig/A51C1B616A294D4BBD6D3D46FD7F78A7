using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;

namespace KCP.Plugin.LiuXing.LoadLiuXing.Piaohua
{
    public class Search
    {
        public Search()
        {
            var temppath =
                System.Web.HttpUtility.HtmlDecode(@"http://www.xiaobajiew.com/index.php?s=video/search&submit=搜索&wd=" +
                                                  PublicStatic.SearchWord);
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
            var orignlis = resultstr.GetSingle("<ul class=\"relist clearfix\">", "</ul>");
            if (string.IsNullOrEmpty(orignlis))
            {
                return;
            }

            // 得到 <li> ~ </li>
            var orignli = orignlis.GetValue("<li>", "</li>");
            if (orignli == null || orignli.Count <= 0) return;
            // 得到Url列表
            var listurls = new System.Collections.Generic.List<string>();
            foreach (var v in orignli)
            {
                var orign = v.GetSingle("<div class=\"minfo_op\"><a href=\"", "\" class=\"info\">下载");
                if (!string.IsNullOrEmpty(orign))
                {
                    listurls.Add(orign);
                }
            }
            if (listurls.Count <= 0) return;
            // 解析数据
            foreach (var listurl in listurls)
            {
                var listtemp = listurl;
                if (!string.IsNullOrEmpty(listtemp))
                {
                    // 依次启动下载
                    SearchPage.StartSearchPage(listurl);
                }
            }

            #endregion
        }
    }
}
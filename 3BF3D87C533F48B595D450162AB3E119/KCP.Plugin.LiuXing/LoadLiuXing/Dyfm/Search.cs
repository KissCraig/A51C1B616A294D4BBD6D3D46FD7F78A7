using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;

namespace KCP.Plugin.LiuXing.LoadLiuXing.Dyfm
{
    public class Search
    {
        public Search()
        {
            var temppath = System.Web.HttpUtility.HtmlDecode(@"http://dianying.fm/search?key=" + PublicStatic.SearchWord);
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
            //
            // 得到<ul class=\"x-movie-list nav nav-pills\" style=\"padding-top:0;\"> ~ </ul>
            var orignlis = resultstr.GetSingle("<ul class=\"x-movie-list nav nav-pills\" style=\"padding-top:0;\">",
                                               "</ul>");
            if (string.IsNullOrEmpty(orignlis))
            {
                return;
            }
            // 得到 <li> ~ </li>
            var orignli = orignlis.GetValue("<li>", "</li>");
            if (orignli == null || orignli.Count <= 0) return;
            // 得到影片页地址
            var urls = new System.Collections.Generic.List<string>();
            for (var i = 0; i < orignli.Count; i++)
            {
                var celllistr = orignli[i];
                if (string.IsNullOrEmpty(celllistr)) continue;
                // 电影名称
                var tempname = celllistr.GetSingle("<a target=\"_blank\" href=\"",
                                                   "</a> <span class=\"muted\">");
                if (!string.IsNullOrEmpty(tempname))
                {
                    if (tempname.Contains(">"))
                    {
                        if (tempname.Length > 0)
                        {
                            var spitename = tempname.Split('>');
                            // 电影网址
                            var urltemp = "http://dianying.fm" + spitename[0].Replace("\"", "");
                            if (!string.IsNullOrEmpty(urltemp))
                            {
                                urls.Add(urltemp);
                            }
                        }
                    }
                }
            }
            if (urls.Count > 0)
            {
                foreach (var url in urls)
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        using (
                            var urldown = new System.Net.WebClient
                                {
                                    Encoding = System.Text.Encoding.UTF8,
                                    Proxy = PublicStatic.MyProxy
                                })
                        {
                            urldown.DownloadStringAsync(new System.Uri(url));
                            urldown.DownloadStringCompleted += SearchUrl.Urldown_DownloadDataCompleted;
                        }
                    }
                }
            }
        }
    }
}
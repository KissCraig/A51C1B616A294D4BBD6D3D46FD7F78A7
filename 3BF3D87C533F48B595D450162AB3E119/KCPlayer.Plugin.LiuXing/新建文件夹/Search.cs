using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;

namespace KCPlayer.Plugin.LiuXing.LoadLiuXing.Piaohua
{
    public class Search
    {
        public Search()
        {
            string temppath =
                HttpUtility.HtmlDecode(@"http://www.xiaobajiew.com/index.php?s=video/search&submit=搜索&wd=" +
                                       PublicStatic.SearchWord);
            if (string.IsNullOrEmpty(temppath)) return;
            // 开始下载地址层
            using (
                var datadown = new WebClient
                    {
                        Encoding = Encoding.UTF8,
                        Proxy = PublicStatic.MyProxy
                    })
            {
                datadown.DownloadStringAsync(new Uri(temppath));
                datadown.DownloadStringCompleted += Datadown_DownloadStringCompleted;
            }
        }

        private static void Datadown_DownloadStringCompleted(object sender,
                                                             DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            string resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;

            #region 得到搜索结果的几个页面地址

            // 得到<ul class=\"allsearch dashed boxPadd6\"> ~ </ul>
            string orignlis = LiuXingRegex.GetSingle(resultstr, "<ul class=\"relist clearfix\">", "</ul>");
            if (string.IsNullOrEmpty(orignlis))
            {
                return;
            }

            // 得到 <li> ~ </li>
            List<string> orignli = LiuXingRegex.GetValue(orignlis, "<li>", "</li>");
            if (orignli == null || orignli.Count <= 0) return;
            // 得到Url列表
            var listurls = new List<string>();
            foreach (string v in orignli)
            {
                string orign = LiuXingRegex.GetSingle(v, "<div class=\"minfo_op\"><a href=\"", "\" class=\"info\">下载");
                if (!string.IsNullOrEmpty(orign))
                {
                    listurls.Add(orign);
                }
            }
            if (listurls.Count <= 0) return;
            // 解析数据
            foreach (string listurl in listurls)
            {
                string listtemp = listurl;
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
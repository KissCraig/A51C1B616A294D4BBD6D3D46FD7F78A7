using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.LoadLiuXing.torrentkitty
{
    public class Search
    {
        public Search(int num)
        {
            var temppath =
                System.Web.HttpUtility.HtmlDecode(@"http://www.torrentkitty.com/search/" + PublicStatic.SearchWord + "/" +
                                                  num);
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

            // 得到<ul class=\"allsearch dashed boxPadd6\"> ~ </ul>
            var orignlis = resultstr.GetSingle("<table id=\"archiveResult\">", "</table>");
            orignlis = orignlis.Replace("<tbody>", "");
            if (string.IsNullOrEmpty(orignlis))
            {
                return;
            }
            // 得到 <li> ~ </li>
            var orignli = orignlis.GetValue("Detail", "Open");
            if (orignli == null || orignli.Count <= 0) return;

            // 解析数据
            var zuiReDatas = new System.Collections.Generic.List<LiuXingData>();
            foreach (var v in orignli)
            {
                var tag = SearchData.JieXiSearchData(v);
                if (tag != null)
                {
                    zuiReDatas.Add(tag);
                }
            }
            if (zuiReDatas.Count <= 0) return;

            // 遍历数据中的图片
            for (var i = 0; i < zuiReDatas.Count; i++)
            {
                var tag = zuiReDatas[i];
                if (tag != null)
                {
                    var imgtemp = tag.Img;
                    if (!string.IsNullOrEmpty(imgtemp))
                    {
                        using (
                            var imgdown = new System.Net.WebClient
                                {
                                    Encoding = System.Text.Encoding.UTF8,
                                    Proxy = PublicStatic.MyProxy
                                })
                        {
                            imgdown.DownloadDataAsync(new System.Uri(imgtemp), tag);
                            imgdown.DownloadDataCompleted += SearchImg.Imgdown_DownloadDataCompleted;
                        }
                    }
                }
            }
        }
    }
}
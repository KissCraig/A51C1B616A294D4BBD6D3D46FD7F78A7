using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;

namespace KCP.Plugin.LiuXing.LoadLiuXing.Search
{
    public class StartSearch
    {
        /// <summary>
        /// 开始搜索
        /// </summary>
        public static void BeginSearch()
        {
            if (PublicStatic.SearchBox == null) return;
            if (string.IsNullOrEmpty(PublicStatic.SearchWord)) return;
            // 清理搜索容器
            PublicStatic.LiuXingCon.Controls.Clear();

            if ((PublicStatic.SearchWord.Contains("av") || PublicStatic.SearchWord.Contains("AV") ||
                 PublicStatic.SearchWord.Contains("成人") ||
                 PublicStatic.SearchWord.Contains("女优") || PublicStatic.SearchWord.Contains("成人电影")) &&
                PublicStatic.SearchWord.Contains(" "))
            {
                PublicStatic.SearchWord =
                    PublicStatic.SearchWord.Replace(" ", "")
                                .Replace("AV", "")
                                .Replace("成人", "")
                                .Replace("女优", "")
                                .Replace("成人电影", "");
                new torrentkitty.Search(1);
                new torrentkitty.Search(2);
                new torrentkitty.Search(3);
            }
            else if ((PublicStatic.SearchWord.Contains("高清") || PublicStatic.SearchWord.Contains("HR-HDTV")) &&
                     PublicStatic.SearchWord.Contains(" "))
            {
                PublicStatic.SearchWord = PublicStatic.SearchWord.Replace(" ", "")
                                                      .Replace("高清", "")
                                                      .Replace("HR-HDTV", "");
                new YYet.Search();
                new Dyfm.Search();
            }
            else if ((PublicStatic.SearchWord.Contains("普清") || PublicStatic.SearchWord.Contains("流畅")) &&
                     PublicStatic.SearchWord.Contains(" "))
            {
                PublicStatic.SearchWord = PublicStatic.SearchWord.Replace(" ", "")
                                                      .Replace("普清", "")
                                                      .Replace("流畅", "");
                new Xunbo.Search();
                new Piaohua.Search();
            }
            else
            {
                new Xunbo.Search();
                new Piaohua.Search();
                new YYet.Search();
                new Dyfm.Search();
                new torrentkitty.Search(1);
                new torrentkitty.Search(2);
                new torrentkitty.Search(3);
            }
        }
    }
}
using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.LoadLiuXing.Xunbo
{
    public class Search
    {
        public Search()
        {
            var urlEncode = System.Web.HttpUtility.UrlEncode(PublicStatic.SearchWord, System.Text.Encoding.Default);
            if (urlEncode == null) return;
            var tempword =
                urlEncode.ToUpper();
            if (string.IsNullOrEmpty(tempword)) return;
            var temppath = @"http://www.2tu.cc/search.asp?searchword=" + tempword;
            if (string.IsNullOrEmpty(temppath)) return;
            ListStart.StartList(temppath, new LiuXingType()
                {
                    Encoding = System.Text.Encoding.Default,
                    Proxy = PublicStatic.MyProxy,
                    Type = LiuXingEnum.XunboListItem
                });
        }
    }
}
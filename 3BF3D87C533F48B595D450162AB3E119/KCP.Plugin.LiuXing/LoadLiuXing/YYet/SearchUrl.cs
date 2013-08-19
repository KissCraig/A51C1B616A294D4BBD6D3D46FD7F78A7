namespace KCP.Plugin.LiuXing.LoadLiuXing.YYet
{
    public class SearchUrl
    {
        /// <summary>
        /// 解析搜索页面人人资源链接数据
        /// </summary>
        /// <param name="vodUrlStr"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<string> GetThisUrl(string vodUrlStr)
        {
            var tagurls = new System.Collections.Generic.List<string>();
            var urllists = vodUrlStr.GetSingle("<ul class=\"resod_list\"",
                                               "</ul>");
            if (string.IsNullOrEmpty(urllists)) return null;
            if (urllists.Contains("type=\"ed2k\""))
            {
                var orignli = urllists.GetValue("type=\"ed2k\" href=\"",
                                                "\" target=\"_blank\">");
                if (orignli == null || orignli.Count <= 0) return null;
                foreach (var v in orignli)
                {
                    tagurls.Add(v);
                }
            }
            if (urllists.Contains("thunder=\""))
            {
                var orignli = urllists.GetValue("thunder=\"", "\" king=\"\">");
                if (orignli == null || orignli.Count <= 0) return null;
                foreach (var v in orignli)
                {
                    tagurls.Add(v);
                }
            }
            var yyets = new System.Collections.Generic.List<string>();
            foreach (var tagurl in tagurls)
            {
                if (!string.IsNullOrEmpty(tagurl))
                {
                    if (tagurl.StartsWith("ed2k") || tagurl.StartsWith("ED2K") || tagurl.StartsWith("http") ||
                        tagurl.StartsWith("magnet") || tagurl.StartsWith("thunder") || tagurl.StartsWith("flashget") ||
                        tagurl.StartsWith("flashget"))
                    {
                        yyets.Add(System.Web.HttpUtility.HtmlDecode(tagurl));
                    }
                }
            }
            return yyets;
        }
    }
}
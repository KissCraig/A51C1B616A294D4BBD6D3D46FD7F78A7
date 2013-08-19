namespace KCP.Plugin.LiuXing.LoadLiuXing.Piaohua
{
    public class SearchUrl
    {
        /// <summary>
        /// 解析搜索页面飘花资源链接数据
        /// </summary>
        /// <param name="vodUrlStr"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<string> GetThisUrl(string vodUrlStr)
        {
            var tagurls = new System.Collections.Generic.List<string>();
            var urllists = vodUrlStr.GetSingle("<div class=\"zylistbox\">", "</div");
            if (string.IsNullOrEmpty(urllists)) return null;
            var orignli = urllists.GetValue("xzurl=", "'/><a");
            if (orignli == null || orignli.Count <= 0) return null;
            foreach (var v in orignli)
            {
                if (v.Contains("&"))
                {
                    var vv = v.Split('&');
                    if (vv.Length > 0)
                    {
                        tagurls.Add(vv[0]);
                    }
                }
                else
                {
                    tagurls.Add(v);
                }
            }
            return tagurls;
        }
    }
}
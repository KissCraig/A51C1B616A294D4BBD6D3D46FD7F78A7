using System.Collections.Generic;

namespace KCPlayer.Plugin.LiuXing.LoadLiuXing.Piaohua
{
    public class SearchUrl
    {
        /// <summary>
        ///     解析搜索页面飘花资源链接数据
        /// </summary>
        /// <param name="vodUrlStr"></param>
        /// <returns></returns>
        public static List<string> GetThisUrl(string vodUrlStr)
        {
            var tagurls = new List<string>();
            string urllists = LiuXingRegex.GetSingle(vodUrlStr, "<div class=\"zylistbox\">", "</div");
            if (string.IsNullOrEmpty(urllists)) return null;
            List<string> orignli = LiuXingRegex.GetValue(urllists, "xzurl=", "'/><a");
            if (orignli == null || orignli.Count <= 0) return null;
            foreach (string v in orignli)
            {
                if (v.Contains("&"))
                {
                    string[] vv = v.Split('&');
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
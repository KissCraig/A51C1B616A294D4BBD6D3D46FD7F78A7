using System.Collections.Generic;
using System.Web;
using KCPlayer.Plugin.LiuXing.Helper;
using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.LiuXing
{
    public class ListFile
    {
        /// <summary>
        ///     二次解析资源地址
        /// </summary>
        /// <param name="vodUrlStr"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public static List<string> JieUrls(string vodUrlStr, LiuXingType iType)
        {
            var tagurls = new List<string>();
            switch (iType.Type)
            {
                    // 迅播影院正常列表
                case LiuXingEnum.XunboSearchItem:
                case LiuXingEnum.XunboListItem:
                    {
                        #region case LiuXingEnum.XunboListItem:

                        string urllists = StringRegexHelper.GetSingle(vodUrlStr, "<script>var GvodUrls = \"", "\";</script>");
                        if (string.IsNullOrEmpty(urllists)) return null;
                        if (urllists.Contains("###"))
                        {
                            string[] urltemps = urllists.Split("###".ToCharArray());
                            for (int i = 0; i < urltemps.Length; i++)
                            {
                                tagurls.Add(HttpUtility.HtmlDecode(urltemps[i]));
                            }
                        }
                        else
                        {
                            tagurls.Add(HttpUtility.HtmlDecode(urllists));
                        }

                        #endregion
                    }
                    break;
                    // 人人影视正常列表
                case LiuXingEnum.YYetListItem:
                    {
                        #region case LiuXingEnum.YYetListItem:

                        string urllists = StringRegexHelper.GetSingle(vodUrlStr,
                                                                 "<ul class=\"resod_list\" season=\"0\" style=\"display:none;\">",
                                                                 "</ul>");
                        if (string.IsNullOrEmpty(urllists)) return null;
                        if (urllists.Contains("type=\"ed2k\""))
                        {
                            var orignli = StringRegexHelper.GetValue(urllists, "type=\"ed2k\" href=\"", "\"");
                            if (orignli == null || orignli.Count <= 0) return null;
                            foreach (string v in orignli)
                            {
                                if (!tagurls.Contains(v))
                                {
                                    tagurls.Add(v);
                                }
                            }
                        }
                        if (urllists.Contains("thunder=\""))
                        {
                            var orignli = StringRegexHelper.GetValue(urllists, "thunder=\"", "\"");
                            if (orignli == null || orignli.Count <= 0) return null;
                            foreach (string v in orignli)
                            {
                                if (!tagurls.Contains(v))
                                {
                                    tagurls.Add(v);
                                }
                            }
                        }
                        var yyets = new System.Collections.Generic.List<string>();
                        foreach (string tagurl in tagurls)
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
                        tagurls = yyets;
                        #endregion
                    }
                    break;
            }
            return Helper.UrlCodeHelper.GetDecodeList(tagurls);
        }
    }
}
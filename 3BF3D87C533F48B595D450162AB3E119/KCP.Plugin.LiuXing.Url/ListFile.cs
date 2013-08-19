using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Url
{
    public class ListFile
    {
        /// <summary>
        /// 二次解析资源地址
        /// </summary>
        /// <param name="vodUrlStr"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<string> JieUrls(string vodUrlStr, LiuXingType iType)
        {
            var tagurls = new System.Collections.Generic.List<string>();
            switch (iType.Type)
            {
                    // 迅播影院正常列表
                case LiuXingEnum.XunboListItem:
                    {
                        #region case LiuXingEnum.XunboListItem:

                        var urllists = vodUrlStr.GetSingle("<script>var GvodUrls = \"", "\";</script>");
                        if (string.IsNullOrEmpty(urllists)) return null;
                        if (urllists.Contains("###"))
                        {
                            var urltemps = urllists.Split("###".ToCharArray());
                            for (var i = 0; i < urltemps.Length; i++)
                            {
                                tagurls.Add(System.Web.HttpUtility.HtmlDecode(urltemps[i]));
                            }
                        }
                        else
                        {
                            tagurls.Add(System.Web.HttpUtility.HtmlDecode(urllists));
                        }

                        #endregion
                    }
                    break;
                    // 人人影视正常列表
                case LiuXingEnum.YYetListItem:
                    {
                        #region case LiuXingEnum.YYetListItem:

                        var urllists =
                            vodUrlStr.GetSingle("<ul class=\"resod_list\" season=\"0\" style=\"display:none;\">",
                                                "</ul>");
                        if (string.IsNullOrEmpty(urllists)) return null;
                        if (urllists.Contains("type=\"ed2k\""))
                        {
                            var orignli = urllists.GetValue("type=\"ed2k\" href=\"",
                                                            "\" target=\"_blank\">");
                            if (orignli == null || orignli.Count <= 0) return null;
                            foreach (var v in orignli)
                            {
                                tagurls.Add(System.Web.HttpUtility.HtmlDecode(v));
                            }
                        }
                        if (urllists.Contains("thunder=\""))
                        {
                            var orignli = urllists.GetValue("thunder=\"", "\" king=\"\">");
                            if (orignli == null || orignli.Count <= 0) return null;
                            foreach (var v in orignli)
                            {
                                tagurls.Add(System.Web.HttpUtility.HtmlDecode(v));
                            }
                        }

                        #endregion
                    }
                    break;
            }
            return tagurls;
        }
    }
}
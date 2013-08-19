using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Url
{
    public class ListVod
    {
        /// <summary>
        /// 适配最佳播放地址 - GetVodUrl
        /// </summary>
        /// <param name="iType"></param>
        /// <returns></returns>
        public static string GetVodUrl(LiuXingType iType)
        {
            #region 适配最佳播放地址 - GetVodUrl

            var vodUrl = string.Empty;
            switch (iType.Type)
            {
                    // 迅播影院正常列表
                case LiuXingEnum.XunboListItem:
                    {
                        #region case LiuXingEnum.XunboListItem:

                        var longer = GetLongerWithXunbo(iType.Data);
                        if (!string.IsNullOrEmpty(longer))
                        {
                            vodUrl = longer;
                        }
                        else
                        {
                            var h1080P = Get1080PWithXunbo(iType.Data);
                            if (!string.IsNullOrEmpty(h1080P))
                            {
                                vodUrl = h1080P;
                            }
                            else
                            {
                                var h1024P = Get1024PWithXunbo(iType.Data);
                                if (!string.IsNullOrEmpty(h1024P))
                                {
                                    vodUrl = h1024P;
                                }
                                else
                                {
                                    vodUrl = iType.Data.Drl[iType.Data.Drl.Count - 1];
                                }
                            }
                        }

                        #endregion
                    }
                    break;
                    // 人人影视正常列表
                case LiuXingEnum.YYetListItem:
                    {
                        #region case LiuXingEnum.YYetListItem:

                        var hrhdtv = GetHrHdtvWithYYet(iType.Data);
                        if (!string.IsNullOrEmpty(hrhdtv))
                        {
                            vodUrl = hrhdtv;
                        }
                        else
                        {
                            var h720P = Get720PWithYYet(iType.Data);
                            if (!string.IsNullOrEmpty(h720P))
                            {
                                vodUrl = h720P;
                            }
                            else
                            {
                                var h1080P = Get1080PWithYYet(iType.Data);
                                if (!string.IsNullOrEmpty(h1080P))
                                {
                                    vodUrl = h1080P;
                                }
                                else
                                {
                                    var ed2K = GetEd2KWithYYet(iType.Data);
                                    if (!string.IsNullOrEmpty(ed2K))
                                    {
                                        vodUrl = ed2K;
                                    }
                                    else
                                    {
                                        vodUrl = iType.Data.Drl[iType.Data.Drl.Count - 1];
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    break;
            }
            return vodUrl;

            #endregion
        }

        #region Private Code

        /// <summary>
        /// 人人影视获取HR-HDTV
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static string GetHrHdtvWithYYet(LiuXingData tag)
        {
            var hr = new System.Collections.Generic.List<string>();
            for (var i = tag.Drl.Count - 1; i >= 0; i--)
            {
                var tagdrl = tag.Drl[i];
                if (!tagdrl.Contains("HR-HDTV") && !tagdrl.Contains("Hr-HDTV")) continue;
                if (tagdrl.StartsWith("ed2k") || tagdrl.StartsWith("ED2K"))
                {
                    hr.Add(tagdrl);
                }
            }
            if (hr.Count > 0)
            {
                for (var i = hr.Count - 1; i >= 0; i--)
                {
                    var hrdrl = hr[i];
                    if (hrdrl.Contains("V2"))
                    {
                        return hrdrl;
                    }
                }
                return hr[hr.Count - 1];
            }
            return null;
        }

        /// <summary>
        /// 人人影视获取720P
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static string Get720PWithYYet(LiuXingData tag)
        {
            for (var i = tag.Drl.Count - 1; i >= 0; i--)
            {
                var tagdrl = tag.Drl[i];
                if (!tagdrl.Contains("720P") && !tagdrl.Contains("720p")) continue;
                if (tagdrl.StartsWith("ed2k") || tagdrl.StartsWith("ED2K"))
                {
                    return tagdrl;
                }
            }
            return null;
        }

        /// <summary>
        /// 人人影视获取1080P
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static string Get1080PWithYYet(LiuXingData tag)
        {
            for (var i = tag.Drl.Count - 1; i >= 0; i--)
            {
                var tagdrl = tag.Drl[i];
                if (!tagdrl.Contains("1080P") && !tagdrl.Contains("1080p")) continue;
                if (tagdrl.StartsWith("ed2k") || tagdrl.StartsWith("ED2K"))
                {
                    return tagdrl;
                }
            }
            return null;
        }

        /// <summary>
        /// 人人影视获取ED2K
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static string GetEd2KWithYYet(LiuXingData tag)
        {
            for (var i = tag.Drl.Count - 1; i >= 0; i--)
            {
                var tagdrl = tag.Drl[i];
                if (!tagdrl.Contains("ed2k") && !tagdrl.Contains("ED2K")) continue;
                if (tagdrl.StartsWith("ed2k") || tagdrl.StartsWith("ED2K"))
                {
                    return tagdrl;
                }
            }
            return null;
        }

        /// <summary>
        /// 迅播影院获取加长版
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static string GetLongerWithXunbo(LiuXingData tag)
        {
            for (var i = tag.Drl.Count - 1; i >= 0; i--)
            {
                var tagdrl = tag.Drl[i];
                if (!tagdrl.Contains("加长")) continue;
                if (tagdrl.StartsWith("ftp"))
                {
                    return tagdrl;
                }
            }
            return null;
        }

        /// <summary>
        /// 迅播影院获取1080P
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static string Get1080PWithXunbo(LiuXingData tag)
        {
            var h1080P = new System.Collections.Generic.List<string>();
            for (var i = tag.Drl.Count - 1; i >= 0; i--)
            {
                var tagdrl = tag.Drl[i];
                if (!tagdrl.Contains("1280")) continue;
                if (tagdrl.StartsWith("ftp"))
                {
                    h1080P.Add(tagdrl);
                }
            }
            if (h1080P.Count > 0)
            {
                for (var i = h1080P.Count - 1; i >= 0; i--)
                {
                    var h1080 = h1080P[i];
                    if (h1080.Contains("mkv"))
                    {
                        return h1080;
                    }
                }
                return h1080P[h1080P.Count - 1];
            }
            return null;
        }

        /// <summary>
        /// 迅播影院获取1024P
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static string Get1024PWithXunbo(LiuXingData tag)
        {
            var h1024P = new System.Collections.Generic.List<string>();
            for (var i = tag.Drl.Count - 1; i >= 0; i--)
            {
                var tagdrl = tag.Drl[i];
                if (!tagdrl.Contains("1024")) continue;
                if (tagdrl.StartsWith("ftp"))
                {
                    h1024P.Add(tagdrl);
                }
            }
            if (h1024P.Count > 0)
            {
                for (var i = 0; i < h1024P.Count; i++)
                {
                    var h1024 = h1024P[i];
                    if (h1024.Contains("mkv"))
                    {
                        return h1024;
                    }
                }
                return h1024P[h1024P.Count - 1];
            }
            return null;
        }

        #endregion
    }
}
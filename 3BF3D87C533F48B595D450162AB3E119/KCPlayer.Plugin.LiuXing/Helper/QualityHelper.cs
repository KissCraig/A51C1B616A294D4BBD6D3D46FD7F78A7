namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class QualityHelper
    {
        /// <summary>
        /// 清晰度的一些常用项
        /// </summary>
        private const string Quality1080 = @"1080";
        private const string Quality720 = @"720";
        private const string QualityHrhdtv = @"HR-HDTV";
        private const string Quality1280 = @"1280";
        private const string Quality1024 = @"1024";
        private const string Quality480 = "480";
        private const string QualityDvd = @"DVD";
        private const string QualityTc = @"TC";
        private const string QualityTs = @"TS";
        private const string QualityUnKnow = @"Unknown";
        private const string QualityNone = @"None";
        private const string QualitySuper = @"SQ";
        private const string QualityHigh = @"HQ";
        private const string QualityLower = @"LQ";
        private const string QualityUnKnown = @"UN";

        /// <summary>
        /// 判断清晰度，设置类型标识
        /// </summary>
        /// <param name="drl"></param>
        /// <returns></returns>
        public static string GetHdsSign(string drl)
        {
            #region 判断清晰度，设置类型标识
            if (drl.Contains(Quality1080) || drl.Contains(Quality720))
            {
                return QualitySuper;
            }
            if (drl.Contains(QualityHrhdtv) || drl.Contains(Quality1280))
            {
                return QualityHigh;
            }
            if (drl.Contains(Quality1024) || drl.Contains(QualityDvd) || drl.Contains(Quality480) || drl.Contains(QualityTc) || drl.Contains(QualityTs))
            {
                return QualityLower;
            }
            return QualityUnKnown; 
            #endregion
        }

        /// <summary>
        /// 判断清晰度，设置文字标识
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        public static string GetHdsData(System.Collections.Generic.List<string> drls)
        {
            #region 判断清晰度，设置文字标识

            // 鉴别是否存在资源
            if (drls != null && drls.Count > 0)
            {
                // 鉴别是否包括1080P
                var h1080 = Get1080P(drls);
                if (!string.IsNullOrEmpty(h1080))
                {
                    return Quality1080;
                }
                // 鉴别是否包括720P
                var h720 = Get720P(drls);
                if (!string.IsNullOrEmpty(h720))
                {
                    return Quality720;
                }
                // 鉴别是否包括HR-HDTV
                var hrhdtv = GetHrhdtv(drls);
                if (!string.IsNullOrEmpty(hrhdtv))
                {
                    return QualityHrhdtv;
                }
                // 鉴别是否包括1280P
                var p1280 = Get1280P(drls);
                if (!string.IsNullOrEmpty(p1280))
                {
                    return Quality1280;
                }
                // 鉴别是否包括1024P
                var p1024 = Get1024P(drls);
                if (!string.IsNullOrEmpty(p1024))
                {
                    return Quality1024;
                }
                // 鉴别是否包括480P
                var p480 = Get480P(drls);
                if (!string.IsNullOrEmpty(p480))
                {
                    return Quality480;
                }
                // 鉴别是否包括DVD画质
                var pdvd = GetDvdp(drls);
                if (!string.IsNullOrEmpty(pdvd))
                {
                    return QualityDvd;
                }
                var ptsc = GetTsc(drls);
                // 鉴别是否包括TC/TS画质
                if (!string.IsNullOrEmpty(ptsc))
                {
                    return QualityTs;
                }
                return QualityUnKnow;
            }
            return QualityNone; 

            #endregion
        }

        /// <summary>
        /// 判断清晰度，取清晰度最高
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        public static string GetHdsVod(System.Collections.Generic.List<string> drls)
        {
            #region 判断清晰度，取清晰度最高
            // 鉴别是否存在资源
            if (drls != null && drls.Count > 0)
            {
                // 鉴别是否包括1080P
                var h1080 = Get1080P(drls);
                if (!string.IsNullOrEmpty(h1080))
                {
                    return h1080;
                }
                // 鉴别是否包括720P
                var h720 = Get720P(drls);
                if (!string.IsNullOrEmpty(h720))
                {
                    return h720;
                }
                // 鉴别是否包括HR-HDTV
                var hrhdtv = GetHrhdtv(drls);
                if (!string.IsNullOrEmpty(hrhdtv))
                {
                    return hrhdtv;
                }
                // 鉴别是否包括1280P
                var p1280 = Get1280P(drls);
                if (!string.IsNullOrEmpty(p1280))
                {
                    return p1280;
                }
                // 鉴别是否包括1024P
                var p1024 = Get1024P(drls);
                if (!string.IsNullOrEmpty(p1024))
                {
                    return p1024;
                }
                // 鉴别是否包括480P
                var p480 = Get480P(drls);
                if (!string.IsNullOrEmpty(p480))
                {
                    return p480;
                }
                // 鉴别是否包括DVD画质
                var pdvd = GetDvdp(drls);
                if (!string.IsNullOrEmpty(pdvd))
                {
                    return pdvd;
                }
                var ptsc = GetTsc(drls);
                // 鉴别是否包括TC/TS画质
                if (!string.IsNullOrEmpty(ptsc))
                {
                    return ptsc;
                }
                return drls[drls.Count - 1];
            }
            return null;  
            #endregion
        }

        /// <summary>
        /// 鉴别是否包括1080P
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        private static string Get1080P(System.Collections.Generic.IEnumerable<string> drls)
        {
            #region 鉴别是否包括1080P
            foreach (string url in drls)
            {
                if (url.Contains("1080") || url.Contains("1080P") || url.Contains("1080p"))
                {
                    return url;
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// 鉴别是否包括720P
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        private static string Get720P(System.Collections.Generic.IEnumerable<string> drls)
        {
            #region 鉴别是否包括720P
            foreach (string url in drls)
            {
                if (url.Contains("720") || url.Contains("720P") || url.Contains("720p"))
                {
                    return url;
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// 鉴别是否包括HR-HDTV
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        private static string GetHrhdtv(System.Collections.Generic.IEnumerable<string> drls)
        {
            #region 鉴别是否包括HR-HDTV
            foreach (string url in drls)
            {
                if (url.Contains("HR-HDTV") || url.Contains("Hr-HDTV") || url.ToLower().Contains("hr-hdtv"))
                {
                    return url;
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// 鉴别是否包括1280P
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        private static string Get1280P(System.Collections.Generic.IEnumerable<string> drls)
        {
            #region 鉴别是否包括1280P
            foreach (string url in drls)
            {
                if (url.Contains("1280") || url.Contains("1280P") || url.Contains("1280p"))
                {
                    return url;
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// 鉴别是否包括1024P
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        private static string Get1024P(System.Collections.Generic.IEnumerable<string> drls)
        {
            #region 鉴别是否包括1024P
            foreach (string url in drls)
            {
                if (url.Contains("1024") || url.Contains("1024P") || url.Contains("1024p"))
                {
                    return url;
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// 鉴别是否包括480P画质
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        private static string Get480P(System.Collections.Generic.IEnumerable<string> drls)
        {
            #region 鉴别是否包括480P画质
            foreach (string url in drls)
            {
                if (url.Contains("480") || url.Contains("480P") || url.Contains("480p"))
                {
                    return url;
                }
            }
            return null; 
            #endregion
        }

        /// <summary>
        /// 鉴别是否包括DVD画质
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        private static string GetDvdp(System.Collections.Generic.IEnumerable<string> drls)
        {
            #region 鉴别是否包括DVD画质
            foreach (string url in drls)
            {
                if (url.Contains("dvd") || url.Contains("DVD"))
                {
                    return url;
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// 鉴别是否包括TS/C画质
        /// </summary>
        /// <param name="drls"></param>
        /// <returns></returns>
        private static string GetTsc(System.Collections.Generic.IEnumerable<string> drls)
        {
            #region 鉴别是否包括TS/C画质
            foreach (string url in drls)
            {
                if (url.Contains(QualityTs) || url.Contains(QualityTs.ToLower()) || url.Contains(QualityTc) || url.Contains(QualityTc.ToLower()))
                {
                    return url;
                }
            }
            return null; 
            #endregion
        }
    }
}

using KCPlayer.Plugin.TestVod.Helper;

namespace KCPlayer.Plugin.TestVod
{
    public class VodUrl
    {
        /// <summary>
        /// 解析链接地址
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static void AnalyzeVodPath(string origin)
        {
            #region 解析链接地址
            try
            {
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetDataObject(origin);
            }
            catch
            {

            }
            var vodType = AnalyzeVodType(origin);
            switch (vodType)
            {
                case VodUrlType.Ed2K:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl(System.Web.HttpUtility.UrlEncode(origin));
                        //TestVodPlayer.PlayUrl(ToXunleiCode(origin));
                    }
                    break;
                case VodUrlType.Ftp:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl(System.Web.HttpUtility.UrlEncode(origin));
                        //TestVodPlayer.PlayUrl(ToXunleiCode(origin));
                    }
                    break;
                case VodUrlType.Hash:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl(AnalyzeHash(origin));
                    }
                    break;
                case VodUrlType.LocalBt:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl( AnalyzeLocalBt(origin));
                    }
                    break;
                case VodUrlType.Magnet:
                    {
                        // 进入解析
                        VodStyleHelper.PlayUrl(System.Web.HttpUtility.UrlEncode(origin));
                        //GetMagnet(origin);
                    }
                    break;
                case VodUrlType.QQdl:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl(DeQQdl(origin));
                    }
                    break;
                case VodUrlType.Web:
                    {
                        TestVodAction.ShouQi(false);
                        // System.Windows.Forms.MessageBox.Show(@"暂不支持此资源格式或链接");
                    }
                    break;
                case VodUrlType.WebBt:
                    {
                        TestVodAction.ShouQi(false);
                        // System.Windows.Forms.MessageBox.Show(@"暂不支持此资源格式或链接");
                    }
                    break;
                case VodUrlType.BtHash:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl(origin);
                    }
                    break;
                case VodUrlType.Flashget:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl(DeFlashget(origin));
                    }
                    break;
                case VodUrlType.Thunder:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl(DeThunder(origin));
                    }
                    break;
                case VodUrlType.None:
                    {
                        TestVodAction.ShouQi(false);
                        // System.Windows.Forms.MessageBox.Show(@"暂不支持此资源格式或链接");
                    }
                    break;
                case VodUrlType.Ffdycc:
                    {
                        VodStyleHelper.PlayUrl(origin);
                    }
                    break;
                case VodUrlType.Kuai:
                    {
                        // 直接点播
                        VodStyleHelper.PlayUrl(ToXunleiCode(origin));
                    }
                    break;
                case VodUrlType.KuaiXunlei:
                    {
                        GetUrlFromKuaiXunlei(origin);
                    }
                    break;
                case VodUrlType.Flash:
                case VodUrlType.M1905:// http://www.m1905.com/video/v/656294/v.swf
                case VodUrlType.YinFuFlash:
                    {
                        VodStyleHelper.PlayUrl(origin.Replace("\\", ""));
                    }
                    break;
                case VodUrlType.Qiyi:
                case VodUrlType.Tudou:
                case VodUrlType.Youku:
                case VodUrlType.Vqq:
                    {
                        GetFlashUrl(origin, vodType);
                    }
                    break;
                default:
                    {
                        TestVodAction.ShouQi(false);
                    }
                    break;
            }

            #endregion
        }

        private static void GetFlashUrl(string url, VodUrlType vodType)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (!url.StartsWith("http://"))
                {
                    url = "http://" + url;
                }
                using (var getFlash = new System.Net.WebClient())
                {
                    getFlash.Proxy = null;
                    getFlash.Encoding = vodType == VodUrlType.Tudou ? System.Text.Encoding.GetEncoding("GBK") : System.Text.Encoding.UTF8;
                    getFlash.DownloadStringAsync(
                        new System.Uri(url), vodType);
                    getFlash.DownloadStringCompleted += getFlash_DownloadStringCompleted;
                }
            }

        }

        private static void getFlash_DownloadStringCompleted(object sender,
                                                             System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;
            var vodType = e.UserState is VodUrlType ? (VodUrlType) e.UserState : VodUrlType.Ftp;
            var flashname = string.Empty;
            var flashurl = string.Empty;

            switch (vodType)
            {
                case VodUrlType.Youku:
                    {
                        flashname = GetSingle(resultstr, "\n<title>", "</title>\n");
                        if (!string.IsNullOrEmpty(flashname))
                        {
                            var flashnames = flashname.Split('—');
                            if (flashnames.Length > 0)
                            {
                                flashname = flashnames[0];
                                if (!string.IsNullOrEmpty(flashname))
                                {
                                    var flashsubname = GetSingle(resultstr, "<meta name=\"title\" content=\"",
                                                                 "\"> \n<meta name=\"keywords\"");
                                    if (!string.IsNullOrEmpty(flashsubname))
                                    {
                                        if (!flashname.Contains(flashsubname))
                                        {
                                            flashname = flashsubname + "-" + flashname;
                                        }
                                    }
                                }
                            }
                        }
                        flashurl = GetSingle(resultstr, "&swfurl=", "&screenshot=");
                        if (!string.IsNullOrEmpty(flashurl))
                        {
                            // "http://player.youku.com/player.php/sid/XNTk0MzQ4NzEy/v.swf"
                            if (flashurl.Contains("v.swf") && flashurl.Contains("http://"))
                            {

                                if (flashurl.Contains("/"))
                                {
                                    var flashurls = flashurl.Split('/');
                                    if (flashurls.Length >= 6)
                                    {
                                        flashurl = string.Format("http://static.youku.com/v/swf/qplayer.swf?VideoIDS={0}=&isAutoPlay=true&isShowRelatedVideo=false&embedid=-&showAd=0", flashurls[5]);
                                    }
                                }
                            }
                        }

                    }
                    break;
                case VodUrlType.Qiyi:
                    {
                        // http://www.iqiyi.com/dianying/20130614/afe51f0dd7aecfb6.html 
                        flashname = GetSingle(resultstr, "<title>", "</title>");
                        if (!string.IsNullOrEmpty(flashname))
                        {
                            var flashnames = flashname.Split('-');
                            if (flashnames.Length > 0)
                            {
                                flashname = flashnames[0];
                            }
                        }

                        var codecom = GetSingle(resultstr, "data-drama-vid=\"", "\"");
                        if (string.IsNullOrEmpty(codecom)) return;
                        var codecurrenturl =
                            GetSingle(resultstr, "data-drama-currenturl=\"http://www.iqiyi.com/", "\"")
                                .Replace(".html", ".swf");
                        if (string.IsNullOrEmpty(codecurrenturl)) return;
                        var albumid = GetSingle(resultstr, "data-drama-albumid=\"", "\"");
                        if (string.IsNullOrEmpty(albumid)) return;
                        var tvid = GetSingle(resultstr, "data-drama-tvid=\"", "\"");
                        if (string.IsNullOrEmpty(resultstr)) return;
                        var qitanid = GetSingle(resultstr, "data-qitancomment-qitanid=\"", "\"");
                        if (string.IsNullOrEmpty(qitanid)) return;
                        flashurl =
                            string.Format(
                                "http://player.video.qiyi.com/{0}/0/0/{1}-pid=0-ptype=0-albumId={2}-tvId={3}-cnId=1-autoplay=1-qitanId={4}-isDrm=0-isPurchase=0",
                                codecom, codecurrenturl, albumid, tvid, qitanid);
                    }
                    break;
                case VodUrlType.Tudou:
                    {
                        var niid = GetSingle(resultstr, "href=\"http://www.tudou.com/plcover/", "/\"");
                        if (string.IsNullOrEmpty(niid)) return;
                        var acode = GetSingle(resultstr, "acode='", "'\n");
                        if (string.IsNullOrEmpty(acode))
                        {
                            acode = GetSingle(resultstr, "itemData={\r\n\tiid:", "\r\n\t");
                        }
                        if (string.IsNullOrEmpty(acode)) return;
                        flashurl = string.Format("http://www.tudou.com/l/{0}/&iid={1}&autoPlay=true/v.swf", niid, acode);
                        flashname = GetSingle(resultstr, "<title>", "</title>");
                        if (!string.IsNullOrEmpty(flashname))
                        {
                            var flashnames = flashname.Split('_');
                            if (flashnames.Length > 0)
                            {
                                flashname = flashnames[0];
                            }
                        }
                    }
                    break;
                case VodUrlType.Vqq:
                    {
                        // http://v.qq.com/cover/o/ob61q1c2lram4lj.html
                        flashurl = GetSingle(resultstr, "vid:\"", "\"");
                        if (!string.IsNullOrEmpty(flashurl))
                        {
                            flashurl =
                                string.Format(
                                    "http://static.video.qq.com/TPout.swf?vid={0}&exid=k0&showend=1&autoplay=1",
                                    flashurl);
                        }
                        flashname = GetSingle(resultstr, "<title>", "</title>");
                        if (!string.IsNullOrEmpty(flashname))
                        {
                            if (flashname.Contains("-"))
                            {
                                flashname = flashname.Split('-')[0].Trim();
                            }
                        }
                    }
                    break;
            }

            if (string.IsNullOrEmpty(flashurl)) return;
            if (string.IsNullOrEmpty(flashname)) return;
            VodStyleHelper.PlayUrl(flashurl.Replace("\\", ""));
        }

        /// <summary>
        ///     获取单个值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetSingle(string str, string s, string e)
        {
            var rg = new System.Text.RegularExpressions.Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))",
                                                              System.Text.RegularExpressions.RegexOptions.Multiline |
                                                              System.Text.RegularExpressions.RegexOptions.Singleline);
            string ss = rg.Match(str).Value;
            return string.IsNullOrEmpty(ss) ? null : ss;
        }

        /// <summary>
        /// 获取快传地址 - 请求网页数据
        /// </summary>
        /// <param name="url"></param>
        private static void GetUrlFromKuaiXunlei(string url)
        {
            if (!url.StartsWith("http://"))
            {
                url = "http://" + url;
            }
            using (var wc = new System.Net.WebClient())
            {
                wc.Proxy = null;
                wc.DownloadStringAsync(new System.Uri(url));
                wc.DownloadStringCompleted += wc_DownloadStringCompleted;
            }
        }

        /// <summary>
        /// 获取快传地址 - 解析网页数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void wc_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;
            var url = GetSingle(resultstr,
                                "style=\"width:144px;text-align:left;\">\r\n                                                                            \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<a class=\"btn_yulan_sml_hui\" href=\"javascript:;\"",
                                "class=\"this_fname\">");
            if (!string.IsNullOrEmpty(url))
            {
                url = GetSingle(url, "\"this_gcid\">\r\n\t\t\t\t\t\t\t\t\t\t\t<input type=\"hidden\" value=\"",
                                "\" class=\"this_url\">\r\n");
                if (string.IsNullOrEmpty(url)) return;
                // 直接点播
                VodStyleHelper.PlayUrl(ToXunleiCode(url));
            }
        }

        /// <summary>
        /// 转码成为迅雷支持的编码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string ToXunleiCode(string url)
        {
            return System.Web.HttpUtility.UrlEncode(url, System.Text.Encoding.GetEncoding("GB2312"));
        }

        /// <summary>
        /// 辨别链接类型
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static VodUrlType AnalyzeVodType(string origin)
        {
            #region 辨别链接类型

            // BT直接连接
            // bt://1E7D080D585B70461CFC17B30F552A594424A750%2F7
            // bt://1E7D080D585B70461CFC17B30F552A594424A750
            if (origin.Contains("tudou.com"))
            {
                return VodUrlType.Tudou;
            }
            if (origin.Contains("v.qq.com"))
            {
                return VodUrlType.Vqq;
            }
            if (origin.Contains("youku.com"))
            {
                return VodUrlType.Youku;
            }
            if (origin.Contains("iqiyi.com"))
            {
                return VodUrlType.Qiyi;
            }
            if (origin.Contains("http://yinfu.cc/v/"))
            {
                return VodUrlType.YinFuFlash;
            }
            if (origin.Contains("m1905.com"))
            {
                return VodUrlType.M1905;
            }
            if (origin.StartsWith("http://192.168"))
            {
                return VodUrlType.Kuai;
            }
            if (origin.Contains("kuai.xunlei.com"))
            {
                return VodUrlType.KuaiXunlei;
            }
            if (origin.Contains("ffdy.cc"))
            {
                return VodUrlType.Ffdycc;
            }
            if (origin.StartsWith("bt://") && origin.Length >= 45)
            {
                return VodUrlType.BtHash;
            }
            if (origin.EndsWith(".swf"))
            {
                return VodUrlType.Flash;
            }
            // 本地种子链接 C://1.torrent
            if (System.IO.File.Exists(origin) && System.IO.Path.GetExtension(origin) == ".torrent")
            {
                return VodUrlType.LocalBt;
            }
            // 在线种子链接 http://www.baidu.com/1.torrent
            if ((origin.StartsWith("http://") || origin.StartsWith("https://") || origin.StartsWith("www.")) &&
                origin.EndsWith(".torrent"))
            {
                return VodUrlType.WebBt;
            }
            // Hash值链接 1E7D080D585B70461CFC17B30F552A594424A750
            if (!(origin.StartsWith("http://") || origin.StartsWith("https://") || origin.StartsWith("www.")) &&
                (origin.Length >= 40 && origin.Length <= 50))
            {
                return VodUrlType.Hash;
            }
            // 迅雷链接
            if (origin.ToLower().StartsWith("thunder://"))
            {
                return VodUrlType.Thunder;
            }
            // 快车链接
            if (origin.ToLower().StartsWith("flashget://"))
            {
                return VodUrlType.Flashget;
            }
            // FTP链接
            if (origin.ToLower().StartsWith("ftp://"))
            {
                return VodUrlType.Ftp;
            }
            // 磁力链接
            if (origin.ToLower().StartsWith("magnet:?"))
            {
                return VodUrlType.Magnet;
            }
            // QQ旋风链接
            if (origin.ToLower().StartsWith("qqdl://"))
            {
                return VodUrlType.QQdl;
            }
            // 快播链接
            if (origin.ToLower().StartsWith("qvod//"))
            {
                return VodUrlType.QQdl;
            }
            // ED2K链接
            return origin.ToLower().StartsWith("ed2k://")
                       ? VodUrlType.Ed2K
                       : VodUrlType.None;

            #endregion
        }

        /// <summary>
        /// 读取本地BT种子
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string AnalyzeLocalBt(string origin)
        {
            #region 读取本地

            try
            {
                int tn = 0;
                bool flag = true;
                var tp = new TorrentParser(origin);
                var btfiles = tp.Files;
                for (int i = 0; i < btfiles.Count; i++)
                {
                    foreach (var videos in VFilter)
                    {
                        foreach (string items in videos.Value)
                        {
                            if (!btfiles[i].Name.EndsWith(items) || !flag) continue;
                            tn = i;
                            flag = false;
                        }
                    }
                }
                return string.Format("bt://{0}/{1}", ToXunleiCode(tp.ShaHash).ToUpper(), tn);
            }
            catch
            {
                return string.Empty;
            }

            #endregion
        }

        /// <summary>
        /// 解析Hash值
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string AnalyzeHash(string origin)
        {
            #region 解析Hash值

            if (!origin.EndsWith("/0"))
            {
                origin += "/0";
            }
            return "bt://" +
                   System.Web.HttpUtility.UrlEncode(origin, System.Text.Encoding.GetEncoding("GB2312")).ToUpper();

            #endregion
        }

        /// <summary>
        /// 解析QQ旋风链
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string DeQQdl(string origin)
        {
            #region 解析QQ旋风链

            try
            {
                return System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(origin.Remove(0, 7)));
            }
            catch (System.Exception)
            {
                return string.Empty;
            }

            #endregion
        }

/*
        /// <summary>
        /// 生成QQ旋风链
        /// </summary>
        /// <param name="Origin"></param>
        /// <returns></returns>
        private static string EnQQdl(string Origin)
        {
            #region 生成QQ旋风链
            try
            {
                return "qqdl://" + Convert.ToBase64String(Encoding.Default.GetBytes(Origin));
            }
            catch (Exception)
            {
                return String.Empty;
            } 
            #endregion
        }
*/

        /// <summary>
        /// 解析快车链接
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string DeFlashget(string origin)
        {
            #region 解析快车链接

            try
            {
                return
                    System.Text.Encoding.Default.GetString(
                        System.Convert.FromBase64String(origin.Remove(0, 11).Split('&')[0]))
                          .Replace("[FLASHGET]", "");
            }
            catch (System.Exception)
            {
                return string.Empty;
            }

            #endregion
        }

/*
        /// <summary>
        /// 生成快车链接
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string EnFlashget(string origin)
        {
            #region 生成快车链接
            try
            {
                return "Flashget://" +
                       Convert.ToBase64String(Encoding.Default.GetBytes(String.Format("[FLASHGET]{0}[FLASHGET]", origin))) +
                       "&";
            }
            catch (Exception)
            {
                return String.Empty;
            } 
            #endregion
        }
*/

        /// <summary>
        /// 解析迅雷链接
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string DeThunder(string origin)
        {
            #region 解析迅雷链接

            try
            {
                return
                    System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(origin.Remove(0, 10)))
                          .Replace("A", "")
                          .Replace("Z", "");
            }
            catch (System.Exception)
            {
                return string.Empty;
            }

            #endregion
        }

        /// <summary>
        /// 生成迅雷链接
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static string EnThunder(string origin)
        {
            #region 生成迅雷链接

            try
            {
                return "Thunder://" +
                       System.Convert.ToBase64String(
                           System.Text.Encoding.Default.GetBytes(string.Format("AA{0}ZZ", origin)));
            }
            catch (System.Exception)
            {
                return string.Empty;
            }

            #endregion
        }

        #region 解析磁力链接

/*
        /// <summary>
        /// 获取磁力链接数据 - 请求磁力链接网页数据
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static void GetMagnet(string origin)
        {
            var hash = origin.Split('&')[0].Split(':')[3].ToUpper();
            using (var getMagnet = new System.Net.WebClient())
            {
                getMagnet.Proxy = null;
                getMagnet.DownloadStringAsync(
                    new System.Uri(string.Format("http://i.vod.xunlei.com/req_subBT/info_hash/{0}/req_num/1000/req_offset/0",
                                          hash.ToUpper()))
                    , hash);
                getMagnet.DownloadStringCompleted += getMagnet_DownloadStringCompleted;
            }
        }
*/

        /// <summary>
        /// 获取磁力链接数据 - 解析磁力链接网页数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void getMagnet_DownloadStringCompleted(object sender,
                                                              System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;
            var hash = e.UserState as string;
            if (string.IsNullOrEmpty(hash)) return;
            var url = string.Format("bt://{0}/{1}", hash,
                                    System.Text.RegularExpressions.Regex.Matches(resultstr, "(?<=index\": ).*?(?=,)")[0]
                                        .Value);
            // 直接点播
            VodStyleHelper.PlayUrl(url);
        }

        private static string GetMagnetIndex(string hash)
        {
            using (var wc = new System.Net.WebClient())
            {
                wc.Proxy = null;
                var wcstr = wc.DownloadString("http://i.vod.xunlei.com/req_subBT/info_hash/" + hash.ToUpper() +
                                              "/req_num/1000/req_offset/0");
                if (!string.IsNullOrEmpty(wcstr))
                {
                    return "/" + System.Text.RegularExpressions.Regex.Matches(wcstr, "(?<=index\": ).*?(?=,)")[0].Value;
                }
            }
            return string.Empty;
        }

        #endregion

        // 影片后缀字典
        public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> VFilter = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>
            {
                {"3GP 移动视频", new System.Collections.Generic.List<string> {".3gp", ".3g2", ".3gp2", ".3gpp"}},
                {"Advanced Streaming 视频", new System.Collections.Generic.List<string> {".asf"}},
                {"AVI 视频剪辑", new System.Collections.Generic.List<string> {".avi"}},
                {"Flash 视频", new System.Collections.Generic.List<string> {".f4v", ".flv", ".swf"}},
                {"Matroska 视频", new System.Collections.Generic.List<string> {".mkv"}},
                {"QuickTime 视频", new System.Collections.Generic.List<string> {".mov", ".qt"}},
                {
                    "MPEG 视频",
                    new System.Collections.Generic.List<string> {".mp4", ".mpeg", ".mpg", ".mpv", ".m1v", ".m2v", ".m4v", ".pss", ".pva", ".tpr"}
                },
                {"OGG 视频", new System.Collections.Generic.List<string> {".ogm"}},
                {"PMP 视频", new System.Collections.Generic.List<string> {".pmp"}},
                {"RealMedia 视频", new System.Collections.Generic.List<string> {".rm", ".rmvb"}},
                {"DVD 视频", new System.Collections.Generic.List<string> {".vob"}},
                {"WebM 视频", new System.Collections.Generic.List<string> {".webm"}},
                {"Windows Media 视频", new System.Collections.Generic.List<string> {".wmv"}},
                {"HDTV 视频", new System.Collections.Generic.List<string> {".tp", ".ts", ".m2ts", ".mts", ".evo"}},
                {"AviSynth 视频", new System.Collections.Generic.List<string> {".avs"}},
                {"Bink 视频", new System.Collections.Generic.List<string> {".bik"}},
                {"VCD 视频", new System.Collections.Generic.List<string> {".dat"}},
                {"DIVX 视频", new System.Collections.Generic.List<string> {".divx"}},
                {"VP6/VP7 视频", new System.Collections.Generic.List<string> {".vp6"}},
            };
    }
}
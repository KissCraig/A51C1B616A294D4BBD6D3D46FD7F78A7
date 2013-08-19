
namespace KCPlayer.Plugin.TestVod.Helper
{
    public class VodStyleHelper
    {
        // ftp://dy:dy@xlj.2tu.cc:20392/[迅雷下载www.2tu.cc]青春海滩大电影.BD1024超清中英双字.mkv
        // ftp://dy:dy@xlj.2tu.cc:30399/[迅雷下载www.2tu.cc]青春海滩大电影.HD1024高清中英双字.rmvb
        // ftp://dy:dy@xlj.2tu.cc:40376/[迅雷下载www.2tu.cc]青春海滩大电影.HD1280高清中英双字.rmvb
        // ftp://dy:dy@xlj.2tu.cc:50374/[迅雷下载www.2tu.cc]青春海滩大电影.HD1280超清中英双字.mkv

        /// <summary>
        /// 播放视频地址
        /// </summary>
        /// <param name="url"></param>
        public static void PlayUrl(string url)
        {
            #region 播放视频地址
            if (string.IsNullOrEmpty(url)) return;
            PublicStatic.NowVodPathUrl = url;
            // 如果是在线视频，那么就直接用在线视频
            if (PublicStatic.NowVodPathUrl.Contains(".swf"))
            {
                WebBrowerHeper.PlayByFlash();
            }
            // 如果是资源链接，那么就转资源播放
            else
            {
                UpdateThisPlay();
            }
            #endregion
        }

        #region 不同核心，不同函数
        private static void PlayByXunLeiHuiYuan()
        {
            var nowVodPathUrl = string.Format("http://vod.xunlei.com/mini.html?url={0}", PublicStatic.NowVodPathUrl);
            if (PublicStatic.NowUserOne.IsVip)
            {
                // 两个最重要的参数
                InternetSetCookie(nowVodPathUrl, "sessionid", PublicStatic.NowUserOne.Sessionid);
                InternetSetCookie(nowVodPathUrl, "userid", PublicStatic.NowUserOne.Userid);
            }
            WebBrowerHeper.PlayNavigateUri(nowVodPathUrl);
        }

        private static void PlayByOkDvd()
        {
            var nowVodPathUrl = string.Format("http://www.okdvd.com/api.html?url={0}", PublicStatic.NowVodPathUrl);
            WebBrowerHeper.PlayNavigateUri(nowVodPathUrl);
        }
        private static void PlayByHdp4P()
        {
            var nowVodPathUrl = string.Format("http://www.hdp4p.com/vod/#URL={0}", PublicStatic.NowVodPathUrl);
            WebBrowerHeper.PlayNavigateUri(nowVodPathUrl);
        }

        private static void PlayByBteye()
        {
            var nowVodPathUrl = string.Format("http://www.bteye.com/#u={0}", PublicStatic.NowVodPathUrl);
            WebBrowerHeper.PlayNavigateUri(nowVodPathUrl);
        }

        private static void PlayByHappyVod()
        {
            var nowVodPathUrl = string.Format("http://www.happyfuns.com/happyvod/api.html?v=1.06&url={0}", PublicStatic.NowVodPathUrl);
            WebBrowerHeper.PlayNavigateUri(nowVodPathUrl);
        }

        /// <summary>
        /// 开始下载火焰数据
        /// </summary>
        private static void PlayByHuoYan()
        {
            #region 开始下载火焰数据
            var nowVodPathUrl = string.Format("http://{0}.huoyan.tv/?u={1}", PublicStatic.NowTestVodPlayAPI, PublicStatic.NowVodPathUrl);
            using (var huoYan = new ResponseUriClient())
            {
                huoYan.Encoding = System.Text.Encoding.GetEncoding("gb2312");
                huoYan.Headers.Add("Cookie", "pgv_pvi=2704140288; pgv_si=s1284784128");
                huoYan.DownloadStringAsync(new System.Uri(nowVodPathUrl));
                huoYan.DownloadStringCompleted += HuoYan_DownloadStringCompleted;
            } 
            #endregion
        }

        /// <summary>
        /// 解析火焰数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void HuoYan_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            #region 解析火焰数据
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled || !LoadNewHuoYanPlayer(e.Result))
            {
                // 切换到下一个通道
                UpdateNextTongDao();
                // 更新通道名称
                UpdateTongDao();
                // 重新播放一次
                UpdateThisPlay();
            } 
            #endregion
        }

        /// <summary>
        /// 解析并加载火焰播放器资源
        /// </summary>
        /// <param name="resultstr"></param>
        /// <returns></returns>
        private static bool LoadNewHuoYanPlayer(string resultstr)
        {
            #region 解析并加载火焰播放器资源
            if (string.IsNullOrEmpty(resultstr)) return false;
            var iframinnerhtml = StringRegexHelper.GetSingle(resultstr, "<iframe", "</iframe>");
            if (string.IsNullOrEmpty(iframinnerhtml)) return false;
            var iframsrc = StringRegexHelper.GetSingle(iframinnerhtml, "src=\"", "\"");
            if (string.IsNullOrEmpty(iframsrc)) return false;
            iframinnerhtml = "<iframe scrolling=\"no\" frameborder=\"0\" margin=\"0\"  padding=\"0\" name=\"apiwind\" id=\"apiwind\" rel=\"nofollow\" border=\"0\" style=\"background:#000; word-wrap: break-word; width: 100%; height: 100%\" src=" + iframsrc + "</iframe>";
            if (string.IsNullOrEmpty(iframinnerhtml)) return false;
            iframinnerhtml = "<!DOCTYPE html><html><head><title></title><style>*{margin:0px;padding:0px;height:100%;width:100%;}</style></head>" +
                             "<body>" + iframinnerhtml + "</body></html>";
            return !string.IsNullOrEmpty(iframinnerhtml) && WebBrowerHeper.PlayDocumentText(iframinnerhtml);

            #endregion
        }
        #endregion

        #region 不同通道，不同方式
        /// <summary>
        /// 更新通道名称
        /// </summary>
        public static void UpdateTongDao()
        {
            #region 更新通道名称
            if (string.IsNullOrEmpty(PublicStatic.NowTestVodPlayAPI)) return;
            switch (PublicStatic.NowTestVodPlayAPI)
            {
                case ConstStyleHelper.燃烧版:
                    {
                        PublicStatic.TestVodNavLeft.ListItemTxts = new System.Collections.Generic.List<object>
                        {
                            ConstStyleHelper.可拖不断流+",Select",ConstStyleHelper.稳定不可拖,ConstStyleHelper.高速半可拖,ConstStyleHelper.可拖会断流,ConstStyleHelper.单种多视频,ConstStyleHelper.白金共享号,ConstStyleHelper.白金独享号
                        };
                    }
                    break;
                case ConstStyleHelper.稳定版:
                    {
                        PublicStatic.TestVodNavLeft.ListItemTxts = new System.Collections.Generic.List<object>
                        {
                            ConstStyleHelper.可拖不断流,ConstStyleHelper.稳定不可拖+",Select",ConstStyleHelper.高速半可拖,ConstStyleHelper.可拖会断流,ConstStyleHelper.单种多视频,ConstStyleHelper.白金共享号,ConstStyleHelper.白金独享号
                        };
                    }
                    break;
                case ConstStyleHelper.高速版:
                    {
                        PublicStatic.TestVodNavLeft.ListItemTxts = new System.Collections.Generic.List<object>
                        {
                            ConstStyleHelper.可拖不断流,ConstStyleHelper.稳定不可拖,ConstStyleHelper.高速半可拖+",Select",ConstStyleHelper.可拖会断流,ConstStyleHelper.单种多视频,ConstStyleHelper.白金共享号,ConstStyleHelper.白金独享号
                        };
                    }
                    break;
                case ConstStyleHelper.拖动版:
                    {
                        PublicStatic.TestVodNavLeft.ListItemTxts = new System.Collections.Generic.List<object>
                        {
                            ConstStyleHelper.可拖不断流,ConstStyleHelper.稳定不可拖,ConstStyleHelper.高速半可拖,ConstStyleHelper.可拖会断流+",Select",ConstStyleHelper.单种多视频,ConstStyleHelper.白金共享号,ConstStyleHelper.白金独享号
                        };
                    }
                    break;
                case ConstStyleHelper.快乐版:
                    {
                        PublicStatic.TestVodNavLeft.ListItemTxts = new System.Collections.Generic.List<object>
                        {
                            ConstStyleHelper.可拖不断流,ConstStyleHelper.稳定不可拖,ConstStyleHelper.高速半可拖,ConstStyleHelper.可拖会断流,ConstStyleHelper.单种多视频+",Select",ConstStyleHelper.白金共享号,ConstStyleHelper.白金独享号
                        };
                    }
                    break;
                case ConstStyleHelper.共享版:
                    {
                        PublicStatic.TestVodNavLeft.ListItemTxts = new System.Collections.Generic.List<object>
                        {
                            ConstStyleHelper.可拖不断流,ConstStyleHelper.稳定不可拖,ConstStyleHelper.高速半可拖,ConstStyleHelper.可拖会断流,ConstStyleHelper.单种多视频,ConstStyleHelper.白金共享号+",Select",ConstStyleHelper.白金独享号
                        };
                    }
                    break;
                case ConstStyleHelper.独享版:
                    {
                        PublicStatic.TestVodNavLeft.ListItemTxts = new System.Collections.Generic.List<object>
                        {
                            ConstStyleHelper.可拖不断流,ConstStyleHelper.稳定不可拖,ConstStyleHelper.高速半可拖,ConstStyleHelper.可拖会断流,ConstStyleHelper.单种多视频,ConstStyleHelper.白金共享号,ConstStyleHelper.白金独享号+",Select"
                        };
                    }
                    break;
            }
            PublicStatic.TestVodNavLeft.UpdateListItem();
            #endregion
        }

        /// <summary>
        /// 选择切换通道
        /// </summary>
        /// <param name="tongdaokey"></param>
        public static void UpdateThisTongDao(string tongdaokey)
        {
            #region 选择切换通道
            switch (tongdaokey)
            {
                case ConstStyleHelper.可拖不断流:
                    {
                        // 切换到燃烧版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.燃烧版;
                    }
                    break;
                case ConstStyleHelper.稳定不可拖:
                    {
                        // 切换到稳定版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.稳定版;
                    }
                    break;
                case ConstStyleHelper.高速半可拖:
                    {
                        // 切换到高速版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.高速版;
                    }
                    break;
                case ConstStyleHelper.可拖会断流:
                    {
                        // 切换到拖动版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.拖动版;
                    }
                    break;
                case ConstStyleHelper.单种多视频:
                    {
                        // 切换到快乐版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.快乐版;
                    }
                    break;
                case ConstStyleHelper.白金共享号:
                    {
                        // 切换到共享版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.共享版;
                    }
                    break;
                case ConstStyleHelper.白金独享号:
                    {
                        // 切换到独享版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.独享版;
                    }
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 选择播放通道
        /// </summary>
        public static void UpdateThisPlay()
        {
            #region 选择播放通道
            if (string.IsNullOrEmpty(PublicStatic.NowTestVodPlayAPI)) return;
            switch (PublicStatic.NowTestVodPlayAPI)
            {
                case ConstStyleHelper.燃烧版:
                case ConstStyleHelper.稳定版:
                case ConstStyleHelper.高速版:
                case ConstStyleHelper.拖动版:
                    {
                        PlayByHuoYan();
                    }
                    break;
                case ConstStyleHelper.快乐版:
                    {
                        PlayByHappyVod();
                    }
                    break;
                case ConstStyleHelper.独享版:
                    {
                        PlayByXunLeiHuiYuan();
                    }
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 切换到下一个通道
        /// </summary>
        public static void UpdateNextTongDao()
        {
            #region 切换到下一个通道

            switch (PublicStatic.NowTestVodPlayAPI)
            {
                case ConstStyleHelper.燃烧版:
                    {
                        // 切换到稳定版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.稳定版; // 稳定版
                    }
                    break;
                case ConstStyleHelper.稳定版:
                    {
                        // 切换到高速版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.高速版; // 高速版
                    }
                    break;
                case ConstStyleHelper.高速版:
                    {
                        // 切换到拖动版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.拖动版; // 拖动版
                    }
                    break;
                case ConstStyleHelper.拖动版:
                    {
                        // 切换到快乐版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.快乐版; // 快乐版
                    }
                    break;
                case ConstStyleHelper.快乐版:
                    {
                        // 切换到燃烧版
                        PublicStatic.NowTestVodPlayAPI = ConstStyleHelper.燃烧版; // 燃烧版
                    }
                    break;
            }

            #endregion
        } 
        #endregion

        #region 浏览器基本设置
        [System.Runtime.InteropServices.DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        private static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName, string lpszCookieData);

        #endregion
    }
}

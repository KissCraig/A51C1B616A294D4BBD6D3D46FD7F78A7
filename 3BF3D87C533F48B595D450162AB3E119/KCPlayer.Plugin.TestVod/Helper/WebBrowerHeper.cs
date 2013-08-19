using KCPlayer.Plugin.TestVod.Controls;

namespace KCPlayer.Plugin.TestVod.Helper
{
    public class WebBrowerHeper
    {
        /// <summary>
        /// 播放Flash
        /// </summary>
        /// <returns></returns>
        public static bool PlayByFlash()
        {
            try
            {
                PublicStatic.VodBrowser.Navigate("about:blank");
                PublicStatic.VodBrowser.DocumentText = "";
                PublicStatic.VodBrowser.Navigate(PublicStatic.NowVodPathUrl);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 播放文本
        /// </summary>
        /// <param name="iframinnerhtml"></param>
        /// <returns></returns>
        public static bool PlayDocumentText(string iframinnerhtml)
        {
            try
            {
                PublicStatic.VodBrowser.Navigate("about:blank");
                PublicStatic.VodBrowser.DocumentText = "";
                PublicStatic.VodBrowser.DocumentText = iframinnerhtml;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 播放地址
        /// </summary>
        /// <param name="uripath"></param>
        /// <returns></returns>
        public static bool PlayNavigateUri(string uripath)
        {
            try
            {
                PublicStatic.VodBrowser.Navigate("about:blank");
                PublicStatic.VodBrowser.DocumentText = "";
                PublicStatic.VodBrowser.Navigate(uripath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 过滤广告和一不用 内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void LiuXingCon_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            #region 过滤广告和一些内容

            System.Windows.Forms.HtmlDocument htmlDocument = null;
            try
            {
                var enBrowser = sender as EnBrowser;
                if (enBrowser != null) htmlDocument = enBrowser.Document;
            }
            catch
            {
                htmlDocument = null;
            }
            // 屏幕右下角广告
            if (htmlDocument == null) return;
            if (htmlDocument.Window != null)
            {
                htmlDocument.Window.Error += Window_Error;
            }


            #region 去广告
            var blocklistForScript = new System.Collections.Generic.List<string>
            {
                "cnzz.com",
                "7794.com",
                "qiyou.com",
                "tajs.qq.com",
                "share.baidu.com",
            };
            var scriptv = htmlDocument.GetElementsByTagName("SCRIPT");
            if (scriptv.Count > 0)
            {
                for (var i = scriptv.Count - 1; i > 0; i--)
                {
                    var script = scriptv[i];
                    var hasblock = false;
                    if (!string.IsNullOrEmpty(script.InnerHtml))
                    {
                        foreach (var blocklist in blocklistForScript)
                        {
                            if (script.GetAttribute("src").Contains(blocklist))
                            {
                                hasblock = true;
                            }
                        }
                    }

                    if (hasblock)
                    {
                        script.Style = "";
                        script.InnerHtml = "";
                        script.InnerText = "";
                        script.OuterHtml = "";
                        script.OuterText = "";
                        script.SetAttribute("src", "");
                    }
                }
            }

            #endregion

            switch (PublicStatic.NowTestVodPlayAPI)
            {
                case ConstStyleHelper.快乐版:
                    {
                        #region case TestVodPlayStyle.HappyVod:

                        var a = htmlDocument.GetElementsByTagName("A");
                        if (a.Count > 0)
                        {
                            foreach (System.Windows.Forms.HtmlElement aa in a)
                            {
                                if (aa.GetAttribute("title").Contains("稳定"))
                                {
                                    aa.InnerText = "不断流";
                                }
                                if (aa.GetAttribute("title").Contains("限速"))
                                {
                                    aa.InnerText = "做备胎";
                                }
                                if (aa.GetAttribute("title") == "可拖放")
                                {
                                    aa.InnerText = "速度快";
                                }
                            }
                        }

                        #endregion
                    }
                    break;
                case ConstStyleHelper.噢科版:
                    {
                        #region case TestVodPlayStyle.OkDvd:

                        // var body = htmlDocument.Url;
                        var script = htmlDocument.GetElementsByTagName("SCRIPT");
                        foreach (System.Windows.Forms.HtmlElement he in script)
                        {
                            if (he.GetAttribute("src").Contains("cnzz.com") ||
                                he.GetAttribute("src").Contains("7794.com"))
                            {
                                he.SetAttribute("src", "");
                            }
                        }
                        var a = htmlDocument.GetElementsByTagName("a");
                        foreach (System.Windows.Forms.HtmlElement aa in a)
                        {
                            if (aa.GetAttribute("title").Contains("稳定"))
                            {
                                aa.InnerHtml = "不断流";
                            }
                            if (aa.GetAttribute("title").Contains("限速"))
                            {
                                aa.InnerHtml = "做备胎";
                            }
                            if (aa.GetAttribute("title") == "可拖放")
                            {
                                aa.InnerHtml = "速度快";
                            }
                        }

                        #endregion
                    }
                    break;
            }

            #endregion
        }

        private static void Window_Error(object sender, System.Windows.Forms.HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }

        public static void LiuXingCon_BeforeNewWindow(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            e.Cancel = true;
            //return;
            //((EnBrowser) sender).Navigate(e.Url);
        }
    }
}

using System.Windows.Forms;
using KCP.Control.Base;
using KCP.Control.Gase;

namespace KCP.Plugin.WatchTV
{
    public class WatchTvStart
    {
        /// <summary>
        /// 添加导航面板和内容面板
        /// </summary>
        private static void LoadPal2Pal()
        {
            #region 添加导航面板和内容面板

            if (PublicStatic.LiuXingPal == null) return;
            // 导航面板
            PublicStatic.LiuXingNav = new EFlyPal
                {
                    Size = new System.Drawing.Size(PublicStatic.LiuXingPal.Width, 189),
                    Dock = DockStyle.Top,
                    Location = new System.Drawing.Point(0, 0),
                    BackColor = System.Drawing.Color.FromArgb(20, 0, 0, 0),
                };
            PublicStatic.LiuXingPal.Controls.Add(PublicStatic.LiuXingNav);

            // 内容面板
            PublicStatic.LiuXingCon = new EnBrowser
                {
                    ScriptErrorsSuppressed = true,
                    AllowWebBrowserDrop = false,
                    ScrollBarsEnabled = true,
                    IsWebBrowserContextMenuEnabled = false,
                    Dock = DockStyle.Fill,
                    Location = new System.Drawing.Point(0, PublicStatic.LiuXingNav.Height + 3),
                };
            PublicStatic.LiuXingPal.Controls.Add(PublicStatic.LiuXingCon);
            PublicStatic.LiuXingCon.DocumentCompleted += LiuXingCon_DocumentCompleted;
            PublicStatic.LiuXingCon.BeforeNewWindow += LiuXingCon_BeforeNewWindow;

            #endregion
        }

        #region About Browser

        private static void LiuXingCon_BeforeNewWindow(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            e.Cancel = true;
            // ((Controls.EnBrowser)sender).Navigate(e.Url);
        }

        private static void LiuXingCon_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var htmlDocument = ((EnBrowser) sender).Document;
            if (htmlDocument != null)
                if (htmlDocument.Window != null)
                    htmlDocument.Window.Error += Window_Error;
        }

        private static void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }

        #endregion

        /// <summary>
        /// 初始化进入
        /// </summary>
        public static void LoadLiuXing()
        {
            // 载入面板
            LoadPal2Pal();
            // 载入导航
            Nav.StartNav.AddLiuXingNav();
        }
    }
}
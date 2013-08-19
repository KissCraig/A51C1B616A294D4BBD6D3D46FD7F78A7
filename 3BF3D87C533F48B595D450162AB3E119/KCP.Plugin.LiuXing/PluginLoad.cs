using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing
{
    public class PluginLoad
    {
        #region public Code

        /// <summary>
        /// 初始化进入
        /// </summary>
        public static void LoadLiuXing()
        {
            if (!InitializePanel.InitializeMainPanel()) return;
            if (!InitializePanel.InitializeAsidePanel()) return;
            if (!InitializePanel.InitializeTopNavPanel()) return;
            if (!InitializePanel.InitializePanelAction()) return;
            if (!InitializePanel.InitializeGlobalPara()) return;
            new System.Threading.Tasks.Task(StartToVod).Start();
        }

        /// <summary>
        /// 启动点播
        /// </summary>
        public static void StartToVod()
        {
            // 初始清理
            PublicStatic.LiuXingCon.Controls.Clear();
            switch (PublicStatic.CurrentSite)
            {
                case MovieSite.Xunbo:
                    {
                        #region MovieSite.Xunbo:

                        new ListStart(PublicStatic.AnPageNum, PublicStatic.AnTypeNum, new LiuXingType()
                            {
                                Encoding = System.Text.Encoding.Default,
                                Proxy = PublicStatic.MyProxy,
                                Type = LiuXingEnum.XunboListItem
                            });
                        new ListStart(PublicStatic.AnPageNum + 1, PublicStatic.AnTypeNum, new LiuXingType()
                            {
                                Encoding = System.Text.Encoding.Default,
                                Proxy = PublicStatic.MyProxy,
                                Type = LiuXingEnum.XunboListItem
                            });

                        #endregion
                    }
                    break;
                case MovieSite.YYets:
                    {
                        #region MovieSite.YYets:

                        new ListStart(PublicStatic.AnPageNum, PublicStatic.AnTypeNum, new LiuXingType()
                            {
                                Encoding = System.Text.Encoding.UTF8,
                                Proxy = PublicStatic.MyProxy,
                                Type = LiuXingEnum.YYetListItem
                            });
                        new ListStart(PublicStatic.AnPageNum + 1, PublicStatic.AnTypeNum, new LiuXingType()
                            {
                                Encoding = System.Text.Encoding.UTF8,
                                Proxy = PublicStatic.MyProxy,
                                Type = LiuXingEnum.YYetListItem
                            });

                        #endregion
                    }
                    break;
            }
        }

        #endregion

        #region Private Code


        private static void LiuXingCon_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            PublicStatic.LiuXingCon.Update();
            PublicStatic.LiuXingCon.Refresh();
        }

        /// <summary>
        /// 在导航面板聚焦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void LiuXingNav_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            PublicStatic.LiuXingCon.Focus();
        }

        /// <summary>
        /// 在内容导航右键换页，左键聚焦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void LiuXingCon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Clicks < 0) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                LiuXingNextPage();
            }
            else
            {
                PublicStatic.LiuXingCon.Focus();
            }
        }


        /// <summary>
        /// 执行下一页
        /// </summary>
        public static void LiuXingPrevPage()
        {
            if (PublicStatic.AnPageNum - 2 <= 1)
            {
                PublicStatic.AnPageNum = 1;
            }
            else
            {
                PublicStatic.AnPageNum = PublicStatic.AnPageNum - 2;
            }

            if (PublicStatic.CurrentSite == MovieSite.YYets)
            {
                StartToVod();
            }
            if (PublicStatic.CurrentSite == MovieSite.Xunbo)
            {
                StartToVod();
            }
        }

        /// <summary>
        /// 执行下一页
        /// </summary>
        public static void LiuXingNextPage()
        {
            PublicStatic.AnPageNum = PublicStatic.AnPageNum + 2;
            if (PublicStatic.CurrentSite == MovieSite.YYets)
            {
                StartToVod();
            }
            if (PublicStatic.CurrentSite == MovieSite.Xunbo)
            {
                StartToVod();
            }
        }

        #endregion
    }
}
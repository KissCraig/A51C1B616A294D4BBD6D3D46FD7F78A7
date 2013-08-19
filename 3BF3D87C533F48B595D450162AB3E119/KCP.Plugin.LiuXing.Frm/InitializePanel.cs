using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Frm
{
    public class InitializePanel
    {
        /// <summary>
        /// 初始化主面板
        /// </summary>
        /// <returns></returns>
        public static bool InitializeMainPanel()
        {
            #region 初始化主面板

            // 检查 插件全局 主承载面板
            if (PublicStatic.LiuXingPal == null) return false;

            // 插件全局 主导航面板
            PublicStatic.LiuXingNav = new Control.Fase.LFlyPal(
                PublicStatic.LiuXingPal,
                new System.Drawing.Size(PublicStatic.LiuXingPal.Width+100, 40),
                new System.Drawing.Point(5, 0),
                BaseAnchor.AnchorTopFill
                ){AutoScroll = false};

            // 插件全局 主侧面面板
            PublicStatic.LiuXingAside = new Control.Fase.LPanel(
                PublicStatic.LiuXingPal,
                0,
                new System.Drawing.Size(109, PublicStatic.LiuXingPal.Height - PublicStatic.LiuXingNav.Height),
                new System.Drawing.Point(0, PublicStatic.LiuXingNav.Height),
                System.Drawing.Color.Transparent,
                System.Drawing.Color.Transparent,
                BaseAnchor.AnchorLeftFill
                );

            // 插件全局 流动内容面板
            PublicStatic.LiuXingCon = new Control.Fase.LFlyPal(
                PublicStatic.LiuXingPal,
                new System.Drawing.Size(PublicStatic.LiuXingPal.Width - PublicStatic.LiuXingAside.Width,
                                            PublicStatic.LiuXingPal.Height - PublicStatic.LiuXingNav.Height),
                                            new System.Drawing.Point(5 + PublicStatic.LiuXingAside.Width, PublicStatic.LiuXingNav.Height),
                BaseAnchor.AnchorFill
                );

            return true; 
            #endregion
        }
        /// <summary>
        /// 初始化顶导航面板
        /// </summary>
        /// <returns></returns>
        public static bool InitializeTopNavPanel()
        {
            #region 初始化顶导航面板

            if (PublicStatic.LiuXingNav == null) return false;

            foreach (var mapKey in BandingNavMap.NavMap)
            {
                if (mapKey.Key == BandingNavMap.Map.WuHeSouSuo)
                {
                    PublicStatic.SearchBox = new Control.Gase.InputSearch(
                    PublicStatic.LiuXingNav,
                    1,
                    1,
                    mapKey.Value,
                    "",
                    new System.Drawing.Font(PublicStatic.MainFont, 12.5F),
                    new System.Drawing.Size(101, 32),
                    new System.Drawing.Font(PublicStatic.MainFont, 12F),
                    new System.Drawing.Size(168 + 106, 32),
                    new System.Drawing.Point(8 + (101 + 8) * 5, 0),
                    PublicStatic.FontColor[1],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    PublicStatic.FontColor[1],
                    System.Drawing.Color.FromArgb(75, 75, 75),
                    System.Drawing.Color.FromArgb(25, 25, 25),
                    BandingNav.anSearchbtn_MouseClick,
                    BandingNav.SearchBox_KeyDown,
                    System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top
                    );
                }
                else
                {
                    if (mapKey.Key != BandingNavMap.Map.DiPinZhi)
                    {
                        new Control.Fase.LButton
                        (
                        PublicStatic.LiuXingNav,
                        1,
                        mapKey.Value,
                        new System.Drawing.Font(PublicStatic.MainFont, 12.5F),
                        new System.Drawing.Size(101, 32),
                        new System.Drawing.Point(8 + (101 + 8) * 3, 0),
                        PublicStatic.MainColor[PublicStatic.MainIndex],
                        PublicStatic.MainColor[PublicStatic.MainIndex],
                        PublicStatic.FontColor[1],
                        PublicStatic.MainColor[PublicStatic.MainIndex],
                        PublicStatic.MainColor[PublicStatic.MainIndex],
                        PublicStatic.FontColor[1],
                        BaseAnchor.AnchorTopLeft
                        ).MouseClick += BandingNav.BandingNav_MouseClick;
                    }
                }
            }
            return true;

            #endregion
        }
        /// <summary>
        /// 初始化侧边导航面板
        /// </summary>
        /// <returns></returns>
        public static bool InitializeAsidePanel()
        {
            #region 初始化侧边导航面板
            int index = 0;
            foreach (var type in PublicStatic.Types)
            {
                var andongzuobtn = new Control.Fase.LButton
                    (
                    PublicStatic.LiuXingAside,
                    1,
                    type.Key,
                    new System.Drawing.Font(PublicStatic.MainFont, 12.5F),
                    new System.Drawing.Size(101, 32),
                    new System.Drawing.Point(8, 31 * index),
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top
                    );
                andongzuobtn.MouseClick += BandingNav.andongzuobtn_MouseClick;
                index++;
            }
            return true;

            #endregion
        }

        /// <summary>
        /// 初始化面板绑定动作
        /// </summary>
        /// <returns></returns>
        public static bool InitializePanelAction()
        {
            #region 初始化面板绑定动作
            if (PublicStatic.LiuXingCon != null)
            {
                PublicStatic.LiuXingCon.MouseClick += BandingNav.LiuXingCon_MouseClick;
                return true;
            }
            return false; 
            #endregion
        }

        /// <summary>
        /// 初始化第一次载入参数
        /// </summary>
        /// <returns></returns>
        public static bool InitializeGlobalPara()
        {
            #region 初始化第一次载入参数
            PublicStatic.CurrentSite = MovieSite.Xunbo;
            PublicStatic.DisPlayStyle = LiuXingStyle.DisPlayTile;
            PublicStatic.AnSortType = SortType.AnShiJian;
            PublicStatic.AnPageNum = 1;
            PublicStatic.AnTypeNum = PublicStatic.Types["电　影"];
            return true; 
            #endregion
        }
        
    }
}

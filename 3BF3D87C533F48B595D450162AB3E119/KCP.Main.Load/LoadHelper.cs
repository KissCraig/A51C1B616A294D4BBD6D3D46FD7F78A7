namespace KCP.Main.Load
{
    public class LoadHelper
    {
        /// <summary>
        /// 界面之前加载
        /// </summary>
        /// <returns></returns>
        public static bool BeforeLoad()
        {
            #region 界面之前加载

            // 加载保护
            if (Guard.GuardHelper.Start())
            {
                // 加载字体
                if (Theme.ThemeHelper.LoadThemeFont())
                {
                    // 加载主题
                    if (Theme.ThemeHelper.ChangeThemeImage(@"Groove_1920x1080x.jpg",
                                                           System.Windows.Forms.ImageLayout.Stretch))
                    {
                        return true;
                    }
                    @"程序默认主题加载失败,可能是背景图异常".ToErrorMsgBox("");
                    return false;
                }
                @"程序默认主题加载失败,可能是字体异常".ToErrorMsgBox("");
                return false;
            }
            @"程序默认保护加载失败,可能是人为破坏".ToErrorMsgBox("");
            return false;

            #endregion
        }

        /// <summary>
        /// 界面之后加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Ui_Load(object sender, System.EventArgs e)
        {
            #region 界面之后加载

            Base.BaseHelper.LoadBanner();
            Bar.FrmBarHelper.LoadTopFrmBar();
            Tile.FlyTileHelper.LoadFlyTile();

            #endregion
        }
    }
}
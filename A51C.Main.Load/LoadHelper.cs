using A51C.Main.Theme;
using A51C.Main.Tile;

namespace A51C.Load
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
            // 加载配置
            if (!Main.Config.ConfigHelper.ReadEducationLocalConfig()) return false;
            // 加载保护
            if (Main.Guard.GuardHelper.Start())
            {
                // 加载字体
                if (ThemeHelper.LoadThemeFont())
                {
                    // 加载主题
                    if (ThemeHelper.ChangeThemeImage(@"Groove_1920x1080x.jpg", System.Windows.Forms.ImageLayout.Stretch))
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
            if (BaseHelper.LoadBanner())
            {
                FlyTileHelper.LoadFlyTiles();
            }
            else
            {
                @"程序顶部面板加载失败,请联系作者".ToErrorMsgBox("");
            }
            #endregion
        }
    }
}
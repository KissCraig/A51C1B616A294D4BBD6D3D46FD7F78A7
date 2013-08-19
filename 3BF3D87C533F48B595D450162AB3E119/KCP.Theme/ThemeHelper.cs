namespace KCP.Theme
{
    public class ThemeHelper
    {
        /// <summary>
        /// 加载主体默认字体
        /// </summary>
        public static bool LoadThemeFont()
        {
            BasePublic.KcpFrmFont = new Font.FontsHelper().GetFont("Segoe UI Light.TTF");
            BasePublic.KcpBarFont = new Font.FontsHelper().GetFont("KCPlayer-Light.ttf");
            return BasePublic.KcpFrmFont != null && BasePublic.KcpBarFont != null;
        }

        /// <summary>
        /// 加载主体背景
        /// </summary>
        /// <param name="imagename"></param>
        /// <param name="imageLayout"></param>
        public static bool ChangeThemeImage(string imagename, System.Windows.Forms.ImageLayout imageLayout)
        {
            BasePublic.Ui.BackgroundImageLayout = imageLayout;
            BasePublic.Ui.BackgroundImage = imagename.IsExistFile()
                                                ? imagename.LoadLocalImage()
                                                : new Default.ResxHelper().GetImage(imagename);
            return BasePublic.Ui.BackgroundImage != null;
        }
    }
}
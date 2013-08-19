using A51C.Main.Theme.Default;
using A51C.Main.Theme.Font;

namespace A51C.Main.Theme
{
    public class ThemeHelper
    {
        /// <summary>
        /// 加载主体默认字体
        /// </summary>
        public static bool LoadThemeFont()
        {
            BasePublic.KcpFrmFont = new FontsHelper().GetFont("Segoe UI Light.TTF");
            BasePublic.KcpBarFont = new FontsHelper().GetFont("KCPlayer-Light.ttf");
            return BasePublic.KcpFrmFont != null && BasePublic.KcpBarFont != null;
        }

        /// <summary>
        /// 加载主体背景
        /// </summary>
        /// <param name="imagename"></param>
        /// <param name="imageLayout"></param>
        public static bool ChangeThemeImage(string imagename, System.Windows.Forms.ImageLayout imageLayout)
        {
            var image = imagename.IsExistFile()
                            ? imagename.LoadLocalImage()
                            : new ResxHelper().GetImage(imagename);
            if (!image.IsEmptyImage())
            {
                BasePublic.Ui.BackgroundImage = image;
                BasePublic.Ui.BackgroundImageLayout = imageLayout;
            }
            return BasePublic.Ui.BackgroundImage != null;
        }
    }
}
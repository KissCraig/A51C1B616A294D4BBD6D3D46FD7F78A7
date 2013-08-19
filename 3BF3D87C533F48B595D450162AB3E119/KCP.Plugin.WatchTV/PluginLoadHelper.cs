using KCP.Control.Base;
using KCP.Control.Gase;

namespace KCP.Plugin.WatchTV
{
    /// <summary>
    /// 插件加载类-基本信息-基本设置-基本程序
    /// </summary>
    public class PluginLoadHelper
    {
        #region 插件基本信息

        /// <summary>
        /// 插件名称
        /// </summary>
        public string Title = "高清电视";

        /// <summary>
        /// 插件作者
        /// </summary>
        public string Author = "CraigTaylor、5L";

        /// <summary>
        /// 插件描述
        /// </summary>
        public string Description = "高清电视";

        /// <summary>
        /// 插件版权
        /// </summary>
        public string Copyright = "Copyright ©  2013";

        /// <summary>
        /// 插件版本号
        /// </summary>
        public string Version = "1.0.0.0";

        /// <summary>
        /// 插件唯一标识
        /// </summary>
        public string Guid = "1A526868-DEE7-48CF-B0B1-DC6A1089D258";

        /// <summary>
        /// 插件磁贴背景色
        /// </summary>
        public System.Drawing.Color BColor = System.Drawing.Color.FromArgb(90, 0, 0, 0);

        /// <summary>
        /// 插件磁贴字体颜色
        /// </summary>
        public System.Drawing.Color FColor = System.Drawing.Color.FromArgb(220, 220, 220);

        /// <summary>
        /// 插件磁贴字体背景
        /// </summary>
        public System.Drawing.Color FBolor = System.Drawing.Color.FromArgb(40, 220, 220, 220);

        /// <summary>
        /// 插件磁贴图标
        /// </summary>
        public System.Drawing.Image PluginLogo = Properties.Resources.ApplicationLogo;

        /// <summary>
        /// 插件磁贴背景
        /// </summary>
        public System.Drawing.Image PluginBg = Properties.Resources.ApplicationBG;

        #endregion

        #region 插件基本设置

        /// <summary>
        /// 应用总面板，取自主程序
        /// </summary>
        public Control.Fase.LPanel MainPanel { get; set; }

        /// <summary>
        /// 应用可用的全局显示字体，取自主程序
        /// </summary>
        public System.Drawing.FontFamily MainFont { get; set; }

        /// <summary>
        /// 应用可用的全局图标字体，取自主程序
        /// </summary>
        public System.Drawing.FontFamily ImageFont { get; set; }

        /// <summary>
        /// 应用间隔顶部的距离，传回主程序
        /// </summary>
        public int MarginTop = 0;

        /// <summary>
        /// 应用是否需要隐藏顶部全屏执行，传回主程序
        /// </summary>
        public bool HiddenTop = false;

        #endregion

        #region 插件基本程序

        /// <summary>
        /// 插件基本信息 - 无需修改
        /// </summary>
        /// <returns></returns>
        public Info.TileItem GetPluginInfo()
        {
            return new Info.TileItem
                {
                    Title = Title,
                    Author = Author,
                    Description = Description,
                    Copyright = Copyright,
                    Version = Version,
                    Guid = Guid,
                    BColor = BColor,
                    FColor = FColor,
                    FBolor = FBolor,
                    PluginLogo = PluginLogo,
                    PluginBg = PluginBg
                };
        }

        /// <summary>
        /// 插件加载完成之后 - 关闭插件 
        /// 切记这里应该把关闭时需要清理的资源全部清理了，以免滞留内存中
        /// </summary>
        /// <returns></returns>
        public bool PluginClose()
        {
            PublicStatic.LiuXingCon.Dispose();
            PublicStatic.LiuXingCon = null;
            return true;
        }

        /// <summary>
        /// 插件加载完之后 - 按需定制
        /// </summary>
        /// <returns></returns>
        public bool PluginLoaded()
        {
            // 初始化 全局静态主字体
            PublicStatic.MainFont = MainFont;
            // 初始化 全局静态图字体
            PublicStatic.ImageFont = ImageFont;
            PublicStatic.LiuXingPal = new ELabel
                {
                    Dock = System.Windows.Forms.DockStyle.Fill,
                };
            MainPanel.Controls.Add(PublicStatic.LiuXingPal);
            try
            {
                WatchTvStart.LoadLiuXing();
            }
            catch (System.Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message);
            }
            return true;
        }

        #endregion
    }
}
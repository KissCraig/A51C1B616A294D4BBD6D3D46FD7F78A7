using A51C.Control.Info;

namespace A51C.Plugin.Home
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
        public string Title = "插件管理";

        /// <summary>
        /// 插件作者
        /// </summary>
        public string Author = "CraigTaylor";

        /// <summary>
        /// 插件描述
        /// </summary>
        public string Description = "插件管理";

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
        public string Guid = "C5269068-6A34-4AF7-93DD-C30F6F0FDCCC";

        /// <summary>
        /// 插件磁贴背景色
        /// </summary>
        public System.Drawing.Color BColor = System.Drawing.Color.FromArgb(40, 0, 0, 0);

        /// <summary>
        /// 插件磁贴字体颜色
        /// </summary>
        public System.Drawing.Color FColor = System.Drawing.Color.FromArgb(220, 220, 220);

        /// <summary>
        /// 插件磁贴字体背景
        /// </summary>
        public System.Drawing.Color FBolor = System.Drawing.Color.FromArgb(40, 220, 220, 220);

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

        ///// <summary>
        ///// 插件基本信息 - 无需修改
        ///// </summary>
        ///// <returns></returns>
        public TileItem GetPluginInfo()
        {
            #region 插件基本信息 - 无需修改
            return new TileItem
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
                        PluginLogo = null,
                        PluginBg = null
                    };

            #endregion
        }

        /// <summary>
        /// 插件加载完之后 - 按需定制
        /// </summary>
        /// <returns></returns>
        public bool PluginLoaded()
        {
            #region 插件加载完之后 - 按需定制

            if (PluginBefore())
            {
                try
                {
                    PublicStatic.MainPanel.BackColor = System.Drawing.Color.OliveDrab;
                    return true;
                }
                catch (System.Exception exception)
                {
                    System.Windows.Forms.MessageBox.Show(exception.Message);
                    return false;
                }
            }
            return false; 
            #endregion
        }



        /// <summary>
        /// 插件加载完成之后 - 关闭插件
        /// </summary>
        /// <returns></returns>
        public bool PluginClose()
        {
            #region 插件加载完成之后 - 关闭插件
            return true; 
            #endregion
        }

        /// <summary>
        /// 加载之前的任务
        /// </summary>
        /// <returns></returns>
        private bool PluginBefore()
        {
            #region 加载之前的任务
            // 初始化 全局静态主字体
            PublicStatic.MainFont = MainFont;
            // 初始化 全局静态图字体
            PublicStatic.ImageFont = ImageFont;
            // 初始化 全局主体面板
            PublicStatic.MainPanel = MainPanel;
            return true; 
            #endregion
        }

        #endregion
    }
}
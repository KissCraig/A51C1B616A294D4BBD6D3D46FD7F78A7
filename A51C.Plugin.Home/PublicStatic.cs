using System.Drawing;
using A51C.Control.Fase;

namespace A51C.Plugin.Home
{
    public class PublicStatic
    {
        /// <summary>
        /// 插件全局 静态主字体
        /// </summary>
        public static FontFamily MainFont { get; set; }

        /// <summary>
        /// 插件全局 静态图字体
        /// </summary>
        public static FontFamily ImageFont { get; set; }

        /// <summary>
        /// 插件全局 主承载面板
        /// </summary>
        public static LPanel MainPanel { get; set; }

        /// <summary>
        /// 磁贴面板
        /// </summary>
        public static LFlyPal TilePal { get; set; }
    }
}
using System.Drawing;
using A51C.Control.Fase;

namespace A51C.Plugin.Test
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
        public static LPanel LiuXingPal { get; set; }

        /// <summary>
        /// 全局导航细胞宽度
        /// </summary>
        public static int NavCellWidth { get; set; }

        /// <summary>
        /// 全局需要隐藏滚动条距离
        /// </summary>
        public static int ScorllWidth { get; set; }

        public static int MainIndex = 0;

        public static Color[] FontColor = new[] { Color.FromArgb(60, 60, 60), Color.FromArgb(248, 248, 248) };

        public static Color[] MainColor = new[]
            {
                Color.FromArgb(0, 122, 204), Color.FromArgb(221, 75, 75),
                Color.FromArgb(33, 115, 70), Color.FromArgb(103, 55, 134)
            };

    }
}
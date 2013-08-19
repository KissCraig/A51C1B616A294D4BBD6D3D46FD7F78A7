using System.Collections.Generic;
using KCP.Control.Fase;
using KCP.Control.Gase;

namespace KCP.Plugin.Music.Base
{
    public class PublicStatic
    {
        /// <summary>
        /// 插件全局 主承载面板
        /// </summary>
        public static LPanel LiuXingPal { get; set; }
        /// <summary>
        /// 插件全局 主导航面板
        /// </summary>
        public static LFlyPal LiuXingNav { get; set; }
        /// <summary>
        /// 插件全局 主侧面面板
        /// </summary>
        public static LPanel LiuXingAside { get; set; }
        /// <summary>
        /// 插件全局 流动内容面板
        /// </summary>
        public static LFlyPal LiuXingCon { get; set; }

        /// <summary>
        /// 全局主字体
        /// </summary>
        public static System.Drawing.FontFamily MainFont { get; set; }

        /// <summary>
        /// 全局图字体
        /// </summary>
        public static System.Drawing.FontFamily ImageFont { get; set; }
        
        public static int AnPageNum { get; set; }
        public static int AnTypeNum { get; set; }
        public static string AnPathTemp { get; set; }
        public static bool IsCopyUrl { get; set; }
        public static InputSearch SearchBox { get; set; }
        public static string SearchWord { get; set; }
        public static bool IsLuoYiXia { get; set; }
        public static string CurrentUrl { get; set; }
        
        public static System.Net.WebProxy MyProxy { get; set; }
        public static int MainIndex = 0;

        public static System.Drawing.Color[] FontColor = new[]
            {System.Drawing.Color.FromArgb(60, 60, 60), System.Drawing.Color.FromArgb(248, 248, 248)};

        public static System.Drawing.Color[] MainColor = new[]
            {
                System.Drawing.Color.FromArgb(0, 122, 204), System.Drawing.Color.FromArgb(221, 75, 75),
                System.Drawing.Color.FromArgb(33, 115, 70), System.Drawing.Color.FromArgb(103, 55, 134)
            };

    }
}
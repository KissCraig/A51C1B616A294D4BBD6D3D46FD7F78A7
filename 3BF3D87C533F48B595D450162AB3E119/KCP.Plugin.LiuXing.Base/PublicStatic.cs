using System.Collections.Generic;
using KCP.Control.Fase;
using KCP.Control.Gase;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Base
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

        ///////////////////////////////////////////////

        /// <summary>
        /// 显示风格
        /// </summary>
        public static LiuXingStyle DisPlayStyle { get; set; }
        /// <summary>
        /// 排序原则
        /// </summary>
        public static SortType AnSortType { get; set; }
        /// <summary>
        /// 当前站点
        /// </summary>
        public static MovieSite CurrentSite { get; set; }





















        /// <summary>
        /// 全局主字体
        /// </summary>
        public static System.Drawing.FontFamily MainFont { get; set; }

        /// <summary>
        /// 全局图字体
        /// </summary>
        public static System.Drawing.FontFamily ImageFont { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public const string LiuXingYuName = @"http://www.2tu.cc";

        
        
        public static LButton PinZhibtn { get; set; }
        
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

        public static System.Collections.Generic.Dictionary<string, int> Types = new Dictionary<string, int>
            {
                {"电　影", 15},
                {"电　视", 16},
                {"动作片", 1},
                {"爱情片", 3},
                {"喜剧片", 2},
                {"科幻片", 4},
                {"战争片", 13},
                {"恐怖片", 5},
                {"剧情片", 6},
                {"动画片", 7},
                {"综艺片", 8},
                {"其他片", 14},
                {"新马泰", 17},
                {"国产剧", 9},
                {"港台剧", 10},
                {"欧美剧", 11},
                {"日韩剧", 12},
                {"预告片", 18},
            };

        public static System.Drawing.Color[] FontColor = new[]
            {System.Drawing.Color.FromArgb(60, 60, 60), System.Drawing.Color.FromArgb(248, 248, 248)};

        public static System.Drawing.Color[] MainColor = new[]
            {
                System.Drawing.Color.FromArgb(0, 122, 204), System.Drawing.Color.FromArgb(221, 75, 75),
                System.Drawing.Color.FromArgb(33, 115, 70), System.Drawing.Color.FromArgb(103, 55, 134)
            };

        /// <summary>
        /// 更新根地址
        /// </summary>
        public const string AnGengXin = @"http://www.2tu.cc/GvodHtml/";

        /// <summary>
        /// 热度根地址
        /// </summary>
        public const string AnReDu = @"http://www.2tu.cc/click/";

        /// <summary>
        /// 评分根地址
        /// </summary>
        public const string AnPengFeng = @"http://www.2tu.cc/mscoref/";

        /// <summary>
        /// 人人影视列表地址
        /// </summary>
        public const string YYetsListHost = @"http://www.yyets.com/php/resourcelist?page=";

        /// <summary>
        /// 按时间更新
        /// </summary>
        public const string AnShiJian = @"http://www.2tu.cc/search.asp?t=15&y=2013&page=";

    }
}
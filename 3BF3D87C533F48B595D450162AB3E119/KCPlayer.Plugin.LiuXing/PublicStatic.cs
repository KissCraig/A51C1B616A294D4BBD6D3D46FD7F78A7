
using System.Drawing;

using KCPlayer.Plugin.LiuXing.Controls;
using KCPlayer.Plugin.LiuXing.Helper;
using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing
{
    public class PublicStatic
    {
        #region XunLeiLogin
        public static System.Net.CookieContainer LoginCookies { get; set; }
        public static System.Net.CookieCollection LoginCollection { get; set; }
        public static System.Uri ClientUri { get; set; }
        public static TestVodVip NowUserOne { get; set; }
        public static InputBoxWithDesc UserName { get; set; }
        public static InputBoxWithDesc PassWord { get; set; }
        public static InputBoxWithDesc YanZhengMa { get; set; }
        public static LButton YanZhengCode { get; set; }
        public static LButton XunLeiLoginBtn { get; set; }
        public static LButton XunLeiLogOffBtn { get; set; }

        public const string KcPlayerUserXunLeiInfoDb = @"ProgramData/KCPlayer_User_XunLei_Info.db";

        public static string[] KcPlayerUserXunLeiInfoKeys = { "z,x.", "c/1," };
        #endregion

        #region LiuXingVideo
        public static System.Collections.Generic.List<string> HaveToBeDeleteList { get; set; }
        public const string LastVideoRecordFileName = @"";
        public const string LiuXingVideoRecordCacheDir = @"ProgramData/LiuXingVideoRecordCacheDir/";
        public const string LiuXingVideoImageCacheDir = @"ProgramData/LiuXingVideoImageCacheDir/";

        public static System.Collections.Generic.List<System.Threading.Thread> Threads { get; set; }
        public static LiuXingStyle DisPlayStyle { get; set; }

        public const string LiuXingYuName = @"http://www.2tu.cc";
        public static bool IsTestVod { get; set; }
        public static string IsTestVodConfigPath = "http://api.kcplayer.com/InterFace/2.txt";

        /// <summary>
        ///     更新根地址
        /// </summary>
        public const string AnGengXin = @"http://www.2tu.cc/GvodHtml/";

        /// <summary>
        ///     热度根地址
        /// </summary>
        public const string AnReDu = @"http://www.2tu.cc/click/";

        /// <summary>
        ///     评分根地址
        /// </summary>
        public const string AnPengFeng = @"http://www.2tu.cc/mscoref/";

        /// <summary>
        ///     按时间更新
        /// </summary>
        public const string AnShiJian = @"http://www.2tu.cc/search.asp?t=15&y=2013&page=";

        /// <summary>
        ///     人人影视列表地址
        /// </summary>
        public const string YYetsListHost = @"http://www.yyets.com/php/resourcelist?page=";

        public static int MainIndex = 0;

        public static System.Collections.Generic.Dictionary<string, int> Types = new System.Collections.Generic.Dictionary<string, int>
            {
                {"电影", 15},
                {"电视", 16},
                {"动作", 1},
                {"爱情", 3},
                {"喜剧", 2},
                {"科幻", 4},
                {"战争", 13},
                {"恐怖", 5},
                {"剧情", 6},
                {"动画", 7},
                {"综艺", 8},
                {"其他", 14},
                {"新马", 17},
                {"国产", 9},
                {"港台", 10},
                {"欧美", 11},
                {"日韩", 12},
                {"预告", 18},
            };

        public static Color[] FontColor = new[] { Color.FromArgb(60, 60, 60), Color.FromArgb(255, 255, 255) };

        public static Color[] MainColor = new[]
            {
                Color.FromArgb(0, 122, 204), Color.FromArgb(221, 75, 75),
                Color.FromArgb(33, 115, 70), Color.FromArgb(103, 55, 134)
            };

        public static EPanel LiuXingPal { get; set; }

        public static EPanel ThisHot { get; set; }

        public static ELabel ThisHotNext { get; set; }
        public static MetroForList LiuXingNav { get; set; }
        public static MetroForList LiuXingAside { get; set; }
        public static MetroForList LiuXingCategory { get; set; }
        public static MetroForList LiuXingCategoryType { get; set; }

        public static MetroForList LiuXingFilmType { get; set; }
        public static MetroForList LiuXingFilmLoc { get; set; }
        public static MetroForList LiuXingFilmYear { get; set; }

        public static MetroForFly LiuXingCon { get; set; }
        public static LButton PinZhibtn { get; set; }
        public static SortType AnSortType { get; set; }
        public static int AnPageNum { get; set; }
        public static int AnTypeNum { get; set; }
        public static string AnPathTemp { get; set; }
        public static bool IsCopyUrl { get; set; }
        public static string SegoeFont { get; set; }
        public static InputSearch SearchBox { get; set; }
        public static string SearchWord { get; set; }
        public static bool IsLuoYiXia { get; set; }
        public static string CurrentUrl { get; set; }
        public static MovieSite CurrentSite { get; set; }
        public static System.Net.WebProxy MyProxy { get; set; } 
        #endregion

        public static Helper.CategoryHelper.Category NowCategory { get; set; }


    }
}
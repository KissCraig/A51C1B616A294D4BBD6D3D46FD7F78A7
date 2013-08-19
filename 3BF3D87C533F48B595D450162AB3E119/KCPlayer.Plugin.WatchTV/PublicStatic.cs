using KCPlayer.Plugin.WatchTV.Controls;
using KCPlayer.Plugin.WatchTV.LoadWatchTV;

namespace KCPlayer.Plugin.WatchTV
{
    public class PublicStatic
    {
        public static ELabel LiuXingPal { get; set; }
        public static EFlyPal LiuXingNav { get; set; }
        public static EnBrowser LiuXingCon { get; set; }
        public static LButton PinZhibtn { get; set; }
        public static LButton AnShouQibtn { get; set; }
        public static LButton AnQieDingbtn { get; set; }
        public static LButton AnPeiSebtn { get; set; }
        public static LButton AnTianJiabtn { get; set; }
        public static SortType AnSortType { get; set; }
        public static int AnPageNum { get; set; }
        public static string AnPathTemp { get; set; }
        public static bool IsCopyUrl { get; set; }
        public static System.Drawing.FontFamily SegoeFont { get; set; }
        public static InputSearch SearchBox { get; set; }
        public static string SearchWord { get; set; }
        public static bool IsLuoYiXia { get; set; }
        public static string CurrentUrl { get; set; }
        public static MovieSite CurrentSite { get; set; }
        public static System.Net.WebProxy MyProxy { get; set; }
        public static WatchTvData NowPlayWatch { get; set; }
        public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<WatchTvData>> Dics { get; set; }

        public static System.Drawing.Color[] SouSouColor = new[]
            {
                System.Drawing.Color.FromArgb(0, 122, 204),
                System.Drawing.Color.FromArgb(221, 75, 75),
                System.Drawing.Color.FromArgb(116, 40, 148),
                System.Drawing.Color.FromArgb(239, 129, 24),
                System.Drawing.Color.FromArgb(10, 163, 68),
            };

        public static int SouSouIndex { get; set; }
    }
}
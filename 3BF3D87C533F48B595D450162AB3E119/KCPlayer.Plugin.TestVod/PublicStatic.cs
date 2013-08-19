using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KCPlayer.Plugin.TestVod.Controls;
using KCPlayer.Plugin.TestVod.Helper;

namespace KCPlayer.Plugin.TestVod
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

        public static string NowVodPathUrl { get; set; }
        public static string NowTestVodPlayAPI { get; set; }

        public static System.Collections.Generic.List<string> HaveToBeDeleteList { get; set; }


        public static VodPlayType TestVodPlayType { get; set; }
        public static MetroForList TestVodNavLeft { get; set; }
        public static MetroForList TestVodNavRight { get; set; }


        public static int LeftWidth = 240;
        public static int MainIndex = 0;
        public static Color[] FontColor = new[] {Color.FromArgb(60, 60, 60), Color.FromArgb(248, 248, 248)};
        public static string IsTestVodConfigPath = "http://api.kcplayer.com/InterFace/1.txt";
        public static string IsWhoPlayer { get; set; }



        public static Color[] MainColor = new[]
            {
                Color.FromArgb(0, 122, 204), Color.FromArgb(221, 75, 75),
                Color.FromArgb(33, 115, 70), Color.FromArgb(103, 55, 134)
            };

        public static Dictionary<string, string> VodList { get; set; }
        public static string VodListPath { get; set; }
        public static EPanel LeftPal { get; set; }
        public static EPanel RightPal { get; set; }
        public static EnBrowser VodBrowser { get; set; }
        public static EPanel VodPal { get; set; }
        public static EPanel VodBar { get; set; }
        public static InputSearch VodInput { get; set; }
        public static TextBox VodIndex { get; set; }
        public static LButton ShouQibtn { get; set; }
        public static LButton Dingbtn { get; set; }
        public static LButton PlayListbtn { get; set; }
        public static string SegoeFont { get; set; }
        public static bool IsViewList { get; set; }

        public static List<string> Key { get; set; }
        public static List<string> UserId { get; set; }
        public static List<string> HmCode { get; set; }

        public static System.Collections.Generic.List<string> Userids { get; set; }
        public static System.Collections.Generic.List<string> Sessionids { get; set; }
    }
}
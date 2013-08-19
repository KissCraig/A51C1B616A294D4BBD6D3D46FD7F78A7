using KCPlayer.Json;
using KCPlayer.Plugin.TestVod.Helper;

namespace KCPlayer.Plugin.TestVod
{
    public class TestVodReset
    {
        /// <summary>
        /// 整个数据得以初始化
        /// </summary>
        public static void MakeTestVodReset()
        {
            PublicStatic.NowUserOne = new TestVodVip();
            PublicStatic.HaveToBeDeleteList = new System.Collections.Generic.List<string>
                {
                    "//Plugin//Share.dll",
                    "//Plugin//SkyPlayerPlugin.dll",
                    "//Plugin//Weather.dll"
                };
            PublicStatic.TestVodPlayType = VodPlayType.OK;
            var iThread = new System.Threading.Thread(LoadLocaLXunLeiUserInfo);
            iThread.SetApartmentState(System.Threading.ApartmentState.STA);
            iThread.Start();
            new System.Threading.Thread(Helper.ClearCachoHelper.CleanTempFiles).Start();
        }

        /// <summary>
        /// 从本地读取用户信息
        /// </summary>
        private static void LoadLocaLXunLeiUserInfo()
        {
            #region 从本地读取用户信息
            try
            {
                var localTxt = ReadFile("KCPlayer_User_XunLei_Info.db");
                if (!string.IsNullOrEmpty(localTxt))
                {
                    localTxt = TestVodSafe.DES_Dec_Str(localTxt, "z,x.", "c/1,");
                    if (!string.IsNullOrEmpty(localTxt))
                    {
                        PublicStatic.NowUserOne = JsonMapper.ToObject<TestVodVip>(localTxt);
                    }
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {

            } 
            #endregion
        }

        public static string ReadFile(string path)
        {
            #region String -> Path -> ReadFile

            if (!System.IO.File.Exists(path)) return null;
            try
            {
                var str = System.IO.File.ReadAllText(path);
                return str;
            }
            catch
            {
                return null;
            }

            #endregion
        }


    }
}
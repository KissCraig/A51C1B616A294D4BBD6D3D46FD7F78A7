using System;
using System.Net;
using KCPlayer.Plugin.LiuXing.Helper;

namespace KCPlayer.Plugin.LiuXing.LiuXing
{
    public class LiuXingStart
    {
        #region public Code

        /// <summary>
        /// 初始化进入
        /// </summary>
        public static void LoadLiuXing()
        {
            #region 初始化进入

            PanelTileHelper.InitializeMainPanel();
            StartListHelper.StartReadConfig();
            //PublicStatic.NowCategory = CategoryHelper.Category.大家都看;
            PublicStatic.NowCategory = CategoryHelper.Category.迅播影院;
            StartListHelper.StartActionOne();
            

            #endregion
        }


        #endregion
    }
}
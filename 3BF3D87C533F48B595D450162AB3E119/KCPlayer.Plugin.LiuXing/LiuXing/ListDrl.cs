using KCPlayer.Plugin.LiuXing.Helper;
using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.LiuXing
{
    public class ListDrl
    {
        /// <summary>
        ///     解析地址并启动复制或者点播
        /// </summary>
        /// <param name="resultstr"></param>
        /// <param name="iType"></param>
        public static void GetThisDrl(string resultstr, LiuXingType iType)
        {
            if (string.IsNullOrEmpty(resultstr) || iType == null) return;
            // 二次解析地址
            iType.Data.Drl = ListFile.JieUrls(resultstr, iType);
            if (iType.Data.Drl == null || iType.Data.Drl.Count <= 0) return;
            // 判断是否是执行复制地址
            if (iType.IsCopy)
            {
                VodCopyHelper.CopyThisUrl(iType.Data.Drl);
            }
                // 判断是否是执行点播地址
            else
            {
                // 启动点播
                VodCopyHelper.StartToVod(iType.Data.Drl, iType.Data);
            }
        }
    }
}
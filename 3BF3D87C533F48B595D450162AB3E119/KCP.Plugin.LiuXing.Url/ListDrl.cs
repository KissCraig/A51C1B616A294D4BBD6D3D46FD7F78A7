using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Url
{
    public class ListDrl
    {
        /// <summary>
        /// 解析地址并启动复制或者点播
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
                Pub.CopyThisUrl(iType.Data.Drl);
            }
                // 判断是否是执行点播地址
            else
            {
                // 智能判断适当的点播地址
                var currentUrl = ListVod.GetVodUrl(iType);
                if (!string.IsNullOrEmpty(currentUrl))
                {
                    // 启动点播
                    Pub.StartToVod(currentUrl);
                }
            }
        }
    }
}
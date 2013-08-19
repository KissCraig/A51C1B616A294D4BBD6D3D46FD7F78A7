namespace KCPlayer.Plugin.LiuXing.LoadLiuXing.YYet
{
    public class SearchBase
    {
        /// <summary>
        /// 解析人人影视列表中的类别信息
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string JieXiYYetsType(string txt)
        {
            if (!txt.Contains("/"))
            {
                return txt;
            }
            var txts = txt.Split('/');
            return txts.Length > 0 ? txts[0].Trim() : "";
        }

        /// <summary>
        /// 解析人人影视列表中的地区信息
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string JieXiYYetsDiQu(string txt)
        {
            if (txt.Contains("美国"))
                return "美国";
            if (txt.Contains("中英"))
                return "欧美";
            if (txt.Contains("暂无"))
                return "未知";
            if (txt.Contains("中德"))
                return "德国";
            if (txt.Contains("中法"))
                return "法国";
            if (txt.Contains("金球"))
                return "欧美";
            if (txt.Contains("中日"))
                return "日本";
            if (txt.Contains("印坛"))
                return "印度";
            if (txt.Contains("韩剧"))
                return "韩剧";
            return "未知";
        }
    }
}
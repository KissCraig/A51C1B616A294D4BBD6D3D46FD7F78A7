namespace KCPlayer.Plugin.WatchTV.LoadWatchTV
{
    public class WatchTvRegex
    {
        #region public Code

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetSingle(string str, string s, string e)
        {
            var rg = new System.Text.RegularExpressions.Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))",
                                                              System.Text.RegularExpressions.RegexOptions.Multiline |
                                                              System.Text.RegularExpressions.RegexOptions.Singleline);
            var ss = rg.Match(str).Value;
            return string.IsNullOrEmpty(ss) ? null : ss;
        }

        /// <summary>
        /// 获取批量值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<string> GetValue(string str, string s, string e)
        {
            var rg = new System.Text.RegularExpressions.Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))");
            var mc = rg.Matches(str);
            var aStrings = new System.Collections.Generic.List<string>();
            for (var i = 0; i < mc.Count; i++)
            {
                aStrings.Add(mc[i].Value);
            }
            return aStrings.Count <= 0 ? null : aStrings;
        }

        #endregion
    }
}
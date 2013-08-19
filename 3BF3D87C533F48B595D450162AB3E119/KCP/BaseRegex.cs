namespace KCP
{
    public static class BaseRegex
    {
        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="originalStr"></param>
        /// <param name="prevstr"></param>
        /// <param name="nextstr"></param>
        /// <returns></returns>
        public static string GetSingle(this string originalStr, string prevstr, string nextstr)
        {
            var rg = new System.Text.RegularExpressions.Regex("(?<=(" + prevstr + "))[.\\s\\S]*?(?=(" + nextstr + "))",
                                                              System.Text.RegularExpressions.RegexOptions.Multiline |
                                                              System.Text.RegularExpressions.RegexOptions.Singleline);
            var ss = rg.Match(originalStr).Value;
            return string.IsNullOrEmpty(ss) ? null : ss;
        }

        /// <summary>
        /// 获取批量值
        /// </summary>
        /// <param name="originalStr"></param>
        /// <param name="prevstr"></param>
        /// <param name="nextstr"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<string> GetValue(this string originalStr, string prevstr,
                                                                       string nextstr)
        {
            var rg = new System.Text.RegularExpressions.Regex("(?<=(" + prevstr + "))[.\\s\\S]*?(?=(" + nextstr + "))");
            var mc = rg.Matches(originalStr);
            var aStrings = new System.Collections.Generic.List<string>();
            for (var i = 0; i < mc.Count; i++)
            {
                aStrings.Add(mc[i].Value);
            }
            return aStrings.Count <= 0 ? null : aStrings;
        }
    }
}
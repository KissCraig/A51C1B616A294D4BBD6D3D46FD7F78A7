using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class StringRegexHelper
    {
        #region public Code

        /// <summary>
        ///     获取单个值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetSingle(string str, string s, string e)
        {
            var rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))",
                               RegexOptions.Multiline |
                               RegexOptions.Singleline);
            string ss = rg.Match(str).Value;
            return string.IsNullOrEmpty(ss) ? null : ss;
        }

        /// <summary>
        ///     获取批量值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static List<string> GetValue(string str, string s, string e)
        {
            var rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))");
            MatchCollection mc = rg.Matches(str);
            var aStrings = new List<string>();
            for (int i = 0; i < mc.Count; i++)
            {
                aStrings.Add(mc[i].Value);
            }
            return aStrings.Count <= 0 ? null : aStrings;
        }

        #endregion
    }
}
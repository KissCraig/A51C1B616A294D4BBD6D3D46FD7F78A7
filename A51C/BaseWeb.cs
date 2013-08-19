namespace A51C
{
    public static class BaseWeb
    {
        /// <summary>
        /// String -> ToHtmlDecode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ToHtmlDecode(this string code)
        {
            return System.Web.HttpUtility.HtmlDecode(code);
        }

        /// <summary>
        /// String -> ToHtmlEncode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ToHtmlEncode(this string code)
        {
            return System.Web.HttpUtility.HtmlEncode(code);
        }

        /// <summary>
        /// String -> ToUrlDecode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ToUrlDecode(this string code)
        {
            return System.Web.HttpUtility.UrlDecode(code);
        }

        /// <summary>
        /// String -> ToUrlEncode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ToUrlEncode(this string code)
        {
            return System.Web.HttpUtility.UrlEncode(code);
        }
    }
}
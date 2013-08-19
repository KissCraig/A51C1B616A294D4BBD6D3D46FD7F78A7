namespace A51C
{
    public static class BaseString
    {
        /// <summary>
        /// String -> Check Value -> GetBack -> String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSafeValue(this string value)
        {
            #region String -> Check Value -> GetBack -> String

            return value.IsNullOrEmptyOrSpace() ? "" : value;

            #endregion
        }

        /// <summary>
        /// String -> Check Value -> IsNotNullOrEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            #region String -> Check Value -> IsNotNullOrEmpty

            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);

            #endregion
        }

        /// <summary>
        /// String -> Check Value -> IsNullOrEmptyOrSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrSpace(this string value)
        {
            #region String -> Check Value -> IsNullOrEmptyOrSpace

            return string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value);

            #endregion
        }

        /// <summary>
        /// String To Uri
        /// </summary>
        /// <param name="path"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static System.Uri ToUri(this string path, System.UriKind kind)
        {
            #region String To Uri

            try
            {
                return new System.Uri(path, kind);
            }
            catch
            {
                return null;
            }

            #endregion
        }

        /// <summary>
        /// String -> Int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            #region String -> Int
            int a;
            if (int.TryParse(s, out a))
            {
                return a;
            }
            return -1; 
            #endregion
        }


        /// <summary>
        /// String -> Int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToFloat(this string s)
        {
            #region String -> Int
            float a;
            if (float.TryParse(s, out a))
            {
                return a;
            }
            return -1;
            #endregion
        }
    }
}
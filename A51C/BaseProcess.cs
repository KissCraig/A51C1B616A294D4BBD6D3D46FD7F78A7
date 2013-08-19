namespace A51C
{
    public static class BaseProcess
    {
        /// <summary>
        /// String -> OpenWebLink
        /// </summary>
        /// <param name="s"></param>
        public static void OpenWebLink(this string s)
        {
            #region String -> OpenWebLink

            if (s.IsNullOrEmptyOrSpace()) return;
            if (!s.StartsWith("http://") && s.StartsWith("www."))
            {
                s = "http://" + s;
            }
            try
            {
                System.Diagnostics.Process.Start(s);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }

        /// <summary>
        /// String -> OpenLocalPath
        /// </summary>
        /// <param name="s"></param>
        public static void OpenLocalPath(this string s)
        {
            #region String -> OpenLocalPath

            if (s.IsNullOrEmptyOrSpace()) return;
            try
            {
                System.Diagnostics.Process.Start(s);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }

        /// <summary>
        /// Sting -> LocalFile -> OpenFile
        /// </summary>
        /// <param name="s"></param>
        public static void OpenFile(this string s)
        {
            if (!s.IsExistFile()) return;
            try
            {
                System.Diagnostics.Process.Start(s);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        /// <summary>
        /// String -> LocalPath -> PickMe
        /// </summary>
        /// <param name="s"></param>
        public static void PickMe(this string s)
        {
            #region String -> LocalPath -> PickMe

            if (s.IsNullOrEmptyOrSpace()) return;
            try
            {
                System.Diagnostics.Process.Start("Explorer.exe", "/select," + s);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }


    }
}
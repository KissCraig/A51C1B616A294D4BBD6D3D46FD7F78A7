namespace KCP
{
    public static class BaseFile
    {
        /// <summary>
        /// String -> Check Path -> IsExistFile
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExistFile(this string path)
        {
            #region String -> Check Path -> IsExistFile

            return !path.IsNullOrEmptyOrSpace() && System.IO.File.Exists(path);

            #endregion
        }

        /// <summary>
        /// String -> Check Dir -> IsExistDir
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsExistDir(this string dir)
        {
            #region String -> Check Dir -> IsExistDir

            return !dir.IsNullOrEmptyOrSpace() && System.IO.Directory.Exists(dir);

            #endregion
        }

        /// <summary>
        /// String -> File -> ToDelete
        /// </summary>
        /// <param name="path"></param>
        public static void ToDeleteFile(this string path)
        {
            #region String -> File -> ToDelete

            if (!path.IsExistFile()) return;
            try
            {
                System.IO.File.Delete(path);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }

        /// <summary>
        /// String -> Dir -> ToDelete
        /// </summary>
        /// <param name="dir"></param>
        public static void ToDeleteDir(this string dir)
        {
            #region String -> Dir -> ToDelete

            if (!dir.IsExistDir()) return;
            try
            {
                System.IO.Directory.Delete(dir);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }

        /// <summary>
        /// String -> Dir -> ToCreatDir
        /// </summary>
        /// <param name="dir"></param>
        public static void ToCreatDir(this string dir)
        {
            #region String -> Dir -> ToCreatDir

            if (dir.IsExistDir()) return;
            try
            {
                System.IO.Directory.CreateDirectory(dir);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

            #endregion
        }

        /// <summary>
        /// String -> Path -> Ext
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToExt(this string path)
        {
            #region String -> Path -> Ext

            if (!path.IsNullOrEmptyOrSpace() && path.IsExistFile())
            {
                var ext = System.IO.Path.GetExtension(path);
                return !ext.IsNullOrEmptyOrSpace() ? ext : null;
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Path -> NameWithExt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToNameWithExt(this string path)
        {
            #region String -> Path -> NameWithExt

            if (!path.IsNullOrEmptyOrSpace() && path.IsExistFile())
            {
                var name = System.IO.Path.GetFileName(path);
                return !name.IsNullOrEmptyOrSpace() ? name : null;
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Path -> NameNoExt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToNameNoExt(this string path)
        {
            #region String -> Path -> ToNameNoExt

            if (!path.IsNullOrEmptyOrSpace() && path.IsExistFile())
            {
                var name = System.IO.Path.GetFileNameWithoutExtension(path);
                return !name.IsNullOrEmptyOrSpace() ? name : null;
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Path -> ReadFile
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(this string path)
        {
            #region String -> Path -> ReadFile

            if (!path.IsNullOrEmptyOrSpace())
            {
                if (path.IsExistFile())
                {
                    try
                    {
                        var str = System.IO.File.ReadAllText(path);
                        if (str.IsNotNullOrEmpty())
                        {
                            return str;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Path -> SaveFile
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="path"></param>
        public static void SaveFile(this string txt, string path)
        {
            #region String -> Path -> SaveFile

            if (txt.IsNullOrEmptyOrSpace()) return;
            if (path.IsNullOrEmptyOrSpace()) return;
            try
            {
                System.IO.File.WriteAllText(path, txt);
            }
            catch (System.Exception exception)
            {
                exception.Message.ToInfoMsgBox(@"文件保存错误");
            }

            #endregion
        }

        /// <summary>
        /// String -> Dir -> DirName WithoutPath
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string[] ToDirNamesWithoutPath(this string dir)
        {
            #region String -> Dir -> DirName WithoutPath

            if (dir.IsExistDir())
            {
                var dirnames = dir.ToDirNamesWithPath();
                if (!dirnames.IsEmptyStrings())
                {
                    for (var i = 0; i < dirnames.Length; i++)
                    {
                        var dirSplit = dirnames[i].Split("\\".ToCharArray());
                        var dirclear = dirSplit[dirSplit.Length - 1];
                        dirnames[i] = dirclear;
                    }
                    return dirnames;
                }
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Dir -> DirName WithPath
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string[] ToDirNamesWithPath(this string dir)
        {
            #region String -> Dir -> DirName WithPath

            if (dir.IsExistDir())
            {
                var dirnames = System.IO.Directory.GetDirectories(dir, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                if (!dirnames.IsEmptyStrings())
                {
                    return dirnames;
                }
            }
            return null;

            #endregion
        }

        /// <summary>
        /// String -> Fir -> ToFileNamesWithPath
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static string[] ToFileNamesWithPath(this string dir, string filters)
        {
            #region String -> Fir -> ToFileNamesWithPath

            if (filters.IsNullOrEmptyOrSpace())
            {
                filters = "*.*";
            }
            if (dir.IsExistDir())
            {
                var filenames = System.IO.Directory.GetFiles(dir, filters, System.IO.SearchOption.TopDirectoryOnly);
                if (!filenames.IsEmptyStrings())
                {
                    return filenames;
                }
            }
            return null;

            #endregion
        }
    }
}
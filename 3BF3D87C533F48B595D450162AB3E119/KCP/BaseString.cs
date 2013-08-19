namespace KCP
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
        /// filePath -> ToDynamic
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this string filePath)
        {
            #region filePath -> ToDynamic

            if (!filePath.IsExistFile()) return null;
            // 反射文件程序集
            System.Reflection.Assembly assembly;
            try
            {
                assembly = System.Reflection.Assembly.Load(System.IO.File.ReadAllBytes(filePath));
                //assembly = System.Reflection.Assembly.LoadFile(filePath);
            }
            catch
            {
                return null;
            }
            if (assembly == null) return null;
            // 动态反射程序
            dynamic obj;
            try
            {
                obj = System.Activator.CreateInstance(assembly.GetType(assembly.GetName().Name + ".PluginLoadHelper"));
            }
            catch
            {
                obj = null;
            }
            return obj;

            #endregion
        }

        public static dynamic ToDynamic(this byte[] bytes)
        {
            System.Reflection.Assembly assembly;
            try
            {
                assembly = System.Reflection.Assembly.Load(bytes);
            }
            catch
            {
                return null;
            }
            if (assembly == null) return null;
            // 动态反射程序
            dynamic obj;
            try
            {
                obj = System.Activator.CreateInstance(assembly.GetType(assembly.GetName().Name + ".PluginLoadHelper"));
            }
            catch
            {
                obj = null;
            }
            return obj;
        }
    }
}
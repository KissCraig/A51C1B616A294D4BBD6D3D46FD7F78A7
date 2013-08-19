namespace KCP
{
    public static class BaseRegistry
    {
        /// <summary>
        /// 根键的值
        /// </summary>
        private static Microsoft.Win32.RegistryKey _rootKey;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootKeys"></param>
        public static void RegistryHelper(this string rootKeys)
        {
            switch (rootKeys.ToUpper())
            {
                case "CLASSES_ROOT":
                    _rootKey = Microsoft.Win32.Registry.ClassesRoot;
                    break;
                case "CURRENT_USER":
                    _rootKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
                case "LOCAL_MACHINE":
                    _rootKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
                case "USERS":
                    _rootKey = Microsoft.Win32.Registry.Users;
                    break;
                case "CURRENT_CONFIG":
                    _rootKey = Microsoft.Win32.Registry.CurrentConfig;
                    break;
                case "DYN_DATA":
#pragma warning disable 612,618
                    if (Microsoft.Win32.Registry.DynData != null) _rootKey = Microsoft.Win32.Registry.DynData;
#pragma warning restore 612,618
                    break;
                case "PERFORMANCE_DATA":
                    _rootKey = Microsoft.Win32.Registry.PerformanceData;
                    break;
                default:
                    _rootKey = Microsoft.Win32.Registry.CurrentUser;
                    break;
            }
        }

        /// <summary>
        /// 读取指定注册表的值
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetRegistryData(this string subkey, string name)
        {
            try
            {
                var registData = string.Empty;
                var myKey = _rootKey.OpenSubKey(subkey, true);
                if (myKey != null)
                {
                    registData = myKey.GetValue(name).ToString();
                }
                return registData;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 写入注册表
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static bool SetRegistryData(this string subkey, string name, string value)
        {
            try
            {
                using (var myKey = _rootKey.CreateSubKey(subkey))
                {
                    if (myKey != null) myKey.SetValue(name, value);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除注册表
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        public static bool DeleteRegist(this string subkey, string name)
        {
            try
            {
                using (var myKey = _rootKey.OpenSubKey(subkey, true))
                {
                    if (myKey != null) myKey.DeleteValue(name, true);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="subkey"/>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsRegistryExist(this string subkey, string name)
        {
            try
            {
                using (var myKey = _rootKey.OpenSubKey(subkey, true))
                {
                    return myKey != null && myKey.GetValue(name) != null;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
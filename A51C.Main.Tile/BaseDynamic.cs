
namespace A51C.Main.Tile
{
    public static class BaseDynamic
    {
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
                assembly = System.Reflection.Assembly.Load(BasePublic.SafeMode ? Dens.FileReadHelper.DecryptFormFilePath(filePath, BasePublic.UiKey) : System.IO.File.ReadAllBytes(filePath));
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
    }
}

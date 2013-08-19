namespace KCP.Config
{
    public class DllHelper
    {
        /// <summary>
        /// DLL丢失时补充辅助类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, System.ResolveEventArgs args)
        {
            var assemblyInfo = args.Name;
            if (assemblyInfo.IsNotNullOrEmpty())
            {
                var assemblyName = assemblyInfo.Split(',')[0];
                if (assemblyName.IsNotNullOrEmpty())
                {
                    var assemlyPath = string.Format("{0}/{1}.dll", BasePublic.PluginsDirPath, assemblyName);
                    if (assemlyPath.IsExistFile())
                    {
                        return System.Reflection.Assembly.LoadFile(assemlyPath);
                    }
                }
            }
            
            return null;
        }
    }
}

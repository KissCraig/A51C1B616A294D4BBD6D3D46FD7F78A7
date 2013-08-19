namespace KCP.Config
{
    // ConfigHelper.ReadLocalConfig(@"\Geno.Hospital.Win.exe.config", @"\App.config");
    public class ConfigHelper
    {
        /// <summary>
        /// 从本地路径去读取配置文件
        /// </summary>
        public static void ReadLocalConfig(string lastDirConfigName, string selfDirConfigName)
        {
            // 程序启动路径
            BasePublic.AppStartupPath = System.Windows.Forms.Application.StartupPath;
            if (!BasePublic.AppStartupPath.IsNullOrEmptyOrSpace())
            {
                var appStartupPathInfo = new System.IO.DirectoryInfo(BasePublic.AppStartupPath);
                var lastPathInfo = appStartupPathInfo.Parent;
                if (lastPathInfo != null)
                {
                    var appGenoConfigPath = lastPathInfo.FullName + lastDirConfigName;
                    var tempconfigdatafolder = PickConfigDataFolder(appGenoConfigPath,
                                                                    "<add key=\"DataFolderDirectory\" value=\"", "\" />");
                    if (tempconfigdatafolder.IsNullOrEmptyOrSpace())
                    {
                        appGenoConfigPath = BasePublic.AppStartupPath + selfDirConfigName;
                        tempconfigdatafolder = PickConfigDataFolder(appGenoConfigPath,
                                                                    "<add key=\"DataFolderDirectory\" value=\"", "\" />");
                        if (tempconfigdatafolder.IsNullOrEmptyOrSpace())
                        {
                            tempconfigdatafolder = "DataFolderDirectory".ToConfigValue();
                            if (tempconfigdatafolder.IsNullOrEmptyOrSpace())
                            {
                                @"找不到您的本地配置文件,请及时修复".ToErrorMsgBox(@"配置文件丢失");
                            }
                            else
                            {
                                BasePublic.DataFolderDirectory = tempconfigdatafolder;
                            }
                        }
                        else
                        {
                            BasePublic.DataFolderDirectory = tempconfigdatafolder;
                        }
                    }
                    else
                    {
                        BasePublic.DataFolderDirectory = tempconfigdatafolder;
                    }
                }
            }
            else
            {
                @"获取启动路径失败，请检查启动权限!".ToErrorMsgBox(@"启动错误");
            }
            if (!BasePublic.DataFolderDirectory.IsExistDir())
            {
                @"找不到本地数据文件夹!".ToErrorMsgBox(@"配置文件错误");
                return;
            }
            BasePublic.DataFolderDirectory += @"\Edu\English";
            BasePublic.DataFolderDirectory.ToCreatDir();
            BasePublic.CheckUpdatePath = "CheckUpdatePath".ToConfigValue();
            BasePublic.ServerRequestPath = "ServerRequestPath".ToConfigValue();
        }

        /// <summary>
        /// 从配置文件路径去获取配置
        /// </summary>
        /// <param name="appGenoConfigPath"></param>
        /// <param name="prevstr"></param>
        /// <param name="nextstr"></param>
        /// <returns></returns>
        private static string PickConfigDataFolder(string appGenoConfigPath, string prevstr, string nextstr)
        {
            if (appGenoConfigPath.IsExistFile())
            {
                var configtxt = appGenoConfigPath.ReadFile();
                if (!string.IsNullOrWhiteSpace(configtxt))
                {
                    var datapath = configtxt.GetSingle(prevstr, nextstr);
                    if (!string.IsNullOrWhiteSpace(datapath))
                    {
                        return datapath;
                    }
                }
            }
            return null;
        }
    }
}
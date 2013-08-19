namespace A51C.Main.Config
{
    public class ConfigHelper
    {
        /// <summary>
        /// 读取真诺教育本地配置文件
        /// </summary>
        public static bool ReadEducationLocalConfig()
        {
            #region 读取真诺教育本地配置文件

            //if (!ReadLocalConfig("Geno.Education.Win.exe.config", "*.config"))
            //{
            //    AppEntity.ExamDirectory = @"ExamDirectory".ToConfigValue();
            //    AppEntity.ConnectString = @"connectionString".ToConfigValue();
            //    AppEntity.DataFolderDirectory = @"DataDirectory".ToConfigValue();
            //}
            //if (!AppEntity.ExamDirectory.IsExistDir())
            //{
            //    @"配置中指定的教育考试位置不存在".ToErrorMsgBox(@"读取配置失败！");
            //    return false;
            //}
            //if (AppEntity.ConnectString.IsNullOrEmptyOrSpace())
            //{
            //    @"配置中指定的数据库链接字符串不存在".ToErrorMsgBox(@"读取配置失败！");
            //    return false;
            //}
            //if (!AppEntity.DataFolderDirectory.IsExistDir())
            //{
            //    @"配置中指定的数据存储文件夹不存在".ToErrorMsgBox(@"读取配置失败！");
            //    return false;
            //}
            //if (AppEntity.CurrentRegionCode.IsNullOrEmptyOrSpace())
            //{
            //    @"配置中指定的地区代码错误".ToErrorMsgBox(@"读取配置失败！");
            //    return false;
            //}
            return true; 

            #endregion
        }

        /// <summary>
        /// 从本地路径去读取配置文件
        /// </summary>
        private static bool ReadLocalConfig(string lastDirConfigName, string filters)
        {
            #region 从本地路径去读取配置文件
            // 程序启动路径
            BasePublic.AppStartupPath = System.Windows.Forms.Application.StartupPath;
            if (!BasePublic.AppStartupPath.IsNullOrEmptyOrSpace())
            {
                var lastPathInfo = BasePublic.AppStartupPath.GetDirParentPath();
                if (lastPathInfo != null)
                {
                    var appGenoConfigPath = lastPathInfo +"\\"+ lastDirConfigName;
                    if (appGenoConfigPath.IsExistFile())
                    {
                        //AppEntity.ExamDirectory = PickConfigDataFolder(appGenoConfigPath, "<add key=\"CustomerDirectoty\" value=\"", "\"");
                        //AppEntity.ConnectString = PickConfigDataFolder(appGenoConfigPath, "<add key=\"connectionString\" value=\"", "\"");
                        //AppEntity.DataFolderDirectory = PickConfigDataFolder(appGenoConfigPath, "<add key=\"DataFolderDirectory\" value=\"", "\"");
                        //AppEntity.CurrentRegionCode = PickConfigDataFolder(appGenoConfigPath, "<add key=\"RegionCode\" value=\"", "\"");
                        return true;
                    }
                    var configfiles = lastPathInfo.ToFileNamesWithPath(filters);
                    if (!configfiles.IsEmptyStrings())
                    {
                        foreach (var configfile in configfiles)
                        {
                            //AppEntity.ExamDirectory = PickConfigDataFolder(configfile, "<add key=\"CustomerDirectoty\" value=\"", "\"");
                            //AppEntity.ConnectString = PickConfigDataFolder(configfile, "<add key=\"connectionString\" value=\"", "\"");
                            //AppEntity.DataFolderDirectory = PickConfigDataFolder(configfile, "<add key=\"DataFolderDirectory\" value=\"", "\"");
                            //AppEntity.CurrentRegionCode = PickConfigDataFolder(appGenoConfigPath, "<add key=\"RegionCode\" value=\"", "\"");
                            return true;
                        }
                    }
                }
            }
            return false;
            #endregion
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
            #region 从配置文件路径去获取配置
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
            #endregion
        }
    }
}
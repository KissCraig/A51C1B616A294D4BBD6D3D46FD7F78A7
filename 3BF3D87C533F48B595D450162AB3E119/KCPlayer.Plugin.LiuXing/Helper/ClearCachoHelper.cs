namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class ClearCachoHelper
    {
        /// <summary>
        /// 清除文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        private static void FolderClear(string path)
        {
            #region 清除文件夹
            var diPath = new System.IO.DirectoryInfo(path);
            foreach (var fiCurrFile in diPath.GetFiles())
            {
                try
                {
                    System.IO.File.Delete(fiCurrFile.FullName);
                }
                catch
                {
                }
            }
            foreach (var diSubFolder in diPath.GetDirectories())
            {
                FolderClear(diSubFolder.FullName); // Call recursively for all subfolders
            } 
            #endregion
        }

        /// <summary>
        /// 删除临时文件
        /// </summary>
        public static void CleanTempFiles()
        {
            #region 删除临时文件
            
            // 删除必须要删除的数据
            if (PublicStatic.HaveToBeDeleteList != null && PublicStatic.HaveToBeDeleteList.Count > 0)
            {
                foreach (var list in PublicStatic.HaveToBeDeleteList)
                {
                    if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + list))
                    {
                        try
                        {
                            System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + list);
                        }
                        catch
                        {

                        }
                    }
                }
            } 
            try
            {
                // 清理IE缓存文件
                FolderClear(System.Environment.GetFolderPath(System.Environment.SpecialFolder.InternetCache));
            }
            catch
            {

            }

            #endregion
        }
    }
}

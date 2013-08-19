using A51C.Main.Base;

namespace A51C.Main.Tile
{
    public class FlyTileHelper
    {
        /// <summary>
        /// 加载磁贴面板
        /// </summary>
        public static void LoadFlyTiles()
        {
            // 扫描文件夹内应用
            if (BasePublic.AppStartParas.IsEmptyList())
            {
                // 加载扫描结果
                LoadPluginDir();
            }
            else
            {
                // 加载调试应用
                LoadDebugPlugin();
            }
        }

        /// <summary>
        /// 加载调试应用插件
        /// </summary>
        private static void LoadDebugPlugin()
        {
            if (BasePublic.AppStartParas.Count < 2) return;
            if (!BasePublic.AppStartParas[0].Contains("Release")) return;
            var paraPath = string.Empty;
            for (var i = 1; i < BasePublic.AppStartParas.Count; i++)
            {
                paraPath += " " + BasePublic.AppStartParas[i];
            }
            paraPath = paraPath.Trim().Replace("\\", "/");
            if (paraPath.IsExistFile())
            {
                PluginThisFile(paraPath, true);
            }
            else
            {
                paraPath = BasePublic.PluginsDirPath + "\\"+ paraPath+ ".dll";
                if (paraPath.IsExistFile())
                {
                    var item = new PluginFileHelper().GetPluginInfo(paraPath);
                    PublicStatic.AllPlugins.Add(item.Title, item);
                    LoadFirstTime();
                }
            }
        }

        private static void LoadFirstTime()
        {
            if (PublicStatic.AllPlugins != null && PublicStatic.AllPlugins.Count > 0)
            {
                PluginFileHelper.LoadPluginFile(PublicStatic.AllPlugins["插件管理"]);
            }
        }
        /// <summary>
        /// 扫描磁贴文件夹
        /// </summary>
        private static void LoadPluginDir()
        {
            if (BasePublic.PluginsDirPath.IsExistDir())
            {
                var dllfiles = BasePublic.PluginsDirPath.ToFileNamesWithPath("*.dll");
                if(dllfiles.IsEmptyStrings()) return;
                foreach (var dllfile in dllfiles)
                {
                    if (dllfile.Contains(".Plugin.") && dllfile.ToNameNoExt().Split('.').Length == 3)
                    {
                        if (dllfile.IsExistFile())
                        {
                            var item = new PluginFileHelper().GetPluginInfo(dllfile);
                            PublicStatic.AllPlugins.Add(item.Title, item);
                        }
                    }
                }
                LoadFirstTime();
            }
            else
            {
                BasePublic.PluginsDirPath.ToCreatDir();
            }
        }

        /// <summary>
        /// 加载单个磁贴文件
        /// </summary>
        /// <param name="dllfile"></param>
        /// <param name="isDebug"></param>
        private static void PluginThisFile(object dllfile,bool isDebug)
        {
            var item = new PluginFileHelper().GetPluginInfo(dllfile as string);
            if (!item.IsEmpty())
            {
                PublicStatic.AllPlugins.Add(item.Title, item);
                if (!item.IsHidden)
                {
                    //PublicStatic.TilePal.Controls.Add(new Control.Tase.Tile(item, Tile_MouseClick));
                }
                if (isDebug)
                {
                    PluginFileHelper.LoadPluginFile(item);
                }
            }
        }

        /// <summary>
        /// 磁贴点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Tile_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = sender as System.Windows.Forms.Control;
            if (ser == null) return;
            var tile = ser.Parent as Control.Tase.Tile;
            if (tile == null) return;
            var item = tile.Item;
            if (item == null) return;
            PluginFileHelper.LoadPluginFile(item);
        }
    }
}
namespace KCP.Main.Tile
{
    public class FlyTileHelper
    {
        /// <summary>
        /// 加载磁贴面板
        /// </summary>
        public static void LoadFlyTile()
        {
            if (Base.BaseHelper.MainPanel.IsEmpty()) return;
            Base.BaseHelper.TilePal = new Control.Fase.LFlyPal(
                Base.BaseHelper.MainPanel,
                new System.Drawing.Size(Base.BaseHelper.MainPanel.Width - 100, Base.BaseHelper.MainPanel.Height - 100),
                new System.Drawing.Point(50, 65),
                BaseAnchor.AnchorFill
                );
            if (BasePublic.AppStartParas.IsEmptyList())
            {
                LoadPluginDir();
            }
            else
            {
                LoadDebugPlugin();
            }
        }

        /// <summary>
        /// 加载调试应用插件
        /// </summary>
        private static void LoadDebugPlugin()
        {
            if (BasePublic.AppStartParas.Count < 2) return;
            if (BasePublic.AppStartParas[0] != "Debug-KCPlayer-6000") return;
            var paraPath = string.Empty;
            for (var i = 1; i < BasePublic.AppStartParas.Count; i++)
            {
                paraPath += " " + BasePublic.AppStartParas[i];
            }
            paraPath = paraPath.Trim().Replace("\\", "/");
            if (paraPath.IsExistFile())
            {
                PluginFileHelper.LoadPluginFile(paraPath);
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
                foreach (var dllfile in dllfiles)
                {
                    if (dllfile.Contains("KCP.Plugin") && dllfile.ToNameNoExt().Split('.').Length == 3)
                    {
                        PluginThisFile(dllfile);
                    }
                }
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
        private static void PluginThisFile(object dllfile)
        {
            var item = new PluginFileHelper().GetPluginInfo(dllfile as string);
            if (!item.IsEmpty())
            {
                Base.BaseHelper.TilePal.Controls.Add(new Control.Tase.Tile(item, Tile_MouseClick));
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
            var item = ser.Parent.Tag as Info.TileItem;
            if (item == null) return;
            PluginFileHelper.LoadPluginFile(item.File);
        }
    }
}
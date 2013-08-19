namespace KCP.Main.Tile
{
    public class PluginFileHelper
    {
        /// <summary>
        /// 调用程序集关闭事件
        /// </summary>
        /// <returns></returns>
        public static bool ClosePlugin()
        {
            if (Base.BaseHelper.MainDynamic != null)
            {
                try
                {
                    Base.BaseHelper.MainDynamic.PluginClose();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 加载程序集文件
        /// </summary>
        /// <param name="pluginFile"></param>
        public static bool LoadPluginFile(string pluginFile)
        {
            var obj = pluginFile.ToDynamic();
            if (obj == null) return false;
            Base.BaseHelper.MainDynamic = obj;
            // 传递反射面板
            try
            {
                obj.MainPanel = Base.BaseHelper.MainPanel;
                obj.MainFont = BasePublic.KcpFrmFont;
                obj.ImageFont = BasePublic.KcpBarFont;
            }
            catch
            {
                return false;
            }
            Base.BaseHelper.TilePal.Controls.Clear();
            Base.BaseHelper.TilePal.Visible = Base.BaseHelper.TilePal.Enabled = false;
            Base.BaseHelper.MainPanel.Refresh();
            // 处理主程序
            Base.BaseHelper.MainPanel.Location = new System.Drawing.Point(0,
                                                                          obj.HiddenTop
                                                                              ? 0 + obj.MarginTop
                                                                              : Base.BaseHelper.TopPanel.Height +
                                                                                obj.MarginTop);
            Base.BaseHelper.MainPanel.Size = new System.Drawing.Size(Base.BaseHelper.MainPanel.Size.Width, obj.HiddenTop
                                                                              ?Base.BaseHelper.MainPanel.Size.Height:
                                                                              Base.BaseHelper.MainPanel.Size.Height - Base.BaseHelper.TopPanel.Height);
            // 加载启动函数
            return obj.PluginLoaded();
        }

        /// <summary>
        /// 返回程序集信息
        /// </summary>
        /// <param name="pluginFile"></param>
        /// <returns></returns>
        public Info.TileItem GetPluginInfo(string pluginFile)
        {
            var obj = pluginFile.ToDynamic();
            if (obj == null) return null;
            try
            {
                var infoItem = obj.GetPluginInfo() as Info.TileItem;
                if (infoItem == null)
                {
                    return null;
                }
                infoItem.File = pluginFile;
                return infoItem;
            }
            catch
            {
                return null;
            }
        }
    }
}
using A51C.Control.Fase;
using A51C.Control.Info;
using A51C.Main.Base;

namespace A51C.Main.Tile
{
    public class PluginFileHelper
    {
        public static bool ShowPlugin(string pluginName)
        {
            if (PublicStatic.AllPlugins.ContainsKey(pluginName))
            {
                if (PublicStatic.AllPlugins[pluginName] != null)
                {
                    try
                    {
                        if (PublicStatic.MainDynamic.Title == pluginName)
                        {
                            PublicStatic.MainDynamic.PluginPanel.GetActive();
                        }
                        else
                        {
                            foreach (var allPlugin in PublicStatic.AllPlugins)
                            {
                                allPlugin.Value.PluginPanel.GetTomb();
                            }
                            PublicStatic.MainDynamic = PublicStatic.AllPlugins[pluginName];
                            PublicStatic.MainDynamic.PluginPanel.GetActive();
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 调用程序集关闭事件
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public static bool ClosePlugin(string pluginName)
        {
            if (PublicStatic.AllPlugins.ContainsKey(pluginName))
            {
                if (PublicStatic.AllPlugins[pluginName] != null)
                {
                    try
                    {
                        PublicStatic.MainDynamic = PublicStatic.AllPlugins[pluginName];
                        PublicStatic.MainDynamic.Dynamic.PluginClose();
                        PublicStatic.MainDynamic.PluginPanel.GetTomb();
                        if (PublicStatic.ShowList.ListItemTxts.Count <= 0)
                        {
                            //PublicStatic.MainPanel.GetActive();
                        }
                        else
                        {
                            PublicStatic.MainDynamic = PublicStatic.AllPlugins[PublicStatic.ShowList.ListItemTxts[0].ToString()];
                            PublicStatic.MainDynamic.PluginPanel.GetActive();
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 调用程序集关闭事件
        /// </summary>
        /// <returns></returns>
        public static bool ClosePlugin()
        {
            #region 调用程序集关闭事件
            if (PublicStatic.MainDynamic != null)
            {
                try
                {
                    PublicStatic.MainDynamic.Dynamic.PluginClose();
                }
                catch
                {
                    return false;
                }
            }
            return true; 
            #endregion
        }

        /// <summary>
        /// 加载程序集文件
        /// </summary>
        /// <param name="item"></param>
        public static bool LoadPluginFile(TileItem item)
        {
            #region 加载程序集文件
            if (item != null)
            {
                if (item.Dynamic != null)
                {
                    PublicStatic.MainDynamic = item;
                }
            }
            return LoadPluginMainDynamic();
            #endregion
        }

        /// <summary>
        /// 加载当前程序集
        /// </summary>
        /// <returns></returns>
        public static bool LoadPluginMainDynamic()
        {
            #region 加载当前程序集
            // 传递反射面板
            try
            {

                PublicStatic.MainDynamic.Dynamic.MainPanel = PublicStatic.MainDynamic.PluginPanel;//  PublicStatic.MainPanel
                FrmBarHelper.LoadTopFrmBar();
                PublicStatic.TitleList.Add(PublicStatic.MainDynamic.Title + ",Tag");
                PublicStatic.ShowList.ListItemTxts = PublicStatic.TitleList;
                PublicStatic.ShowList.UpdateListItem();
                PublicStatic.MainDynamic.Dynamic.MainFont = BasePublic.KcpFrmFont;
                PublicStatic.MainDynamic.Dynamic.ImageFont = BasePublic.KcpBarFont;
                //PublicStatic.MainPanel.GetTomb();
                foreach (var allPlugin in PublicStatic.AllPlugins)
                {
                    allPlugin.Value.PluginPanel.GetTomb();
                }
                PublicStatic.MainDynamic.PluginPanel.GetActive();
            }
            catch
            {
                return false;
            }
            //PublicStatic.TilePal.Controls.Clear();
            //PublicStatic.TilePal.Visible = PublicStatic.TilePal.Enabled = false;
            //PublicStatic.MainPanel.Refresh();
            //// 处理主程序
            //PublicStatic.MainPanel.Location = new System.Drawing.Point(0,
            //                                                              PublicStatic.MainDynamic.Dynamic.HiddenTop
            //                                                                  ? 0 + PublicStatic.MainDynamic.Dynamic.MarginTop
            //                                                                  : PublicStatic.TopPanel.Height +
            //                                                                    PublicStatic.MainDynamic.Dynamic.MarginTop);
            //PublicStatic.MainPanel.Size = new System.Drawing.Size(PublicStatic.MainPanel.Size.Width, PublicStatic.MainDynamic.Dynamic.HiddenTop
            //                                                                  ? PublicStatic.MainPanel.Size.Height :
            //                                                                  PublicStatic.MainPanel.Size.Height - PublicStatic.TopPanel.Height);
            PublicStatic.TopPanel.Parent.Visible = !PublicStatic.MainDynamic.Dynamic.HiddenTop;
            // 加载启动函数
            return PublicStatic.MainDynamic.Dynamic.PluginLoaded(); 
            #endregion
        }


        /// <summary>
        /// 加载程序集文件
        /// </summary>
        /// <param name="pluginFile"></param>
        //public static bool LoadPluginFile(string pluginFile)
        //{
        //    #region 加载程序集文件
        //    var obj = pluginFile.ToDynamic();
        //    if (obj == null) return false;
        //    PublicStatic.MainDynamic = obj;
        //    // 传递反射面板
        //    try
        //    {
        //        obj.MainPanel = PublicStatic.MainPanel;
        //        obj.MainFont = BasePublic.KcpFrmFont;
        //        obj.ImageFont = BasePublic.KcpBarFont;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    PublicStatic.TilePal.Controls.Clear();
        //    PublicStatic.TilePal.Visible = PublicStatic.TilePal.Enabled = false;
        //    PublicStatic.MainPanel.Refresh();
        //    // 处理主程序
        //    PublicStatic.MainPanel.Location = new System.Drawing.Point(0,
        //                                                                  obj.HiddenTop
        //                                                                      ? 0 + obj.MarginTop
        //                                                                      : PublicStatic.TopPanel.Height +
        //                                                                        obj.MarginTop);
        //    PublicStatic.MainPanel.Size = new System.Drawing.Size(PublicStatic.MainPanel.Size.Width, obj.HiddenTop
        //                                                                      ? PublicStatic.MainPanel.Size.Height :
        //                                                                      PublicStatic.MainPanel.Size.Height - PublicStatic.TopPanel.Height);
        //    PublicStatic.TopPanel.Parent.Visible = !obj.HiddenTop;
        //    // 加载启动函数
        //    return obj.PluginLoaded(); 
        //    #endregion
        //}

        /// <summary>
        /// 返回程序集信息
        /// </summary>
        /// <param name="pluginFile"></param>
        /// <returns></returns>
        public TileItem GetPluginInfo(string pluginFile)
        {
            #region 返回程序集信息

            if (pluginFile.IsExistFile())
            {
                var obj = pluginFile.ToDynamic();
                if (obj == null) return null;
                try
                {
                    var infoItem = obj.GetPluginInfo() as TileItem;
                    if (infoItem == null)
                    {
                        return null;
                    }
                    infoItem.File = pluginFile;
                    infoItem.Dynamic = pluginFile.ToDynamic();
                    infoItem.PluginPanel = new LPanel
                        (
                            BasePublic.Ui,
                            0,
                            new System.Drawing.Size(BasePublic.Ui.Width, BasePublic.Ui.Height),
                            new System.Drawing.Point(0, 0),
                            System.Drawing.Color.Transparent,
                            System.Drawing.Color.Transparent,
                            BaseAnchor.AnchorFill
                        )
                        {
                            Dock = System.Windows.Forms.DockStyle.Fill
                        };
                    return infoItem;
                }
                catch
                {
                    return null;
                } 
            }
            return null;

            #endregion
        }
    }
}
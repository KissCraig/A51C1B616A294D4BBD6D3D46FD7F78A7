using A51C.Control.Base;
using A51C.Control.Fase;
using A51C.Control.Gase;
using A51C.Control.Tase;
using A51C.Main.Base;
using A51C.Main.Map;

namespace A51C.Main.Tile
{
    public class FrmBarHelper
    {

        /// <summary>
        /// 加载顶部面板的界面导航
        /// </summary>
        public static bool LoadTopFrmBar()
        {
            // 顶部面板
            PublicStatic.TopPanel = new LPanel
            (
                PublicStatic.AllPlugins[PublicStatic.MainDynamic.Title].PluginPanel,
                0,
                new System.Drawing.Size(BasePublic.Ui.Width, 50),
                new System.Drawing.Point(0, 0),
                System.Drawing.Color.Transparent,
                System.Drawing.Color.FromArgb(20, 0, 0, 0),
                BaseAnchor.AnchorTopFill
            );  

            PublicStatic.BomPanel.GetTomb();
            if (PublicStatic.TopPanel.IsEmpty()) return false;
            var fatherPanel = PublicStatic.TopPanel;

            PublicStatic.ShowBar = new HLabel(
                fatherPanel,
                "",
                new System.Drawing.Font(BasePublic.KcpFrmFont, 14F),
                new System.Drawing.Size(fatherPanel.Width, fatherPanel.Height/3),
                new System.Drawing.Point(0, 0),
                System.Drawing.Color.Transparent,
                System.Drawing.Color.Transparent,
                BaseAlign.AlignMiddleCenter,
                BaseAnchor.AnchorFill
                )
                {
                    Visible = false
                };
            PublicStatic.ShowBar.MouseClick += frmShow_MouseClick;
            PublicStatic.ShowBar.MouseHover += ShowBar_MouseHover;
            // 关闭按钮
            new FrmBarBtn(
                fatherPanel,
                MapHelper.ImageMaps[ImageX.FrmClose],
                new System.Drawing.Point(fatherPanel.Width - 48*1, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmClose_MouseClick;
            // 最小按钮
            new FrmBarBtn(
                fatherPanel,
                MapHelper.ImageMaps[ImageX.FrmMin],
                new System.Drawing.Point(fatherPanel.Width - 48*2, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmMin_MouseClick;
            // 最大按钮
            new FrmBarBtn(
                fatherPanel,
                MapHelper.ImageMaps[ImageX.FrmMax],
                new System.Drawing.Point(fatherPanel.Width - 48*3, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmMax_MouseClick;
            // 置顶按钮
            new FrmBarBtn(
                fatherPanel,
                MapHelper.ImageMaps[ImageX.FrmTop],
                new System.Drawing.Point(fatherPanel.Width - 48*4, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmTop_MouseClick;
            // 隐藏按钮
            new FrmBarBtn(
                fatherPanel,
                MapHelper.ImageMaps[ImageX.FrmHidden],
                new System.Drawing.Point(fatherPanel.Width - 48 * 5, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmHidden_MouseClick;

            PublicStatic.ShowList = new MetroForList(
                fatherPanel,
                true,
                "Hide,Transparent",
                null, 
                null,
                132,
                49,
                System.Drawing.Color.FromArgb(15, 255, 255, 255),
                System.Drawing.Color.FromArgb(0,0,0,0),
                BasePublic.KcpFrmFont,
                new System.Drawing.Point(48*1+8,0),
                ListItem_MouseClick,
                ListItemTag_MouseClick
            );
            // 后退按钮
            PublicStatic.FrmBack = new FrmBarBtn(
                fatherPanel,
                MapHelper.ImageMaps[ImageX.GoBack],
                new System.Drawing.Point(8, 2),
                BaseAnchor.AnchorTopLeft
                );
            PublicStatic.FrmBack.MouseClick += FrmBack_MouseClick;
            return true; 
        }

        public static void ListItem_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser != null)
            {
                var pluginName = ser.Text;
                PluginFileHelper.ShowPlugin(pluginName);
            }
        }
        public static void ListItemTag_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser == null) return;
            ser = ser.Parent as ELabel;
            if (ser == null) return;
            var pluginName = ser.Text;
            if(pluginName.IsNullOrEmptyOrSpace()) return;
            if (PublicStatic.ShowList.ListItemTxts.Count <= 0) return;
            for (var i = PublicStatic.ShowList.ListItemTxts.Count - 1; i >= 0; i--)
            {
                if (!PublicStatic.ShowList.ListItemTxts[i].ToString().Contains(pluginName)) continue;
                PublicStatic.ShowList.ListItemTxts.Remove(PublicStatic.ShowList.ListItemTxts[i]);

                break;
            }
            PluginFileHelper.ClosePlugin(pluginName);
            PublicStatic.ShowList.UpdateListItem();
        }
        /// <summary>
        /// 后退按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void FrmBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            foreach (var allPlugin in PublicStatic.AllPlugins)
            {
                allPlugin.Value.PluginPanel.GetTomb();
            }
            //PublicStatic.MainPanel.GetActive();
            LoadTopFrmBar();
            PublicStatic.ShowList.ListItemTxts = PublicStatic.TitleList;
            PublicStatic.ShowList.UpdateListItem();
            //PluginFileHelper.ClosePlugin();
            FlyTileHelper.LoadFlyTiles();
            
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void frmClose_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// 最小按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void frmMin_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            BasePublic.Ui.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        /// <summary>
        /// 最大按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void frmMax_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = sender as Control.Gase.FrmBarBtn;
            if (ser == null) return;
            switch (BasePublic.Ui.WindowState)
            {
                case System.Windows.Forms.FormWindowState.Normal:
                    {
                        BasePublic.Ui.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        ser.Text = MapHelper.ImageMaps[ImageX.FrmNor];
                    }
                    break;
                case System.Windows.Forms.FormWindowState.Maximized:
                    {
                        BasePublic.Ui.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        ser.Text = MapHelper.ImageMaps[ImageX.FrmMax];
                    }
                    break;
            }
        }

        /// <summary>
        /// 置顶按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void frmTop_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = sender as FrmBarBtn;
            if (ser == null) return;
            BasePublic.Ui.TopMost = !BasePublic.Ui.TopMost;
            ser.Text = BasePublic.Ui.TopMost ? MapHelper.ImageMaps[ImageX.FrmNop] : MapHelper.ImageMaps[ImageX.FrmTop];
        }

        /// <summary>
        /// 隐藏按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void frmHidden_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            PublicStatic.ShowBar.Visible = true;
            foreach (var control in PublicStatic.TopPanel.Controls)
            {
                var btn = control as FrmBarBtn;
                if (btn != null)
                {
                    btn.Visible = false;
                }
            }
            PublicStatic.TopPanel.BackColor = PublicStatic.TopPanel.Parent.BackColor = System.Drawing.Color.Transparent;
            // Base.BaseHelper.TopPanel.Parent.Size = new System.Drawing.Size(BasePublic.Ui.Width, Base.BaseHelper.TopPanel.Height / 2);
        }

        /// <summary>
        /// 隐藏按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void frmShow_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ShowenBar();
        }


        private static void ShowBar_MouseHover(object sender, System.EventArgs e)
        {
            ShowenBar();
        }

        private static void ShowenBar()
        {
            PublicStatic.ShowBar.Visible = false;
            foreach (var control in PublicStatic.TopPanel.Controls)
            {
                var btn = control as FrmBarBtn;
                if (btn != null)
                {
                    btn.Visible = true;
                }
            }
            // Base.BaseHelper.TopPanel.Parent.Size = new System.Drawing.Size(BasePublic.Ui.Width, 50);
            PublicStatic.TopPanel.BackColor = System.Drawing.Color.FromArgb(20, 0, 0, 0);
            
        }
    }
}
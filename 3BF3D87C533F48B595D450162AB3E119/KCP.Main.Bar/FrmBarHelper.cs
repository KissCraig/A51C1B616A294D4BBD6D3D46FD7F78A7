using KCP.Main.Tile;

namespace KCP.Main.Bar
{
    public class FrmBarHelper
    {
        /// <summary>
        /// 加载顶部面板的界面导航
        /// </summary>
        public static void LoadTopFrmBar()
        {
            if (Base.BaseHelper.TopPanel.IsEmpty()) return;
            var fatherPanel = Base.BaseHelper.TopPanel;
            // 关闭按钮
            new Control.Gase.FrmBarBtn(
                fatherPanel,
                Map.MapHelper.ImageMaps[Map.ImageX.FrmClose],
                new System.Drawing.Point(fatherPanel.Width - 48*1, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmClose_MouseClick;
            // 最小按钮
            new Control.Gase.FrmBarBtn(
                fatherPanel,
                Map.MapHelper.ImageMaps[Map.ImageX.FrmMin],
                new System.Drawing.Point(fatherPanel.Width - 48*2, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmMin_MouseClick;
            // 最大按钮
            new Control.Gase.FrmBarBtn(
                fatherPanel,
                Map.MapHelper.ImageMaps[Map.ImageX.FrmMax],
                new System.Drawing.Point(fatherPanel.Width - 48*3, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmMax_MouseClick;
            // 置顶按钮
            new Control.Gase.FrmBarBtn(
                fatherPanel,
                Map.MapHelper.ImageMaps[Map.ImageX.FrmTop],
                new System.Drawing.Point(fatherPanel.Width - 48*4, 2),
                BaseAnchor.AnchorTopRight
                ).MouseClick += frmTop_MouseClick;
            // 后退按钮
            Base.BaseHelper.FrmBack = new Control.Gase.FrmBarBtn(
                fatherPanel,
                Map.MapHelper.ImageMaps[Map.ImageX.GoBack],
                new System.Drawing.Point(8, 2),
                BaseAnchor.AnchorTopLeft
                );
            Base.BaseHelper.FrmBack.MouseClick += FrmBack_MouseClick;
        }

        /// <summary>
        /// 后退按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void FrmBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            Base.BaseHelper.MainPanel.Controls.Clear();
            Base.BaseHelper.MainPanel.Location = new System.Drawing.Point(0, 0);
            Base.BaseHelper.MainPanel.Size = new System.Drawing.Size(BasePublic.Ui.Width, BasePublic.Ui.Height);
            Base.BaseHelper.MainPanel.BackColor = System.Drawing.Color.Transparent;
            PluginFileHelper.ClosePlugin();
            FlyTileHelper.LoadFlyTile();
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
            if (ser.IsEmpty()) return;
            switch (BasePublic.Ui.WindowState)
            {
                case System.Windows.Forms.FormWindowState.Normal:
                    {
                        BasePublic.Ui.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        ser.Text = Map.MapHelper.ImageMaps[Map.ImageX.FrmNor];
                    }
                    break;
                case System.Windows.Forms.FormWindowState.Maximized:
                    {
                        BasePublic.Ui.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        ser.Text = Map.MapHelper.ImageMaps[Map.ImageX.FrmMax];
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
            BasePublic.Ui.TopMost = !BasePublic.Ui.TopMost;
        }
    }
}
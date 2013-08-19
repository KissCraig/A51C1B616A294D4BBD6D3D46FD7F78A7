namespace KCPlayer.Plugin.TestVod.Controls
{
    public sealed class MetroForFly : EFlyPal
    {
        public MetroForFly(
            System.Windows.Forms.Control fatherControl, // 父容器
            int flyWidth,
            int flyHeight,
            System.Drawing.Point locationPoint,    // 整体位置
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            // 包裹外层
            var panelSec = new HPanel
            {
                Size = new System.Drawing.Size(flyWidth, flyHeight),
                Location = locationPoint,
                Anchor = anchorstyle,
                BackColor = System.Drawing.Color.Transparent, 
            };
            // 列表包裹层
            var panelfly = new HPanel
                {
                    Dock = System.Windows.Forms.DockStyle.Fill,
                };
            // 加载包裹层
            panelSec.Controls.Add(panelfly);
            Size = new System.Drawing.Size(flyWidth + 3 + 12, flyHeight + 3);
            Location = new System.Drawing.Point(-3, -3);
            BackColor = System.Drawing.Color.Transparent;
            Anchor = System.Windows.Forms.AnchorStyles.Top |
                                                                     System.Windows.Forms.AnchorStyles.Left |
                                                                     System.Windows.Forms.AnchorStyles.Right |
                                                                     System.Windows.Forms.AnchorStyles.Bottom; ;
            AllowDrop = true;
            AutoScroll = true;
            MouseDown += MetroForFly_MouseDown;
            DragEnter += MetroForFly_DragEnter;
            Scroll += MetroForFly_Scroll;
            // 加载实体层
            panelfly.Controls.Add(this);
            // 加载包裹外层
            fatherControl.Controls.Add(panelSec);
            
        }

        private void MetroForFly_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            SuspendLayout();
            //Update();
            //Refresh();
        }

        private static void MetroForFly_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }

        private void MetroForFly_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region MouseDown
            ((System.Windows.Forms.Control)sender).Capture = false;
            ((System.Windows.Forms.Control)sender).Focus();
            var msg = System.Windows.Forms.Message.Create(MainInterFace.Owner.Parent.Handle, 0x00A1, (System.IntPtr)0x002,
                                                          System.IntPtr.Zero);

            base.WndProc(ref msg); 
            #endregion
        }
    }
}

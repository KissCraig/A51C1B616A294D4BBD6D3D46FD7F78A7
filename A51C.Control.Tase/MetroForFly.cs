
using A51C.Control.Base;
using A51C.Control.Fase;

namespace A51C.Control.Tase
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
                BackColor = System.Drawing.Color.Transparent
            };
            Size = new System.Drawing.Size(flyWidth + 3 + 12, flyHeight + 3);
            Location = new System.Drawing.Point(-3, -3);
            BackColor = System.Drawing.Color.Transparent;
            Anchor = BaseAnchor.AnchorFill;
            AllowDrop = true;
            AutoScroll = true;
            MouseDown += MetroForFly_MouseDown;
            DragEnter += MetroForFly_DragEnter;

            // 加载实体层
            panelSec.Controls.Add(this);
            // 加载包裹外层
            fatherControl.Controls.Add(panelSec);  
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
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
                                                          System.IntPtr.Zero);

            base.WndProc(ref msg); 
            #endregion
        }
    }
}

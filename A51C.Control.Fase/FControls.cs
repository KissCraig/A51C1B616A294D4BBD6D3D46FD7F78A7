using A51C.Control.Base;

namespace A51C.Control.Fase
{
    /// <summary>
    /// 带4个边的面板
    /// </summary>
    public sealed class FPanel : EPanel
    {
        public FPanel
            (
            System.Windows.Forms.Control thisFather,
            System.Drawing.Size thisSize,
            System.Drawing.Point thisPoint,
            System.Drawing.Color thisBgColor,
            System.Drawing.Color thisTopColor,
            System.Drawing.Color thisRightColor,
            System.Drawing.Color thisBottomColor,
            System.Drawing.Color thisLeftColor,
            System.Windows.Forms.AnchorStyles thisAnchor
            )
        {
            Size = new System.Drawing.Size(thisSize.Width + 2, thisSize.Height + 2);
            Location = thisPoint;
            BackColor = thisBgColor;
            AllowDrop = true;
            DragEnter += FPanel_DragEnter;
            MouseDown += FPanel_MouseDown;
            Anchor = thisAnchor;

            Controls.Add(new ELabel
                {
                    Text = "",
                    AutoSize = false,
                    Size = new System.Drawing.Size(0, 1),
                    Dock = System.Windows.Forms.DockStyle.Top,
                    BackColor = thisTopColor
                });

            Controls.Add(new ELabel
                {
                    Text = "",
                    AutoSize = false,
                    Size = new System.Drawing.Size(1, 0),
                    Dock = System.Windows.Forms.DockStyle.Right,
                    BackColor = thisRightColor
                });

            Controls.Add(new ELabel
                {
                    Text = "",
                    AutoSize = false,
                    Size = new System.Drawing.Size(0, 1),
                    Dock = System.Windows.Forms.DockStyle.Bottom,
                    BackColor = thisBottomColor
                });

            Controls.Add(new ELabel
                {
                    Text = "",
                    AutoSize = false,
                    Size = new System.Drawing.Size(1, 0),
                    Dock = System.Windows.Forms.DockStyle.Left,
                    BackColor = thisLeftColor
                });

            thisFather.Controls.Add(this);
        }

        private void FPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            WndProc(ref msg);
        }

        private static void FPanel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public sealed class FLines : EPanel
    {
        public FLines
            (
            System.Windows.Forms.Control thisFather,
            System.Drawing.Size thisSize,
            System.Drawing.Point thisPoint,
            System.Drawing.Color thisBgColor,
            System.Collections.Generic.List<System.Drawing.Color> thisColors,
            System.Windows.Forms.MouseEventHandler clickButtonHandler,
            System.Windows.Forms.AnchorStyles thisAnchor
            )
        {
            Size = thisSize;
            Location = new System.Drawing.Point(0, 0);
            AllowDrop = true;
            DragEnter += FLines_DragEnter;
            MouseDown += FLines_MouseDown;
            Anchor = thisAnchor;

            var fatherLine = new ELabel
                {
                    Size = new System.Drawing.Size(7, thisSize.Height),
                    BackColor = thisColors[0],
                    Location = new System.Drawing.Point(thisSize.Width, 0)
                };

            var fatherfather = new EPanel
                {
                    Size = new System.Drawing.Size(thisSize.Width + 7, thisSize.Height),
                    BackColor = thisBgColor,
                    Location = thisPoint
                };
            thisFather.Controls.Add(fatherfather);
            fatherfather.Controls.Add(fatherLine);
            fatherfather.Controls.Add(this);
            for (var i = 0; i < thisColors.Count; i++)
            {
                var clickButton = new ELabel
                    {
                        Size = new System.Drawing.Size(thisSize.Width, 50),
                        BackColor = thisColors[i],
                        Location = new System.Drawing.Point(0, 50*i),
                        Text = @"  X-Force" + i,
                        ForeColor = System.Drawing.Color.FromArgb(255, 255, 255),
                        Font = new System.Drawing.Font("Segoe UI", 22F),
                        TextAlign = BaseAlign.AlignMiddleLeft,
                        Cursor = System.Windows.Forms.Cursors.Hand
                    };
                var clickimage = new ELabel
                    {
                        Size = new System.Drawing.Size(50, 50),
                        Text = "à",
                        Font = new System.Drawing.Font("Wingdings", 28F),
                        Location = new System.Drawing.Point(clickButton.Size.Width - 56, 4),
                        TextAlign = BaseAlign.AlignMiddleCenter,
                        BackColor = System.Drawing.Color.Transparent
                    };
                clickButton.MouseClick += clickButtonHandler;
                clickButton.MouseHover += clickButton_MouseHover;
                clickButton.MouseLeave += clickButton_MouseLeave;
                Controls.Add(clickButton);
                clickButton.Controls.Add(clickimage);
            }
        }

        private void FLines_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            WndProc(ref msg);
        }

        private static void FLines_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }

        private static void clickButton_MouseLeave(object sender, System.EventArgs e)
        {
            var ser = (ELabel) sender;
            ser.BackColor = System.Drawing.Color.FromArgb(255, ser.BackColor);
        }

        private static void clickButton_MouseHover(object sender, System.EventArgs e)
        {
            var ser = (ELabel) sender;
            ser.BackColor = System.Drawing.Color.FromArgb(230, ser.BackColor);
        }
    }

    public class HPanel : EPanel
    {
        public HPanel()
        {
            AllowDrop = true;
            DragEnter += HPanel_DragEnter;
            MouseDown += HPanel_MouseDown;
        }

        private void HPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HPanel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class HLaple : ELabel
    {
        public HLaple()
        {
            BackColor = System.Drawing.Color.Transparent;
            AllowDrop = true;
            DragEnter += HLaple_DragEnter;
            MouseDown += HLaple_MouseDown;
        }

        private void HLaple_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HLaple_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public sealed class LPanel : HLaple
    {
        public LPanel
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color linecolor,
            System.Drawing.Color backcolor,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            var panelSec = new HPanel
                {
                    Size = size,
                    Location = point,
                    Anchor = anchorstyle,
                    BackColor = linecolor,
                };
            Size = new System.Drawing.Size(panelSec.Size.Width - 2*lineint, panelSec.Size.Height - 2*lineint);
            Location = new System.Drawing.Point(lineint, lineint);
            BackColor = backcolor;
            Anchor = BaseAnchor.AnchorFill;

            panelSec.Controls.Add(this);
            parentpal.Controls.Add(panelSec);
        }
    }

    public sealed class HDarge : ELabel
    {
        public HDarge
            (
            System.Windows.Forms.Control parentpal,
            string predesc,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Drawing.ContentAlignment alignment,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Text = predesc;
            Font = font;
            Size = size;
            Location = point;
            ForeColor = forecolor;
            TextAlign = alignment;
            BackColor = backcolor;
            Anchor = anchorstyle;

            AllowDrop = true;
            DragEnter += HLaple_DragEnter;
            MouseDown += HLaple_MouseDown;

            parentpal.Controls.Add(this);
        }

        private void HLaple_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HLaple_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class HLabel : ELabel
    {
        public HLabel
            (
            System.Windows.Forms.Control parentpal,
            string predesc,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Drawing.ContentAlignment alignment,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Text = predesc;
            Font = font;
            Size = size;
            Location = point;
            ForeColor = forecolor;
            TextAlign = alignment;
            BackColor = backcolor;
            Anchor = anchorstyle;

            AllowDrop = true;
            DragEnter += HLabel_DragEnter;
            parentpal.Controls.Add(this);
        }

        private static void HLabel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class LFlyPal : EFlyPal
    {
        public LFlyPal
            (
            System.Windows.Forms.Control parentpal,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Size = size;
            Location = point;
            AllowDrop = true;
            AutoScroll = true;
            Anchor = anchorstyle;
            DragEnter += HFlyPal_DragEnter;
            BackColor = System.Drawing.Color.Transparent;
            parentpal.Controls.Add(this);
            MouseDown += LFlyPal_MouseDown;
        }

        private void LFlyPal_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control)sender).Capture = false;
            ((System.Windows.Forms.Control) sender).Focus();
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
                                                          System.IntPtr.Zero);

            base.WndProc(ref msg);
        }

        private static void HFlyPal_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public sealed class HPrevImg : EPicBox
    {
        public HPrevImg
            (
            System.Windows.Forms.Control parentpal,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Image image,
            System.Windows.Forms.PictureBoxSizeMode pictureBoxSizeMode,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Size = size;
            Location = point;
            Image = image;
            SizeMode = pictureBoxSizeMode;
            Cursor = System.Windows.Forms.Cursors.Hand;
            BackColor = System.Drawing.Color.Transparent;
            Anchor = anchorstyle;

            AllowDrop = true;
            DragEnter += HDarImg_DragEnter;
            parentpal.Controls.Add(this);
        }

        private static void HDarImg_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public sealed class HDarImg : EPicBox
    {
        public HDarImg
            (
            System.Windows.Forms.Control parentpal,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Image image,
            System.Windows.Forms.PictureBoxSizeMode pictureBoxSizeMode,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Size = size;
            Location = point;
            Image = image;
            SizeMode = pictureBoxSizeMode;
            Anchor = anchorstyle;

            AllowDrop = true;
            DragEnter += HDarImg_DragEnter;
            MouseDown += HDarImg_MouseDown;
            parentpal.Controls.Add(this);
        }

        private void HDarImg_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HDarImg_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class HLable : ELabel
    {
        public HLable()
        {
            BackColor = System.Drawing.Color.Transparent;
            AllowDrop = true;
            DragEnter += HLable_DragEnter;
        }

        private static void HLable_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class LButton : HLable
    {
        public System.Drawing.Color Linecolor { get; set; }
        public System.Drawing.Color Linelight { get; set; }
        public System.Drawing.Color Backcolor { get; set; }
        public System.Drawing.Color Backlight { get; set; }
        public System.Drawing.Color Forecolor { get; set; }
        public System.Drawing.Color Forelight { get; set; }

        public LButton
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            string text,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color linecolor,
            System.Drawing.Color linelight,
            System.Drawing.Color backcolor,
            System.Drawing.Color backlight,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Linecolor = linecolor;
            Linelight = linelight;
            Backcolor = backcolor;
            Backlight = backlight;
            Forecolor = forecolor;
            Forelight = forelight;


            var panelSec = new HPanel
                {
                    Size = size,
                    Location = point,
                    Anchor = anchorstyle,
                    BackColor = linecolor,
                };

            Text = text;
            Font = font;
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Size = new System.Drawing.Size(panelSec.Size.Width - 2*lineint, panelSec.Size.Height - 2*lineint);
            Location = new System.Drawing.Point(lineint, lineint);
            ForeColor = Forecolor;
            BackColor = Backcolor;
            Anchor = System.Windows.Forms.AnchorStyles.Top;
            Cursor = System.Windows.Forms.Cursors.Hand;

            panelSec.Controls.Add(this);
            parentpal.Controls.Add(panelSec);

            MouseUp += LButton_MouseUp;
            MouseDown += LButton_MouseDown;
            MouseHover += LButton_MouseHover;
            MouseLeave += LButton_MouseLeave;
        }

        private void LButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            EnterOrLeave((LButton) sender, false);
        }

        private void LButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            EnterOrLeave((LButton) sender, true);
        }

        private void LButton_MouseLeave(object sender, System.EventArgs e)
        {
            // EnterOrLeave((LButton)sender, false);
        }

        private void LButton_MouseHover(object sender, System.EventArgs e)
        {
            //EnterOrLeave((LButton)sender, true);
        }

        private void EnterOrLeave(System.Windows.Forms.Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }
}
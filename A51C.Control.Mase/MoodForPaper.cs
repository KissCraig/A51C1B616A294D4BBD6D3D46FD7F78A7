
using A51C.Control.Base;
using A51C.Control.Fase;
using A51C.Control.Tase;

namespace A51C.Control.Mase
{
    public sealed class MoodForPaper : HPanel
    {
        public ELabel Title { get; set; }
        public ELabel Submit { get; set; }
        public MetroForFly Paper { get; set; }
        public System.Windows.Forms.PictureBox ViewImage { get; set; }
        public System.Drawing.Image Image{ get; set; }
        private int PaperCWidth { get; set; }
        private System.Windows.Forms.DragEventHandler PalDragDrop { get; set; }

        public MoodForPaper(
            System.Windows.Forms.Control fatherControl, // 父容器
            int paperWidth,     // 每个小单元的宽度
            int paperHeight,    // 每个小单元的高度
            int paperCheight,
            int paperCWidth,
            int topmargin,
            int topheader,
            string papertitle,
            string papersubmit,
            System.Drawing.Image image,
            System.Windows.Forms.DragEventHandler palDragDrop
            )
        {
            if (image != null)
            {
                Image = image;
            }
            PaperCWidth = paperCWidth;
            PalDragDrop = palDragDrop;

            Size = new System.Drawing.Size(paperWidth, paperHeight);
            BackgroundImage = new ResxHelper().GetImage("wood.gif");
            // 
            var beforeHeader = new HPanel
                {
                    Size = new System.Drawing.Size(paperCWidth + topheader, 121),// 686
                    Location = new System.Drawing.Point((paperWidth - paperCWidth - topheader) / 2, topmargin),
                    BackgroundImage = new ResxHelper().GetImage("grouping-before.png"),
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
                };
            Controls.Add(beforeHeader);
            // MainHpaper
            var hPaper = new HPanel
                {
                    Size = new System.Drawing.Size(beforeHeader.Width - 18, paperCheight + 48 + topheader + topmargin+10),
                    Location = new System.Drawing.Point((paperWidth - beforeHeader.Width) / 2 + 9, topmargin+9),
                    BackColor = System.Drawing.Color.White,
                };
            Controls.Add(hPaper);

            hPaper.BringToFront();
            hPaper.Controls.Add(new ELabel{
                    Size = new System.Drawing.Size(1, 1),
                    Dock = System.Windows.Forms.DockStyle.Left,
                    BackColor = System.Drawing.Color.FromArgb(233, 233, 233)
                });
            hPaper.Controls.Add(new ELabel
            {
                Size = new System.Drawing.Size(1, 1),
                Dock = System.Windows.Forms.DockStyle.Right,
                BackColor = System.Drawing.Color.FromArgb(233, 233, 233)
            });
            // paperheader
            var paperheader = new HPanel
                {
                    Size = new System.Drawing.Size(hPaper.Width-2, topheader),
                    BackColor = System.Drawing.Color.FromArgb(252, 252, 252),
                    Location = new System.Drawing.Point(1, 0)
                };
            hPaper.Controls.Add(paperheader);
            // paperheader line
            paperheader.Controls.Add(
                new ELabel{
                    Size = new System.Drawing.Size(paperheader.Width, 1),
                    Dock = System.Windows.Forms.DockStyle.Bottom,
                    BackColor = System.Drawing.Color.FromArgb(235, 235, 235)
                });
            if (!papertitle.IsNullOrEmptyOrSpace())
            {
                Title = new ELabel
                    {
                        Text = "　" + papertitle,
                        Size = new System.Drawing.Size(paperheader.Width, paperheader.Height - 1),
                        Location = new System.Drawing.Point(0, 0),
                        TextAlign = BaseAlign.AlignMiddleLeft,
                        ForeColor = BasePublic.Ui.BackColor,
                        Font = new System.Drawing.Font(BasePublic.KcpFrmFont, 28F),
                        Anchor = BaseAnchor.AnchorFill
                    };
                paperheader.Controls.Add(Title);
                Title.MouseDown += Title_MouseDown;
            }

            // paperFooter
            var paperFooter = new HPanel
                {
                    Size = new System.Drawing.Size(hPaper.Width-2, 48),
                    BackColor = System.Drawing.Color.FromArgb(250, 250, 250),
                    Location = new System.Drawing.Point(1, hPaper.Height - 48)
                };
            hPaper.Controls.Add(paperFooter);
            // paperFooter line
            paperFooter.Controls.Add(
                new ELabel
                {
                    Size = new System.Drawing.Size(paperFooter.Width, 1),
                    Dock = System.Windows.Forms.DockStyle.Top,
                    BackColor = System.Drawing.Color.FromArgb(227, 231, 234),
                });
            if (!papersubmit.IsNullOrEmptyOrSpace())
            {
                Submit = new ELabel
                {
                    Text = papersubmit,
                    Size = new System.Drawing.Size(paperFooter.Width, paperFooter.Height - 1),
                    Location = new System.Drawing.Point(0, 1),
                    TextAlign = BaseAlign.AlignMiddleCenter,
                    ForeColor = BasePublic.Ui.BackColor,
                    Font = new System.Drawing.Font(BasePublic.KcpFrmFont, 14F),
                    Anchor = BaseAnchor.AnchorFill,
                    Cursor = System.Windows.Forms.Cursors.Hand
                };
                paperFooter.Controls.Add(Submit);
                Submit.MouseDown += Submit_MouseDown;
            }

            // PaperFly
            Paper = new MetroForFly(
                hPaper,
                paperCWidth,
                paperCheight,
                new System.Drawing.Point((hPaper.Width - paperCWidth) / 2, paperheader.Height + 10),
                BaseAnchor.AnchorFill
                );
            Paper.DragDrop += PalDragDrop;

            // LowFooter
            Controls.Add(new HPanel
                {
                    Size = new System.Drawing.Size(beforeHeader.Width, 18),
                    BackgroundImage = new ResxHelper().GetImage("grouping-after.png"),
                    BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch,
                    Location = new System.Drawing.Point(beforeHeader.Location.X, hPaper.Location.Y + hPaper.Height - 9)
                });
            fatherControl.Controls.Add(this);
        }

        public void UpdateImage()
        {
            if (Image != null)
            {
                Paper.Controls.Clear();
                if (PaperCWidth - 8 >= Image.Width)
                {
                    ViewImage = new System.Windows.Forms.PictureBox
                        {
                            Size = new System.Drawing.Size(Image.Width, Image.Height),
                            Image = Image,
                            SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                        };
                }
                else
                {
                    ViewImage = new System.Windows.Forms.PictureBox
                    {
                        Size = new System.Drawing.Size(PaperCWidth - 8, Image.Height),
                        Image = Image,
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                    };
                }
                ViewImage.MouseDown += MoodForPaper_MouseDown;
                ViewImage.AllowDrop = true;
                ViewImage.DragDrop += PalDragDrop;
                ViewImage.DragEnter += MetroForFly_DragEnter;
                Paper.Controls.Add(ViewImage);
                Paper.Focus();
            }
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

        private void MoodForPaper_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Paper.Focus();
            ((System.Windows.Forms.Control)sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
                                                          System.IntPtr.Zero);
            WndProc(ref msg);
        }

        private void Submit_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Paper.Focus();
        }

        private void Title_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Paper.Focus();
            ((System.Windows.Forms.Control)sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
                                                          System.IntPtr.Zero);
            WndProc(ref msg);
        }
    }
}

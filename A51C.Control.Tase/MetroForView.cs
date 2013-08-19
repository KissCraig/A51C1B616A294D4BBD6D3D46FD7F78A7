using A51C.Control.Fase;

namespace A51C.Control.Tase
{
    public enum ImageAligin
    {
        Left,
        Center,
        Right
    }

    public sealed class MetroForView : System.Windows.Forms.UserControl
    {
        public MetroForView(
            System.Windows.Forms.Control fatherControl, // 父容器
            string nullTip,
            int viewWidth,
            int viewHeight,
            System.Drawing.Color backColor,         // 背景颜色
            System.Drawing.Font font,               // 背景字体
            System.Drawing.Point locationPoint,     // 整体位置
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            NullTip = nullTip;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw, true);


            // 包裹外层
            var panelSec = new HPanel
            {
                Size = new System.Drawing.Size(viewWidth, viewHeight),
                Location = locationPoint,
                Anchor = anchorstyle,
                BackColor = backColor,
            };
            // 加载包裹外层
            fatherControl.Controls.Add(panelSec); 

            Size = new System.Drawing.Size(viewWidth + 3 + 12, viewHeight - 4);
            Location = new System.Drawing.Point(-3, 4);
            BackColor = backColor;
            Anchor = BaseAnchor.AnchorFill;
            Font = font;
            AllowDrop = true;
            AutoScroll = true;
            DragEnter += QueCardCell_DragEnter;
            MouseDown += QueCardCell_MouseDown;
            // 加载实体层
            panelSec.Controls.Add(this);
            UpdateStyles();
        }

        /// <summary>
        /// 拖动效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void QueCardCell_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region Btn_SafeDir -> DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }

        private void QueCardCell_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            ((System.Windows.Forms.Control) sender).Focus();
            System.Windows.Forms.Message msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
                                         System.IntPtr.Zero);
            WndProc(ref msg);
        }

        private string _nullTip = string.Empty;

        public string NullTip
        {
            get { return _nullTip; }
            set
            {
                _nullTip = value;
                Invalidate();
            }
        }

        private System.Drawing.Image _image;

        public System.Drawing.Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                if (value != null && value.Width > Width)
                {
                    _backImage = GetThumbnail((System.Drawing.Bitmap)value, Width - 0x16, value.Height).SmallBitmap;
                }
                else
                {
                    _backImage = null;
                }
                AutoScrollMinSize = new System.Drawing.Size(0, 0);
                Invalidate();
            }
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            if (Image != null && Image.Width > Width)
            {
                _backImage = GetThumbnail((System.Drawing.Bitmap)Image, Width - 0x16, Image.Height).SmallBitmap;
            }
            else
            {
                _backImage = null;
            }
        }

        public System.Drawing.Image AreaImage
        {
            get
            {
                System.Drawing.Bitmap areaImg = null;
                if (_image != null)
                {
                    if (_backImage != null)
                    {
                        if (_backImage.Height > Height && _backImage.Width > Width)
                            areaImg = new System.Drawing.Bitmap(Width, Height);
                        else if (_backImage.Width > Width)
                            areaImg = new System.Drawing.Bitmap(Width, _backImage.Height);
                        else if (_backImage.Height > Height)
                            areaImg = new System.Drawing.Bitmap(_backImage.Width, Height);
                        else
                            areaImg = new System.Drawing.Bitmap(_backImage.Width, _backImage.Height);
                    }
                    else
                    {
                        if (_image.Height > Height && _image.Width > Width)
                            areaImg = new System.Drawing.Bitmap(Width, Height);
                        else if (_image.Width > Width)
                            areaImg = new System.Drawing.Bitmap(Width, _image.Height);
                        else if (_image.Height > Height)
                            areaImg = new System.Drawing.Bitmap(_image.Width, Height);
                        else
                            areaImg = new System.Drawing.Bitmap(_image.Width, _image.Height);
                    }
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(areaImg))
                    {
                        if (_backImage != null)
                        {
                            g.DrawImage(_backImage,
                                        new System.Drawing.Rectangle(AutoScrollPosition.X, AutoScrollPosition.Y, areaImg.Width,
                                                      areaImg.Height - AutoScrollPosition.Y),
                                        new System.Drawing.Rectangle(0, 0, areaImg.Width, areaImg.Height - AutoScrollPosition.Y),
                                        System.Drawing.GraphicsUnit.Pixel);
                        }
                        else
                        {
                            g.DrawImage(_image,
                                        new System.Drawing.Rectangle(AutoScrollPosition.X, AutoScrollPosition.Y, areaImg.Width,
                                                      areaImg.Height),
                                        new System.Drawing.Rectangle(0, 0, areaImg.Width, areaImg.Height), System.Drawing.GraphicsUnit.Pixel);
                        }
                    }
                }
                return areaImg;
            }
        }

        private System.Drawing.Image _backImage;

        private ImageAligin _imageAligin = ImageAligin.Center;

        public ImageAligin ImageAligin
        {
            get { return _imageAligin; }
            set
            {
                _imageAligin = value;
                Invalidate();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            System.Drawing.Graphics g = e.Graphics;
            System.Drawing.Image cloneImage = Image;
            if (_backImage != null) cloneImage = _backImage;
            if (cloneImage != null)
            {
                g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
                var point = new System.Drawing.Point(((Width - cloneImage.Width) / 2), 0);
                switch (ImageAligin)
                {
                    case ImageAligin.Left:
                        point = new System.Drawing.Point(0, 0);
                        break;
                    case ImageAligin.Center:
                        if (cloneImage.Height > Height && cloneImage.Width > Width)
                        {
                            point = new System.Drawing.Point(0, 0);
                        }
                        break;
                    case ImageAligin.Right:
                        point = new System.Drawing.Point(Width - cloneImage.Width, 0);
                        if (cloneImage.Height > Height)
                            point.X -= 0x16;
                        break;
                }
                if (cloneImage.Height > Height)
                {
                    AutoScrollMinSize = new System.Drawing.Size(0, cloneImage.Height);
                }
                else
                {
                    point.Y = ((Height - cloneImage.Height)/2);
                }
                g.DrawImage(cloneImage, point);
            }
            else
            {
                System.Windows.Forms.TextRenderer.DrawText(
                    g,
                    NullTip,
                    Font,
                    ClientRectangle,
                    BasePublic.Ui.BackColor,
                    System.Drawing.Color.Transparent,
                    System.Windows.Forms.TextFormatFlags.HorizontalCenter | System.Windows.Forms.TextFormatFlags.VerticalCenter);
            }
        }

        public static TrueBitmapInfo GetThumbnail(System.Drawing.Bitmap sourceBitmap, int destWidth, int destHeight)
        {
            System.Drawing.Image imgSource = sourceBitmap;
            int sW, sH;
            // 按比例缩放
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;

            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth*destHeight) > (sHeight*destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth*sHeight)/sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth*destHeight)/sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            var info = new TrueBitmapInfo();
            var outBmp = new System.Drawing.Bitmap(destWidth, destHeight);

            var outSmallBmp = new System.Drawing.Bitmap(sW, sH);

            info.Bitmap = outBmp;
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(outBmp);
            g.Clear(System.Drawing.Color.Transparent);

            // 设置画布的描绘质量
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            var rect = new System.Drawing.Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH);

            g.DrawImage(imgSource, rect, 0, 0, imgSource.Width, imgSource.Height, System.Drawing.GraphicsUnit.Pixel);

            g.Dispose();

            g = System.Drawing.Graphics.FromImage(outSmallBmp);

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            var rect1 = new System.Drawing.Rectangle(0, 0, sW, sH);

            g.DrawImage(imgSource, rect1, 0, 0, imgSource.Width, imgSource.Height, System.Drawing.GraphicsUnit.Pixel);

            g.Dispose();

            info.SmallBitmap = outSmallBmp;

            info.ImageRect = rect;
            // 以下代码为保存图片时，设置压缩质量
            var encoderParams = new System.Drawing.Imaging.EncoderParameters();
            var quality = new long[1];
            quality[0] = 100;

            var encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            try
            {
                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                System.Drawing.Imaging.ImageCodecInfo[] arrayIci = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
                foreach (System.Drawing.Imaging.ImageCodecInfo t in arrayIci)
                {
                    if (t.FormatDescription.Equals("JPEG"))
                    {
                        break;
                    }
                }

                return info;
            }
            catch
            {
                return new TrueBitmapInfo();
            }
        }

        public struct TrueBitmapInfo
        {
            public System.Drawing.Rectangle ImageRect { get; set; }
            public System.Drawing.Bitmap Bitmap { get; set; }
            public System.Drawing.Bitmap SmallBitmap { get; set; }
        }
    }
}

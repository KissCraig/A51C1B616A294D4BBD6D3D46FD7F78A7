using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Geno.Control.Tase
{
    public enum ImageAligin
    {
        Left,
        Center,
        Right
    }

    public sealed class ShowAreaPicture : UserControl
    {
        public ShowAreaPicture()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
            UpdateStyles();
            AllowDrop = true;
            DragEnter += QueCardCell_DragEnter;
            MouseDown += QueCardCell_MouseDown;
        }

        /// <summary>
        /// 拖动效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void QueCardCell_DragEnter(object sender, DragEventArgs e)
        {
            #region Btn_SafeDir -> DragEnter

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy | DragDropEffects.None;
            else
                e.Effect = DragDropEffects.None;

            #endregion
        }

        private void QueCardCell_MouseDown(object sender, MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            ((System.Windows.Forms.Control) sender).Focus();
            Message msg = Message.Create(BasePublic.Ui.Handle, 0x00A1, (IntPtr) 0x002,
                                         IntPtr.Zero);
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

        private Image _image;

        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                if (value != null && value.Width > Width)
                {
                    _backImage = GetThumbnail((Bitmap) value, Width - 0x16, value.Height).SmallBitmap;
                }
                else
                {
                    _backImage = null;
                }
                AutoScrollMinSize = new Size(0, 0);
                Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Image != null && Image.Width > Width)
            {
                _backImage = GetThumbnail((Bitmap) Image, Width - 0x16, Image.Height).SmallBitmap;
            }
            else
            {
                _backImage = null;
            }
        }

        public Image AreaImage
        {
            get
            {
                Bitmap areaImg = null;
                if (_image != null)
                {
                    if (_backImage != null)
                    {
                        if (_backImage.Height > Height && _backImage.Width > Width)
                            areaImg = new Bitmap(Width, Height);
                        else if (_backImage.Width > Width)
                            areaImg = new Bitmap(Width, _backImage.Height);
                        else if (_backImage.Height > Height)
                            areaImg = new Bitmap(_backImage.Width, Height);
                        else
                            areaImg = new Bitmap(_backImage.Width, _backImage.Height);
                    }
                    else
                    {
                        if (_image.Height > Height && _image.Width > Width)
                            areaImg = new Bitmap(Width, Height);
                        else if (_image.Width > Width)
                            areaImg = new Bitmap(Width, _image.Height);
                        else if (_image.Height > Height)
                            areaImg = new Bitmap(_image.Width, Height);
                        else
                            areaImg = new Bitmap(_image.Width, _image.Height);
                    }
                    using (Graphics g = Graphics.FromImage(areaImg))
                    {
                        if (_backImage != null)
                        {
                            g.DrawImage(_backImage,
                                        new Rectangle(AutoScrollPosition.X, AutoScrollPosition.Y, areaImg.Width,
                                                      areaImg.Height - AutoScrollPosition.Y),
                                        new Rectangle(0, 0, areaImg.Width, areaImg.Height - AutoScrollPosition.Y),
                                        GraphicsUnit.Pixel);
                        }
                        else
                        {
                            g.DrawImage(_image,
                                        new Rectangle(AutoScrollPosition.X, AutoScrollPosition.Y, areaImg.Width,
                                                      areaImg.Height),
                                        new Rectangle(0, 0, areaImg.Width, areaImg.Height), GraphicsUnit.Pixel);
                        }
                    }
                }
                return areaImg;
            }
        }

        private Image _backImage;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Image cloneImage = Image;
            if (_backImage != null) cloneImage = _backImage;
            if (cloneImage != null)
            {
                g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
                var point = new Point(((Width - cloneImage.Width)/2), 0);
                switch (ImageAligin)
                {
                    case ImageAligin.Left:
                        point = new Point(0, 0);
                        break;
                    case ImageAligin.Center:
                        if (cloneImage.Height > Height && cloneImage.Width > Width)
                        {
                            point = new Point(0, 0);
                        }
                        break;
                    case ImageAligin.Right:
                        point = new Point(Width - cloneImage.Width, 0);
                        if (cloneImage.Height > Height)
                            point.X -= 0x16;
                        break;
                }
                if (cloneImage.Height > Height)
                {
                    AutoScrollMinSize = new Size(0, cloneImage.Height);
                }
                else
                {
                    point.Y = ((Height - cloneImage.Height)/2);
                }
                g.DrawImage(cloneImage, point);
            }
            else
            {
                TextRenderer.DrawText(
                    g,
                    NullTip,
                    Font,
                    ClientRectangle,
                    BasePublic.Ui.BackColor,
                    Color.Transparent,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        public static TrueBitmapInfo GetThumbnail(Bitmap sourceBitmap, int destWidth, int destHeight)
        {
            Image imgSource = sourceBitmap;
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
            var outBmp = new Bitmap(destWidth, destHeight);

            var outSmallBmp = new Bitmap(sW, sH);

            info.Bitmap = outBmp;
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var rect = new Rectangle((destWidth - sW)/2, (destHeight - sH)/2, sW, sH);

            g.DrawImage(imgSource, rect, 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            g = Graphics.FromImage(outSmallBmp);

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var rect1 = new Rectangle(0, 0, sW, sH);

            g.DrawImage(imgSource, rect1, 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            info.SmallBitmap = outSmallBmp;

            info.ImageRect = rect;
            // 以下代码为保存图片时，设置压缩质量
            var encoderParams = new EncoderParameters();
            var quality = new long[1];
            quality[0] = 100;

            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            try
            {
                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                ImageCodecInfo[] arrayIci = ImageCodecInfo.GetImageEncoders();
                foreach (ImageCodecInfo t in arrayIci)
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
            public Rectangle ImageRect { get; set; }
            public Bitmap Bitmap { get; set; }
            public Bitmap SmallBitmap { get; set; }
        }
    }
}

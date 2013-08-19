using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GraphicLayoutControl.GraphicAbstract.Interface;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;

namespace SongTastePlayer.Controls.GraphicBaseControls
{
    public class GradientButton : GraphicBase
    {
        public event EventHandler OnClick;

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private Font _font = SystemFonts.DefaultFont;
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        private Color _foreColor = Color.Empty;
        public Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        private TextFormatFlags _textAlign = TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis;
        public TextFormatFlags TextAlign
        {
            get { return _textAlign; }
            set { _textAlign = value; }
        }

        private Image _image;

        public Image Image
        {
            get { return _image; }
            set 
            {
                _image = value;
                base.Invalidate();
            }
        }

        private bool _enable = true;
        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                alphas[0] = 0;
                alphas[1] = 0;
                alphas[2] = 0;
                base.Invalidate();
            }
        }

        private bool _isEnter = false;
        private bool _isDown = false;

        private bool _isShowBorder = true;

        public bool IsShowBorder
        {
            get { return _isShowBorder; }
            set
            {
                _isShowBorder = value;
                base.Invalidate();
            }
        }

        private int[] alphas = new int[] { 0, 0, 0 };

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            using (Pen pen = new Pen(Color.FromArgb(alphas[1], 72, 123, 152), 1))
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(ContentRectangle, 2))
                {
                    g.DrawPath(pen, path);
                }
                Pen nBorderPen = new Pen(Color.FromArgb(alphas[1], Color.White), 1);

                if (IsShowBorder)
                    nBorderPen = new Pen(Color.FromArgb(100, Color.White), 1);

                Rectangle nBorder = new Rectangle(
                    ContentRectangle.X + 1,
                    ContentRectangle.Y + 1,
                    ContentRectangle.Width - 2,
                    ContentRectangle.Height - 2);

                g.DrawRectangle(nBorderPen, nBorder);

                if (Enable)
                {
                    LinearGradientBrush linear = new LinearGradientBrush(nBorder, Color.FromArgb(alphas[2], Color.White), Color.FromArgb(0, Color.White), LinearGradientMode.Vertical);
                    if (_isDown)
                        linear = new LinearGradientBrush(nBorder, Color.FromArgb(0, Color.White), Color.FromArgb(alphas[2], Color.White), LinearGradientMode.Vertical);
                    g.FillRectangle(linear, nBorder);
                    linear.Dispose();
                }

                if (Enable)
                {
                    if (Image != null)
                    {
                        g.DrawImage(Image, new Point(
                            base.ContentRectangle.X + ((base.ContentRectangle.Width - Image.Width) / 2),
                            base.ContentRectangle.Y + ((base.ContentRectangle.Height - Image.Height) / 2)));
                    }
                }

                using (GraphicsPath textPath = new GraphicsPath())
                {
                    Size textSize = TextRenderer.MeasureText(Text, Font);

                    int tY = base.ContentRectangle.Y + (base.ContentRectangle.Height / 2 - TextRenderer.MeasureText(Text, Font).Height / 2 - 1);

                    float emSize = Font.Size / 0.74f;
                    textPath.AddString(Text, this.Font.FontFamily, (int)this.Font.Style, emSize, new Point(base.ContentRectangle.X + (base.ContentRectangle.Width / 2) - textSize.Width / 2, tY), StringFormat.GenericDefault);

                    if (Enable)
                    {
                        Blur.DrawOuterGlow2(g, textPath, 2, Color.FromArgb(75, Color.Black), Color.White);
                    }
                    else
                    {
                        Blur.DrawOuterGlow2(g, textPath, 2, Color.FromArgb(75, Color.Black), Color.Gray);
                    }
                }

                //TextRenderer.DrawText(
                //    g,
                //    Text,
                //    Font,
                //    nBorder,
                //    ForeColor,
                //    Color.Transparent,
                //    TextAlign);
            }
        }

        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        System.Threading.Timer timerEnter;
        System.Threading.Timer timerLeave;
        public override void OnMouseRectangleEnter()
        {
            base.OnMouseRectangleEnter();
            _isEnter = true;
            InEffect();
        }
        public override void OnMouseRectangleLeave()
        {
            base.OnMouseRectangleLeave();
            _isEnter = false;
            _isDown = false;
            OutEffect();
        }

        public override void OnMouseRectangleDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseRectangleDown(e);
            _isDown = true;
            base.Invalidate();
        }
        public override void OnMouseRectangleUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseRectangleUp(e);
            if (_isDown)
            {
                if (OnClick != null && Enable != false)
                    OnClick(this, EventArgs.Empty);
            }
            _isDown = false;
            base.Invalidate();
        }
        public override void OnMouseRectangleClick(MouseEventArgs e)
        {
            base.OnMouseRectangleClick(e);

        }

        private void InEffect()
        {
            if (Enable)
            {
                if (timerLeave != null)
                    timerLeave.Dispose();
                alphas[0] = 0;
                alphas[1] = 0;
                alphas[2] = 0;
                timerEnter = new System.Threading.Timer(new TimerCallback(delegate(object obj)
                {
                    alphas[0] += 20;
                    alphas[1] += 15;
                    alphas[2] += 10;
                    if (alphas[2] >= 70)
                    {
                        alphas[0] = 255;
                        alphas[1] = 100;
                        alphas[2] = 70;
                        base.Invalidate();
                        timerEnter.Dispose();
                    }
                    base.Invalidate();
                }), null, 0, 20);
            }
        }
        private void OutEffect()
        {
            if (Enable)
            {
                alphas[0] = 255;
                alphas[1] = 100;
                alphas[2] = 70;
                if (timerEnter != null)
                    timerEnter.Dispose();
                timerLeave = new System.Threading.Timer(new TimerCallback(delegate(object obj)
                {
                    alphas[0] -= 20;
                    alphas[1] -= 15;
                    alphas[2] -= 10;
                    if (alphas[2] <= 0)
                    {
                        alphas[0] = 0;
                        alphas[1] = 0;
                        alphas[2] = 0;
                        base.Invalidate();
                        timerLeave.Dispose();
                    }
                    base.Invalidate();
                }), null, 0, 20);
            }
        }
    }
}


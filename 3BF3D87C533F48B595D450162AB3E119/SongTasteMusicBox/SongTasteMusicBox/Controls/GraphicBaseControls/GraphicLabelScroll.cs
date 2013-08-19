using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using GraphicLayoutControl.GraphicAbstract.Interface;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;

namespace SongTastePlayer.Controls.GraphicBaseControls
{
    public class GraphicLabelScroll : GraphicBase
    {
        private string _text = string.Empty;
        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                BeginScroll();
                base.Invalidate();
            }
        }

        private Color _foreColor = Color.Black;
        public virtual Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        private Color _backColor = Color.Transparent;
        public virtual Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        private Font _font = new Font(new FontFamily("微软雅黑"), 10.5f);
        public virtual Font Font { get; set; }

        private GraphicTextAlign _textAlign = GraphicTextAlign.LeftTop;
        public GraphicTextAlign TextAlign
        {
            get { return _textAlign; }
            set { _textAlign = value; }
        }

        private int _offsetX = 0;

        public int OffsetX
        {
            get { return _offsetX; }
            set
            {
                _offsetX = value;
            }
        }

        Timer timer;

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            //if (AutoSize)
            //{
            //    Size size = TextRenderer.MeasureText(Text, Font);
            //    ContentRectangle = new Rectangle(
            //        ContentRectangle.X,
            //        ContentRectangle.Y,
            //        size.Width,
            //        size.Height);
            //}

            if (BackColor != Color.Empty)
                g.FillRectangle(
                    new SolidBrush(BackColor),
                    ContentRectangle);

            using (GraphicsPath textPath = new GraphicsPath())
            {
                Size textSize = System.Windows.Forms.TextRenderer.MeasureText(Text, Font);

                int tY = base.ContentRectangle.Y;
                int tX = base.ContentRectangle.X;
                switch (TextAlign)
                {
                    case GraphicTextAlign.MiddleLeft:
                        tX = base.ContentRectangle.X;
                        tY = base.ContentRectangle.Y + (base.ContentRectangle.Height / 2 - System.Windows.Forms.TextRenderer.MeasureText(Text, Font).Height / 2 - 1);
                        break;
                    case GraphicTextAlign.MiddleCenter:
                        tX = base.ContentRectangle.X + (base.ContentRectangle.Width / 2) - (textSize.Width / 2);
                        tY = base.ContentRectangle.Y + (base.ContentRectangle.Height / 2 - textSize.Height / 2);
                        break;
                    case GraphicTextAlign.MiddleRight:
                        tX = base.ContentRectangle.X + base.ContentRectangle.Width - textSize.Width;
                        tY = base.ContentRectangle.Y + (base.ContentRectangle.Height / 2 - textSize.Height / 2);
                        break;
                    case GraphicTextAlign.MiddleBottom:
                        tX = base.ContentRectangle.X + (base.ContentRectangle.Width / 2) - (textSize.Width / 2);
                        tY = base.ContentRectangle.Y + base.ContentRectangle.Height - textSize.Height;
                        break;
                    case GraphicTextAlign.MiddleTop:
                        tX = base.ContentRectangle.X + textSize.Height;
                        tY = base.ContentRectangle.Y + base.ContentRectangle.Height - textSize.Height;
                        break;
                    case GraphicTextAlign.LeftTop:
                        tX = base.ContentRectangle.X;
                        tY = base.ContentRectangle.Y;
                        break;
                    case GraphicTextAlign.LeftBottom:
                        tX = base.ContentRectangle.X;
                        tY = base.ContentRectangle.Y + base.ContentRectangle.Height - textSize.Height;
                        break;
                    case GraphicTextAlign.RightTop:
                        tX = base.ContentRectangle.X + base.ContentRectangle.Width - textSize.Width;
                        tY = base.ContentRectangle.X;
                        break;
                    case GraphicTextAlign.RightBottom:
                        tX = base.ContentRectangle.X + base.ContentRectangle.Width - textSize.Width;
                        tY = base.ContentRectangle.Y + base.ContentRectangle.Height - textSize.Height;
                        break;
                }
                float emSize = Font.Size / 0.74f;
                textPath.AddString(
                    Text,
                    this.Font.FontFamily,
                    (int)this.Font.Style,
                    emSize,
                    new Point(OffsetX, tY - base.ContentRectangle.Y),
                    StringFormat.GenericDefault);

                Bitmap bit = Blur.DrawOuterGlowReturnBitmap(base.ContentRectangle, textPath, 2, Color.FromArgb(75, Color.Black), ForeColor);
                g.DrawImage(bit, new Point(base.ContentRectangle.X, base.ContentRectangle.Y));
            }
            //g.DrawRectangle(Pens.Red, base.ContentRectangle);
        }

        public void BeginScroll()
        {
            StopScroll();

            Size size = System.Windows.Forms.TextRenderer.MeasureText(Text, Font);
            if (size.Width > base.ContentRectangle.Width)
            {
                timer = new Timer(new TimerCallback(delegate(object obj)
                {
                    OffsetX -= 5;
                    if (-OffsetX > -(base.ContentRectangle.X - size.Width))
                    {
                        OffsetX = base.ContentRectangle.X + base.ContentRectangle.Width;
                    }
                    base.Invalidate(base.ContentRectangle);
                }), null, 0, 80);
            }
        }

        public void StopScroll()
        {
            if (timer != null)
            {
                OffsetX = 0;
                timer.Dispose();
                timer = null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using GraphicLayoutControl.GraphicAbstract.Interface;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;

namespace SongTastePlayer.Controls.GraphicBaseControls
{
    public enum GraphicTextAlign
    {
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        MiddleBottom,
        MiddleTop,

        LeftTop,
        LeftBottom,

        RightTop,
        RightBottom
    }

    public class GraphicLabelPlus : GraphicBase
    {
        private string _text = string.Empty;
        public virtual string Text
        {
            get { return _text; }
            set
            {
                _text = value;
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

        private bool _autoSize = true;
        public virtual bool AutoSize
        {
            get { return _autoSize; }
            set { _autoSize = value; }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            if (AutoSize)
            {
                Size size = TextRenderer.MeasureText(Text, Font);
                ContentRectangle = new Rectangle(
                    ContentRectangle.X,
                    ContentRectangle.Y,
                    size.Width,
                    size.Height);
            }

            if (BackColor != Color.Empty)
                g.FillRectangle(
                    new SolidBrush(BackColor),
                    ContentRectangle);

            using (GraphicsPath textPath = new GraphicsPath())
            {
                Size textSize = TextRenderer.MeasureText(Text, Font);

                int tY = base.ContentRectangle.Y;
                int tX = base.ContentRectangle.X;
                if (!AutoSize)
                {
                    switch (TextAlign)
                    {
                        case GraphicTextAlign.MiddleLeft:
                            tX = base.ContentRectangle.X;
                            tY = base.ContentRectangle.Y + (base.ContentRectangle.Height / 2 - TextRenderer.MeasureText(Text, Font).Height / 2 - 1);
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
                }
                float emSize = Font.Size / 0.74f;
                textPath.AddString(
                    Text,
                    this.Font.FontFamily,
                    (int)this.Font.Style,
                    emSize,
                    new Point(tX, tY),
                    StringFormat.GenericDefault);

                Blur.DrawOuterGlow2(g, textPath, 2, Color.FromArgb(75, Color.Black), Color.White);
            }
        }
    }
}

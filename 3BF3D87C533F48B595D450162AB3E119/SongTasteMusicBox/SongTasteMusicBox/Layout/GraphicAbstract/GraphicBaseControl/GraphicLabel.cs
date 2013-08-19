using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GraphicLayoutControl.GraphicAbstract.Interface;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;

namespace GraphicLayoutControl.GraphicAbstract.GraphicBaseControl
{
    public class GraphicLabel : GraphicBase
    {
        private string _text = string.Empty;
        public virtual string Text
        {
            get { return _text; }
            set { _text = value; }
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

        private TextFormatFlags _textAlign = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
        public TextFormatFlags TextAlign
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

            TextRenderer.DrawText(
                g,
                Text,
                Font,
                ContentRectangle,
                ForeColor,
                BackColor,
                TextAlign);
        }
    }
}

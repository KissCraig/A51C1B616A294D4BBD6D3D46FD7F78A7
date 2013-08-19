using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;

namespace SongTastePlayer.Controls.GraphicBaseControls
{
    public class GradientRect : GraphicBase
    {
        private Color _baseColor = Color.Gray;

        public Color BaseColor
        {
            get { return _baseColor; }
            set { _baseColor = value; }
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            base.Draw(g);
            //Rectangle left = new Rectangle(base.ContentRectangle.X, base.ContentRectangle.Y, 3, base.ContentRectangle.Height);
            //Rectangle top = new Rectangle(base.ContentRectangle.X, base.ContentRectangle.Y, base.ContentRectangle.Width, 3);
            //Rectangle right = new Rectangle(base.ContentRectangle.X + base.ContentRectangle.Width, base.ContentRectangle.Y, 3, base.ContentRectangle.Height);
            //Rectangle bottom = new Rectangle(base.ContentRectangle.X, base.ContentRectangle.Y + base.ContentRectangle.Height, base.ContentRectangle.Width, 3);

            //Color q = Color.FromArgb(50, Color.Black);
            //Color b = Color.FromArgb(0, Color.Black);

            Color drawColor = Color.FromArgb(BaseColor.A, BaseColor.R + 10, BaseColor.G + 10, BaseColor.B + 10);

            Rectangle rect = new Rectangle(ContentRectangle.X - 1, ContentRectangle.Y - 1, ContentRectangle.Width + 1, ContentRectangle.Height + 1);

            g.DrawRectangle(new Pen(Color.FromArgb(150, drawColor), 1), rect);
            rect = new Rectangle(rect.X, rect.Y, rect.Width + 1, rect.Height + 1);
            g.DrawRectangle(new Pen(Color.FromArgb(100, drawColor), 1), rect);
            rect = new Rectangle(rect.X, rect.Y, rect.Width + 1, rect.Height + 1);
            g.DrawRectangle(new Pen(Color.FromArgb(50, drawColor), 1), rect);
        }
    }
}

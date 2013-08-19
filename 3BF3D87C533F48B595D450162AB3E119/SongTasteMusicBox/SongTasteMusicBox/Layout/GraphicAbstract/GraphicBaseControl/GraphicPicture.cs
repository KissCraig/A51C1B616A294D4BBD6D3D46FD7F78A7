using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GraphicLayoutControl.GraphicAbstract.Interface;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;

namespace GraphicLayoutControl.GraphicAbstract.GraphicBaseControl
{
    public class GraphicPicture : GraphicBase
    {
        public virtual ImageLayout ImageLayout { get; set; }

        private Image _image = null;
        public virtual Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public override void Draw(Graphics g)
        {
            if (Image != null)
            {
                switch (ImageLayout)
                {
                    case ImageLayout.None:
                        g.DrawImage(Image, new Point(ContentRectangle.X, ContentRectangle.Y));
                        break;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GraphicLayoutControl.GraphicAbstract.Interface.Class
{
    [Serializable]
    public class GraphicBase : IGraphicBase
    {
        internal bool _isLeave = false;
        internal Control _baseControl = null;

        private Rectangle _contentRectangle = Rectangle.Empty;
        public virtual Rectangle ContentRectangle
        {
            get { return _contentRectangle; }
            set { _contentRectangle = value; }
        }

        public virtual void Draw(Graphics g) { }
        public virtual void OnMouseRectangleMove(System.Windows.Forms.MouseEventArgs e) { }
        public virtual void OnMouseRectangleDown(System.Windows.Forms.MouseEventArgs e) { }
        public virtual void OnMouseRectangleUp(System.Windows.Forms.MouseEventArgs e) { }
        public virtual void OnMouseRectangleLeave() { }
        public virtual void OnMouseRectangleEnter() { }
        public virtual void OnMouseHover() { }
        public virtual void OnMouseRectangleDoubleClick(MouseEventArgs e) { }
        public virtual void OnMouseRectangleClick(MouseEventArgs e) { }


        public virtual void OnMouseBaseMove(MouseEventArgs e)
        {

        }
        public virtual void OnMouseBaseDown(MouseEventArgs e)
        {

        }
        public virtual void OnMouseBaseUp(MouseEventArgs e)
        {

        }
        public virtual void OnMouseBaseDoubleClick(MouseEventArgs e)
        {

        }
        public virtual void OnMouseBaseClick(MouseEventArgs e)
        {

        }

        public virtual void Invalidate() 
        {
            if (_baseControl != null)
            {
                _baseControl.Invalidate();
            }
        }
        public virtual void Invalidate(Rectangle rc) 
        {
            if (_baseControl != null)
            {
                _baseControl.Invalidate(rc);
            }
        }

        public Graphics CreateGraphics()
        {
            if (_baseControl != null)
            {
                return _baseControl.CreateGraphics();
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GraphicLayoutControl.GraphicAbstract.Interface
{
    public interface IGraphicBase
    {
        void Draw(Graphics g);
        void OnMouseRectangleMove(MouseEventArgs e);
        void OnMouseRectangleDown(MouseEventArgs e);
        void OnMouseRectangleUp(MouseEventArgs e);
        void OnMouseRectangleLeave();
        void OnMouseRectangleEnter();
        void OnMouseRectangleDoubleClick(MouseEventArgs e);
        void OnMouseRectangleClick(MouseEventArgs e);

        void OnMouseBaseMove(MouseEventArgs e);
        void OnMouseBaseDown(MouseEventArgs e);
        void OnMouseBaseUp(MouseEventArgs e);
        void OnMouseBaseDoubleClick(MouseEventArgs e);
        void OnMouseBaseClick(MouseEventArgs e);

        void Invalidate();
        void Invalidate(Rectangle rc);
        Graphics CreateGraphics();
    }
}

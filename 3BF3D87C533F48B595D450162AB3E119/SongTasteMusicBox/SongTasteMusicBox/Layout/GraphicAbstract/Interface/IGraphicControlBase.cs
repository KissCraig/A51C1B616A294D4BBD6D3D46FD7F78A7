using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GraphicLayoutControl.GraphicAbstract.Interface
{
    public interface IGraphicControlBase
    {
        Control Control { get; }
        void Draw(Graphics g);
        void DrawBackground(Graphics g);
    }
}

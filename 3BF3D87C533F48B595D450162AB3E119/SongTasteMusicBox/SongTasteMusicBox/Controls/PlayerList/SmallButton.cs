using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SongTastePlayer.Controls
{
    public enum DownState
    {
        Normal = 0,
        Hover = 16,
        Passed = 32
    }
    public class SmallButton
    {
        public Rectangle ButtonRect { get; set; }

        public DownState DownState { get; set; }

        public int Number { get; set; }

        public Font Font { get; set; }

        public bool IsDown { get; set; }

        public void Draw(Graphics g)
        {
            TextRenderer.DrawText(
                g,
                Number.ToString(),
                Font,
                ButtonRect,
               Color.FromArgb(185, 185, 185),
                Color.Transparent,
                TextFormatFlags.VerticalCenter | 
                TextFormatFlags.HorizontalCenter);

            switch (DownState)
            {
                case DownState.Hover:
                    g.DrawRectangle(new Pen(Color.FromArgb(100, Color.Black), 1), ButtonRect);
                    break;
            }
        }
    }
}

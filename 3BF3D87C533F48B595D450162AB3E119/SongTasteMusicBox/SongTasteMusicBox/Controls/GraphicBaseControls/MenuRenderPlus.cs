using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SongTastePlayer.Controls.GraphicBaseControls
{
    public class MenuRenderPlus : ToolStripRenderer
    {
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBorder(e);
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            using (Pen pen = new Pen(Color.FromArgb(113, 128, 145)))
            {
                g.DrawRectangle(pen, rect);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);

            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

            if (e.Item.Selected)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(92, 187, 254)), rect);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            //base.OnRenderItemText(e);
            Color color = Color.FromArgb(58, 67, 74);
            if(e.Item.Selected)
                color = Color.White;

            TextRenderer.DrawText(
                e.Graphics,
                e.Text,
                new Font("微软雅黑", 9),
                e.TextRectangle,
                color,
                Color.Transparent,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }
    }
}

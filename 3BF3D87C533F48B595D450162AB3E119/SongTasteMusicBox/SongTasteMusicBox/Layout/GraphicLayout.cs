using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GraphicLayoutControl.GraphicAbstract;
using GraphicLayoutControl.GraphicAbstract.Interface;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;

namespace GraphicLayoutControl
{
    public class GraphicLayout : Panel
    {
        public List<GraphicBase> GraphicControls = new List<GraphicBase>();

        public GraphicLayout()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (GraphicBase item in GraphicControls)
            {
                item.Draw(e.Graphics);
                item._baseControl = this;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            base.Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object se)
            {
                foreach (GraphicBase item in GraphicControls)
                {
                    item.OnMouseBaseClick(e);
                    if (item.ContentRectangle.Contains(e.Location))
                    {
                        item.OnMouseRectangleClick(e);
                    }
                }
            }), null);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object se)
            {
                foreach (GraphicBase item in GraphicControls)
                {
                    item.OnMouseBaseDoubleClick(e);
                    if (item.ContentRectangle.Contains(e.Location))
                    {
                        item.OnMouseRectangleDoubleClick(e);
                    }
                }
            }), null);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object se)
            {
                foreach (GraphicBase item in GraphicControls)
                {
                    item.OnMouseBaseDown(e);
                    if (item.ContentRectangle.Contains(e.Location))
                    {
                        item.OnMouseRectangleDown(e);
                    }
                }
            }), null);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object se)
            {
                foreach (GraphicBase item in GraphicControls)
                {
                    item.OnMouseBaseMove(e);
                    if (item.ContentRectangle.Contains(e.Location))
                    {
                        if (!item._isLeave)
                        {
                            item.OnMouseRectangleEnter();
                            item._isLeave = true;
                        }
                        item.OnMouseRectangleMove(e);
                    }
                    else
                    {
                        if (item._isLeave)
                        {
                            item.OnMouseRectangleLeave();
                            item._isLeave = false;
                        }
                    }
                }
            }), e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object se)
            {
                foreach (GraphicBase item in GraphicControls)
                {
                    item.OnMouseBaseUp(e);
                    if (item.ContentRectangle.Contains(e.Location))
                    {
                        item.OnMouseRectangleUp(e);
                    }
                }
            }), null);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object se)
            {
                foreach (GraphicBase item in GraphicControls)
                {
                    item._isLeave = false;
                    base.Invalidate();
                }
            }), null);
        }
    }
}

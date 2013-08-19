using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using VodWatching.Properties;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;
using System.Net;

namespace VodWatching
{
    public partial class ContentPanel : UserControl
    {
        private List<VMovieData> _items = new List<VMovieData>();

        internal List<VMovieData> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public ContentPanel()
        {
            InitializeComponent();
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw, true);
        }

        int offsetX = 0;
        int offsetY = 0;

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            offsetX = 0;
            offsetY = 0;
            Graphics g = e.Graphics;
            //g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            this.AutoScrollMinSize = new Size(0, ((this.Items.Count / (this.Width / 128)) + 2) * 80);
            if (Items.Count == 0)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    "正在处理数据......",
                    PublicInterFace.Font,
                    this.ClientRectangle,
                    Parent.ForeColor,
                    this.BackColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    VMovieData item = Items[i];
                    Rectangle rect = GetRectangle();
                    if (item.IsDown)
                    {
                        //rect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
                        item.Leavel();
                    }

                    item.ClientRect = rect;

                    g.DrawImage(item.BackImage, rect);

                    g.SmoothingMode = SmoothingMode.HighSpeed;
                    g.InterpolationMode = InterpolationMode.Low;

                    //g.DrawRectangle(new Pen(Parent.ForeColor), rect);

                    using (SolidBrush brushes = new SolidBrush(
                        item.IsHover ? Color.FromArgb(item.alpha, Color.Black) : Color.Transparent))
                    {
                        g.FillRectangle(brushes, rect);
                    }

                    using (SolidBrush brushes = new SolidBrush(Color.FromArgb(181, 230, 29)))
                    {
                        g.FillRectangle(brushes, new Rectangle(rect.X, rect.Bottom - 18, rect.Width, 20));
                    }

                    if (item.Url != null)
                    {
                        TextRenderer.DrawText(
                            g,
                            DecodeUrl(item.Url),
                            new Font(PublicInterFace.Font.FontFamily, 8f),
                            rect,
                            Color.Black,
                            Color.Transparent,
                            TextFormatFlags.Left | TextFormatFlags.Bottom | TextFormatFlags.WordEllipsis);
                    }
                }
            }
        }

        VMovieData selectData = null;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            for (int i = 0; i < Items.Count; i++)
            {
                VMovieData item = Items[i];
                if (item.ClientRect.Contains(e.Location))
                {
                    item.IsHover = true;
                    selectData = item;
                    this.Cursor = Cursors.Hand;
                    item.Enter();
                    //base.Invalidate(item.ClientRect);
                }
                else if (selectData != null && !selectData.ClientRect.Contains(e.Location))
                {
                    selectData.IsHover = false;
                    selectData.IsDown = false;
                    this.Cursor = Cursors.Default;
                    selectData.Leavel();
                }
                else
                {
                    item.IsHover = false;
                    item.Leavel();
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (selectData != null && selectData.ClientRect.Contains(e.Location))
                {
                    selectData.IsDown = true;
                    base.Invalidate();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (selectData != null && selectData.IsDown)
            {
                selectData.IsDown = false;
                base.Invalidate();
                selectData.OnClick();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].IsHover = false;
            }
            this.Cursor = Cursors.Default;
            base.Invalidate();
        }

        public string DecodeUrl(string url)
        {
            string tempurl = url.ToLower();
            try
            {
                if (tempurl.StartsWith("ed2k://|file|"))
                {
                    return Regex.Match(tempurl, "(?<=ed2k://\\|file\\|).*?(?=\\.|\\|)").Value;
                }
                else if (tempurl.StartsWith("ftp://"))
                {
                    int index = tempurl.LastIndexOf(']') != -1 ? tempurl.LastIndexOf(']') + 1 : tempurl.LastIndexOf('/') + 1;
                    return tempurl.Substring(index, tempurl.Length - index);
                }
                else if (tempurl.StartsWith("aa"))
                {
                    tempurl = tempurl.Substring(2, tempurl.Length - 2);
                    tempurl = tempurl.Substring(0, tempurl.Length - 2);
                    return DecodeUrl(tempurl);
                }
                else if (tempurl.StartsWith("thunder://"))
                {
                    return DecodeUrl(Encoding.UTF8.GetString(Convert.FromBase64String(tempurl.Substring(10))));
                }
            }
            catch (Exception)
            {
                return "未命名";
            }
            return "未命名";
        }

        public Rectangle GetRectangle()
        {
            Rectangle rect = new Rectangle(offsetX + 5, this.AutoScrollPosition.Y + offsetY + 5, 128, 80);
            offsetX += rect.Width + 5;
            if (offsetX + rect.Width + 5 >= this.Width)
            {
                offsetY += rect.Height + 5;
                offsetX = 0;
            }
            return rect;
        }
    }
    class VMovieData
    {
        public event EventHandler Click;

        public void OnClick()
        {
            if (Click != null)
                Click(this, EventArgs.Empty);
        }

        public string Url { get; set; }
        public string Gcid { get; set; }

        public Rectangle ClientRect { get; set; }
        public Image BackImage = Resources.no_img;
        public bool IsHover { get; set; }
        public bool IsDown { get; set; }

        private System.Timers.Timer enterTimer = new System.Timers.Timer(10);
        private System.Timers.Timer leavelTimer = new System.Timers.Timer(10);

        private Control baseParent = null;

        public VMovieData(Control baseParent)
        {
            this.baseParent = baseParent;
            enterTimer.Elapsed += enterTimer_Elapsed;
            leavelTimer.Elapsed += leavelTimer_Elapsed;
        }

        void leavelTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            alpha -= 15;
            if (alpha <= 0)
            {
                alpha = 0;
                leavelTimer.Stop();
            }
            baseParent.Invalidate();
        }

        void enterTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            alpha += 15;
            if (alpha >= 120)
            {
                alpha = 120;
                enterTimer.Stop();
            }
            baseParent.Invalidate();
        }

        internal int alpha = 0;

        public void Enter()
        {
            leavelTimer.Stop();
            enterTimer.Start();
        }

        public void Leavel()
        {
            enterTimer.Stop();
            leavelTimer.Start();
        }

    }
}

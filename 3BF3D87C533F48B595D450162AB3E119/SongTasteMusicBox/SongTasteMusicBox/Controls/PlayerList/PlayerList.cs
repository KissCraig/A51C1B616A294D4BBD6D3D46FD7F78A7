using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using SongTasteMusicBox.Properties;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace SongTastePlayer.Controls
{

    public delegate void ReturnEntityHandler(object sender, MusicEntity entity);
    public delegate void NumberClickHandler(object sender, int number);
    public partial class PlayerList : UserControl
    {
        private bool _enable = false;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        public event ReturnEntityHandler OnItemDoubleClick;
        public event NumberClickHandler OnNumber;

        Rectangle topInfoRect = Rectangle.Empty;

        public List<MusicEntity> Items = new List<MusicEntity>();

        private int _playIndex = -1;

        public int PlayIndex
        {
            get { return _playIndex; }
            set { _playIndex = value; }
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public PlayerList()
        {
            InitializeComponent();
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);

            for (int i = 1; i <= 10; i++)
            {
                _buttons.Add(new SmallButton()
                {
                    Font = new Font("微软雅黑", 9),
                    Number = i
                });
            }
        }

        GifPlay play;
        System.Threading.Timer addPointStr;

        StringBuilder pointStrs = new StringBuilder();
        Rectangle ellipseBarRect;
        Rectangle gBarRect;

        MusicEntity _hoverEntity;
        MusicEntity _downEntity;

        bool _isEllipeBarHover = false;
        bool _isEllipeBarDown = false;
        bool _isInit = true;

        int _itemHeight = 30;
        int offsetY = 0;

        int barMinSize = 20;

        List<SmallButton> _buttons = new List<SmallButton>();
        SmallButton _hoverButton = new SmallButton();


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            #region 区域声明以及提前判断

            topInfoRect = new Rectangle(0, 1, this.Width, 25);
            if (_isInit)
            {
                ellipseBarRect = new Rectangle(this.Width - 11, topInfoRect.Height + 3, 10, 20);
                _isInit = false;
            }
            else
            {
                ellipseBarRect = new Rectangle(this.Width - 11, ellipseBarRect.Y, ellipseBarRect.Width, ellipseBarRect.Height);
            }

            gBarRect = new Rectangle(this.Width - 11, topInfoRect.Height + 3, 10, this.Height - topInfoRect.Height - 4);

            g.Clear(Color.FromArgb(244, 249, 239));

            if (Items.Count > 0)
            {
                int height = (gBarRect.Height / _itemHeight) * gBarRect.Height / Items.Count;

                int y = ellipseBarRect.Y - (ellipseBarRect.Height - height);

                if (ellipseBarRect.Y < y)
                    y = ellipseBarRect.Y;

                if (y < topInfoRect.Height + 3)
                    y = topInfoRect.Height + 3;

                ellipseBarRect = new Rectangle(
                    ellipseBarRect.X,
                    y,
                    ellipseBarRect.Width,
                    height);
            }

            if (ellipseBarRect.Height < barMinSize)
            {
                ellipseBarRect = new Rectangle(
                    ellipseBarRect.X, ellipseBarRect.Y, ellipseBarRect.Width, barMinSize);
            }
            #endregion


            if (Items.Count > 0)
            {
                //offsetY = 
                //    (ellipseBarRect.Y - topInfoRect.Height - 3) * 
                //    (Items.Count * _itemHeight) / gBarRect.Height;
                int gh = (gBarRect.Height / _itemHeight) * gBarRect.Height / Items.Count;
                int elY = (ellipseBarRect.Y - topInfoRect.Height - 3);
                offsetY = (int)(elY * ((double)(Items.Count * _itemHeight) / gBarRect.Height));

                #region 绘制列表项
                Font itemFont = new Font("微软雅黑", 9);
                int maxLength = 100;

                int subStart = 0;
                int subEnd = 0;
                if (offsetY > 0)
                    subStart = (offsetY / _itemHeight) - 1;

                if (subStart < 0)
                    subStart = 0;

                subEnd = ((this.ClientRectangle.Height - topInfoRect.Height - 3 + offsetY) / _itemHeight) + 1;
                if (subEnd > Items.Count)
                    subEnd = Items.Count;

                for (int i = subStart; i < subEnd; i++)
                {
                    if ((i * _itemHeight) - offsetY + topInfoRect.Height > topInfoRect.Height - _itemHeight)
                    {
                        Rectangle itemRect = new Rectangle(1, (i * _itemHeight) - offsetY + topInfoRect.Height, this.Width - 2 - ellipseBarRect.Width, _itemHeight);
                        Rectangle musicName = new Rectangle(itemRect.X, itemRect.Y, itemRect.Width - maxLength, _itemHeight);
                        Rectangle sharePerson = new Rectangle(itemRect.X + (itemRect.Width - maxLength), itemRect.Y, maxLength, _itemHeight);

                        Items[i].Rect = itemRect;
                        if (_downEntity == Items[i])
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(205, 227, 180)), itemRect);
                        }

                        if (_hoverEntity == Items[i]
                            && _hoverEntity != _downEntity)
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(222, 237, 206)), itemRect);
                        }

                        TextRenderer.DrawText(
                            g,
                            Items[i].MusicName,
                            itemFont,
                            musicName,
                            Color.FromArgb(85, 85, 85),
                            Color.Transparent,
                            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                        TextRenderer.DrawText(
                            g,
                            Items[i].SharePeople,
                            new Font("微软雅黑", 9),
                            sharePerson,
                            Color.FromArgb(85, 85, 85),
                            Color.Transparent,
                            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                    }
                }
                #endregion

                #region 绘制滚动条
                Pen pens = new Pen(Color.FromArgb(170, 174, 167), ellipseBarRect.Width);
                pens.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
                if (_isEllipeBarHover)
                {
                    pens.Color = Color.FromArgb(122, 124, 119);
                }
                else if (_isEllipeBarDown)
                {
                    pens.Color = Color.FromArgb(73, 74, 71);
                }

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                g.DrawLine(pens, ellipseBarRect.X + 5, ellipseBarRect.Y + 5, ellipseBarRect.X + 5, ellipseBarRect.Y + ellipseBarRect.Height - 5);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                //g.DrawRectangle(Pens.Red, ellipseBarRect);
                //g.DrawRectangle(Pens.Red, gBarRect);
                #endregion

                #region 绘制虚线以及多少首歌
                g.FillRectangle(new SolidBrush(Color.FromArgb(244, 249, 239)), topInfoRect);
                using (Pen dashedPen = new Pen(Color.FromArgb(196, 207, 182)))
                {
                    dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawLine(dashedPen, 0, 26, this.Width, 26);
                }
                TextRenderer.DrawText(
                    g,
                    string.Format("共{0}首歌曲", Items.Count),
                    new Font("微软雅黑", 9),
                    topInfoRect,
                    Color.FromArgb(185, 185, 185),
                    Color.Transparent,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                #endregion

                #region 在显示歌曲总数区域绘制选择页数
                //g.DrawRectangle(Pens.Red, topInfoRect);
                for (int i = 0; i < _buttons.Count; i++)
                {
                    SmallButton item = _buttons[i];
                    if (_hoverButton != item)
                    {
                        item.DownState = DownState.Normal;
                    }
                    item.ButtonRect = new Rectangle(
                        i * 20 + (topInfoRect.Width - (20 * _buttons.Count)), topInfoRect.Top + (topInfoRect.Height / 2) - 10, 18, 18);

                    item.Draw(g);
                }
                #endregion
            }

            #region 动画效果
            else
            {

                Size size = TextRenderer.MeasureText("列表正在加载", this.Font);

                if (play == null)
                {
                    play = new GifPlay(Resources.ScanLoader);
                    play.Play();
                    play.OnFrameChanged += play_OnFrameChanged;
                    addPointStr = new System.Threading.Timer(new TimerCallback(delegate(object obj)
                        {
                            if (pointStrs.Length >= 6)
                                pointStrs.Remove(0, 6);
                            else
                                pointStrs.Append(".");

                            if (Items.Count > 0)
                            {
                                if (play != null)
                                {
                                    play.Stop();
                                    play = null;
                                }
                                addPointStr.Dispose();
                            }
                        }), null, 0, 200);

                    g.DrawImage(play.Image, new Rectangle(this.Width / 2 - size.Width / 2 - 16, this.Height / 2 - 8, 16, 16));
                    base.Invalidate();
                }
                else
                {
                    lock (play.Image)
                    {
                        TextRenderer.DrawText(
                            g,
                            "列表正在加载" + pointStrs.ToString(),
                            this.Font,
                            new Rectangle(this.Width / 2 - size.Width / 2, 0, this.Width, this.Height),
                            Color.Black,
                            Color.Transparent,
                            TextFormatFlags.EndEllipsis |
                            TextFormatFlags.VerticalCenter);
                        g.DrawImage(play.Image, new Rectangle(this.Width / 2 - size.Width / 2 - 16, this.Height / 2 - size.Height / 2 - 1, 16, 16));
                    }
                }
            }
            #endregion
        }

        public void Add(MusicEntity entity)
        {
            Items.Add(entity);

            int height = (gBarRect.Height / _itemHeight) * gBarRect.Height / Items.Count;

            ellipseBarRect = new Rectangle(
                ellipseBarRect.X, ellipseBarRect.Y, ellipseBarRect.Width,
                height);

            base.Invalidate();
        }

        public void Clear()
        {
            ellipseBarRect.Y = topInfoRect.Height + 3;
            Items.Clear();
            base.Invalidate();
        }

        private void play_OnFrameChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }
        int jY = 0;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!Enable)
            {
                if (ellipseBarRect.Contains(e.Location)
                    && !_isEllipeBarDown)
                {
                    _isEllipeBarHover = true;
                }
                else if (!ellipseBarRect.Contains(e.Location))
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object obj)
                        {
                            if (topInfoRect.Contains(e.Location))
                            {
                                foreach (SmallButton item in _buttons)
                                {
                                    if (item.ButtonRect.Contains(e.Location))
                                    {
                                        this.Invoke(new MethodInvoker(delegate()
                                        {
                                            this.Cursor = Cursors.Hand;
                                        }));
                                        item.DownState = DownState.Hover;
                                        _hoverButton = item;
                                        base.Invalidate(item.ButtonRect);
                                        return;
                                    }
                                    else if (_hoverButton != null
                                        && !_hoverButton.ButtonRect.Contains(e.Location))
                                    {
                                        _hoverButton = null;
                                        this.Invoke(new MethodInvoker(delegate()
                                        {
                                            this.Cursor = Cursors.Default;
                                        }));
                                    }
                                }
                            }
                            else
                            {
                                this.Invoke(new MethodInvoker(delegate()
                                {
                                    this.Cursor = Cursors.Default;
                                }));
                                _hoverButton = null;
                                for (int i = 0; i < Items.Count; i++)
                                {
                                    MusicEntity item = Items[i];
                                    if (item.Rect.Contains(e.Location))
                                    {
                                        _hoverEntity = item;
                                        base.Invalidate(item.Rect);
                                        return;
                                    }
                                }
                            }
                        }));
                }
                else
                {
                    _isEllipeBarHover = false;
                }
                if (_isEllipeBarDown)
                {
                    int y = e.Y - jY;

                    if (y < topInfoRect.Y + topInfoRect.Height + 2)
                        y = topInfoRect.Y + topInfoRect.Height + 2;

                    if (y > this.Height - ellipseBarRect.Height - 1)
                        y = this.Height - ellipseBarRect.Height - 1;

                    ellipseBarRect = new Rectangle(
                        ellipseBarRect.X, y, ellipseBarRect.Width, ellipseBarRect.Height);
                }
                base.Invalidate();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (!Enable)
            {
                if (OnItemDoubleClick != null)
                {
                    PlayIndex = Items.FindIndex(ui => ui == _hoverEntity);
                    OnItemDoubleClick(this, _hoverEntity);
                }
            }
        }

        public MusicEntity GetMusicEntity(int index)
        {
            return Items[index];
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int y = ellipseBarRect.Y;
            int z = SystemInformation.MouseWheelScrollDelta;

            //单步行走行数
            int c = (gBarRect.Height - ellipseBarRect.Height);
            int gLength = c == 0 ? 1 : c / Items.Count == 0 ? 1 : Items.Count;

            y -= e.Delta / z * gLength;

            if (y < topInfoRect.Y + topInfoRect.Height + 2)
                y = topInfoRect.Y + topInfoRect.Height + 2;

            if (y > this.Height - ellipseBarRect.Height - 1)
                y = this.Height - ellipseBarRect.Height - 1;

            ellipseBarRect = new Rectangle(
                ellipseBarRect.X, y, ellipseBarRect.Width, ellipseBarRect.Height);
            base.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (ellipseBarRect.Contains(e.Location))
            {
                _isEllipeBarHover = false;
                _isEllipeBarDown = true;
                jY = e.Y - ellipseBarRect.Y;
            }
            else if (_hoverEntity != null && _hoverEntity.Rect.Contains(e.Location))
            {
                _downEntity = _hoverEntity;
            }
            else
            {
                _isEllipeBarDown = false;
            }
            if (_hoverButton != null && _hoverButton.ButtonRect.Contains(e.Location))
            {
                _hoverButton.IsDown = true;
            }
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isEllipeBarDown = false;
            _isEllipeBarHover = false;

            _hoverEntity = null;
            _hoverButton = null;
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isEllipeBarDown = false;
            _isEllipeBarHover = false;

            if (_hoverButton != null && _hoverButton.ButtonRect.Contains(e.Location) && _hoverButton.IsDown)
            {
                if (!Enable)
                {
                    if (OnNumber != null)
                    {
                        OnNumber(this, _hoverButton.Number);
                    }
                }
            }

            base.Invalidate();
        }
    }
}

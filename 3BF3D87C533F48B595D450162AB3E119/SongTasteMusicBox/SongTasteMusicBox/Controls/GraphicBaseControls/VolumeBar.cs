using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;
using SongTasteMusicBox.Properties;

namespace SongTastePlayer.Controls.GraphicBaseControls
{
    public class VolumeBar : GraphicBase
    {
        public event EventHandler OnChangeVolume;
        public event EventHandler SetOk;
        private float _volume = 0;

        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                base.Invalidate();
            }
        }

        Rectangle lineRect;
        RectangleF round;
        bool _isHover = false;
        bool _isDown = false;

        int position = 0;

        public override void Draw(System.Drawing.Graphics g)
        {
            base.Draw(g);


            if (Volume < 0)
                Volume = 0;

            if (Volume > ContentRectangle.Width)
                Volume = ContentRectangle.Width;


            using (Pen borderpen = new Pen(Color.FromArgb(139, 141, 144)))
            {
                borderpen.SetLineCap(
                    LineCap.Round,
                    LineCap.Round,
                    DashCap.Round);

                lineRect = new Rectangle(
                    base.ContentRectangle.X,
                    base.ContentRectangle.Y + base.ContentRectangle.Height / 2 - 1,
                    base.ContentRectangle.Width,
                    3);

                round = new RectangleF(
                    Volume + base.ContentRectangle.X - 4.5f,
                    lineRect.Y - 4.5f,
                    11, 11);

                //绘制 线性 白色底景
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.White)), lineRect);
                g.DrawRectangle(borderpen, lineRect);

                //绘制播放进度线
                Rectangle greedLine = new Rectangle(
                    lineRect.X, lineRect.Y,
                    (int)Volume, 3);
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 130, 255)), greedLine);
                //g.DrawRectangle(new Pen(Color.FromArgb(48, 192, 136)), greedLine);

                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;


                if (antiTimer != null)
                {
                    Bitmap bitmap = Resources.AnimateProgress;

                    Bitmap comment = new Bitmap(368, 26);
                    Graphics g2 = Graphics.FromImage(comment);
                    g2.DrawImage(bitmap, new Rectangle(0, 0, 368, 26));
                    g2.Dispose();

                    Bitmap newbit = new Bitmap(46, 26);
                    Graphics g1 = Graphics.FromImage(newbit);
                    g1.DrawImage(comment, new Point(-position, 0));
                    g1.Dispose();

                    g.DrawImage(newbit, new PointF(round.X - 33.5f, round.Y - 22f));

                    newbit.Dispose();
                    bitmap.Dispose();
                    comment.Dispose();
                }

                //绘制圆球
                g.DrawEllipse(borderpen, round);
                //g.FillEllipse(
                //    new LinearGradientBrush(
                //        round, Color.FromArgb(255, Color.White), Color.FromArgb(255, Color.White), LinearGradientMode.Vertical), round);
                g.FillEllipse(
                        new SolidBrush(Color.FromArgb(231, 232, 229)), round);

                if (_isHover)
                    g.FillEllipse(
                        new SolidBrush(Color.FromArgb(200, 190, 185)), round);

                if (_isDown)
                    g.FillEllipse(
                        new SolidBrush(Color.FromArgb(175, 185, 180)), round);
            }
        }

        Timer antiTimer;
        public void StratAnti()
        {
            StopAnti();
            antiTimer = new Timer(new TimerCallback(delegate(object ia)
                {
                    position += 46;
                    if (position > 368)
                        position = 0;

                    base.Invalidate(ContentRectangle);
                }), null, 0, 200);
        }
        public void StopAnti()
        {
            if (antiTimer != null)
            {
                antiTimer.Dispose();
                antiTimer = null;
            }
        }

        public override void OnMouseBaseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseBaseMove(e);
            if (round.Contains(e.Location))
            {
                _isHover = true;
            }
            else
            {
                _isHover = false;
            }
            if (_isDown)
            {
                Volume = e.X - jX;

                if (OnChangeVolume != null)
                {
                    OnChangeVolume(this, EventArgs.Empty);
                }
            }
            base.Invalidate();
        }
        int jX = 0;
        public override void OnMouseRectangleDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseRectangleDown(e);
            if (round.Contains(e.Location))
            {
                jX = e.X - (int)Volume;
                _isDown = true;
            }
            else
            {
                _isDown = false;
            }
            base.Invalidate();
        }

        public override void OnMouseRectangleUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseRectangleUp(e);
            Volume = (float)(e.X - base.ContentRectangle.X);

            if (OnChangeVolume != null)
            {
                OnChangeVolume(this, EventArgs.Empty);
            }

            OnMouseRectangleLeave();
        }

        public override void OnMouseRectangleLeave()
        {
            base.OnMouseRectangleLeave();
            _isHover = false;
            _isDown = false;
            base.Invalidate();

            if (SetOk != null)
            {
                SetOk(this, EventArgs.Empty);
            }
        }
    }
}

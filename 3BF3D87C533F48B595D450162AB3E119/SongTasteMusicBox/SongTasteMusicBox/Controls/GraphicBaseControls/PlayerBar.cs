using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GraphicLayoutControl.GraphicAbstract.Interface.Class;
using SongTasteMusicBox.Properties;

namespace SongTastePlayer.Controls.GraphicBaseControls
{
    public class PlayerBar : GraphicBase
    {
        public event EventHandler OnChangePlayPos;
        public event EventHandler SetOk;
        private float _playPos = 0;

        public float PlayPos
        {
            get { return _playPos; }
            set
            {
                _playPos = value;
                base.Invalidate();
            }
        }

        private float _bufferPos = 0;

        public float BufferPos
        {
            get { return _bufferPos; }
            set
            {
                _bufferPos = value;
                base.Invalidate();
            }
        }

        public PlayerBar()
        {
            antiTimer.Interval = 200;
            antiTimer.Tick += antiTimer_Tick;
        }

        void antiTimer_Tick(object sender, EventArgs e)
        {
            position += 46;
            if (position > 368)
                position = 0;

            base.Invalidate(ContentRectangle);
        }

        Rectangle lineRect;
        RectangleF round;
        bool _isHover = false;
        bool _isDown = false;

        int position = 0;

        public override void Draw(System.Drawing.Graphics g)
        {
            base.Draw(g);

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
                    PlayPos + base.ContentRectangle.X - 4.5f,
                    lineRect.Y - 4.5f,
                    11, 11);

                //绘制 线性 白色底景
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.White)), lineRect);
                g.DrawRectangle(borderpen, lineRect);

                //绘制缓冲线条
                Rectangle bufferRect = new Rectangle(
                    lineRect.X,
                    lineRect.Y,
                    (int)BufferPos,
                    3);
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 130, 255)), bufferRect);

                //绘制播放进度线
                Rectangle greedLine = new Rectangle(
                    lineRect.X, lineRect.Y,
                    (int)PlayPos, 3);
                g.FillRectangle(new SolidBrush(Color.FromArgb(133, 230, 9)), greedLine);
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

        System.Windows.Forms.Timer antiTimer = new System.Windows.Forms.Timer();
        public void StratAnti()
        {
            StopAnti();
            antiTimer.Start();
        }
        public void StopAnti()
        {
            if (antiTimer != null)
            {
                antiTimer.Stop();
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
                PlayPos = e.X - jX;
                if (OnChangePlayPos != null)
                {
                    OnChangePlayPos(this, EventArgs.Empty);
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
                jX = e.X - (int)PlayPos;
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
            OnMouseRectangleLeave();
            if (lineRect.Contains(e.Location))
            {
                PlayPos = (float)e.X - 11f - 4.5f;
                if (PlayPos < 0)
                    PlayPos = 0;
                OnChangePlayPos(this, EventArgs.Empty);
            }
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

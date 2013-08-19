using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace SongTastePlayer
{
    public static class Blur
    {
        public static void DrawOuterGlow(Graphics gps, GraphicsPath gp, Rectangle rect, float size,
            Color lightColor,
            Color bodyColor)
        {
            Bitmap bm = new Bitmap(rect.Width / 5, rect.Height / 5);
            Graphics g = Graphics.FromImage(bm);
            Matrix mx = new Matrix(
                1.0f / 5,
                0,
                0,
                1.0f / 5,
                -(1.0f / 5),
                -(1.0f / 5));
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Transform = mx;
            Pen p = new Pen(lightColor, size);
            g.DrawPath(p, gp);
            //g.FillPath(new SolidBrush(lightColor), gp);
            g.Dispose();
            gps.Transform = new Matrix();

            gps.SmoothingMode = SmoothingMode.AntiAlias;
            gps.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gps.DrawImage(bm, rect, 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel);
            gps.FillPath(new SolidBrush(bodyColor), gp);
        }
        public static void DrawOuterGlow2(Graphics gps, GraphicsPath gp, float size, Color lightColor, Color bodyColor)
        {
            Pen p = new Pen(lightColor, size);

            gps.SmoothingMode = SmoothingMode.AntiAlias;
            gps.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gps.DrawPath(p, gp);
            gps.FillPath(new SolidBrush(bodyColor), gp);
            p.Dispose();
        }
        public static Bitmap DrawOuterGlowReturnBitmap(Rectangle rect, GraphicsPath gp, float size, Color lightColor, Color bodyColor)
        {
            Bitmap bitm = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bitm);
            Pen p = new Pen(lightColor, size);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawPath(p, gp);
            g.FillPath(new SolidBrush(bodyColor), gp);
            p.Dispose();
            g.Dispose();
            return bitm;
        }

        /// <summary>
        /// 剪切图片
        /// </summary>
        /// <param name="path_source">原始图片路径</param>
        /// <param name="path_save">目标图片路径</param>
        /// <param name="x">剪切位置的左上角x坐标</param>
        /// <param name="y">剪切位置的左上角y坐标</param>
        /// <param name="width">要剪切的宽度</param>
        /// <param name="height">要剪切的高度</param>
        public static Bitmap Cut(Bitmap sourceBitmap, int x, int y, int width, int height)
        {
            //加载底图
            Image img = sourceBitmap;
            int w = img.Width;
            int h = img.Height;
            //设置画布
            width = width >= w ? w : width;
            height = height >= h ? h : height;
            Bitmap map = new Bitmap(width, height);
            //绘图
            Graphics g = Graphics.FromImage(map);
            g.DrawImage(img, 0, 0, new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            //保存
            return map;
        }
    }
}

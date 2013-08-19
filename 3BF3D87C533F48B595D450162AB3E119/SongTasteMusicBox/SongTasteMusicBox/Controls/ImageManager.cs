using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SongTasteMusicBox.Properties;

namespace SongTastePlayer.Controls
{
    public class ImageManager
    {
        /// <summary>
        /// 播放按钮
        /// </summary>
        public Image Play { get; set; }
        /// <summary>
        /// 暂停按钮
        /// </summary>
        public Image Pause { get; set; }
        /// <summary>
        /// 上一首
        /// </summary>
        public Image UpMusic { get; set; }
        /// <summary>
        /// 下一首
        /// </summary>
        public Image NextMusic { get; set; }
        /// <summary>
        /// 静音
        /// </summary>
        public Image Mute { get; set; }

        /// <summary>
        /// 随机播放
        /// </summary>
        public Image Random { get; set; }
        /// <summary>
        /// 顺序播放
        /// </summary>
        public Image Order { get; set; }
        /// <summary>
        /// 单曲循环
        /// </summary>
        public Image SingleLoop { get; set; }
        /// <summary>
        /// 循环列表
        /// </summary>
        public Image LoopList { get; set; }
        /// <summary>
        /// 停止
        /// </summary>
        public Image Stop { get; set; }
        /// <summary>
        /// 音量中等
        /// </summary>
        public Image VMiddle { get; set; }
        /// <summary>
        /// 音量最大
        /// </summary>
        public Image VMax { get; set; }
        /// <summary>
        /// 音量最小
        /// </summary>
        public Image VMin { get; set; }
        /// <summary>
        /// 下载
        /// </summary>
        public Image Download { get; set; }
        public ImageManager()
        {
            Play = setPixel(
                Resources.播放, Color.White);
            Pause = setPixel(
                Resources.暂停, Color.White);
            UpMusic = setPixel(
                Resources.上一首, Color.White);
            NextMusic = setPixel(
                Resources.下一首, Color.White);
            Stop = setPixel(
                Resources.停止, Color.White);
            Mute = setPixel(
                Resources.静音, Color.White);
            VMiddle = setPixel(
                Resources.音量中等, Color.White);
            VMax = setPixel(
                Resources.音量最大, Color.White);
            VMin = setPixel(
                Resources.音量最小, Color.White);

            SingleLoop = setPixel(
                Resources.单曲循环, Color.White);
            Order = setPixel(
                Resources.顺序播放, Color.White);
            LoopList = setPixel(
                Resources.循环列表, Color.White);
            Random = setPixel(
                Resources.随机, Color.White);
            Download = setPixel(
                Resources.下载, Color.White);
        }

        private Bitmap setPixel(Bitmap bitmap, Color color)
        {
            Bitmap bit = bitmap.Clone() as Bitmap;
            Bitmap _32bit = new Bitmap(32,32);
            Graphics g = Graphics.FromImage(_32bit);
            g.DrawImage(bit, new Rectangle(0, 0, 32, 32));
            g.Dispose();
            bit.Dispose();

            for (int i = 0; i < _32bit.Width; i++)
            {
                for (int j = 0; j < _32bit.Height; j++)
                {
                    _32bit.SetPixel(
                        i, j, Color.FromArgb(
                        _32bit.GetPixel(i, j).A,
                        color));
                }
            }
            return _32bit;
        }
    }
}

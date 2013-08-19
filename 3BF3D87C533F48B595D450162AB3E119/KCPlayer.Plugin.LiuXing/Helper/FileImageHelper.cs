

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class FileImageHelper
    {
        /// <summary>
        /// 二进制转换成图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static System.Drawing.Image GetImageFromByte(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0) return null;
            var stream = new System.IO.MemoryStream(bytes);
            var img = System.Drawing.Image.FromStream(stream);
            return img;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static System.IO.Stream ToStream(byte[] bytes)
        {
            var stream = new System.IO.MemoryStream(bytes);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return stream;
        }
    }
}

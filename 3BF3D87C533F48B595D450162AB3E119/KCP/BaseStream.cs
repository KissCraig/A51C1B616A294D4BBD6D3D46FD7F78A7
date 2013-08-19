namespace KCP
{
    public static class BaseStream
    {
        /// <summary>
        /// 二进制转换成图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static System.Drawing.Image GetImageFromByte(this byte[] bytes)
        {
            return System.Drawing.Image.FromStream(bytes.ToStream());
        }

        /// <summary>
        /// Stream 转换为 image 图片
        /// </summary>
        /// <returns></returns>
        public static System.Drawing.Image ToImage(this System.IO.Stream stream)
        {
            var img = new System.Drawing.Bitmap(stream);
            return img;
        }

        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this System.IO.Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static System.IO.Stream ToStream(this byte[] bytes)
        {
            var stream = new System.IO.MemoryStream(bytes);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// Stream 转换为 string ,使用 Encoding.Default 编码
        /// </summary>
        /// <returns></returns>
        public static string ToStr(this System.IO.Stream stream)
        {
            return System.Text.Encoding.UTF8.GetString(stream.ToBytes());
        }

        /// <summary>
        /// string 转换为 byte[] 
        /// </summary>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            return System.Text.Encoding.Default.GetBytes(str);
        }

        /// <summary>
        /// string 转换为 Stream 
        /// </summary>
        /// <returns></returns>
        public static System.IO.Stream ToStream(this string str)
        {
            System.IO.Stream stream = new System.IO.MemoryStream(str.ToBytes());
            // 设置当前流的位置为流的开始 
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public static void StreamToFile(this System.IO.Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            // 把 byte[] 写入文件
            var fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            var bw = new System.IO.BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static System.IO.Stream FileToStream(this string fileName)
        {
            // 打开文件
            var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read,
                                                      System.IO.FileShare.Read);
            // 读取文件的 byte[]
            var bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            var stream = new System.IO.MemoryStream(bytes);
            return stream;
        }
    }
}
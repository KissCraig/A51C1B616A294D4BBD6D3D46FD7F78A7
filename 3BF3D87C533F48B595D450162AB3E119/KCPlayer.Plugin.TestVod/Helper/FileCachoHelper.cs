
namespace KCPlayer.Plugin.TestVod.Helper
{
    public class FileCachoHelper
    {
        /// <summary>
        /// 读取本地文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            #region String -> Path -> ReadFile

            try
            {
                string str = System.IO.File.ReadAllText(path);
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            catch
            {
                return null;
            }
            return null;
            #endregion
        }

        /// <summary>
        /// string 转换为 byte[] 
        /// </summary>
        /// <returns></returns>
        public static byte[] ToBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// string 转换为 Stream 
        /// </summary>
        /// <returns></returns>
        public static System.IO.Stream ToStream(string str)
        {
            System.IO.Stream stream = new System.IO.MemoryStream(ToBytes(str));
            // 设置当前流的位置为流的开始 
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return stream;
        }
    }
}

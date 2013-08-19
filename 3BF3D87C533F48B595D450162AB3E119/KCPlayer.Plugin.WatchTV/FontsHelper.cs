using System.IO;

namespace KCPlayer.Plugin.WatchTV
{
    public class FontsHelper
    {
        /// <summary>
        /// 取得字体家族
        /// </summary>
        /// <param name="resxname"></param>
        /// <returns></returns>
        public System.Drawing.FontFamily GetFont(string resxname)
        {
            if (string.IsNullOrEmpty(resxname)) return null;
            var pFont = new System.Drawing.Text.PrivateFontCollection();
            try
            {
                var stream = GetType().Assembly.GetManifestResourceStream("KCPlayer.Plugin.WatchTV.Fonts." + resxname);
                if (stream == null)
                {
                    return null;
                }
                var bFont = StreamToBytes(stream);
                var meAdd =
                    System.Runtime.InteropServices.Marshal.AllocHGlobal(
                        System.Runtime.InteropServices.Marshal.SizeOf(typeof (byte))*bFont.Length);
                System.Runtime.InteropServices.Marshal.Copy(bFont, 0, meAdd, bFont.Length);
                pFont.AddMemoryFont(meAdd, bFont.Length);
            }
            catch
            {
                return null;
            }
            return pFont.Families[0];
        }

        /// <summary>
        /// 数据流转字节
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static byte[] StreamToBytes(Stream stream)
        {
            try
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);

                // 设置当前流的位置为流的开始 
                stream.Seek(0, SeekOrigin.Begin);
                return bytes;
            }
            catch
            {
                return null;
            }
        }
    }
}
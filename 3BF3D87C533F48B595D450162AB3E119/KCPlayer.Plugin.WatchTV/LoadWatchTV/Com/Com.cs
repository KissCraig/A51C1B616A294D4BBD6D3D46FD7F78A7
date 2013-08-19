namespace KCPlayer.Plugin.WatchTV.LoadWatchTV.Com
{
    public class Pub
    {
        /// <summary>
        /// 从字节转图片
        /// </summary>
        /// <param name="imgbyte"></param>
        /// <returns></returns>
        public static System.Drawing.Image ByteToImage(byte[] imgbyte)
        {
            if (imgbyte == null || imgbyte.Length <= 0) return null;
            var stream = new System.IO.MemoryStream(imgbyte);
            var img = System.Drawing.Image.FromStream(stream);
            return img;
        }

        /// <summary>
        /// 复制List<string/> 的数据
        /// </summary>
        /// <param name="tagDrl"></param>
        public static void CopyThisUrl(System.Collections.Generic.IEnumerable<string> tagDrl)
        {
            System.Windows.Forms.Clipboard.Clear();
            var clipboard = "";
            foreach (var tagdrl in tagDrl)
            {
                clipboard += System.Web.HttpUtility.HtmlDecode(tagdrl) + System.Environment.NewLine;
            }
            clipboard = clipboard.Replace(@"

", "");
            System.Windows.Forms.Clipboard.SetDataObject(clipboard);
            PublicStatic.LiuXingCon.Focus();
        }

        /// <summary>
        /// 启动点播动作
        /// </summary>
        /// <param name="url"></param>
        public static void StartToVod(string url)
        {
            if (string.IsNullOrEmpty(url)) return;
            var vodurl = System.Web.HttpUtility.HtmlDecode(url);
            if ((bool) MainInterFace.OuterInvoke.InvokeOuterMethod(
                "云点播白金版",
                "IsVip", false, null))
            {
                try
                {
                    MainInterFace.OuterInvoke.InvokeOuterMethod(
                        "云点播白金版",
                        "OutPlay",
                        true,
                        vodurl,
                        true);
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    MainInterFace.OuterInvoke.InvokeOuterMethod(
                        "云点播",
                        "OutPlay",
                        true,
                        vodurl,
                        true);
                }
                catch
                {
                }
            }
            PublicStatic.LiuXingCon.Focus();
        }
    }
}
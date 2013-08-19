namespace KCP.Plugin.LiuXing.Url
{
    public class Pub
    {
        /// <summary>
        /// 复制List<string/> 的数据
        /// </summary>
        /// <param name="tagDrl"></param>
        public static void CopyThisUrl(System.Collections.Generic.IEnumerable<string> tagDrl)
        {
            try
            {
                System.Windows.Forms.Clipboard.Clear();
                var clipboard = "";
                foreach (var tagdrl in tagDrl)
                {
                    clipboard += tagdrl.ToHtmlDecode() + System.Environment.NewLine;
                }
                clipboard = clipboard.Replace(@"

", "");
                System.Windows.Forms.Clipboard.SetDataObject(clipboard);
                //PublicStatic.LiuXingCon.Focus();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 启动点播动作
        /// </summary>
        /// <param name="url"></param>
        public static void StartToVod(string url)
        {
            if (string.IsNullOrEmpty(url)) return;
            var vodurl = url.ToHtmlDecode();
            //if ((bool) MainInterFace.OuterInvoke.InvokeOuterMethod(
            //    "云点播白金版",
            //    "IsVip", false, null))
            //{
            //    try
            //    {
            //        MainInterFace.OuterInvoke.InvokeOuterMethod(
            //            "云点播白金版",
            //            "OutPlay",
            //            true,
            //            vodurl,
            //            true);
            //    }
            //    catch
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        MainInterFace.OuterInvoke.InvokeOuterMethod(
            //            "云点播",
            //            "OutPlay",
            //            true,
            //            vodurl,
            //            true);
            //    }
            //    catch
            //    {
            //        return;
            //    }
            //}
            //PublicStatic.LiuXingCon.Focus();
        }
    }
}
using KCPlayer.Plugin.LiuXing.Controls;
using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class VodCopyHelper
    {
        /// <summary>
        /// 复制List<string /> 的数据
        /// </summary>
        /// <param name="tagDrl"></param>
        public static void CopyThisUrl(System.Collections.Generic.IEnumerable<string> tagDrl)
        {
            try
            {
                System.Windows.Forms.Clipboard.Clear();
                string clipboard = "";
                foreach (string tagdrl in tagDrl)
                {
                    clipboard += System.Web.HttpUtility.HtmlDecode(tagdrl) + System.Environment.NewLine;
                }
                clipboard = clipboard.Replace(@"

", "");
                System.Windows.Forms.Clipboard.SetDataObject(clipboard);
                AutoCloseDlg.ShowMessageBoxTimeout(@"偶已经复制好了", "^_^", System.Windows.Forms.MessageBoxButtons.OK, 1000);
                PublicStatic.LiuXingCon.Focus();
            }
            catch
            {
            }
        }

        /// <summary>
        ///     启动点播动作
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tag"></param>
        public static void StartToVod(System.Collections.Generic.List<string> url, LiuXingData tag)
        {
            if (url == null || url.Count <= 0) return;
            if (tag != null)
            {
                FileCachoHelper.SaveThisVodTag(tag);
            }
            var bestVodUrl = Helper.QualityHelper.GetHdsVod(url);
            if (string.IsNullOrEmpty(bestVodUrl)) return;
            string vodurl = System.Web.HttpUtility.HtmlDecode(bestVodUrl);

            try
            {
                if ((bool)MainInterFace.OuterInvoke.InvokeOuterMethod(
                    "云点播白金版",
                    "IsVip", false, null))
                {
                    MainInterFace.OuterInvoke.InvokeOuterMethod(
                        "云点播白金版",
                        "OutPlay",
                        true,
                        vodurl,
                        true);
                }
                else
                {
                    MainInterFace.OuterInvoke.InvokeOuterMethod(
                    "云点播放",
                    "CallMeAction",
                    true,
                    "影片名字",
                    vodurl);
                }

                //if (PublicStatic.IsTestVod)
                //{
                    
                //}
                //else
                //{
                //    MainInterFace.OuterInvoke.InvokeOuterMethod(
                //        "云点播",
                //        "OutPlay",
                //        true,
                //        vodurl,
                //        true);
                //}

                //if ((bool)MainInterFace.OuterInvoke.InvokeOuterMethod(
                //    "云点播白金版",
                //    "IsVip", false, null))
                //{
                //    MainInterFace.OuterInvoke.InvokeOuterMethod(
                //        "云点播白金版",
                //        "OutPlay",
                //        true,
                //        vodurl,
                //        true);
                //}
                //else
                //{
                    
                //}
            }
            catch
            {
                return;
            }
            PublicStatic.LiuXingCon.Focus();
        }
    }
}

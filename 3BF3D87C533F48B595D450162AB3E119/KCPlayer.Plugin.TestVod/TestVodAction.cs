using System.Drawing;
using System.IO;


namespace KCPlayer.Plugin.TestVod
{
    public class TestVodAction
    {
        /// <summary>
        ///     执行搜索
        /// </summary>
        public static void AnSearchStart()
        {
            // 取搜索值
            string boxvalue = PublicStatic.VodInput.Text.Trim();
            // 开始搜索
            if (string.IsNullOrEmpty(boxvalue)) return;
            VodUrl.AnalyzeVodPath(boxvalue);
        }

        public static void VodInput_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Get All Path From Drag In
            string[] dropPath;
            try
            {
                dropPath = ((string[]) e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop));
            }
            catch
            {
                dropPath = null;
            }
            // Judge Path Is Null
            if (dropPath == null) return;
            if (dropPath.Length <= 0) return;
            string path = dropPath[0];
            string extension = Path.GetExtension(path);
            if (extension != null && extension.ToLower().StartsWith(".to"))
            {
                PublicStatic.VodInput.Text = path;
                AnSearchStart();
            }
        }

        public static void anShouQibtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            ShouQi(true);
        }

        public static void aZhidingbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            ((System.Windows.Forms.Form) MainInterFace.Owner.Parent).TopMost =
                !((System.Windows.Forms.Form) MainInterFace.Owner.Parent).TopMost;
            PublicStatic.Dingbtn.Text = ((System.Windows.Forms.Form) MainInterFace.Owner.Parent).TopMost
                                            ? @"置　后"
                                            : @"置　顶";
        }

        public static void VodBar_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            try
            {
                var clipData = System.Windows.Forms.Clipboard.GetText();
                if (string.IsNullOrEmpty(clipData))
                {
                    ShouQi(false);
                }
                else
                {
                    if (clipData != TestVodPal.VodInputTip)
                    {
                        if (clipData != PublicStatic.VodInput.Text)
                        {
                            PublicStatic.VodInput.Text = clipData;
                            AnSearchStart();
                            //System.Windows.Forms.Clipboard.Clear();
                        }
                        else
                        {
                            ShouQi(false);
                        }
                    }
                    else
                    {
                        ShouQi(false);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        ///     回车搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SearchBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // 接受回车搜索
            if (e.KeyCode != System.Windows.Forms.Keys.Enter) return;
            AnSearchStart();
        }

        /// <summary>
        ///     点击搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void anSearchbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            AnSearchStart();
        }

        /// <summary>
        /// </summary>
        /// <param name="isShouQi"></param>
        public static void ShouQi(bool isShouQi)
        {
            if (isShouQi)
            {
                if (PublicStatic.VodBar.Size.Height>30)
                {
                    PublicStatic.VodBar.Size = new Size(PublicStatic.VodPal.Width, 3);
                    PublicStatic.VodBar.BackColor = PublicStatic.MainColor[PublicStatic.MainIndex];
                }
            }
            else
            {
                PublicStatic.VodBar.Size = new Size(PublicStatic.VodPal.Width, 38 * 2);
                PublicStatic.VodBar.BackColor = Color.Transparent;
                PublicStatic.VodIndex.Focus();
            }
        }
    }
}
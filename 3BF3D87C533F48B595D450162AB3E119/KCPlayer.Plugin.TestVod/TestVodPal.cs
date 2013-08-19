
using KCPlayer.Plugin.TestVod.Controls;
using KCPlayer.Plugin.TestVod.Helper;

namespace KCPlayer.Plugin.TestVod
{
    public class TestVodPal
    {
        public const string VodInputTip = @" - 粘贴资源地址 / 推拽或双击添加种子 + 单击蓝色条恢复输入面板 - ";

        /// <summary>
        ///     加载整个公共的主面板
        /// </summary>
        public static
        void LoadPublicPal()
        {
            PublicStatic.RightPal = new EPanel
                {
                    Size =
                        new System.Drawing.Size(PublicStatic.VodPal.Width,
                                 PublicStatic.VodPal.Height),
                    Location = new System.Drawing.Point(0, 0),
                    BackColor = System.Drawing.Color.Transparent,
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    Anchor =
                        System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right |
                        System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                };
            PublicStatic.VodPal.Controls.Add(PublicStatic.RightPal);
            LoadRightPal();
        }

        /// <summary>
        /// </summary>
        private static void LoadRightPal()
        {
            // 加载点播导航条
            PublicStatic.VodBar = new EPanel
                {
                    Size = new System.Drawing.Size(PublicStatic.RightPal.Width, 38*2),
                    Location = new System.Drawing.Point(0, 0),
                    BackColor = System.Drawing.Color.Transparent,
                    Dock = System.Windows.Forms.DockStyle.Top,
                    Anchor =
                        System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top |
                        System.Windows.Forms.AnchorStyles.Right,
                    AllowDrop = true
                };
            PublicStatic.VodBar.DragEnter += HLable_DragEnter;
            PublicStatic.VodBar.MouseClick += TestVodAction.VodBar_MouseClick;
            PublicStatic.RightPal.Controls.Add(PublicStatic.VodBar);
            LoadVodBarItem();

            // 内容面板
            PublicStatic.VodBrowser = new EnBrowser
                {
                    ScriptErrorsSuppressed = true,
                    AllowWebBrowserDrop = false,
                    ScrollBarsEnabled = false,
                    IsWebBrowserContextMenuEnabled = false,
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    Size =
                        new System.Drawing.Size(PublicStatic.RightPal.Width,
                                 PublicStatic.RightPal.Height - PublicStatic.VodBar.Height),
                    Location = new System.Drawing.Point(0, PublicStatic.VodBar.Height),
                    BackColor = System.Drawing.Color.Black
                };
            PublicStatic.VodBrowser.DocumentCompleted += WebBrowerHeper.LiuXingCon_DocumentCompleted;
            PublicStatic.VodBrowser.BeforeNewWindow += WebBrowerHeper.LiuXingCon_BeforeNewWindow;
            PublicStatic.VodBrowser.Navigate("http://www.kcplayer.com/Load.html");
            PublicStatic.VodBrowser.Focus();
            PublicStatic.RightPal.Controls.Add(PublicStatic.VodBrowser);
        }


        /// <summary>
        ///     加载顶部操作面板
        /// </summary>
        private static void LoadVodBarItem()
        {
            PublicStatic.VodIndex = new System.Windows.Forms.TextBox
                {
                    Visible = true,
                    Size = new System.Drawing.Size(0, 0),
                    Location = new System.Drawing.Point(0, 0)
                };
            PublicStatic.VodBar.Controls.Add(PublicStatic.VodIndex);

            PublicStatic.VodInput = new InputSearch(
                PublicStatic.VodBar,
                1,
                1,
                @"点　播",
                VodInputTip,
                new System.Drawing.Font(PublicStatic.SegoeFont, 12.5F),
                new System.Drawing.Size(103, 34),
                new System.Drawing.Font(PublicStatic.SegoeFont, 12.5F),
                new System.Drawing.Size(PublicStatic.VodBar.Width - 103 * 3 + 2 + 102, 34),
                new System.Drawing.Point(0, 3),
                PublicStatic.FontColor[1],
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.FontColor[1],
                PublicStatic.FontColor[1],
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                TestVodAction.anSearchbtn_MouseClick,
                TestVodAction.SearchBox_KeyDown,
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right
                );
            PublicStatic.VodInput.DragDrop += TestVodAction.VodInput_DragDrop;
            PublicStatic.VodInput.DoubleClick += VodInput_DoubleClick;
            PublicStatic.VodBar.DragDrop += TestVodAction.VodInput_DragDrop;
            PublicStatic.VodInput.GotFocus += VodInput_GotFocus;

            PublicStatic.VodIndex.Focus();

            // 加载顶部导航面板
            PublicStatic.TestVodNavLeft = new MetroForList(
                PublicStatic.VodBar,
                true,
                "可选通道",
                null,
                null,
                125,
                32,
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.FontColor[1],
                PublicStatic.SegoeFont,
                new System.Drawing.Point(0, 2 + 38),
                TestVodNavLeft_ListItemTxt_MouseClick
            );
            PublicStatic.NowTestVodPlayAPI = "v4api"; // 稳定版
            // 更新通道
            VodStyleHelper.UpdateTongDao();
            PublicStatic.TestVodNavLeft.UpdateListItem();

            PublicStatic.TestVodNavRight = new MetroForList(
                PublicStatic.VodBar,
                true,
                "",
                new System.Collections.Generic.List<object>
                    {
                    "收　起,Button","置　顶,Button"
                },
                null,
                101,
                32,
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.FontColor[1],
                PublicStatic.SegoeFont,
                new System.Drawing.Point(PublicStatic.VodBar.Width - 101 * 2 - 3, 3),
                TestVodNavRight_ListItemTxt_MouseClick
            )
            {
                Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            };

        }

        /// <summary>
        /// 自动粘贴地址进来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void VodInput_GotFocus(object sender, System.EventArgs e)
        {
            try
            {
                var clipData = System.Windows.Forms.Clipboard.GetText();
                if (string.IsNullOrEmpty(clipData)) return;
                if (clipData != VodInputTip)
                {
                    if (clipData != PublicStatic.VodInput.Text)
                    {
                        if (VodUrl.AnalyzeVodType(clipData) != VodUrlType.None)
                        {
                            PublicStatic.VodInput.Text = clipData;
                            TestVodAction.AnSearchStart();
                        }
                    }
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }


        /// <summary>
        /// 双击打开选择种子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void VodInput_DoubleClick(object sender, System.EventArgs e)
        {
            #region 双击打开选择种子
            var localpath = OpenAndSelectFile("请选择你要播放的种子文件", "常见种子格式|*.torrent");
            if (string.IsNullOrEmpty(localpath)) return;
            PublicStatic.VodInput.Text = localpath;
            TestVodAction.AnSearchStart(); 
            #endregion
        }

        /// <summary>
        /// 打开并选择文档
        /// </summary>
        /// <returns></returns>
        private static string OpenAndSelectFile(string title, string filter)
        {
            #region 打开并选择文档
            var op = new System.Windows.Forms.OpenFileDialog
            {
                Title = title,
                Filter = filter,
                RestoreDirectory = true,
            };
            if (op.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;
            var oppath = op.FileName;
            return System.IO.File.Exists(oppath) ? oppath : null; 
            #endregion
        }

        private static void TestVodNavRight_ListItemTxt_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region 第一层面板
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser != null)
            {
                if ("收　起".Contains(ser.Text))
                {
                    TestVodAction.ShouQi(true);
                }
                else
                {
                    if ("置　顶".Contains(ser.Text))
                    {
                        ((System.Windows.Forms.Form)MainInterFace.Owner.Parent).TopMost = !((System.Windows.Forms.Form)MainInterFace.Owner.Parent).TopMost;
                    }
                }
            } 
            #endregion
        }

        private static void TestVodNavLeft_ListItemTxt_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region 通道面板点击
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser != null)
            {
                VodStyleHelper.UpdateThisTongDao(ser.Text.Trim());
                // 开始点播
                VodStyleHelper.UpdateThisPlay();
                // 更新通道
                VodStyleHelper.UpdateTongDao();
            } 
            #endregion
        }

        private static void HLable_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }
}
using KCP.Control.Base;
using KCP.Control.Fase;
using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;
using KCP.Plugin.LiuXing.Url;

namespace KCP.Plugin.LiuXing.LoadLiuXing.Dyfm
{
    public class SearchDis
    {
        /// <summary>
        /// 逐个显示
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="img"></param>
        public static void DisPlayListItem(LiuXingData tag, System.Drawing.Image img)
        {
            if (tag == null) return;
            // 磁贴方块
            var cellpal = new EPanel
                {
                    Size = new System.Drawing.Size(406, 210),
                    BackColor = System.Drawing.Color.White,
                    Tag = tag
                };
            PublicStatic.LiuXingCon.Controls.Add(cellpal);

            if (!string.IsNullOrEmpty(tag.Cos))
            {
                new HDarge(
                    cellpal,
                    tag.Cos,
                    new System.Drawing.Font(PublicStatic.MainFont, 12F),
                    new System.Drawing.Size(40, 25),
                    new System.Drawing.Point(-2, 44 + 24 + 45 + 25 + 20 - 135),
                    System.Drawing.Color.FromArgb(248, 248, 248), System.Drawing.Color.FromArgb(0, 122, 204),
                    System.Drawing.ContentAlignment.MiddleCenter, System.Windows.Forms.AnchorStyles.Top
                    );
            }
            // 方块内海报
            if (img != null)
            {
                var cellimg = new EPicBox
                    {
                        Size = new System.Drawing.Size(150, 210),
                        BackColor = System.Drawing.Color.White,
                        Image = img,
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                    };
                cellpal.Controls.Add(cellimg);
            }
            // 方块内文字
            var tempname = tag.Name;
            if (!string.IsNullOrEmpty(tempname))
            {
                if (tempname.Contains("/"))
                {
                    tempname = tempname.Split("/".ToCharArray())[0];
                }
                new HDarge(
                    cellpal,
                    tempname,
                    new System.Drawing.Font(PublicStatic.MainFont, 22F),
                    new System.Drawing.Size(244, 42),
                    new System.Drawing.Point(150, 10),
                    System.Drawing.Color.FromArgb(0, 122, 204), System.Drawing.Color.Transparent,
                    System.Drawing.ContentAlignment.BottomCenter, System.Windows.Forms.AnchorStyles.Top
                    );
            }
            new HDarge(
                cellpal,
                "",
                new System.Drawing.Font(PublicStatic.MainFont, 14F),
                new System.Drawing.Size(240, 1),
                new System.Drawing.Point(150 + 8, 48 + 10),
                System.Drawing.Color.Transparent, System.Drawing.Color.FromArgb(90, 122, 204),
                System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "主演：" + tag.Car,
                new System.Drawing.Font(PublicStatic.MainFont, 12F),
                new System.Drawing.Size(248, 45),
                new System.Drawing.Point(150 + 6, 44 + 24),
                System.Drawing.Color.FromArgb(60, 60, 60), System.Drawing.Color.Transparent,
                System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "年代：" + tag.Tim,
                new System.Drawing.Font(PublicStatic.MainFont, 12F),
                new System.Drawing.Size(120, 25),
                new System.Drawing.Point(150 + 6, 44 + 24 + 45),
                System.Drawing.Color.FromArgb(60, 60, 60), System.Drawing.Color.Transparent,
                System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "地区：" + tag.Loc,
                new System.Drawing.Font(PublicStatic.MainFont, 12F),
                new System.Drawing.Size(120, 25),
                new System.Drawing.Point(150 + 6 + 120, 44 + 24 + 45),
                System.Drawing.Color.FromArgb(60, 60, 60), System.Drawing.Color.Transparent,
                System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "类型：" + tag.Typ,
                new System.Drawing.Font(PublicStatic.MainFont, 12F),
                new System.Drawing.Size(120, 25),
                new System.Drawing.Point(150 + 6, 44 + 24 + 45 + 25),
                System.Drawing.Color.FromArgb(60, 60, 60), System.Drawing.Color.Transparent,
                System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "更新：" + tag.Upt,
                new System.Drawing.Font(PublicStatic.MainFont, 12F),
                new System.Drawing.Size(120, 25),
                new System.Drawing.Point(150 + 6 + 120, 44 + 24 + 45 + 25),
                System.Drawing.Color.FromArgb(60, 60, 60), System.Drawing.Color.Transparent,
                System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                );

            // 方块内按钮
            var temphds = tag.HDs;
            if (string.IsNullOrEmpty(temphds)) return;
            var playbtn = new LButton
                (
                cellpal,
                1,
                temphds,
                new System.Drawing.Font(PublicStatic.MainFont, 12.5F),
                new System.Drawing.Size(101, 32),
                new System.Drawing.Point(150 + 152, 40 + 25*5 + 2*5),
                System.Drawing.Color.FromArgb(0, 122, 204),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(0, 122, 204),
                System.Drawing.Color.FromArgb(0, 122, 204),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Windows.Forms.AnchorStyles.Top
                );
            playbtn.MouseClick += Playbtn_MouseClick;

            var copybtn = new LButton
                (
                cellpal,
                1,
                "复制链接",
                new System.Drawing.Font(PublicStatic.MainFont, 12.5F),
                new System.Drawing.Size(101, 32),
                new System.Drawing.Point(150 + 152 - 101 - 8, 40 + 25*5 + 2*5),
                System.Drawing.Color.FromArgb(0, 122, 204),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Drawing.Color.FromArgb(0, 122, 204),
                System.Drawing.Color.FromArgb(0, 122, 204),
                System.Drawing.Color.FromArgb(248, 248, 248),
                System.Windows.Forms.AnchorStyles.Top
                );

            copybtn.MouseClick += Copybtn_MouseClick;

            if (string.IsNullOrEmpty(PublicStatic.SearchBox.Text))
            {
                PublicStatic.LiuXingCon.Focus();
            }
        }

        /// <summary>
        /// 点击复制按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Copybtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = sender as LButton;
            if (ser == null) return;
            var tag = ser.Parent.Parent.Tag as LiuXingData;
            if (tag == null) return;
            if (tag.Drl != null && tag.Drl.Count > 0)
            {
                Pub.CopyThisUrl(tag.Drl);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(@"暂无数据可复制");
            }
        }

        /// <summary>
        /// 点击观看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Playbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = sender as LButton;
            if (ser == null) return;
            var tag = ser.Parent.Parent.Tag as LiuXingData;
            if (tag == null) return;
            if (tag.Drl != null && tag.Drl.Count > 0)
            {
                // 从链接里面挑选可以播放的最后一个链接
                var currentUrl = tag.Drl[0];
                if (!string.IsNullOrEmpty(currentUrl))
                {
                    Pub.StartToVod(currentUrl);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(@"暂无数据可播放");
            }
        }
    }
}
using System.Drawing;
using System.Windows.Forms;
using KCPlayer.Plugin.LiuXing.Controls;
using KCPlayer.Plugin.LiuXing.LoadLiuXing.Com;

namespace KCPlayer.Plugin.LiuXing.LoadLiuXing.Piaohua
{
    public class SearchDis
    {
        /// <summary>
        ///     逐个显示
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="img"></param>
        public static void DisPlayListItem(LiuXingData tag, Image img)
        {
            if (tag == null) return;
            // 磁贴方块
            var cellpal = new EPanel
                {
                    Size = new Size(406, 210),
                    BackColor = Color.White,
                    Tag = tag
                };
            PublicStatic.LiuXingCon.Controls.Add(cellpal);

            if (!string.IsNullOrEmpty(tag.Cos))
            {
                new HDarge(
                    cellpal,
                    tag.Cos,
                    new Font(PublicStatic.SegoeFont, 12F),
                    new Size(40, 25),
                    new Point(-2, 44 + 24 + 45 + 25 + 20 - 135),
                    Color.FromArgb(248, 248, 248), Color.FromArgb(0, 122, 204),
                    ContentAlignment.MiddleCenter, AnchorStyles.Top
                    );
            }
            // 方块内海报
            if (img != null)
            {
                var cellimg = new EPicBox
                    {
                        Size = new Size(150, 210),
                        BackColor = Color.White,
                        Image = img,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                cellpal.Controls.Add(cellimg);
            }
            // 方块内文字
            string tempname = tag.Name;
            if (!string.IsNullOrEmpty(tempname))
            {
                if (tempname.Contains("/"))
                {
                    tempname = tempname.Split("/".ToCharArray())[0];
                }
                new HDarge(
                    cellpal,
                    tempname,
                    new Font(PublicStatic.SegoeFont, 22F),
                    new Size(244, 42),
                    new Point(150, 10),
                    Color.FromArgb(0, 122, 204), Color.Transparent,
                    ContentAlignment.BottomCenter, AnchorStyles.Top
                    );
            }
            new HDarge(
                cellpal,
                "",
                new Font(PublicStatic.SegoeFont, 14F),
                new Size(240, 1),
                new Point(150 + 8, 48 + 10),
                Color.Transparent, Color.FromArgb(90, 122, 204),
                ContentAlignment.MiddleLeft, AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "主演：" + tag.Car,
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(248, 45),
                new Point(150 + 6, 44 + 24),
                Color.FromArgb(60, 60, 60), Color.Transparent,
                ContentAlignment.MiddleLeft, AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "年代：" + tag.Tim,
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(120, 25),
                new Point(150 + 6, 44 + 24 + 45),
                Color.FromArgb(60, 60, 60), Color.Transparent,
                ContentAlignment.MiddleLeft, AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "地区：" + tag.Loc,
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(120, 25),
                new Point(150 + 6 + 120, 44 + 24 + 45),
                Color.FromArgb(60, 60, 60), Color.Transparent,
                ContentAlignment.MiddleLeft, AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "类型：" + tag.Typ,
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(120, 25),
                new Point(150 + 6, 44 + 24 + 45 + 25),
                Color.FromArgb(60, 60, 60), Color.Transparent,
                ContentAlignment.MiddleLeft, AnchorStyles.Top
                );
            new HDarge(
                cellpal,
                "更新：" + tag.Upt,
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(120, 25),
                new Point(150 + 6 + 120, 44 + 24 + 45 + 25),
                Color.FromArgb(60, 60, 60), Color.Transparent,
                ContentAlignment.MiddleLeft, AnchorStyles.Top
                );

            // 方块内按钮
            string temphds = tag.HDs;
            if (string.IsNullOrEmpty(temphds)) return;
            var playbtn = new LButton
                (
                cellpal,
                1,
                temphds,
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(101, 32),
                new Point(150 + 152, 40 + 25*5 + 2*5),
                Color.FromArgb(0, 122, 204),
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(0, 122, 204),
                Color.FromArgb(0, 122, 204),
                Color.FromArgb(248, 248, 248),
                AnchorStyles.Top
                );
            var copybtn = new LButton
                (
                cellpal,
                1,
                "复制链接",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(101, 32),
                new Point(150 + 152 - 101 - 8, 40 + 25*5 + 2*5),
                Color.FromArgb(0, 122, 204),
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(0, 122, 204),
                Color.FromArgb(0, 122, 204),
                Color.FromArgb(248, 248, 248),
                AnchorStyles.Top
                );
            copybtn.MouseClick += Copybtn_MouseClick;
            playbtn.MouseClick += Playbtn_MouseClick;

            if (string.IsNullOrEmpty(PublicStatic.SearchBox.Text))
            {
                PublicStatic.LiuXingCon.Focus();
            }
        }

        /// <summary>
        ///     点击复制按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Copybtn_MouseClick(object sender, MouseEventArgs e)
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
                MessageBox.Show(@"暂无数据可复制");
            }
        }

        /// <summary>
        ///     点击观看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Playbtn_MouseClick(object sender, MouseEventArgs e)
        {
            var ser = sender as LButton;
            if (ser == null) return;
            var tag = ser.Parent.Parent.Tag as LiuXingData;
            if (tag == null) return;
            if (tag.Drl != null && tag.Drl.Count > 0)
            {
                // 从链接里面挑选可以播放的最后一个链接
                string currentUrl = tag.Drl[tag.Drl.Count - 1];
                if (!string.IsNullOrEmpty(currentUrl))
                {
                    //Pub.StartToVod(currentUrl,new LiuXingData());
                }
            }
            else
            {
                MessageBox.Show(@"暂无数据可播放");
            }
        }
    }
}
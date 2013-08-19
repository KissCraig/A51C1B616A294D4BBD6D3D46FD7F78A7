using KCP.Control.Base;
using KCP.Control.Fase;
using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;
using KCP.Plugin.LiuXing.Url;

namespace KCP.Plugin.LiuXing
{
    public sealed class DisPlayTile : HPanel
    {
        public DisPlayTile(LiuXingType iType)
        {
            Size = new System.Drawing.Size(406, 210);
            BackColor = PublicStatic.FontColor[1];
            if (iType.Data.IsEmpty()) return;
            Tag = iType.Data;

            // 影片得分
            if (iType.Data.Cos.IsNotNullOrEmpty())
            {
                new HDarge(
                    this,
                    iType.Data.Cos,
                    new System.Drawing.Font(PublicStatic.MainFont, 12F),
                    new System.Drawing.Size(40, 25),
                    new System.Drawing.Point(-2, 44 + 24 + 45 + 25 + 20 - 135),
                    PublicStatic.FontColor[1], PublicStatic.MainColor[PublicStatic.MainIndex],
                    System.Drawing.ContentAlignment.MiddleCenter, System.Windows.Forms.AnchorStyles.Top
                    );
            }

            // 影片海报
            if (!iType.Img.IsEmpty())
            {
                try
                {
                    Controls.Add(new EPicBox
                        {
                            Size = new System.Drawing.Size(150, 210),
                            BackColor = PublicStatic.FontColor[1],
                            Image = iType.Img,
                            SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                        });
                }
                catch
                {
                }
            }

            // 影片标题
            if (iType.Data.Name.IsNotNullOrEmpty())
            {
                var tempname = iType.Data.Name;
                if (tempname.Contains("/"))
                {
                    tempname = tempname.Split("/".ToCharArray())[0];
                }
                new HDarge(
                    this,
                    tempname,
                    new System.Drawing.Font(PublicStatic.MainFont, 22F),
                    new System.Drawing.Size(244, 42),
                    new System.Drawing.Point(150, 10),
                    PublicStatic.MainColor[PublicStatic.MainIndex], System.Drawing.Color.Transparent,
                    System.Drawing.ContentAlignment.BottomCenter, System.Windows.Forms.AnchorStyles.Top
                    );
                // 影片横线
                new HDarge(
                    this,
                    "",
                    new System.Drawing.Font(PublicStatic.MainFont, 14F),
                    new System.Drawing.Size(240, 1),
                    new System.Drawing.Point(150 + 8, 48 + 10),
                    System.Drawing.Color.Transparent, PublicStatic.MainColor[PublicStatic.MainIndex],
                    System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                    );
            }

            // 影片演员
            if (iType.Data.Car.IsNotNullOrEmpty())
            {
                new HDarge(
                    this,
                    "主演：" + iType.Data.Car,
                    new System.Drawing.Font(PublicStatic.MainFont, 12F),
                    new System.Drawing.Size(248, 45),
                    new System.Drawing.Point(150 + 6, 44 + 24),
                    PublicStatic.FontColor[0], System.Drawing.Color.Transparent,
                    System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                    );
            }

            // 影片年代
            if (iType.Data.Tim.IsNotNullOrEmpty())
            {
                new HDarge(
                    this,
                    "年代：" + iType.Data.Tim,
                    new System.Drawing.Font(PublicStatic.MainFont, 12F),
                    new System.Drawing.Size(120, 25),
                    new System.Drawing.Point(150 + 6, 44 + 24 + 45),
                    PublicStatic.FontColor[0], System.Drawing.Color.Transparent,
                    System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                    );
            }

            // 影片地区
            if (iType.Data.Loc.IsNotNullOrEmpty())
            {
                new HDarge(
                    this,
                    "地区：" + iType.Data.Loc,
                    new System.Drawing.Font(PublicStatic.MainFont, 12F),
                    new System.Drawing.Size(120, 25),
                    new System.Drawing.Point(150 + 6 + 120, 44 + 24 + 45),
                    PublicStatic.FontColor[0], System.Drawing.Color.Transparent,
                    System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                    );
            }

            // 影片类型
            if (iType.Data.Typ.IsNotNullOrEmpty())
            {
                new HDarge(
                    this,
                    "类型：" + iType.Data.Typ,
                    new System.Drawing.Font(PublicStatic.MainFont, 12F),
                    new System.Drawing.Size(120, 25),
                    new System.Drawing.Point(150 + 6, 44 + 24 + 45 + 25),
                    PublicStatic.FontColor[0], System.Drawing.Color.Transparent,
                    System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                    );
            }

            // 影片更新
            if (iType.Data.Upt.IsNotNullOrEmpty())
            {
                new HDarge(
                    this,
                    "更新：" + iType.Data.Upt,
                    new System.Drawing.Font(PublicStatic.MainFont, 12F),
                    new System.Drawing.Size(120, 25),
                    new System.Drawing.Point(150 + 6 + 120, 44 + 24 + 45 + 25),
                    PublicStatic.FontColor[0], System.Drawing.Color.Transparent,
                    System.Drawing.ContentAlignment.MiddleLeft, System.Windows.Forms.AnchorStyles.Top
                    );
            }


            // 影片点播
            if (iType.Data.HDs.IsNotNullOrEmpty())
            {
                new LButton
                    (
                    this,
                    1,
                    iType.Data.HDs,
                    new System.Drawing.Font(PublicStatic.MainFont, 12.5F),
                    new System.Drawing.Size(101, 32),
                    new System.Drawing.Point(150 + 152, 40 + 25*5 + 2*5),
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    PublicStatic.FontColor[1],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    System.Windows.Forms.AnchorStyles.Top
                    ) {Tag = iType}.MouseClick += Playbtn_MouseClick;

                // 影片复制
                new LButton
                    (
                    this,
                    1,
                    "复制链接",
                    new System.Drawing.Font(PublicStatic.MainFont, 12.5F),
                    new System.Drawing.Size(101, 32),
                    new System.Drawing.Point(150 + 152 - 101 - 8, 40 + 25*5 + 2*5),
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    PublicStatic.FontColor[1],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    System.Windows.Forms.AnchorStyles.Top
                    ) {Tag = iType}.MouseClick += Copybtn_MouseClick;
            }

            // 滚轮聚焦
            if (string.IsNullOrEmpty(PublicStatic.SearchBox.Text))
            {
                PublicStatic.LiuXingCon.Invoke(
                new System.Windows.Forms.MethodInvoker
                    (() => PublicStatic.LiuXingCon.Focus()));
                
            }
        }

        /// <summary>
        /// 点击复制按钮 - Copybtn_MouseClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Copybtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region 点击复制按钮 - Copybtn_MouseClick

            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as LButton;
            if (ser == null) return;
            var iType = ser.Tag as LiuXingType;
            if (iType == null) return;
            var tag = ser.Parent.Parent.Tag as LiuXingData;
            if (tag == null) return;
            // 如果有复制的就直接复制
            if (tag.Drl != null && tag.Drl.Count > 0)
            {
                Pub.CopyThisUrl(tag.Drl);
            }
            else
            {
                // 否则执行获取连接并复制链接
                var iClass = new LiuXingType
                    {
                        Type = iType.Type,
                        Proxy = iType.Proxy,
                        Encoding = iType.Encoding,
                        Data = tag
                    };
                ListUrl.GetThisUrl(true, iClass);
            }

            #endregion
        }

        /// <summary>
        /// 点击观看按钮 - Playbtn_MouseClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Playbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region 点击观看按钮 - Playbtn_MouseClick

            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as LButton;
            if (ser == null) return;
            var iType = ser.Tag as LiuXingType;
            if (iType == null) return;
            var tag = ser.Parent.Parent.Tag as LiuXingData;
            if (tag == null) return;
            // 如果有点播资源就直接点播
            if (tag.Drl != null && tag.Drl.Count > 0)
            {
                var currentUrl = tag.Drl[tag.Drl.Count - 1];
                if (!string.IsNullOrEmpty(currentUrl))
                {
                    Pub.StartToVod(currentUrl);
                }
            }
            else
            {
                // 否则直接执行获取连接并点播
                var iClass = new LiuXingType
                    {
                        Type = iType.Type,
                        Proxy = iType.Proxy,
                        Encoding = iType.Encoding,
                        Data = tag
                    };
                ListUrl.GetThisUrl(false, iClass);
            }

            #endregion
        }
    }
}
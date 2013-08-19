using KCPlayer.Plugin.LiuXing.Helper;
using KCPlayer.Plugin.LiuXing.LiuXing;
using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.Controls
{
    public sealed class MetroForTile : HPanel
    {
        private LiuXingType Type { get; set; }
        // ReSharper disable InconsistentNaming
        private LiuXingData ITag { get; set; }
        // ReSharper restore InconsistentNaming
        public MetroForTile(LiuXingType iType)
        {
            if(iType == null) return;
            Type = iType;
            if (iType.Data == null) return;
            ITag = iType.Data;
            BackColor = PublicStatic.FontColor[1];
            Location = new System.Drawing.Point(1, 1);
            switch (iType.Type)
            {
                case LiuXingEnum.EverybodyWatch:
                {
                    Size = new System.Drawing.Size(256+30, 145);

                    new MetroForList(
                              this,
                              false,
                              "",
                              new System.Collections.Generic.List<object>
                                {
                                    "详情,Button","复制,Button","点播,Button,TI" + Helper.QualityHelper.GetHdsSign(iType.Data.HDs) + "-P"
                                },
                              null,
                              58,
                              47,
                              PublicStatic.MainColor[PublicStatic.MainIndex],
                              PublicStatic.FontColor[1],
                              PublicStatic.SegoeFont,
                              new System.Drawing.Point(Size.Width - 60, 0),
                              AsideList_ListItemTxt_MouseClick
                          );

                    // 影片海报
                    if (iType.Img != null)
                    {
                        try
                        {
                            var imageview = new EPicBox
                            {
                                Size = new System.Drawing.Size(256, 145),
                                BackColor = PublicStatic.FontColor[1],
                                Image = iType.Img,
                                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                            };
                            Controls.Add(imageview);
                            var tempname = iType.Data.Name;
                            if (tempname.Length > 25)
                            {
                                tempname = tempname.Substring(0, 25);
                            }
                            new HDarge(
                                imageview,
                                tempname,
                                new System.Drawing.Font(PublicStatic.SegoeFont, 16F),
                                new System.Drawing.Size(Size.Width - 57, 40),
                                new System.Drawing.Point(0, Size.Height-40),
                                
                                //PublicStatic.MainColor[PublicStatic.MainIndex],
                                //System.Drawing.Color.FromArgb(48, 48, 48),
                                PublicStatic.FontColor[1],
                                System.Drawing.Color.FromArgb(48, 0, 0, 0),
                                System.Drawing.ContentAlignment.MiddleCenter,
                                System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                                );
                            //imageview.MouseClick += imageview_MouseClick;
                        }
                        // ReSharper disable EmptyGeneralCatchClause
                        catch
                        // ReSharper restore EmptyGeneralCatchClause
                        {
                        }
                    }

                }
                break;
                case LiuXingEnum.DyfmSearchItem:
                case LiuXingEnum.PiaoHuaSearchItem:
                case LiuXingEnum.TorrentKittySearchItem:
                case LiuXingEnum.XunboSearchItem:
                case LiuXingEnum.YYetSearchItem:
                case LiuXingEnum.ZhangYuSearchItem:
                {
                    Size = new System.Drawing.Size(435*2, 40);
                    var tempname = iType.Data.Name;
                    if (tempname.Length > 25)
                    {
                        tempname = tempname.Substring(0, 25);
                    }
                    new MetroForList(
                              this,
                              true,
                              tempname,
                              null,
                              null,
                              Size.Width - 102*3-1,
                              38,
                              PublicStatic.MainColor[PublicStatic.MainIndex],
                              PublicStatic.FontColor[1],
                              PublicStatic.SegoeFont,
                              new System.Drawing.Point(0, 0),
                              AsideList_ListItemTxt_MouseClick
                          );
                    new MetroForList(
                               this,
                               true,
                               "",
                               new System.Collections.Generic.List<object>
                                {
                                    "详情,Button","复制,Button","点播,Button,TI" + Helper.QualityHelper.GetHdsSign(iType.Data.HDs) + "-P"
                                },
                               null,
                               101,
                               38,
                               PublicStatic.MainColor[PublicStatic.MainIndex],
                               PublicStatic.FontColor[1],
                               PublicStatic.SegoeFont,
                               new System.Drawing.Point(Size.Width - 102 * 3-1, 0),
                               AsideList_ListItemTxt_MouseClick
                           );
                }
                break;
                default:
                {
                    Size = new System.Drawing.Size(435, 210);
                    // 影片得分
                    if (!string.IsNullOrEmpty(iType.Data.Cos))
                    {
                        new HDarge(
                            this,
                            iType.Data.Cos,
                            new System.Drawing.Font(PublicStatic.SegoeFont, 12F),
                            new System.Drawing.Size(40, 25),
                            new System.Drawing.Point(-2, 44 + 24 + 45 + 25 + 20 - 135),
                            PublicStatic.FontColor[1], PublicStatic.MainColor[PublicStatic.MainIndex],
                            System.Drawing.ContentAlignment.MiddleCenter,
                            System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                            );
                    }

                    // 影片海报
                    if (iType.Img != null)
                    {
                        try
                        {
                            var imageview = new EPicBox()
                            {
                                Size = new System.Drawing.Size(150, 210),
                                BackColor = PublicStatic.FontColor[1],
                                Image = iType.Img,
                                SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                            };
                            Controls.Add(imageview);
                            //imageview.MouseClick += imageview_MouseClick;
                        }
                        // ReSharper disable EmptyGeneralCatchClause
                        catch
                        // ReSharper restore EmptyGeneralCatchClause
                        {
                        }
                    }

                    // 影片标题
                    if (!string.IsNullOrEmpty(iType.Data.Name))
                    {
                        var tempname = iType.Data.Name;
                        if (tempname.Contains("/"))
                        {
                            tempname = tempname.Split("/".ToCharArray())[0];
                        }
                        new HDarge(
                           this,
                           tempname,
                           new System.Drawing.Font(PublicStatic.SegoeFont, 22F),
                           new System.Drawing.Size(Size.Width - 150, 60),
                           new System.Drawing.Point(150, 0),
                           PublicStatic.MainColor[PublicStatic.MainIndex],
                            //System.Drawing.Color.FromArgb(48, 48, 48),
                           PublicStatic.FontColor[1],
                           System.Drawing.ContentAlignment.MiddleCenter,
                           System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                           ).Controls.Add(new HDarge(
                           this,
                           "",
                           new System.Drawing.Font(PublicStatic.SegoeFont, 14F),
                           new System.Drawing.Size(Size.Width - 150 - 16, 1),
                           new System.Drawing.Point(8, 59),
                           System.Drawing.Color.Transparent,
                           PublicStatic.MainColor[PublicStatic.MainIndex],
                           System.Drawing.ContentAlignment.MiddleLeft,
                           System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                           ));
                    }

                    // 影片演员
                    if (!string.IsNullOrEmpty(iType.Data.Car))
                    {
                        new HDarge(
                            this,
                            "主演：" + iType.Data.Car,
                            new System.Drawing.Font(PublicStatic.SegoeFont, 12F),
                            new System.Drawing.Size(Size.Width - 150 - 16, 50),
                            new System.Drawing.Point(150 + 8, 45 + 25),
                            PublicStatic.FontColor[0],
                            //System.Drawing.Color.FromArgb(48, 48, 48),
                            System.Drawing.Color.Transparent,
                            System.Drawing.ContentAlignment.MiddleLeft,
                            System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                            );
                    }

                    // 影片年代
                    if (!string.IsNullOrEmpty(iType.Data.Tim))
                    {
                        new HDarge(
                            this,
                            "年代：" + iType.Data.Tim,
                            new System.Drawing.Font(PublicStatic.SegoeFont, 12F),
                            new System.Drawing.Size((Size.Width - 150 - 16) / 2, 25),
                            new System.Drawing.Point(150 + 8, 45 + 25 + 45),
                            PublicStatic.FontColor[0],
                            //System.Drawing.Color.FromArgb(48, 48, 48),
                            System.Drawing.Color.Transparent,
                            System.Drawing.ContentAlignment.MiddleLeft,
                            System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                            );
                    }

                    // 影片地区
                    if (!string.IsNullOrEmpty(iType.Data.Loc))
                    {
                        new HDarge(
                            this,
                            "地区：" + iType.Data.Loc,
                            new System.Drawing.Font(PublicStatic.SegoeFont, 12F),
                            new System.Drawing.Size((Size.Width - 150 - 16) / 2, 25),
                            new System.Drawing.Point(150 + 8 + (Size.Width - 150 - 16) / 2, 45 + 25 + 45),
                            PublicStatic.FontColor[0],
                            //System.Drawing.Color.FromArgb(48, 48, 48),
                            System.Drawing.Color.Transparent,
                            System.Drawing.ContentAlignment.MiddleLeft,
                            System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                            );
                    }

                    // 影片类型
                    if (!string.IsNullOrEmpty(iType.Data.Typ))
                    {
                        new HDarge(
                            this,
                            "类型：" + iType.Data.Typ,
                            new System.Drawing.Font(PublicStatic.SegoeFont, 12F),
                            new System.Drawing.Size((Size.Width - 150 - 16) / 2, 25),
                            new System.Drawing.Point(150 + 8, 45 + 25 + 45 + 25),
                            PublicStatic.FontColor[0],
                            //System.Drawing.Color.FromArgb(48, 48, 48),
                            System.Drawing.Color.Transparent,
                            System.Drawing.ContentAlignment.MiddleLeft,
                            System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                            );
                    }

                    // 影片更新
                    if (!string.IsNullOrEmpty(iType.Data.Upt))
                    {
                        new HDarge(
                            this,
                            "更新：" + iType.Data.Upt,
                            new System.Drawing.Font(PublicStatic.SegoeFont, 12F),
                            new System.Drawing.Size((Size.Width - 150 - 16) / 2, 25),
                            new System.Drawing.Point(150 + 8 + (Size.Width - 150 - 16) / 2, 45 + 25 + 45 + 25),
                            PublicStatic.FontColor[0],
                            //System.Drawing.Color.FromArgb(48, 48, 48),
                            System.Drawing.Color.Transparent,
                            System.Drawing.ContentAlignment.MiddleLeft,
                            System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left
                            );
                    }

                    // 影片点播
                    if (!string.IsNullOrEmpty(iType.Data.HDs))
                    {
                        // iType.Data.HDs
                        new MetroForList(
                               this,
                               true,
                               "",
                               new System.Collections.Generic.List<object>
                            {
                                "详情,Button","复制,Button","点播,Button,TI" + Helper.QualityHelper.GetHdsSign(iType.Data.HDs) + "-P"
                            },
                               null,
                               (Size.Width - 150 - 8) / 3,
                               31,
                               PublicStatic.MainColor[PublicStatic.MainIndex],
                               PublicStatic.FontColor[1],
                               PublicStatic.SegoeFont,
                               new System.Drawing.Point(152, 40 + 25 * 5 + 2 * 5 + 1),
                               AsideList_ListItemTxt_MouseClick
                           );


                        new MetroForList(
                            this,
                            true,
                            "",
                            new System.Collections.Generic.List<object>
                        {
                            "资　源,Select",
                            "简　介",
                            "评　价"
                        },
                            null,
                            138,
                            31,
                            PublicStatic.MainColor[PublicStatic.MainIndex],
                            PublicStatic.FontColor[1],
                            PublicStatic.SegoeFont,
                            new System.Drawing.Point(412, 1),
                            AsideList_ListItemTxt_MouseClick
                            );
                    }

                    // 加载面板
                    new HPanel
                    {
                        Size = new System.Drawing.Size(435 + 2, 210 + 2),
                        BackColor = PublicStatic.FontColor[1]
                    }.Controls.Add(this);
                }
                break;
            }





            // 滚轮聚焦
            if (PublicStatic.SearchBox.Focused)
            {
                PublicStatic.LiuXingCon.Invoke(
                new System.Windows.Forms.MethodInvoker
                    (() => PublicStatic.LiuXingCon.Focus()));
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsideList_ListItemTxt_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region 侧面菜单点击事件

            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser != null)
            {
                if (@"详情".Contains(ser.Text) )
                {
                    AutoCloseDlg.ShowMessageBoxTimeout(@"主人要偶晚点出来", "^_^", System.Windows.Forms.MessageBoxButtons.OK, 1000);
                    return;
                    //if (fatherMetroForTile.Size.Width >= 412 * 2)
                    //{
                    //    fatherMetroForTile.Size = new System.Drawing.Size(412*1, 210);
                    //}
                    //else
                    //{
                    //    fatherMetroForTile.Size = new System.Drawing.Size(412*2+6, 210);
                    //}
                }
                if (@"复制".Contains(ser.Text))
                {
                    CopyBtnAllVodPath();
                }
                if (@"点播".Contains(ser.Text))
                {
                    PlayBtnBestVodPath();
                }
            }

            #endregion
        }

        /// <summary>
        /// 操作面板 - 链接
        /// </summary>
        private void CopyBtnAllVodPath()
        {
            #region 操作面板 - 链接
            if (Type == null || ITag == null) return;
            // 如果有复制的就直接复制
            if (ITag.Drl != null && ITag.Drl.Count > 0)
            {
                VodCopyHelper.CopyThisUrl(ITag.Drl);

            }
            else
            {
                // 否则执行获取连接并复制链接
                var iClass = new LiuXingType
                {
                    Type = Type.Type,
                    Proxy = Type.Proxy,
                    Encoding = Type.Encoding,
                    Data = ITag
                };
                ListUrl.GetThisUrl(true, iClass);
            } 
            #endregion
        }

       
        /// <summary>
        /// 操作面板 - 点播
        /// </summary>
        private void PlayBtnBestVodPath()
        {
            #region 操作面板 - 点播
            if (Type == null || ITag == null) return;
            // 如果有点播资源就直接点播
            if (ITag.Drl != null && ITag.Drl.Count > 0)
            {
                VodCopyHelper.StartToVod(ITag.Drl, ITag);
            }
            else
            {
                // 否则直接执行获取连接并点播
                var iClass = new LiuXingType
                {
                    Type = Type.Type,
                    Proxy = Type.Proxy,
                    Encoding = Type.Encoding,
                    Data = ITag
                };
                ListUrl.GetThisUrl(false, iClass);
            } 

            #endregion
        }
    }
}
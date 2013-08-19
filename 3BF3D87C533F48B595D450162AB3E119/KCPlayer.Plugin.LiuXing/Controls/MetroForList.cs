using KCPlayer.Plugin.LiuXing.Helper;

namespace KCPlayer.Plugin.LiuXing.Controls
{
    public sealed class MetroForList : HPanel
    {
        public System.Collections.Generic.List<object> ListItemTxts { get; set; }
        public System.Collections.Generic.List<object> ListItemTips { get; set; }

        public string TitleDesc { get; set; }
        private int WidthNum { get; set; }
        private int HeightIndex { get; set; }
        private int HeightNum { get; set; }
        private int SmallStep { get; set; }
        private int SmallWidth { get; set; }
        private int SmallHeight { get; set; }
        private bool IsHorizontal { get; set; }
        private bool IsCanEdit { get; set; }
        private bool IsButton { get; set; }
        private bool IsImage { get; set; }
        private bool IsHideTitle { get; set; }
        private bool IsTransparent { get; set; }
        private bool IsTable { get; set; }
        private System.Drawing.Color IforeColor { get; set; }
        private System.Drawing.Color IbackColor { get; set; }
        private string FontFamily { get; set; }
        private System.Windows.Forms.MouseEventHandler ListItemMouseClick { get; set; }

        public MetroForList(
            System.Windows.Forms.Control fatherControl, // 父容器
            bool isHorizontal,  // True为横向，False是纵向
            string titleDesc,   // 抬头说明
            System.Collections.Generic.List<object> listItemTxts,
            System.Collections.Generic.List<object> listItemTips,
            int smallWidth,     // 每个小单元的宽度
            int smallHeight,    // 每个小单元的高度
            System.Drawing.Color foreColor,    // 小单元字体颜色
            System.Drawing.Color backColor,    // 小单元背景颜色
            string fontFamily,
            System.Drawing.Point locationPoint,    // 整体位置
            System.Windows.Forms.MouseEventHandler listItemMouseClick   // 小单元点击事件
            )
        {
            #region MetroForList
            Location = locationPoint;
            IforeColor =  foreColor;
            IbackColor = backColor;
            TitleDesc = titleDesc;
            SmallWidth = smallWidth;
            SmallHeight = smallHeight;
            IsHorizontal = isHorizontal;
            FontFamily = fontFamily;
            ListItemMouseClick = listItemMouseClick;
            SmallStep = 1;
            if (titleDesc.Contains("Edit"))
            {
                IsCanEdit = true;
                titleDesc = titleDesc.Replace("Edit", "");
            }
            if (titleDesc.Contains("Button"))
            {
                IsButton = true;
                titleDesc = titleDesc.Replace("Button", "");
            }
            if (titleDesc.Contains("Image"))
            {
                IsImage = true;
                titleDesc = titleDesc.Replace("Image", "");
            }
            if (titleDesc.Contains("Hide"))
            {
                IsHideTitle = true;
                titleDesc = titleDesc.Replace("Hide", "");
            }
            if (titleDesc.Contains("Table"))
            {
                IsTable = true;
                titleDesc = titleDesc.Replace("Table", "");
            }
            if (titleDesc.Contains("Transparent"))
            {
                IsTransparent = true;
                titleDesc = titleDesc.Replace("Transparent", "");
            }
            if (titleDesc.Contains(","))
            {
                var splittitle = titleDesc.Split(',');
                if (splittitle.Length >= 1)
                {
                    TitleDesc = splittitle[0];
                }
                if (splittitle.Length >= 2)
                {
                    if (IsTable)
                    {
                        WidthNum = ToInt(splittitle[1]);
                    }
                    else
                    {
                        SmallStep = ToInt(splittitle[1]);
                    }
                }
                if (SmallStep == -1)
                {
                    SmallStep = 1;
                }
            }
            // 如果要求隐藏标题
            if (IsHideTitle)
            {
                TitleDesc = null;
            }
            if (!IsEmptyObjectList(listItemTxts))
            {
                ListItemTxts = listItemTxts;
            }
            if (!IsEmptyObjectList(listItemTips))
            {
                ListItemTips = listItemTips;
            }
            // 刷新
            UpdateListItem();
            BackColor = IsTransparent ? System.Drawing.Color.Transparent : IforeColor;
            fatherControl.Controls.Add(this);
            #endregion
        }
        /// <summary>
        /// List -> IsEmptyObjectList
        /// </summary>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static bool IsEmptyObjectList(System.Collections.Generic.List<object> iList)
        {
            #region List -> IsEmptyObjectList
            return iList == null || iList.Count <= 0;
            #endregion
        }
        /// <summary>
        /// List -> IsEmptyList
        /// </summary>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static bool IsEmptyList(System.Collections.Generic.List<string> iList)
        {
            #region List -> IsEmptyList

            return iList == null || iList.Count <= 0;

            #endregion
        }
        /// <summary>
        /// String -> Int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(string s)
        {
            #region String -> Int
            int a;
            if (int.TryParse(s, out a))
            {
                return a;
            }
            return -1;
            #endregion
        }
        /// <summary>
        /// String -> Check Value -> IsNullOrEmptyOrSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrSpace(string value)
        {
            #region String -> Check Value -> IsNullOrEmptyOrSpace

            return string.IsNullOrEmpty(value);

            #endregion
        }
        /// <summary>
        /// 刷新列表界面
        /// </summary>
        public void UpdateListItem()
        {
            #region 刷新列表界面
            Controls.Clear();
            if (IsTable && ListItemTxts != null)
            {
                var listItemTxtsYu = ListItemTxts.Count % WidthNum;
                var listItemTxtsBu = 0;
                if (listItemTxtsYu != 0)
                {
                    listItemTxtsBu = WidthNum - listItemTxtsYu;
                    for (var i = 0; i < listItemTxtsBu; i++)
                    {
                        ListItemTxts.Add("Empty");
                    }
                }
                HeightNum = (ListItemTxts.Count + listItemTxtsBu) / WidthNum;
                HeightIndex = 1;
            }
            var smallCount = 0;
            if (ListItemTxts != null)
            {
                // 所有小单元的总数
                smallCount = ListItemTxts.Count;
            }

            // 是否有抬头标题说明
            var hastitleDesc = false;
            // 是否自定义了头部面积
            if (!IsNullOrEmptyOrSpace(TitleDesc))
            {
                smallCount++;
                hastitleDesc = true;
            }
            if (smallCount == 0)
            {
                Size = new System.Drawing.Size(0, 0);
            }
            else
            {
                // 横向
                if (IsHorizontal || IsTable)
                {
                    #region 横向
                    Size = IsTable ? new System.Drawing.Size(SmallWidth * WidthNum + WidthNum + 1, HeightNum * SmallHeight + HeightNum + 1) : new System.Drawing.Size(SmallWidth * smallCount + smallCount + 1, SmallHeight + 2);
                    for (var i = 0; i < smallCount; i++)
                    {
                        object tip = null;
                        if (hastitleDesc)
                        {
                            if (i == 0)
                            {
                                // ReSharper disable ObjectCreationAsStatement
                                new MetroForListItem(this, true, IsCanEdit, false, false, IsTable, IforeColor, IbackColor, FontFamily, TitleDesc, null, ListItemMouseClick)
                                // ReSharper restore ObjectCreationAsStatement
                                {
                                    Size = new System.Drawing.Size(SmallWidth, SmallHeight),
                                    Location = new System.Drawing.Point(1 * (i + 1) + SmallWidth * i, 1)
                                };
                            }
                            else
                            {
                                if (ListItemTxts != null)
                                {
                                    if (ListItemTips != null && ListItemTips.Count <= ListItemTxts.Count)
                                    {
                                        if (i - 1 <= ListItemTips.Count - 1)
                                        {
                                            tip = ListItemTips[i - 1];
                                        }
                                    }
                                    // ReSharper disable ObjectCreationAsStatement
                                    new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i - 1], tip, ListItemMouseClick)
                                    // ReSharper restore ObjectCreationAsStatement
                                    {
                                        Size = new System.Drawing.Size(SmallWidth, SmallHeight),
                                        Location = new System.Drawing.Point(1 * (i + 1) + SmallWidth * i, 1)
                                    };
                                }
                            }
                        }
                        else
                        {
                            if (ListItemTxts != null)
                            {
                                if (ListItemTips != null && ListItemTips.Count <= ListItemTxts.Count)
                                {
                                    if (i <= ListItemTips.Count - 1)
                                    {
                                        tip = ListItemTips[i];
                                    }
                                }
                                if (IsTable)
                                {
                                    if (i < WidthNum * HeightIndex)
                                    {
                                        // ReSharper disable ObjectCreationAsStatement
                                        new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i], tip, ListItemMouseClick)
                                        // ReSharper restore ObjectCreationAsStatement
                                        {
                                            Size = new System.Drawing.Size(SmallWidth, SmallHeight),
                                            Location = new System.Drawing.Point(1 * (i - WidthNum * (HeightIndex - 1) + 1) + SmallWidth * (i - WidthNum * (HeightIndex - 1)), SmallHeight * (HeightIndex - 1) + 1 + (HeightIndex >= 2 ? HeightIndex - 1 : 0))
                                        };

                                    }
                                    else
                                    {
                                        HeightIndex++;
                                        // ReSharper disable ObjectCreationAsStatement
                                        new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i], tip, ListItemMouseClick)
                                        // ReSharper restore ObjectCreationAsStatement
                                        {
                                            Size = new System.Drawing.Size(SmallWidth, SmallHeight),
                                            Location = new System.Drawing.Point(1 * (i - WidthNum * (HeightIndex - 1) + 1) + SmallWidth * (i - WidthNum * (HeightIndex - 1)), SmallHeight * (HeightIndex - 1) + 1 + (HeightIndex >= 2 ? HeightIndex - 1 : 0))
                                        };
                                    }
                                }
                                else
                                {
                                    // ReSharper disable ObjectCreationAsStatement
                                    new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i], tip, ListItemMouseClick)
                                    // ReSharper restore ObjectCreationAsStatement
                                    {
                                        Size = new System.Drawing.Size(SmallWidth, SmallHeight),
                                        Location = new System.Drawing.Point(1 * (i + 1) + SmallWidth * i, 1)
                                    };
                                }
                            }
                        }
                    }
                    #endregion
                }
                // 纵向
                else
                {
                    #region 纵向
                    Size = new System.Drawing.Size(SmallWidth + 2, SmallHeight * smallCount + smallCount + 1 + SmallHeight * (SmallStep - 1));
                    for (var i = 0; i < smallCount; i++)
                    {
                        object tip = null;
                        if (hastitleDesc)
                        {
                            if (i == 0)
                            {
                                // ReSharper disable ObjectCreationAsStatement
                                new MetroForListItem(this, true, IsCanEdit, false, false, IsTable, IforeColor, IbackColor, FontFamily, TitleDesc, null, ListItemMouseClick)
                                // ReSharper restore ObjectCreationAsStatement
                                {
                                    Size = new System.Drawing.Size(SmallWidth, SmallHeight * SmallStep),
                                    Location = new System.Drawing.Point(1, 1 * (i + 1) + SmallHeight * i)
                                };
                            }
                            else
                            {
                                if (ListItemTxts != null)
                                {
                                    if (ListItemTips != null && ListItemTips.Count <= ListItemTxts.Count)
                                    {
                                        if (i - 1 <= ListItemTips.Count - 1)
                                        {
                                            tip = ListItemTips[i - 1];
                                        }
                                    }
                                    // ReSharper disable ObjectCreationAsStatement
                                    new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i - 1], tip, ListItemMouseClick)
                                    // ReSharper restore ObjectCreationAsStatement
                                    {
                                        Size = new System.Drawing.Size(SmallWidth, SmallHeight),
                                        Location = new System.Drawing.Point(1, 1 * (i + 1) + SmallHeight * i + SmallHeight * (SmallStep - 1))
                                    };
                                }
                            }
                        }
                        else
                        {
                            if (ListItemTxts != null)
                            {
                                if (ListItemTips != null && ListItemTips.Count <= ListItemTxts.Count)
                                {
                                    if (i <= ListItemTips.Count - 1)
                                    {
                                        tip = ListItemTips[i];
                                    }
                                }
                                // ReSharper disable ObjectCreationAsStatement
                                new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i], tip, ListItemMouseClick)
                                // ReSharper restore ObjectCreationAsStatement
                                {
                                    Size = new System.Drawing.Size(SmallWidth, SmallHeight),
                                    Location = new System.Drawing.Point(1, 1 * (i + 1) + SmallHeight * i)
                                };
                            }
                        }
                    }
                    #endregion
                }
            }

            #endregion
        }
    }

    public sealed class MetroForListItem : HPanel
    {
        private ELabel ListItemTxt { get; set; }
        public override System.Drawing.Color ForeColor { get; set; }
        public override System.Drawing.Color BackColor { get; set; }
        private string FontFamily { get; set; }
        private bool IsTitleDesc { get; set; }
        private bool IsCanEdit { get; set; }
        private bool IsButton { get; set; }
        private bool IsLarge { get; set; }
        private bool IsSelect { get; set; }
        private bool IsImage { get; set; }
        private bool IsTable { get; set; }
        private bool IsEmpty { get; set; }
        private InputSearch ModifyTitleInput { get; set; }
        /// <summary>
        /// String -> Check Value -> IsNullOrEmptyOrSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrSpace(string value)
        {
            #region String -> Check Value -> IsNullOrEmptyOrSpace

            return string.IsNullOrEmpty(value);

            #endregion
        }
        /// <summary>
        /// String -> Check Path -> IsExistFile
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExistFile(string path)
        {
            #region String -> Check Path -> IsExistFile

            return System.IO.File.Exists(path);

            #endregion
        }
        /// <summary>
        /// String -> Image
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static System.Drawing.Image LoadLocalImage(string s)
        {
            #region String -> Image

            if (IsExistFile(s))
            {
                try
                {
                    var img = System.Drawing.Image.FromFile(s);
                    System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
                    img.Dispose();
                    return bmp;
                }
                catch
                {
                    return null;
                }
            }
            return null;

            #endregion
        }
        /// <summary>
        /// 文字方向 上下左右居中
        /// </summary>
        public static System.Drawing.ContentAlignment AlignMiddleCenter = System.Drawing.ContentAlignment.MiddleCenter;

        /// <summary>
        /// 文字方向 顶部居中
        /// </summary>
        public static System.Drawing.ContentAlignment AlignTopCenter = System.Drawing.ContentAlignment.TopCenter;
        /// <summary>
        /// Anchor 底部右边
        /// </summary>
        public static System.Windows.Forms.AnchorStyles AnchorBottomRight = System.Windows.Forms.AnchorStyles.Bottom |
                                                                            System.Windows.Forms.AnchorStyles.Right;

        public MetroForListItem(
            System.Windows.Forms.Control fatherControl,
            bool isTitleDesc,
            bool isCanEdit,
            bool isButton,
            bool isImage,
            bool isTable,
            System.Drawing.Color foreColor,
            System.Drawing.Color backColor,
            string fontFamily,
            object listItemDescTxt,
            object listItemDescTip,
            System.Windows.Forms.MouseEventHandler listItemMouseClick
            )
        {
            #region MetroForListItem
            ForeColor = foreColor;
            BackColor = backColor;
            IsTitleDesc = isTitleDesc;
            IsCanEdit = isCanEdit;
            IsButton = isButton;
            IsImage = isImage;
            IsTable = isTable;
            FontFamily = fontFamily;

            var descTxt = listItemDescTxt.ToString();
            if (descTxt.Contains("Button"))
            {
                IsButton = true;
                descTxt = descTxt.Replace("Button", "");
            }
            if (descTxt.Contains("Select"))
            {
                IsSelect = true;
                descTxt = descTxt.Replace("Select", "");
            }
            if (descTxt.Contains("Large"))
            {
                IsLarge = true;
                descTxt = descTxt.Replace("Large", "");
            }
            if (descTxt.Contains("Image"))
            {
                IsImage = true;
                descTxt = descTxt.Replace("Image", "");
            }
            if (descTxt.Contains("TI") && descTxt.Contains("-P"))
            {
                listItemDescTip = StringRegexHelper.GetSingle(descTxt,"TI", "-P");
                descTxt = descTxt.Replace("TI" + listItemDescTip + "-P", "");
            }
            descTxt = descTxt.Replace(",", "");
            if (IsImage)
            {
                Controls.Add(new EPicBox
                {
                    Image = LoadLocalImage(descTxt),
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                    Dock = System.Windows.Forms.DockStyle.Fill
                });
            }
            if (descTxt == "Empty")
            {
                IsEmpty = true;
            }
            else
            {
                ListItemTxt = new ELabel
                {
                    Text = descTxt,
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    Font =
                        isTitleDesc
                            ? new System.Drawing.Font(fontFamily, 15F)
                            : new System.Drawing.Font(fontFamily, 12.5F),
                    TextAlign = AlignMiddleCenter,
                    ForeColor = IsSelect ? backColor : foreColor,
                    Cursor = isTitleDesc ? System.Windows.Forms.Cursors.Default : System.Windows.Forms.Cursors.Hand,
                    BackColor = IsSelect ? foreColor : backColor
                };
                Controls.Add(ListItemTxt);
                if (IsTable)
                {
                    ListItemTxt.Tag = listItemDescTip;
                }
                ListItemTxt.MouseClick += listItemMouseClick;

                if (!IsTable && listItemDescTip != null)
                {
                    if (!isButton)
                    {
                        ListItemTxt.Controls.Add(new ELabel
                        {
                            Text = listItemDescTip.ToString(),
                            Size = new System.Drawing.Size(21, 13),
                            Location = new System.Drawing.Point(Width - 21, ListItemTxt.Height - 13),
                            Anchor = AnchorBottomRight,
                            TextAlign = AlignTopCenter,
                            BackColor = foreColor,
                            ForeColor = backColor,
                            Font = new System.Drawing.Font(fontFamily, 6.5F)
                        });
                    }
                }
                else
                {
                    ListItemTxt.MouseDown += ListItemTxt_MouseDown;
                    if (IsButton)
                    {
                        ListItemTxt.MouseUp += ListItemTxt_MouseUp;
                    }
                }
                if (IsCanEdit)
                {
                    ListItemTxt.DoubleClick += ListItemTxt_DoubleClick;
                }
            }
            fatherControl.Controls.Add(this);
            #endregion
        }

        #region Action

        private void ListItemTxt_DoubleClick(object sender, System.EventArgs e)
        {
            var ser = sender as ELabel;
            if (ser != null)
            {
                var father = ser.Parent as MetroForListItem;
                ser.Visible = false;
                if (father != null)
                {
                    if (ModifyTitleInput != null)
                    {
                        ModifyTitleInput.Parent.Visible = true;
                    }
                    else
                    {
                        ModifyTitleInput = new InputSearch
                         (
                         father,
                         1,
                         1,
                         "修改",
                         "",
                         new System.Drawing.Font(FontFamily, 12.5F),
                         new System.Drawing.Size(60, 32),
                         new System.Drawing.Font(FontFamily, 12F),
                         new System.Drawing.Size(ser.Width + 1, 32),
                         new System.Drawing.Point(-1, ser.Height / 2 - 15),
                         BackColor,
                         ForeColor,
                         ForeColor,
                         ForeColor,
                         BackColor,
                         BackColor,
                         System.Drawing.Color.FromArgb(75, 75, 75),
                         System.Drawing.Color.FromArgb(25, 25, 25),
                         Searchbtn_MouseClick,
                         SearchBox_KeyDown,
                         System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top
                         );
                    }
                    // 
                    ModifyTitleInput.Text = ListItemTxt.Text;
                }
            }

        }

        private void Searchbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as LButton;
            if (ser != null)
            {
                var father = ser.Parent.Parent as LPanel;
                if (father != null)
                {
                    var fatherfather = father.Parent.Parent.Parent as MetroForList;
                    if (fatherfather != null)
                    {
                        fatherfather.TitleDesc = ListItemTxt.Text = ModifyTitleInput.Text;
                        father.Visible = false;
                        ListItemTxt.Visible = true;
                    }
                }
            }
        }

        private static void SearchBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // 接受回车搜索
            if (e.KeyCode != System.Windows.Forms.Keys.Enter) return;
        }

        private void ListItemTxt_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = sender as ELabel;
            if (ser == null) return;
            // 使自己高亮
            ser.ForeColor = ForeColor;
            ser.BackColor = BackColor;
        }

        private void ListItemTxt_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (IsTitleDesc)
            {
                if (!IsCanEdit)
                {
                    ((System.Windows.Forms.Control)sender).Capture = false;
                    var msg = System.Windows.Forms.Message.Create(MainInterFace.Owner.Parent.Handle, 0x00A1, (System.IntPtr)0x002,
                                                                  System.IntPtr.Zero);
                    WndProc(ref msg);
                }
            }
            else
            {
                var ser = sender as ELabel;
                if (ser == null) return;
                if (!IsButton)
                {
                    // 清空别人
                    foreach (MetroForListItem listItem in ser.Parent.Parent.Controls)
                    {
                        foreach (ELabel eLabel in listItem.Controls)
                        {
                            eLabel.ForeColor = listItem.ForeColor;
                            eLabel.BackColor = listItem.BackColor;
                        }
                    }
                }
                // 使自己高亮
                ser.ForeColor = BackColor;
                ser.BackColor = ForeColor;
            }

        }
        #endregion
    }

}

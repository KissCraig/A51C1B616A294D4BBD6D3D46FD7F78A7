
using A51C.Control.Base;
using A51C.Control.Fase;
using A51C.Control.Gase;

namespace A51C.Control.Tase
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
        private System.Drawing.FontFamily FontFamily { get; set; }
        private System.Windows.Forms.MouseEventHandler ListItemMouseClick { get; set; }
        private System.Windows.Forms.MouseEventHandler ListItemTagMouseClick { get; set; }
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
            System.Drawing.FontFamily fontFamily,
            System.Drawing.Point locationPoint,    // 整体位置
            System.Windows.Forms.MouseEventHandler listItemMouseClick,   // 小单元点击事件
            System.Windows.Forms.MouseEventHandler listItemTagMouseClick   // 小单元点击事件
            )
        {
            #region MetroForList
            Location = locationPoint;
            IforeColor =  foreColor;
            IbackColor = backColor;
            SmallWidth = smallWidth;
            SmallHeight = smallHeight;
            TitleDesc = titleDesc;
            IsHorizontal = isHorizontal;
            FontFamily = fontFamily;
            ListItemMouseClick = listItemMouseClick;
            ListItemTagMouseClick = listItemTagMouseClick;
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
                        WidthNum = splittitle[1].ToInt();
                    }
                    else
                    {
                        SmallStep = splittitle[1].ToInt();
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
            if (!listItemTxts.IsEmptyObjectList())
            {
                ListItemTxts = listItemTxts;
            }
            if (!listItemTips.IsEmptyObjectList())
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
        /// 刷新列表界面
        /// </summary>
        public void UpdateListItem()
        {
            #region 刷新列表界面
            Controls.Clear();
            if (IsTable && ListItemTxts!= null)
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
            if (!TitleDesc.IsNullOrEmptyOrSpace())
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
                                new MetroForListItem(this, true, IsCanEdit, false, false, IsTable, IforeColor, IbackColor, FontFamily, TitleDesc, null, ListItemMouseClick,ListItemTagMouseClick)
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
                                    new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i - 1], tip, ListItemMouseClick, ListItemTagMouseClick)
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
                                        new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i], tip, ListItemMouseClick, ListItemTagMouseClick)
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
                                        new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i], tip, ListItemMouseClick, ListItemTagMouseClick)
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
                                    new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i], tip, ListItemMouseClick, ListItemTagMouseClick)
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
                    #region // 纵向
                    Size = new System.Drawing.Size(SmallWidth + 2, SmallHeight * smallCount + smallCount + 1 + SmallHeight * (SmallStep - 1));
                    for (var i = 0; i < smallCount; i++)
                    {
                        object tip = null;
                        if (hastitleDesc)
                        {
                            if (i == 0)
                            {
                                // ReSharper disable ObjectCreationAsStatement
                                new MetroForListItem(this, true, IsCanEdit, false, false, IsTable, IforeColor, IbackColor, FontFamily, TitleDesc, null, ListItemMouseClick, ListItemTagMouseClick)
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
                                    new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i - 1], tip, ListItemMouseClick, ListItemTagMouseClick)
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
                                new MetroForListItem(this, false, IsCanEdit, IsButton, IsImage, IsTable, IforeColor, IbackColor, FontFamily, ListItemTxts[i], tip, ListItemMouseClick, ListItemTagMouseClick)
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
        private System.Drawing.FontFamily FontFamily { get; set; }
        private bool IsTitleDesc { get; set; }
        private bool IsCanEdit { get; set; }
        private bool IsButton { get; set; }
        private bool IsSelect { get; set; }
        private bool IsLarge { get; set; }
        private bool IsImage { get; set; }
        private bool IsTable { get; set; }
        private bool IsEmpty { get; set; }
        // private bool IsActived { get; set; }
        private bool IsActived { get; set; }
        private InputSearch ModifyTitleInput { get; set; }

        private bool IsTag { get; set; }

        public MetroForListItem(
            System.Windows.Forms.Control fatherControl,
            bool isTitleDesc,
            bool isCanEdit,
            bool isButton,
            bool isImage,
            bool isTable,
            System.Drawing.Color foreColor,
            System.Drawing.Color backColor,
            System.Drawing.FontFamily fontFamily,
            object listItemDescTxt,
            object listItemDescTip,
            System.Windows.Forms.MouseEventHandler listItemMouseClick,   // 小单元点击事件
            System.Windows.Forms.MouseEventHandler listItemTagMouseClick   // 小单元点击事件
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
            if(listItemDescTxt==null) return;
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
            if (descTxt.Contains("Desc"))
            {
                IsTitleDesc = true;
                descTxt = descTxt.Replace("Desc", "");
            }
            if (descTxt.Contains("Tag"))
            {
                IsTag = true;
                descTxt = descTxt.Replace("Tag", "");
            }
            descTxt = descTxt.Replace(",", "");
            if (IsImage)
            {
                Controls.Add(new EPicBox
                    {
                        Image = descTxt.LoadLocalImage(),
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
                    Text = IsEmpty ? "" : descTxt,
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    Font = IsLarge ? new System.Drawing.Font(fontFamily, 15F) : IsTitleDesc ? new System.Drawing.Font(fontFamily, 15F) : new System.Drawing.Font(fontFamily, 12.5F),
                    TextAlign = BaseAlign.AlignMiddleCenter,
                    ForeColor = IsSelect ? backColor : foreColor,
                    Cursor = IsTitleDesc ? System.Windows.Forms.Cursors.Default : System.Windows.Forms.Cursors.Hand,
                    BackColor = IsSelect ? foreColor : backColor
                };
                Controls.Add(ListItemTxt);
                if (IsTable)
                {
                    ListItemTxt.Tag = listItemDescTip;
                    Tag = listItemDescTip;
                }
                ListItemTxt.MouseClick += listItemMouseClick;
                if (IsTag)
                {
                    listItemDescTip = "X";
                    ListItemTxt.MouseDown += ListItemTxt_MouseDown;
                }
                if (!IsTable && listItemDescTip != null)
                {
                    if (!isButton)
                    {
                        var tag = new ELabel
                        {
                            Text = listItemDescTip.ToString(),
                            Size = new System.Drawing.Size(30, 19),
                            Location = new System.Drawing.Point(Width - 30, IsTag ? ListItemTxt.Height - 49 : ListItemTxt.Height - 19),
                            Anchor = BaseAnchor.AnchorBottomRight,
                            TextAlign = BaseAlign.AlignTopCenter,
                            BackColor = IsTag ? backColor : foreColor,
                            ForeColor = IsTag ? foreColor : backColor,
                            Font = IsTag ? new System.Drawing.Font("微软雅黑", 10.5F) : new System.Drawing.Font(fontFamily, 10.5F)
                        };
                        ListItemTxt.Controls.Add(tag);
                        if (IsTag)
                        {
                            tag.MouseClick += listItemTagMouseClick;
                        }
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
            // ReSharper disable RedundantJumpStatement
            if (e.KeyCode != System.Windows.Forms.Keys.Enter) return;
            // ReSharper restore RedundantJumpStatement
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
            if (IsTitleDesc || IsEmpty)
            {
                if (!IsCanEdit || IsEmpty)
                {
                    ((System.Windows.Forms.Control)sender).Capture = false;
                    var msg = System.Windows.Forms.Message.Create(BasePublic.Ui.Handle, 0x00A1, (System.IntPtr)0x002,
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
                        if (listItem.IsActived) continue;
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

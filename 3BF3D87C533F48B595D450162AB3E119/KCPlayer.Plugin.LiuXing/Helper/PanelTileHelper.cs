
using System.Drawing;
using System.Windows.Forms;
using KCPlayer.Plugin.LiuXing.Controls;

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class PanelTileHelper
    {
        /// <summary>
        /// 初始化主面板
        /// </summary>
        /// <returns></returns>
        public static void InitializeMainPanel()
        {
            #region 初始化主面板

            // 检查 插件全局 主承载面板
            if (PublicStatic.LiuXingPal == null) return;

            //PublicStatic.ThisHot = new EPanel
            //{
            //    Size = PublicStatic.LiuXingPal.Size,
            //    Dock = System.Windows.Forms.DockStyle.Fill,
            //    Location = new Point(0,0),
            //    BackColor = System.Drawing.Color.Black,
            //    BackgroundImage = null,
            //    BackgroundImageLayout = ImageLayout.Stretch
            //};
            //PublicStatic.LiuXingPal.Controls.Add(PublicStatic.ThisHot);

            //PublicStatic.ThisHotNext = new ELabel
            //{
            //    Text = @"再潜一下",
            //    Size = new Size(210,78),
            //    BackColor = System.Drawing.Color.FromArgb(120,0,0,0),
            //    ForeColor = System.Drawing.Color.White,
            //    Font = new System.Drawing.Font(PublicStatic.SegoeFont,14F),
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    Cursor = System.Windows.Forms.Cursors.Hand,
            //    Location = new Point(PublicStatic.ThisHot.Width - 100-210, PublicStatic.ThisHot.Height-100-78)
            //};
            //PublicStatic.ThisHot.Controls.Add(PublicStatic.ThisHotNext);
            //PublicStatic.ThisHotNext.MouseClick += ThisHotNext_MouseClick;

            // 加载顶部导航面板
            PublicStatic.LiuXingNav = new MetroForList(
                PublicStatic.LiuXingPal,
                true,
                "",
                null,
                null,
                101,
                32,
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.FontColor[1],
                PublicStatic.SegoeFont,
                new System.Drawing.Point(3, 0),
                PanelActHelper.ManageList_ListItemTxt_MouseClick
            ) {ListItemTxts = PanelActHelper.LiuXingNavForAct};
            PublicStatic.LiuXingNav.UpdateListItem();

            // 加载搜索框面板
            PublicStatic.SearchBox = new InputSearch(
                PublicStatic.LiuXingPal,
                1,
                1,
                @"六核搜索",
                "",
                new System.Drawing.Font(PublicStatic.SegoeFont, 12.5F),
                new System.Drawing.Size(101, 34),
                new System.Drawing.Font(PublicStatic.SegoeFont, 12F),
                new System.Drawing.Size(PublicStatic.LiuXingPal.Width - 6 - PublicStatic.LiuXingNav.Width, 34),
                new System.Drawing.Point(PublicStatic.LiuXingNav.Location.X + PublicStatic.LiuXingNav.Width - 1, 0),
                PublicStatic.FontColor[1],
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.FontColor[1],
                PublicStatic.FontColor[1],
                System.Drawing.Color.FromArgb(75, 75, 75),
                System.Drawing.Color.FromArgb(25, 25, 25),
                PanelActHelper.anSearchbtn_MouseClick,
                PanelActHelper.SearchBox_KeyDown,
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top
                );



            // 加载左侧导航
            PublicStatic.LiuXingCategoryType = new MetroForList(PublicStatic.LiuXingPal,
                    true,
                    "Hide",
                    null,
                    null,
                    50,
                    32,
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    PublicStatic.SegoeFont,
                    new System.Drawing.Point(3, PublicStatic.LiuXingNav.Height + 8),
                    PanelActHelper.LiuXingCategoryType_MouseClick
                ) {ListItemTxts = CategoryHelper.LiuXingCategoryForType};
            PublicStatic.LiuXingCategoryType.UpdateListItem();

            PublicStatic.LiuXingCategory = new MetroForList(
                PublicStatic.LiuXingPal,
                false,
                "Hide",
                null,
                null,
                101,
                73,
                PublicStatic.MainColor[PublicStatic.MainIndex],
                PublicStatic.FontColor[1],
                PublicStatic.SegoeFont,
                new System.Drawing.Point(3, PublicStatic.LiuXingNav.Height + 8 + 34 +8),
                PanelActHelper.LiuXingCategory_MouseClick
                ) {ListItemTxts = CategoryHelper.LiuXingCategoryForZiYuan};

            PublicStatic.LiuXingCategory.UpdateListItem();

            // 加载中间列表栏
            PublicStatic.LiuXingCon = new MetroForFly(
                PublicStatic.LiuXingPal,
                PublicStatic.LiuXingPal.Width - PublicStatic.LiuXingCategoryType.Width - 3 - 8,
                // PublicStatic.LiuXingPal.Width - PublicStatic.LiuXingCategoryType.Width - 3 - 8 - PublicStatic.LiuXingFilmType.Width - 8 + 2,
                PublicStatic.LiuXingPal.Height - PublicStatic.LiuXingNav.Height - 16 + 3,
                new System.Drawing.Point(PublicStatic.LiuXingCategoryType.Width + 3 + 8 - 2, PublicStatic.LiuXingNav.Height + 8),
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right
                )
            {
                BackColor = System.Drawing.Color.Transparent
            };

            PublicStatic.LiuXingFilmType = new MetroForList(
                    PublicStatic.LiuXingPal,
                    false,
                    "Table,3",
                    null,
                    null,
                    46,
                    30,
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    PublicStatic.SegoeFont,
                    new System.Drawing.Point(PublicStatic.LiuXingPal.Width - 46*3 -8, PublicStatic.LiuXingNav.Height + 8),
                    PanelActHelper.LiuXingCategory_MouseClick
                ) {ListItemTxts = PanelActHelper.LiuXingFilmForType};
            PublicStatic.LiuXingFilmType.UpdateListItem();

            PublicStatic.LiuXingFilmLoc = new MetroForList(
                    PublicStatic.LiuXingPal,
                    false,
                    "Table,3",
                    null,
                    null,
                    46,
                    30,
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    PublicStatic.SegoeFont,
                    new System.Drawing.Point(PublicStatic.LiuXingPal.Width - 46*3 - 8,
                                             PublicStatic.LiuXingNav.Height + 8  + PublicStatic.LiuXingFilmType.Height+8),
                    PanelActHelper.LiuXingCategory_MouseClick
                ) {ListItemTxts = PanelActHelper.LiuXingFilmForLoc};
            PublicStatic.LiuXingFilmLoc.UpdateListItem();

            PublicStatic.LiuXingFilmYear = new MetroForList(
                    PublicStatic.LiuXingPal,
                    false,
                    "Table,3",
                    null,
                    null,
                    46,
                    30,
                    PublicStatic.MainColor[PublicStatic.MainIndex],
                    PublicStatic.FontColor[1],
                    PublicStatic.SegoeFont,
                    new System.Drawing.Point(PublicStatic.LiuXingPal.Width - 46*3 - 8,
                                             PublicStatic.LiuXingNav.Height + 8  + PublicStatic.LiuXingFilmType.Height + PublicStatic.LiuXingFilmLoc.Height + 16),
                    PanelActHelper.LiuXingCategory_MouseClick
                ) {ListItemTxts = PanelActHelper.LiuXingFilmForYear};
            PublicStatic.LiuXingFilmYear.UpdateListItem();



            // 初始化界面

            #endregion
        }

        static void ThisHotNext_MouseClick(object sender, MouseEventArgs e)
        {
            PublicStatic.NowCategory = CategoryHelper.Category.电影fmHot;
            StartListHelper.StartActionOne();
        }
    }
}

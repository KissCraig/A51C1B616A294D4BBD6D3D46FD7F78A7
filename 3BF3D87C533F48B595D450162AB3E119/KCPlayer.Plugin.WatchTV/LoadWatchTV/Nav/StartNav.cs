using System.Drawing;
using System.Windows.Forms;
using KCPlayer.Plugin.WatchTV.Controls;
using KCPlayer.Plugin.WatchTV.LoadWatchTV.Data;

namespace KCPlayer.Plugin.WatchTV.LoadWatchTV.Nav
{
    public class StartNav
    {
        #region static

        private const int ItemWidth = 115;
        private const int XianDing = 30;
        private static bool ChangeUi { get; set; }

        #endregion

        #region Load

        /// <summary>
        /// 初始化并开始导航
        /// </summary>
        public static void AddLiuXingNav()
        {
            if (PublicStatic.LiuXingNav == null) return;
            // 复位所有数据
            TvList.ResetTvList();
            // 清洗管理面板
            PublicStatic.LiuXingNav.Controls.Clear();
            // 逐个加载应用
            LoadNavItems();
            // 加载管理面板按钮
            LoadFuncClicks();
        }

        /// <summary>
        /// 逐个加载应用
        /// </summary>
        private static void LoadNavItems()
        {
            // 加载应用
            var index = 0;
            var count = 0;
            foreach (var dic in PublicStatic.Dics)
            {
                for (int j = 0; j < dic.Value.Count; j++)
                {
                    // 限定显示这么多应用
                    if (count <= XianDing)
                    {
                        WatchTvData vk = dic.Value[j];
                        // 加载
                        var btn = new LButton
                            (
                            PublicStatic.LiuXingNav,
                            1,
                            vk.Name,
                            new Font(PublicStatic.SegoeFont, 12.5F),
                            new Size(115, 32),
                            new Point(8 + (101 + 8)*0, 6),
                            PublicStatic.SouSouColor[index],
                            PublicStatic.SouSouColor[index],
                            Color.FromArgb(248, 248, 248),
                            PublicStatic.SouSouColor[index],
                            PublicStatic.SouSouColor[index],
                            Color.FromArgb(248, 248, 248),
                            AnchorStyles.Left | AnchorStyles.Top
                            ) {Tag = vk};
                        btn.MouseClick += btn_MouseClick;
                        // 发现第一个应用的时候加载下
                        if (count == 0)
                        {
                            if (ChangeUi)
                            {
                                ChangeUi = false;
                            }
                            else
                            {
                                PublicStatic.NowPlayWatch = vk;
                                PublicStatic.LiuXingCon.Navigate(vk.Url);
                            }
                        }
                        // 继续下一个
                        count++;
                    }
                }
                // 控制颜色序号
                index++;
            }
        }

        /// <summary>
        /// 加载管理面板按钮
        /// </summary>
        private static void LoadFuncClicks()
        {
            // 收起和推开
            PublicStatic.AnShouQibtn = new LButton
                (
                PublicStatic.LiuXingNav,
                1,
                "收起",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(ItemWidth, 32),
                new Point(8 + (101 + 8)*7, 6),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                );
            PublicStatic.AnShouQibtn.MouseClick += anShouQibtn_MouseClick;
            PublicStatic.LiuXingNav.MouseClick += LiuXingNav_MouseClick;
            //PublicStatic.LiuXingCon.GotFocus += LiuXingCon_GotFocus;
            // 置顶和置后
            PublicStatic.AnQieDingbtn = new LButton
                (
                PublicStatic.LiuXingNav,
                1,
                @"置顶",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(ItemWidth, 32),
                new Point(8 + (101 + 8)*7, 6),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                );
            PublicStatic.AnQieDingbtn.MouseClick += AnQieDingbtn_MouseClick;

            // 手动添加
            PublicStatic.AnTianJiabtn = new LButton
                (
                PublicStatic.LiuXingNav,
                1,
                "管理",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(ItemWidth, 32),
                new Point(8 + (101 + 8)*7, 6),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                );
            PublicStatic.AnTianJiabtn.MouseClick += AnTianJiabtn_MouseClick;

            // 主题配色
            PublicStatic.AnPeiSebtn = new LButton
                (
                PublicStatic.LiuXingNav,
                1,
                PublicStatic.SouSouIndex == 0 ? @"活红" : @"清蓝",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(ItemWidth, 32),
                new Point(8 + (101 + 8)*7, 6),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                );
            PublicStatic.AnPeiSebtn.MouseClick += AnPeiSebtn_MouseClick;
        }

        #endregion

        #region 私有动作

        /// <summary>
        /// 切换到管理面板去
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AnTianJiabtn_MouseClick(object sender, MouseEventArgs e)
        {
            AddItem.AddPalItem();
        }

        /// <summary>
        /// 切换配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AnPeiSebtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks < 0) return;
            switch (PublicStatic.AnPeiSebtn.Text)
            {
                case @"清蓝":
                    PublicStatic.SouSouIndex = 0;
                    break;
                case @"活红":
                    PublicStatic.SouSouIndex = 1;
                    break;
            }
            ChangeUi = true;
            AddLiuXingNav();
        }

        /// <summary>
        /// 切换置顶和置后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AnQieDingbtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks < 0) return;
            ((Form) MainInterFace.Owner.Parent).TopMost =
                !((Form) MainInterFace.Owner.Parent).TopMost;
            PublicStatic.AnQieDingbtn.Text = ((Form) MainInterFace.Owner.Parent).TopMost
                                                 ? @"置后"
                                                 : @"置顶";
        }

        /// <summary>
        /// 点击执行应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks < 0) return;
            PublicStatic.NowPlayWatch = (WatchTvData) ((LButton) sender).Tag;
            PublicStatic.LiuXingCon.Navigate(PublicStatic.NowPlayWatch.Url);
        }

        /// <summary>
        /// 切换收缩和推开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void anShouQibtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (PublicStatic.AnShouQibtn.Text.Trim() == @"收起")
            {
                PublicStatic.LiuXingNav.Size = new Size(PublicStatic.LiuXingNav.Size.Width, 3);
                PublicStatic.AnShouQibtn.Text = @"推开";
                PublicStatic.LiuXingNav.BackColor = PublicStatic.SouSouColor[PublicStatic.SouSouIndex];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void LiuXingCon_GotFocus(object sender, System.EventArgs e)
        {
            if (PublicStatic.AnShouQibtn.Text.Trim() == @"收起")
            {
                PublicStatic.LiuXingNav.Size = new Size(PublicStatic.LiuXingNav.Size.Width, 3);
                PublicStatic.AnShouQibtn.Text = @"推开";
                PublicStatic.LiuXingNav.BackColor = PublicStatic.SouSouColor[PublicStatic.SouSouIndex];
            }
        }

        /// <summary>
        /// 切换收缩和推开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void LiuXingNav_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            if (PublicStatic.AnShouQibtn.Text.Trim() == @"推开")
            {
                PublicStatic.LiuXingNav.Size = new System.Drawing.Size(PublicStatic.LiuXingNav.Size.Width, 189);
                PublicStatic.AnShouQibtn.Text = @"收起";
                PublicStatic.LiuXingNav.BackColor = System.Drawing.Color.Transparent;
            }
        }

        #endregion
    }
}
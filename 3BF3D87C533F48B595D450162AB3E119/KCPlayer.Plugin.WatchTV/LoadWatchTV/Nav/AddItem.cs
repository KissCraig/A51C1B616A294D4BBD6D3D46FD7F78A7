using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using KCPlayer.Json;
using KCPlayer.Plugin.WatchTV.Controls;

namespace KCPlayer.Plugin.WatchTV.LoadWatchTV.Nav
{
    public class AddItem
    {
        #region Static

        private const int BtnWidth = 159;
        private const string AppNameTip = "必填，例如：KCPlayer官网";
        private const string AppUrlTip = "必填，例如：http://www.kcplayer.com";
        private const string AppWidthTip = "可选，例如：847";
        private const string AppHeightTip = "可选，例如：483";
        public static List<NavItem> UserItemLists { get; set; }
        private static LPanel AddPal { get; set; }
        private static InputBoxWithDesc AppName { get; set; }
        private static InputBoxWithDesc AppUrl { get; set; }
        private static InputBoxWithDesc AppWidth { get; set; }
        private static InputBoxWithDesc AppHeight { get; set; }
        private static LButton AppCancel { get; set; }
        private static LButton AppAddItem { get; set; }
        private static LButton AppSaveBack { get; set; }
        private static LButton AppPreview { get; set; }
        private static LButton AppDelete { get; set; }

        #endregion

        #region Load

        public static void AddPalItem()
        {
            if (PublicStatic.LiuXingNav == null) return;
            PublicStatic.LiuXingNav.Controls.Clear();

            #region 添加面板

            // 总面板
            AddPal = new LPanel
                (
                PublicStatic.LiuXingNav,
                0,
                new Size(PublicStatic.LiuXingNav.Width, PublicStatic.LiuXingNav.Height),
                new Point(0, 0),
                Color.Transparent,
                Color.Transparent, //System.Drawing.Color.FromArgb(255, 255, 255)
                AnchorStyles.Left | AnchorStyles.Right |
                AnchorStyles.Top
                );
            // 应用名字
            AppName = new InputBoxWithDesc(
                AddPal,
                1,
                2,
                @"链接命名",
                AppNameTip,
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(104, 34),
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(444, 34),
                new Point(8, 8 + (34 + 8)*0),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(220, 220, 220),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(75, 75, 75),
                Color.FromArgb(25, 25, 25),
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 1};

            // 应用链接
            AppUrl = new InputBoxWithDesc(
                AddPal,
                1,
                2,
                @"链接地址",
                AppUrlTip,
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(104, 34),
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(444, 34),
                new Point(8, 8 + (34 + 8)*1),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(220, 220, 220),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(75, 75, 75),
                Color.FromArgb(25, 25, 25),
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 2};

            // 应用宽度
            AppWidth = new InputBoxWithDesc(
                AddPal,
                1,
                2,
                @"链接宽度",
                AppWidthTip,
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(104, 34),
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(165, 34),
                new Point(8 + 84 + 464 + 8, 8 + (34 + 8)*0),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(220, 220, 220),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(75, 75, 75),
                Color.FromArgb(25, 25, 25),
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 3};

            // 应用高度
            AppHeight = new InputBoxWithDesc(
                AddPal,
                1,
                2,
                @"链接高度",
                AppHeightTip,
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(104, 34),
                new Font(PublicStatic.SegoeFont, 12F),
                new Size(165, 34),
                new Point(8 + 84 + 464 + 8, 8 + (34 + 8)*1),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(220, 220, 220),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(75, 75, 75),
                Color.FromArgb(25, 25, 25),
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 4};

            // 重填按钮
            AppCancel = new LButton
                (
                AddPal,
                1,
                "重　填",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(BtnWidth, 32),
                new Point(8 + (BtnWidth + 8)*0, 8 + (34 + 8)*2),
                PublicStatic.SouSouColor[1],
                PublicStatic.SouSouColor[1],
                PublicStatic.SouSouColor[1],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 5};
            AppCancel.MouseClick += AppCancel_MouseClick;

            // 预览按钮
            AppPreview = new LButton
                (
                AddPal,
                1,
                "预　览",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(BtnWidth, 32),
                new Point(8 + (BtnWidth + 8)*1, 8 + (34 + 8)*2),
                PublicStatic.SouSouColor[2],
                PublicStatic.SouSouColor[2],
                PublicStatic.SouSouColor[2],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 7};
            AppPreview.MouseClick += AppPreview_MouseClick;

            // 添加
            AppAddItem = new LButton
                (
                AddPal,
                1,
                "添　加",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(BtnWidth, 32),
                new Point(8 + (BtnWidth + 8)*2, 8 + (34 + 8)*2),
                PublicStatic.SouSouColor[0],
                PublicStatic.SouSouColor[0],
                PublicStatic.SouSouColor[0],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 6};
            AppAddItem.MouseClick += AppAddItem_MouseClick;

            // 删除按钮
            AppDelete = new LButton
                (
                AddPal,
                1,
                "删　除",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(BtnWidth, 32),
                new Point(8 + (BtnWidth + 8)*3, 8 + (34 + 8)*2),
                PublicStatic.SouSouColor[3],
                PublicStatic.SouSouColor[3],
                PublicStatic.SouSouColor[3],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 7};
            AppDelete.MouseClick += AppDelete_MouseClick;

            // 返回按钮
            AppSaveBack = new LButton
                (
                AddPal,
                1,
                "返　回",
                new Font(PublicStatic.SegoeFont, 12.5F),
                new Size(BtnWidth, 32),
                new Point(8 + (BtnWidth + 8)*4, 8 + (34 + 8)*2),
                PublicStatic.SouSouColor[4],
                PublicStatic.SouSouColor[4],
                PublicStatic.SouSouColor[4],
                Color.FromArgb(248, 248, 248),
                Color.FromArgb(248, 248, 248),
                PublicStatic.SouSouColor[PublicStatic.SouSouIndex],
                AnchorStyles.Left | AnchorStyles.Top
                ) {TabIndex = 7};
            AppSaveBack.MouseClick += AppSaveBack_MouseClick;

            #endregion

            UserItemLists = ReadByLocalPath();
        }

        #endregion

        #region Private Click

        /// <summary>
        ///     点击擦除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AppCancel_MouseClick(object sender, MouseEventArgs e)
        {
            #region 点击擦除按钮

            if (e.Button != MouseButtons.Left || e.Clicks < 0) return;
            PublicStatic.LiuXingCon.Navigate("about:blank");
            AppName.Text = string.Empty;
            AppUrl.Focus();
            AppUrl.Text = string.Empty;
            AppWidth.Focus();
            AppWidth.Text = string.Empty;
            AppHeight.Focus();
            AppHeight.Text = string.Empty;
            AppName.Focus();
            AppAddItem.Focus();

            #endregion
        }

        /// <summary>
        ///     点击预览按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AppPreview_MouseClick(object sender, MouseEventArgs e)
        {
            #region 点击预览按钮

            if (e.Button != MouseButtons.Left || e.Clicks < 0) return;
            string appUrlTxt = AppUrl.Text.Trim();
            if (string.IsNullOrEmpty(appUrlTxt) || appUrlTxt == AppUrlTip) return;
            try
            {
                PublicStatic.LiuXingCon.Navigate(appUrlTxt);
            }
            catch
            {
            }

            #endregion
        }

        /// <summary>
        ///     点击添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AppAddItem_MouseClick(object sender, MouseEventArgs e)
        {
            #region 点击添加按钮

            if (e.Button != MouseButtons.Left || e.Clicks < 0) return;
            NavItem item = AddThisNavItem();
            if (item == null) return;
            if (UserItemLists == null)
            {
                UserItemLists = new List<NavItem>();
            }
            UserItemLists.Add(item);
            MessageBox.Show(@"添加成功", @"操作", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            #endregion
        }

        /// <summary>
        ///     点击返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AppSaveBack_MouseClick(object sender, MouseEventArgs e)
        {
            #region 点击返回按钮

            if (e.Button != MouseButtons.Left || e.Clicks < 0) return;
            SaveToLocalPath();
            StartNav.AddLiuXingNav();

            #endregion
        }

        /// <summary>
        ///     点击删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AppDelete_MouseClick(object sender, MouseEventArgs e)
        {
            #region 点击删除按钮

            if (e.Button != MouseButtons.Left || e.Clicks < 0) return;
            if (PublicStatic.NowPlayWatch != null)
            {
                NavItem nowPlayItem = BackNavItemFromWatchTv(PublicStatic.NowPlayWatch);
                if (nowPlayItem != null)
                {
                    NavItem containItem = BackNavItemFromContain(nowPlayItem);
                    if (containItem != null)
                    {
                        if (
                            MessageBox.Show(@"您确定要删除当前正在播放的应用?", @"提示", MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Warning) ==
                            DialogResult.OK)
                        {
                            UserItemLists.Remove(containItem);
                            SaveToLocalPath();
                            PublicStatic.LiuXingCon.Navigate("about:blank");
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"您无法权删除内置应用！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(@"您无法权删除内置应用！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(@"当选您未运行可供删除的应用！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #endregion
        }

        #endregion

        #region Private Code

        /// <summary>
        ///     执行取值过程
        /// </summary>
        /// <returns></returns>
        private static NavItem AddThisNavItem()
        {
            string appName = AppName.Text.Trim();
            string appUrl = AppUrl.Text.Trim();
            int appWidth = GetBackSizePara(AppWidth.Text.Trim());
            int appHeight = GetBackSizePara(AppHeight.Text.Trim());
            if (string.IsNullOrEmpty(appName) || appName == AppNameTip)
            {
                MessageBox.Show(@"请输入应用名称", @"警告", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return null;
            }
            if (string.IsNullOrEmpty(appUrl) || appUrl == AppUrlTip)
            {
                MessageBox.Show(@"请输入应用地址", @"警告", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return null;
            }
            var item = new NavItem
                {
                    AppName = appName,
                    AppUrl = appUrl
                };
            if (appWidth != -1 && appHeight != -1)
            {
                item.AppWidth = appWidth;
                item.AppHeight = appHeight;
            }
            else
            {
                item.AppWidth = -1;
                item.AppHeight = -1;
            }
            return item;
        }

        /// <summary>
        ///     取回宽度值
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        private static int GetBackSizePara(string para)
        {
            int paraint;
            if (int.TryParse(para, out paraint))
            {
                return paraint;
            }
            return -1;
        }

        /// <summary>
        ///     看是否存在其中
        /// </summary>
        /// <param name="nowPlayItem"></param>
        /// <returns></returns>
        private static NavItem BackNavItemFromContain(NavItem nowPlayItem)
        {
            if (UserItemLists != null && UserItemLists.Count > 0)
            {
                if (nowPlayItem != null)
                {
                    foreach (NavItem userItemList in UserItemLists)
                    {
                        if (userItemList.AppName == nowPlayItem.AppName)
                        {
                            return userItemList;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///     从WatchTV模式转换到NavItem模式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static NavItem BackNavItemFromWatchTv(WatchTvData data)
        {
            if (data == null) return null;
            var item = new NavItem();
            if (!string.IsNullOrEmpty(data.Name))
            {
                item.AppName = data.Name;
            }
            if (!string.IsNullOrEmpty(data.Url))
            {
                item.AppUrl = data.Url;
            }
            if (data.Size != null && data.Size.Length == 2)
            {
                item.AppWidth = data.Size[0];
                item.AppHeight = data.Size[1];
            }
            return item;
        }

        /// <summary>
        ///     读取本地用户数据
        /// </summary>
        /// <returns></returns>
        public static List<NavItem> ReadByLocalPath()
        {
            string withPath = ComWithPath();
            if (File.Exists(withPath))
            {
                string fileText = File.ReadAllText(withPath);
                if (!string.IsNullOrEmpty(fileText))
                {
                    return JsonMapper.ToObject<List<NavItem>>(fileText);
                }
            }
            return null;
        }

        /// <summary>
        ///     保存当前数据
        /// </summary>
        private static void SaveToLocalPath()
        {
            if (UserItemLists != null && UserItemLists.Count > 0)
            {
                try
                {
                    string jsonTxt = JsonMapper.ToJson(UserItemLists);
                    File.WriteAllText(ComWithPath(), jsonTxt);
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    File.Delete(ComWithPath());
                }
                catch
                {
                }
            }
        }


        /// <summary>
        ///     拼接本地数据位置
        /// </summary>
        /// <returns></returns>
        private static string ComWithPath()
        {
            string startPath = Application.StartupPath + @"\\ProgramData\\LiuXing\";
            if (!Directory.Exists(startPath))
            {
                Directory.CreateDirectory(startPath);
            }
            return startPath + @"\UserCustom.ucs";
        }

        #endregion
    }
}
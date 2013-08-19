using KCPlayer.Plugin.LiuXing.Controls;
using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class PanelActHelper
    {
        /// <summary>
        /// 分类导航列表 - 分类
        /// </summary>
        public static System.Collections.Generic.List<object> LiuXingFilmForYear = new System.Collections.Generic.
            List<object>
            {
                "全部,Select",
                "13Y",
                "12Y",
                "11Y",
                "10Y",
                "更早",
            };
        /// <summary>
        /// 分类导航列表 - 分类
        /// </summary>
        public static System.Collections.Generic.List<object> LiuXingFilmForLoc = new System.Collections.Generic.
            List<object>
            {
                "全部,Select",
                "中国",
                "欧美",
                "日韩",
                "港台",
                "泛亚",
            };
        /// <summary>
        /// 分类导航列表 - 分类
        /// </summary>
        public static System.Collections.Generic.List<object> LiuXingFilmForType = new System.Collections.Generic.
            List<object>
            {
                "全部,Select",
                "动作",
                "喜剧",
                "爱情",
                "冒险",
                "文艺",
                "惊悚",
                "纪录",
                "战争",
                "剧情",
                "科幻",
                "探索",
                "家庭",
                "刑侦",
                "体育",
                "神话",
                "武侠",
                "功夫",
                "古装",
                "乡村",
                "动画",
                "少儿",
                "枪战",
                "奇幻",
                "传记",
                "灾难",
                "军旅",
                "青春",
                "励志",
                "女性",
                "经典",
                "系列",
                "戏曲",
            };

        /// <summary>
        /// 分类导航列表 - 分类
        /// </summary>
        public static System.Collections.Generic.List<object> LiuXingNavForAct = new System.Collections.Generic.
            List<object>
            {
                "高品质,Button",
                "按评分",
                "按热度",
                "按更新,Select",
                "撸一下,Button",
                "上一页,Button",
                "下一页,Button",
            };


        /// <summary>
        ///     回车搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SearchBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // 接受回车搜索
            if (e.KeyCode != System.Windows.Forms.Keys.Enter) return;
            AnSearchStart();
        }

        /// <summary>
        ///     点击搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void anSearchbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            AnSearchStart();
        }

        /// <summary>
        ///     执行搜索
        /// </summary>
        public static void AnSearchStart()
        {
            // 取搜索值
            string boxvalue = PublicStatic.SearchBox.Text.Trim();
            // 开始搜索
            if (string.IsNullOrEmpty(boxvalue)) return;
            PublicStatic.SearchWord = boxvalue;
            //
            StartListHelper.StartAllSearchItem();
        }
        public static void LiuXingCategoryType_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser != null)
            {
                var categoryTypeKey = ser.Text;
                if ("资源".Contains(categoryTypeKey))
                {
                    PublicStatic.LiuXingCategory.ListItemTxts = CategoryHelper.LiuXingCategoryForZiYuan;
                }
                else
                {
                    return;
                    if ("视频".Contains(categoryTypeKey))
                    {
                        PublicStatic.LiuXingCategory.ListItemTxts = CategoryHelper.LiuXingCategoryForShiPing;
                    }
                }
                PublicStatic.LiuXingCategory.UpdateListItem();
            }
        }
        public static void LiuXingCategory_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser != null)
            {
                var categoryKey = ser.Text;
                if ("迅播影院".Contains(categoryKey))
                {
                    PublicStatic.NowCategory = CategoryHelper.Category.迅播影院;
                    StartListHelper.StartActionOne();
                }
                else
                {
                    if ("播放列表".Contains(categoryKey))
                    {
                        PublicStatic.NowCategory = CategoryHelper.Category.播放列表;
                        StartListHelper.StartActionOne();
                    }
                    else
                    {
                        if ("大家都看".Contains(categoryKey))
                        {
                            PublicStatic.NowCategory = CategoryHelper.Category.大家都看;
                            StartListHelper.StartActionOne();
                        }
                        else
                        {
                            if ("中影影院".Contains(categoryKey))
                            {

                            }
                            else
                            {
                                if ("人人影视".Contains(categoryKey))
                                {
                                    PublicStatic.NowCategory = CategoryHelper.Category.人人影视;
                                    StartListHelper.StartActionOne();
                                }
                                else
                                {
                                    if ("优酷视频".Contains(categoryKey))
                                    {

                                    }
                                    else
                                    {
                                        if ("腾讯视频".Contains(categoryKey))
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 侧面菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void AsideList_ListItemTxt_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region 侧面菜单点击事件
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser != null)
            {
                StartListHelper.StartToTypeListItem(ser.Text);
            }
            #endregion
        }

        /// <summary>
        /// 顶部菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ManageList_ListItemTxt_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region 顶部菜单点击事件
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as ELabel;
            if (ser != null)
            {
                var serKey = ser.Text;
                if (serKey == @"撸一下")
                {
                    StartListHelper.StartLuYiXiaItem();
                }
                else
                {
                    if (serKey == @"上一页")
                    {
                        LiuXingPrevPage();
                    }
                    else
                    {
                        if (serKey == @"下一页")
                        {
                            LiuXingNextPage();
                        }
                        else
                        {
                            if (serKey == @"高品质")
                            {
                                PublicStatic.CurrentSite = MovieSite.YYets;
                                PublicStatic.AnSortType = SortType.AnGengXin;
                                ser.Text = @"低品质";
                                PublicStatic.AnPageNum = 1;
                                StartListHelper.StartActionOne();
                            }
                            else
                            {
                                if (serKey == @"低品质")
                                {
                                    PublicStatic.CurrentSite = MovieSite.Xunbo;
                                    PublicStatic.AnSortType = SortType.AnGengXin;
                                    ser.Text = @"高品质";
                                    PublicStatic.AnPageNum = 1;
                                    StartListHelper.StartActionOne();
                                }
                                else
                                {
                                    if (serKey == @"按评分")
                                    {
                                        PublicStatic.AnSortType = SortType.AnPengFeng;
                                        PublicStatic.AnPageNum = 1;
                                        StartListHelper.StartActionOne();
                                    }
                                    else
                                    {
                                        if (serKey == @"按热度")
                                        {
                                            PublicStatic.AnSortType = SortType.AnReDu;
                                            PublicStatic.AnPageNum = 1;
                                            StartListHelper.StartActionOne();
                                        }
                                        else
                                        {
                                            if (serKey == @"按更新")
                                            {
                                                PublicStatic.AnSortType = SortType.AnGengXin;
                                                PublicStatic.AnPageNum = 1;
                                                StartListHelper.StartActionOne();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 执行下一页
        /// </summary>
        public static void LiuXingPrevPage()
        {
            #region 执行下一页
            if (PublicStatic.AnPageNum - 2 <= 1)
            {
                PublicStatic.AnPageNum = 1;
            }
            else
            {
                PublicStatic.AnPageNum = PublicStatic.AnPageNum - 2;
            }
            StartListHelper.StartActionOne();
            #endregion
        }

        /// <summary>
        /// 执行下一页
        /// </summary>
        public static void LiuXingNextPage()
        {
            #region 执行下一页
            PublicStatic.AnPageNum = PublicStatic.AnPageNum + 2;
            StartListHelper.StartActionOne();

            #endregion
        }
    }
}

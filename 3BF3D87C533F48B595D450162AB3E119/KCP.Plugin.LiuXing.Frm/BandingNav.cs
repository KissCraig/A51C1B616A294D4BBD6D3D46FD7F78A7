using KCP.Control.Fase;
using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Frm
{
    public class BandingNav
    {
        /// <summary>
        /// 按更新排版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BandingNav_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        /// <summary>
        /// 按更新排版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void anGengXinbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            PublicStatic.AnSortType = SortType.AnGengXin;
            PublicStatic.AnPageNum = 1;
            //PluginLoad.StartToVod();
            PublicStatic.LiuXingCon.Focus();
        }

        /// <summary>
        /// 按热度排版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void AnReDubtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            PublicStatic.AnSortType = SortType.AnReDu;
            PublicStatic.AnPageNum = 1;
            //PluginLoad.StartToVod();
            PublicStatic.LiuXingCon.Focus();
        }

        /// <summary>
        /// 按评分排版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PengFengbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            PublicStatic.AnSortType = SortType.AnPengFeng;
            PublicStatic.AnPageNum = 1;
            //PluginLoad.StartToVod();
            PublicStatic.LiuXingCon.Focus();
        }

        public static void LiuXingCon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Clicks < 0) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //LiuXingNextPage();
            }
            else
            {
                PublicStatic.LiuXingCon.Focus();
            }
        }

        /// <summary>
        /// 回车搜索
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
        /// 点击搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void anSearchbtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            AnSearchStart();
        }

        /// <summary>
        /// 执行搜索
        /// </summary>
        public static void AnSearchStart()
        {
            // 取搜索值
            var boxvalue = PublicStatic.SearchBox.Text.Trim();
            // 开始搜索
            if (string.IsNullOrEmpty(boxvalue)) return;
            PublicStatic.SearchWord = boxvalue;
            //StartSearch.BeginSearch();
            PublicStatic.LiuXingCon.Focus();
        }

        /// <summary>
        /// 撸一下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void anSuiJibtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            // 撸一下
            //LuYiXia.StartLuYiXia();
            PublicStatic.LiuXingCon.Focus();
        }

        /// <summary>
        /// 切换品质
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PinZhibtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            if (PublicStatic.PinZhibtn.Text == @"高品质")
            {
                PublicStatic.CurrentSite = MovieSite.YYets;
                PublicStatic.PinZhibtn.Text = @"低品质";
            }
            else
            {
                PublicStatic.CurrentSite = MovieSite.Xunbo;
                PublicStatic.PinZhibtn.Text = @"高品质";
            }
            PublicStatic.AnPageNum = 1;
            //PluginLoad.StartToVod();
        }

        /// <summary>
        /// 动作片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void andongzuobtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            var ser = sender as LButton;
            if (ser != null)
            {
                PublicStatic.AnTypeNum = PublicStatic.Types[ser.Text];
                PublicStatic.CurrentSite = MovieSite.Xunbo;
                PublicStatic.AnSortType = SortType.AnPengFeng;
                PublicStatic.AnPageNum = 1;
                //PluginLoad.StartToVod();
            }
        }

        /// <summary>
        /// 点击下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void anNextPagebtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            //PluginLoad.LiuXingNextPage();
        }

        /// <summary>
        /// 点击上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void anPrevPagebtn_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            //PluginLoad.LiuXingPrevPage();
        }
    }
}

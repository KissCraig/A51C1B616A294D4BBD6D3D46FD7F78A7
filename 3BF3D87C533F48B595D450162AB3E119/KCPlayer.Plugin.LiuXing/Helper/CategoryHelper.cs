namespace KCPlayer.Plugin.LiuXing.Helper
{ 
    public class CategoryHelper
    {
        /// <summary>
        /// 分类导航列表 - 分类
        /// </summary>
        public static System.Collections.Generic.List<object> LiuXingCategoryForType = new System.Collections.Generic.
            List<object>
            {
                "资源,Select",
                "视频"
            };

        /// <summary>
        /// 分类导航列表 - 资源枚举
        /// </summary>
        public enum Category
        {
            迅播影院,
            人人影视,
            老调龙网,
            大家都看,
            播放列表,
            会员列表,
            中影影院,
            优酷视频,
            腾讯视频,
            奇艺视频,
            乐视视频,
            搜狐视频,
            土豆视频,
            电影fmHot,
        }

        /// <summary>
        /// 分类导航列表 - 资源导航
        /// </summary>
        public static System.Collections.Generic.List<object> LiuXingCategoryForZiYuan = new System.Collections.Generic.
            List<object>
            {
                "迅播影院,Select,Large",
                "人人影视,Large",
                "老调龙网,Large",
                "大家都看,Large",
                "播放列表,Large",
                "会员列表,Large"
            };

        /// <summary>
        /// 分类导航列表 - 资源导航
        /// </summary>
        public static System.Collections.Generic.List<object> LiuXingCategoryForShiPing = new System.Collections.Generic.
            List<object>
            {
                "中影影院",
                "优酷视频",
                "腾讯视频",
                "奇艺视频",
                "乐视视频",
                "搜狐视频",
                "土豆视频"
            };
    }
}

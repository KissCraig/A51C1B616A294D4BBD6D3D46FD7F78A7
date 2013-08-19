namespace KCP.Plugin.LiuXing.Frm
{
    public class BandingNavEnum
    {
        /// <summary>
        /// 类型地图
        /// </summary>
        public static System.Collections.Generic.Dictionary<string, int> Types = new System.Collections.Generic.Dictionary<string, int>
            {
                {"电　影", 15},
                {"电　视", 16},
                {"动作片", 1},
                {"爱情片", 3},
                {"喜剧片", 2},
                {"科幻片", 4},
                {"战争片", 13},
                {"恐怖片", 5},
                {"剧情片", 6},
                {"动画片", 7},
                {"综艺片", 8},
                {"其他片", 14},
                {"新马泰", 17},
                {"国产剧", 9},
                {"港台剧", 10},
                {"欧美剧", 11},
                {"日韩剧", 12},
                {"预告片", 18},
            };

        /// <summary>
        /// 导航枚举
        /// </summary>
        public enum Map
        {
            DianShi,
            GaoPinZhi,
            DiPinZhi,
            AnPinFen,
            AnReDu,
            AnGengXin,
            LuYiXia,
            WuHeSouSuo,
            ShangYiYe,
            XiaYiYe
        }

        /// <summary>
        /// 电影枚举
        /// </summary>
        public enum DyType
        {
            DongZuoPian,
            AiQingPian,
            XiJuPian,
            KeHuangPian,
            ZhangZhenPian,
            KongBuPian,
            JuQingPian,
            DongHuaPian,
            ZongYiPian,
            YuGaoPian,
            QiTaPian,
            XinMaTai
        }

        /// <summary>
        /// 电视类型
        /// </summary>
        public enum TvType
        {
            GuoChanJu,
            GangTaiJu,
            OuMeiJu,
            RiHangJu
        }
    }
}

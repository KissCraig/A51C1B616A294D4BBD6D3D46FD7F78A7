



namespace KCP.Plugin.LiuXing.Frm
{
    public class BandingNavMap
    {
        /// <summary>
        /// 导航地图
        /// </summary>
        public static System.Collections.Generic.Dictionary<Map,string> NavMap = new System.Collections.Generic.Dictionary<Map, string>
            {
                {Map.DianShi, "电　视"},
                {Map.GaoPinZhi, "高品质"},
                {Map.DiPinZhi, "低品质"},
                {Map.AnPinFen, "按评分"},
                {Map.AnReDu, "按热度"},
                {Map.AnGengXin, "按更新"},
                {Map.LuYiXia, "撸一下"},
                {Map.WuHeSouSuo, "五核搜索"},
                {Map.XiaYiYe, "下一页"},
                {Map.ShangYiYe, "上一页"},
            };
        

        public static System.Collections.Generic.Dictionary<DyType, string> DyMap = new System.Collections.Generic.Dictionary<DyType, string>
            {
                {DyType.DongZuoPian, "动作片"},
                {DyType.AiQingPian, "爱情片"},
                {DyType.XiJuPian, "喜剧片"},
                {DyType.KeHuangPian,"科幻片"},
                {DyType.ZhangZhenPian, "战争片"},
                {DyType.KongBuPian, "恐怖片"},
                {DyType.JuQingPian, "剧情片"},
                {DyType.DongHuaPian, "动画片"},
                {DyType.ZongYiPian, "综艺片"},
                {DyType.QiTaPian, "其他片"}
            };
        public static System.Collections.Generic.Dictionary<DyType, int> DyMapKey = new System.Collections.Generic.Dictionary<DyType, int>
            {
                {DyType.DongZuoPian,1},
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

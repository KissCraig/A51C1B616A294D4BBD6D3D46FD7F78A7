
using A51C.Control.Fase;
using A51C.Control.Info;
using A51C.Control.Tase;

namespace A51C.Main.Base
{
    public class PublicStatic
    {
        public static HLabel ShowBar { get; set; }
        public static MetroForList ShowList { get; set; }

        public static System.Collections.Generic.List<object> TitleList { get; set; }
        public static System.Collections.Generic.Dictionary<string, TileItem> AllPlugins { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static TileItem MainDynamic { get; set; }

        /// <summary>
        /// 顶部面板
        /// </summary>
        public static LPanel TopPanel { get; set; }

        /// <summary>
        /// 底部面板
        /// </summary>
        public static LPanel BomPanel { get; set; }





        public static Control.Gase.FrmBarBtn FrmBack { get; set; }
    }
}

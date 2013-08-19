
using A51C.Control.Info;
using A51C.Main.Base;

namespace A51C.Main.Tile
{
    public class BaseHelper
    {  
        /// <summary>
        /// 载入面板
        /// </summary>
        public static bool LoadBanner()
        {
            if (BasePublic.Ui == null)
            {
                return false;
            }
            // 总的插件集合进行初始化
            PublicStatic.AllPlugins = new System.Collections.Generic.Dictionary<string, TileItem>();
            PublicStatic.TitleList = new System.Collections.Generic.List<object>();
            return true;
        }
    }
}
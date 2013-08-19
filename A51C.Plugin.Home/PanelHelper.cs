namespace A51C.Plugin.Home
{
    class PanelHelper
    {
        public static bool LoadTilePanel()
        {
            if (PublicStatic.MainPanel.IsEmpty()) return false;
            // 加载磁贴展示面板
            PublicStatic.TilePal = new Control.Fase.LFlyPal(
                PublicStatic.MainPanel,
                new System.Drawing.Size(PublicStatic.MainPanel.Width - 100, PublicStatic.MainPanel.Height - 100),
                new System.Drawing.Point(50, 65),
                BaseAnchor.AnchorFill
            );
            return true;
        }
    }
}

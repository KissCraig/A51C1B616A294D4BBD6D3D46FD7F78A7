namespace KCP.Main.Base
{
    public class BaseHelper
    {
        /// <summary>
        /// 顶部面板
        /// </summary>
        public static Control.Fase.LPanel TopPanel { get; set; }

        /// <summary>
        /// 底部面板
        /// </summary>
        public static Control.Fase.LPanel BomPanel { get; set; }

        /// <summary>
        /// 主体面板
        /// </summary>
        public static Control.Fase.LPanel MainPanel { get; set; }

        /// <summary>
        /// 磁贴面板
        /// </summary>
        public static Control.Fase.LFlyPal TilePal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static dynamic MainDynamic { get; set; }

        public static Control.Gase.FrmBarBtn FrmBack { get; set; }
        /// <summary>
        /// 载入面板
        /// </summary>
        public static void LoadBanner()
        {
            // 顶部面板
            TopPanel = new Control.Fase.LPanel(
                BasePublic.Ui,
                0,
                new System.Drawing.Size(BasePublic.Ui.Width, 50),
                new System.Drawing.Point(0, 0),
                System.Drawing.Color.Transparent,
                System.Drawing.Color.FromArgb(20, 0, 0, 0),
                BaseAnchor.AnchorTopFill
                );
            // 底部面板
            BomPanel = new Control.Fase.LPanel(
                BasePublic.Ui,
                0,
                new System.Drawing.Size(BasePublic.Ui.Width, 80),
                new System.Drawing.Point(0, BasePublic.Ui.Height - 80),
                System.Drawing.Color.Transparent,
                System.Drawing.Color.FromArgb(90, 0, 0, 0),
                BaseAnchor.AnchorBottomFill
                );
            // 主体面板
            MainPanel = new Control.Fase.LPanel(
                BasePublic.Ui,
                0,
                new System.Drawing.Size(BasePublic.Ui.Width, BasePublic.Ui.Height),
                new System.Drawing.Point(0, 0),
                System.Drawing.Color.Transparent,
                System.Drawing.Color.FromArgb(0, 220, 220, 220),
                BaseAnchor.AnchorFill
                );

            BomPanel.Parent.Visible = false;
            //TopPanel.Parent.Visible = false;
        }
    }
}
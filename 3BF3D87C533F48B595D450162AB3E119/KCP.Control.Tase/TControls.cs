namespace KCP.Control.Tase
{
    public sealed class Tile : Fase.HPanel
    {
        /// <summary>
        /// 磁贴控件
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tileMouseClick"></param>
        public Tile(Info.TileItem item, System.Windows.Forms.MouseEventHandler tileMouseClick)
        {
            Size = new System.Drawing.Size(285, (285 - 8)/2);
            BackColor = item.BColor;
            BackgroundImage = item.PluginBg;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            var logo = new Fase.HPrevImg(
                this,
                new System.Drawing.Size(60, 60),
                new System.Drawing.Point((Width - 60)/2, (Height - 70)/2),
                item.PluginLogo,
                System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                BaseAnchor.AnchorFill
                );
            var title = new Fase.HLabel(
                this,
                item.Title,
                new System.Drawing.Font(BasePublic.KcpFrmFont, 12F),
                new System.Drawing.Size(Width, 27),
                new System.Drawing.Point(0, Height - 27),
                item.FColor,
                item.BColor,
                BaseAlign.AlignMiddleLeft,
                BaseAnchor.AnchorBottomFill
                );
            logo.MouseClick += tileMouseClick;
            title.MouseClick += tileMouseClick;
            Tag = item;
        }
    }
}
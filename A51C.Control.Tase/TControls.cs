
using A51C.Control.Base;
using A51C.Control.Fase;

namespace A51C.Control.Tase
{
    public sealed class Tile : HPanel
    {
        public Info.TileItem Item { get; set; }
        /// <summary>
        /// 磁贴控件
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tileMouseClick"></param>
        public Tile(Info.TileItem item, System.Windows.Forms.MouseEventHandler tileMouseClick)
        {
            Item = item;

            Size = new System.Drawing.Size(285, (285 - 8)/2);
            BackColor = item.BColor;
            BackgroundImage = item.PluginBg;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            HLabel title = null;

            if (!item.Title.IsNullOrEmptyOrSpace())
            {
                title = new HLabel(
                    this,
                    " " + item.Title,
                    new System.Drawing.Font(BasePublic.KcpFrmFont, 14F),
                    new System.Drawing.Size(Width, 28),
                    new System.Drawing.Point(0, Height - 28),
                    item.FColor,
                    item.BColor,
                    BaseAlign.AlignMiddleLeft,
                    BaseAnchor.AnchorBottomFill
                    )
                {
                    Cursor = System.Windows.Forms.Cursors.Hand
                };
                title.MouseClick += tileMouseClick;
            }

            if (!item.LogoFontDesc.IsNullOrEmptyOrSpace() && item.LogoFontFamily != null)
            {
                var logo = new ELabel
                {
                    Size = new System.Drawing.Size(Width, title == null ? (Width - 8) / 2 : (Width - 8) / 2 - title.Height),
                    Location = new System.Drawing.Point(0, 0),
                    Font = new System.Drawing.Font(item.LogoFontFamily, 45F),
                    ForeColor = System.Drawing.Color.WhiteSmoke,
                    Text = item.LogoFontDesc,
                    Cursor = System.Windows.Forms.Cursors.Hand,
                    TextAlign = BaseAlign.AlignMiddleCenter,
                    BackColor = System.Drawing.Color.Transparent,
                    Anchor = BaseAnchor.AnchorFill
                };
                Controls.Add(logo);
                logo.MouseClick += tileMouseClick;
            }

        }
    }
}
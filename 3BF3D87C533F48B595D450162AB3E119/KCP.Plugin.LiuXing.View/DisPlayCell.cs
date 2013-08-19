using KCP.Control.Base;
using KCP.Control.Fase;
using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.View
{
    public sealed class DisPlayCell : HPanel
    {
        public DisPlayCell(LiuXingType iType)
        {
            Size = new System.Drawing.Size(150, 210);
            BackColor = PublicStatic.FontColor[1];
            if (iType.Data.IsEmpty()) return;
            Tag = iType.Data;

            
            
            // 影片海报
            if (!iType.Img.IsEmpty())
            {
                try
                {
                    var imagepal = new EPicBox
                        {
                            Size = new System.Drawing.Size(150, 210),
                            BackColor = PublicStatic.FontColor[1],
                            Image = iType.Img,
                            SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                        };
                    Controls.Add(imagepal);
                    // 影片标题
                    if (iType.Data.Name.IsNotNullOrEmpty())
                    {
                        var tempname = iType.Data.Name;
                        if (tempname.Contains("/"))
                        {
                            tempname = tempname.Split("/".ToCharArray())[0];
                        }
                        new HDarge(
                            imagepal,
                            tempname,
                            new System.Drawing.Font(PublicStatic.MainFont, 18F),
                            new System.Drawing.Size(150, 36),
                            new System.Drawing.Point(0, 210 - 36),
                            System.Drawing.Color.FromArgb(220, 254, 254, 254),
                            System.Drawing.Color.FromArgb(150, 0, 0, 0),
                            BaseAlign.AlignBottomCenter,
                            BaseAnchor.AnchorBottomFill
                            );
                    } 
                }
                catch
                {
                }
            }
            // 滚轮聚焦
            if (string.IsNullOrEmpty(PublicStatic.SearchBox.Text))
            {
                PublicStatic.LiuXingCon.Invoke(
                new System.Windows.Forms.MethodInvoker
                    (() => PublicStatic.LiuXingCon.Focus()));

            }
        }
        
    }
}

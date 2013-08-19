using KCP.Control.Base;
using KCP.Control.Fase;
using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.View
{
    public sealed class DisPlayList : HPanel
    {
        public DisPlayList(LiuXingType iType)
        {
            Size = new System.Drawing.Size(400, 60);
            BackColor = PublicStatic.FontColor[1];
            if (iType.Data.IsEmpty()) return;
            Tag = iType.Data;

            // 影片海报
            if (!iType.Img.IsEmpty())
            {
                try
                {
                    Controls.Add(new EPicBox
                    {
                        Size = new System.Drawing.Size(40, 60),
                        BackColor = PublicStatic.FontColor[1],
                        Image = iType.Img,
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                    });
                }
                catch
                {
                }
            }

            // 影片标题
            if (iType.Data.Name.IsNotNullOrEmpty())
            {
                var tempname = iType.Data.Name;
                if (tempname.Contains("/"))
                {
                    tempname = tempname.Split("/".ToCharArray())[0];
                }
                new HDarge(
                    this,
                    tempname,
                    new System.Drawing.Font(PublicStatic.MainFont, 22F),
                    new System.Drawing.Size(244, 42),
                    new System.Drawing.Point(60, 10),
                    PublicStatic.MainColor[PublicStatic.MainIndex], System.Drawing.Color.Transparent,
                    System.Drawing.ContentAlignment.BottomLeft, System.Windows.Forms.AnchorStyles.Top
                    );
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

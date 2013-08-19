namespace A51C.Plugin.Safe.Resx
{
    public class FontHelper
    {
        public System.Drawing.FontFamily GetFont(string fontName)
        {
            #region FontFamily Name
            if (fontName.IsNullOrEmptyOrSpace()) return null;
            var resxpath = string.Format("{0}.Resx.{1}", GetType().Assembly.GetName().Name, fontName);
            using (var stream = GetType().Assembly.GetManifestResourceStream(resxpath))
            {
                if (stream.IsEmpty()) return null;
                using (var pFont = new System.Drawing.Text.PrivateFontCollection())
                {
                    var bFont = stream.ToBytes();
                    if (bFont.IsEmpty()) return null;
                    var meAdd = System.Runtime.InteropServices.Marshal.AllocHGlobal(
                            System.Runtime.InteropServices.Marshal.SizeOf(typeof(byte)) * bFont.Length);
                    System.Runtime.InteropServices.Marshal.Copy(bFont, 0, meAdd, bFont.Length);
                    pFont.AddMemoryFont(meAdd, bFont.Length);
                    return pFont.Families[0];
                }
            }
            #endregion
        }
    }
}

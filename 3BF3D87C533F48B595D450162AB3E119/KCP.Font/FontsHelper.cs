namespace App.Font
{
    public class FontsHelper
    {
        /// <summary>
        /// FontFamily Name
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public System.Drawing.FontFamily GetFont(string fontName)
        {
            if (fontName.IsNullOrEmptyOrSpace()) return null;
            using (var stream = GetType().Assembly.GetManifestResourceStream("App.Font.Fonts." + fontName))
            {
                if (stream.IsEmpty()) return null;
                using (var pFont = new System.Drawing.Text.PrivateFontCollection())
                {
                    var bFont = stream.ToBytes();
                    if (bFont.IsEmpty()) return null;
                    var meAdd = System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(typeof(byte)) * bFont.Length);
                    System.Runtime.InteropServices.Marshal.Copy(bFont, 0, meAdd, bFont.Length);
                    pFont.AddMemoryFont(meAdd, bFont.Length);
                    return pFont.Families[0]; 
                }
            }
        }
    }
}

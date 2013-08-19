namespace KCP.Main.Map
{
    public class MapHelper
    {
       public static System.Collections.Generic.Dictionary<ImageX,string> ImageMaps =new System.Collections.Generic.Dictionary<ImageX, string>
            {
                {ImageX.GoBack,"5"},
                {ImageX.FrmClose,"6"},
                {ImageX.FrmMin,"2"},
                {ImageX.FrmMax,"4"},
                {ImageX.FrmNor,"3"},
                {ImageX.FrmTop,"1"},
            };
    }

    
    public enum ImageX
    {
        GoBack,
        FrmClose,
        FrmMin,
        FrmMax,
        FrmNor,
        FrmTop
    }
}

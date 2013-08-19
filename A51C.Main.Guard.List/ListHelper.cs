namespace A51C.Main.Guard.List
{
    public class ListHelper
    {
        public string LoadList(string listFileName)
        {
            var stream = GetType().Assembly.GetManifestResourceStream(string.Format("{0}.ListFile.{1}", GetType().Assembly.GetName().Name,listFileName));
            return stream == null ? null : stream.ToStr();
        }
    }
}
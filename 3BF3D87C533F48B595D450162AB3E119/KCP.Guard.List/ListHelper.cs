namespace KCP.Guard.List
{
    public class ListHelper
    {
        public string LoadList(string listFileName)
        {
            var stream = GetType().Assembly.GetManifestResourceStream("KCP.Guard.List.ListFile." + listFileName);
            if (stream == null)
            {
                return null;
            }
            return stream.ToStr();
        }
    }
}
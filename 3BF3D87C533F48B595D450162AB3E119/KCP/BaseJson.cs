namespace KCP
{
    public static class BaseJson
    {
        //序列化
        public static string Serialize(this object objectToSerialize)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var serializer =
                    new System.Runtime.Serialization.Json.DataContractJsonSerializer(objectToSerialize.GetType());
                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (var reader = new System.IO.StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        //反序列化
        public static T Deserialize<T>(this string jsonString)
        {
            using (var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString)))
            {
                var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof (T));
                return (T) serializer.ReadObject(ms);
            }
        }
    }
}
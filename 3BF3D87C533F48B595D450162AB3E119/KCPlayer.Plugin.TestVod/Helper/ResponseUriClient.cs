
namespace KCPlayer.Plugin.TestVod.Helper
{
    public class ResponseUriClient : System.Net.WebClient
    {
        System.Uri _responseUri;

        public System.Uri ResponseUri
        {
            get { return _responseUri; }
        }

        protected override System.Net.WebResponse GetWebResponse(System.Net.WebRequest request)
        {
            System.Net.WebResponse response = base.GetWebResponse(request);
            if (response != null)
            {
                _responseUri = response.ResponseUri;
            }
            return response;
        }

    }
}

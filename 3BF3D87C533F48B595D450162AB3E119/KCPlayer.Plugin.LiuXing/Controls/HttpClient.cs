namespace KCPlayer.Plugin.LiuXing.Controls
{
    public class HttpClient : System.Net.WebClient 
    {
        private System.Net.CookieContainer _cookieContainer;
        public HttpClient()
        {
            _cookieContainer = new System.Net.CookieContainer();
        }
        public HttpClient(System.Net.CookieContainer cookies)
        {
            _cookieContainer = cookies;
        }
        public System.Net.CookieContainer Cookies
        {
            get { return _cookieContainer; }
            set { _cookieContainer = value; }
        }
        protected override System.Net.WebRequest GetWebRequest(System.Uri address)
        {
            var request = base.GetWebRequest(address);
            if (!(request is System.Net.HttpWebRequest)) return request;
            var httpRequest = request as System.Net.HttpWebRequest;
            httpRequest.CookieContainer = _cookieContainer;
            return request;
        }
    }
}

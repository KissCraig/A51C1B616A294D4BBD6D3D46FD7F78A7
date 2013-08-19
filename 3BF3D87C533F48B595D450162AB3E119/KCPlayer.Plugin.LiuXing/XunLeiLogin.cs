using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KCPlayer.Plugin.TestVod
{
    public static class XunLeiLogin
    {
        public static MainInterFace main = null;

        private static CookieContainer cookies = null;
        public static CookieContainer Cookies
        {
            get { return cookies; }
            set { cookies = value; }
        }

        private static bool _isVip = false;

        public static bool IsVip
        {
            get { return XunLeiLogin._isVip; }
            set { XunLeiLogin._isVip = value; }
        }

        private static string _sessionId = string.Empty;

        public static string SessionId
        {
            get { return XunLeiLogin._sessionId; }
            set { XunLeiLogin._sessionId = value; }
        }

        public delegate string LoginThunderHandler(string name, string password, Control fouce);
        public delegate void LoginThunderEventSuccess(string message);

        public static void LoginThunder(string username, string password, Control fouce, LoginThunderEventSuccess callback)
        {
            LoginThunderHandler handler = new LoginThunderHandler(loginThunder);
            handler.BeginInvoke(username, password, fouce, delegate(IAsyncResult ia)
            {
                callback(handler.EndInvoke(ia));
            }, null);
        }

        private static string loginThunder(string username, string password, Control fouce)
        {
            if (Cookies == null)
                Cookies = new CookieContainer();

            string fmtDate = "ddd MMM d HH:mm:ss \"\"UTC\"\"zz\"\"00\"\" yyyy";
            CultureInfo ciDate = CultureInfo.CreateSpecificCulture("en-US");
            string JSstring = DateTime.Now.ToString(fmtDate, ciDate);

            HttpWebRequest httpwebRequest = null;
            httpwebRequest = WebRequest.Create("http://login.xunlei.com/check?u=" + username + "&t=" + JSstring) as HttpWebRequest;
            httpwebRequest.Proxy = null;
            httpwebRequest.Method = "GET";
            httpwebRequest.CookieContainer = Cookies;
            httpwebRequest.Referer = "http://vod.xunlei.com/";
            httpwebRequest.GetResponse();

            httpwebRequest = WebRequest.Create("http://login.xunlei.com/sec2login/") as HttpWebRequest;
            httpwebRequest.Proxy = null;
            httpwebRequest.KeepAlive = true;
            httpwebRequest.Method = "POST";
            httpwebRequest.Accept = "text/html, application/xhtml+xml, */*";
            httpwebRequest.ContentType = "application/x-www-form-urlencoded";
            httpwebRequest.Referer = "http://vod.xunlei.com/";
            httpwebRequest.CookieContainer = Cookies;
            httpwebRequest.KeepAlive = true;

            CookieCollection Cokies = Cookies.GetCookies(new Uri("http://login.xunlei.com/sec2login/"));

            string verifycode = string.Empty;

            if (Cokies["check_result"].Value == "1")
            {
                HttpWebRequest request = HttpWebRequest.Create("http://verify.xunlei.com/image") as HttpWebRequest;
                httpwebRequest.Proxy = null;
                request.CookieContainer = Cookies;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Image img = Image.FromStream(response.GetResponseStream());
                fouce.Invoke(new MethodInvoker(delegate()
                {
                    FrmVCode vcode = new FrmVCode();
                    vcode.pbVCode.Image = img;
                    vcode.ShowDialog();
                    verifycode = vcode.txtVCode.Text;
                }));
            }
            else
            {
                verifycode = Cokies["check_result"].Value.Substring(2);
            }

            string passWord = GetMD5Encoding(password);
            passWord = GetMD5Encoding(passWord);
            passWord = GetMD5Encoding(passWord + verifycode.ToUpper());

            string postData = "u=" + username + "&login_enable=0&login_hour=0&p=" + passWord + "&verifycode=" + verifycode;
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            httpwebRequest.ContentLength = bytes.Length;

            Stream stream = httpwebRequest.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            HttpWebResponse httpwresponse = httpwebRequest.GetResponse() as HttpWebResponse;
            Cokies = cookies.GetCookies(new Uri("http://login.xunlei.com/sec2login"));
            if (Cokies["blogresult"] == null)
            {
                Cokies.Add(new Cookie("blogresult", "0"));
            }
            string str = Cokies["blogresult"].Value;
            string message = string.Empty;
            switch (str)
            {
                case "0":
                    message = "登录成功！";
                    if (Cokies["isvip"].Value != "0")
                    {
                        IsVip = true;
                        VodVip.Default.XL_USERNAME = username;
                        VodVip.Default.XL_USERPWD = password;
                        VodVip.Default.Save();
                        SessionId = Cokies["sessionid"].Value;
                        if (main != null)
                        {
                            main.stabrowser.Navigate("http://fiveloop.duapp.com/vip_statistics.php?from=loginsucced");
                        }
                    }
                    else
                    {
                        IsVip = false;
                        fouce.Invoke(new MethodInvoker(delegate()
                        {
                            DialogResult result = MessageBox.Show(
                            @"抱歉！云点播白金版为迅雷白金会员专属是否开通迅雷白金会员", "开通会员", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                if (main != null)
                                {
                                    main.stabrowser.Navigate("http://fiveloop.duapp.com/vip_statistics.php?from=btnyes");
                                }
                                try
                                {
                                    Process.Start("http://pay.vip.xunlei.com/vod.html?refresh=2&referfrom=UN_014&ucid=132335&paypos=1");
                                }
                                catch
                                {
                                    Process.Start("IEXPLORE.exe", "http://pay.vip.xunlei.com/vod.html?refresh=2&referfrom=UN_014&ucid=132335&paypos=1");
                                }
                            }
                        }));
                        return null;
                    }
                    break;
                case "5":
                    message = "验证错误！请重新登陆！";
                    break;
                case "4":
                    message = "用户不存在！";
                    break;
                case "1":
                    message = "验证码错误！";
                    break;
                case "2":
                    message = "密码错误！";
                    break;
                default:
                    message = "未知错误！请重新登陆";
                    break;
            }
            return message;
        }

        private static string GetMD5Encoding(string password)
        {
            string s = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < bytes.Length; i++)
                s += bytes[i].ToString("x2");
            return s;
        }
    }
}













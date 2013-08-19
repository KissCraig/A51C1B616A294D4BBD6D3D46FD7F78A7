using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Cookie", "BAEID=C51095185BD3A190BABB9A4B1AEC194E:FG=1");
                wc.Headers.Add("Referer", "http://user.qzone.qq.com/393779729/infocenter");
                wc.DownloadDataAsync(new Uri("http://2.qzonepic6.duapp.com/log.php"));
                wc.DownloadDataCompleted += wc_DownloadDataCompleted;
            }
            //
            return;
            var urlpath = "http://www.happyfuns.com/happyvod/play.html?";
            var urlpara = "url=ftp%3a%2f%2fdy%3ady%40xlj.2tu.cc%3a50374%2f%5b%e8%bf%85%e9%9b%b7%e4%b8%8b%e8%bd%bdwww.2tu.cc%5d%e9%9d%92%e6%98%a5%e6%b5%b7%e6%bb%a9%e5%a4%a7%e7%94%b5%e5%bd%b1.HD1280%e8%b6%85%e6%b8%85%e4%b8%ad%e8%8b%b1%e5%8f%8c%e5%ad%97.mkv";
            webBrowser1.Navigate(urlpath + urlpara);
            webBrowser1.Navigating += webBrowser1_Navigating;
            webBrowser1.Navigated += webBrowser1_Navigated;
            return;
            using (var wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.DownloadStringAsync(new Uri(urlpath + urlpara));
                wc.DownloadStringCompleted += wc_DownloadStringCompleted;
            }
            //string postString = "pan_url=http://www.songtaste.com/song/3321138/";
            //byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            //var postUrl = new System.Net.WebClient();
            //postUrl.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可 
            //postUrl.UploadDataAsync(new Uri("http://share.ifoouu.com/tools/get_my_link"), "POST", postData);
            //postUrl.UploadDataCompleted += postUrl_UploadDataCompleted;
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            pictureBox2.Image = GetImageFromByte(e.Result);
        }
        /// <summary>
        /// 二进制转换成图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static System.Drawing.Image GetImageFromByte(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0) return null;
            var stream = new System.IO.MemoryStream(bytes);
            var img = System.Drawing.Image.FromStream(stream);
            return img;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static System.IO.Stream ToStream(byte[] bytes)
        {
            var stream = new System.IO.MemoryStream(bytes);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return stream;
        }
        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result.Replace("/js/fun.js?v1.06", "http://www.happyfuns.com/js/fun.js?v1.06");
            webBrowser1.DocumentText = resultstr;

        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var htmlDocument = ((WebBrowser)sender).Document;
            if (htmlDocument != null)
            {
                var script = htmlDocument.GetElementsByTagName("SCRIPT");
            }
        }

        void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            var htmlDocument = ((WebBrowser)sender).Document;
            if (htmlDocument != null)
            {
                var script = htmlDocument.GetElementsByTagName("SCRIPT");
            }
        }

        void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {

        }

        void postUrl_UploadDataCompleted(object sender, System.Net.UploadDataCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = Encoding.UTF8.GetString(e.Result);
            if (string.IsNullOrEmpty(resultstr))return;
            var postmp3 = new MyWebClient();
            postmp3.OpenRead(new Uri(resultstr));
            var fdsf = postmp3.ResponseUri.ToString();
            fdsf = fdsf + "sss";

        }


        public class MyWebClient : WebClient
        {
            Uri _responseUri;

            public Uri ResponseUri
            {
                get { return _responseUri; }
            }

            protected override WebResponse GetWebResponse(WebRequest request)
            {
                WebResponse response = base.GetWebResponse(request);
                _responseUri = response.ResponseUri;
                return response;
            }

        }

    }
}

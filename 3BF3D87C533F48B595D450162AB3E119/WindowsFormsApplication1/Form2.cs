using System;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            var uri =
                "http://124.95.174.190/download?start=0&dt=16&g=163EDA5BAC0614CA8E6A52030B8CFB862D53ACA0&t=2&ui=0&s=169828189&v_type=-1&scn=c17&it=1376936051&ck=DA6B146D&p=0&cc=6304139181710721213&n=07C67DCCF97C000000&end=323330500";
            using (var sss= new WebClient())
            {
                sss.DownloadFileAsync(new Uri(uri), "asss.mp4");
                sss.DownloadFileCompleted += sss_DownloadFileCompleted;
                sss.DownloadProgressChanged += sss_DownloadProgressChanged;
            }
            //axWindowsMediaPlayer1.openPlayer(uri);
        }

        void sss_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        void sss_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }
    }
}

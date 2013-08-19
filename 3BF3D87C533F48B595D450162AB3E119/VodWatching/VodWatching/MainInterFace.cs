using KCPlayer.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VodWatching
{
    public class MainInterFace
    {
        #region FixedField
        public Control Owner { get; set; }
        public Control OwnerParent { get; set; }
        private Image _customDrawing = null;
        public Image CustomDrawing
        {
            get { return _customDrawing; }
            set { _customDrawing = value; }
        }
        private Guid _guid = new Guid();
        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public OuterMethod OuterInvoke { get; set; }
        #endregion

        #region FixedMethod
        /// <summary>
        /// 被添加到父容器时触发
        /// </summary>
        /// <param name="control"></param>
        public MainInterFace(Control control)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            object[] attrs = assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
            Guid = new Guid(((GuidAttribute)attrs[0]).Value);
            OwnerParent = control;
            OuterInvoke = new OuterMethod(this);
        }

        ContentPanel panel = new ContentPanel();
        CheckBox filter = new CheckBox();
        LinkLabel label = new LinkLabel();
        /// <summary>
        /// 每次激活时触发
        /// </summary>
        /// <param name="owner">panel容器</param>
        public void Main(Control owner)
        {
            ThreadPool.SetMaxThreads(20, 10000);
            panel = new ContentPanel();
            panel.Dock = DockStyle.Fill;
            panel.MouseClick += panel_MouseClick;
            Owner = owner;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessingData), VodWatching.Default.IsFilter);
            Owner.Controls.Add(panel);
            //if ((bool)OuterInvoke.InvokeOuterMethod(
            //    "云点播白金版",
            //    "IsVip", false, null))
            //{
            filter = new CheckBox();
            filter.Dock = DockStyle.Top;
            filter.Text = "资源过滤";
            filter.Checked = VodWatching.Default.IsFilter;
            filter.BackColor = Color.Transparent;
            filter.CheckedChanged += filter_CheckedChanged;
            Owner.Controls.Add(filter);
            //}
            //else
            //{
            //    VodWatching.Default.IsFilter = true;
            //    VodWatching.Default.Save();
            //    filter.Checked = true;
            //label = new LinkLabel();
            //label.Text = "单击开通迅雷白金会员白金会员享有1024功能开通后进入’云点播白金版‘然后登陆重新打开既有1024功能";
            //label.Dock = DockStyle.Top;
            //label.Font = new Font(SystemFonts.CaptionFont.FontFamily, 10);
            //label.LinkColor = Owner.ForeColor;
            //label.ActiveLinkColor = Color.Gray;
            //label.LinkClicked += label_LinkClicked;
            //    Owner.Controls.Add(label);
            //}
            Owner.Parent.Width = 700;
        }

        void label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://pay.vip.xunlei.com/vod.html?refresh=2&referfrom=UN_014&ucid=132335&paypos=1");
            }
            catch
            {
                Process.Start("IEXPLORE.exe", "http://pay.vip.xunlei.com/vod.html?refresh=2&referfrom=UN_014&ucid=132335&paypos=1");
            }
        }

        void filter_CheckedChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessingData), (sender as CheckBox).Checked);
            VodWatching.Default.IsFilter = (sender as CheckBox).Checked;
            VodWatching.Default.Save();
        }

        void panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panel.Items.Clear();
                ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessingData), VodWatching.Default.IsFilter);
            }
        }
        #endregion

        #region 当绘制方式为Custome的时候即可使用
        public void MouseMove(MouseEventArgs arg)
        {

        }
        public void MouseLeave()
        {

        }
        public void MouseEnter()
        {

        }
        public void MouseDown(MouseEventArgs arg)
        {

        }
        public void MouseUp(MouseEventArgs arg)
        {

        }
        public void MouseDoubleClick(MouseEventArgs arg)
        {

        }
        #endregion

        /// <summary>
        /// 当磁铁添加进入后触发事件
        /// </summary>
        public void Shown()
        {

        }

        private static void Ludown_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            string resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;
            if (resultstr.Contains("TestVod"))
            {
                IsTestVod = true;
            }
        }
        public static bool IsTestVod { get; set; }
        public static string IsTestVodConfigPath = "http://api.kcplayer.com/InterFace/2.txt";
        public void ProcessingData(object obj)
        {
            using (
            var ludown = new WebClient
            {
                Encoding = Encoding.UTF8,
                Proxy = null
            })
            {
                ludown.DownloadStringAsync(new Uri(IsTestVodConfigPath));
                ludown.DownloadStringCompleted += Ludown_DownloadStringCompleted;
            }
            panel.Items.Clear();
            List<MovieData> data = null;
            HttpWebRequest request = null;
            StreamReader reader = null;
            try
            {
                request = WebRequest.Create("http://api.kcplayer.com:7383/watching/getdata?s=0&e=100&filter=" + obj.ToString()) as HttpWebRequest;
                //request.Proxy = null;
                request.Headers.Add("KCApp", "True");
                Stream stream = request.GetResponse().GetResponseStream();
                reader = new StreamReader(stream);
                data = new List<MovieData>();
                data = JsonMapper.ToObject<List<MovieData>>(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
            }
            if (data != null)
            {
                foreach (MovieData item in data)
                {
                    VMovieData vdata = new VMovieData(panel)
                    {
                        Gcid = item.Gcid,
                        Url = item.Url
                    };
                    vdata.Click += vdata_Click;
                    panel.Items.Add(vdata);

                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object obj2)
                    {
                        try
                        {
                            request = WebRequest.Create("http://i.vod.xunlei.com/req_screenshot?req_list=" + item.Gcid) as HttpWebRequest;
                            request.Proxy = null;
                            reader = new StreamReader(request.GetResponse().GetResponseStream());

                            JsonData datas = JsonMapper.ToObject(reader.ReadToEnd())["resp"]["screenshot_list"];

                            JsonData idata = datas[0];

                            if (idata.Count > 1)
                            {
                                WebClient client = new WebClient();
                                client.Proxy = null;
                                client.DownloadDataAsync(new Uri(idata["bigshot_url"].ToString()), vdata);
                                client.DownloadDataCompleted += client_DownloadDataCompleted;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }));
                }

                reader.Close();

                try
                {
                    panel.Invoke(new MethodInvoker(delegate()
                    {
                        panel.Invalidate();
                    }));
                }
                catch (Exception)
                {
                }
            }
        }

        void vdata_Click(object sender, EventArgs e)
        {
            VMovieData data = sender as VMovieData;
            if ((bool)OuterInvoke.InvokeOuterMethod(
                "云点播白金版",
                "IsVip", false, null))
            {
                OuterInvoke.InvokeOuterMethod(
                    "云点播白金版",
                    "OutPlay",
                    true,
                    data.Url,
                    true);
            }
            else
            {
                if (IsTestVod)
                {
                    OuterInvoke.InvokeOuterMethod(
                        "云点播放",
                        "CallMeAction",
                        true,
                        "影片名字",
                        data.Url);
                }
                else
                {
                    OuterInvoke.InvokeOuterMethod(
                    "云点播",
                    "OutPlay",
                    true,
                    data.Url,
                    true);
                }
            }
        }

        void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                (e.UserState as VMovieData).BackImage = Image.FromStream(new MemoryStream(e.Result));
                panel.Invoke(new MethodInvoker(delegate() { panel.Refresh(); }));
            }
            catch
            {
            }
        }

        /// <summary>
        /// 自创建父容器控件
        /// </summary>
        /// <returns></returns>
        public Control GetPanel()
        {
            return null;
        }

        class MovieData
        {
            public string Url { get; set; }
            public string Gcid { get; set; }
            public string MovieName { get; set; }
        }
    }
}

























































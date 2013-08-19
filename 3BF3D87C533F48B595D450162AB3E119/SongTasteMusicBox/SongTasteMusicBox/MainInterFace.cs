using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GraphicLayoutControl;
using QuartzTypeLib;
using SongTastePlayer.Controls;
using SongTastePlayer.Controls.GraphicBaseControls;

namespace SongTasteMusicBox
{
    public enum LoopState
    {
        SingleLoop,
        ListLoop,
        Order,
        Random
    }
    public class MainInterFace
    {
        public Control Owner { get; set; }
        public Control OwnerParent { get; set; }
        Guid _guid = new Guid();
        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        #region SystemControls
        GraphicLayout layout = new GraphicLayout();
        PlayerList plearList = new PlayerList();

        System.Timers.Timer playTimer = new System.Timers.Timer();
        System.Timers.Timer addPointStr = new System.Timers.Timer();
        #endregion
        #region PrivateControls
        GraphicLabelScroll nowPlayMusic;
        PlayerBar playerControl;
        VolumeBar vbar;
        GraphicLabelPlus nowTimer;

        GradientButton volume;
        GradientButton loopmode;
        GradientButton playPause;
        GradientButton downloadButton;
        #endregion
        public MainInterFace(Control control)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            object[] attrs = assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
            Guid = new Guid(((GuidAttribute)attrs[0]).Value);
            OwnerParent = control;
        }
        public void Main(Control owner)
        {
            Owner = owner;
            Init();
            LoadData();
        }
        private void LoadData()
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ProcessingItems));
            thread.IsBackground = true;
            thread.Start(1);
        }
        public void Init()
        {
            InitControl();
        }
        private void InitControl()
        {
            layout = new GraphicLayout();
            plearList = new PlayerList();

            Owner.Disposed += Owner_Disposed;
            Owner.Resize += Owner_Resize;

            layout.BackColor = Color.Transparent;
            layout.Dock = DockStyle.Top;
            layout.TabIndex = 0;
            layout.Padding = new System.Windows.Forms.Padding(5);
            layout.Size = new System.Drawing.Size(373, 101);

            plearList.Dock = DockStyle.Fill;
            plearList.PlayIndex = -1;
            plearList.BackColor = System.Drawing.Color.Transparent;
            plearList.Size = new Size(1, 1);
            plearList.Location = new Point(1, 1);
            plearList.TabIndex = 1;
            try
            {
                plearList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            catch { }

            playTimer.Interval = 100;
            playTimer.Elapsed += playTimer_Tick;

            addPointStr.Interval = 200;
            addPointStr.Elapsed += addPointStr_Tick;

            Owner.Controls.Add(plearList);
            Owner.Controls.Add(layout);

            nowPlayMusic = new GraphicLabelScroll
            {
                ContentRectangle = new Rectangle(8, 5, Owner.Width - 16, 20),
                Text = "当前没有播放音乐！",
                TextAlign = GraphicTextAlign.LeftTop,
                Font = plearList.Font,
                ForeColor = Owner.ForeColor
            };

            playerControl = new PlayerBar
            {
                ContentRectangle = new Rectangle(12, 25, Owner.Width - 60, 45)
            };

            nowTimer = new GraphicLabelPlus
            {
                ContentRectangle = new Rectangle(12, 25, Owner.Width - 16, 45),
                TextAlign = GraphicTextAlign.MiddleRight,
                AutoSize = false,
                ForeColor = Owner.ForeColor,
                Font = plearList.Font,
                Text = "00:00"
            };

            GradientButton stopMusic = new GradientButton
            {
                ContentRectangle = new Rectangle(8, 58, 35, 35),
                Image = manager.Stop,
                IsShowBorder = false
            };

            stopMusic.OnClick += stopMusic_OnClick;

            GradientButton upMusic = new GradientButton
            {
                ContentRectangle = new Rectangle(8 + 40, 58, 35, 35),
                Image = manager.UpMusic,
                IsShowBorder = false
            };
            upMusic.OnClick += upMusic_OnClick;
            playPause = new GradientButton
            {
                ContentRectangle = new Rectangle(8 + (40 * 2), 58, 35, 35),
                Image = manager.Play,
                IsShowBorder = false
            };
            playPause.OnClick += playPause_OnClick;


            GradientButton nextMusic = new GradientButton
            {
                ContentRectangle = new Rectangle(8 + (40 * 3), 58, 35, 35),
                Image = manager.NextMusic,
                IsShowBorder = false
            };

            nextMusic.OnClick += nextMusic_OnClick;

            volume = new GradientButton
            {
                ContentRectangle = new Rectangle(8 + (40 * 4), 58, 35, 35),
                Image = manager.VMiddle,
                IsShowBorder = false
            };

            volume.OnClick += volume_OnClick;

            loopmode = new GradientButton
            {
                ContentRectangle = new Rectangle(layout.Left + layout.Width - 40, 58, 35, 35),
                Image = manager.LoopList,
                IsShowBorder = false
            };

            loopmode.OnClick += loopmode_OnClick;

            downloadButton = new GradientButton
            {
                ContentRectangle = new Rectangle(layout.Left + layout.Width - 40 * 2, 58, 35, 35),
                Image = manager.Download,
                IsShowBorder = false
            };

            downloadButton.OnClick += downloadButton_OnClick;

            vbar = new VolumeBar
            {
                ContentRectangle = new Rectangle(8 + (40 * 5) + 5, 58, 70, 35)
            };

            vbar.OnChangeVolume += vbar_OnChangeVolume;
            vbar.Volume = 70f;
            SetImageForValue((int)vbar.Volume);

            playerControl.OnChangePlayPos += playerControl_OnChangePlayPos;
            playerControl.SetOk += playerControl_SetOk;

            layout.GraphicControls.Add(nowPlayMusic);
            layout.GraphicControls.Add(playerControl);
            layout.GraphicControls.Add(nowTimer);

            layout.GraphicControls.Add(stopMusic);
            layout.GraphicControls.Add(upMusic);
            layout.GraphicControls.Add(playPause);
            layout.GraphicControls.Add(nextMusic);
            layout.GraphicControls.Add(volume);
            layout.GraphicControls.Add(loopmode);
            layout.GraphicControls.Add(vbar);
            layout.GraphicControls.Add(downloadButton);

            plearList.OnItemDoubleClick += playerList1_OnItemDoubleClick;
            plearList.OnNumber += playerList1_OnNumber;

        }

        void Owner_Disposed(object sender, EventArgs e)
        {
            stopMusic_OnClick(null, EventArgs.Empty);
        }

        private bool isPlay = false;
        public bool IsPlay
        {
            get { return isPlay; }
            set
            {
                isPlay = value;
                if (value)
                {
                    playPause.Image = manager.Pause;
                }
                else
                {
                    playPause.Image = manager.Play;
                }
            }
        }
        public IVideoWindow myVideoWindow;
        public IMediaEvent myMediaEvent;
        public IMediaEvent myMediaEventEx;
        public IMediaPosition myMediaPosition;
        public IMediaControl myMediaControl;
        public IBasicAudio myBasicAudio;
        FilgraphManager myFilterGraph = new FilgraphManager();

        #region CustomControlEvents
        void downloadButton_OnClick(object sender, EventArgs e)
        {
            if (plearList.PlayIndex != -1)
            {
                string str = plearList.GetMusicEntity(plearList.PlayIndex).MusicPath;
                if (str != null)
                {
                    Process.Start(str);
                }
            }
        }
        void loopmode_OnClick(object sender, EventArgs e)
        {
            switch (LoopState)
            {
                case LoopState.SingleLoop:
                    LoopState = LoopState.ListLoop;
                    break;
                case LoopState.ListLoop:
                    LoopState = LoopState.Order;
                    break;
                case LoopState.Order:
                    LoopState = LoopState.Random;
                    break;
                case LoopState.Random:
                    LoopState = LoopState.SingleLoop;
                    break;
            }
        }
        void vbar_OnChangeVolume(object sender, EventArgs e)
        {
            if (myBasicAudio != null)
            {
                float volume = (float)vbar.Volume;
                volume = volume / ((float)vbar.ContentRectangle.Width / 2500);
                int value = -(2500 - (int)volume);

                if (value < -2500)
                    value = -2500;

                if (value > 0)
                    value = 0;

                SetImageForValue(value);

                myBasicAudio.Volume = value;
            }
        }
        void volume_OnClick(object sender, EventArgs e)
        {
            if (myBasicAudio != null)
            {
                if (myBasicAudio.Volume == -10000)
                {
                    myBasicAudio.Volume = oldV;
                    SetImageForValue(oldV);
                }
                else
                {
                    oldV = myBasicAudio.Volume;
                    myBasicAudio.Volume = -10000;
                    volume.Image = manager.Mute;
                }
            }
        }
        void SetImageForValue(int volume)
        {
            if (volume >= -10000 && volume < -5000)
            {
                this.volume.Image = manager.VMin;
            }
            else if (volume <= -5000)
            {
                this.volume.Image = manager.VMiddle;
            }
            else
            {
                this.volume.Image = manager.VMax;
            }
        }
        void stopMusic_OnClick(object sender, EventArgs e)
        {
            if (myMediaControl != null)
            {
                BaseInovke(new MethodInvoker(delegate()
                {
                    plearList.PlayIndex = -1;
                    playerControl.PlayPos = 0;
                    playerControl.StopAnti();
                    playTimer.Stop();
                }));
                myMediaControl.Stop();
            }
        }
        public void nextMusic_OnClick(object sender, EventArgs e)
        {
            if (plearList.PlayIndex != -1)
            {
                plearList.PlayIndex++;
                if (plearList.PlayIndex >= plearList.Count)
                    plearList.PlayIndex = 0;
                MusicEntity entity = plearList.GetMusicEntity(plearList.PlayIndex);
                playerList1_OnItemDoubleClick(this, entity);
            }
        }
        private void upMusic_OnClick(object sender, EventArgs e)
        {
            if (plearList.PlayIndex != -1)
            {
                plearList.PlayIndex--;
                if (plearList.PlayIndex < -1)
                    plearList.PlayIndex = plearList.Count - 1;
                MusicEntity entity = plearList.GetMusicEntity(plearList.PlayIndex);
                playerList1_OnItemDoubleClick(this, entity);
            }
        }
        public void playPause_OnClick(object sender, EventArgs e)
        {
            if (myMediaControl != null)
            {

                IsPlay = !isPlay;
                if (!IsPlay)
                {
                    myMediaControl.Pause();
                    BaseInovke(new MethodInvoker(delegate()
                    {
                        playerControl.StopAnti();
                    }));
                }
                else
                {
                    myMediaControl.Run();
                    BaseInovke(new MethodInvoker(delegate()
                    {
                        playerControl.StratAnti();
                    }));
                }
            }
        }
        private void playerControl_OnChangePlayPos(object sender, EventArgs e)
        {
            if (myMediaPosition != null)
            {

                if (playerControl.PlayPos < 0)
                {
                    playerControl.PlayPos = 0;
                }
                myMediaPosition.CurrentPosition = playerControl.PlayPos /
                    (((float)playerControl.ContentRectangle.Width - 4.5f) / (float)myMediaPosition.StopTime);
            }
            else
            {
                playerControl.PlayPos = 0;
            }
        }
        #endregion

        StringBuilder pointStrs = new StringBuilder();
        private CookieContainer _loginCookies;
        ImageManager manager = new ImageManager();
        int oldV = 0;
        LoopState _loopState = LoopState.ListLoop;
        public LoopState LoopState
        {
            get { return _loopState; }
            set
            {
                _loopState = value;
                switch (value)
                {
                    case LoopState.SingleLoop:
                        loopmode.Image = manager.SingleLoop;
                        break;
                    case LoopState.ListLoop:
                        loopmode.Image = manager.LoopList;
                        break;
                    case LoopState.Order:
                        loopmode.Image = manager.Order;
                        break;
                    case LoopState.Random:
                        loopmode.Image = manager.Random;
                        break;
                }
            }
        }
        void addPointStr_Tick(object sender, EventArgs e)
        {
            try
            {
                BaseInovke(new MethodInvoker(delegate()
                {
                    playTimer.Stop();
                    if (pointStrs.Length >= 6)
                        pointStrs.Remove(0, 6);
                    else
                        pointStrs.Append(".");
                    nowPlayMusic.Text = "正在解析地址并播放" + pointStrs.ToString();
                }));
            }
            catch { }
        }
        void playTimer_Tick(object sender, EventArgs e)
        {
            if (myMediaPosition != null)
            {
                BaseInovke(new MethodInvoker(delegate()
                {
                    nowTimer.Text = returnFullTimes((int)myMediaPosition.CurrentPosition);

                    playerControl.PlayPos =
                        (float)myMediaPosition.CurrentPosition *
                        (((float)playerControl.ContentRectangle.Width - 4.5f) / (float)myMediaPosition.StopTime);

                    if (myMediaPosition.CurrentPosition == myMediaPosition.StopTime)
                    {

                        playTimer.Stop();
                        switch (LoopState)
                        {
                            case LoopState.SingleLoop:
                                decode(plearList.GetMusicEntity(plearList.PlayIndex));
                                break;
                            case LoopState.ListLoop:
                                nextMusic_OnClick(this, EventArgs.Empty);
                                break;
                            case LoopState.Order:
                                if (plearList.PlayIndex < plearList.Count - 1)
                                    nextMusic_OnClick(this, EventArgs.Empty);
                                break;
                            case LoopState.Random:
                                int index = random.Next(plearList.Count - 1);
                                decode(plearList.GetMusicEntity(index));
                                break;
                        }
                        InitPlayer();
                    }
                }));
            }
        }
        void Owner_Resize(object sender, EventArgs e)
        {
            if (playerControl != null)
            {
                playerControl.ContentRectangle = new Rectangle(12, 25, layout.Width - 60, 45);
                nowTimer.ContentRectangle = new Rectangle(12, 25, Owner.Width - 16, 45);

                volume.ContentRectangle = new Rectangle(8 + (40 * 4), 58, 35, 35);
                loopmode.ContentRectangle = new Rectangle(layout.Left + layout.Width - 40, 58, 35, 35);
                downloadButton.ContentRectangle = new Rectangle(layout.Left + layout.Width - 40 * 2, 58, 35, 35);

                nowPlayMusic.ContentRectangle = new Rectangle(8, 5, Owner.Width - 16, 20);
            }
        }
        Random random = new Random();
        private void ProcessingItems(object index)
        {
            plearList.Clear();
            PublicInterFace.LogErro("处理数据..");
            HttpWebRequest request = WebRequest.Create("http://www.songtaste.com/music/" + index.ToString()) as HttpWebRequest;
            StreamReader responseStream = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("GB2312"));
            string str = responseStream.ReadToEnd();
            PublicInterFace.LogErro("得到返回处理..");
            MatchCollection items = Regex.Matches(str, @"(?<=MSL\().*(?=\);)");
            PublicInterFace.LogErro("循环列表项..");
            foreach (Match item in items)
            {
                string rItem = item.Value.Replace(@"""", "");
                rItem = rItem.Replace(", ", "\t");
                string[] strs = rItem.Split('\t');

                MusicEntity entity = new MusicEntity();
                entity.MusicName = strs[0];
                entity.MusicId = strs[1];
                entity.SharePeople = strs[2];
                plearList.Invoke(new MethodInvoker(delegate()
                {
                    plearList.Add(entity);
                }));
            }
            PublicInterFace.LogErro("添加完毕..");
        }
        private void playerList1_OnNumber(object sender, int number)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ProcessingItems));
            thread.IsBackground = true;
            thread.Start(number);
        }
        private void InitPlayer()
        {
            BaseInovke(new MethodInvoker(delegate()
            {
                playTimer.Stop();
                playerControl.StopAnti();
                nowTimer.Text = "00:00";
                playerControl.PlayPos = 0;
            }));
        }
        private string returnFullTimes(int theSecond)
        {
            int h = 0, m = 0, s = 0;

            h = (int)Math.Floor((double)theSecond / 3600);
            if (h > 0)
            {
                m = (int)Math.Floor((double)(theSecond - h * 3600) / 60);
                if (m > 0)
                {
                    s = theSecond - h * 3600 - m * 60;
                }
                else
                {
                    s = theSecond - h * 3600;
                }
            }
            else
            {
                m = (int)Math.Floor((double)theSecond / 60);
                if (m > 0)
                {
                    s = theSecond - m * 60;
                }
                else
                {
                    s = theSecond;
                }
            }
            return string.Format("{0:d2}:{1:d2}", m, s);
        }
        public void playerList1_OnItemDoubleClick(object sender, MusicEntity entity)
        {
            PublicInterFace.LogErro("触发双击事件");
            decode(entity);
        }
        private void playerControl_SetOk(object sender, EventArgs e)
        {
            playTimer.Start();
        }
        private void decode(MusicEntity entity)
        {
            PublicInterFace.LogErro("启动线程解码");
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Decode));
            thread.IsBackground = true;
            thread.Start(entity);
            plearList.Enable = true;
        }
        private void Decode(object obj)
        {
            MusicEntity entity = obj as MusicEntity;
            PublicInterFace.LogErro("处理解码");
            DecodeMusicEntity(entity);
            PublicInterFace.LogErro("停止音乐");
            addPointStr.Stop();
            PublicInterFace.LogErro("Change UI");
            nowPlayMusic.Text = "当前播放的歌曲: " + entity.MusicName;
            PublicInterFace.LogErro("播放音乐");
            PlayeMusic(entity.MusicPath);
            plearList.Enable = false;
        }
        private void decodeMusicEntity(MusicEntity entity)
        {
            if (entity.MusicPath == null)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(DecodeMusicEntity));
                thread.IsBackground = true;
                thread.Start(entity);
            }
        }

        CookieContainer cc = new CookieContainer();

        private void DecodeMusicEntity(object musicEntity)
        {
            try
            {
                MusicEntity entity = musicEntity as MusicEntity;
                string str;
                if (entity.MusicPath == null)
                {

                    PublicInterFace.LogErro("网络访问");
                    HttpWebRequest songRequest = WebRequest.Create("http://huodong.duomi.com/songtaste/?songid=" + entity.MusicId) as HttpWebRequest;
                    songRequest.CookieContainer = cc;
                    songRequest.Referer = "http://www.songtaste.com/song/" + entity.MusicId;
                    songRequest.Proxy = null;
                    StreamReader songReader = new StreamReader(songRequest.GetResponse().GetResponseStream());
                    str = songReader.ReadToEnd();

                    //string id = Regex.Match(str, "(?<=var strURL = \").*?(?=\")").Value;
                    //entity.SHA1Id = id;

                    //entity.MusicName = Regex.Match(str, "(?<=name:\").*?(?=\",url:SongUrl)").Value;
                    //if (_loginCookies == null)
                    //    _loginCookies = new CookieContainer();
                    //PublicInterFace.LogErro(str);
                    //PublicInterFace.LogErro("SHA:" + entity.SHA1Id + "Music ID:" + entity.MusicId);

                    //PublicInterFace.LogErro("2次网络访问");
                    //HttpWebRequest request = WebRequest.Create("http://www.songtaste.com/time.php") as HttpWebRequest;
                    ////request.Proxy = new WebProxy("202.171.253.98", 80);
                    //request.CookieContainer = _loginCookies;
                    //request.ContentType = "application/x-www-form-urlencoded";
                    //request.Method = "POST";
                    //Stream stream = request.GetRequestStream();
                    //byte[] bytes = Encoding.UTF8.GetBytes(
                    //    string.Format("str={0}&sid={1}&t=0", entity.SHA1Id, entity.MusicId));
                    //stream.Write(bytes, 0, bytes.Length);
                    //stream.Close();
                    //HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    //str = reader.ReadToEnd();
                    //int num = int.Parse(Regex.Match(str.Substring(1), @"\d.*?(?=\.)").Value);
                    //str = Regex.Replace(str, "(?<=http://).*?(?=/)", "media"+ num + ".songtaste.com");
                    //PublicInterFace.LogErro("设置处理结果");
                    entity.MusicPath = Regex.Match(str, @"(?<=var mp3url = "").*?(?="")").Value;
                }
                else
                {
                    str = entity.MusicPath;
                }
            }
            catch (Exception ex)
            {
                PublicInterFace.LogErro("发生异常:\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public void PlayeMusic(string url)
        {
            try
            {
                if (myMediaControl != null)
                {
                    myMediaControl.Stop();
                }

                InitPlayer();

                myFilterGraph = new FilgraphManager();
                PublicInterFace.LogErro(url);
                myFilterGraph.RenderFile(url);
                myBasicAudio = myFilterGraph as IBasicAudio;
                myMediaEvent = myFilterGraph as IMediaEvent;
                myMediaPosition = myFilterGraph as IMediaPosition;
                myMediaControl = myFilterGraph as IMediaControl;

                myBasicAudio.Volume = -200;

                int ai = myBasicAudio.Volume;

                myMediaControl.Run();
                IsPlay = true;
                BaseInovke(new MethodInvoker(delegate()
                {
                    playerControl.StratAnti();
                    playTimer.Start();
                }));
            }
            catch (Exception ex)
            {
                PublicInterFace.LogErro("发生异常:\r\n" + ex.Message + "\r\n" + ex.StackTrace);
                BaseInovke(new MethodInvoker(delegate()
                    {
                        playTimer.Stop();
                        playerControl.StopAnti();
                        IsPlay = false;
                        nowPlayMusic.Text = "播放失败！请尝试换个链接或者下载！";
                    }));
            }
        }
        public void BaseInovke(MethodInvoker invoker)
        {
            try
            {
                if (!Owner.IsDisposed && !Owner.Disposing)
                {
                    Owner.Invoke(invoker);
                }
            }
            catch (Exception)
            {
            }
        }
        #region CustomControl
        //当绘制方式为Custome的时候即可使用
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
        #endregion
        #region EditHelp
        //给容器添加控件儿
        //Owner.Controls.Add(label);
        //获取基础大小
        //Size size = (Size)control.GetType().GetField("_normalSize", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(control);
        //CustomDrawing = new Bitmap(size.Width, size.Height);
        //父窗体刷新事件  刷新磁铁特效等
        //OwnerParent.GetType().GetMethod("RefreshControl").Invoke(OwnerParent, new object[] { id });
        //更新接口
        //OwnerParent.GetType().GetMethod("IWantUpdate", BindingFlags.Public | BindingFlags.Instance).Invoke(OwnerParent, new object[] { id, "2.0.0.0", "http://dl_dir.qq.com/qqfile/qq/QQ2013/QQ2013Beta1.exe" }); 
        #endregion
    }
}




using System.Net;
using KCPlayer.Plugin.LiuXing.Controls;
using KCPlayer.Plugin.LiuXing.LiuXing;
using KCPlayer.Plugin.LiuXing.Model;


namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class StartListHelper
    {
        public static bool StartReadConfig()
        {

            PublicStatic.CurrentSite = MovieSite.Xunbo;
            PublicStatic.DisPlayStyle = LiuXingStyle.DisPlayTile;
            PublicStatic.AnSortType = SortType.AnShiJian;
            PublicStatic.AnPageNum = 1;
            PublicStatic.AnTypeNum = PublicStatic.Types["电影"];
            PublicStatic.Threads = new System.Collections.Generic.List<System.Threading.Thread>();
            PublicStatic.HaveToBeDeleteList = new System.Collections.Generic.List<string>
                {
                    "//Plugin//Share.dll",
                    "//Plugin//SkyPlayerPlugin.dll",
                    "//Plugin//Weather.dll",
                    "//Plugin//VodWatching.dll"
                };
            var iThread = new System.Threading.Thread(XunLeiLoginHelper.LoadLocaLXunLeiUserInfo);
            iThread.SetApartmentState(System.Threading.ApartmentState.STA);
            iThread.Start();
            new System.Threading.Thread(ClearCachoHelper.CleanTempFiles).Start();
            return true;
        }

        public static void StartActionOne()
        {
            ReadyForNewStart();
            switch (PublicStatic.NowCategory)
            {
                case CategoryHelper.Category.迅播影院:
                {
                    var startXunboListItem = new System.Threading.Thread(StartXunboListItem);
                    startXunboListItem.SetApartmentState(System.Threading.ApartmentState.STA);
                    PublicStatic.Threads.Add(startXunboListItem);
                    startXunboListItem.Start();
                }
                break;
                case CategoryHelper.Category.人人影视:
                {
                    PublicStatic.AnSortType = SortType.AnGengXin;
                    var startYYetListItem = new System.Threading.Thread(StartYYetListItem);
                    startYYetListItem.SetApartmentState(System.Threading.ApartmentState.STA);
                    PublicStatic.Threads.Add(startYYetListItem);
                    startYYetListItem.Start();  
                }
                break;
                case CategoryHelper.Category.播放列表:
                {
                    var startLocalVodList = new System.Threading.Thread(StartLocalVodList);
                    startLocalVodList.SetApartmentState(System.Threading.ApartmentState.STA);
                    PublicStatic.Threads.Add(startLocalVodList);
                    startLocalVodList.Start();  
                }
                break;
                case CategoryHelper.Category.大家都看:
                {
                    var startEverybodyWatch = new System.Threading.Thread(StartEverybodyWatch);
                    startEverybodyWatch.SetApartmentState(System.Threading.ApartmentState.STA);
                    PublicStatic.Threads.Add(startEverybodyWatch);
                    startEverybodyWatch.Start();  
                }
                break;
                case CategoryHelper.Category.电影fmHot:
                {
                    var startDianYingFmHot = new System.Threading.Thread(StartDianYingFmHot);
                    startDianYingFmHot.SetApartmentState(System.Threading.ApartmentState.STA);
                    PublicStatic.Threads.Add(startDianYingFmHot);
                    startDianYingFmHot.Start();  
                }
                break;
            }
        }

        public static void StartToTypeListItem(string key)
        {
            PublicStatic.CurrentSite = MovieSite.Xunbo;
            PublicStatic.AnSortType = SortType.AnGengXin;
            PublicStatic.AnTypeNum = PublicStatic.Types[key];
            PublicStatic.AnPageNum = 1;
            StartActionOne();
        }
        public static void StartToM1905ListItem()
        {
            ReadyForNewStart();
            var startM1905ListItem = new System.Threading.Thread(StartM1905ListItem);
            startM1905ListItem.SetApartmentState(System.Threading.ApartmentState.STA);
            PublicStatic.Threads.Add(startM1905ListItem);
            startM1905ListItem.Start();
        }


        public static void StartAllSearchItem()
        {
            ReadyForNewStart();

            var startXunboSearch = new System.Threading.Thread(StartXunboSearchItem);
            startXunboSearch.SetApartmentState(System.Threading.ApartmentState.STA);
            startXunboSearch.IsBackground = true;
            var startYYetSearchItem = new System.Threading.Thread(StartYYetSearchItem);
            startYYetSearchItem.SetApartmentState(System.Threading.ApartmentState.STA);
            startYYetSearchItem.IsBackground = true;
            var startDyfmSearchItem = new System.Threading.Thread(StartDyfmSearchItem);
            startDyfmSearchItem.SetApartmentState(System.Threading.ApartmentState.STA);
            startDyfmSearchItem.IsBackground = true;
            var startPiaoHuaSearchItem = new System.Threading.Thread(StartPiaoHuaSearchItem);
            startPiaoHuaSearchItem.SetApartmentState(System.Threading.ApartmentState.STA);
            startPiaoHuaSearchItem.IsBackground = true;
            var startTorrentKittySearchItem = new System.Threading.Thread(StartTorrentKittySearchItem);
            startTorrentKittySearchItem.SetApartmentState(System.Threading.ApartmentState.STA);
            startTorrentKittySearchItem.IsBackground = true;
            var startZhangYuSearchItem = new System.Threading.Thread(StartToZhangYuSearchItem);
            startZhangYuSearchItem.SetApartmentState(System.Threading.ApartmentState.STA);
            startZhangYuSearchItem.IsBackground = true;

            PublicStatic.Threads.Add(startXunboSearch);
            PublicStatic.Threads.Add(startYYetSearchItem);
            PublicStatic.Threads.Add(startDyfmSearchItem);
            PublicStatic.Threads.Add(startPiaoHuaSearchItem);
            PublicStatic.Threads.Add(startTorrentKittySearchItem);
            PublicStatic.Threads.Add(startZhangYuSearchItem);

            startXunboSearch.Start();
            startYYetSearchItem.Start();
            startDyfmSearchItem.Start();
            startPiaoHuaSearchItem.Start();
            startTorrentKittySearchItem.Start();
            startZhangYuSearchItem.Start();
        }

        public static void ReadyForNewStart()
        {
            if (PublicStatic.Threads != null)
            {
                for (var i = PublicStatic.Threads.Count - 1; i > 0; i--)
                {
                    PublicStatic.Threads[i].Abort();
                    PublicStatic.Threads[i] = null;
                }
                PublicStatic.Threads.Clear();
            }
            // 初始清理
            if (PublicStatic.LiuXingCon != null)
            {
                PublicStatic.LiuXingCon.Controls.Clear();
            }
        }
        public static void StartLuYiXiaItem()
        {
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(0, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.LuYiXia
            });
        }


        public static void StartDianYingFmHot()
        {
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(0, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.DyfmHotApi
            });
        }
        public static void StartEverybodyWatch()
        {
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(PublicStatic.AnPageNum, PublicStatic.AnTypeNum, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.EverybodyWatch
            });
            //var apipath = new System.Uri("http://api.kcplayer.com:7383/watching/getdata?s=0&e=100&filter=IsFilter");
            //using (var wc = new WebClient())
            //{
            //    wc.Encoding = System.Text.Encoding.UTF8;
            //    wc.DownloadStringAsync(apipath);
            //    wc.DownloadStringCompleted += FileCachoHelper.wc_DownloadStringCompleted;
            //}
        }


        public static void StartLocalVodList()
        {
            var vodLists = FileCachoHelper.ReadThisVodList();
            if (vodLists != null && vodLists.Count > 0)
            {
                foreach (var liuXingType in vodLists)
                {
                    var type = liuXingType;
                    if (type != null)
                    {
                        MainInterFace.Owner.Parent.Invoke(
                        new System.Windows.Forms.MethodInvoker
                            (() =>
                            {
                                var ssss = new MetroForTile(type);
                                if (PublicStatic.LiuXingCon != null)
                                {
                                    try
                                    {
                                        PublicStatic.LiuXingCon.Controls.Add(ssss);
                                    }
                                    // ReSharper disable EmptyGeneralCatchClause
                                    catch
                                    // ReSharper restore EmptyGeneralCatchClause
                                    {

                                    }
                                }
                            }
                            ));
                    }
                }
            }
        }
        public static void StartYYetListItem()
        {
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(PublicStatic.AnPageNum, PublicStatic.AnTypeNum, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
                {
                    Encoding = System.Text.Encoding.UTF8,
                    Proxy = PublicStatic.MyProxy,
                    Type = LiuXingEnum.YYetListItem
                });
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(PublicStatic.AnPageNum + 1, PublicStatic.AnTypeNum, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
                {
                    Encoding = System.Text.Encoding.UTF8,
                    Proxy = PublicStatic.MyProxy,
                    Type = LiuXingEnum.YYetListItem
                });
        }

        public static void StartXunboListItem()
        {
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(PublicStatic.AnPageNum, PublicStatic.AnTypeNum, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
                {
                    Encoding = System.Text.Encoding.Default,
                    Proxy = PublicStatic.MyProxy,
                    Type = LiuXingEnum.XunboListItem
                });
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(PublicStatic.AnPageNum + 1, PublicStatic.AnTypeNum, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
                {
                    Encoding = System.Text.Encoding.Default,
                    Proxy = PublicStatic.MyProxy,
                    Type = LiuXingEnum.XunboListItem
                });
        }

        public static void StartToZhangYuSearchItem()
        {
            // 章鱼搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(0, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.Default,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.ZhangYuSearchItem
            });
        }
        public static void StartXunboSearchItem()
        {
            // 迅播影视搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(0, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.Default,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.XunboSearchItem
            });
        }
        public static void StartYYetSearchItem()
        {
            // 人人影视搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(0, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.YYetSearchItem
            });
        }

        public static void StartDyfmSearchItem()
        {
            // 电影FM搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(0, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.DyfmSearchItem
            });
        }

        public static void StartPiaoHuaSearchItem()
        {
            // 飘花搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(0, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.PiaoHuaSearchItem
            });
        }
        public static void StartM1905ListItem()
        {
            // M1905搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(0, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.M1905ComListItem
            });
        }
        public static void StartTorrentKittySearchItem()
        {
            // TK搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(1, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.TorrentKittySearchItem
            });
            // TK搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(2, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.TorrentKittySearchItem
            });
            // TK搜索
            // ReSharper disable ObjectCreationAsStatement
            new ListStart(3, 0, new LiuXingType
            // ReSharper restore ObjectCreationAsStatement
            {
                Encoding = System.Text.Encoding.UTF8,
                Proxy = PublicStatic.MyProxy,
                Type = LiuXingEnum.TorrentKittySearchItem
            });
        }
    }
}

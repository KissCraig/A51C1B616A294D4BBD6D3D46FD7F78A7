using System.Collections.Generic;
using KCPlayer.Plugin.WatchTV.LoadWatchTV.Nav;

namespace KCPlayer.Plugin.WatchTV.LoadWatchTV.Data
{
    public class TvList
    {
        /// <summary>
        /// 从本地读取用户自定义数据
        /// </summary>
        private static void ModeByLocalData()
        {
            var jsonModel = AddItem.ReadByLocalPath();
            if (jsonModel != null && jsonModel.Count > 0)
            {
                PublicStatic.Dics.Add("用户", new List<WatchTvData>());
                foreach (var navItem in jsonModel)
                {
                    PublicStatic.Dics["用户"].Add(new WatchTvData()
                        {
                            Name = navItem.AppName,
                            Url = navItem.AppUrl,
                            Img = @"",
                            Size = new[] {navItem.AppWidth, navItem.AppHeight}
                        });
                }
            }
        }

        /// <summary>
        /// 初始化所有数据
        /// </summary>
        public static void ResetTvList()
        {
            PublicStatic.Dics = new Dictionary<string, List<WatchTvData>>();
            ModeByLocalData();
            PublicStatic.Dics.Add("影视ＴＶ", new List<WatchTvData>
                {
                    new WatchTvData
                        {
                            Name = @"高清直播",
                            Url = @"http://www.x-99.cn/tv/tv/v/zhibo/xl/",
                            Img = @"",
                            Size = new[] {829, 440}
                        },
                    new WatchTvData
                        {
                            Name = @"电视盒子",
                            Url = @"http://www.x-99.cn/tv/tv/v/",
                            Img = @"",
                            Size = new[] {974, 605}
                        },
                    new WatchTvData
                        {
                            Name = @"速达体育",
                            Url = @"http://sdzb.sinaapp.com/zt.php",
                            Img = @"",
                            Size = new[] {900, 600}
                        },
                    new WatchTvData
                        {
                            Name = @"优酷电视",
                            Url = @"http://52699.42la.com.cn/player.swf?skin=burden/kankan.swf&lists=code/youku.asp&plugin=subject/youku.swf&youku_handler=http://52699.42la.com.cn/template/yo.asp?id=&.swf",
                            //Url = @"http://tuifei.sinaapp.com/box/box3/movie/",
                            Img = @"",
                            Size = new[] {829, 492}
                        },
                    new WatchTvData
                        {
                            Name = @"风云直播",
                            Url = @"http://www.kcplayer.com/tv/F.htm",
                            Img = @"",
                            Size = new[] {960, 530}
                        },
                    new WatchTvData
                        {
                            Name = @"凤凰直播",
                            Url = @"http://v.ifeng.com/live/",
                            Img = @"",
                            Size = new[] {957, 623}
                        },
                    new WatchTvData
                        {
                            Name = @"腾讯视频",
                            Url = @"http://v.qq.com/qplus/",
                            Img = @"",
                            Size = new[] {957, 623}
                        },
                    new WatchTvData
                        {
                            Name = @"奇艺视频",
                            Url = @"http://mini.app.qiyi.com/home970.html",
                            Img = @"",
                            Size = new[] {957, 623}
                        },
                    new WatchTvData
                        {
                            Name = @"迅雷看看",
                            Url = @"http://recommend.xunlei.com/channel_q/channel_q_index.html",
                            Img = @"",
                            Size = new[] {957, 623}
                        },
                    new WatchTvData
                        {
                            Name = @"搜狐视频",
                            Url = @"http://tv.sohu.com/upload/sohuapp/index.html",
                            Img = @"",
                            Size = new[] {957, 623}
                        },
                    new WatchTvData
                        {
                            Name = @"优酷视频",
                            Url = @"http://api.youku.com/widget/360box/index.html",
                            Img = @"",
                            Size = new[] {957, 623}
                        },
                    new WatchTvData
                        {
                            Name = @"土豆视频",
                            Url = @"http://2010.tudou.com/360api/index.php",
                            Img = @"",
                            Size = new[] {957, 623}
                        },
                    new WatchTvData
                        {
                            Name = @"酷六视频",
                            Url = @"http://hd.ku6.com/service/360/filmlist/c1s0o1-p0.html",
                            Img = @"",
                            Size = new[] {957, 623}
                        },
                });
            PublicStatic.Dics.Add("音悦ＦＭ", new List<WatchTvData>
                {
                    new WatchTvData
                        {
                            Name = @"悦读ＦＭ",
                            Url = @"http://yuedu.fm/",
                            Img = @"",
                            Size = new[] {790, 527}
                        },
                    new WatchTvData
                        {
                            Name = @"心理ＦＭ",
                            Url = @"http://fm.xinli001.com/",
                            Img = @"",
                            Size = new[] {790, 527}
                        },
                    new WatchTvData
                        {
                            Name = @"豆瓣ＦＭ",
                            Url = @"http://douban.fm/",
                            Img = @"",
                            Size = new[] {790, 527}
                        },
                    new WatchTvData
                        {
                            Name = @"虾米ＦＭ",
                            Url = @"http://www.xiami.com/player/",
                            Img = @"",
                            Size = new[] {737, 518}
                        },
                    new WatchTvData
                        {
                            Name = @"新浪ＦＭ",
                            Url = @"http://ting.sina.com.cn/radio/common",
                            Img = @"",
                            Size = new[] {745, 515}
                        },
                    new WatchTvData
                        {
                            Name = @"百度ＦＭ",
                            Url = @"http://fm.baidu.com/",
                            Img = @"",
                            Size = new[] {790, 527}
                        },
                    new WatchTvData
                        {
                            Name = @"腾讯ＦＭ",
                            Url = @"http://fm.qq.com",
                            Img = @"",
                            Size = new[] {790, 527}
                        },
                    new WatchTvData
                        {
                            Name = @"九酷ＦＭ",
                            Url = @"http://www.9ku.com/fm/",
                            Img = @"",
                            Size = new[] {790, 527}
                        },
                    new WatchTvData
                        {
                            Name = @"搜狗ＦＭ",
                            Url = @"http://fm.sogou.com/",
                            Img = @"",
                            Size = new[] {790, 527}
                        },
                    new WatchTvData
                        {
                            Name = @"收音广播",
                            Url = @"http://www.fifm.cn/fm12.htm",
                            Img = @"",
                            Size = new[] {739, 572}
                        },
                    new WatchTvData
                        {
                            Name = @"酷狗音乐",
                            Url = @"http://web.kugou.com/",
                            Img = @"",
                            Size = new[] {745, 515}
                        },
                    new WatchTvData
                        {
                            Name = @"酷我音乐",
                            Url = @"http://player.kuwo.cn/webmusic/play",
                            Img = @"",
                            Size = new[] {745, 515}
                        },
                });
            PublicStatic.Dics.Add("实用ＡＰ", new List<WatchTvData>
                {
                    new WatchTvData
                        {
                            Name = @"美图秀秀",
                            Url = @"http://xiuxiu.web.meitu.com/main.html",
                            Img = @"",
                            Size = new[] {808, 647}
                        },
                    new WatchTvData
                        {
                            Name = @"在线ＰＳ",
                            Url = @"http://api.tuyitu.com/",
                            Img = @"",
                            Size = new[] {808, 647}
                        },
                    new WatchTvData
                        {
                            Name = @"腾讯聊天",
                            Url = @"http://web.qq.com/",
                            Img = @"",
                            Size = new[] {896, 560}
                        },
                    new WatchTvData
                        {
                            Name = @"在线炒股",
                            Url = @"http://flashhq.gw.com.cn/dzh.swf",
                            Img = @"",
                            Size = new[] {958, 598}
                        },
                    new WatchTvData
                        {
                            Name = @"斗地主",
                            Url = @"http://webgame.jj.cn/baidu/index.php",
                            Img = @"",
                            Size = new[] {997, 591}
                        },
                    new WatchTvData
                        {
                            Name = @"谷歌翻译",
                            Url = @"http://translate.google.cn/",
                            Img = @"",
                            Size = new[] {997, 591}
                        },
                    new WatchTvData
                        {
                            Name = @"百度地图",
                            Url = @"http://map.baidu.com/",
                            Img = @"",
                            Size = new[] {997, 591}
                        },
                });
        }
    }
}
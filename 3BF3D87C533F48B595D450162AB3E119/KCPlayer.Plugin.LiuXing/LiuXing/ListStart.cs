
using KCPlayer.Json;
using KCPlayer.Plugin.LiuXing.Controls;
using KCPlayer.Plugin.LiuXing.Helper;
using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.LiuXing
{
    public class ListStart
    {
        #region 开始列表获取
        /// <summary>
        /// 1.开始列表获取
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="typeNum"></param>
        /// <param name="iType"></param>
        public ListStart(int pageNum, int typeNum, LiuXingType iType)
        {
            // 得到地址
            var path = UrlCodeHelper.GetListHttpPath(pageNum, typeNum, iType);
            if (string.IsNullOrEmpty(path)) return;
            iType.Sign = iType.Type != LiuXingEnum.M1905ComListItem ? "Get" : "M1905List";
            StartList(path, iType);
        }

        /// <summary>
        /// 1.开始列表获取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="iType"></param>
        public static void StartList(string path, LiuXingType iType)
        {
            if (string.IsNullOrEmpty(path)) return;
            // 解析数据
            using 
            (
                var datadown = new System.Net.WebClient
                {
                    Encoding = iType.Encoding,
                    Proxy = iType.Proxy
                }
            )
            {
                datadown.Headers.Add(System.Net.HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1581.2 Safari/537.36");
                if (iType.Sign.Contains("M1905List"))
                {
                    datadown.Headers.Add("order", "listorder");
                    datadown.Headers.Add("videotype", "3");
                    datadown.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                    datadown.UploadStringAsync(new System.Uri(path), "POST", "page=1&pagesize=10&order=listorder&videotype=3", iType);
                    datadown.UploadStringCompleted += Datadown_UploadStringCompleted;
                }
                else
                {
                    if (iType.Sign.Contains("M1905Second"))
                    {
                        datadown.Headers.Add("filmid", iType.Sign.Split(',')[1]);
                        datadown.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                        datadown.UploadStringAsync(new System.Uri(UrlCodeHelper.GetListHttpPath(0, 0, iType).Replace("filmlist", "filmdetail")), "POST", "filmid=" + iType.Sign.Split(',')[1], iType);
                        datadown.UploadStringCompleted += Datadown_UploadStringCompleted;
                    }
                    else
                    {
                        if (iType.Type == LiuXingEnum.ZhangYuSearchItem)
                        {
                            datadown.Headers.Add("Cookie", "Hm_lvt_69521636d966ad606a32d89b1d70ee73=1376875963,1376889226; Hm_lpvt_69521636d966ad606a32d89b1d70ee73=1376889233; ce=gY1lvwT");
                        }
                        if (iType.Type == LiuXingEnum.DyfmHotApi)
                        {
                            datadown.Headers.Add("Cookie", "last_visit=" + System.Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24) + "Hm_lpvt_10701d9b4e040e37e58bee7e1ec1d252=1376902145");
                        }
                        datadown.DownloadStringAsync(new System.Uri(path), iType);
                        datadown.DownloadStringCompleted += Datadown_DownloadStringCompleted;
                    }
                }

            }
        }

        #endregion

        #region 一级数据下载
        /// <summary>
        /// 3.一级数据下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Datadown_UploadStringCompleted(object sender, System.Net.UploadStringCompletedEventArgs e)
        {
            // 判断结果
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (string.IsNullOrEmpty(resultstr)) return;
            // 解析迅播数据
            JieXiOne(resultstr, e.UserState as LiuXingType);
        }

        /// <summary>
        /// 3.一级数据下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Datadown_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            // 判断结果
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            var iType = e.UserState as LiuXingType;
            if (iType == null) return;
            // 这里做DyfmSearchItem的递归解析
            if (!string.IsNullOrEmpty(iType.Sign) && iType.Sign.Contains("DyfmSearchItemDOCTYPE"))
            {
                var apitxt = JsonMapper.ToObject<DianyingFmApi>(resultstr.Replace("\n", ""));
                if (apitxt == null) return;
                resultstr = apitxt.html;
            }
            if (string.IsNullOrEmpty(resultstr)) return;
            // 解析迅播数据
            JieXiOne(resultstr, iType);
        } 
        #endregion

        #region 一级数据解析
        /// <summary>
        /// 4.一级数据解析
        /// </summary>
        /// <param name="resultstr"></param>
        /// <param name="iType"></param>
        private static void JieXiOne(string resultstr, LiuXingType iType)
        {
            var zuiReDatas = new System.Collections.Generic.List<LiuXingData>();
            switch (iType.Type)
            {
                // 迅播影院正常列表
                case LiuXingEnum.XunboListItem:
                case LiuXingEnum.XunboSearchItem:
                    {
                        #region case LiuXingEnum.XunboListItem:

                        // 得到 <ul class=\"piclist\"> ~ </ul> 
                        var orignlis = StringRegexHelper.GetSingle(resultstr, "<ul class=\"piclist\">", "</ul>");
                        if (string.IsNullOrEmpty(orignlis))
                        {
                            return;
                        }
                        // 得到 <li> ~ </li>
                        var orignli = StringRegexHelper.GetValue(orignlis, "<li>", "</li>");
                        if (orignli == null || orignli.Count <= 0) return;
                        for (var i = 0; i < orignli.Count; i++)
                        {
                            var celllistr = orignli[i];
                            if (string.IsNullOrEmpty(celllistr)) continue;
                            var tempcell = DataTagHelper.AnalyzeData(celllistr, iType);
                            if (tempcell != null)
                            {
                                zuiReDatas.Add(tempcell);
                            }
                        }
                        // 开始下载图片
                        StartImageDown(zuiReDatas, iType);
                        return;
                        #endregion
                    }
                // 人人影视正常列表
                case LiuXingEnum.YYetListItem:
                    {
                        #region case LiuXingEnum.YYetListItem:

                        // 得到<ul class="boxPadd dashed"> ~ </ul>
                        var orignlis = StringRegexHelper.GetSingle(resultstr, "<ul class=\"boxPadd dashed\">", "</ul>");
                        if (string.IsNullOrEmpty(orignlis))
                        {
                            return;
                        }
                        // 得到 <li> ~ </li>
                        var orignli = StringRegexHelper.GetValue(orignlis, "<li ", "</li>");
                        if (orignli == null || orignli.Count <= 0)
                        {
                            return;
                        }
                        for (var i = 0; i < orignli.Count; i++)
                        {
                            var celllistr = orignli[i];
                            if (!string.IsNullOrEmpty(celllistr))
                            {
                                var tag = DataTagHelper.AnalyzeData(celllistr, iType);
                                if (tag != null)
                                {
                                    zuiReDatas.Add(tag);
                                }
                            }
                        }
                        // 开始下载图片
                        StartImageDown(zuiReDatas, iType);
                        return;
                        #endregion
                    }
                case LiuXingEnum.YYetSearchItem:
                    {
                        #region case LiuXingEnum.YYetSearchItem:
                        if (!string.IsNullOrEmpty(iType.Sign) && iType.Sign.Contains("YYetSearchSecond"))
                        {
                            // 解析影视资料页的数据并生成模型
                            var tag = DataTagHelper.AnalyzeData(resultstr, iType);
                            if (tag == null) return;
                            if (!string.IsNullOrEmpty(tag.Img))
                            {
                                using (
                                    var imgdown = new System.Net.WebClient
                                    {
                                        Encoding = iType.Encoding,
                                        Proxy = iType.Proxy
                                    })
                                {
                                    iType.Data = tag;
                                    imgdown.DownloadDataAsync(new System.Uri(tag.Img), iType);
                                    imgdown.DownloadDataCompleted += Imgdown_DownloadDataCompleted;
                                }
                            }
                        }
                        else
                        {
                            // 得到<ul class=\"allsearch dashed boxPadd6\"> ~ </ul>
                            string orignlis = StringRegexHelper.GetSingle(resultstr, "<ul class=\"allsearch dashed boxPadd6\">", "</ul>");
                            if (string.IsNullOrEmpty(orignlis))
                            {
                                return;
                            }

                            // 得到 <li> ~ </li>
                            var orignli = StringRegexHelper.GetValue(orignlis, "<a href=\"", "\" target=\"_blank\">");
                            if (orignli == null || orignli.Count <= 0) return;

                            // 解析数据
                            for (int i = 0; i < orignli.Count; i++)
                            {
                                iType.Sign = "YYetSearchSecond";
                                StartList(orignli[i], iType);
                            }
                        }

                        return;
                        #endregion
                    }
                case LiuXingEnum.PiaoHuaSearchItem:
                    {
                        #region case LiuXingEnum.PiaoHuaSearchItem:
                        if (!string.IsNullOrEmpty(iType.Sign) && iType.Sign.Contains("PiaoHuaSearchSecond"))
                        {
                            // 解析影视资料页的数据并生成模型
                            var tag = DataTagHelper.AnalyzeData(resultstr, iType);
                            if (tag == null) return;
                            if (!string.IsNullOrEmpty(tag.Img))
                            {
                                using (
                                    var imgdown = new System.Net.WebClient
                                    {
                                        Encoding = iType.Encoding,
                                        Proxy = iType.Proxy
                                    })
                                {
                                    iType.Data = tag;
                                    imgdown.DownloadDataAsync(new System.Uri(tag.Img), iType);
                                    imgdown.DownloadDataCompleted += Imgdown_DownloadDataCompleted;
                                }
                            }
                        }
                        else
                        {
                            // 得到<ul class=\"allsearch dashed boxPadd6\"> ~ </ul>
                            string orignlis = StringRegexHelper.GetSingle(resultstr, "<ul class=\"relist clearfix\">", "</ul>");
                            if (string.IsNullOrEmpty(orignlis))
                            {
                                return;
                            }

                            // 得到 <li> ~ </li>
                            var orignli = StringRegexHelper.GetValue(orignlis, "<li>", "</li>");
                            if (orignli == null || orignli.Count <= 0) return;
                            // 得到Url列表
                            var listurls = new System.Collections.Generic.List<string>();
                            foreach (var v in orignli)
                            {
                                var orign = StringRegexHelper.GetSingle(v, "<div class=\"minfo_op\"><a href=\"", "\" class=\"info\">下载");
                                if (!string.IsNullOrEmpty(orign))
                                {
                                    listurls.Add(orign);
                                }
                            }
                            if (listurls.Count <= 0) return;
                            // 解析数据
                            foreach (var listurl in listurls)
                            {
                                var listtemp = listurl;
                                if (!string.IsNullOrEmpty(listtemp))
                                {
                                    iType.Sign = "PiaoHuaSearchSecond";
                                    StartList(listurl, iType);
                                }
                            }
                        }

                        return;
                        #endregion
                    }
                case LiuXingEnum.DyfmSearchItem:
                    {
                        #region case LiuXingEnum.DyfmSearchItem:
                        if (!string.IsNullOrEmpty(iType.Sign) && iType.Sign.Contains("DyfmSecond"))
                        {
                            // 解析影视资料页的数据并生成模型
                            var tag = DataTagHelper.AnalyzeData(resultstr, iType);
                            if (tag == null) return;
                            if (!string.IsNullOrEmpty(tag.Img))
                            {
                                using (
                                    var imgdown = new System.Net.WebClient
                                    {
                                        Encoding = iType.Encoding,
                                        Proxy = iType.Proxy
                                    })
                                {
                                    iType.Data = tag;
                                    imgdown.DownloadDataAsync(new System.Uri(tag.Img), iType);
                                    imgdown.DownloadDataCompleted += Imgdown_DownloadDataCompleted;
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(iType.Sign) && iType.Sign.Contains("DyfmSearchItemDOCTYPE"))
                            {
                                // 得到 <li> ~ </li>
                                var orignli = StringRegexHelper.GetValue(resultstr, "<li>", "</li>");
                                if (orignli == null || orignli.Count <= 0) return;
                                // 得到影片页地址
                                var urls = new System.Collections.Generic.List<string>();
                                for (var i = 0; i < orignli.Count; i++)
                                {
                                    var urlkey = StringRegexHelper.GetSingle(orignli[i], "<a target=\"_blank\" href=\"", "\">");
                                    if (!string.IsNullOrEmpty(urlkey))
                                    {
                                        var urltemp = "http://dianying.fm" + urlkey;
                                        urls.Add(urltemp);
                                    }
                                }
                                if (urls.Count > 0)
                                {
                                    foreach (string url in urls)
                                    {
                                        if (!string.IsNullOrEmpty(url))
                                        {
                                            iType.Sign = "DyfmSecond";
                                            StartList(url, iType);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // 得到<ul class=\"x-movie-list nav nav-pills\" style=\"padding-top:0;\"> ~ </ul>
                                string orignlis = StringRegexHelper.GetSingle(resultstr, "var apiURL = '", "'");
                                orignlis = string.Format("http://dianying.fm/{0}?page=1", orignlis);
                                if (string.IsNullOrEmpty(orignlis))
                                {
                                    return;
                                }
                                iType.Sign = "DyfmSearchItemDOCTYPE";
                                StartList(orignlis, iType);
                            }
                        }

                        return;
                        #endregion
                    }
                case LiuXingEnum.TorrentKittySearchItem:
                    {
                        #region case LiuXingEnum.TorrentKittySearchItem:
                        // 得到<ul class=\"allsearch dashed boxPadd6\"> ~ </ul>
                        string orignlis = StringRegexHelper.GetSingle(resultstr, "<table id=\"archiveResult\">", "</table>");
                        orignlis = orignlis.Replace("<tbody>", "");
                        if (string.IsNullOrEmpty(orignlis))
                        {
                            return;
                        }
                        // 得到 <li> ~ </li>
                        var orignli = StringRegexHelper.GetValue(orignlis, "Detail", "Open");
                        if (orignli == null || orignli.Count <= 0) return;

                        // 解析数据
                        foreach (string v in orignli)
                        {
                            LiuXingData tag = DataTagHelper.AnalyzeData(v, iType);
                            if (tag != null)
                            {
                                zuiReDatas.Add(tag);
                            }
                        }
                        // 开始下载图片
                        StartImageDown(zuiReDatas, iType);
                        return;
                        #endregion
                    }
                case LiuXingEnum.M1905ComListItem:
                    {
                        #region case LiuXingEnum.M1905ComListItem:
                        if (resultstr.Contains("flashurl"))
                        {
                            var tag = DataTagHelper.AnalyzeData(resultstr, iType);
                            zuiReDatas.Add(tag);
                        }
                        else
                        {
                            var films = StringRegexHelper.GetValue(resultstr, "<film>", "</film>");
                            if (films == null || films.Count <= 0) return;
                            foreach (var film in films)
                            {
                                var tag = DataTagHelper.AnalyzeData(film, iType);
                                zuiReDatas.Add(tag);
                            }
                        }
                        // 开始下载图片
                        StartImageDown(zuiReDatas, iType);
                        return;
                        #endregion
                    }
                case LiuXingEnum.LuYiXia:
                    {
                        #region  case LiuXingEnum.LuYiXia:
                        if (string.IsNullOrEmpty(resultstr))
                        {
                            AutoCloseDlg.ShowMessageBoxTimeout(@"噢噢！众人一起撸，管子都断了，捏捏泥鳅等修复！", @"亲，不好意思", System.Windows.Forms.MessageBoxButtons.OK, 1000);
                            return;
                        }
                        var urllists = new System.Collections.Generic.List<string>
                        {
                            StringRegexHelper.GetSingle(resultstr, "\"Url\":\"", "\",\"Gcid\":")
                        };
                        if (urllists.Count <= 0)
                        {
                            AutoCloseDlg.ShowMessageBoxTimeout(@"噢噢！众人一起撸，管子都断了，捏捏泥鳅等修复！", @"亲，不好意思", System.Windows.Forms.MessageBoxButtons.OK, 1000);
                            return;
                        }
                        VodCopyHelper.StartToVod(urllists, new LiuXingData());
                        return;
                        #endregion
                    }
                case LiuXingEnum.ZhangYuSearchItem:
                    {
                        #region case LiuXingEnum.ZhangYuSearchItem:

                        var zhangyuapi = JsonMapper.ToObject<ZhangYuApi>(resultstr);
                        if (zhangyuapi != null)
                        {
                            var zhangyuapihtml = zhangyuapi.html;
                            if (!string.IsNullOrEmpty(zhangyuapihtml))
                            {
                                var dataurls = StringRegexHelper.GetValue(zhangyuapihtml, "<span class=\"p reslink\"", "<span class");
                                if (dataurls != null && dataurls.Count > 0)
                                {
                                    foreach (var dataurl in dataurls)
                                    {
                                        if (!string.IsNullOrEmpty(dataurl))
                                        {
                                            var url = StringRegexHelper.GetSingle(dataurl, "data-url=\"", "\"");
                                            if (!string.IsNullOrEmpty(url))
                                            {
                                                if (url.Contains("_id=") && url.Contains("&"))
                                                {
                                                    url = StringRegexHelper.GetSingle(url, "id=", "&");
                                                    if (!string.IsNullOrEmpty(url))
                                                    {
                                                        var name = StringRegexHelper.GetSingle(dataurl, "data-title=\"", "\"");
                                                        if (!string.IsNullOrEmpty(name))
                                                        {
                                                            name = UrlCodeHelper.GetClearVideoName(name);
                                                            url = "magnet:?xt=urn:btih:" + url;
                                                            zuiReDatas.Add(new LiuXingData
                                                            {
                                                                Name = name,
                                                                HDs = QualityHelper.GetHdsSign(name),
                                                                Drl = new System.Collections.Generic.List<string> { url },
                                                                Img = "http://www.qq7.com/uploads/allimg/120510/1s31110w-31.jpg"
                                                            });
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (zuiReDatas.Count > 0)
                                    {
                                        // 开始下载图片
                                        StartImageDown(zuiReDatas, iType);
                                    }

                                }
                            }
                        }


                        return; 
                        #endregion
                    }
                case LiuXingEnum.DyfmHotApi:
                    {
                        #region case LiuXingEnum.DyfmHotApi:
                        var tag = DataTagHelper.AnalyzeData(resultstr, iType);
                        if (tag != null)
                        {
                            zuiReDatas.Add(tag);
                        }
                        if (zuiReDatas.Count > 0)
                        {
                            StartImageDown(zuiReDatas,iType);
                        }

                        return; 
                        #endregion
                    }
                case LiuXingEnum.EverybodyWatch:
                    {
                        #region case LiuXingEnum.EverybodyWatch:
                        // 二次请求数据
                        if (resultstr.Contains("bigshot_url"))
                        {
                            //var oldimg = iType.Data.Img;
                            var newimg = StringRegexHelper.GetSingle(resultstr, "\"bigshot_url\": \"", "\"}");
                            if (!string.IsNullOrEmpty(newimg))
                            {
                                iType.Data.Img = newimg;
                                if (!string.IsNullOrEmpty(iType.Data.Img))
                                {
                                    StartImageDown(iType.Data, newimg, iType);
                                }

                            }
                        }
                        else
                        {
                            // 一次请求数据
                            System.Collections.Generic.List<ApiItem> apiItems;
                            try
                            {
                                apiItems = JsonMapper.ToObject<System.Collections.Generic.List<ApiItem>>(resultstr);
                            }
                            catch
                            {
                                apiItems = null;
                            }
                            if (apiItems == null || apiItems.Count <= 0) return;
                            foreach (var apiItem in apiItems)
                            {
                                LiuXingData tag;
                                try
                                {
                                    tag = new LiuXingData
                                    {
                                        Name = apiItem.MovieName,
                                        Img =
                                            string.Format("http://i.vod.xunlei.com/req_screenshot?req_list={0}",
                                                apiItem.Gcid),
                                        HDs = "未知",
                                        Drl = new System.Collections.Generic.List<string> { apiItem.Url }
                                    };
                                }
                                catch (System.Exception)
                                {
                                    tag = null;
                                }
                                if (tag != null)
                                {
                                    zuiReDatas.Add(tag);
                                }
                            }
                            if (zuiReDatas.Count <= 0) return;
                            foreach (var zuiReData in zuiReDatas)
                            {
                                iType.Data = zuiReData;
                                StartList(zuiReData.Img, iType);
                            }
                        }
                        return; 
                        #endregion
                    }
            }
        } 


        #endregion

        #region 图片下载
        /// <summary>
        /// 5.图片下载
        /// </summary>
        /// <param name="zuiReDatas"></param>
        /// <param name="iType"></param>
        private static void StartImageDown(System.Collections.Generic.List<LiuXingData> zuiReDatas, LiuXingType iType)
        {
            if (zuiReDatas.Count <= 0)
            {
                return;
            }
            // 遍历数据中的图片
            for (var i = 0; i < zuiReDatas.Count; i++)
            {
                if (iType.Type == LiuXingEnum.M1905ComListItem)
                {
                    if (!string.IsNullOrEmpty(zuiReDatas[i].Url))
                    {
                        iType.Sign = "M1905Second," + zuiReDatas[i].Url;
                        StartList(UrlCodeHelper.GetListHttpPath(0, 0, iType), iType);
                    }
                    else
                    {
                        StartImageDown(zuiReDatas[i], zuiReDatas[i].Img, iType);
                    }

                }
                else
                {
                    StartImageDown(zuiReDatas[i], zuiReDatas[i].Img, iType);
                }
            }
        }
        /// <summary>
        /// 5.图片下载
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="imageurl"></param>
        /// <param name="iType"></param>
        public static void StartImageDown(LiuXingData tag, string imageurl, LiuXingType iType)
        {
            if (string.IsNullOrEmpty(imageurl)) return;
            using (
                var imgdown = new System.Net.WebClient
                {
                    Encoding = iType.Encoding,
                    Proxy = iType.Proxy
                })
            {
                if (!string.IsNullOrEmpty(imageurl))
                {
                    var iClass = new LiuXingType
                    {
                        Encoding = iType.Encoding,
                        Proxy = iType.Proxy,
                        Type = iType.Type,
                        Data = tag
                    };
                    var image = FileCachoHelper.ImageCacho(imageurl);
                    if (image != null)
                    {
                        iClass.Img = image;
                        GoToDisPlay(iClass);
                    }
                    else
                    {
                        try
                        {
                            var imguri = new System.Uri(imageurl);
                            imgdown.Headers.Add(System.Net.HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1581.2 Safari/537.36");
                            imgdown.DownloadDataAsync(imguri, iClass);
                            imgdown.DownloadDataCompleted += Imgdown_DownloadDataCompleted;
                        }
                        // ReSharper disable EmptyGeneralCatchClause
                        catch
                        // ReSharper restore EmptyGeneralCatchClause
                        {
                            // System.Windows.Forms.MessageBox.Show(exception.Message+zuiReDatas[i].Name+zuiReDatas[i].Img+i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 6.下载影片海报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Imgdown_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (resultstr == null) return;
            var tag = e.UserState as LiuXingType;
            if (tag == null) return;
            // 转换成图片
            tag.Img = FileImageHelper.GetImageFromByte(resultstr);
            // 缓存图片
            FileCachoHelper.CachoImage(tag.Data.Img, tag.Img);
            GoToDisPlay(tag);
        } 
        #endregion

        #region 显示影片信息
        /// <summary>
        /// 7.显示影片信息
        /// </summary>
        /// <param name="tag"></param>
        private static void GoToDisPlay(LiuXingType tag)
        {
            if (tag != null)
            {
                if (tag.Type == LiuXingEnum.DyfmHotApi)
                {
                    PublicStatic.ThisHot.BackgroundImage = tag.Img;

                }
                else
                {
                    switch (PublicStatic.DisPlayStyle)
                    {
                        case LiuXingStyle.DisPlayTile:
                            {
                                MainInterFace.Owner.Parent.Invoke(
                                new System.Windows.Forms.MethodInvoker
                                    (() =>
                                    {
                                        var ssss = new MetroForTile(tag);
                                        if (PublicStatic.LiuXingCon != null && PublicStatic.LiuXingCon.Visible)
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
                            break;
                    }
                }
            }
        } 
        #endregion
    }
}
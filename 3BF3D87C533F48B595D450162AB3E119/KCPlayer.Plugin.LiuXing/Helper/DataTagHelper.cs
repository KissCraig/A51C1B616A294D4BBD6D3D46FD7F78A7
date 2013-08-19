using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class DataTagHelper
    {
        /// <summary>
        /// AnalyzeData - 解析数据数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public static LiuXingData AnalyzeData(string celllistr, LiuXingType iType)
        {
            #region AnalyzeData - 解析数据数据模型

            if (string.IsNullOrEmpty(celllistr)) return null;
            if (iType == null) return null;
            var cellitem = new LiuXingData();
            try
            {
                switch (iType.Type)
                {
                    // 迅播影院正常列表
                    case LiuXingEnum.XunboSearchItem:
                    case LiuXingEnum.XunboListItem:
                        {
                            cellitem = GetXunboListItem(celllistr);
                        }
                        break;
                    // 人人影视正常列表
                    case LiuXingEnum.YYetListItem:
                        {
                            cellitem = GetYYetListItem(celllistr);
                        }
                        break;
                    case LiuXingEnum.DyfmSearchItem:
                        {
                            cellitem = GetDianYingFmItem(celllistr);
                        }
                        break;
                    case LiuXingEnum.PiaoHuaSearchItem:
                        {
                            cellitem = GetPiaoHuaSearchItem(celllistr);
                        }
                        break;
                    case LiuXingEnum.YYetSearchItem:
                        {
                            cellitem = GetYYetSearchItem(celllistr);
                        }
                        break;
                    case LiuXingEnum.TorrentKittySearchItem:
                        {
                            cellitem = GetTorrentKittySearchItem(celllistr);
                        }
                        break;
                    case LiuXingEnum.M1905ComListItem:
                        {
                            cellitem = GetM1905ComListItem(celllistr);
                        }
                        break;
                    case LiuXingEnum.DyfmHotApi:
                        {
                            cellitem = GetDyFmHotApi(celllistr);
                        }
                        break;
                }
                // 清理不必要的链接
                cellitem.Drl = UrlCodeHelper.GetDecodeList(cellitem.Drl);
                return cellitem;
            }
            catch
            {
                return null;
            }
            #endregion
        }

        #region GetDyFmHotApi
        /// <summary>
        /// 取回DyFmHotApi的数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        private static LiuXingData GetDyFmHotApi(string celllistr)
        {
            #region 取回DyFmHotApi的数据模型
            var cellitem = new LiuXingData();
            var img = StringRegexHelper.GetSingle(celllistr, "http://poster.dianying.fm/movie/", "/bdmt/720");
            if (!string.IsNullOrEmpty(img))
            {
                img = string.Format("http://poster.dianying.fm/movie/{0}/bdmt/720", img);
                // 电影海报
                cellitem.Img = img;
            }
            var detail = StringRegexHelper.GetSingle(celllistr, "id=\"x-kankan-detail\"", "id=\"mark\"");
            if (!string.IsNullOrEmpty(detail))
            {
                var info = StringRegexHelper.GetSingle(celllistr, "<div class=\"x-kankan-full-desc\" style=\"display: none;\">", "</div>");
                if (!string.IsNullOrEmpty(info))
                {
                    // 电影简介
                    cellitem.Des = info.Replace("\n", "");
                }
                var cars = StringRegexHelper.GetSingle(celllistr, "x-kankan-starring\">", "</p>");
                if (!string.IsNullOrEmpty(cars))
                {
                    // 电影演员
                    cellitem.Car = cars.Replace("主演：", "").Replace("/", "、").Replace(" ", "");
                }
                var coss = StringRegexHelper.GetSingle(celllistr, "豆瓣", "<span");
                if (!string.IsNullOrEmpty(coss))
                {
                    coss = coss.Replace("\n\n", "").Replace(" ", "");
                    // 电影评分
                    cellitem.Cos = coss;
                }
                var typs = StringRegexHelper.GetSingle(celllistr, "<p class=\"muted\"", "/p>\n<p class=\"muted x-kankan-starring");
                if (!string.IsNullOrEmpty(typs))
                {
                    if (typs.Contains(">"))
                    {
                        typs = StringRegexHelper.GetSingle(typs, ">", "<");
                        cellitem.Typ = typs.Replace("/", "、").Replace(" ", "");
                    }

                }
            }
            return cellitem;
            #endregion
        } 
        #endregion

        #region GetXunboListItem
        /// <summary>
        /// 取回XunBoList的数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        private static LiuXingData GetXunboListItem(string celllistr)
        {
            #region 取回XunBoList的数据模型
            var cellitem = new LiuXingData();
            // 电影名称
            string tempname = StringRegexHelper.GetSingle(celllistr, "alt=\"", "\" /><em");
            if (!string.IsNullOrEmpty(tempname))
            {
                var nametemp = tempname.Replace("：", "");
                if (nametemp.Length > 7)
                {
                    cellitem.Name = nametemp.Substring(0, 7);
                }
                else
                {
                    cellitem.Name = nametemp;
                }

            }
            // 电影网址
            string tempurl = StringRegexHelper.GetSingle(celllistr, "<a href=\"", "\" class=\"i\"><img");
            if (!string.IsNullOrEmpty(tempurl))
            {
                cellitem.Url = PublicStatic.LiuXingYuName + tempurl;
            }
            // 电影封面
            string tempimg = StringRegexHelper.GetSingle(celllistr, "src=\"", "\" alt=");
            if (!string.IsNullOrEmpty(tempimg))
            {
                cellitem.Img = tempimg;
            }
            // 电影质量
            string tempHDs = StringRegexHelper.GetSingle(celllistr, "class=\"v\">", "</em></a");
            if (!string.IsNullOrEmpty(tempHDs))
            {
                cellitem.HDs = tempHDs;
            }
            // 电影评分
            string tempCos = StringRegexHelper.GetSingle(celllistr, "<em class=\"fenshu\">", "</sup></em>");
            if (!string.IsNullOrEmpty(tempCos))
            {
                cellitem.Cos = tempCos.Replace("<sup>", "");
            }
            // 电影地区
            string tempLoc = StringRegexHelper.GetSingle(celllistr, "<b>地区：", "</b></p>");
            if (!string.IsNullOrEmpty(tempLoc))
            {
                cellitem.Loc = tempLoc.Replace(" ", "");
            }
            // 电影年代
            string tempTim = StringRegexHelper.GetSingle(celllistr, "</a><em>", "</em></h1>");
            if (!string.IsNullOrEmpty(tempTim))
            {
                cellitem.Tim = tempTim.Replace(" ", "");
            }
            // 电影演员
            string tempCar = StringRegexHelper.GetSingle(celllistr, "<p>主演：", "</p>");
            if (!string.IsNullOrEmpty(tempCar))
            {
                cellitem.Car = tempCar.Replace(',', '、').Replace("#8226;", "");
            }
            // 电影类型
            string tempTyp = StringRegexHelper.GetSingle(celllistr, "类型：", "</b><b>");
            if (!string.IsNullOrEmpty(tempTyp))
            {
                cellitem.Typ = tempTyp;
            }
            // 电影更新
            string tempUpt = StringRegexHelper.GetSingle(celllistr, "<b>更新：", "</b></p>");
            if (!string.IsNullOrEmpty(tempUpt))
            {
                cellitem.Upt = tempUpt.Replace("-", "-");
            }
            return cellitem;
            #endregion
        } 
        #endregion

        #region GetYYetListItem
        /// <summary>
        /// 取回YYetList的数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        private static LiuXingData GetYYetListItem(string celllistr)
        {
            #region 取回YYetList的数据模型
            var cellitem = new LiuXingData();

            // 影片大类
            string tempmpe = StringRegexHelper.GetSingle(celllistr, ">【", "】<strong>");
            if (!string.IsNullOrEmpty(tempmpe))
            {
                cellitem.Mpe = tempmpe;
            }
            // 电影名称
            string tempname = StringRegexHelper.GetSingle(celllistr, "<strong>", "》");
            if (!string.IsNullOrEmpty(tempname))
            {
                cellitem.Name = tempname.Replace("《", "")
                                        .Replace("【", "")
                                        .Replace("】", "")
                                        .Replace("》", "");
            }
            // 电影网址
            string tempurl = StringRegexHelper.GetSingle(celllistr, "href=\"", "\"><img");
            if (!string.IsNullOrEmpty(tempurl))
            {
                cellitem.Url = tempurl;
            }
            // 电影封面
            string tempimg = StringRegexHelper.GetSingle(celllistr, "<img src=\"", "\"></a>");
            if (!string.IsNullOrEmpty(tempimg))
            {
                cellitem.Img = tempimg.Replace("m_", "b_");
            }
            // 电影质量
            const string tempHDs = @"HR-HDTV";
            if (!string.IsNullOrEmpty(tempHDs))
            {
                cellitem.HDs = tempHDs;
            }
            // 电影评分
            string tempCos = StringRegexHelper.GetSingle(celllistr, "【人气】</font>", "分</span>");
            if (!string.IsNullOrEmpty(tempCos))
            {
                string[] tempCoss = tempCos.Split('|');
                if (tempCoss.Length >= 3)
                {
                    cellitem.Cos = tempCos.Split('|')[2].Trim().Substring(0, 3);
                }
            }
            // 电影地区
            string tempLoc = StringRegexHelper.GetSingle(celllistr, "【说明】", "</span><span><font");
            if (!string.IsNullOrEmpty(tempLoc))
            {
                cellitem.Loc = UrlCodeHelper.GetVideoLocation(tempLoc);
            }
            // 电影年代
            string tempTim = StringRegexHelper.GetSingle(celllistr, "》", "</strong>");
            if (!string.IsNullOrEmpty(tempTim))
            {
                cellitem.Tim = tempTim;
            }
            // 电影演员
            string tempCar = StringRegexHelper.GetSingle(celllistr, "【说明】</font>", "</span><span><f");
            if (!string.IsNullOrEmpty(tempCar))
            {
                cellitem.Car = tempCar.Length >= 25 ? tempCar.Substring(0, 25) : tempCar;
            }
            // 电影类型
            string tempTyp = StringRegexHelper.GetSingle(celllistr, "【类型】</font>", "</span><span><f");
            if (!string.IsNullOrEmpty(tempTyp))
            {
                if (!tempTyp.Contains("/"))
                {
                    cellitem.Typ = tempTyp;
                }
                else
                {
                    string[] txts = tempTyp.Split('/');
                    cellitem.Typ = txts.Length > 0 ? txts[0].Trim() : "";
                }
            }
            // 电影更新
            string tempUpt = StringRegexHelper.GetSingle(celllistr, "【更新】</font>", "</span></dd>");
            if (!string.IsNullOrEmpty(tempUpt))
            {
                string[] tempUpts = tempUpt.Split('|');
                if (tempUpts.Length >= 1)
                {
                    cellitem.Upt = tempUpt.Split('|')[0].Trim().Substring(5, 5).Replace("-", "~");
                }
            }
            return cellitem;
            #endregion
        } 
        #endregion

        #region GetDianYingFmItem
        /// <summary>
        /// 取回DianYingFM的数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        private static LiuXingData GetDianYingFmItem(string celllistr)
        {
            #region 取回DianYingFM的数据模型
            var cellitem = new LiuXingData();
            // 电影名称
            string tempname = StringRegexHelper.GetSingle(celllistr, "<div class=\"x-m-title\">", "</div>");
            if (!string.IsNullOrEmpty(tempname))
            {
                string namewithyear = tempname.Replace("\n", "");
                string[] names = namewithyear.Split('<');
                if (names.Length > 0)
                {
                    string[] namss = names[0].Split(' ');
                    if (namss.Length > 0)
                    {
                        cellitem.Name = namss[0];
                    }
                }
                // 电影年代
                string temptim = StringRegexHelper.GetSingle(tempname, "class=\"muted\">", "</span>");
                if (!string.IsNullOrEmpty(temptim))
                {
                    cellitem.Tim = temptim.Replace("(", "").Replace(")", "").Replace(" ", "");
                }
            }
            // 电影封面
            string tempimg = StringRegexHelper.GetSingle(celllistr, "<div class=\"x-m-poster\">", "</div>");
            if (!string.IsNullOrEmpty(tempimg))
            {
                tempimg = StringRegexHelper.GetSingle(tempimg, "src=\"", "\">");
                if (!string.IsNullOrEmpty(tempimg))
                {
                    cellitem.Img = "http://dianying.fm" + tempimg;
                }
            }
            string tempinfo = StringRegexHelper.GetSingle(celllistr,
                                                     "<table class=\"table table-condensed table-striped table-bordered\"",
                                                     "</table>");

            if (!string.IsNullOrEmpty(tempinfo))
            {
                var temps = StringRegexHelper.GetValue(tempinfo, "<!-- <td> ", " </td>-->");
                if (temps.Count > 0)
                {
                    if (temps.Count >= 2)
                    {
                        // 电影演员
                        string tempcar = temps[1].Replace(" / ", "、").Replace(" ", "");
                        if (!string.IsNullOrEmpty(tempcar))
                        {
                            cellitem.Car = tempcar;
                        }
                    }
                    if (temps.Count >= 3)
                    {
                        // 电影类型
                        string tempTyp = temps[2].Replace(" / ", "、").Replace(" ", "");
                        if (!string.IsNullOrEmpty(tempTyp))
                        {
                            cellitem.Typ = tempTyp;
                            if (cellitem.Typ.Length >= 2)
                            {
                                cellitem.Typ = cellitem.Typ.Substring(0, 2);
                            }
                        }
                    }
                    if (temps.Count >= 4)
                    {
                        // 电影地区
                        string tempLoc = temps[3].Replace(" / ", "、").Replace(" ", "");
                        if (!string.IsNullOrEmpty(tempLoc))
                        {
                            cellitem.Loc = tempLoc.Replace(" ", "");
                            if (cellitem.Loc.Length >= 2)
                            {
                                cellitem.Loc = cellitem.Loc.Substring(0, 2);
                            }
                        }
                    }
                    if (temps.Count >= 5)
                    {
                        string tempUpt = temps[4].Replace(" / ", "、").Replace(" ", "");
                        if (!string.IsNullOrEmpty(tempUpt))
                        {
                            // 电影年份
                            if (tempUpt.Length >= 4)
                            {
                                cellitem.Tim = tempUpt.Substring(0, 4);
                            }
                            // 电影更新
                            if (tempUpt.Length >= 10)
                            {
                                tempUpt = tempUpt.Substring(5, 5);
                                cellitem.Upt = tempUpt.Replace("-", "~");
                            }
                        }
                    }
                }
            }
            // 影片得分
            string rangestr = StringRegexHelper.GetSingle(celllistr, "<tr class=\"x-m-rating\">", "</tr>");
            if (!string.IsNullOrEmpty(rangestr))
            {
                var ranges = StringRegexHelper.GetValue(rangestr, "font-weight: bold;\">", "</span></a>");
                if (ranges.Count > 0)
                {
                    System.Double count = 0;
                    foreach (string range in ranges)
                    {
                        count += System.Convert.ToDouble(range);
                    }
                    cellitem.Cos = (count / ranges.Count).ToString("###,###.0");
                }
                else
                {
                    cellitem.Cos = 5.5.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            // 影片链接
            var urls = StringRegexHelper.GetValue(celllistr, "<tr class=\"resources\" style=\"\">", "</tr>");
            if (urls != null)
            {
                if (urls.Count > 0)
                {
                    cellitem.Drl = new System.Collections.Generic.List<string>();
                    foreach (string url in urls)
                    {
                        if (string.IsNullOrEmpty(url)) continue;
                        string urlitem = StringRegexHelper.GetSingle(url, "url=\"", "\"");
                        if (!string.IsNullOrEmpty(urlitem))
                        {
                            cellitem.Drl.Add(System.Web.HttpUtility.UrlDecode(urlitem));
                        }
                    }
                }
            }
            cellitem.HDs = Helper.QualityHelper.GetHdsData(cellitem.Drl);
            return cellitem;
            #endregion
        } 
        #endregion

        #region GetYYetSearchItem
        /// <summary>
        /// 取回YYetSearch的数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        private static LiuXingData GetYYetSearchItem(string celllistr)
        {
            #region 取回YYetSearch的数据模型
            var cellitem = new LiuXingData();
            // 影片大类
            string tempmpe = StringRegexHelper.GetSingle(celllistr, "【", "】");
            if (!string.IsNullOrEmpty(tempmpe))
            {
                cellitem.Mpe = tempmpe;
            }
            // 电影名称
            string tempname = StringRegexHelper.GetSingle(celllistr, "<strong>", "<label id=\"play");
            if (!string.IsNullOrEmpty(tempname))
            {
                cellitem.Name = tempname
                    .Replace("【", "")
                    .Replace("】", "")
                    .Replace("《", "")
                    .Replace("》", "").Replace(cellitem.Mpe, "").Trim();
            }
            // 电影网址
            const string tempurl = "";
            if (!string.IsNullOrEmpty(tempurl))
            {
                cellitem.Url = tempurl;
            }
            // 电影封面
            string tempimg = StringRegexHelper.GetSingle(celllistr, "<img src=\"", "\" /></a>");
            if (!string.IsNullOrEmpty(tempimg))
            {
                cellitem.Img = tempimg.Replace("m_", "b_");
            }
            // 电影质量
            const string tempHDs = @"HR-HDTV";
            if (!string.IsNullOrEmpty(tempHDs))
            {
                cellitem.HDs = tempHDs;
            }
            // 电影评分
            const string tempCos = "6.5";
            if (!string.IsNullOrEmpty(tempCos))
            {
                cellitem.Cos = tempCos;
            }
            // 电影地区
            string tempLoc = StringRegexHelper.GetSingle(celllistr, "<span>地区：</span><strong", "</strong>");
            if (!string.IsNullOrEmpty(tempLoc))
            {
                cellitem.Loc = UrlCodeHelper.GetVideoLocation(tempLoc.Replace(">", ""));
            }
            // 电影年代
            string tempTim = StringRegexHelper.GetSingle(celllistr, "<span>年代：</span><strong>",
                                                    "</strong>             <font class=\"f5\">类");
            if (!string.IsNullOrEmpty(tempTim))
            {
                cellitem.Tim = tempTim;
            }
            // 电影演员
            const string tempCar = "";
            if (!string.IsNullOrEmpty(tempCar))
            {
                cellitem.Car = tempCar;
            }
            // 电影类型
            const string tempTyp = "";
            if (!string.IsNullOrEmpty(tempTyp))
            {
                cellitem.Typ = tempTyp;
            }
            // 电影更新
            const string tempUpt = "";
            if (!string.IsNullOrEmpty(tempUpt))
            {
                cellitem.Upt = tempUpt;
            }

            var tagurls = new System.Collections.Generic.List<string>();
            string urllists = celllistr;//LiuXingRegex.GetSingle(,"<ul class=\"resod_list\"","</ul>");
            if (string.IsNullOrEmpty(urllists)) return null;
            if (urllists.Contains("type=\"ed2k\""))
            {
                var orignli = StringRegexHelper.GetValue(urllists, "type=\"ed2k\" href=\"", "\"");
                if (orignli == null || orignli.Count <= 0) return null;
                foreach (string v in orignli)
                {
                    if (!tagurls.Contains(v))
                    {
                        tagurls.Add(v);
                    }
                }
            }
            if (urllists.Contains("thunder=\""))
            {
                var orignli = StringRegexHelper.GetValue(urllists, "thunder=\"", "\"");
                if (orignli == null || orignli.Count <= 0) return null;
                foreach (string v in orignli)
                {
                    if (!tagurls.Contains(v))
                    {
                        tagurls.Add(v);
                    }
                }
            }
            var yyets = new System.Collections.Generic.List<string>();
            foreach (string tagurl in tagurls)
            {
                if (!string.IsNullOrEmpty(tagurl))
                {
                    if (tagurl.StartsWith("ed2k") || tagurl.StartsWith("ED2K") || tagurl.StartsWith("http") ||
                        tagurl.StartsWith("magnet") || tagurl.StartsWith("thunder") || tagurl.StartsWith("flashget") ||
                        tagurl.StartsWith("flashget"))
                    {
                        yyets.Add(System.Web.HttpUtility.HtmlDecode(tagurl));
                    }
                }
            }
            cellitem.Drl = yyets;
            return cellitem;
            #endregion
        } 
        #endregion

        #region GetPiaoHuaSearchItem
        /// <summary>
        /// 取回PiaoHua的数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        private static LiuXingData GetPiaoHuaSearchItem(string celllistr)
        {
            #region 取回PiaoHua的数据模型
            var cellitem = new LiuXingData();
            // 影片大类
            string tempmpe = StringRegexHelper.GetSingle(celllistr, "<div id=\"location\">", "</div>");
            if (!string.IsNullOrEmpty(tempmpe))
            {
                var titless = StringRegexHelper.GetValue(tempmpe, ">", "<");
                if (titless.Count > 0)
                {
                    if (titless.Count >= 3)
                    {
                        string te = titless[2];
                        if (!string.IsNullOrEmpty(te))
                        {
                            // 大类
                            cellitem.Mpe = te;
                        }
                    }
                    if (titless.Count >= 5)
                    {
                        string ye = titless[4];
                        if (!string.IsNullOrEmpty(ye))
                        {
                            // 类型
                            cellitem.Typ = ye.Replace("片", "");
                        }
                    }
                    if (titless.Count >= 6)
                    {
                        string tt = titless[6];
                        if (!string.IsNullOrEmpty(tt))
                        {
                            // 姓名
                            cellitem.Name = tt.Trim().Replace(" ", "");
                        }
                    }
                }
            }
            // 图片
            string picImg = StringRegexHelper.GetSingle(celllistr, "<div class=\"moviepic\">", "</div>");
            if (!string.IsNullOrEmpty(picImg))
            {
                string imgurl = StringRegexHelper.GetSingle(picImg, "<img src=\"", "\" title=");
                if (!string.IsNullOrEmpty(imgurl))
                {
                    cellitem.Img = imgurl;
                }
            }
            // 电影演员
            var ifons = new System.Collections.Generic.List<string>();
            string info = StringRegexHelper.GetSingle(celllistr, "<div class=\"yycon\">", "</div>");
            if (!string.IsNullOrEmpty(info))
            {
                var infos = StringRegexHelper.GetValue(info, "target=\"_blank\">", "</a>");
                if (infos.Count > 0)
                {
                    foreach (string info1 in infos)
                    {
                        if (!string.IsNullOrEmpty(info1))
                        {
                            ifons.Add(info1);
                        }
                    }
                    if (ifons.Count > 0)
                    {
                        for (int i = 0; i < ifons.Count; i++)
                        {
                            if (i < 7)
                            {
                                cellitem.Car += ifons[i] + "、";
                            }
                        }
                        if (!string.IsNullOrEmpty(cellitem.Car))
                        {
                            cellitem.Car = cellitem.Car.Substring(0, cellitem.Car.Length - 1);
                        }
                    }
                }
            }
            // 电影地区
            string tempLoc = StringRegexHelper.GetSingle(celllistr, "<li><label>地区：</label>", "</li>");
            if (!string.IsNullOrEmpty(tempLoc))
            {
                cellitem.Loc = tempLoc.Replace(" ", "").Replace("\"", "");
            }
            // 电影年代
            string tempTim = StringRegexHelper.GetSingle(celllistr, "<li><label>年代：</label>", "</li>");
            if (!string.IsNullOrEmpty(tempTim))
            {
                cellitem.Tim = tempTim.Replace(" ", "").Replace("\"", "");
            }
            // 电影更新
            string tempUpt = StringRegexHelper.GetSingle(celllistr, "<li><label>时间：</label>", "</li>");
            if (!string.IsNullOrEmpty(tempUpt))
            {
                tempUpt = tempUpt.Replace(" ", "").Replace("\"", "");
                if (!string.IsNullOrEmpty(tempUpt))
                {
                    if (tempUpt.Length >= 11)
                    {
                        tempUpt = tempUpt.Substring(5, 5);
                        if (!string.IsNullOrEmpty(tempUpt))
                        {
                            cellitem.Upt = tempUpt.Replace("月", "~");
                        }
                    }
                }
            }
            // 电影质量
            string tempHDs = StringRegexHelper.GetSingle(celllistr, "<li><label>版本：</label>", "</li>");
            if (!string.IsNullOrEmpty(tempHDs))
            {
                cellitem.HDs = tempHDs.Replace(" ", "");
            }

            cellitem.Drl = new System.Collections.Generic.List<string>();
            string urllists = StringRegexHelper.GetSingle(celllistr, "<div class=\"zylistbox\">", "</div");
            if (string.IsNullOrEmpty(urllists)) return null;
            var orignli = StringRegexHelper.GetValue(urllists, "xzurl=", "'/><a");
            if (orignli == null || orignli.Count <= 0) return null;
            foreach (string v in orignli)
            {
                if (v.Contains("&"))
                {
                    string[] vv = v.Split('&');
                    if (vv.Length > 0)
                    {
                        cellitem.Drl.Add(vv[0]);
                    }
                }
                else
                {
                    cellitem.Drl.Add(v);
                }
            }
            return cellitem;
            #endregion
        } 
        #endregion

        #region GetTorrentKittySearchItem
        /// <summary>
        /// 取回TorrentKitty的数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        private static LiuXingData GetTorrentKittySearchItem(string celllistr)
        {
            #region 取回TorrentKitty的数据模型
            var cellitem = new LiuXingData();
            // 电影名称
            string tempname = StringRegexHelper.GetSingle(celllistr, "title=\"", "\" rel=\"magnet\">");
            if (!string.IsNullOrEmpty(tempname))
            {
                if (tempname.Contains("@"))
                {
                    string[] names = tempname.Split('@');
                    if (names.Length > 0)
                    {
                        string last = names[names.Length - 1];
                        if (!string.IsNullOrEmpty(last))
                        {
                            string name = last.Replace("草榴社区", "")
                                              .Replace("草榴社區", "")
                                              .Replace("@", "")
                                              .Replace("www.sexinsex.net", "")
                                              .Replace(".RMVB", "");
                            if (!string.IsNullOrEmpty(name))
                            {
                                cellitem.Name = name;
                            }
                        }
                    }
                }
                else
                {
                    cellitem.Name = tempname;
                }
            }
            // 电影链接
            string tempdrl = StringRegexHelper.GetSingle(celllistr, "<a href=\"", "\" title");
            if (!string.IsNullOrEmpty(tempdrl))
            {
                cellitem.Drl = new System.Collections.Generic.List<string> { tempdrl };
            }
            cellitem.HDs = Helper.QualityHelper.GetHdsData(cellitem.Drl);
            cellitem.Img = @"http://img2.2bbx.com/20713/07669943.jpg";
            return cellitem;
            #endregion
        } 
        #endregion

        #region GetM1905ComListItem
        /// <summary>
        /// 取回M1905的数据模型
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        private static LiuXingData GetM1905ComListItem(string celllistr)
        {
            #region 取回M1905的数据模型
            var cellitem = new LiuXingData();
            if (celllistr.Contains("flashurl"))
            {
                cellitem.Name = Helper.UrlCodeHelper.GetClearVideoName(ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<title>", "</title>")));
                cellitem.Img = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<bigpicUrl>", "</bigpicUrl>"));
                cellitem.Cos = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<grade>", "</grade>"));
                cellitem.Des = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<description>", "</description>"));
                cellitem.Car = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<starring>", "</starring>"));
                cellitem.Drl = new System.Collections.Generic.List<string>();
                var idrl = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<flashurl>", "</flashurl>"));
                if (!string.IsNullOrEmpty(idrl))
                {
                    cellitem.Drl.Add(idrl);
                }
                cellitem.Typ = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<mtype>", "</mtype>"));
                if (cellitem.Typ.Contains(","))
                {
                    cellitem.Typ = cellitem.Typ.Split(',')[0];
                }
                cellitem.Upt = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<pubDate>", "</pubDate>"));
                if (cellitem.Upt.Length > 7)
                {
                    cellitem.Tim = cellitem.Upt.Substring(0, 4);
                    cellitem.Upt = cellitem.Upt.Substring(5, 5).Replace("-", "~");
                }
                cellitem.Loc = "未知";
                cellitem.HDs = "1280";
            }
            else
            {
                cellitem.Name = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<title>", "</title>"));
                cellitem.Img = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<thumb>", "</thumb>"));
                cellitem.Cos = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<score>", "</score>"));
                cellitem.Url = ClearM1905ComXmlCdata(StringRegexHelper.GetSingle(celllistr, "<filmid>", "</filmid>"));
            }
            return cellitem; 
            #endregion
        }

        /// <summary>
        /// M1905 - 去杂质
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string ClearM1905ComXmlCdata(string origin)
        {
            return !string.IsNullOrEmpty(origin) ? origin.Replace("<![CDATA[", "").Replace("]]>", "") : origin;
        } 
        #endregion
    }
}

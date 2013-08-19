using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.LoadLiuXing.Dyfm
{
    public class SearchData
    {
        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        public static LiuXingData JieXiData(string celllistr)
        {
            try
            {
                if (string.IsNullOrEmpty(celllistr)) return null;
                var cellitem = new LiuXingData();
                // 电影名称
                var tempname = celllistr.GetSingle("<div class=\"x-m-title\">", "</div>");
                if (!string.IsNullOrEmpty(tempname))
                {
                    var namewithyear = tempname.Replace("\n", "");
                    var names = namewithyear.Split('<');
                    if (names.Length > 0)
                    {
                        var namss = names[0].Split(' ');
                        if (namss.Length > 0)
                        {
                            cellitem.Name = namss[0];
                        }
                    }
                    // 电影年代
                    var temptim = tempname.GetSingle("class=\"muted\">", "</span>");
                    if (!string.IsNullOrEmpty(temptim))
                    {
                        cellitem.Tim = temptim.Replace("(", "").Replace(")", "").Replace(" ", "");
                    }
                }
                // 电影封面
                var tempimg = celllistr.GetSingle("<div class=\"x-m-poster\">", "</div>");
                if (!string.IsNullOrEmpty(tempimg))
                {
                    tempimg = tempimg.GetSingle("src=\"", "\">");
                    if (!string.IsNullOrEmpty(tempimg))
                    {
                        cellitem.Img = tempimg;
                    }
                }
                var tempinfo =
                    celllistr.GetSingle("<table class=\"table table-condensed table-striped table-bordered\">",
                                        "</table>");

                if (!string.IsNullOrEmpty(tempinfo))
                {
                    var temps = tempinfo.GetValue("<!-- <td> ", " </td>-->");
                    if (temps.Count > 0)
                    {
                        if (temps.Count >= 2)
                        {
                            // 电影演员
                            var tempcar = temps[1].Replace(" / ", "、").Replace(" ", "");
                            if (!string.IsNullOrEmpty(tempcar))
                            {
                                cellitem.Car = tempcar;
                            }
                        }
                        if (temps.Count >= 3)
                        {
                            // 电影类型
                            var tempTyp = temps[2].Replace(" / ", "、").Replace(" ", "");
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
                            var tempLoc = temps[3].Replace(" / ", "、").Replace(" ", "");
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
                            var tempUpt = temps[4].Replace(" / ", "、").Replace(" ", "");
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
                var rangestr = celllistr.GetSingle("<tr class=\"x-m-rating\">", "</tr>");
                if (!string.IsNullOrEmpty(rangestr))
                {
                    var ranges = rangestr.GetValue("font-weight: bold;\">", "</span></a>");
                    if (ranges.Count > 0)
                    {
                        System.Double count = 0;
                        foreach (var range in ranges)
                        {
                            count += System.Convert.ToDouble(range);
                        }
                        cellitem.Cos = (count/ranges.Count).ToString("###,###.0");
                    }
                    else
                    {
                        cellitem.Cos = 5.5.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                // 影片链接
                var urls = celllistr.GetValue("<tr class=\"resources\" style=\"\">", "</tr>");
                if (urls != null)
                {
                    if (urls.Count > 0)
                    {
                        cellitem.Drl = new System.Collections.Generic.List<string>();
                        foreach (var url in urls)
                        {
                            if (string.IsNullOrEmpty(url)) continue;
                            var urlitem = url.GetSingle("url=\"", "\"");
                            if (!string.IsNullOrEmpty(urlitem))
                            {
                                cellitem.Drl.Add(System.Web.HttpUtility.UrlDecode(urlitem));
                            }
                        }
                    }
                }
                if (cellitem.Drl != null)
                {
                    var h1080 = Get1080P(cellitem.Drl);
                    if (!string.IsNullOrEmpty(h1080))
                    {
                        cellitem.HDs = h1080;
                    }
                    else
                    {
                        var h720 = Get720P(cellitem.Drl);
                        if (!string.IsNullOrEmpty(h720))
                        {
                            cellitem.HDs = h720;
                        }
                        else
                        {
                            var hrhdtv = GetHrhdtv(cellitem.Drl);
                            cellitem.HDs = !string.IsNullOrEmpty(hrhdtv) ? hrhdtv : "未知画质";
                        }
                    }
                }
                else
                {
                    cellitem.HDs = "暂无资源";
                }
                return cellitem;
            }
            catch (System.Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message + "1111111");
            }
            return null;
        }

        private static string GetHrhdtv(System.Collections.Generic.IEnumerable<string> drls)
        {
            foreach (var url in drls)
            {
                if (url.Contains("HR-HDTV") || url.Contains("Hr-HDTV"))
                {
                    return "HR-HDTV";
                }
            }
            return null;
        }

        private static string Get1080P(System.Collections.Generic.IEnumerable<string> drls)
        {
            foreach (var url in drls)
            {
                if (url.Contains("1080P") || url.Contains("1080p"))
                {
                    return "HD1080P";
                }
            }
            return null;
        }

        private static
            string Get720P(System.Collections.Generic.IEnumerable<string> drls)
        {
            foreach (var url in drls)
            {
                if (url.Contains("720P") || url.Contains("720p"))
                {
                    return "HD720P";
                }
            }
            return null;
        }
    }
}
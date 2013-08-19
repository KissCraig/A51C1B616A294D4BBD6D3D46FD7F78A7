using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class UrlCodeHelper
    {
        /// <summary>
        /// 整理并解码点播链接
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<string> GetDecodeList(System.Collections.Generic.List<string> lists)
        {
            #region 整理并解码点播链接
            var ends = new System.Collections.Generic.List<string>();
            if (lists != null && lists.Count > 0)
            {
                for (var i = 0; i < lists.Count; i++)
                {
                    var newurl = System.Web.HttpUtility.UrlDecode(lists[i]);
                    if (!string.IsNullOrEmpty(newurl))
                    {
                        if (newurl.StartsWith("magnet:?xt=urn:btih:"))
                        {
                            if (newurl.Contains("&"))
                            {
                                newurl = newurl.Split('&')[0] + string.Format("&dn=KCPlayer[{0}]", QualityHelper.GetHdsData(new System.Collections.Generic.List<string> { newurl }));
                            }
                        }
                        ends.Add(newurl);
                    }
                }
                if (ends.Count > 0)
                {
                    try
                    {
                        ends.Sort();
                    }
// ReSharper disable EmptyGeneralCatchClause
                    catch
// ReSharper restore EmptyGeneralCatchClause
                    {
                    }
                }
            }
            return ends; 
            #endregion
        }

        /// <summary>
        /// 清理一些不必要的名字值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetClearVideoName(string name)
        {
            #region 清理一些不必要的名字值

            string nameend = null;
            if (!string.IsNullOrEmpty(name))
            {
                nameend = name.Replace("【", "")
                            .Replace("】", "")
                            .Replace("·", "")
                            .Replace("《", "")
                            .Replace("》", "")
                            .Replace("）", "")
                            .Replace("（", "")
                            .Replace("2013", "")
                            .Replace("资源","");
                
            }
            return nameend;
            #endregion
        }

        /// <summary>
        /// 整理地区信息
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string GetVideoLocation(string txt)
        {
            #region 整理地区信息
            if (txt.Contains("美国"))
                return "美国";
            if (txt.Contains("中英"))
                return "欧美";
            if (txt.Contains("暂无"))
                return "未知";
            if (txt.Contains("中德"))
                return "德国";
            if (txt.Contains("中法"))
                return "法国";
            if (txt.Contains("金球"))
                return "欧美";
            if (txt.Contains("中日"))
                return "日本";
            if (txt.Contains("印坛"))
                return "印度";
            if (txt.Contains("韩剧"))
                return "韩剧";
            return "未知"; 
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="typeNum"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public static string GetListHttpPath(int pageNum, int typeNum, LiuXingType iType)
        {
            #region GetListHttpPath - 合成正常列表所需的请求地址

            var pathtemp = new System.Text.StringBuilder();
            switch (iType.Type)
            {
                // 迅播影院正常列表
                case LiuXingEnum.XunboListItem:
                    {
                        #region case LiuXingType.XunboListItem:

                        if (pageNum == 1)
                        {
                            switch (PublicStatic.AnSortType)
                            {
                                case SortType.AnGengXin:
                                    {
                                        pathtemp.Append(string.Format("{0}{1}.html", PublicStatic.AnGengXin, typeNum));
                                    }
                                    break;
                                case SortType.AnReDu:
                                    {
                                        pathtemp.Append(string.Format("{0}{1}.html", PublicStatic.AnReDu, typeNum));
                                    }
                                    break;
                                case SortType.AnPengFeng:
                                    {
                                        pathtemp.Append(string.Format("{0}{1}.html", PublicStatic.AnPengFeng, typeNum));
                                    }
                                    break;
                                case SortType.AnShiJian:
                                    {
                                        pathtemp.Append(string.Format("{0}{1}", PublicStatic.AnShiJian, pageNum));
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (PublicStatic.AnSortType)
                            {
                                case SortType.AnGengXin:
                                    {
                                        pathtemp.Append(string.Format("{0}{1}_{2}.html", PublicStatic.AnGengXin, typeNum,
                                                                      pageNum));
                                    }
                                    break;
                                case SortType.AnReDu:
                                    {
                                        pathtemp.Append(string.Format("{0}{1}_{2}.html", PublicStatic.AnReDu, typeNum,
                                                                      pageNum));
                                    }
                                    break;
                                case SortType.AnPengFeng:
                                    {
                                        pathtemp.Append(string.Format("{0}{1}_{2}.html", PublicStatic.AnPengFeng,
                                                                      typeNum, pageNum));
                                    }
                                    break;
                                case SortType.AnShiJian:
                                    {
                                        pathtemp.Append(string.Format("{0}{1}", PublicStatic.AnShiJian, pageNum));
                                    }
                                    break;
                            }
                        }

                        #endregion
                    }
                    break;
                // 人人影视正常列表
                case LiuXingEnum.YYetListItem:
                    {
                        #region case LiuXingType.YYetListItem:

                        switch (PublicStatic.AnSortType)
                        {
                            case SortType.AnGengXin:
                                {
                                    pathtemp.Append(string.Format("{0}{1}&channel=movie&format=HR-HDTV&sort=update",
                                                                  PublicStatic.YYetsListHost, pageNum));
                                }
                                break;
                            case SortType.AnReDu:
                                {
                                    pathtemp.Append(string.Format("{0}{1}&channel=movie&format=HR-HDTV&sort=views",
                                                                  PublicStatic.YYetsListHost, pageNum));
                                }
                                break;
                            case SortType.AnPengFeng:
                                {
                                    pathtemp.Append(string.Format("{0}{1}&channel=movie&format=HR-HDTV&sort=score",
                                                                  PublicStatic.YYetsListHost, pageNum));
                                }
                                break;
                        }

                        #endregion
                    }
                    break;
                case LiuXingEnum.YYetSearchItem:
                    {
                        #region case LiuXingEnum.YYetSearchItem:
                        pathtemp.Append(@"http://www.yyets.com/php/search/index?type=resource&keyword=");
                        pathtemp.Append(PublicStatic.SearchWord);
                        #endregion
                    }
                    break;
                case LiuXingEnum.PiaoHuaSearchItem:
                    {
                        #region case LiuXingEnum.PiaoHuaSearchItem:
                        pathtemp.Append(@"http://www.xiaobajiew.com/index.php?s=video/search&submit=搜索&wd=");
                        pathtemp.Append(System.Web.HttpUtility.UrlEncode(PublicStatic.SearchWord));
                        #endregion
                    }
                    break;
                case LiuXingEnum.DyfmSearchItem:
                    {
                        #region case LiuXingEnum.DyfmSearchItem:
                        pathtemp.Append(@"http://dianying.fm/search?key=");
                        pathtemp.Append(System.Web.HttpUtility.UrlEncode(PublicStatic.SearchWord));
                        #endregion
                    }
                    break;
                case LiuXingEnum.XunboSearchItem:
                    {
                        #region case LiuXingEnum.XunboSearchItem:
                        pathtemp.Append(@"http://www.2tu.cc/search.asp?searchword=");
                        pathtemp.Append(System.Web.HttpUtility.UrlEncode(PublicStatic.SearchWord, System.Text.Encoding.Default).ToUpper());
                        #endregion
                    }
                    break;
                case LiuXingEnum.TorrentKittySearchItem:
                    {
                        #region case LiuXingEnum.TorrentKittySearchItem:
                        pathtemp.Append(@"http://www.torrentkitty.com/search/");
                        pathtemp.Append(PublicStatic.SearchWord);
                        pathtemp.Append("/");
                        pathtemp.Append(pageNum); 
                        #endregion
                    }
                    break;
                case LiuXingEnum.M1905ComListItem:
                    {
                        #region case LiuXingEnum.M1905ComListItem:
                        pathtemp.Append(@"http://zbyk.m1905.com/service/index.php/Api/Apiql/filmlist"); 
                        #endregion
                    }
                    break;
                case LiuXingEnum.LuYiXia:
                    {
                        #region case LiuXingEnum.LuYiXia:
                        pathtemp.Append(@"http://api.kcplayer.com:7383/watching/getdata?s=0&e=1&filter=false"); 
                        #endregion
                    }
                    break;
                case LiuXingEnum.ZhangYuSearchItem:
                    {
                        #region case LiuXingEnum.ZhangYuSearchItem:
                        pathtemp.Append(@"http://www.happygolife.com/ajax/search?p=1&s=");
                        pathtemp.Append(System.Web.HttpUtility.UrlEncode(PublicStatic.SearchWord)); 
                        #endregion
                    }
                    break;
                case LiuXingEnum.DyfmHotApi:
                    {
                        #region case LiuXingEnum.DyfmHotApi:
                        pathtemp.Append(@"http://dianying.fm/kankan?cmd=next"); 
                        #endregion
                    }
                    break;
                case LiuXingEnum.EverybodyWatch:
                    {
                        #region case LiuXingEnum.EverybodyWatch:
                        pathtemp.Append(@"http://api.kcplayer.com:7383/watching/getdata?s=" + 50 * (pageNum-1) + "&e="+50 * (pageNum+1)+"&filter=IsFilter"); 
                        #endregion
                    }
                    break;
            }
            return pathtemp.ToString();

            #endregion
        }
    }
}

using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Data
{
    public class ListData
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
                    case LiuXingEnum.XunboListItem:
                        {
                            #region case LiuXingEnum.XunboListItem:

                            // 电影名称
                            var tempname = celllistr.GetSingle("alt=\"", "\" /><em");
                            if (!string.IsNullOrEmpty(tempname))
                            {
                                cellitem.Name = tempname;
                            }
                            // 电影网址
                            var tempurl = celllistr.GetSingle("<a href=\"", "\" class=\"i\"><img");
                            if (!string.IsNullOrEmpty(tempurl))
                            {
                                cellitem.Url = PublicStatic.LiuXingYuName + tempurl;
                            }
                            // 电影封面
                            var tempimg = celllistr.GetSingle("src=\"", "\" alt=");
                            if (!string.IsNullOrEmpty(tempimg))
                            {
                                cellitem.Img = tempimg;
                            }
                            // 电影质量
                            var tempHDs = celllistr.GetSingle("class=\"v\">", "</em></a");
                            if (!string.IsNullOrEmpty(tempHDs))
                            {
                                cellitem.HDs = tempHDs;
                            }
                            // 电影评分
                            var tempCos = celllistr.GetSingle("<em class=\"fenshu\">", "</sup></em>");
                            if (!string.IsNullOrEmpty(tempCos))
                            {
                                cellitem.Cos = tempCos.Replace("<sup>", "");
                            }
                            // 电影地区
                            var tempLoc = celllistr.GetSingle("<b>地区：", "</b></p>");
                            if (!string.IsNullOrEmpty(tempLoc))
                            {
                                cellitem.Loc = tempLoc.Replace(" ", "");
                            }
                            // 电影年代
                            var tempTim = celllistr.GetSingle("</a><em>", "</em></h1>");
                            if (!string.IsNullOrEmpty(tempTim))
                            {
                                cellitem.Tim = tempTim.Replace(" ", "");
                            }
                            // 电影演员
                            var tempCar = celllistr.GetSingle("<p>主演：", "</p>");
                            if (!string.IsNullOrEmpty(tempCar))
                            {
                                cellitem.Car = tempCar.Replace(',', '、');
                            }
                            // 电影类型
                            var tempTyp = celllistr.GetSingle("类型：", "</b><b>");
                            if (!string.IsNullOrEmpty(tempTyp))
                            {
                                cellitem.Typ = tempTyp;
                            }
                            // 电影更新
                            var tempUpt = celllistr.GetSingle("<b>更新：", "</b></p>");
                            if (!string.IsNullOrEmpty(tempUpt))
                            {
                                cellitem.Upt = tempUpt.Replace("-", "~");
                            }

                            #endregion
                        }
                        break;
                        // 人人影视正常列表
                    case LiuXingEnum.YYetListItem:
                        {
                            #region case LiuXingEnum.YYetListItem:

                            // 影片大类
                            var tempmpe = celllistr.GetSingle(">【", "】<strong>");
                            if (!string.IsNullOrEmpty(tempmpe))
                            {
                                cellitem.Mpe = tempmpe;
                            }
                            // 电影名称
                            var tempname = celllistr.GetSingle("<strong>", "》");
                            if (!string.IsNullOrEmpty(tempname))
                            {
                                cellitem.Name = tempname.Replace("《", "")
                                                        .Replace("【", "")
                                                        .Replace("】", "")
                                                        .Replace("》", "");
                            }
                            // 电影网址
                            var tempurl = celllistr.GetSingle("href=\"", "\"><img");
                            if (!string.IsNullOrEmpty(tempurl))
                            {
                                cellitem.Url = tempurl;
                            }
                            // 电影封面
                            var tempimg = celllistr.GetSingle("<img src=\"", "\"></a>");
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
                            var tempCos = celllistr.GetSingle("【人气】</font>", "分</span>");
                            if (!string.IsNullOrEmpty(tempCos))
                            {
                                var tempCoss = tempCos.Split('|');
                                if (tempCoss.Length >= 3)
                                {
                                    cellitem.Cos = tempCos.Split('|')[2].Trim().Substring(0, 3);
                                }
                            }
                            // 电影地区
                            var tempLoc = celllistr.GetSingle("【说明】", "</span><span><font");
                            if (!string.IsNullOrEmpty(tempLoc))
                            {
                                cellitem.Loc = JieXiYYetsDiQu(tempLoc);
                            }
                            // 电影年代
                            var tempTim = celllistr.GetSingle("》", "</strong>");
                            if (!string.IsNullOrEmpty(tempTim))
                            {
                                cellitem.Tim = tempTim;
                            }
                            // 电影演员
                            var tempCar = celllistr.GetSingle("【说明】</font>", "</span><span><f");
                            if (!string.IsNullOrEmpty(tempCar))
                            {
                                cellitem.Car = tempCar.Length >= 25 ? tempCar.Substring(0, 25) : tempCar;
                            }
                            // 电影类型
                            var tempTyp = celllistr.GetSingle("【类型】</font>", "</span><span><f");
                            if (!string.IsNullOrEmpty(tempTyp))
                            {
                                cellitem.Typ = JieXiYYetsType(tempTyp);
                            }
                            // 电影更新
                            var tempUpt = celllistr.GetSingle("【更新】</font>", "</span></dd>");
                            if (!string.IsNullOrEmpty(tempUpt))
                            {
                                var tempUpts = tempUpt.Split('|');
                                if (tempUpts.Length >= 1)
                                {
                                    cellitem.Upt = tempUpt.Split('|')[0].Trim().Substring(5, 5).Replace("-", "~");
                                }
                            }

                            #endregion
                        }
                        break;
                }
                return cellitem;
            }
            catch (System.Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message);
            }
            return null;

            #endregion
        }

        #region Private Code

        /// <summary>
        /// 解析人人影视列表中的类别信息
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private static string JieXiYYetsType(string txt)
        {
            if (!txt.Contains("/"))
            {
                return txt;
            }
            var txts = txt.Split('/');
            return txts.Length > 0 ? txts[0].Trim() : "";
        }

        /// <summary>
        /// 解析人人影视列表中的地区信息
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private static string JieXiYYetsDiQu(string txt)
        {
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
        }

        #endregion
    }
}
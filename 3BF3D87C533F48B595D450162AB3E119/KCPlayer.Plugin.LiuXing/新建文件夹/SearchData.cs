using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KCPlayer.Plugin.LiuXing.LoadLiuXing.Piaohua
{
    public class SearchData
    {
        /// <summary>
        ///     解析人人对象数据
        /// </summary>
        /// <param name="celllistr"></param>
        /// <returns></returns>
        public static LiuXingData JieXiSearchData(string celllistr)
        {
            try
            {
                // 
                if (string.IsNullOrEmpty(celllistr)) return null;
                var cellitem = new LiuXingData();
                // 影片大类
                string tempmpe = LiuXingRegex.GetSingle(celllistr, "<div id=\"location\">", "</div>");
                if (!string.IsNullOrEmpty(tempmpe))
                {
                    List<string> titless = LiuXingRegex.GetValue(tempmpe, ">", "<");
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
                string picImg = LiuXingRegex.GetSingle(celllistr, "<div class=\"moviepic\">", "</div>");
                if (!string.IsNullOrEmpty(picImg))
                {
                    string imgurl = LiuXingRegex.GetSingle(picImg, "<img src=\"", "\" title=");
                    if (!string.IsNullOrEmpty(imgurl))
                    {
                        cellitem.Img = imgurl;
                    }
                }
                // 电影演员
                var ifons = new List<string>();
                string info = LiuXingRegex.GetSingle(celllistr, "<div class=\"yycon\">", "</div>");
                if (!string.IsNullOrEmpty(info))
                {
                    List<string> infos = LiuXingRegex.GetValue(info, "target=\"_blank\">", "</a>");
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
                string tempLoc = LiuXingRegex.GetSingle(celllistr, "<li><label>地区：</label>", "</li>");
                if (!string.IsNullOrEmpty(tempLoc))
                {
                    cellitem.Loc = tempLoc.Replace(" ", "").Replace("\"", "");
                }
                // 电影年代
                string tempTim = LiuXingRegex.GetSingle(celllistr, "<li><label>年代：</label>", "</li>");
                if (!string.IsNullOrEmpty(tempTim))
                {
                    cellitem.Tim = tempTim.Replace(" ", "").Replace("\"", "");
                }
                // 电影更新
                string tempUpt = LiuXingRegex.GetSingle(celllistr, "<li><label>时间：</label>", "</li>");
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
                string tempHDs = LiuXingRegex.GetSingle(celllistr, "<li><label>版本：</label>", "</li>");
                if (!string.IsNullOrEmpty(tempHDs))
                {
                    cellitem.HDs = tempHDs.Replace(" ", "");
                }

                return cellitem;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return null;
        }
    }
}
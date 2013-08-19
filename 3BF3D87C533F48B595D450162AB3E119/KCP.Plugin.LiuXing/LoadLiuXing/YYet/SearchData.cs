using KCP.Plugin.LiuXing.Model;
using KCPlayer.Plugin.LiuXing.LoadLiuXing.YYet;

namespace KCP.Plugin.LiuXing.LoadLiuXing.YYet
{
    public class SearchData : SearchBase
    {
        public static LiuXingData JieXiSearchData(string celllistr)
        {
            try
            {
                if (string.IsNullOrEmpty(celllistr)) return null;
                var cellitem = new LiuXingData();
                // 影片大类
                var tempmpe = celllistr.GetSingle("【", "】");
                if (!string.IsNullOrEmpty(tempmpe))
                {
                    cellitem.Mpe = tempmpe;
                }
                // 电影名称
                var tempname = celllistr.GetSingle("<strong>", "<label id=\"play");
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
                var tempimg = celllistr.GetSingle("<img src=\"", "\" /></a>");
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
                var tempLoc = celllistr.GetSingle("<span>地区：</span><strong", "</strong>");
                if (!string.IsNullOrEmpty(tempLoc))
                {
                    cellitem.Loc = JieXiYYetsDiQu(tempLoc.Replace(">", ""));
                }
                // 电影年代
                var tempTim = celllistr.GetSingle("<span>年代：</span><strong>",
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
                return cellitem;
            }
            catch (System.Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message);
            }
            return null;
        }
    }
}
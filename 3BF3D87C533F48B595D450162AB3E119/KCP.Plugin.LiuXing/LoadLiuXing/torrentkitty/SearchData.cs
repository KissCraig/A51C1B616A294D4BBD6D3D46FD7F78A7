using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.LoadLiuXing.torrentkitty
{
    public class SearchData
    {
        /// <summary>
        /// 解析人人对象数据
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

                // 电影名称
                var tempname = celllistr.GetSingle("title=\"", "\" rel=\"magnet\">");
                if (!string.IsNullOrEmpty(tempname))
                {
                    if (tempname.Contains("@"))
                    {
                        var names = tempname.Split('@');
                        if (names.Length > 0)
                        {
                            var last = names[names.Length - 1];
                            if (!string.IsNullOrEmpty(last))
                            {
                                var name = last.Replace("草榴社区", "")
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
                var tempdrl = celllistr.GetSingle("<a href=\"", "\" title");
                if (!string.IsNullOrEmpty(tempdrl))
                {
                    cellitem.Drl = new System.Collections.Generic.List<string> {tempdrl};
                }
                cellitem.Img = @"http://img3.douban.com/img/celebrity/large/6470.jpg";
                cellitem.HDs = "立即点播";
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
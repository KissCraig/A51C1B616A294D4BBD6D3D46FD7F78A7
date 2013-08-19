using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;

namespace KCP.Plugin.LiuXing.Data
{
    public class ListPath
    {
        /// <summary>
        /// GetListHttpPath - 合成正常列表所需的请求地址
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
            }
            return pathtemp.ToString();

            #endregion
        }
    }
}
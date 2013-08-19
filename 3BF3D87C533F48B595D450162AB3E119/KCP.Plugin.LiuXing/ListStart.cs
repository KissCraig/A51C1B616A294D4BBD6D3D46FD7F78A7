using System;
using System.Collections.Generic;
using System.Net;
using KCP.Plugin.LiuXing.Base;
using KCP.Plugin.LiuXing.Data;
using KCP.Plugin.LiuXing.Frm;
using KCP.Plugin.LiuXing.Model;
using KCP.Plugin.LiuXing.View;

namespace KCP.Plugin.LiuXing
{
    public class ListStart
    {
        /// <summary>
        /// 1.开始列表获取
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="typeNum"></param>
        /// <param name="iType"></param>
        public ListStart(int pageNum, int typeNum, LiuXingType iType)
        {
            // 得到地址
            var path = ListPath.GetListHttpPath(pageNum, typeNum, iType);
            if (String.IsNullOrEmpty(path)) return;
            // 开始载入
            StartList(path, iType);
        }

        /// <summary>
        /// 2.开始列表获取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="iType"></param>
        public static void StartList(string path, LiuXingType iType)
        {
            if (!String.IsNullOrEmpty(path))
            {
                // 解析数据
                using (
                    var datadown = new WebClient
                        {
                            Encoding = iType.Encoding,
                            Proxy = iType.Proxy
                        })
                {
                    datadown.DownloadStringAsync(new Uri(path), iType);
                    datadown.DownloadStringCompleted += Datadown_DownloadStringCompleted;
                }
            }
        }

        /// <summary>
        /// 3.一级数据下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Datadown_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // 判断结果
            if (e.Error != null || e.Result.Length <= 0 || e.Cancelled)
            {
                return;
            }
            var resultstr = e.Result;
            if (String.IsNullOrEmpty(resultstr)) return;
            // 解析迅播数据
            JieXiOne(resultstr, e.UserState as LiuXingType);
        }

        /// <summary>
        /// 4.一级数据解析
        /// </summary>
        /// <param name="resultstr"></param>
        /// <param name="iType"></param>
        private static void JieXiOne(string resultstr, LiuXingType iType)
        {
            switch (iType.Type)
            {
                    // 迅播影院正常列表
                case LiuXingEnum.XunboListItem:
                    {
                        #region case LiuXingEnum.XunboListItem:

                        // 得到 <ul class=\"piclist\"> ~ </ul> 
                        var orignlis = resultstr.GetSingle("<ul class=\"piclist\">", "</ul>");
                        if (String.IsNullOrEmpty(orignlis))
                        {
                            return;
                        }
                        // 得到 <li> ~ </li>
                        var orignli = orignlis.GetValue("<li>", "</li>");
                        if (orignli == null || orignli.Count <= 0) return;
                        // 解析数据
                        var zuiReDatas = new List<LiuXingData>();
                        for (var i = 0; i < orignli.Count; i++)
                        {
                            var celllistr = orignli[i];
                            if (String.IsNullOrEmpty(celllistr)) continue;
                            var tempcell = ListData.AnalyzeData(celllistr, iType);
                            if (tempcell != null)
                            {
                                zuiReDatas.Add(tempcell);
                            }
                        }
                        if (zuiReDatas.Count <= 0) return;

                        // 遍历数据中的图片
                        for (var i = 0; i < zuiReDatas.Count; i++)
                        {
                            if (String.IsNullOrEmpty(zuiReDatas[i].Img)) continue;
                            using (
                                var imgdown = new WebClient
                                    {
                                        Encoding = iType.Encoding,
                                        Proxy = iType.Proxy
                                    })
                            {
                                if (!String.IsNullOrEmpty(zuiReDatas[i].Img))
                                {
                                    var iClass = new LiuXingType
                                        {
                                            Encoding = iType.Encoding,
                                            Proxy = iType.Proxy,
                                            Type = iType.Type,
                                            Data = zuiReDatas[i]
                                        };
                                    try
                                    {
                                        var imguri = new Uri(zuiReDatas[i].Img);
                                        imgdown.DownloadDataAsync(imguri, iClass);
                                        imgdown.DownloadDataCompleted += Imgdown_DownloadDataCompleted;
                                    }
                                    catch (Exception exception)
                                    {
                                        // System.Windows.Forms.MessageBox.Show(exception.Message+zuiReDatas[i].Name+zuiReDatas[i].Img+i);
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    break;
                    // 人人影视正常列表
                case LiuXingEnum.YYetListItem:
                    {
                        #region case LiuXingEnum.YYetListItem:

                        // 得到<ul class="boxPadd dashed"> ~ </ul>
                        var orignlis = resultstr.GetSingle("<ul class=\"boxPadd dashed\">", "</ul>");
                        if (String.IsNullOrEmpty(orignlis))
                        {
                            return;
                        }
                        // 得到 <li> ~ </li>
                        var orignli = orignlis.GetValue("<li ", "</li>");
                        if (orignli == null || orignli.Count <= 0)
                        {
                            return;
                        }
                        // 解析数据
                        var zuiReDatas = new List<LiuXingData>();
                        for (var i = 0; i < orignli.Count; i++)
                        {
                            var celllistr = orignli[i];
                            if (!String.IsNullOrEmpty(celllistr))
                            {
                                var tag = ListData.AnalyzeData(celllistr, iType);
                                if (tag != null)
                                {
                                    zuiReDatas.Add(tag);
                                }
                            }
                        }
                        if (zuiReDatas.Count <= 0)
                        {
                            return;
                        }

                        // 遍历数据中的图片
                        for (var i = 0; i < zuiReDatas.Count; i++)
                        {
                            if (String.IsNullOrEmpty(zuiReDatas[i].Img)) continue;
                            using (
                                var imgdown = new WebClient
                                    {
                                        Encoding = iType.Encoding,
                                        Proxy = iType.Proxy
                                    })
                            {
                                var iClass = new LiuXingType
                                    {
                                        Encoding = iType.Encoding,
                                        Proxy = iType.Proxy,
                                        Type = iType.Type,
                                        Data = zuiReDatas[i]
                                    };
                                if (iClass.Data != null)
                                {
                                    imgdown.DownloadDataAsync(new Uri(zuiReDatas[i].Img), iClass);
                                    imgdown.DownloadDataCompleted += Imgdown_DownloadDataCompleted;
                                }
                            }
                        }

                        #endregion
                    }
                    break;
            }
        }

        /// <summary>
        /// 5.下载影片海报
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

            // 图片准备好了后进入显示
            var img = resultstr.GetImageFromByte();
            if (img == null) return;
            var tag = e.UserState as LiuXingType;
            if (tag == null) return;
            tag.Img = img;
            PublicStatic.LiuXingCon.Invoke(
            new System.Windows.Forms.MethodInvoker
            (() =>
                {     
                    switch (PublicStatic.DisPlayStyle)
                    {
                            case LiuXingStyle.DisPlayCell:
                            {
                                PublicStatic.LiuXingCon.Controls.Add(new DisPlayCell(tag));
                            }
                            break;
                            case LiuXingStyle.DisPlayList:
                            {
                                PublicStatic.LiuXingCon.Controls.Add(new DisPlayList(tag));
                            }
                            break;
                            case LiuXingStyle.DisPlayTile:
                            {
                                PublicStatic.LiuXingCon.Controls.Add(new DisPlayTile(tag));
                            }
                            break;
                    }
                }
            ));
        }
    }
}
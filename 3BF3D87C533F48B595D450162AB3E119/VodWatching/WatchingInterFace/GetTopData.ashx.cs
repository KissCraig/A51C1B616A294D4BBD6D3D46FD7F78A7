using KCPlayer.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;

namespace WatchingInterFace
{
    /// <summary>
    /// GetTopData 的摘要说明
    /// </summary>
    public class GetTopData : IHttpHandler
    {
        HttpResponse response = null;
        HttpRequest request = null;
        WaitHandle handle = new AutoResetEvent(false);
        ProcessAjaxTopNews news = null;
        bool isFilter = true;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            request = context.Request;
            response = context.Response;

            //if (request.Headers["KCApp"] == null)
            //{
            //    if (request.Headers["Referer"] != null)
            //    {
            //        if (!request.Headers["Referer"].StartsWith("http://vod.kcplayer.com/") && !request.Headers["Referer"].StartsWith("http://yun.7tbw.com/"))
            //        {
            //            lock (StaticTempSave.RefreshList)
            //            {
            //                StaticTempSave.RefreshList.Add(request.Headers["Referer"]);
            //            }
            //            response.StatusCode = 403;
            //            response.End();
            //        }
            //    }
            //    else
            //    {
            //        response.StatusCode = 403;
            //        response.End();
            //    }
            //}

            string callback = request.QueryString["callback"];
            isFilter = request.QueryString["isfilter"] == "true" ? true : false;

            StaticTempSave.TopNew.GetNew += TopNew_GetNew;
            handle.WaitOne(60000);
            lock (StaticTempSave.TopNew)
            {
                response.StatusCode = 200;
                if (isFilter && !news.Data.data.Url.StartsWith("ftp"))
                {
                    news = null;
                }
                if (news == null)
                {
                    MovieDataState data = new MovieDataState();
                    data.data = new MovieData()
                    {
                        Url = string.Empty,
                        Gcid = string.Empty
                    };
                    data.state = 1;
                    string dataStr = JsonMapper.ToJson(data);
                    response.Write(dataStr);
                    response.End();
                }
                else
                {
                    string data = JsonMapper.ToJson(news.Data);
                    if (callback != null && callback != string.Empty)
                    {
                        response.Write(callback + "(" + data + ");");
                    }
                    else
                    {
                        response.Write(data);
                    }
                    response.End();
                }
            }
        }

        void TopNew_GetNew(object sender, EventArgs e)
        {
            news = sender as ProcessAjaxTopNews;
            if (isFilter)
            {
                if (news.Data.data.Url.StartsWith("ftp"))
                {
                    (handle as AutoResetEvent).Set();
                }
            }
            else
            {
                (handle as AutoResetEvent).Set();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
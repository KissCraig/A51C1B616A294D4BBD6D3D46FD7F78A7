using KCPlayer.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace WatchingInterFace
{
    /// <summary>
    /// InsertData 的摘要说明
    /// </summary>
    public class InsertData : IHttpHandler
    {
        public string DataPath
        {
            get
            {
                return "~/WatchingData/Data_" + DateTime.Now.ToLongDateString() + ".txt";
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //HttpRequest request = context.Request;
            //HttpResponse response = context.Response;

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

            string text = context.Request.QueryString["url"];
            string gcid = context.Request.QueryString["gcid"];

            if (text != string.Empty
                && gcid != string.Empty)
            //&& context.Request.Headers["KCApp"] != null)
            {
                //if (File.Exists(context.Server.MapPath(DataPath)))
                //    listData = JsonMapper.ToObject<List<MovieData>>(File.ReadAllText(context.Server.MapPath(DataPath)));
                lock (StaticTempSave.ListData)
                {
                    MovieData data = null;
                    foreach (MovieData item in StaticTempSave.ListData)
                    {
                        if (item.Url == text.Trim())
                        {
                            data = item;
                            break;
                        }
                    }
                    if (data != null)
                    {
                        context.Response.StatusCode = 403;
                        context.Response.End();
                        return;
                    }

                    data = new MovieData
                    {
                        Gcid = gcid,
                        Url = text
                    };

                    StaticTempSave.ListData.Insert(0, data);
                    //if (data.Url.StartsWith("ftp"))
                    //{
                    StaticTempSave.TopNew.Get(data);
                    //}
                    TimeSpan span = (DateTime.Now - StaticTempSave.DateTime);
                    if (span.TotalHours >= 24)
                    {
                        StaticTempSave.ListData.Clear();
                        File.WriteAllText(context.Server.MapPath(DataPath), JsonMapper.ToJson(StaticTempSave.ListData));
                        StaticTempSave.DateTime = DateTime.Now;
                    }
                }
            }

            context.Response.StatusCode = 403;
            context.Response.End();
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
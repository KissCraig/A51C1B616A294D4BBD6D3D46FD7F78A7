using KCPlayer.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace WatchingInterFace
{
    /// <summary>
    /// GetData 的摘要说明
    /// </summary>
    public class GetData : IHttpHandler
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
            //lock (StaticTempSave.ListData)
            //{

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

            string jsonp = context.Request.QueryString["jsonp"];

            int start = -1;
            bool issok = int.TryParse(context.Request.QueryString["s"], out start);
            int end = -1;
            bool iseok = int.TryParse(context.Request.QueryString["e"], out end);
            string str = context.Request.QueryString["filter"];
            bool isFilter = true;
            if (!string.IsNullOrEmpty(str))
            {
                isFilter = str.ToLower() == "true" ? true : false;
            }
            lock (StaticTempSave.ListData)
            {
                MovieData[] tempData = null;
                //if (context.Request.Headers["KCApp"] != null)
                //{
                if (issok)
                {
                    List<MovieData> allData = null;
                    if (isFilter)
                    {
                        //allData = StaticTempSave.ListData.FindAll(item => item.Url.Trim().ToLower().StartsWith("ftp"));
                        allData = new List<MovieData>();
                        foreach (MovieData item in StaticTempSave.ListData)
                        {
                            if (item != null && item.Url.ToLower().Trim().StartsWith("ftp://"))
                            {
                                allData.Add(item);
                            }
                        }
                    }
                    else
                    {
                        allData = StaticTempSave.ListData;
                    }
                    end = iseok ? end : allData.Count;

                    if (start > allData.Count - 1)
                    {
                        context.Response.StatusCode = 403;
                        context.Response.End();
                    }

                    int count = end - start;
                    if (count > allData.Count)
                        count = allData.Count - start;

                    if (count <= 0)
                    {
                        context.Response.StatusCode = 403;
                        context.Response.End();
                    }
                    else
                    {
                        tempData = new MovieData[count];
                        allData.CopyTo(start, tempData, 0, tempData.Length);
                    }
                }
                //}
                if (tempData != null)
                {
                    string data = JsonMapper.ToJson(tempData);
                    if (data != null)
                    {
                        if (jsonp != null && jsonp != string.Empty)
                            context.Response.Write(jsonp + "(" + data + ")");
                        else
                            context.Response.Write(data);
                    }
                }
                else
                {
                    context.Response.StatusCode = 403;
                }
            }
            context.Response.End();
            //}
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
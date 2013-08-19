using KCPlayer.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace WatchingInterFace
{
    /// <summary>
    /// LibData 的摘要说明
    /// </summary>
    public class LibData : IHttpHandler
    {
        public string DataPath
        {
            get
            {
                return "~/WatchingData/Data_" + DateTime.Now.ToLongDateString() + ".txt";
            }
        }
        public HttpContext Context { get; set; }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Context = context;
            lock (StaticTempSave.ListData)
            {
                Write(@"总数统计: " + StaticTempSave.ListData.Count);
            }
        }

        public void Write(string str)
        {
            this.Context.Response.Write(str);
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
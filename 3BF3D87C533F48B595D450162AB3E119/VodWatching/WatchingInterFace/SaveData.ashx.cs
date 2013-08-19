using KCPlayer.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace WatchingInterFace
{
    /// <summary>
    /// SaveData 的摘要说明
    /// </summary>
    public class SaveData : IHttpHandler
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
            string clear = context.Request.QueryString["clear"];
            if (!string.IsNullOrEmpty(clear) && clear.ToLower() == "true")
            {
                StaticTempSave.ListData.Clear();
            }
            File.WriteAllText(context.Server.MapPath(DataPath), JsonMapper.ToJson(StaticTempSave.ListData));
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
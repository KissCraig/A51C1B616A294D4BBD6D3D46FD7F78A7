using System;
using System.Collections.Generic;
using System.Web;

namespace WatchingInterFace
{
    /// <summary>
    /// LibRefresh 的摘要说明
    /// </summary>
    public class LibRefresh : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            lock (StaticTempSave.RefreshList)
            {
                if (StaticTempSave.RefreshList.Count > 0)
                {
                    foreach (string item in StaticTempSave.RefreshList)
                    {
                        context.Response.Write("来路调用: " + item + "\r\n");
                    }
                }
                else
                {
                    context.Response.Write("当前没有数据！");
                }
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
using KCPlayer.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace WatchingInterFace
{
    //public class PublicProcessing
    //{
    //    public static object obj = new object();
    //}
    public class MovieData
    {
        public string Url { get; set; }
        public string Gcid { get; set; }
        public string MovieName
        {
            get
            {
                if (Url == null)
                    return string.Empty;
                if (Url.StartsWith("ftp"))
                {
                    int index = Url.LastIndexOf('/');
                    if (index != -1)
                    {
                        string name = Url.Substring(index + 1);
                        index = name.IndexOf(']');
                        if (index != -1)
                        {
                            return name.Substring(name.IndexOf(']') + 1);
                        }
                        else
                        {
                            index = name.IndexOf('】');
                            if (index != -1)
                            {
                                return name.Substring(name.IndexOf(']') + 1);
                            }
                            else
                            {
                                return name;
                            }
                        }
                    }
                    else
                    {
                        return "未命名";
                    }
                }
                else if (Url.StartsWith("ed2k"))
                {
                    string str = Regex.Match(Url, "(?<=ed2k://|file|).*?(?=|)").Value;
                    return str == string.Empty ? "未命名" : str;
                }
                else
                {
                    return "未命名";
                }
            }
        }
    }
}
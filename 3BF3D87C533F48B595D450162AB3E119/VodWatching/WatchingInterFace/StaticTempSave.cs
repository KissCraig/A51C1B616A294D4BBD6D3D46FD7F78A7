using System;
using System.Collections.Generic;
using System.Web;

namespace WatchingInterFace
{
    public static class StaticTempSave
    {
        private static DateTime _dateTime = DateTime.Now;
        public static DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        private static List<MovieData> listData = new List<MovieData>();
        public static List<MovieData> ListData
        {
            get { return StaticTempSave.listData; }
            set { StaticTempSave.listData = value; }
        }

        private static List<string> _refreshList = new List<string>();
        public static List<string> RefreshList
        {
            get { return StaticTempSave._refreshList; }
            set { StaticTempSave._refreshList = value; }
        }

        private static ProcessAjaxTopNews _topNew = new ProcessAjaxTopNews();

        public static ProcessAjaxTopNews TopNew
        {
            get { return StaticTempSave._topNew; }
            set { StaticTempSave._topNew = value; }
        }
    }

    public class ProcessAjaxTopNews
    {
        public MovieDataState Data { get; set; }
        public event EventHandler GetNew;
        public void Get(MovieData data)
        {
            Data = new MovieDataState();
            Data.data = data;
            Data.state = 0;
            if (GetNew != null)
            {
                GetNew(this, EventArgs.Empty);
            }
        }
    }

    public class MovieDataState
    {
        public int state { get; set; }
        public MovieData data { get; set; }        
    }
}
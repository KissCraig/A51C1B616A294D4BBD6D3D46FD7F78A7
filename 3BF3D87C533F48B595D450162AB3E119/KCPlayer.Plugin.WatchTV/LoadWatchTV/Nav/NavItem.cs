using System;

namespace KCPlayer.Plugin.WatchTV.LoadWatchTV.Nav
{
    [Serializable]
    public class NavItem
    {
        public string AppName { get; set; }
        public string AppUrl { get; set; }
        public int AppWidth { get; set; }
        public int AppHeight { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SongTastePlayer.Controls
{
    public class MusicEntity
    {
        public string MusicId { get; set; }
        public string SHA1Id { get; set; }
        public string SharePeople { get; set; }
        public string MusicName { get; set; }
        public string MusicPath { get; set; }
        public Rectangle Rect { get; set; }
    }
}

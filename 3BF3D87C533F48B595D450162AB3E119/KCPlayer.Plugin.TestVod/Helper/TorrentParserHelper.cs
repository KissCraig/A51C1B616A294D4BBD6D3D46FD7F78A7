
namespace KCPlayer.Plugin.TestVod.Helper
{
    public class TorrentParser
    {
        private readonly FileCallback _rCallback;
        private long _infoEnd;
        private long _infoStart;
        private byte[] _shaHash;
        private System.Collections.Generic.List<File> _files;
        private string _path;
        private System.Collections.Generic.Dictionary<string, Val> _root;
        private System.IO.BinaryReader _torrent;

        public TorrentParser(string tpath)
        {
            Process(tpath);
        }

        public TorrentParser(string path, FileCallback readNotify)
        {
            _rCallback = readNotify;
            Process(path);
        }

        public string[] AnnounceList
        {
            get
            {
                if (!_root.ContainsKey("announce-list"))
                {
                    return new string[0];
                }
                var list = new System.Collections.Generic.List<string>();
                foreach (var val in (System.Collections.Generic.List<Val>)_root["announce-list"].DObject)
                {
                    foreach (var val2 in (System.Collections.Generic.List<Val>)val.DObject)
                    {
                        var item = (string) val2.DObject;
                        if (!list.Contains(item))
                        {
                            list.Add(item);
                        }
                    }
                }
                return list.ToArray();
            }
        }

        public string AnnounceUrl
        {
            get { return KeyFind(_root, "announce"); }
        }

        public byte[] ByteHash
        {
            get { return _shaHash; }
        }

        public string Comment
        {
            get { return KeyFind(_root, "comment"); }
        }

        public string CreatedBy
        {
            get { return KeyFind(_root, "created by"); }
        }

        public System.DateTime CreationDate
        {
            get
            {
                var time = new System.DateTime(0x7b2, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                return time.AddSeconds(KeyFindint(_root, "creation date"));
            }
        }

        public string Encoding
        {
            get { return KeyFind(Info, "encoding"); }
        }

        public int FileCount
        {
            get { return Files.Count; }
        }

        public System.Collections.Generic.List<File> Files
        {
            get { return _files; }
        }

        public System.Collections.Generic.Dictionary<string, Val> Info
        {
            get { return (System.Collections.Generic.Dictionary<string, Val>)_root["info"].DObject; }
        }

        public bool IsSingle
        {
            get { return !Info.ContainsKey("files"); }
        }

        public string Name
        {
            get { return KeyFind(Info, "name"); }
        }

        public string Path
        {
            get { return _path; }
        }

        public long PieceLength
        {
            get { return KeyFindint(Info, "piece length"); }
        }

        public byte[] Pieces
        {
            get { return (byte[]) Info["pieces"].DObject; }
        }

        public bool Private
        {
            get
            {
                string str = KeyFind(Info, "private");
                if (str == "")
                {
                    return false;
                }
                if (str != "1")
                {
                    return false;
                }
                return true;
            }
        }

        public System.Collections.Generic.Dictionary<string, Val> Root
        {
            get { return _root; }
        }

        public string ShaHash { get; private set; }

        public long Size { get; set; }

        public long TotalValues { get; private set; }

        private System.Collections.Generic.List<File> GetFiles()
        {
            var list = new System.Collections.Generic.List<File>();
            if (Info.ContainsKey("files"))
            {
                var num = 0L;
                foreach (var val in (System.Collections.Generic.List<Val>)Info["files"].DObject)
                {
                    var dObject = (System.Collections.Generic.Dictionary<string, Val>)val.DObject;
                    var pStart = ((int) (num/(PieceLength))) + 1;
                    num += (long) dObject["length"].DObject;
                    var pLen = (((int) (num/(PieceLength))) + 2) - pStart;
                    list.Add(new File((long)dObject["length"].DObject, (System.Collections.Generic.List<Val>)dObject["path"].DObject, pStart,
                                       pLen));
                }
                Size = num;
                return list;
            }
            list.Add(new File((long) Info["length"].DObject, (string) Info["name"].DObject, 1, Pieces.Length/20));
            Size = (long) Info["length"].DObject;
            return list;
        }

        private string GetShaHash()
        {
            var managed = new System.Security.Cryptography.SHA1Managed();
            _torrent.BaseStream.Position = _infoStart;
            var buffer = _torrent.ReadBytes(System.Convert.ToInt32(_infoEnd));
            _shaHash = managed.ComputeHash(buffer);
            return System.BitConverter.ToString(_shaHash).Replace("-", "");
        }

        private static string KeyFind(System.Collections.Generic.Dictionary<string, Val> dictToSearch, string key)
        {
            if (dictToSearch.ContainsKey(key))
            {
                return (string) dictToSearch[key].DObject;
            }
            return "";
        }

        private static long KeyFindint(System.Collections.Generic.Dictionary<string, Val> dictToSearch, string key)
        {
            if (dictToSearch.ContainsKey(key))
            {
                return (long) dictToSearch[key].DObject;
            }
            return 0;
        }

        private void Process(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                //MessageBox.Show("种子文件异常！", "提示",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            TotalValues = 0L;
            _path = path;
            _torrent = new System.IO.BinaryReader(new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read),
                                        System.Text.Encoding.UTF8);
            if (_torrent.ReadChar() != 'd')
            {
                //MessageBox.Show("种子文件异常！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                //行了
            }
            _root = ProcessDict();
            ShaHash = GetShaHash();
            _files = GetFiles();
            _torrent.Close();
        }

        private Val ProcessByte()
        {
            string s = "";
            do
            {
                s = s + char.ConvertFromUtf32(_torrent.Read());
            } while (char.ConvertFromUtf32(_torrent.PeekChar()) != ":");
            _torrent.Read();
            return new Val(DataType.DByte, _torrent.ReadBytes(int.Parse(s)));
        }

        private System.Collections.Generic.Dictionary<string, Val> ProcessDict()
        {
            var dictionary = new System.Collections.Generic.Dictionary<string, Val>();
            while (_torrent.PeekChar() != 0x65)
            {
                string key = ProcessString();
                if (key == "info")
                {
                    _infoStart = _torrent.BaseStream.Position;
                }
                if (key == "pieces")
                {
                    dictionary.Add(key, ProcessByte());
                }
                else
                {
                    Val val = ProcessVal();
                    if (!dictionary.ContainsKey(key))
                        dictionary.Add(key, val);
                }
                if (key == "info")
                {
                    _infoEnd = _torrent.BaseStream.Position - _infoStart;
                }
            }
            _torrent.Read();
            return dictionary;
        }

        private long ProcessInt()
        {
            string s = "";
            do
            {
                s = s + char.ConvertFromUtf32(_torrent.Read());
            } while (char.ConvertFromUtf32(_torrent.PeekChar()) != "e");
            _torrent.Read();
            return long.Parse(s);
        }

        private System.Collections.Generic.List<Val> ProcessList()
        {
            var list = new System.Collections.Generic.List<Val>();
            while (char.ConvertFromUtf32(_torrent.PeekChar()) != "e")
            {
                list.Add(ProcessVal());
            }
            _torrent.Read();
            return list;
        }

        private string ProcessString()
        {
            string s = "";
            do
            {
                s = s + char.ConvertFromUtf32(_torrent.Read());
            } while (char.ConvertFromUtf32(_torrent.PeekChar()) != ":");
            _torrent.Read();
            byte[] bytes = _torrent.ReadBytes(int.Parse(s));
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        private Val ProcessVal()
        {
            ReadProgress();
            TotalValues += 1L;
            switch (char.ConvertFromUtf32(_torrent.PeekChar()))
            {
                case "d":
                    _torrent.Read();
                    return new Val(DataType.DDictionary, ProcessDict());

                case "l":
                    _torrent.Read();
                    return new Val(DataType.DList, ProcessList());

                case "i":
                    _torrent.Read();
                    return new Val(DataType.DInt, ProcessInt());
            }
            return new Val(DataType.DString, ProcessString());
        }

        private void ReadProgress()
        {
            if (_rCallback != null)
            {
                var precent = (int) (100L/(_torrent.BaseStream.Length/_torrent.BaseStream.Position));
                _rCallback(precent);
            }
        }
    }

    public delegate void FileCallback(int precent);

    public enum DataType
    {
        DString,
        DInt,
        DList,
        DDictionary,
        DByte
    }

    public class Val
    {
        private readonly object _object;
        private readonly DataType _type;

        public Val(DataType dType, object dObject)
        {
            _type = dType;
            _object = dObject;
        }

        public object DObject
        {
            get { return _object; }
        }

        public DataType Type
        {
            get { return _type; }
        }
    }

    public class File
    {
        private readonly int _firstPiece;
        private readonly int _pieceLength;
        private readonly long _len;
        private readonly string _name;
        private readonly string _path;

        public File(long size, System.Collections.Generic.IEnumerable<Val> fullPath, int pStart, int pLen)
        {
            _len = size;
            _firstPiece = pStart;
            _pieceLength = pLen;
            _path = "";
            foreach (Val val in fullPath)
            {
                _path = _path + @"\" + ((string) val.DObject);
            }
            _path = _path.Remove(0, 1);
            int length = _path.LastIndexOf(@"\", System.StringComparison.Ordinal);
            if (length > 0)
            {
                _name = _path.Substring(length + 1);
                _path = _path.Substring(0, length);
            }
            else
            {
                _name = _path;
                _path = "";
            }
        }

        public File(long size, string fullPath, int pStart, int pLen)
        {
            _len = size;
            _firstPiece = pStart;
            _pieceLength = pLen;
            int length = fullPath.LastIndexOf(@"\", System.StringComparison.Ordinal);
            if (length > 0)
            {
                _path = fullPath.Substring(0, length);
                _name = fullPath.Substring(length + 1);
            }
            else
            {
                _path = "";
                _name = fullPath;
            }
        }

        public int FirstPiece
        {
            get { return _firstPiece; }
        }

        public long Length
        {
            get { return _len; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Path
        {
            get { return _path; }
        }

        public int PieceLength
        {
            get { return _pieceLength; }
        }
    }
}

using System.Net;
using KCPlayer.Json;
using KCPlayer.Plugin.LiuXing.LiuXing;
using KCPlayer.Plugin.LiuXing.Model;

namespace KCPlayer.Plugin.LiuXing.Helper
{
    public class FileCachoHelper
    {
        /// <summary>
        /// 读取本地文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            #region String -> Path -> ReadFile

            try
            {
                string str = System.IO.File.ReadAllText(path);
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            catch
            {
                return null;
            }
            return null;
            #endregion
        }
        /// <summary>
        /// 保存本地缓存
        /// </summary>
        /// <param name="imageuri"></param>
        /// <param name="imgImage"></param>
        public static void CachoImage(string imageuri, System.Drawing.Image imgImage)
        {
            #region 缓存本地文件

            if (!string.IsNullOrEmpty(imageuri))
            {
                if (imgImage != null)
                {
                    if (!string.IsNullOrEmpty(PublicStatic.LiuXingVideoImageCacheDir))
                    {
                        var imageurimd5 = XunLeiLoginHelper.GetMd5Encoding(imageuri);
                        if (!string.IsNullOrEmpty(imageurimd5))
                        {
                            if (!System.IO.Directory.Exists(PublicStatic.LiuXingVideoImageCacheDir))
                            {
                                System.IO.Directory.CreateDirectory(PublicStatic.LiuXingVideoImageCacheDir);
                            }
                            try
                            {
                                imgImage.Save(PublicStatic.LiuXingVideoImageCacheDir + imageurimd5 + ".kimage");
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 读取本地缓存
        /// </summary>
        /// <param name="imageuri"></param>
        /// <returns></returns>
        public static System.Drawing.Image ImageCacho(string imageuri)
        {
            #region 读取本地缓存
            if (!string.IsNullOrEmpty(imageuri))
            {
                var imageurimd5 = XunLeiLoginHelper.GetMd5Encoding(imageuri);
                if (!string.IsNullOrEmpty(imageurimd5))
                {
                    var imagelocalcachopath = PublicStatic.LiuXingVideoImageCacheDir + imageurimd5 + ".kimage";
                    if (!string.IsNullOrEmpty(imagelocalcachopath))
                    {
                        if (System.IO.File.Exists(imagelocalcachopath))
                        {
                            System.Drawing.Image image;
                            try
                            {
                                image = System.Drawing.Image.FromFile(imagelocalcachopath);
                            }
                            catch
                            {
                                image = null;
                            }
                            return image;
                        }
                    }
                }
            }
            return null;
            #endregion
        }

        /// <summary>
        /// 播放列表 - 保存数据
        /// </summary>
        /// <param name="tag"></param>
        public static void SaveThisVodTag(LiuXingData tag)
        {
            #region 播放列表 - 保存数据
            if (!System.IO.Directory.Exists(PublicStatic.LiuXingVideoRecordCacheDir))
            {
                System.IO.Directory.CreateDirectory(PublicStatic.LiuXingVideoRecordCacheDir);
            }
            if (tag == null || string.IsNullOrEmpty(tag.Img)) return;
            var jasonTxt = JsonMapper.ToJson(tag);
            if (!string.IsNullOrEmpty(jasonTxt))
            {
                jasonTxt = TestVodSafe.DES_Enc_Str(jasonTxt, PublicStatic.KcPlayerUserXunLeiInfoKeys[0], PublicStatic.KcPlayerUserXunLeiInfoKeys[1]);
                if (!string.IsNullOrEmpty(jasonTxt))
                {
                    XunLeiLoginHelper.SaveFile(jasonTxt, PublicStatic.LiuXingVideoRecordCacheDir + XunLeiLoginHelper.GetMd5Encoding(tag.Img) + ".klist");
                }
            }
            #endregion
        }

        /// <summary>
        /// 播放列表 -  读取数据
        /// </summary>
        /// <returns></returns>
        public static System.Collections.Generic.List<LiuXingType> ReadThisVodList()
        {
            #region 播放列表 -  读取数据
            var thisData = new System.Collections.Generic.List<LiuXingType>();
            if (System.IO.Directory.Exists(PublicStatic.LiuXingVideoRecordCacheDir))
            {
                var dllfiles = ToFileNamesWithPath(PublicStatic.LiuXingVideoRecordCacheDir, "*.klist");
                if (dllfiles == null || dllfiles.Length <= 0) return null;
                foreach (var dllfile in dllfiles)
                {
                    if (System.IO.File.Exists(dllfile))
                    {
                        var origintxt = ReadFile(dllfile);
                        if (!string.IsNullOrEmpty(origintxt))
                        {
                            origintxt = TestVodSafe.DES_Dec_Str(origintxt, PublicStatic.KcPlayerUserXunLeiInfoKeys[0], PublicStatic.KcPlayerUserXunLeiInfoKeys[1]);
                            if (!string.IsNullOrEmpty(origintxt))
                            {
                                var tag = JsonMapper.ToObject<LiuXingData>(origintxt);
                                if (tag != null)
                                {
                                    var type = new LiuXingType
                                    {
                                        Data = tag,
                                        Img = ImageCacho(tag.Img)
                                    };
                                    thisData.Add(type);
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(PublicStatic.LiuXingVideoRecordCacheDir);
            }

            return thisData; 
            #endregion
        }

        /// <summary>
        /// String -> Fir -> ToFileNamesWithPath
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        private static string[] ToFileNamesWithPath(string dir, string filters)
        {
            #region String -> Fir -> ToFileNamesWithPath

            if (string.IsNullOrEmpty(filters))
            {
                filters = "*.*";
            }
            return System.IO.Directory.GetFiles(dir, filters, System.IO.SearchOption.TopDirectoryOnly);

            #endregion
        }

        /// <summary>
        /// String -> Path -> NameNoExt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string ToNameNoExt(string path)
        {
            #region String -> Path -> ToNameNoExt

            return System.IO.Path.GetFileNameWithoutExtension(path);
            #endregion
        }
    }
}

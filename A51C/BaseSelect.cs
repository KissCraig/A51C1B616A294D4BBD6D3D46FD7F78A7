using System.Linq;

namespace A51C
{
    public static class BaseSelect
    {
        /// <summary>
        /// IsImage -> ImageTypes
        /// </summary>
        private static readonly string[] ImageTypes = { ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".tiff" };

        /// <summary>
        /// String -> Path -> IsImage 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsImage(this string path)
        {
            #region String -> Path -> IsImage
            if (path.IsNullOrEmptyOrSpace()) return false;
            var ext = path.ToExt().ToLower();
            return !ext.IsNullOrEmptyOrSpace() && ImageTypes.Any(imagetype => imagetype == ext);
            #endregion
        }

        /// <summary>
        /// IsImage -> ImageTypes
        /// </summary>
        private static readonly string[] VideoTypes = { ".wmv", ".mp4"};

        /// <summary>
        /// String -> Path -> IsSupportVideo 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsSupportVideo(this string path)
        {
            #region String -> Path -> IsSupportVideo
            if (path.IsNullOrEmptyOrSpace()) return false;
            var ext = path.ToExt().ToLower();
            return !ext.IsNullOrEmptyOrSpace() && VideoTypes.Any(videotype => videotype == ext);
            #endregion
        }

        /// <summary>
        /// IsImage -> ImageTypes
        /// </summary>
        private static readonly string[] AudioTypes = { ".mp3"};

        /// <summary>
        /// String -> Path -> IsSupportAudio 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsSupportAudio(this string path)
        {
            #region String -> Path -> IsSupportAudio
            if (path.IsNullOrEmptyOrSpace()) return false;
            var ext = path.ToExt().ToLower();
            return !ext.IsNullOrEmptyOrSpace() && AudioTypes.Any(audiotype => audiotype == ext);
            #endregion
        }
    }
}

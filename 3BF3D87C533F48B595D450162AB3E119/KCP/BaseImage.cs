﻿namespace KCP
{
    public static class BaseImage
    {
        /// <summary>
        /// Image -> IsEmptyImage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool IsEmptyImage(this System.Drawing.Image image)
        {
            #region Image -> IsEmptyImage

            return image == null;

            #endregion
        }

        /// <summary>
        /// Icon -> IsEmptyIcon
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static bool IsEmptyIcon(this System.Drawing.Icon icon)
        {
            #region Image -> IsEmptyImage

            return icon == null;

            #endregion
        }

        /// <summary>
        /// String -> Image
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static System.Drawing.Image LoadLocalImage(this string s)
        {
            #region String -> Image

            if (s.IsExistFile())
            {
                try
                {
                    var img = System.Drawing.Image.FromFile(s);
                    System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
                    img.Dispose();
                    return bmp;
                }
                catch
                {
                    return null;
                }
            }
            return null;

            #endregion
        }
    }
}
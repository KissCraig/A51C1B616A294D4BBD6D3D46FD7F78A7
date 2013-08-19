namespace A51C.Main.Theme.Default
{
    public class ResxHelper
    {
        /// <summary>
        /// 返回Image
        /// </summary>
        /// <param name="imageResxName"></param>
        /// <returns></returns>
        public System.Drawing.Image GetImage(string imageResxName)
        {
            #region 返回Image

            if (imageResxName.IsNullOrEmptyOrSpace()) return null;
            var imageResxNames = imageResxName.Split('.');
            // 先从本地资源开始找
            var localPath = "";
            switch (imageResxNames.Length)
            {
                case 3:
                    localPath = string.Format("{0}\\Image\\{1}\\{2}.{3}", BasePublic.DataFolderDirectory,
                                              imageResxNames[0],
                                              imageResxNames[1], imageResxNames[2]);
                    break;
                case 2:
                    localPath = string.Format("{0}\\Image\\{1}", BasePublic.DataFolderDirectory, imageResxName);
                    break;
            }
            // 读取图片
            System.Drawing.Image image;
            if (localPath.IsExistFile())
            {
                image = System.Drawing.Image.FromFile(localPath);
            }
            else
            {
                var stream = GetType().Assembly.GetManifestResourceStream(string.Format("{0}.Image.{1}", GetType().Assembly.GetName().Name,imageResxName));
                if (stream == null)
                {
                    return null;
                }
                image = System.Drawing.Image.FromStream(stream);
            }
            // 返回值
            return image.IsEmptyImage() ? null : image;

            #endregion
        }

        /// <summary>
        /// 返回ICO
        /// </summary>
        /// <param name="icoResxName"></param>
        /// <returns></returns>
        public System.Drawing.Icon GetIco(string icoResxName)
        {
            #region 返回ICO

            if (icoResxName.IsNullOrEmptyOrSpace()) return null;
            using (var stream = GetType().Assembly.GetManifestResourceStream(string.Format("{0}.ICO.{1}", GetType().Assembly.GetName().Name,icoResxName)))
            {
                if (stream == null)
                {
                    return null;
                }
                var icon = new System.Drawing.Icon(stream);
                return icon.IsEmptyIcon() ? null : icon;
            }

            #endregion
        }
    }
}
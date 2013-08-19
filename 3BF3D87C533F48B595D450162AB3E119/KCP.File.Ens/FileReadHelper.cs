namespace KCP.File.Ens
{
    public class FileReadHelper
    {
        #region Decrypt

        /// <summary>
        /// ImagePath -> Decrypt To Image
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="companyname"></param>
        /// <returns></returns>
        public static System.Drawing.Image DecryptImageFormFilePath(string imagePath, string companyname)
        {
            #region ImagePath -> Decrypt To Image

            // Creat -> FileInfo
            var fileInfo = new System.IO.FileInfo(imagePath);
            // Creat -> Key && IV
            var enckey = EnDecryptHelper.StringMd5ShaToString(false,
                                                              string.Format("<{0}/>{1}</{2}>", companyname,
                                                                            System.IO.Path.GetFileNameWithoutExtension(
                                                                                fileInfo.FullName), companyname), 16,
                                                              false, 1, System.Text.Encoding.UTF8);
            var enciv = EnDecryptHelper.StringMd5ShaToString(false,
                                                             string.Format("[{0}/]{1}[/{2}]", companyname,
                                                                           System.IO.Path.GetFileNameWithoutExtension(
                                                                               fileInfo.FullName), companyname), 16,
                                                             false, 1, System.Text.Encoding.UTF8);
            // Encrypt -> Decrypt
            System.Drawing.Image img = null;
            try
            {
                var imgByte = EnDecryptHelper.ByteAesToByte(true, System.IO.File.ReadAllBytes(imagePath), enckey, enciv);
                using (var ms = new System.IO.MemoryStream(imgByte))
                {
                    ms.Position = 0;
                    img = System.Drawing.Image.FromStream(ms);
                }
            }
            catch
            {
                try
                {
                    var imgTemp = System.Drawing.Image.FromFile(imagePath);
                    img = new System.Drawing.Bitmap(imgTemp);
                    imgTemp.Dispose();
                }
                catch
                {
                    return img;
                }
            }
            return img;

            #endregion
        }

        /// <summary>
        /// XmlPath -> Decrypt To String
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="companyname"></param>
        /// <returns></returns>
        public static string DecryptXmlFromFilePath(string xmlPath, string companyname)
        {
            #region XmlPath -> Decrypt To String

            // Creat -> FileInfo
            var fileInfo = new System.IO.FileInfo(xmlPath);
            // Creat -> Key && IV
            var enckey = EnDecryptHelper.StringMd5ShaToString(false,
                                                              string.Format("<{0}/>{1}</{2}>", companyname,
                                                                            System.IO.Path.GetFileNameWithoutExtension(
                                                                                fileInfo.FullName), companyname), 16,
                                                              false, 1, System.Text.Encoding.UTF8);
            var enciv = EnDecryptHelper.StringMd5ShaToString(false,
                                                             string.Format("[{0}/]{1}[/{2}]", companyname,
                                                                           System.IO.Path.GetFileNameWithoutExtension(
                                                                               fileInfo.FullName), companyname), 16,
                                                             false, 1, System.Text.Encoding.UTF8);
            // Encrypt -> Decrypt
            string decresult;
            try
            {
                decresult = EnDecryptHelper.ByteAesDecToString(System.IO.File.ReadAllBytes(xmlPath), enckey, enciv);
                //if (string.IsNullOrEmpty(decresult))
                //{
                //    decresult = System.IO.File.ReadAllText(xmlPath);
                //}
            }
            catch
            {
                try
                {
                    return null;
                    //decresult = System.IO.File.ReadAllText(xmlPath);
                }
                catch
                {
                    return null;
                }
            }
            return string.IsNullOrEmpty(decresult) ? null : decresult;

            #endregion
        }

        #endregion

        #region Encrypt

        /// <summary>
        /// ImagePath -> Encrypt To SavePath
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="originPath"></param>
        /// <param name="companyname"></param>
        /// <returns></returns>
        public static bool EncryptImageFormiOriginPath(string savePath, string originPath, string companyname)
        {
            #region ImagePath -> Encrypt To SavePath

            return EncryptFileFormPath(savePath, originPath, true, null, companyname);

            #endregion
        }

        /// <summary>
        /// XmlPath -> Encrypt To SavePath
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="originPath"></param>
        /// <param name="companyname"></param>
        /// <returns></returns>
        public static bool EncryptXmlFormiOriginPath(string savePath, string originPath, string companyname)
        {
            #region XmlPath -> Encrypt To SavePath

            return EncryptFileFormPath(savePath, originPath, false, null, companyname);

            #endregion
        }

        /// <summary>
        /// OriginPath -> SavePath With IsImage?
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="originPath"></param>
        /// <param name="isImage"></param>
        /// <param name="keyvi"></param>
        /// <param name="companyname"></param>
        /// <returns></returns>
        private static bool EncryptFileFormPath(string savePath, string originPath, bool isImage, string keyvi,
                                                string companyname)
        {
            #region OriginPath -> SavePath With IsImage?

            if (savePath.IsNullOrEmptyOrSpace() || originPath.IsNullOrEmptyOrSpace()) return false;
            // Creat -> FileInfo
            var fileInfo = new System.IO.FileInfo(originPath);
            // Creat -> Key && IV
            var enckey = EnDecryptHelper.StringMd5ShaToString(false,
                                                              string.Format("<{0}/>{1}</{2}>", companyname,
                                                                            keyvi.IsNullOrEmptyOrSpace()
                                                                                ? System.IO.Path
                                                                                        .GetFileNameWithoutExtension(
                                                                                            fileInfo.FullName)
                                                                                : keyvi, companyname), 16, false, 1,
                                                              System.Text.Encoding.UTF8);
            if (enckey.IsNullOrEmptyOrSpace()) return false;
            var enciv = EnDecryptHelper.StringMd5ShaToString(false,
                                                             string.Format("[{0}/]{1}[/{2}]", companyname,
                                                                           keyvi.IsNullOrEmptyOrSpace()
                                                                               ? System.IO.Path
                                                                                       .GetFileNameWithoutExtension(
                                                                                           fileInfo.FullName)
                                                                               : keyvi, companyname), 16, false, 1,
                                                             System.Text.Encoding.UTF8);
            if (enciv.IsNullOrEmptyOrSpace()) return false;
            // Origin -> Encrypt
            var decresult = isImage
                                ? EnDecryptHelper.ByteAesToByte(false, System.IO.File.ReadAllBytes(fileInfo.FullName),
                                                                enckey, enciv)
                                : EnDecryptHelper.StringAesEncToByte(System.IO.File.ReadAllText(fileInfo.FullName),
                                                                     enckey, enciv);
            if (decresult.IsEmptyBytes()) return false;
            // Encrypt -> SaveFile
            try
            {
                System.IO.File.WriteAllBytes(savePath, decresult);
            }
            catch
            {
                return false;
            }
            return true;

            #endregion
        }

        /// <summary>
        /// UserImagePath -> Encrypt To SavePath
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="originPath"></param>
        /// <param name="companyname"></param>
        /// <returns></returns>
        public static bool EncryptUserImageFormiOriginPath(string savePath, string originPath, string companyname)
        {
            #region UserImagePath -> Encrypt To SavePath

            var savepaths = savePath.Split("\\".ToCharArray());
            return !savepaths.IsEmptyStrings() &&
                   EncryptFileFormPath(savePath, originPath, true, savepaths[savepaths.Length - 1].Split('.')[0],
                                       companyname);

            #endregion
        }

        #endregion
    }
}
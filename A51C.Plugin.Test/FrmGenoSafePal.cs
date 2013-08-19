

using A51C.Dens;

namespace A51C.Plugin.Test
{
    public class FrmGenoSafePal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void LiuXingPal_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Get All Path From Drag In
            string[] dropPath;
            try
            {
                dropPath = ((string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop));
            }
            catch
            {
                dropPath = null;
            }
            // Judge Path Is Null
            if (dropPath == null) return;
            if (dropPath.Length <= 0) return;
            foreach (var s in dropPath)
            {
                FileReadHelper.EncryptFromFilePath(s, BasePublic.UiKey);
            }
            @"完成".ToInfoMsgBox("");
        }
    }
}

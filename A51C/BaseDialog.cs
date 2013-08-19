namespace A51C
{
    public static class BaseDialog
    {
        /// <summary>
        /// 选择保存路径
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string SelectAndSavePath(string title, string filter)
        {
            var sp = new System.Windows.Forms.SaveFileDialog
                {
                    Title = title,
                    RestoreDirectory = true,
                    Filter = filter
                };
            if (sp.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;
            var oppath = sp.FileName;
            return oppath;
        }

        /// <summary>
        /// 打开并选择文档
        /// </summary>
        /// <returns></returns>
        public static string OpenAndSelectFile(string title, string filter)
        {
            var op = new System.Windows.Forms.OpenFileDialog
                {
                    Title = title,
                    Filter = filter,
                    RestoreDirectory = true,
                };
            if (op.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;
            var oppath = op.FileName;
            return oppath.IsExistFile() ? oppath : null;
        }

        /// <summary>
        /// 打开并选择文档
        /// </summary>
        /// <returns></returns>
        public static string[] OpenAndSelectFiles(string title, string filter)
        {
            var op = new System.Windows.Forms.OpenFileDialog
                {
                    Title = title,
                    Filter = filter,
                    RestoreDirectory = true,
                    Multiselect = true
                };
            if (op.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;
            var oppaths = op.FileNames;
            return oppaths.Length > 0 ? oppaths : null;
        }
    }
}
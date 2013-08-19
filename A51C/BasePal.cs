namespace A51C
{
    public static class BasePal
    {
        /// <summary>
        /// 激活面板
        /// </summary>
        /// <param name="pal"></param>
        public static void GetActive(this System.Windows.Forms.Control pal)
        {
            if (pal != null && !pal.Parent.Visible)
            {
                pal.Parent.Enabled = pal.Parent.Visible = true;
            }
        }
        /// <summary>
        /// 激活面板
        /// </summary>
        /// <param name="pal"></param>
        public static void GetTomb(this System.Windows.Forms.Control pal)
        {
            if (pal != null && pal.Parent.Visible)
            {
                pal.Parent.Enabled = pal.Parent.Visible = false;
            }
        }
    }
}

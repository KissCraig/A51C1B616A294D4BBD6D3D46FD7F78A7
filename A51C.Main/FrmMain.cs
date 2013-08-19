namespace A51C.Main
{
    public partial class FrmMain : System.Windows.Forms.Form
    {
        public FrmMain()
        {
            InitializeComponent();

            #region Frm Set

            SetClassLong(Handle, GclStyle, GetClassLong(Handle, GclStyle) | CsDropShadow);
            SetStyle(
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);

            #endregion
        }

        #region Frm Set

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                    // 重载最小化
                case 0x112:
                    {
                        if (m.WParam.ToInt32() == 0xF020)
                        {
                            Visible = false;
                        }
                        base.WndProc(ref m);
                    }
                    break;
                case 0x0084:
                    {
                        // 拖动
                        if ((int) m.Result == 0x1)
                        {
                            m.Result = (System.IntPtr) 0x2;
                        }
                    }
                    break;
                case 0x0201: //鼠标左键按下的消息
                    {
                        m.Msg = 0x00A1; //更改消息为非客户区按下鼠标 
                        m.LParam = System.IntPtr.Zero; //默认值 
                        m.WParam = new System.IntPtr(2); //鼠标放在标题栏内 
                        base.WndProc(ref m);
                    }
                    break;
            }
        }

        private const int CsDropShadow = 0x20000;
        private const int GclStyle = (-26);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int SetClassLong(System.IntPtr hwnd, int nIndex, int dwNewLong);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetClassLong(System.IntPtr hwnd, int nIndex);

        private const int WmSettext = 0x000C;

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WmSettext:
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        #endregion
    }
}
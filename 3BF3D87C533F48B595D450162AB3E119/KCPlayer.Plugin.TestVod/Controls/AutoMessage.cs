using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace KCPlayer.Plugin.TestVod.Controls
{
    public class AutoCloseDlg
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool EndDialog(IntPtr hDlg, out IntPtr nResult);

        //外部调用的方法 
        //参数:timeout定义多少毫秒关闭对话框 
        public static void ShowMessageBoxTimeout(string text, string caption,
        MessageBoxButtons buttons, int timeout)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CloseMessageBox), new CloseState(caption, timeout));

                MessageBox.Show(text, caption, buttons);
            }
            catch
            {
            }
        }

        /// <summary> 
        /// 由WaitCallback调用的关闭窗体方法 
        /// </summary> 
        private static void CloseMessageBox(object state)
        {
            CloseState closeState = state as CloseState;

            Thread.Sleep(closeState.Timeout);

            IntPtr dlg = FindWindow(null, closeState.Caption);

            if (dlg != IntPtr.Zero)
            {
                IntPtr result;
                EndDialog(dlg, out result);
            }
        }
    }

    /// <summary> 
    /// 作为ThreadPool.QueueUserWorkItem方法的State参数 
    /// </summary> 
    public class CloseState
    {
        private int _Timeout;

        /// <summary> 
        /// In millisecond 
        /// </summary> 
        public int Timeout
        {
            get
            {
                return _Timeout;
            }
        }

        private string _Caption;

        /// <summary> 
        /// Caption of dialog 
        /// </summary> 
        public string Caption
        {
            get
            {
                return _Caption;
            }
        }

        public CloseState(string caption, int timeout)
        {
            _Timeout = timeout;
            _Caption = caption;
        }
    }
}
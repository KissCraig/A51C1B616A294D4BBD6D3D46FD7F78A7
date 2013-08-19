using System.Linq;

namespace A51C.Main
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [System.STAThread]
        static void Main(string[] args)
       {
            #region 启动窗体
            System.Windows.Forms.Application.EnableVisualStyles();
            // 进程判重
            var process = RuningInstance();
            if (process == null)
            {
                BasePublic.Ui = new FrmMain();
                BasePublic.PluginsDirPath = System.Windows.Forms.Application.StartupPath + "\\Plugins";
                BasePublic.PluginsDirPath.ToCreatDir();
                System.AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                // 加载之前
                if (Load.LoadHelper.BeforeLoad())
                {
                    // 强制参数
                    if (!BasePublic.SafeMode)
                    {
                        // args = new[] { "Release", "A51C.Plugin.Safe" };
                    }
                    // 传递参数
                    if (!args.IsEmptyStrings())
                    {
                        BasePublic.AppStartParas = args.ToList();
                    }
                    // 加载之后
                    BasePublic.Ui.Load += Load.LoadHelper.Ui_Load;
                    // 启动加载
                    System.Windows.Forms.Application.Run(BasePublic.Ui);
                }
            }
            else
            {
                // 激活下这个窗体
                HandleRunningInstance(process);
            }

            #endregion
        }

        #region 调用Win32 API，并激活并程序的窗口，显示在最前端

        private const int SwShownomal = 1;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);

        private static void HandleRunningInstance(System.Diagnostics.Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SwShownomal); //显示
            SetForegroundWindow(instance.MainWindowHandle); //当到最前端
        }

        private static System.Diagnostics.Process RuningInstance()
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var processes = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
            return
                processes.Where(process => process.Id != currentProcess.Id)
                         .FirstOrDefault(
                             process =>
                             System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
                             currentProcess.MainModule.FileName);
        }

        #region DLL丢失时补充辅助类
        /// <summary>
        /// DLL丢失时补充辅助类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, System.ResolveEventArgs args)
        {
            #region DLL丢失时补充辅助类

            var assemblyInfo = args.Name;
            if (assemblyInfo.IsNotNullOrEmpty())
            {
                var assemblyName = assemblyInfo.Split(',')[0];
                if (assemblyName.IsNotNullOrEmpty())
                {
                    var assemlyPath = string.Format("{0}\\{1}.dll", BasePublic.PluginsDirPath, assemblyName);
                    if (assemlyPath.IsExistFile())
                    {
                         return System.Reflection.Assembly.Load(
                             BasePublic.SafeMode
                             ?Dens.FileReadHelper.DecryptFormFilePath(assemlyPath, BasePublic.UiKey)
                             :System.IO.File.ReadAllBytes(assemlyPath));
                    }
                }
            }

            return null; 
            #endregion
        }

        #endregion

        #endregion
    }
}
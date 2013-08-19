namespace KCP.Guard
{
    public class OptimizeHelper
    {
        #region 公有化

        public static bool BeginOptimize()
        {
            try
            {
                // Optimize HTTP Server
                System.Net.ServicePointManager.DefaultConnectionLimit = 512;
                // Start Auto Optimize
                AutoOptimize = new System.Timers.Timer {Enabled = true, Interval = 5*1000};
                AutoOptimize.Start();
                AutoOptimize.Elapsed += AutoOptimize_Elapsed;
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 私有化

        private static void AutoOptimize_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().MinWorkingSet = new System.IntPtr(5);
                System.GC.Collect();
            }
            catch
            {
                AutoOptimize.Stop();
                AutoOptimize = null;
            }
        }

        private static System.Timers.Timer AutoOptimize { get; set; }

        #endregion
    }
}
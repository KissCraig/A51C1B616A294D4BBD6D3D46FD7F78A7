namespace KCP.Plugin.Demo
{
    public class PluginLoadHelper
    {
        public Control.Fase.LPanel MainPanel { get; set; }
        // 预留的待会儿主程序会调用
        public bool PluginLoaded()
        {
            MainPanel.BackColor = System.Drawing.Color.FromArgb(40,0,0,0);
            //var sss = new System.Windows.Forms.Panel
            //    {
            //        Dock = System.Windows.Forms.DockStyle.Fill, 
            //        BackColor = System.Drawing.Color.Transparent
            //    };
            //MainPanel.Controls.Add(sss);
            return true;
        }
    }
}

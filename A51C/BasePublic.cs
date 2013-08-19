
namespace A51C
{
    public static class BasePublic
    {
        /// <summary>
        /// 全局承载的主体界面
        /// </summary>
        public static System.Windows.Forms.Form Ui { get; set; }

        /// <summary>
        /// 全局承载的Tip提示
        /// </summary>
        public static System.Windows.Forms.ToolTip Tip = new System.Windows.Forms.ToolTip();

        /// <summary>
        /// 全局承载的安全Key
        /// </summary>
        public const string UiKey = "A51C.KCPlayer.-CB2D0CD9-E543-464D-9740-6B89F22AE41E";

        /// <summary>
        /// 是否开启安全模式，在开发调试阶段记得关闭此模式
        /// </summary>
        public static bool SafeMode = false;

        /// <summary>
        /// 全局承载的Notify托盘
        /// </summary>
        public static System.Windows.Forms.NotifyIcon Notify = new System.Windows.Forms.NotifyIcon {Visible = true};

        /// <summary>
        /// 全局默认文字字体
        /// </summary>
        public static System.Drawing.FontFamily KcpFrmFont { get; set; }

        /// <summary>
        /// 全局默认图标字体
        /// </summary>
        public static System.Drawing.FontFamily KcpBarFont { get; set; }

        /// <summary>
        /// 是否是深色主题,默认是深色背景
        /// </summary>
        public static bool IsDeep { get; set; }

        /// <summary>
        /// 是否为管理员,默认是非管理员
        /// </summary>
        public static bool IsAdmin { get; set; }

        /// <summary>
        /// 是否为服务器版,默认是本地版
        /// </summary>
        public static bool IsServer { get; set; }

        /// <summary>
        /// 本地数据文件夹
        /// </summary>
        public static string DataFolderDirectory { get; set; }

        /// <summary>
        /// 服务器数据请求地址
        /// </summary>
        public static string ServerRequestPath { get; set; }

        /// <summary>
        /// 服务器检查升级地址
        /// </summary>
        public static string CheckUpdatePath { get; set; }

        /// <summary>
        /// 全局插件存储目录
        /// </summary>
        public static string PluginsDirPath { get; set; }

        /// <summary>
        /// 全局程序启动目录
        /// </summary>
        public static string AppStartupPath { get; set; }

        /// <summary>
        /// 全局程序启动参数
        /// </summary>
        public static System.Collections.Generic.List<string> AppStartParas { get; set; } 



    }
}
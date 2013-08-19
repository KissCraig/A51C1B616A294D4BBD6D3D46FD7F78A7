using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using SongTasteMusicBox.Properties;
using System.IO;
using System.Windows.Forms;

namespace SongTasteMusicBox
{
    /// <summary>
    /// 共有信息 主程序读取类
    /// </summary>
    public class PublicInterFace
    {
        //内部版本号
        public static string Version = "1.0.0.0";
        //插件名称
        public static string Name = "SongTaste音乐盒";
        //插件描述
        public static string Description = "实时更新SongTaste网站 实时更新 ~";
        //插件作者
        public static string Author = "5L丶";
        //版权信息
        public static string Copyright = "Copyright ©  2012";
        //创作日期
        public static string MakeDate = "2013年1月6日";
        //主页磁铁颜色
        public static Color SqueryColor = Color.FromArgb(0, 128, 0);
        //主页磁铁所用字体
        public static Font Font = new Font("微软雅黑", 12f);
        //预留接口暂无用处
        public static string NormalSize = "Large";
        //绘制方式
        public static string DrawingWay = "System";
        //是否切换平板处理
        public static bool ChangeUI = true;
        //是否能打开更多
        public static bool CanOpenMore = false;

        public static void LogErro(string str)
        {
            StreamWriter reader = new StreamWriter(Application.StartupPath + "\\SongTasteError.log", true);
            reader.WriteLine(str + "\t--" + DateTime.Now.ToLongTimeString());
            reader.Close();
        }

        //获取图标信息
        public static Image GetMainIcon()
        {
            //可以从Resources读取资源返回也可以自己构造Image
            return Resources.SongTaste;
        }

        //获得外部程序集信息
        public static List<Assembly> GetAssemblys()
        {
            //如果没有则为Null
            List<Assembly> assemblys = new List<Assembly>();
            assemblys.Add(Assembly.Load(Resources.QuartzTypeLib));
            return assemblys;
        }
    }
}

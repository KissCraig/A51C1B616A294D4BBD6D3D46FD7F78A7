using System.Drawing;
using System.Reflection;

namespace KCPlayer.Plugin.LiuXing
{
    public class OuterMethod
    {
        public OuterMethod(MainInterFace face)
        {
            Base = face;
        }

        public MainInterFace Base { get; set; }

        /// <summary>
        ///     获得磁铁大小
        /// </summary>
        /// <returns></returns>
        public Size GetSquerySize()
        {
            return
                (Size)
                Base.OwnerParent.GetType()
                    .GetField("_normalSize", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(Base.OwnerParent);
        }

        /// <summary>
        ///     自定义绘制时所用的刷新方法
        /// </summary>
        public void Invalidate()
        {
            Base.OwnerParent.GetType().GetMethod("RefreshControl").Invoke(Base.OwnerParent, new object[] {Base.Guid});
        }

        /// <summary>
        ///     外部应用(非官方)更新方法
        /// </summary>
        /// <param name="version">版本号</param>
        /// <param name="downloadPath">下载地址</param>
        public void UpdatePlugin(string version, string downloadPath)
        {
            Base.OwnerParent.GetType()
                .GetMethod("IWantUpdate", BindingFlags.Public | BindingFlags.Instance)
                .Invoke(Base.OwnerParent, new object[] {Base.Guid, version, downloadPath});
        }

        /// <summary>
        ///     调用其他插件方法
        /// </summary>
        /// <param name="pluginname">需调用插件名称</param>
        /// <param name="pluginmethod">需调用插件方法</param>
        /// <param name="isActived">是否激活插件</param>
        /// <param name="objects">方法所需参数</param>
        /// <returns></returns>
        public object InvokeOuterMethod(
            string pluginname,
            string pluginmethod,
            bool isActived,
            params object[] objects)
        {
            return Base.OwnerParent.GetType().GetMethod("InvokePluginMethod").Invoke(
                Base.OwnerParent,
                new object[]
                    {
                        pluginname,
                        pluginmethod,
                        isActived,
                        objects
                    });
        }
    }
}
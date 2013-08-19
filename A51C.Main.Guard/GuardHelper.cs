
using A51C.Main.Guard.List;

namespace A51C.Main.Guard
{
    public class GuardHelper
    {
        /// <summary>
        /// 启动保护和优化
        /// </summary>
        /// <returns></returns>
        public static bool Start()
        {
            if (OptimizeHelper.BeginOptimize())
            {
                if (!BasePublic.SafeMode) return true;
                var listStr = new ListHelper().LoadList("KCPlayerBlock.txt");
                if (listStr.IsNotNullOrEmpty())
                {
                    SecurityHelper.SafeList = listStr.Deserialize<System.Collections.Generic.List<string>>();
                    new System.Threading.Tasks.Task(SecurityHelper.BeginSafe).Start();
                    return true;
                }
                @"程序默认保护加载失败,可能是人为破坏".ToErrorMsgBox("");
                return false;
            }
            @"程序默认优化加载失败,可能是系统兼容问题".ToErrorMsgBox("");
            return false;
        }
    }
}
namespace KCPlayer.Plugin.TestVod
{
    public class TestVodStart
    {
        public static void LoadVodStartPal()
        {
            // 进行数据源复位
            TestVodReset.MakeTestVodReset();
            // 加载整个界面元素
            TestVodPal.LoadPublicPal();
        }
    }
}
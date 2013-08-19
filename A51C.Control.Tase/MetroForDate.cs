
using System.Collections.Generic;
using A51C.Control.Base;
using A51C.Control.Fase;

namespace A51C.Control.Tase
{
    public sealed class MetroForDate : HPanel
    {
        public string DateResult { get; set; }
        private System.Drawing.FontFamily FontFamily { get; set; }

        private int NowYear { get; set; }
        private int NowMonth { get; set; }
        private int NowDay { get; set; }
        private int NowWeek { get; set; }
        private MetroForList Now { get; set; }
        private MetroForList YearDate { get; set; }
        private MetroForList ZhouList { get; set; }
        private MetroForList XingQiList { get; set; }
        private MetroForList DateTable { get; set; }
        private System.Drawing.Color IforeColor { get; set; }
        private System.Drawing.Color IbackColor { get; set; }

        public MetroForDate(
            System.Windows.Forms.Control fatherControl, // 父容器
            int smallWidth,     // 每个小单元的宽度
            int smallHeight,    // 每个小单元的高度
            System.Drawing.Color foreColor,    // 小单元字体颜色
            System.Drawing.Color backColor,    // 小单元背景颜色
            System.Drawing.FontFamily fontFamily,
            System.Drawing.Point locationPoint,    // 整体位置
            System.Windows.Forms.MouseEventHandler listItemMouseClick   // 小单元点击事件
            )
        {
            Location = locationPoint;
            FontFamily = fontFamily;
            IforeColor = foreColor;
            IbackColor = backColor;
            Size = new System.Drawing.Size(smallWidth, smallHeight);

            NowYear = System.DateTime.Now.Year;
            NowMonth = System.DateTime.Now.Month;
            NowDay = System.DateTime.Now.Day;
            NowWeek = 12;

            new MetroForList(
                this,
                true,
                "",
                new List<object> { "当前日期,Desc"},
                null,
                118,
                64,
                IforeColor,
                IbackColor,
                FontFamily,
                new System.Drawing.Point(0, 0),
                listItemMouseClick,
                null
                );


            Now = new MetroForList(
               this,
               true,
               "",
               null,
               null,
               413,
               64,
               IforeColor,
               IbackColor,
               FontFamily,
               new System.Drawing.Point(118, 0),
               listItemMouseClick,
                null
               );
            Update_MetroForDate_Now();

            new MetroForList(
               this,
               false,
               "",
               new List<object> { "<<,Button" }, 
               null,
               118,
               32,
               IforeColor,
               IbackColor,
               FontFamily,
               new System.Drawing.Point(0, 63),
               MetroForDate_Year_Prev_MouseClick,
                null
               );
           YearDate = new MetroForList(
               this,
               false,
               "",
               null,
               null,
               295,
               32,
               IforeColor,
               IbackColor,
               FontFamily,
               new System.Drawing.Point(118, 63),
               listItemMouseClick,
                null
               );

            Update_MetroForDate_Year();

            new MetroForList(
               this,
               false,
               "",
               new List<object> { ">>,Button" },
               null,
               118,
               32,
               IforeColor,
               IbackColor,
               FontFamily,
               new System.Drawing.Point(118 + 295, 63),
               MetroForDate_Year_Next_MouseClick,
                null
               );

            ZhouList = new MetroForList(
                this,
                false,
                "",
                null,
                null,
                118,
                32,
                IforeColor,
                IbackColor,
                FontFamily,
                new System.Drawing.Point(0, 63 + 33),
                listItemMouseClick,
                null
                );
            ZhouList.ListItemTxts=new List<object>()
                {
                    "周　历,Button","第一周,Button","第二周,Button","第三周,Button","第四周,Button","第五周,Button","第六周,Button"
                };
            ZhouList.UpdateListItem();

            XingQiList = new MetroForList(
                this,
                true,
                "Hide,Button",
                null,
                null,
                58,
                32,
                IforeColor,
                IbackColor,
                FontFamily,
                new System.Drawing.Point(118 + 1, 63 + 33),
                listItemMouseClick,
                null
                );
            XingQiList.ListItemTxts = new List<object>
                {
                    "一","二","三","四","五","六","日"
                };
            XingQiList.UpdateListItem();

            DateTable = new MetroForList(
                this,
                false,
                "Table,7",
                null,
                null,
                58,
                32,
                IforeColor,
                IbackColor,
                FontFamily,
                new System.Drawing.Point(XingQiList.Location.X, 63 + 33 + 33),
                MetroForDate_Date_MouseClick,
                null
                );
            Update_MetroForDate_DateTable();
            //BackColor = System.Drawing.Color.WhiteSmoke;
            fatherControl.Controls.Add(this); 
        }

        private void MetroForDate_Year_Prev_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            var ser = sender as ELabel;
            if (ser != null)
            {
                NowMonth--;
                if (NowMonth == 0)
                {
                    NowYear--;
                    NowMonth = 12;
                }
                Update_MetroForDate_Year();
            }
        }

        private void MetroForDate_Year_Next_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            var ser = sender as ELabel;
            if (ser != null)
            {
                NowMonth++;
                if (NowMonth == 13)
                {
                    NowYear++;
                    NowMonth = 1;
                }
                Update_MetroForDate_Year();
            }
        }

        private void MetroForDate_Date_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = sender as ELabel;
            if (ser != null)
            {
                var dateItem = ser.Tag as DateItem;
                if (dateItem != null)
                {
                    NowDay = dateItem.Day;
                    NowMonth = dateItem.Month;
                    NowWeek = dateItem.Week;
                    NowYear = dateItem.Year;
                }
                Update_MetroForDate_Now();
            }
        }

        private void Update_MetroForDate_Year()
        {
            YearDate.ListItemTxts = new List<object>
                {
                    string.Format("{0}年{1}月,Button",NowYear,NowMonth)
                };
            YearDate.UpdateListItem();
            Update_MetroForDate_Now();
        }

        private void Update_MetroForDate_Now()
        {
            Now.ListItemTxts = new List<object>
                {
                    string.Format("{0}年{1}月{2}日第{3}周,Desc",NowYear,NowMonth,NowDay,NowWeek)
                };
            Now.UpdateListItem();
        }

        private void Update_MetroForDate_DateTable()
        {
            Make();
            if (DateItems != null)
            {
                if (DateItems.Count > 0)
                {
                    DateTable.ListItemTxts = new List<object>();
                    DateTable.ListItemTips = new List<object>();
                    var today = new DateItem {Day = NowDay, Month = NowMonth, Week = NowWeek, Year = NowYear};

                    foreach (var dateItem in DateItems)
                    {
                        if (dateItem.IsToday)
                        {
                            DateTable.ListItemTxts.Add(dateItem.Day+",Select");
                            DateTable.ListItemTips.Add(dateItem);
                        }
                        else
                        {
                            DateTable.ListItemTxts.Add(dateItem.Day);
                            DateTable.ListItemTips.Add(dateItem);
                        }
                    }
                }
            }
            DateTable.UpdateListItem();
        }
        private void Make()
        {
            DateItems = new System.Collections.Generic.List<DateItem>();
            for (var i = 29; i < 32; i++)
            {
                DateItems.Add(new DateItem
                    {
                        Day = i,
                        Month = NowMonth - 1,
                        Week = NowWeek - 1,
                        Year = NowYear
                    });
            }
            for (var i = 1; i < 32; i++)
            {
                DateItems.Add(new DateItem
                {
                    Day = i,
                    Month = NowMonth,
                    Week = NowWeek,
                    Year = NowYear,
                    IsToday = i == NowDay
                });
            }
            for (var i = 1; i < 9; i++)
            {
                DateItems.Add(new DateItem
                {
                    Day = i,
                    Month = NowMonth+1,
                    Week = NowWeek+1,
                    Year = NowYear
                });
            }
        }

        private void Make_Week()
        {
            WeekItems = new List<int>
                {
                    NowWeek - 2,
                    NowWeek - 1,
                    NowWeek,
                    NowWeek + 1,
                    NowWeek + 2,
                    NowWeek + 3
                };
        }

        private void Update_w()
        {
            if (WeekItems != null)
            {
                if (WeekItems.Count > 0)
                {
                    ZhouList.ListItemTxts = new List<object> {"周　历,Button"};
                    foreach (var weekItem in WeekItems)
                    {
                        ZhouList.ListItemTxts.Add(string.Format("第{0}周,Button", weekItem));
                    }
                }
            }

            ZhouList.UpdateListItem();
        }

        public System.Collections.Generic.List<DateItem> DateItems { get; set; }
        public System.Collections.Generic.List<int> WeekItems { get; set; }
        public class DateItem
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
            public int Week { get; set; }
            public bool IsToday { get; set; }
        }
    }
}

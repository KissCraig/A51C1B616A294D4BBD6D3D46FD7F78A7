namespace A51C
{
    public static class BaseColor
    {
        /// <summary>
        ///  String -> colorCode -> Color
        /// </summary>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        public static System.Drawing.Color GetColorByCode(this string colorCode)
        {
            if (colorCode.Contains("#"))
            {
                try
                {
                    return System.Drawing.ColorTranslator.FromHtml(colorCode);
                }
                catch
                {
                    return System.Drawing.Color.Transparent;
                }
            }
            colorCode = "#" + colorCode;
            try
            {
                return System.Drawing.ColorTranslator.FromHtml(colorCode);
            }
            catch
            {
                return System.Drawing.Color.Transparent;
            }
        }

        /// <summary>
        /// Color -> String
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string GetCodeByColor(this System.Drawing.Color color)
        {
            string colorCode;
            try
            {
                colorCode = System.Drawing.ColorTranslator.ToHtml(color);
            }
            catch
            {
                return null;
            }
            return colorCode;
        }

        /// <summary>
        /// String -> colorName ->  Color
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        public static System.Drawing.Color GetColorByName(this string colorName)
        {
            switch (colorName)
            {
                case "绿色":
                    {
                        return System.Drawing.Color.FromArgb(33, 115, 70);
                    }
                case "暗红":
                    {
                        return System.Drawing.Color.FromArgb(161, 53, 56);
                    }
                case "暗紫":
                    {
                        return System.Drawing.Color.FromArgb(124, 55, 119);
                    }
                case "亮蓝":
                    {
                        return System.Drawing.Color.FromArgb(0, 111, 197);
                    }
                case "橘红":
                    {
                        return System.Drawing.Color.FromArgb(208, 69, 37);
                    }
                case "墨绿":
                    {
                        return System.Drawing.Color.FromArgb(7, 113, 100);
                    }
                case "暗蓝":
                    {
                        return System.Drawing.Color.FromArgb(41, 84, 150);
                    }
                case "深紫":
                    {
                        return System.Drawing.Color.FromArgb(103, 55, 134);
                    }
                case "明蓝":
                    {
                        return System.Drawing.Color.FromArgb(26, 130, 203);
                    }
                case "亮绿":
                    {
                        return System.Drawing.Color.FromArgb(0, 161, 0);
                    }
                case "米黄":
                    {
                        return System.Drawing.Color.FromArgb(254, 119, 0);
                    }
                case "中蓝":
                    {
                        return System.Drawing.Color.FromArgb(15, 134, 216);
                    }
                case "亮黑":
                    {
                        return System.Drawing.Color.FromArgb(72, 72, 72);
                    }
                case "中黑":
                    {
                        return System.Drawing.Color.FromArgb(36, 36, 36);
                    }
                case "灰黑":
                    {
                        return System.Drawing.Color.FromArgb(111, 111, 111);
                    }
                case "灰白":
                    {
                        return System.Drawing.Color.FromArgb(233, 235, 236);
                    }
                case "咖啡粉":
                    {
                        return System.Drawing.Color.FromArgb(246, 243, 237);
                    }
                case "咖啡色":
                    {
                        return System.Drawing.Color.FromArgb(242, 237, 223);
                    }
                case "咖啡黄":
                    {
                        return System.Drawing.Color.FromArgb(227, 215, 185);
                    }
                case "深黄":
                    {
                        return System.Drawing.Color.FromArgb(231, 202, 114);
                    }
                case "淡黄":
                    {
                        return System.Drawing.Color.FromArgb(237, 216, 152);
                    }
                case "淡绿":
                    {
                        return System.Drawing.Color.FromArgb(175, 206, 131);
                    }
                case "草绿":
                    {
                        return System.Drawing.Color.FromArgb(188, 223, 98);
                    }
                case "深绿":
                    {
                        return System.Drawing.Color.FromArgb(139, 158, 75);
                    }
                case "暗黄":
                    {
                        return System.Drawing.Color.FromArgb(216, 208, 186);
                    }
                case "天空蓝":
                    {
                        return System.Drawing.Color.FromArgb(61, 169, 231);
                    }
                case "头像灰":
                    {
                        return System.Drawing.Color.FromArgb(190, 193, 201);
                    }
                case "内嵌灰":
                    {
                        return System.Drawing.Color.FromArgb(226, 221, 209);
                    }
                case "土黄":
                    {
                        return System.Drawing.Color.FromArgb(101, 64, 38);
                    }
                case "深土":
                    {
                        return System.Drawing.Color.FromArgb(63, 34, 15);
                    }
            }
            return System.Drawing.Color.Transparent;
        }
    }
}
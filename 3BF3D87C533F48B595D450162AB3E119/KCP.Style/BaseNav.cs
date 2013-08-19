namespace App.Style
{
    public sealed class BaseNavOff : ELabel
    {
        private System.Drawing.Color Backcolor { get; set; }
        private System.Drawing.Color Backlight { get; set; }
        private System.Drawing.Color Forecolor { get; set; }
        private System.Drawing.Color Forelight { get; set; }

        public BaseNavOff
            (
            System.Windows.Forms.Control parentpal,
            System.Drawing.Size size,
            System.Drawing.Point point,
            float fontSize,
            System.Drawing.Color backcolor,
            System.Drawing.Color backlight,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Backlight = backlight;
            Forelight = forelight;
            BackColor = Backcolor = backcolor;
            ForeColor = Forecolor = forecolor;
            Size = size;
            Location = point;
            Text = @"´";
            TextAlign = Helper.Align;
            Cursor = Helper.HCursors;
            Font = new System.Drawing.Font(Helper.FontSymbol, fontSize);
            Anchor = anchorstyle;
            parentpal.Controls.Add(this);
            MouseClick += BaseNavOff_MouseClick;
            MouseLeave += BaseNavOff_MouseLeave;
            MouseUp += BaseNavOff_MouseUp;
            MouseDown += BaseNavOff_MouseDown;
            MouseHover += BaseNavOff_MouseHover;
        }

        private void BaseNavOff_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GetLeaveOrUp((BaseNavOff) sender);
        }

        private void BaseNavOff_MouseLeave(object sender, System.EventArgs e)
        {
            GetLeaveOrUp((BaseNavOff) sender);
        }

        private void BaseNavOff_MouseHover(object sender, System.EventArgs e)
        {
            GetDownOrHover((BaseNavOff) sender);
        }

        private void BaseNavOff_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GetDownOrHover((BaseNavOff) sender);
        }

        /// <summary>
        /// 执行鼠标抬起或离开动作
        /// </summary>
        /// <param name="navBar"></param>
        private void GetLeaveOrUp(System.Windows.Forms.Control navBar)
        {
            navBar.ForeColor = Forecolor;
            navBar.BackColor = Backcolor;
        }

        /// <summary>
        /// 执行鼠标移上或按下动作
        /// </summary>
        /// <param name="navBar"></param>
        private void GetDownOrHover(System.Windows.Forms.Control navBar)
        {
            navBar.ForeColor = Forelight;
            navBar.BackColor = Backlight;
        }

        /// <summary>
        /// 执行鼠标点击动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void BaseNavOff_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }

    public sealed class BaseNavMin : ELabel
    {
        private System.Drawing.Color Backcolor { get; set; }
        private System.Drawing.Color Backlight { get; set; }
        private System.Drawing.Color Forecolor { get; set; }
        private System.Drawing.Color Forelight { get; set; }

        public BaseNavMin
            (
            System.Windows.Forms.Control parentpal,
            System.Drawing.Size size,
            System.Drawing.Point point,
            float fontSize,
            System.Drawing.Color backcolor,
            System.Drawing.Color backlight,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Backlight = backlight;
            Forelight = forelight;
            BackColor = Backcolor = backcolor;
            ForeColor = Forecolor = forecolor;
            Size = size;
            Location = point;
            Text = @"-";
            TextAlign = Helper.Align;
            Cursor = Helper.HCursors;
            Font = new System.Drawing.Font(Helper.FontSymbol, fontSize);
            Anchor = anchorstyle;
            parentpal.Controls.Add(this);
            MouseClick += BaseNavMin_MouseClick;
            MouseLeave += BaseNavMin_MouseLeave;
            MouseUp += BaseNavMin_MouseUp;
            MouseDown += BaseNavMin_MouseDown;
            MouseHover += BaseNavMin_MouseHover;
        }

        private void BaseNavMin_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GetLeaveOrUp((BaseNavMin) sender);
        }

        private void BaseNavMin_MouseLeave(object sender, System.EventArgs e)
        {
            GetLeaveOrUp((BaseNavMin) sender);
        }

        private void BaseNavMin_MouseHover(object sender, System.EventArgs e)
        {
            GetDownOrHover((BaseNavMin) sender);
        }

        private void BaseNavMin_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GetDownOrHover((BaseNavMin) sender);
        }

        /// <summary>
        /// 执行鼠标抬起或离开动作
        /// </summary>
        /// <param name="navBar"></param>
        private void GetLeaveOrUp(System.Windows.Forms.Control navBar)
        {
            navBar.ForeColor = Forecolor;
            navBar.BackColor = Backcolor;
        }

        /// <summary>
        /// 执行鼠标移上或按下动作
        /// </summary>
        /// <param name="navBar"></param>
        private void GetDownOrHover(System.Windows.Forms.Control navBar)
        {
            navBar.ForeColor = Forelight;
            navBar.BackColor = Backlight;
        }

        /// <summary>
        /// 执行鼠标点击动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void BaseNavMin_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            BasePublic.Ui.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
    }
}
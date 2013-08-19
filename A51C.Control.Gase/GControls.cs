using System.Linq;
using A51C.Control.Base;
using A51C.Control.Fase;

namespace A51C.Control.Gase
{
    /// <summary>
    /// Frm.Bar.Btn
    /// </summary>
    public sealed class FrmBarBtn : ELabel
    {
        public FrmBarBtn(
            System.Windows.Forms.Control parentpal,
            string predesc,
            System.Drawing.Point point,
            System.Windows.Forms.AnchorStyles anchor)
        {
            Text = predesc;
            Font = new System.Drawing.Font(BasePublic.KcpBarFont, 20F);
            Size = new System.Drawing.Size(48, 48);
            Location = point;
            Cursor = System.Windows.Forms.Cursors.Hand;
            ForeColor = System.Drawing.Color.FromArgb(220, 220, 220, 220);
            TextAlign = BaseAlign.AlignMiddleCenter;
            BackColor = System.Drawing.Color.Transparent;
            Anchor = anchor;
            parentpal.Controls.Add(this);
            MouseDown += FrmBarBtn_MouseDown;
            MouseHover += FrmBarBtn_MouseHover;
            MouseLeave += FrmBarBtn_MouseLeave;
            MouseUp += FrmBarBtn_MouseUp;
        }

        private static void FrmBarBtn_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (((FrmBarBtn) sender).Text == "T" && BasePublic.Ui.TopMost)
            {
                ((FrmBarBtn) sender).ForeColor = System.Drawing.Color.FromArgb(220, 254, 254, 254);
            }
            else
            {
                ((FrmBarBtn) sender).ForeColor = System.Drawing.Color.FromArgb(220, 220, 220, 220);
            }
        }

        private static void FrmBarBtn_MouseLeave(object sender, System.EventArgs e)
        {
            if (((FrmBarBtn) sender).Text == "T" && BasePublic.Ui.TopMost)
            {
                ((FrmBarBtn) sender).ForeColor = System.Drawing.Color.FromArgb(220, 254, 254, 254);
            }
            else
            {
                ((FrmBarBtn) sender).ForeColor = System.Drawing.Color.FromArgb(220, 220, 220, 220);
            }
        }

        private static void FrmBarBtn_MouseHover(object sender, System.EventArgs e)
        {
            ((FrmBarBtn) sender).ForeColor = System.Drawing.Color.FromArgb(220, 254, 254, 254);
        }

        private static void FrmBarBtn_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((FrmBarBtn) sender).ForeColor = System.Drawing.Color.FromArgb(220, 254, 254, 254);
        }
    }

    public sealed class InputBoxWithDesc : ETextBox
    {
        private System.Drawing.Color Linecolor { get; set; }
        private System.Drawing.Color Linelight { get; set; }
        private System.Drawing.Color Backcolor { get; set; }
        private System.Drawing.Color Backlight { get; set; }
        private System.Drawing.Color Forecolor { get; set; }
        private System.Drawing.Color Forelight { get; set; }
        private string PreWord { get; set; }

        public InputBoxWithDesc
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            int lineflo,
            string predesc,
            string preword,
            string password,
            System.Drawing.Font descfont,
            System.Drawing.Size descsize,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color defrcolor,
            System.Drawing.Color debkcolor,
            System.Drawing.Color linecolor,
            System.Drawing.Color linelight,
            System.Drawing.Color backcolor,
            System.Drawing.Color backlight,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Linecolor = linecolor;
            Linelight = linelight;
            Backcolor = backcolor;
            Backlight = backlight;
            Forecolor = forecolor;
            Forelight = forelight;

            var inputdesc = new HDarge
                (
                parentpal,
                predesc,
                descfont,
                descsize,
                point,
                defrcolor,
                debkcolor,
                BaseAlign.AlignMiddleCenter,
                BaseAnchor.AnchorLeftFill
                );

            var inputPal = new LPanel
                (
                parentpal,
                lineint,
                size,
                new System.Drawing.Point(inputdesc.Location.X + inputdesc.Width, inputdesc.Location.Y),
                Linecolor,
                Backcolor,
                anchorstyle
                );
            PreWord = preword;
            Text = preword.ToSafeValue();
            Multiline = true;
            Size = new System.Drawing.Size(inputPal.Size.Width - 8*lineint, inputPal.Size.Height - 4*lineint);
            Location = new System.Drawing.Point(4*lineint, (4 + lineflo)*lineint);
            BackColor = Backcolor;
            ForeColor = Forecolor;
            Font = font;
            Anchor = anchorstyle;
            if (!string.IsNullOrEmpty(password))
            {
                PasswordChar = password.ToCharArray()[0];
            }
            Leave += LInput_Leave;
            Enter += LInput_Enter;
            GotFocus += InputBoxWithDesc_GotFocus;
            LostFocus += InputBoxWithDesc_LostFocus;
            inputPal.Controls.Add(this);
        }
        private void InputBoxWithDesc_LostFocus(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(((InputBoxWithDesc)sender).Text))
            {
                ((InputBoxWithDesc)sender).Text = PreWord;
            }
        }

        private void InputBoxWithDesc_GotFocus(object sender, System.EventArgs e)
        {
            if (((InputBoxWithDesc)sender).Text == PreWord)
            {
                ((InputBoxWithDesc)sender).Text = string.Empty;
            }
        }
        private void LInput_Enter(object sender, System.EventArgs e)
        {
            EnterOrLeave((InputBoxWithDesc) sender, true);
        }

        private void LInput_Leave(object sender, System.EventArgs e)
        {
            EnterOrLeave((InputBoxWithDesc) sender, false);
        }

        private void EnterOrLeave(System.Windows.Forms.Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.Parent.BackColor = ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }

    public sealed class InputSearch : ETextBox
    {
        private System.Drawing.Color Linecolor { get; set; }
        private System.Drawing.Color Linelight { get; set; }
        private System.Drawing.Color Backcolor { get; set; }
        private System.Drawing.Color Backlight { get; set; }
        private System.Drawing.Color Forecolor { get; set; }
        private System.Drawing.Color Forelight { get; set; }
        private string PreWord { get; set; }

        public InputSearch
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            int lineflo,
            string predesc,
            string preword,
            System.Drawing.Font descfont,
            System.Drawing.Size descsize,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color defrcolor,
            System.Drawing.Color debkcolor,
            System.Drawing.Color linecolor,
            System.Drawing.Color linelight,
            System.Drawing.Color backcolor,
            System.Drawing.Color backlight,
            System.Drawing.Color forecolor,
            System.Drawing.Color forelight,
            System.Windows.Forms.MouseEventHandler searchEnter,
            System.Windows.Forms.KeyEventHandler searchEnterKey,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Linecolor = linecolor;
            Linelight = linelight;
            Backcolor = backcolor;
            Backlight = backlight;
            Forecolor = forecolor;
            Forelight = forelight;

            var inputPal = new LPanel
                (
                parentpal,
                lineint,
                size,
                point,
                Linecolor,
                Backcolor,
                anchorstyle
                );

            var inputdesc = new LButton
                (
                inputPal,
                0,
                predesc,
                descfont,
                descsize,
                new System.Drawing.Point(size.Width - descsize.Width, -1),
                debkcolor,
                debkcolor,
                debkcolor,
                System.Drawing.Color.FromArgb(51, debkcolor),
                defrcolor,
                defrcolor,
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top
                );
            inputdesc.MouseClick += searchEnter;

            inputdesc.Focus();
            PreWord = preword;
            Text = preword;
            Multiline = false;
            Size = new System.Drawing.Size(inputPal.Size.Width - 8*lineint - descsize.Width,
                                           inputPal.Size.Height - 4*lineint);
            Location = new System.Drawing.Point(4*lineint, (4 + lineflo)*lineint);
            BackColor = Backcolor;
            ForeColor = Forecolor;
            Font = font;
            Anchor = anchorstyle;
            AllowDrop = true;
            DragEnter += FPanel_DragEnter;

            Leave += LInput_Leave;
            Enter += LInput_Enter;
            KeyDown += searchEnterKey;
            GotFocus += InputBoxWithDesc_GotFocus;
            LostFocus += InputBoxWithDesc_LostFocus;
            inputPal.Controls.Add(this);
        }

        private void InputBoxWithDesc_LostFocus(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(((InputSearch)sender).Text))
            {
                ((InputSearch)sender).Text = PreWord;
            }
        }

        private void InputBoxWithDesc_GotFocus(object sender, System.EventArgs e)
        {
            if (((InputSearch)sender).Text == PreWord)
            {
                ((InputSearch)sender).Text = string.Empty;
            }
        }

        private static void FPanel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }

        private void LInput_Enter(object sender, System.EventArgs e)
        {
            EnterOrLeave((InputSearch) sender, true);
        }

        private void LInput_Leave(object sender, System.EventArgs e)
        {
            EnterOrLeave((InputSearch) sender, false);
        }

        private void EnterOrLeave(System.Windows.Forms.Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.Parent.BackColor = ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }

    public class RadioSigleBox : ECheckBox
    {
        private HLabel Radiotap { get; set; }
// ReSharper disable UnusedAutoPropertyAccessor.Local
        private bool Single { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Local
        private string Group { get; set; }
        public string Value { get; set; }

        public RadioSigleBox
            (
            System.Windows.Forms.Control parentpal,
            string rdbtext,
            bool isChecked,
            bool single,
            string group,
            float fla,
            int flo,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Font font,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Windows.Forms.AnchorStyles anchorstyle,
            System.Windows.Forms.MouseEventHandler touchRadio
            )
        {
            Group = group;
            Single = single;
            Size = size;
            Location = point;
            Tag = Group;
            Value = rdbtext;
            Checked = isChecked;

            Radiotap = new HLabel
                (
                this,
                isChecked ? @"þ" : @"¨",
                new System.Drawing.Font(@"Wingdings", fla),
                new System.Drawing.Size(size.Height - 6 - flo, size.Height),
                new System.Drawing.Point(0, 0),
                forecolor,
                backcolor,
                BaseAlign.AlignMiddleCenter,
                anchorstyle
                );
            var radiodesc = new HLabel
                (
                this,
                rdbtext,
                font,
                new System.Drawing.Size(size.Width - Radiotap.Width, size.Height),
                new System.Drawing.Point(Radiotap.Location.X + Radiotap.Width, -1),
                forecolor,
                backcolor,
                System.Drawing.ContentAlignment.MiddleLeft,
                anchorstyle
                );
            Radiotap.MouseDown += touchRadio;
            radiodesc.MouseDown += touchRadio;
            CheckedChanged += RadioSigleBox_CheckedChanged;
            parentpal.Controls.Add(this);
        }

        private void RadioSigleBox_CheckedChanged(object sender, System.EventArgs e)
        {
            Radiotap.Text = ((RadioSigleBox) sender).Checked ? @"þ" : @"¨";
        }
    }

    public class CheckSingleBox : ECheckBox
    {
        private HLabel Radiotap { get; set; }
        private bool Single { get; set; }
        private string Group { get; set; }
        public string Value { get; set; }


        public CheckSingleBox
            (
            System.Windows.Forms.Control parentpal,
            string rdbtext,
            bool isChecked,
            bool single,
            string group,
            float fla,
            int flo,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Font font,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Group = group;
            Single = single;
            Size = size;
            Location = point;
            Tag = Group;
            Value = rdbtext;
            Checked = isChecked;

            Radiotap = new HLabel
                (
                this,
                isChecked ? @"þ" : @"¨",
                new System.Drawing.Font(@"Wingdings", fla),
                new System.Drawing.Size(size.Height - 6 - flo, size.Height),
                new System.Drawing.Point(0, 0),
                forecolor,
                backcolor,
                BaseAlign.AlignMiddleCenter,
                anchorstyle
                );
            var radiodesc = new HLabel
                (
                this,
                rdbtext,
                font,
                new System.Drawing.Size(size.Width - Radiotap.Width, size.Height),
                new System.Drawing.Point(Radiotap.Location.X + Radiotap.Width, -1),
                forecolor,
                backcolor,
                System.Drawing.ContentAlignment.MiddleLeft,
                anchorstyle
                );
            Radiotap.MouseDown += RadioButton_MouseDown;
            radiodesc.MouseDown += RadioButton_MouseDown;
            CheckedChanged += CheckSingleBox_CheckedChanged;
            parentpal.Controls.Add(this);
        }

        private void CheckSingleBox_CheckedChanged(object sender, System.EventArgs e)
        {
            Radiotap.Text = ((CheckSingleBox) sender).Checked ? @"þ" : @"¨";
            var ser = (CheckSingleBox) sender;
            var parevalue = ((CheckMultiBox) ser.Parent.Parent.Parent.Parent).CheckValue;
            switch (((CheckSingleBox) sender).Checked)
            {
                case true:
                    {
                        if (!Single)
                        {
                            if (!parevalue.Contains(Value))
                            {
                                parevalue.Add(Value);
                            }
                        }
                        else
                        {
                            parevalue.Clear();
                            parevalue.Add(Value);
                        }
                    }
                    break;
                case false:
                    {
                        if (parevalue.Contains(Value))
                        {
                            parevalue.Remove(Value);
                        }
                    }
                    break;
            }
        }

        private void RadioButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var ser = (HLabel) sender;
            switch (Radiotap.Text)
            {
                case @"þ":
                    {
                        ((CheckSingleBox) ser.Parent).Checked = false;
                    }
                    break;
                case @"¨":
                    {
                        if (Single)
                        {
                            foreach (
                                var variable in
                                    ser.Parent.Parent.Controls.OfType<CheckSingleBox>()
                                       .Where(variable => (string) variable.Tag == Group))
                            {
                                (variable).Checked = false;
                            }
                        }
                        ((CheckSingleBox) ser.Parent).Checked = true;
                    }
                    break;
            }
        }
    }

    public sealed class CheckMultiBox : HPanel
    {
        public System.Collections.Generic.List<string> CheckValue { get; set; }

        public void GetThisClear(bool all)
        {
            foreach (
                var m in
                    Controls.OfType<HPanel>()
                            .SelectMany(
                                t =>
                                (t).Controls.OfType<LPanel>()
                                   .SelectMany(
                                       k =>
                                       (k).Controls.OfType<LFlyPal>()
                                          .SelectMany(n => (n).Controls.OfType<CheckSingleBox>()))))
            {
                (m).Checked = all;
            }
        }

        public void GetThisCheck(string bychecked, bool check)
        {
            foreach (
                var m in
                    Controls.OfType<HPanel>()
                            .SelectMany(
                                t =>
                                (t).Controls.OfType<LPanel>()
                                   .SelectMany(
                                       k =>
                                       (k).Controls.OfType<LFlyPal>()
                                          .SelectMany(
                                              n =>
                                              (n).Controls.OfType<CheckSingleBox>().Where(m => (m).Value == bychecked))))
                )
            {
                (m).Checked = check;
            }
        }

        public CheckMultiBox
            (
            System.Windows.Forms.Control parentpal,
            string[] values,
            string group,
            bool single,
            float celldesc,
            float cellfit,
            int lineint,
            int lineflo,
            int descsize,
            System.Drawing.Size cellsize,
            System.Drawing.Size allsize,
            System.Drawing.Point point,
            System.Drawing.Font font,
            System.Drawing.Color pabgcolor,
            System.Drawing.Color linecolor,
            System.Drawing.Color foreColor,
            System.Drawing.Color fodecolor,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            Size = allsize;
            Location = point;
            Anchor = anchorstyle;
            CheckValue = new System.Collections.Generic.List<string>();
            if (!single)
            {
                CheckValue.Add(values[0]);
            }

// ReSharper disable ObjectCreationAsStatement
            new HDarge
// ReSharper restore ObjectCreationAsStatement
                (
                this,
                group,
                font,
                new System.Drawing.Size(descsize, allsize.Height),
                new System.Drawing.Point(0, 0),
                pabgcolor,
                foreColor,
                BaseAlign.AlignMiddleCenter,
                anchorstyle
                );
            var checkMultiPal = new LPanel
                (
                this,
                lineint,
                new System.Drawing.Size(allsize.Width - descsize, allsize.Height),
                new System.Drawing.Point(descsize, 0),
                linecolor,
                fodecolor,
                anchorstyle
                );
            var flyPal = new LFlyPal
                (
                checkMultiPal,
                new System.Drawing.Size(checkMultiPal.Width - 10, allsize.Height),
                new System.Drawing.Point(10, lineflo),
                BaseAnchor.AnchorFill
                );
            foreach (var value in values)
            {
                if (single)
                {
// ReSharper disable ObjectCreationAsStatement
                    new CheckSingleBox
// ReSharper restore ObjectCreationAsStatement
                        (
                        flyPal,
                        value,
                        false,
                        true,
                        group,
                        celldesc,
                        1,
                        cellsize,
                        new System.Drawing.Point(0, 0),
                        new System.Drawing.Font(font.FontFamily, cellfit),
                        foreColor,
                        fodecolor,
                        BaseAnchor.AnchorTopRight
                        );
                }
                else
                {
                    if (value == values[0])
                    {
// ReSharper disable ObjectCreationAsStatement
                        new CheckSingleBox
// ReSharper restore ObjectCreationAsStatement
                            (
                            flyPal,
                            value,
                            true,
                            false,
                            group,
                            celldesc,
                            1,
                            cellsize,
                            new System.Drawing.Point(0, 0),
                            new System.Drawing.Font(font.FontFamily, cellfit),
                            foreColor,
                            fodecolor,
                            BaseAnchor.AnchorTopRight
                            );
                    }
                    else
                    {
// ReSharper disable ObjectCreationAsStatement
                        new CheckSingleBox
// ReSharper restore ObjectCreationAsStatement
                            (
                            flyPal,
                            value,
                            false,
                            false,
                            group,
                            celldesc,
                            1,
                            cellsize,
                            new System.Drawing.Point(0, 0),
                            new System.Drawing.Font(font.FontFamily, cellfit),
                            foreColor,
                            fodecolor,
                            BaseAnchor.AnchorTopRight
                            );
                    }
                }
            }
            parentpal.Controls.Add(this);
        }
    }

    public class EnBrowser : System.Windows.Forms.WebBrowser
    {
        private System.Windows.Forms.AxHost.ConnectionPointCookie _cookie;
        private WebBrowserExtendedEvents _events;

        //This method will be called to give you a chance to create your own event sink
        protected override void CreateSink()
        {
            //MAKE SURE TO CALL THE BASE or the normal events won't fire
            base.CreateSink();
            _events = new WebBrowserExtendedEvents(this);
            _cookie = new System.Windows.Forms.AxHost.ConnectionPointCookie(ActiveXInstance, _events,
                                                                           typeof (DWebBrowserEvents2));
        }

        protected override void DetachSink()
        {
            if (null != _cookie)
            {
                _cookie.Disconnect();
                _cookie = null;
            }
            base.DetachSink();
        }

        //This new event will fire when the page is navigating
        public event System.EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNavigate;
        public event System.EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNewWindow;

        protected void OnBeforeNewWindow(string url, out bool cancel)
        {
            System.EventHandler<WebBrowserExtendedNavigatingEventArgs> h = BeforeNewWindow;
            var args = new WebBrowserExtendedNavigatingEventArgs(url, null);
            if (null != h)
            {
                h(this, args);
            }
            cancel = args.Cancel;
        }

        protected void OnBeforeNavigate(string url, string frame, out bool cancel)
        {
            System.EventHandler<WebBrowserExtendedNavigatingEventArgs> h = BeforeNavigate;
            var args = new WebBrowserExtendedNavigatingEventArgs(url, frame);
            if (null != h)
            {
                h(this, args);
            }
            //Pass the cancellation chosen back out to the events
            cancel = args.Cancel;
        }

        //This class will capture events from the WebBrowser

        [System.Runtime.InteropServices.ComImport,
         System.Runtime.InteropServices.Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
         System.Runtime.InteropServices.InterfaceType(
             System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch),
         System.Runtime.InteropServices.TypeLibType(System.Runtime.InteropServices.TypeLibTypeFlags.FHidden)]
        public interface DWebBrowserEvents2
        {
            [System.Runtime.InteropServices.DispId(250)]
            void BeforeNavigate2(
                [System.Runtime.InteropServices.In,
                 System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,
                [System.Runtime.InteropServices.In] ref object url,
                [System.Runtime.InteropServices.In] ref object flags,
                [System.Runtime.InteropServices.In] ref object targetFrameName,
                [System.Runtime.InteropServices.In] ref object postData,
                [System.Runtime.InteropServices.In] ref object headers,
                [System.Runtime.InteropServices.In,
                 System.Runtime.InteropServices.Out] ref bool cancel);

            [System.Runtime.InteropServices.DispId(273)]
            void NewWindow3(
                [System.Runtime.InteropServices.In,
                 System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,
                [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] ref bool cancel,
                [System.Runtime.InteropServices.In] ref object flags,
                [System.Runtime.InteropServices.In] ref object urlContext,
                [System.Runtime.InteropServices.In] ref object url);
        }

        private class WebBrowserExtendedEvents : System.Runtime.InteropServices.StandardOleMarshalObject,
                                                 DWebBrowserEvents2
        {
            private readonly EnBrowser _browser;

            public WebBrowserExtendedEvents(EnBrowser browser)
            {
                _browser = browser;
            }

            //Implement whichever events you wish
            public void BeforeNavigate2(object pDisp, ref object url, ref object flags, ref object targetFrameName,
                                        ref object postData, ref object headers, ref bool cancel)
            {
                _browser.OnBeforeNavigate((string) url, (string) targetFrameName, out cancel);
            }

            public void NewWindow3(object pDisp, ref object flags, ref object URLContext,
                                   ref object url)
            {
                var cancel = false;
                NewWindow3(pDisp, ref cancel, ref flags, ref URLContext, ref url);
            }

            public void NewWindow3(object pDisp, ref bool cancel, ref object flags, ref object urlContext,
                                   ref object url)
            {
                _browser.OnBeforeNewWindow((string) url, out cancel);
            }
        }
    }

    public class WebBrowserExtendedNavigatingEventArgs : System.ComponentModel.CancelEventArgs
    {
        private readonly string _frame;
        private readonly string _url;

        public WebBrowserExtendedNavigatingEventArgs(string url, string frame)
        {
            _url = url;
            _frame = frame;
        }

        public string Url
        {
            get { return _url; }
        }

        public string Frame
        {
            get { return _frame; }
        }
    }
}
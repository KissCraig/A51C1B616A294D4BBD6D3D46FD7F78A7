using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KCPlayer.Plugin.TestVod.Controls
{

    #region EFlyPal

    public class EFlyPal : FlowLayoutPanel
    {
        public EFlyPal()
        {
            BackColor = Color.Transparent;
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region ELabel

    public class ELabel : Label
    {
        public ELabel()
        {
            AutoSize = false;
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region EPanel

    public class EPanel : Panel
    {
        public EPanel()
        {
            BackColor = Color.Transparent;
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region EPicBox

    public class EPicBox : PictureBox
    {
        public EPicBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region ETxtBox

    public class ETextBox : TextBox
    {
        public ETextBox()
        {
            ImeMode = ImeMode.NoControl;
            BorderStyle = BorderStyle.None;
        }
    }

    #endregion

    #region InputBox

    public class InputBoxWithDesc : ETextBox
    {
        public InputBoxWithDesc
            (
            Control parentpal,
            int lineint,
            int lineflo,
            string predesc,
            string preword,
            Font descfont,
            Size descsize,
            Font font,
            Size size,
            Point point,
            Color defrcolor,
            Color debkcolor,
            Color linecolor,
            Color linelight,
            Color backcolor,
            Color backlight,
            Color forecolor,
            Color forelight,
            AnchorStyles anchorstyle
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
                ContentAlignment.MiddleCenter,
                anchorstyle
                );

            var inputPal = new LPanel
                (
                parentpal,
                lineint,
                size,
                new Point(inputdesc.Location.X + inputdesc.Width, inputdesc.Location.Y),
                Linecolor,
                Backcolor,
                anchorstyle
                );
            PreWord = preword;
            Text = PreWord;
            Multiline = true;
            Size = new Size(inputPal.Size.Width - 8*lineint, inputPal.Size.Height - 4*lineint);
            Location = new Point(4*lineint, (4 + lineflo)*lineint);
            BackColor = Backcolor;
            ForeColor = Forecolor;
            Font = font;
            Anchor = anchorstyle;

            Leave += LInput_Leave;
            Enter += LInput_Enter;
            GotFocus += InputBoxWithDesc_GotFocus;
            LostFocus += InputBoxWithDesc_LostFocus;
            inputPal.Controls.Add(this);
        }

        private Color Linecolor { get; set; }
        private Color Linelight { get; set; }
        private Color Backcolor { get; set; }
        private Color Backlight { get; set; }
        private Color Forecolor { get; set; }
        private Color Forelight { get; set; }
        private string PreWord { get; set; }

        private void InputBoxWithDesc_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((InputBoxWithDesc) sender).Text))
            {
                ((InputBoxWithDesc) sender).Text = PreWord;
            }
        }

        private void InputBoxWithDesc_GotFocus(object sender, EventArgs e)
        {
            if (((InputBoxWithDesc) sender).Text == PreWord)
            {
                ((InputBoxWithDesc) sender).Text = string.Empty;
            }
        }

        private void LInput_Enter(object sender, EventArgs e)
        {
            EnterOrLeave((InputBoxWithDesc) sender, true);
        }

        private void LInput_Leave(object sender, EventArgs e)
        {
            EnterOrLeave((InputBoxWithDesc) sender, false);
        }

        private void EnterOrLeave(Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.Parent.BackColor = ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }

    #endregion

    #region InputBox

    public class InputSearch : ETextBox
    {
        public InputSearch
            (
            Control parentpal,
            int lineint,
            int lineflo,
            string predesc,
            string preword,
            Font descfont,
            Size descsize,
            Font font,
            Size size,
            Point point,
            Color defrcolor,
            Color debkcolor,
            Color linecolor,
            Color linelight,
            Color backcolor,
            Color backlight,
            Color forecolor,
            Color forelight,
            MouseEventHandler searchEnter,
            KeyEventHandler searchEnterKey,
            AnchorStyles anchorstyle
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
            inputPal.Controls.Add(this);
            var inputdesc = new LButton
                (
                inputPal,
                0,
                predesc,
                descfont,
                descsize,
                new Point(size.Width - descsize.Width, -1),
                debkcolor,
                debkcolor,
                debkcolor,
                Color.FromArgb(51, debkcolor),
                defrcolor,
                defrcolor,
                AnchorStyles.Right | AnchorStyles.Top |
                AnchorStyles.Bottom
                );

            inputdesc.Focus();
            inputdesc.MouseClick += searchEnter;
            PreWord = preword;
            Text = preword;
            Multiline = false;
            Size = new Size(inputPal.Size.Width - 8*lineint - descsize.Width,
                            inputPal.Size.Height - 4*lineint);
            Location = new Point(4*lineint, (4 + lineflo)*lineint);
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
        }

        private Color Linecolor { get; set; }
        private Color Linelight { get; set; }
        private Color Backcolor { get; set; }
        private Color Backlight { get; set; }
        private Color Forecolor { get; set; }
        private Color Forelight { get; set; }
        private string PreWord { get; set; }

        private void InputBoxWithDesc_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((InputSearch) sender).Text))
            {
                ((InputSearch) sender).Text = PreWord;
            }
        }

        private void InputBoxWithDesc_GotFocus(object sender, EventArgs e)
        {
            if (((InputSearch) sender).Text == PreWord)
            {
                ((InputSearch) sender).Text = string.Empty;
            }
        }

        private static void FPanel_DragEnter(object sender, DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy | DragDropEffects.None;
            else
                e.Effect = DragDropEffects.None;

            #endregion
        }

        private void LInput_Enter(object sender, EventArgs e)
        {
            EnterOrLeave((InputSearch) sender, true);
        }

        private void LInput_Leave(object sender, EventArgs e)
        {
            EnterOrLeave((InputSearch) sender, false);
        }

        private void EnterOrLeave(Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.Parent.BackColor = ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }

    #endregion

    public class HDarge : ELabel
    {
        public HDarge
            (
            Control parentpal,
            string predesc,
            Font font,
            Size size,
            Point point,
            Color forecolor,
            Color backcolor,
            ContentAlignment alignment,
            AnchorStyles anchorstyle
            )
        {
            Text = predesc;
            Font = font;
            Size = size;
            Location = point;
            ForeColor = forecolor;
            TextAlign = alignment;
            BackColor = backcolor;
            Anchor = anchorstyle;

            AllowDrop = true;
            DragEnter += HLaple_DragEnter;
            MouseDown += HLaple_MouseDown;

            parentpal.Controls.Add(this);
        }

        private void HLaple_MouseDown(object sender, MouseEventArgs e)
        {
            ((Control) sender).Capture = false;
            Message msg = Message.Create(MainInterFace.Owner.Parent.Handle, 0x00A1,
                                         (IntPtr) 0x002,
                                         IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HLaple_DragEnter(object sender, DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy | DragDropEffects.None;
            else
                e.Effect = DragDropEffects.None;

            #endregion
        }
    }

    public class HLable : ELabel
    {
        public HLable()
        {
            BackColor = Color.Transparent;
            AllowDrop = true;
            DragEnter += HLable_DragEnter;
        }

        private static void HLable_DragEnter(object sender, DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy | DragDropEffects.None;
            else
                e.Effect = DragDropEffects.None;

            #endregion
        }
    }

    public class LButton : HLable
    {
        public LButton
            (
            Control parentpal,
            int lineint,
            string text,
            Font font,
            Size size,
            Point point,
            Color linecolor,
            Color linelight,
            Color backcolor,
            Color backlight,
            Color forecolor,
            Color forelight,
            AnchorStyles anchorstyle
            )
        {
            Linecolor = linecolor;
            Linelight = linelight;
            Backcolor = backcolor;
            Backlight = backlight;
            Forecolor = forecolor;
            Forelight = forelight;


            var panelSec = new HPanel
                {
                    Size = size,
                    Location = point,
                    Anchor = anchorstyle,
                    BackColor = linecolor,
                };

            Text = text;
            Font = font;
            TextAlign = ContentAlignment.MiddleCenter;
            Size = new Size(panelSec.Size.Width - 2*lineint, panelSec.Size.Height - 2*lineint);
            Location = new Point(lineint, lineint);
            ForeColor = Forecolor;
            BackColor = Backcolor;
            Anchor = AnchorStyles.Top;
            Cursor = Cursors.Hand;

            panelSec.Controls.Add(this);
            parentpal.Controls.Add(panelSec);

            MouseUp += LButton_MouseUp;
            MouseDown += LButton_MouseDown;
            MouseHover += LButton_MouseHover;
            MouseLeave += LButton_MouseLeave;
        }

        private Color Linecolor { get; set; }
        private Color Linelight { get; set; }
        private Color Backcolor { get; set; }
        private Color Backlight { get; set; }
        private Color Forecolor { get; set; }
        private Color Forelight { get; set; }

        private void LButton_MouseUp(object sender, MouseEventArgs e)
        {
            EnterOrLeave((LButton) sender, false);
        }

        private void LButton_MouseDown(object sender, MouseEventArgs e)
        {
            EnterOrLeave((LButton) sender, true);
        }

        private void LButton_MouseLeave(object sender, EventArgs e)
        {
            // EnterOrLeave((LButton)sender, false);
        }

        private void LButton_MouseHover(object sender, EventArgs e)
        {
            //EnterOrLeave((LButton)sender, true);
        }

        private void EnterOrLeave(Control ser, bool isEnter)
        {
            ser.ForeColor = isEnter ? Forelight : Forecolor;
            ser.BackColor = isEnter ? Backlight : Backcolor;
            ser.Parent.BackColor = isEnter ? Linelight : Linecolor;
        }
    }

    public class HPanel : EPanel
    {
        public HPanel()
        {
            AllowDrop = true;
            DragEnter += HPanel_DragEnter;
            MouseDown += HPanel_MouseDown;
        }

        private void HPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ((Control) sender).Capture = false;
            Message msg = Message.Create(MainInterFace.Owner.Parent.Handle, 0x00A1,
                                         (IntPtr) 0x002,
                                         IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HPanel_DragEnter(object sender, DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy | DragDropEffects.None;
            else
                e.Effect = DragDropEffects.None;

            #endregion
        }
    }

    public class HLaple : ELabel
    {
        public HLaple()
        {
            BackColor = Color.Transparent;
            AllowDrop = true;
            DragEnter += HLaple_DragEnter;
            MouseDown += HLaple_MouseDown;
        }

        private void HLaple_MouseDown(object sender, MouseEventArgs e)
        {
            ((Control) sender).Capture = false;
            Message msg = Message.Create(MainInterFace.Owner.Parent.Handle, 0x00A1,
                                         (IntPtr) 0x002,
                                         IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HLaple_DragEnter(object sender, DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy | DragDropEffects.None;
            else
                e.Effect = DragDropEffects.None;

            #endregion
        }
    }

    public class LPanel : HLaple
    {
        public LPanel
            (
            Control parentpal,
            int lineint,
            Size size,
            Point point,
            Color linecolor,
            Color backcolor,
            AnchorStyles anchorstyle
            )
        {
            var panelSec = new HPanel
                {
                    Size = size,
                    Location = point,
                    Anchor = anchorstyle,
                    BackColor = linecolor,
                };
            Size = new Size(panelSec.Size.Width - 2*lineint, panelSec.Size.Height - 2*lineint);
            Location = new Point(lineint, lineint);
            BackColor = backcolor;
            Anchor = AnchorStyles.Top | AnchorStyles.Left |
                     AnchorStyles.Right | AnchorStyles.Bottom;

            panelSec.Controls.Add(this);
            parentpal.Controls.Add(panelSec);
        }
    }

    public class EnBrowser : WebBrowser
    {
        private AxHost.ConnectionPointCookie cookie;
        private WebBrowserExtendedEvents events;

        //This method will be called to give you a chance to create your own event sink
        protected override void CreateSink()
        {
            //MAKE SURE TO CALL THE BASE or the normal events won't fire
            base.CreateSink();
            events = new WebBrowserExtendedEvents(this);
            cookie = new AxHost.ConnectionPointCookie(ActiveXInstance, events, typeof (DWebBrowserEvents2));
        }

        protected override void DetachSink()
        {
            if (null != cookie)
            {
                cookie.Disconnect();
                cookie = null;
            }
            base.DetachSink();
        }

        //This new event will fire when the page is navigating
        public event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNavigate;
        public event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNewWindow;

        protected void OnBeforeNewWindow(string url, out bool cancel)
        {
            EventHandler<WebBrowserExtendedNavigatingEventArgs> h = BeforeNewWindow;
            var args = new WebBrowserExtendedNavigatingEventArgs(url, null);
            if (null != h)
            {
                h(this, args);
            }
            cancel = args.Cancel;
        }

        protected void OnBeforeNavigate(string url, string frame, out bool cancel)
        {
            EventHandler<WebBrowserExtendedNavigatingEventArgs> h = BeforeNavigate;
            var args = new WebBrowserExtendedNavigatingEventArgs(url, frame);
            if (null != h)
            {
                h(this, args);
            }
            //Pass the cancellation chosen back out to the events
            cancel = args.Cancel;
        }

        //This class will capture events from the WebBrowser

        [ComImport, Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
         InterfaceType(ComInterfaceType.InterfaceIsIDispatch),
         TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DWebBrowserEvents2
        {
            [DispId(250)]
            void BeforeNavigate2(
                [In,
                 MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                [In] ref object URL,
                [In] ref object flags,
                [In] ref object targetFrameName, [In] ref object postData,
                [In] ref object headers,
                [In,
                 Out] ref bool cancel);

            [DispId(273)]
            void NewWindow3(
                [In,
                 MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                [In, Out] ref bool cancel,
                [In] ref object flags,
                [In] ref object URLContext,
                [In] ref object URL);
        }

        private class WebBrowserExtendedEvents : StandardOleMarshalObject, DWebBrowserEvents2
        {
            private readonly EnBrowser _Browser;

            public WebBrowserExtendedEvents(EnBrowser browser)
            {
                _Browser = browser;
            }

            //Implement whichever events you wish
            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName,
                                        ref object postData, ref object headers, ref bool cancel)
            {
                _Browser.OnBeforeNavigate((string) URL, (string) targetFrameName, out cancel);
            }

            public void NewWindow3(object pDisp, ref bool cancel, ref object flags, ref object URLContext,
                                   ref object URL)
            {
                _Browser.OnBeforeNewWindow((string) URL, out cancel);
            }
        }
    }

    public class WebBrowserExtendedNavigatingEventArgs : CancelEventArgs
    {
        private readonly string _Frame;
        private readonly string _Url;

        public WebBrowserExtendedNavigatingEventArgs(string url, string frame)
        {
            _Url = url;
            _Frame = frame;
        }

        public string Url
        {
            get { return _Url; }
        }

        public string Frame
        {
            get { return _Frame; }
        }
    }
}
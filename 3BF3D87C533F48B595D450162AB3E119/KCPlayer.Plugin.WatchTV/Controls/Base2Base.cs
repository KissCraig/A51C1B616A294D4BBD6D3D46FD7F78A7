using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KCPlayer.Plugin.WatchTV.Controls
{

    #region EFlyPal

    public class EFlyPal : System.Windows.Forms.FlowLayoutPanel
    {
        public EFlyPal()
        {
            BackColor = System.Drawing.Color.Transparent;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region ELabel

    public class ELabel : System.Windows.Forms.Label
    {
        public ELabel()
        {
            AutoSize = false;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region EPanel

    public class EPanel : System.Windows.Forms.Panel
    {
        public EPanel()
        {
            BackColor = System.Drawing.Color.Transparent;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region EPicBox

    public class EPicBox : System.Windows.Forms.PictureBox
    {
        public EPicBox()
        {
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }

    #endregion

    #region ETxtBox

    public class ETextBox : System.Windows.Forms.TextBox
    {
        public ETextBox()
        {
            ImeMode = System.Windows.Forms.ImeMode.NoControl;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
    }

    #endregion

    #region InputBox

    public class InputBoxWithDesc : ETextBox
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
                System.Drawing.ContentAlignment.MiddleCenter,
                anchorstyle
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
            Text = PreWord;
            Multiline = true;
            Size = new System.Drawing.Size(inputPal.Size.Width - 8*lineint, inputPal.Size.Height - 4*lineint);
            Location = new System.Drawing.Point(4*lineint, (4 + lineflo)*lineint);
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

    #endregion

    #region InputBox

    public class InputSearch : ETextBox
    {
        private System.Drawing.Color Linecolor { get; set; }
        private System.Drawing.Color Linelight { get; set; }
        private System.Drawing.Color Backcolor { get; set; }
        private System.Drawing.Color Backlight { get; set; }
        private System.Drawing.Color Forecolor { get; set; }
        private System.Drawing.Color Forelight { get; set; }

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

            Text = preword;
            Multiline = false;
            Size = new System.Drawing.Size(inputPal.Size.Width - 8*lineint - descsize.Width,
                                           inputPal.Size.Height - 4*lineint);
            Location = new System.Drawing.Point(4*lineint, (4 + lineflo)*lineint);
            BackColor = Backcolor;
            ForeColor = Forecolor;
            Font = font;
            Anchor = anchorstyle;

            Leave += LInput_Leave;
            Enter += LInput_Enter;
            KeyDown += searchEnterKey;
            inputPal.Controls.Add(this);
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

    #endregion

    public class HDarge : ELabel
    {
        public HDarge
            (
            System.Windows.Forms.Control parentpal,
            string predesc,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color forecolor,
            System.Drawing.Color backcolor,
            System.Drawing.ContentAlignment alignment,
            System.Windows.Forms.AnchorStyles anchorstyle
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

        private void HLaple_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(MainInterFace.Owner.Parent.Handle, 0x00A1,
                                                          (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HLaple_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class HLable : ELabel
    {
        public HLable()
        {
            BackColor = System.Drawing.Color.Transparent;
            AllowDrop = true;
            DragEnter += HLable_DragEnter;
        }

        private static void HLable_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class LButton : HLable
    {
        private System.Drawing.Color Linecolor { get; set; }
        private System.Drawing.Color Linelight { get; set; }
        private System.Drawing.Color Backcolor { get; set; }
        private System.Drawing.Color Backlight { get; set; }
        private System.Drawing.Color Forecolor { get; set; }
        private System.Drawing.Color Forelight { get; set; }

        public LButton
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            string text,
            System.Drawing.Font font,
            System.Drawing.Size size,
            System.Drawing.Point point,
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


            var panelSec = new HPanel
                {
                    Size = size,
                    Location = point,
                    Anchor = anchorstyle,
                    BackColor = linecolor,
                };

            Text = text;
            Font = font;
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Size = new System.Drawing.Size(panelSec.Size.Width - 2*lineint, panelSec.Size.Height - 2*lineint);
            Location = new System.Drawing.Point(lineint, lineint);
            ForeColor = Forecolor;
            BackColor = Backcolor;
            Anchor = System.Windows.Forms.AnchorStyles.Top;
            Cursor = System.Windows.Forms.Cursors.Hand;

            panelSec.Controls.Add(this);
            parentpal.Controls.Add(panelSec);

            MouseUp += LButton_MouseUp;
            MouseDown += LButton_MouseDown;
            MouseHover += LButton_MouseHover;
            MouseLeave += LButton_MouseLeave;
        }

        private void LButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            EnterOrLeave((LButton) sender, false);
        }

        private void LButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            EnterOrLeave((LButton) sender, true);
        }

        private void LButton_MouseLeave(object sender, System.EventArgs e)
        {
            // EnterOrLeave((LButton)sender, false);
        }

        private void LButton_MouseHover(object sender, System.EventArgs e)
        {
            //EnterOrLeave((LButton)sender, true);
        }

        private void EnterOrLeave(System.Windows.Forms.Control ser, bool isEnter)
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

        private void HPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(MainInterFace.Owner.Parent.Handle, 0x00A1,
                                                          (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HPanel_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class HLaple : ELabel
    {
        public HLaple()
        {
            BackColor = System.Drawing.Color.Transparent;
            AllowDrop = true;
            DragEnter += HLaple_DragEnter;
            MouseDown += HLaple_MouseDown;
        }

        private void HLaple_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((System.Windows.Forms.Control) sender).Capture = false;
            var msg = System.Windows.Forms.Message.Create(MainInterFace.Owner.Parent.Handle, 0x00A1,
                                                          (System.IntPtr) 0x002,
                                                          System.IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private static void HLaple_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            #region DragEnter

            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.None;
            else
                e.Effect = System.Windows.Forms.DragDropEffects.None;

            #endregion
        }
    }

    public class LPanel : HLaple
    {
        public LPanel
            (
            System.Windows.Forms.Control parentpal,
            int lineint,
            System.Drawing.Size size,
            System.Drawing.Point point,
            System.Drawing.Color linecolor,
            System.Drawing.Color backcolor,
            System.Windows.Forms.AnchorStyles anchorstyle
            )
        {
            var panelSec = new HPanel
                {
                    Size = size,
                    Location = point,
                    Anchor = anchorstyle,
                    BackColor = linecolor,
                };
            Size = new System.Drawing.Size(panelSec.Size.Width - 2*lineint, panelSec.Size.Height - 2*lineint);
            Location = new System.Drawing.Point(lineint, lineint);
            BackColor = backcolor;
            Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left |
                     System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;

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
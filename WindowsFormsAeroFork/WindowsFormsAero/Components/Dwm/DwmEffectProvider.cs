using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("code")]
    [ProvideProperty("FormBorderExtent", typeof(Form))]
    [ProvideProperty("IsSheetOfGlass", typeof(Form))]
    [ProvideProperty("UseStandardFrame", typeof(Form))]
    public class DwmEffectProvider : Component, IExtenderProvider
    {
        private static readonly object EventColorizationChanged = new object();
        private static readonly object EventCompositionChanged = new object();
        private static readonly object EventFrameHitTest = new object();

        private DwmForm _form;

        public event EventHandler ColorizationChanged
        {
            add { Events.AddHandler(EventColorizationChanged, value); }
            remove { Events.RemoveHandler(EventColorizationChanged, value); }
        }

        public event EventHandler CompositionChanged
        {
            add { Events.AddHandler(EventCompositionChanged, value); }
            remove { Events.RemoveHandler(EventCompositionChanged, value); }
        }

        public event EventHandler<FrameHitTestEventArgs> FrameHitTest
        {
            add { Events.AddHandler(EventFrameHitTest, value); }
            remove { Events.RemoveHandler(EventFrameHitTest, value); }
        }

        public DwmEffectProvider()
        {
        }

        public DwmEffectProvider(IContainer container)
        {
            container.Add(this);
        }

        #region FormBorderExtent

        public Padding GetFormBorderExtent(Form form)
        {
            return GetDwmForm(form).FrameExtent;
        }

        public void SetFormBorderExtent(Form form, Padding padding)
        {
            GetDwmForm(form).FrameExtent = padding;
        }

        private bool ShouldSerializeFormBorderExtent(Form form)
        {
            if (GetIsSheetOfGlass(form))
            {
                return false;
            }

            return GetDwmForm(form).FrameExtent != Padding.Empty;
        }

        private void ResetFormBorderExtent(Form form)
        {
            SetFormBorderExtent(form, Padding.Empty);
        }

        #endregion

        #region IsSheetOfGlass

        [DefaultValue(false)]
        public Boolean GetIsSheetOfGlass(Form form)
        {
            var padding = GetFormBorderExtent(form);

            return
               padding.Bottom == -1 &&
               padding.Left == -1 &&
               padding.Right == -1 &&
               padding.Top == -1;
        }

        public void SetIsSheetOfGlass(Form form, Boolean value)
        {
            if (value)
            {
                SetFormBorderExtent(form, new Padding(-1, -1, -1, -1));
            }
            else
            {
                SetFormBorderExtent(form, Padding.Empty);
            }
        }

        #endregion

        #region UseStandardFrame

        [DefaultValue(true)]
        public bool GetUseStandardFrame(Form form)
        {
            return !GetDwmForm(form).CustomFrame;
        }

        public void SetUseStandardFrame(Form form, Boolean value)
        {
            GetDwmForm(form).CustomFrame = !value;
        }

        #endregion

        private DwmForm GetDwmForm(Form form)
        {
            if (_form == null)
            {
                _form = new DwmForm(this, form);
            }
            else if (_form.Form != form)
            {
                throw new InvalidOperationException(Resources.Strings.DwmEffectProviderOnlyOneForm);
            }

            return _form;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((_form != null) && (_form.Filter != null))
                {
                    _form.Filter.Dispose();
                }
            }

            _form = null;

            base.Dispose(disposing);
        }

        protected virtual void OnColorizationChanged(EventArgs e)
        {
            var handler = Events[EventColorizationChanged] as EventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnCompositionChanged(EventArgs e)
        {
            var handler = Events[EventCompositionChanged] as EventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnFrameHitTest(FrameHitTestEventArgs e)
        {
            var handler = Events[EventFrameHitTest] as EventHandler<FrameHitTestEventArgs>;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public bool CanExtend(object extendee)
        {
            return extendee is Form;
        }

        private sealed class DwmForm
        {
            public readonly Form Form;
            public readonly DwmMessageFilter Filter;

            public Padding FrameExtent;
            public Boolean CustomFrame;

            public DwmForm(DwmEffectProvider owner, Form form)
            {
                Form = form;

                if ((form.Site == null) || !(form.Site.DesignMode))
                {
                    Filter = new DwmMessageFilter(owner, this);
                }
            }
        }

        private sealed class DwmMessageFilter : MessageFilter
        {
            private readonly DwmEffectProvider _owner;
            private readonly DwmForm _form;

            public DwmMessageFilter(DwmEffectProvider owner, DwmForm form) 
                : base(form.Form)
            {
                _owner = owner;
                _form = form;
            }

            protected override void OnHandleCreated()
            {
                if (_form.CustomFrame)
                {
                    var pt = _form.Form.Location;
                    var size = _form.Form.Size;

                    if (!NativeMethods.SetWindowPos(
                        GetHandleRef(),
                        IntPtr.Zero,
                        pt.X, pt.Y,
                        size.Width, size.Height,
                        SetWindowPosFlags.FrameChanged))
                    {
                        throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                    }
                }

                base.OnHandleCreated();
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WindowMessages.WM_DWMCOMPOSITIONCHANGED)
                {
                    if (DesktopComposition.IsEnabled)
                    {
                        ExtendFrameIntoClientArea();
                    }

                    _owner.OnCompositionChanged(EventArgs.Empty);
                }

                if (m.Msg == WindowMessages.WM_DWMCOLORIZATIONCOLORCHANGED)
                {
                    _owner.OnColorizationChanged(EventArgs.Empty);
                }

                if (DesktopComposition.IsEnabled)
                {
                    if (m.Msg == WindowMessages.WM_ACTIVATE)
                    {
                        //
                        // MSDN recommends to do this on WM_ACTIVATE to correctly support
                        // maximized windows.
                        // http://msdn2.microsoft.com/library/bb688195.aspx
                        //

                        ExtendFrameIntoClientArea();
                    }

                    if (_form.CustomFrame)
                    {
                        if ((m.Msg == WindowMessages.WM_NCCALCSIZE) && (m.WParam.ToInt32() == 1))
                        {
                            //
                            // tell DWM we'll draw our own frame
                            //

                            m.Result = IntPtr.Zero;
                            return;
                        }
                        else if (m.Msg == WindowMessages.WM_NCHITTEST)
                        {
                            IntPtr result;

                            if (!NativeMethods.DwmDefWindowProc(GetHandleRef(), m.Msg, m.WParam, m.LParam, out result))
                            {
                                //
                                // we must do the hit testing ourselves
                                //

                                var pt = new Point(m.LParam.ToInt32());
                                var e = new FrameHitTestEventArgs(_form.Form, pt);

                                _owner.OnFrameHitTest(e);

                                if (!e.IsAssigned)
                                {
                                    e.Result = DefaultHitTest(_form, pt);
                                }

                                result = new IntPtr((Int32)(e.Result));
                            }

                            m.Result = result;
                            return;
                        }
                    }
                }

                base.WndProc(ref m);
            }

            private void ExtendFrameIntoClientArea()
            {
                if (!(_form.FrameExtent.Size.IsEmpty))
                {
                    var margins = MARGINS.FromPadding(_form.FrameExtent);

                    NativeMethods.DwmExtendFrameIntoClientArea(GetHandleRef(), margins);
                }
            }

            private static FrameHitTestResult DefaultHitTest(DwmForm form, Point point)
            {
                var window = form.Form.Bounds;
                var frame = new RECT();

                if (!NativeMethods.AdjustWindowRectEx(
                    ref frame,
                    WindowStyles.WS_OVERLAPPEDWINDOW & ~WindowStyles.WS_CAPTION,
                    false,
                    0))
                {
                    throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                }

                int row = 1;
                int col = 1;
                bool resize = false;

                if (point.Y >= window.Top && point.Y < window.Top + form.FrameExtent.Top)
                {
                    row = 0;
                    resize = point.Y < (window.Top - frame.Top);
                }
                else if (point.Y < window.Bottom && point.Y >= window.Bottom - form.FrameExtent.Bottom)
                {
                    row = 2;
                }

                if (point.X >= window.Left && point.X < window.Left + form.FrameExtent.Left)
                {
                    col = 0;
                }
                else if (point.X < window.Right && point.X >= window.Right - form.FrameExtent.Right)
                {
                    col = 2;
                }

                var results = new FrameHitTestResult[][]
                {
                    new FrameHitTestResult[] 
                    {
                        FrameHitTestResult.TopLeft, 
                        (resize ? FrameHitTestResult.Top : FrameHitTestResult.Caption), 
                        FrameHitTestResult.TopRight 
                    },
                    new FrameHitTestResult[]
                    { 
                        FrameHitTestResult.Left, 
                        FrameHitTestResult.Nowhere, 
                        FrameHitTestResult.Right 
                    },
                    new FrameHitTestResult[] 
                    {
                        FrameHitTestResult.BottomLeft, 
                        FrameHitTestResult.Bottom, 
                        FrameHitTestResult.BottomRight 
                    }
                };

                return results[row][col];
            }
        }
    }
}

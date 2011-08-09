using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WindowsFormsAero
{
    public class AeroToolTip : NativeWindow
    {
        private readonly Form _ownerForm;
        private readonly Control _owner;

        private String _text;
        private String _title;
        private ToolTipIcon _icon;

        private Point? _ctlLocation;

        private Boolean _visible;
        private Point _location;
        private Int32 _maxWidth;

        public AeroToolTip(Control owner)
        {
            _owner = owner;
            _ownerForm = owner.FindForm();
        }

        public override void DestroyHandle()
        {
            this.DetachEvents();
            base.DestroyHandle();
        }

        public Point Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;

                    if (Handle != IntPtr.Zero)
                    {
                        UpdateLocation();
                    }
                }
            }
        }

        public Point? ControlLocation
        {
            get { return _ctlLocation; }
            set
            {
                if (_ctlLocation != value)
                {
                    _ctlLocation = value;
                    OnUpdateLocation(this, EventArgs.Empty);
                }
            } 
        }

        public String Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;

                    if (Handle != IntPtr.Zero)
                    {
                        UpdateTitleIcon();
                    }
                }
            }
        }

        public String Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;

                    if (Handle != IntPtr.Zero)
                    {
                        NativeMethods.SendMessage(new HandleRef(this, Handle), 
                            WindowMessages.TTM_UPDATETIPTEXT, IntPtr.Zero, ToolInfo);
                    }
                }
            }
        }

        public Boolean Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;

                    if (Handle == IntPtr.Zero)
                    {
                        CreateHandle();
                    }

                    NativeMethods.SendMessage(
                        new HandleRef(this, Handle),
                        WindowMessages.TTM_TRACKACTIVATE,
                        value ? new IntPtr(1) : IntPtr.Zero,
                        ToolInfo);
                }
            }
        }

        public Int32 MaxWidth
        {
            get { return _maxWidth; }
            set
            {
                if (_maxWidth != value)
                {
                    _maxWidth = value;

                    if (Handle != IntPtr.Zero)
                    {
                        UpdateMaxWidth();
                    }
                }
            }
        }

        public ToolTipIcon Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        private void CreateHandle()
        {
            CreateHandle(new CreateParams()
            {
                ClassName = "tooltips_class32",
                Style = WindowStyles.WS_POPUP |
                        WindowStyles.TTS_NOPREFIX |
                        WindowStyles.TTS_ALWAYSTIP |
                        WindowStyles.TTS_BALLOON |
                        WindowStyles.TTS_CLOSE,
                Parent = _owner.Handle,
            });

            NativeMethods.SendMessage(new HandleRef(this, Handle),
                WindowMessages.TTM_ADDTOOLW, IntPtr.Zero, ToolInfo);

            UpdateLocation();
            UpdateMaxWidth();
            UpdateTitleIcon();

            AttachEvents();
        }

        private void AttachEvents()
        {
            _ownerForm.ResizeBegin += OnUpdateLocation;
            _ownerForm.ResizeEnd += OnUpdateLocation;
            _ownerForm.Resize += OnUpdateLocation;
            _ownerForm.Move += OnUpdateLocation;

            _ownerForm.VisibleChanged += OnUpdateLocation;

            _owner.GotFocus += OnUpdateLocation;
            _owner.LostFocus += OnUpdateLocation;
        }

        private void DetachEvents()
        {
            _ownerForm.ResizeBegin -= OnUpdateLocation;
            _ownerForm.ResizeEnd -= OnUpdateLocation;
            _ownerForm.Resize -= OnUpdateLocation;
            _ownerForm.Move -= OnUpdateLocation;

            _ownerForm.VisibleChanged -= OnUpdateLocation;

            _owner.GotFocus -= OnUpdateLocation;
            _owner.LostFocus -= OnUpdateLocation;
        }

        private void UpdateMaxWidth()
        {
            NativeMethods.SendMessage(
                new HandleRef(this, Handle),
                WindowMessages.TTM_SETMAXTIPWIDTH,
                IntPtr.Zero, new IntPtr(_maxWidth > 0 ? _maxWidth : -1));
        }

        private void UpdateTitleIcon()
        {
            var lpszTitle = Marshal.StringToHGlobalUni(_title);

            try
            {
                NativeMethods.SendMessage(new HandleRef(this, Handle),
                    WindowMessages.TTM_SETTITLE, new IntPtr((Int32)(_icon)), lpszTitle);
            }
            finally
            {
                Marshal.FreeHGlobal(lpszTitle);
            }
        }

        private void UpdateLocation()
        {
            NativeMethods.SendMessage(new HandleRef(this, Handle),
                WindowMessages.TTM_TRACKPOSITION, IntPtr.Zero, MakeInt32(_location));
        }

        private void OnUpdateLocation(object sender, EventArgs e)
        {
            Visible =
                _ownerForm.Visible &&
                (_ownerForm.WindowState != FormWindowState.Minimized) &&
                (_owner.Focused || NativeMethods.GetForegroundWindow() == Handle);

            if (_ctlLocation.HasValue)
            {
                Location = _owner.PointToScreen(_ctlLocation.Value);
            }
        }

        private TOOLINFO ToolInfo
        {
            get
            {
                return new TOOLINFO()
                {
                    Flags = ToolTipFlags.Track | ToolTipFlags.IdIsHwnd,
                    Id = _owner.Handle,
                    WindowHandle = _owner.Handle,
                    Text = _text,
                };
            }
        }


        private static IntPtr MakeInt32(Point pt)
        {
            return MakeInt32(pt.X, pt.Y);
        }

        private static IntPtr MakeInt32(int low, int high)
        {
            return new IntPtr((low & 0xffff) | ((high & 0xffff) << 16));
        }
    }
}

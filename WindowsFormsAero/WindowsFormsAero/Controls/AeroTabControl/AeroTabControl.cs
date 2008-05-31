using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("code")]
    [System.ComponentModel.Designer("WindowsFormsAero.Design.AeroTabControlDesigner, " + ThisAssembly.DesignAssemblyFullName)]
    [DefaultProperty("TabPages")]
    [DefaultEvent("SelectedTabChanged")]
    [Docking(DockingBehavior.AutoDock)]
    public partial class AeroTabControl : ContainerControl
    {
        private static readonly object EventCloseButtonClick = new object();
        private static readonly object EventNewTabButtonClick = new object();
        private static readonly object EventSelectedTabChanged = new object();
        private static readonly object EventSelectedTabChanging = new object();

        private readonly AeroTabControlCloseButton _closeButton;
        private readonly AeroTabControlNewTabButton _newTabButton;
        private readonly AeroTabControlTabListButton _tabListButton;

        private readonly List<AeroTabPage> _pages = new List<AeroTabPage>();
        private readonly TabStrip _tabStrip = new TabStrip()
        {
            Dock = DockStyle.Top,
        };

        private TabPageCollection _pageCollection;
        private AeroTabPage _selectedPage;
        private Boolean _hideSingleTab;

        public AeroTabControl()
        {
            EnableCtrlNumbers = true;
            EnableCtrlTab = true;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint | 
                ControlStyles.Opaque, true);

            SetStyle(
                ControlStyles.Selectable |
                ControlStyles.StandardClick |
                ControlStyles.StandardDoubleClick |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserMouse, false);

            _pageCollection = new TabPageCollection(this);
            
            _closeButton = new AeroTabControlCloseButton(_tabStrip);
            _newTabButton = new AeroTabControlNewTabButton(_tabStrip);
            _tabListButton = new AeroTabControlTabListButton(_tabStrip);

            _tabStrip.NewTabButtonClicked += InvokeNewTabButtonClicked;
            _tabStrip.CloseButtonClicked += InvokeCloseButtonClicked;
            _tabStrip.SelectedTabChanged += InvokeSelectedTabChanged;
            _tabStrip.SelectedTabChanging += InvokeSelectedTabChanging;
        }

        public event EventHandler<AeroTabPageEventArgs> CloseButtonClick
        {
            add { Events.AddHandler(EventCloseButtonClick, value); }
            remove { Events.RemoveHandler(EventCloseButtonClick, value); }
        }

        public event EventHandler NewTabButtonClick
        {
            add { Events.AddHandler(EventNewTabButtonClick, value); }
            remove { Events.RemoveHandler(EventNewTabButtonClick, value); }
        }

        public event EventHandler SelectedTabChanged
        {
            add { Events.AddHandler(EventSelectedTabChanged, value); }
            remove { Events.RemoveHandler(EventSelectedTabChanged, value); }
        }

        public event CancelEventHandler SelectedTabChanging
        {
            add { Events.AddHandler(EventSelectedTabChanging, value); }
            remove { Events.RemoveHandler(EventSelectedTabChanging, value); }
        }

        [Browsable(true)]
        [DefaultValue(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool EnableCtrlNumbers
        {
            get;
            set;
        }

        [Browsable(true)]
        [DefaultValue(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool EnableCtrlTab
        {
            get;
            set;
        }

        [Browsable(true)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool HideSingleTab
        {
            get { return _hideSingleTab; }
            set
            {
                if (_hideSingleTab != value)
                {
                    _hideSingleTab = value;
                    UpdateTabStripVisibility();
                }
            }
        }

        [Browsable(true)]
        [MergableProperty(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AeroTabControlCloseButton CloseButton
        {
            get { return _closeButton; }
        }

        [Browsable(true)]
        [MergableProperty(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AeroTabControlNewTabButton NewTabButton
        {
            get { return _newTabButton; }
        }

        [Browsable(true)]
        [MergableProperty(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AeroTabControlTabListButton TabListButton
        {
            get { return _tabListButton; }
        }
        
        [Browsable(false)]
        [MergableProperty(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedTabIndex
        {
            get { return _tabStrip.SelectedTabIndex; }
            set { _tabStrip.SelectedTabIndex = value; }
        }

        [Browsable(true)]
        [MergableProperty(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter("WindowsFormsAero.Design.AeroTabControlSelectedPageConverter, " + ThisAssembly.DesignAssemblyFullName)]
        [Editor("WindowsFormsAero.Design.AeroTabControlSelectedPageEditor, " + ThisAssembly.DesignAssemblyFullName, typeof(UITypeEditor))]
        public AeroTabPage SelectedTab
        {
            get { return _selectedPage; }
            set
            {
                if (_selectedPage != value)
                {
                    if (value != null)
                    {
                        _tabStrip.SelectedTab = value.TabStripButton;
                    }
                    else
                    {
                        _tabStrip.SelectedTab = null;
                    }
                }
            }
        }

        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabPageCollection TabPages
        {
            get 
            {
                if (_pageCollection == null)
                {
                    _pageCollection = new TabPageCollection(this);
                }

                return _pageCollection;
            }
        }

        [Browsable(false)]
        [MergableProperty(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabStrip TabStrip
        {
            get { return _tabStrip; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public void PerformNewTabButtonClick()
        {
            OnNewTabButtonClick(EventArgs.Empty);
        }

        public void PerformCloseButtonClick(AeroTabPage page)
        {
            OnCloseButtonClick(new AeroTabPageEventArgs(page));
        }

        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlCollection(this);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Controls.Add(_tabStrip);
        }

        protected virtual void OnCloseButtonClick(AeroTabPageEventArgs e)
        {
            var handler = Events[EventCloseButtonClick] as EventHandler<AeroTabPageEventArgs>;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNewTabButtonClick(EventArgs e)
        {
            var handler = Events[EventNewTabButtonClick] as EventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSelectedTabChanged(EventArgs e)
        {
            var handler = Events[EventSelectedTabChanged] as EventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSelectedTabChanging(CancelEventArgs e)
        {
            var handler = Events[EventSelectedTabChanging] as CancelEventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_pages.Count > 1)
            {
                if ((EnableCtrlNumbers) && ((keyData & Keys.Control) == Keys.Control))
                {
                    if (ProcessCtrlNumber(keyData))
                    {
                        return true;
                    }
                }
            }

            if (keyData == NewTabButton.ShortcutKeys)
            {
                _tabStrip.PerformNewTabButtonClick();
                return true;
            }

            if (SelectedTab != null)
            {
                const Keys ControlF4 = Keys.Control | Keys.F4;

                if ((keyData == CloseButton.ShortcutKeys) ||
                    ((keyData == ControlF4) && CloseButton.EnableCtrlF4))
                {
                    _tabStrip.PerformCloseButtonClick(SelectedTab.TabStripButton);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (EnableCtrlTab && TabPages.Count > 1)
            {
                if ((keyData & Keys.Control) == Keys.Control)
                {
                    if ((keyData & Keys.KeyCode) == Keys.Tab)
                    {
                        int newIndex = SelectedTabIndex + 1;

                        if ((keyData & Keys.Shift) == Keys.Shift)
                        {
                            newIndex = SelectedTabIndex - 1;
                        }

                        if (newIndex < 0)
                        {
                            newIndex = TabPages.Count - 1;
                        }
                        else if (newIndex >= TabPages.Count)
                        {
                            newIndex = 0;
                        }

                        SelectedTabIndex = newIndex;
                        return true;
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override Size DefaultSize
        {
            get { return new Size(150, 150); }
        }

        protected override Padding DefaultPadding
        {
            get { return new Padding(2); }
        }

        internal Color TabPageBackColor
        {
            get { return TabStrip.SelectedTabBottomColor; }
        }

        private void Add(AeroTabPage page)
        {
            bool suspendPaint = (_hideSingleTab && (_pages.Count == 1));

            if (suspendPaint)
            {
                SetRedraw(this, false);
                SetRedraw(_tabStrip, false);
            }

            try
            {
                page.Visible = false;
                page.Dock = DockStyle.Fill;

                _pages.Add(page);

                _tabStrip.SuspendLayout();
                _tabStrip.Items.Add(page.TabStripButton);
                _tabStrip.ResumeLayout();

                UpdateTabStripVisibility();
            }
            finally
            {
                if (suspendPaint)
                {
                    SetRedraw(_tabStrip, true);
                    SetRedraw(this, true);

                    Invalidate(true);
                }
            }
        }

        private void AddRange(IList<AeroTabPage> pages)
        {
            //BeginUpdate();

            //try
            //{
                var buttons = new TabStripButton[pages.Count];

                for (int i = 0; i < pages.Count; ++i)
                {
                    pages[i].Visible = false;
                    pages[i].Dock = DockStyle.Fill;
                    buttons[i] = pages[i].TabStripButton;
                }

                _pages.AddRange(pages);

                _tabStrip.SuspendLayout();
                _tabStrip.Items.AddRange(buttons);
                _tabStrip.ResumeLayout();

                UpdateTabStripVisibility();
            //}
            //finally
            //{
            //    EndUpdate();
            //}
        }

        private void Remove(AeroTabPage page)
        {
            //BeginUpdate();

            //try
            //{
                if (_tabStrip.Items.Contains(page.TabStripButton))
                {
                    _tabStrip.SuspendLayout();
                    _tabStrip.Items.Remove(page.TabStripButton);
                    _tabStrip.ResumeLayout();
                }

                if (_pages.Contains(page))
                {
                    _pages.Remove(page);
                }

                UpdateTabStripVisibility();
            //}
            //finally
            //{
            //    EndUpdate();
            //}
        }

        private void RemoveAllTabs()
        {
            //BeginUpdate();

            //try
            //{
                SuspendLayout();
                _tabStrip.SuspendLayout();

                SelectedTab = null;

                _tabStrip.RemoveAllTabs();
                _pages.Clear();

                _tabStrip.ResumeLayout();
                UpdateTabStripVisibility();
                ResumeLayout();
            //}
            //finally
            //{
            //    EndUpdate();
            //}
        }

        private void InvokeNewTabButtonClicked(object sender, EventArgs e)
        {
            OnNewTabButtonClick(e);
        }

        private void InvokeCloseButtonClicked(object sender, ToolStripItemEventArgs e)
        {
            var page = AeroTabPage.GetButtonPage(e.Item);

            if (page != null)
            {
                PerformCloseButtonClick(page);
            }
        }

        private void InvokeSelectedTabChanged(object sender, EventArgs e)
        {
            SetRedraw(this, false);

            try
            {
                if (_selectedPage != null)
                {
                    _selectedPage.Visible = false;
                }

                _selectedPage = AeroTabPage.GetButtonPage(_tabStrip.SelectedTab);

                if (_selectedPage != null)
                {
                    _selectedPage.Visible = true;

                    _tabStrip.SendToBack();
                    _tabStrip.SelectedTab = _selectedPage.TabStripButton;

                    _selectedPage.Focus();
                    _selectedPage.SelectNextControl(_selectedPage, true, true, true, false);
                }
            }
            finally
            {
                SetRedraw(this, true);
                Invalidate(true);
            }

            OnSelectedTabChanged(e);
        }

        private void InvokeSelectedTabChanging(object sender, CancelEventArgs e)
        {
            OnSelectedTabChanging(e);
        }

        private bool ProcessCtrlNumber(Keys keyData)
        {
            int? newTab = null;
            var keyCode = keyData & Keys.KeyCode;

            if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
            {
                newTab = (keyCode - Keys.D0);
            }

            if (keyCode >= Keys.NumPad0 && keyCode <= Keys.NumPad9)
            {
                newTab = (keyCode - Keys.NumPad0);
            }

            if (newTab.HasValue)
            {
                if (newTab.Value == 9)
                {
                    SelectedTabIndex = _pages.Count - 1;
                    return true;
                }
                else if (newTab.Value - 1 < _pages.Count)
                {
                    SelectedTabIndex = newTab.Value - 1;
                    return true;
                }
            }

            return false;
        }

        private void UpdateTabStripVisibility()
        {
            if (_hideSingleTab)
            {
                _tabStrip.Visible = _pages.Count > 1;
            }
            else
            {
                _tabStrip.Visible = true;
            }
        }

        private static void SetRedraw(Control control, Boolean redraw)
        {
            NativeMethods.SendMessage(
                new HandleRef(control, control.Handle),
                WindowMessages.WM_SETREDRAW,
                redraw ? new IntPtr(1) : IntPtr.Zero,
                IntPtr.Zero);
        }
    }
}

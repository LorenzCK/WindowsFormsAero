//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("code")]
    [System.Drawing.ToolboxBitmap(typeof(Resources.Images), "TabStripToolboxBitmap.bmp")]
    public partial class TabStrip : ToolStrip
    {
        private const int DefaultMinTabWidth = 63;
        private const int DefaultMaxTabWidth = 180;

        private static readonly object EventNewTabButtonClicked = new object();
        private static readonly object EventCloseButtonClicked = new object();
        private static readonly object EventSelectedTabChanged = new object();

        private readonly TabStripTabListButton _list =
            new TabStripTabListButton();

        private readonly TabStripNewTabButton _newTab =
            new TabStripNewTabButton();

        private readonly TabStripScrollButton _scrollLeft =
            new TabStripScrollButton(TabStripScrollDirection.Left);

        private readonly TabStripScrollButton _scrollRight =
            new TabStripScrollButton(TabStripScrollDirection.Right);

        private Int32 _tabCount;
        private Int32 _selectedIndex = -1;
        private TabStripButton _selectedTab;

        private TabStripLayoutEngine _layout;
        //private ToolTip _closeToolTip;

        private Boolean _clearingTabs;
        private UInt16 _busyTabCount;
        private UInt16 _busyTabTicker;
        private Timer _busyTabTimer;

        private int _minTabWidth = DefaultMinTabWidth;
        private int _maxTabWidth = DefaultMaxTabWidth;

        private CloseButtonVisibility _closeButtonVisibility = CloseButtonVisibility.ExceptSingleTab;

        public TabStrip()
        {
            Items.Add(_list);
            Items.Add(_newTab);
        }

        public event EventHandler NewTabButtonClicked
        {
            add { Events.AddHandler(EventNewTabButtonClicked, value); }
            remove { Events.RemoveHandler(EventNewTabButtonClicked, value); }
        }

        public event ToolStripItemEventHandler CloseButtonClicked
        {
            add { Events.AddHandler(EventCloseButtonClicked, value); }
            remove { Events.RemoveHandler(EventCloseButtonClicked, value); }
        }

        public event EventHandler SelectedTabChanged
        {
            add { Events.AddHandler(EventSelectedTabChanged, value); }
            remove { Events.RemoveHandler(EventSelectedTabChanged, value); }
        }

        [DefaultValue(null)]
        public TabStripButton SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab != value)
                {
                    _selectedIndex = -1;
                    _selectedTab = value;

                    int index = 0;

                    foreach(var tab in ItemsOfType<TabStripButton>())
                    {
                        if (tab == _selectedTab)
                        {
                            _selectedIndex = index;
                        }

                        tab.Checked = (tab == _selectedTab);
                        ++index;
                    }

                    PerformLayout();
                    OnSelectedTabChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedTabIndex
        {
            get { return _selectedIndex; }
            set 
            {
                if (value == -1)
                {
                    SelectedTab = null;
                }
                else
                {
                    int index = 0;

                    foreach (var item in ItemsOfType<TabStripButton>())
                    {
                        if (index == value)
                        {
                            SelectedTab = item;
                            return;
                        }

                        ++index;
                    }

                    throw new IndexOutOfRangeException(
                        string.Format(System.Globalization.CultureInfo.CurrentCulture,
                        Resources.Strings.TabStripInvalidTabIndex, value));
                }
            }
        }

        [DefaultValue(DefaultMaxTabWidth)]
        public int MaximumTabWidth
        {
            get { return _maxTabWidth; }
            set
            {
                if (_maxTabWidth != value)
                {
                    _maxTabWidth = value;
                    PerformLayout();
                }
            }
        }

        [DefaultValue(DefaultMinTabWidth)]
        public int MinimumTabWidth
        {
            get { return _minTabWidth; }
            set
            {
                if (_minTabWidth != value)
                {
                    _minTabWidth = value;
                    PerformLayout();
                }
            }
        }

        [DefaultValue(true)]
        public bool NewTabButtonVisible
        {
            get { return _newTab.Visible; }
            set { _newTab.Visible = value; }
        }

        [DefaultValue(true)]
        public bool TabListButtonVisible
        {
            get { return _list.Visible; }
            set { _list.Visible = value; }
        }

        [DefaultValue(CloseButtonVisibility.ExceptSingleTab)]
        public CloseButtonVisibility CloseButtonVisibility
        {
            get { return _closeButtonVisibility; }
            set
            {
                if (_closeButtonVisibility != value)
                {
                    _closeButtonVisibility = value;
                    PerformLayout();
                }
            }
        }

        [Browsable(false)]
        public override LayoutEngine LayoutEngine
        {
            get 
            {
                if (_layout == null)
                {
                    _layout = new TabStripLayoutEngine(this);
                }

                return _layout; 
            }
        }

        protected override Padding DefaultPadding
        {
            get { return new Padding(0, 2, 2, 4); }
        }

        public void PerformCloseButtonClick(TabStripButton button)
        {
            if (button.IsClosableInternal)
            {
                OnCloseButtonClicked(new ToolStripItemEventArgs(button));
            }
        }

        public void PerformNewTabButtonClick()
        {
            if (NewTabButtonVisible)
            {
                OnNewTabButtonClicked(EventArgs.Empty);
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize = Size.Empty;
            proposedSize -= Padding.Size;

            foreach (ToolStripItem item in Items)
            {
                preferredSize = LayoutUtils.UnionSizes(preferredSize, item.GetPreferredSize(proposedSize) + item.Padding.Size);
            }

            return preferredSize + Padding.Size;
        }

        protected virtual void OnNewTabButtonClicked(EventArgs e)
        {
            var handler = Events[EventNewTabButtonClicked] as EventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnCloseButtonClicked(ToolStripItemEventArgs e)
        {
            var handler = Events[EventCloseButtonClicked] as ToolStripItemEventHandler;

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

        protected override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
        {
            return new TabStripButton(text, image, onClick);
        }

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            if (_clearingTabs)
            {
                base.OnItemAdded(e);
                return;
            }

            if (e.Item is TabStripButton)
            {
                _layout.ScrollDirection = TabStripScrollDirection.Right;
                ++TabCount;
            }

            SuspendLayout();

            base.OnItemAdded(e);
            
            ResumeLayout();
        }

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == _scrollLeft)
            {
                _layout.ScrollDirection = TabStripScrollDirection.Left;
                PerformLayout();
                return;
            }

            if(e.ClickedItem == _scrollRight)
            {
                _layout.ScrollDirection = TabStripScrollDirection.Right;
                PerformLayout();
                return;
            }

            if (e.ClickedItem == _newTab)
            {
                PerformNewTabButtonClick();
                return;
            }

            base.OnItemClicked(e);
        }

        protected override void OnItemRemoved(ToolStripItemEventArgs e)
        {
            if (_clearingTabs)
            {
                base.OnItemAdded(e);
                return;
            }

            if (e.Item is TabStripButton)
            {
                --TabCount;
            }

            if ((e.Item == SelectedTab))
            {
                int newIndex = SelectedTabIndex;

                if (newIndex >= TabCount)
                {
                    newIndex = TabCount - 1;
                }

                SelectedTabIndex = newIndex;
            }

            base.OnItemRemoved(e);
        }

        protected override void OnRendererChanged(EventArgs e)
        {
            base.OnRendererChanged(e);
            UpdateBusyTabTimer();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_busyTabTimer != null)
                {
                    _busyTabTimer.Dispose();
                }

                //if (_closeToolTip != null)
                //{
                //    _closeToolTip.Dispose();
                //}
            }

            _busyTabCount = 0;
            _busyTabTimer = null;
            //_closeToolTip = null;

            base.Dispose(disposing);
        }

        internal int TabCount
        {
            get { return _tabCount; }
            private set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException();
                }

                _tabCount = value;
                _list.Visible = value > 1;
            }
        }

        internal ushort BusyTabCount
        {
            get { return _busyTabCount; }
            set
            {
                _busyTabCount = Math.Max((ushort)(0), value);

                UpdateBusyTabTimer();
            }
        }

        internal int BusyTabTicker
        {
            get { return _busyTabTicker; }
        }

        internal TabStripScrollButton ScrollNearButton
        {
            get { return _scrollLeft; }
        }

        internal TabStripScrollButton ScrollFarButton
        {
            get { return _scrollRight; }
        }

        internal TabStripButtonBase NewTabButton
        {
            get { return _newTab; }
        }

        internal Color SelectedTabBottomColor
        {
            get
            {
                if (TabStripRenderer != null)
                {
                    return TabStripRenderer.SelectedTabBottomColor;
                }

                return SystemColors.Control;
            }
        }

        internal IEnumerable<T> ItemsOfType<T>() where T : ToolStripItem
        {
            foreach (ToolStripItem item in Items)
            {
                T t = (item as T);

                if (t != null)
                {
                    yield return t;
                }
            }
        }

        internal void LayoutItem(ToolStripItem item, Point location, Size size)
        {
            SetItemLocation(item, location);
            item.Size = size;
        }

        internal void RemoveAllTabs()
        {
            SuspendLayout();

            try
            {
                _clearingTabs = true;

                Items.Clear();
                Items.Add(_newTab);
            }
            finally
            {
                _clearingTabs = false;
                _tabCount = 0;
            }

            ResumeLayout();
        }

        private TabStripRenderer TabStripRenderer
        {
            get { return Renderer as TabStripRenderer; }
        }

        private void UpdateBusyTabTimer()
        {
            if (_busyTabCount > 0)
            {
                if (_busyTabTimer == null && TabStripRenderer != null)
                {
                    var interval = TabStripRenderer.BusyTabRefreshInterval.TotalMilliseconds;

                    if (interval > 0)
                    {
                        _busyTabTimer = new Timer()
                        {
                            Interval = (int)interval
                        };
                        _busyTabTimer.Tick += delegate
                        {
                            unchecked
                            {
                                ++_busyTabTicker;
                            }

                            Invalidate();
                        };
                    }
                }
            }

            if (_busyTabTimer != null)
            {
                _busyTabTimer.Enabled = (_busyTabCount > 0);
            }
        }
    }
}
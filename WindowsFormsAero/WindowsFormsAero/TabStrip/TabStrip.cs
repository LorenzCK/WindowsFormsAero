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
    [DesignerCategory("code")]
    [ToolboxItem(typeof(TabStripToolboxItem))]
    public partial class TabStrip : ToolStrip
    {
        private const int DefaultMinTabWidth = 63;
        private const int DefaultMaxTabWidth = 180;

        private readonly TabStripNewTabButton _newTab =
            new TabStripNewTabButton();

        private readonly TabStripScrollButton _scrollLeft =
            new TabStripScrollButton(TabStripScrollDirection.Left);

        private readonly TabStripScrollButton _scrollRight =
            new TabStripScrollButton(TabStripScrollDirection.Right);

        private TabStripButton _selectedTab;
        private TabStripLayoutEngine _layout;

        private Int32 _busyTabCount;
        private Timer _busyTabTimer;
        private Int32 _busyTabTicker;

        private int _minTabWidth = DefaultMinTabWidth;
        private int _maxTabWidth = DefaultMaxTabWidth;

        public TabStrip()
        {
            Items.Add(_newTab);
        }

        public TabStripButton SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab != value)
                {
                    _selectedTab = value;

                    foreach(var tab in ItemsOfType<TabStripButton>())
                    {
                        tab.Checked = (tab == _selectedTab);
                    }
                    
                    if (_selectedTab != null)
                    {
                        if (_selectedTab.TabStripPage != null)
                        {
                            _selectedTab.TabStripPage.Activate();
                        }

                        PerformLayout();
                    }
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

        protected override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
        {
            return new TabStripButton(text, image, onClick);
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
                MessageBox.Show("New TAAAAB!");
                return;
            }

            var button = (e.ClickedItem as TabStripButton);

            if (button != null)
            {
                SelectedTab = button;
            }

            base.OnItemClicked(e);
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
            }

            _busyTabCount = 0;
            _busyTabTimer = null;

            base.Dispose(disposing);
        }

        internal int BusyTabCount
        {
            get { return _busyTabCount; }
            set
            {
                _busyTabCount = Math.Max(0, value);

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
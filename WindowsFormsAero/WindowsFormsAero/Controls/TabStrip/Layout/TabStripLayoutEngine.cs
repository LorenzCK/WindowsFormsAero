//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace WindowsFormsAero
{
    internal sealed partial class TabStripLayoutEngine : LayoutEngine
    {
        private TabStrip _strip;
        private TabStripScrollDirection _direction;

        private int? _availableWidth;
        private int _nearTab;
        private int _farTab;

        public TabStripLayoutEngine(TabStrip strip)
        {
            _strip = strip;
        }

        private bool RTL
        {
            get { return _strip.RightToLeft == RightToLeft.Yes; }
        }

        private Rectangle DisplayRectangle
        {
            get { return _strip.DisplayRectangle; }
        }
        
        private int AvailableWidth
        {
            get
            {
                if (_availableWidth == null)
                {
                    _availableWidth = _strip.DisplayRectangle.Width;

                    if (ScrollNearButtonVisible)
                    {
                        _availableWidth -= ScrollNearButton.Width;
                    }

                    if (ScrollFarButtonVisible)
                    {
                        _availableWidth -= ScrollFarButton.Width;
                    }

                    if (NewTabButtonVisible)
                    {
                        _availableWidth -= NewTabButton.Width;
                    }

                    foreach (var item in GetAvailableUnknownItems())
                    {
                        _availableWidth -= item.Width;    
                    }

                    if (_availableWidth < MinimumTabWidth)
                    {
                        _availableWidth = MinimumTabWidth;
                    }
                }

                return _availableWidth.Value;
            }
        }

        private int MaximumTabWidth
        {
            get { return _strip.MaximumTabWidth; }
        }

        private int MinimumTabWidth
        {
            get { return _strip.MinimumTabWidth; }
        }

        private int GetSelectedTabIndex()
        {
            int tabIndex = 0;

            foreach (var tab in GetTabItems())
            {
                if (tab == SelectedTab)
                {
                    break;
                }

                ++tabIndex;
            }

            return tabIndex;
        }

        private void SetSelectedTabIndex(int value)
        {
            int tabIndex = 0;

            foreach (var tab in GetTabItems())
            {
                if (tabIndex == value)
                {
                    SelectedTab = tab;
                    break;
                }

                ++tabIndex;
            }
        }
        
        private TabStripButton SelectedTab
        {
            get { return _strip.SelectedTab; }
            set { _strip.SelectedTab = value; }
        }

        private TabStripButtonBase NewTabButton
        {
            get { return _strip.NewTabButton; }
        }

        private TabStripScrollButton ScrollNearButton
        {
            get { return _strip.ScrollNearButton; }
        }

        private TabStripScrollButton ScrollFarButton
        {
            get { return _strip.ScrollFarButton; }
        }

        private bool NewTabButtonVisible
        {
            get { return GetButtonVisibility(NewTabButton); }
            set { SetButtonVisibility(NewTabButton, value); }
        }

        private bool ScrollNearButtonVisible
        {
            get { return GetButtonVisibility(ScrollNearButton); }
            set { SetButtonVisibility(ScrollNearButton, value); }
        }

        private bool ScrollFarButtonVisible
        {
            get { return GetButtonVisibility(ScrollFarButton); }
            set { SetButtonVisibility(ScrollFarButton, value); }
        }

        private int TabOverlap
        {
            get { return 1; }
        }

        private void LayoutItem(ToolStripItem item, Point location, Size size)
        {
            _strip.LayoutItem(item, location, size);
        }

        private int GetTabItemCount()
        {
            int count = 0;

            foreach (var item in GetTabItems())
            {
                ++count;
            }

            return count;
        }

        private IEnumerable<TabStripButton> GetTabItems()
        {
            return _strip.ItemsOfType<TabStripButton>();
        }

        private IEnumerable<ToolStripItem> GetAvailableUnknownItems()
        {
            foreach (ToolStripItem item in _strip.Items)
            {
                if (!(item is TabStripButton) &&
                      item != ScrollNearButton &&
                      item != ScrollFarButton &&
                      item != NewTabButton &&
                      item.Available)
                {
                    yield return item;
                }
            }
        }

        private bool GetButtonVisibility(ToolStripItem button)
        {
            return _strip.Items.Contains(button);
        }

        private void SetButtonVisibility(ToolStripItem button, bool value)
        {
            if (GetButtonVisibility(button) != value)
            {
                _availableWidth = null;

                if (value)
                {
                    _strip.Items.Insert(0, button);
                }
                else
                {
                    _strip.Items.Remove(button);
                }
            }
        }

        internal void ClearVolatileState()
        {
            _availableWidth = null;
        }

        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            var tabStrip = (container as TabStrip);

            if (tabStrip != _strip)
            {
                throw new ArgumentException();
            }

            using (var transaction = new TabStripLayoutPass(this))
            {
                transaction.DoLayout();
            }

            return tabStrip.AutoSize;
        }

        public TabStripScrollDirection ScrollDirection
        {
            set { _direction = value; }
        }
    }
}

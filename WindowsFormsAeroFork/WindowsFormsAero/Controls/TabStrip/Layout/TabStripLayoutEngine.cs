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
        internal const int TabOverlap = 1;

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

                    if (ContainsScrollNearButton)
                    {
                        _availableWidth -= ScrollNearButton.Width;
                    }

                    if (ContainsScrollFarButton)
                    {
                        _availableWidth -= ScrollFarButton.Width;
                    }

                    if (ContainsNewTabButton)
                    {
                        _availableWidth -= NewTabButton.Width;
                    }

                    if (ContainsTabListButton)
                    {
                        _availableWidth -= TabListButton.Width;
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

        private int TabCount
        {
            get { return _strip.TabCount; }
        }

        private int SelectedTabIndex
        {
            get { return _strip.SelectedTabIndex; }
            set { _strip.SelectedTabIndex = value; }
        }
        
        private TabStripButtonBase TabListButton
        {
            get { return _strip.TabListButton; }
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

        private bool ContainsNewTabButton
        {
            get { return GetButtonVisibility(NewTabButton); }
        }

        private bool ContainsScrollNearButton
        {
            get { return GetButtonVisibility(ScrollNearButton); }
            set { SetButtonVisibility(ScrollNearButton, value); }
        }

        private bool ContainsScrollFarButton
        {
            get { return GetButtonVisibility(ScrollFarButton); }
            set { SetButtonVisibility(ScrollFarButton, value); }
        }

        private bool ContainsTabListButton
        {
            get { return GetButtonVisibility(TabListButton); }
            set { SetButtonVisibility(TabListButton, value); }
        }

        private void LayoutItem(ToolStripItem item, Point location, Size size)
        {
            _strip.LayoutItem(item, location, size);
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
                      item != TabListButton &&
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
            get { return _direction; }
            set { _direction = value; }
        }
    }
}

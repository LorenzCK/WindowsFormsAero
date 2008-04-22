using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    partial class TabStrip
    {
        internal sealed class TabStripTabDragger
        {
            private const double LeftRightRatio = 0.66;

            private readonly TabStrip _owner;
            private readonly Cursor _cursorTabDrag = new Cursor(typeof(Resources.Images), "TabDrag.cur");
            //private readonly Cursor _cursorTabDragNA = new Cursor(typeof(Resources.Images), "TabDragNotAllowed.cur");

            private Point? _lastMouseDown;

            private Int32 _draggedTabIndex;
            private TabStripButton _draggedTab;

            public TabStripTabDragger(TabStrip owner) 
            {
                _owner = owner;
            }

            private TabStripButton GetTabAt(Point pt)
            {
                return _owner.GetItemAt(pt) as TabStripButton;
            }

            public void WmCaptureChanged()
            {
                EndDrag();
            }

            public void WmLButtonDown(Point pt)
            {
                if (GetTabAt(pt) != null)
                {
                    _lastMouseDown = pt;
                }
            }

            public void WmLButtonUp(Point pt)
            {
                var dropTabIndex = GetDropTabIndex(pt);

                if ((_draggedTab != null) && (_draggedTabIndex != dropTabIndex) && (dropTabIndex != -1))
                {
                    _owner.SuspendLayout();

                    if (dropTabIndex > _draggedTabIndex)
                    {
                        --dropTabIndex;
                    }

                    _owner.Items.Remove(_draggedTab);
                    _owner.Items.Insert(dropTabIndex, _draggedTab);

                    _owner.SetSelectedTab(_draggedTab, true);
                    _owner.ResumeLayout();
                }

                EndDrag();
            }

            public void WmMouseMove(IntPtr wParam, Point pt)
            {
                const int MK_LBUTTON = 0x1;

                if ((wParam.ToInt32() & MK_LBUTTON) == 0)
                {
                    EndDrag();
                    return;
                }

                var mouseOverTab = GetTabAt(pt);

                if (mouseOverTab != null)
                {
                    if (_lastMouseDown.HasValue)
                    {
                        int dx = Math.Abs(_lastMouseDown.Value.X - pt.X);
                        int dy = Math.Abs(_lastMouseDown.Value.Y - pt.Y);

                        if ((dx > SystemInformation.DragSize.Width) ||
                            (dy > SystemInformation.DragSize.Height))
                        {
                            _draggedTab = mouseOverTab;
                            _draggedTabIndex = _owner.Items.IndexOf(mouseOverTab);

                            _lastMouseDown = null;
                        }
                    }
                }

                if (_draggedTab != null)
                {
                    _owner.Capture = true;

                    if (mouseOverTab != null)
                    {
                        Cursor.Current = _cursorTabDrag;

                        var dropTabIndex = GetDropTabIndex(mouseOverTab, pt);

                        if (dropTabIndex == _owner.Items.Count)
                        {
                            if (_owner.RightToLeft == RightToLeft.Yes)
                            {
                                _owner.TabInsertionPoint = _owner.Items[dropTabIndex - 1].Bounds.Left;
                            }
                            else
                            {
                                _owner.TabInsertionPoint = _owner.Items[dropTabIndex - 1].Bounds.Right - _owner._layout.TabOverlap;
                            }
                        }
                        else
                        {
                            if (_owner.RightToLeft == RightToLeft.Yes)
                            {
                                _owner.TabInsertionPoint = _owner.Items[dropTabIndex].Bounds.Right - _owner._layout.TabOverlap;
                            }
                            else
                            {
                                _owner.TabInsertionPoint = _owner.Items[dropTabIndex].Bounds.Left;
                            }
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.No;
                        _owner.TabInsertionPoint = -1;
                    }
                }
            }

            private int GetDropTabIndex(Point pt)
            {
                return GetDropTabIndex(GetTabAt(pt), pt);
            }

            private int GetDropTabIndex(TabStripButton tabOver, Point pt)
            {
                if (tabOver == null)
                {
                    return -1;
                }

                var tabPt = GetPointRelativeTo(tabOver, pt);
                
                var tabOverIndex = _owner.Items.IndexOf(tabOver);
                var draggedTabIndex = _owner.Items.IndexOf(_draggedTab);

                bool isOnFarSide = false;

                if (_owner.RightToLeft == RightToLeft.Yes)
                {
                    isOnFarSide = (tabPt.X <= (tabOver.Width * (1 - LeftRightRatio)));
                }
                else
                {
                    isOnFarSide = (tabPt.X > (tabOver.Width * LeftRightRatio));
                }

                if (isOnFarSide && (_draggedTab != tabOver))
                {
                    return tabOverIndex + 1;
                }
                else if (tabOverIndex == draggedTabIndex + 1)
                {
                    return draggedTabIndex;
                }

                return tabOverIndex;
            }

            private Point GetPointRelativeTo(ToolStripItem item, Point pt)
            {
                return new Point(pt.X - item.Bounds.X, pt.Y - item.Bounds.Y);
            }

            private void EndDrag()
            {
                if (_draggedTab != null)
                {
                    _draggedTab = null;
                    _draggedTabIndex = -1;

                    _owner.TabInsertionPoint = -1;
                    _owner.Capture = false;

                    Cursor.Current = Cursors.Default;
                }
            }
        }
    }
}

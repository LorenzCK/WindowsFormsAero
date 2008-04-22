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
                bool farSide;
                var dropTab = GetDropTab(pt, out farSide);

                if ((_draggedTab != dropTab) && (_draggedTab != null) && (dropTab != null))
                {
                    var dropTabIndex = _owner.Items.IndexOf(dropTab);

                    if (!farSide || ((dropTabIndex + 1) != _owner.Items.IndexOf(_draggedTab)))
                    {
                        _owner.SuspendLayout();
                        _owner.Items.Remove(_draggedTab);

                        if (dropTabIndex > _owner.Items.Count)
                        {
                            _owner.Items.Add(_draggedTab);
                        }
                        else
                        {
                            _owner.Items.Insert(dropTabIndex, _draggedTab);
                        }

                        _owner.SetSelectedTab(_draggedTab, true);
                        _owner.ResumeLayout();
                    }
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
                            _lastMouseDown = null;

                            if (_draggedTab != null)
                            {
                                _owner.Capture = true;
                            }
                        }
                    }
                }

                if (_draggedTab != null)
                {
                    if (mouseOverTab != null)
                    {
                        Cursor.Current = _cursorTabDrag;

                        bool farSide;
                        var dropTab = GetDropTab(mouseOverTab, pt, out farSide);

                        _owner.TabInsertionPoint = farSide ?
                            dropTab.Bounds.Right - _owner._layout.TabOverlap :
                            dropTab.Bounds.Left;
                    }
                    else
                    {
                        Cursor.Current = Cursors.No;
                        _owner.TabInsertionPoint = -1;
                    }
                }
            }

            private TabStripButton GetDropTab(Point pt, out bool farSide)
            {
                return GetDropTab(GetTabAt(pt), pt, out farSide);
            }

            private TabStripButton GetDropTab(TabStripButton tabOver, Point pt, out bool farSide)
            {
                if (tabOver == null)
                {
                    farSide = false;
                    return null;
                }

                var tabPt = GetPointRelativeTo(tabOver, pt);

                if ((tabPt.X > (tabOver.Width * LeftRightRatio)) && (_draggedTab != tabOver))
                {
                    farSide = true;
                }
                else
                {
                    farSide = false;

                    if (_owner.Items.IndexOf(tabOver) == _owner.Items.IndexOf(_draggedTab) + 1)
                    {
                        return _draggedTab;
                    }
                }

                return tabOver;
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

                    _owner.TabInsertionPoint = -1;
                    _owner.Capture = false;

                    Cursor.Current = Cursors.Default;
                }
            }
        }
    }
}

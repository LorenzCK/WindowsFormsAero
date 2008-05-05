using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    partial class TabStripButton
    {
        public sealed class TabStripButtonAccessibleObject : ToolStripItemAccessibleObject
        {
            private readonly TabStripButton _owner;

            public TabStripButtonAccessibleObject(TabStripButton button)
                : base(button)
            {
                _owner = button;
            }

            public override AccessibleRole Role
            {
                get { return AccessibleRole.PageTab; }
            }

            public override int GetChildCount()
            {
                if (_owner.IsCloseButtonVisible)
                {
                    return 1;
                }

                return 0;
            }

            public override AccessibleObject GetChild(int index)
            {
                if (_owner.IsCloseButtonVisible && index == 0)
                {
                    return new CloseButtonAccessibleObject(_owner);
                }

                return base.GetChild(index);
            }

            private sealed class CloseButtonAccessibleObject : AccessibleObject
            {
                private readonly TabStripButton _owner;

                public CloseButtonAccessibleObject(TabStripButton owner)
                {
                    _owner = owner;
                }

                public override AccessibleRole Role
                {
                    get { return AccessibleRole.PushButton; }
                }

                public override void DoDefaultAction()
                {
                    _owner.PerformCloseButtonClick();
                }

                public override System.Drawing.Rectangle Bounds
                {
                    get 
                    { 
                        var rect = _owner.InternalLayout.CloseRectangle;
                        rect.Offset(_owner.Bounds.Location);

                        return _owner.Owner.RectangleToScreen(rect);
                    }
                }
            }
        }
    }
}

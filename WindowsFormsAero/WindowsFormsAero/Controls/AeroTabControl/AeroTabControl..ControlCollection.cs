using System;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    partial class AeroTabControl
    {
        public new class ControlCollection : Control.ControlCollection
        {
            public ControlCollection(AeroTabControl owner)
                : base(owner)
            {
                base.Add(owner._tabStrip);
            }

            public new AeroTabControl Owner
            {
                get { return base.Owner as AeroTabControl; }
            }

            public override void Add(Control value)
            {
                var page = (value as AeroTabPage);

                if (page == null)
                {
                    throw new ArgumentException(Resources.Strings.TabControlInvalidPageType);
                }

                Owner.AddTab(page);
                base.Add(page);
            }

            public override void Clear()
            {
                Owner.RemoveAllTabs();
            }

            public override void Remove(Control value)
            {
                var page = (value as AeroTabPage);

                if (page != null)
                {
                    Owner.RemoveTab(page);
                }

                base.Remove(value);
            }
        }
    }
}

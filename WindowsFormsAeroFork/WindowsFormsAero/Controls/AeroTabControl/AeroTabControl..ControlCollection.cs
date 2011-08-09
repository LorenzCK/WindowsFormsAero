using System;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    partial class AeroTabControl
    {
        private new class ControlCollection : Control.ControlCollection
        {
            public ControlCollection(AeroTabControl owner)
                : base(owner)
            {
            }

            public new AeroTabControl Owner
            {
                get { return base.Owner as AeroTabControl; }
            }

            public override void Add(Control value)
            {
                if (value == Owner._tabStrip)
                {
                    base.Add(value);
                }
                else
                {
                    var page = (value as AeroTabPage);

                    if (page == null)
                    {
                        throw new ArgumentException(Resources.Strings.TabControlInvalidPageType);
                    }

                    Owner.SuspendLayout();

                    base.Add(page);
                    Owner.Add(page);

                    Owner.ResumeLayout();
                }
            }

            public override void AddRange(Control[] controls)
            {
                foreach (var item in controls)
                {
                    if (!(item is AeroTabPage))
                    {
                        throw new ArgumentException(Resources.Strings.TabControlInvalidPageType);
                    }
                }

                var pages = new AeroTabPage[controls.Length];
                controls.CopyTo(pages, 0);

                Owner.SuspendLayout();
                
                base.AddRange(pages);
                Owner.AddRange(pages);

                Owner.ResumeLayout();
            }

            public override void Clear()
            {
                Owner.RemoveAllTabs();
            }

            public override void Remove(Control value)
            {
                var page = (value as AeroTabPage);

                Owner.SuspendLayout();

                if (page != null && Contains(value))
                {
                    Owner.Remove(page);
                }

                base.Remove(value);
                Owner.ResumeLayout();
            }
        }
    }
}

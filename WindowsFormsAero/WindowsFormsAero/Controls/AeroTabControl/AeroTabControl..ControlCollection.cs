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
                System.Diagnostics.Debug.WriteLine("ControlCollection.Add: " + value.Name);

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
                System.Diagnostics.Debug.WriteLine("ControlCollection.Clear");
                Owner.RemoveAllTabs();
            }

            public override void Remove(Control value)
            {
                System.Diagnostics.Debug.WriteLine("ControlCollection.Remove: " + value.Name);
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

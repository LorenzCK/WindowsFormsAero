using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace WindowsFormsAero
{
    [Docking(DockingBehavior.AutoDock)]
    [System.ComponentModel.DesignerCategory("code")]
    [System.ComponentModel.Designer("WindowsFormsAero.Design.AeroTabControlDesigner, " + ThisAssembly.DesignAssemblyName)]
    public class AeroTabControl : ContainerControl
    {
        public new class ControlCollection : Control.ControlCollection
        {
            public ControlCollection(AeroTabControl owner)
                : base(owner)
            {
                base.Add(owner.TabStrip);
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

                page.Visible = false;
                page.Dock = DockStyle.Fill;

                Owner.TabStrip.Items.Add(page.TabStripButton);

                base.Add(page);

            }

            public override void Remove(Control value)
            {
                var page = (value as AeroTabPage);

                if (page != null)
                {
                    Owner.TabStrip.Items.Remove(page.TabStripButton);
                }

                base.Remove(value);
            }
        }

        private static readonly object EventSelectedTabChanged = new object();

        private AeroTabPage _selectedPage;
        private TabStrip _tabStrip = new TabStrip()
        {
            Dock = DockStyle.Top,
            Renderer = new TabStripSystemRenderer(),
        };
        

        public event EventHandler SelectedTabChanged
        {
            add { Events.AddHandler(EventSelectedTabChanged, value); }
            remove { Events.RemoveHandler(EventSelectedTabChanged, value); }
        }

        public AeroTabPage SelectedTab
        {
            get { return _selectedPage; }
            set
            {
                if (_selectedPage != value)
                {
                    if (_selectedPage != null)
                    {
                        _selectedPage.Visible = false;
                    }

                    _selectedPage = value;

                    if (_selectedPage != null)
                    {
                        _selectedPage.Visible = true;
                        _selectedPage.BringToFront();
                        TabStrip.SelectedTab = _selectedPage.TabStripButton;
                    }
                }
            }
        }

        public TabStrip TabStrip
        {
            get { return _tabStrip; }
        }

        protected virtual void OnSelectedTabChanged(EventArgs e)
        {
            var handler = Events[EventSelectedTabChanged] as EventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlCollection(this);
        }

        protected override Size DefaultSize
        {
            get { return new Size(150, 150); }
        }

        protected override Padding DefaultPadding
        {
            get { return new Padding(2); }
        }
    }
}

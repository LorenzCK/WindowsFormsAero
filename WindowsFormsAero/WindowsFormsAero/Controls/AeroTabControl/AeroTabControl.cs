using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("code")]
    [System.ComponentModel.Designer("WindowsFormsAero.Design.AeroTabControlDesigner, " + ThisAssembly.DesignAssemblyName)]
    [DefaultProperty("TabPages")]
    [DefaultEvent("SelectedTabChanged")]
    [Docking(DockingBehavior.AutoDock)]
    public partial class AeroTabControl : ContainerControl
    {
        private static readonly object EventCloseButtonClick = new object();
        private static readonly object EventNewTabButtonClick = new object();
        private static readonly object EventSelectedTabChanged = new object();

        private readonly List<AeroTabPage> _pages = new List<AeroTabPage>();
        private readonly TabStrip _tabStrip = new TabStrip()
        {
            Dock = DockStyle.Top,
            Renderer = new TabStripSystemRenderer(),
        };

        private TabPageCollection _pageCollection;
        private AeroTabPage _selectedPage;

        public AeroTabControl()
        {
            _pageCollection = new TabPageCollection(this);
            _tabStrip.NewTabButtonClicked += new EventHandler(InvokeNewTabButtonClicked);
        }

        public event EventHandler<AeroTabPageEventArgs> CloseButtonClick
        {
            add { Events.AddHandler(EventCloseButtonClick, value); }
            remove { Events.RemoveHandler(EventCloseButtonClick, value); }
        }

        public event EventHandler NewTabButtonClick
        {
            add { Events.AddHandler(EventNewTabButtonClick, value); }
            remove { Events.RemoveHandler(EventNewTabButtonClick, value); }
        }

        public event EventHandler SelectedTabChanged
        {
            add { Events.AddHandler(EventSelectedTabChanged, value); }
            remove { Events.RemoveHandler(EventSelectedTabChanged, value); }
        }

        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedTabIndex
        {
            get { return _tabStrip.SelectedTabIndex; }
            set { _tabStrip.SelectedTabIndex = value; }
        }

        [MergableProperty(false)]
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
                        
                        _tabStrip.SelectedTab = _selectedPage.TabStripButton;
                    }

                    OnSelectedTabChanged(EventArgs.Empty);
                }
            }
        }

        //public TabStrip TabStrip
        //{
        //    get { return _tabStrip; }
        //}

        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //[Editor("WindowsFormsAero.Design.AeroTabPageCollectionEditor, " + ThisAssembly.DesignAssemblyName, typeof(UITypeEditor))]
        public TabPageCollection TabPages
        {
            get 
            {
                if (_pageCollection == null)
                {
                    _pageCollection = new TabPageCollection(this);
                }

                return _pageCollection;
            }
        }

        protected internal virtual void OnCloseButtonClick(AeroTabPageEventArgs e)
        {
            var handler = Events[EventCloseButtonClick] as EventHandler<AeroTabPageEventArgs>;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNewTabButtonClick(EventArgs e)
        {
            var handler = Events[EventNewTabButtonClick] as EventHandler;

            if (handler != null)
            {
                handler(this, e);
            }
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

        private void AddTab(AeroTabPage page)
        {
            System.Diagnostics.Debug.WriteLine("AddTab " + page.Name);

            SuspendLayout();

            page.Visible = false;
            page.Dock = DockStyle.Fill;

            _pages.Add(page);
            _tabStrip.Items.Add(page.TabStripButton);
            
            ResumeLayout();
        }

        private void RemoveTab(AeroTabPage page)
        {
            System.Diagnostics.Debug.WriteLine("RemoveTab " + page.Name);

            SuspendLayout();

            if (_tabStrip.Items.Contains(page.TabStripButton))
            {
                _tabStrip.Items.Remove(page.TabStripButton);
            }

            if (_pages.Contains(page))
            {
                _pages.Remove(page);
            }

            ResumeLayout();
        }

        private void RemoveAllTabs()
        {
            System.Diagnostics.Debug.WriteLine("RemoveAllTabs");

            SuspendLayout();

            _tabStrip.RemoveAllTabs();
            _pages.Clear();

            ResumeLayout();
        }

        private void InvokeNewTabButtonClicked(object sender, EventArgs e)
        {
            OnNewTabButtonClick(e);
        }
    }
}

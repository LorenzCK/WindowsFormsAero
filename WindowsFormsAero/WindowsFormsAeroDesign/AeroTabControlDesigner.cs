using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Drawing;

namespace WindowsFormsAero.Design
{
    internal sealed class AeroTabControlDesigner : ParentControlDesigner, IDesignerUtilsClient
    {
        private DesignerVerbCollection _verbs;
        private DesignerUtils _utils;

        private bool _tabControlSelected;

        public AeroTabControlDesigner()
        {
            _utils = new DesignerUtils(this);
        }

        public override bool CanParent(Control control)
        {
            return (control is AeroTabPage) && !(Control.Controls.Contains(control));
        }

        public override DesignerVerbCollection Verbs
        {
            get 
            {
                if (_verbs == null)
                {
                    _verbs = new DesignerVerbCollection()
                    {
                        new DesignerVerb(Resources.AddTabPage, OnAddTab),
                        new DesignerVerb(Resources.RemoveTabPage, OnRemoveTab),
                    };
                }
                return _verbs; 
            }
        }

        public AeroTabControl TabControl
        {
            get { return base.Control as AeroTabControl; }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            _utils.SelectionService.SelectionChanged += OnSelectionChanged;

            TabControl.NewTabButtonClick += OnAddTab;
            TabControl.CloseButtonClick += OnRemoveTab;
        }

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            AddTabPage();
            AddTabPage();

            _utils.SetPropertyValueWithNotification("SelectedTabIndex", 0);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TabControl.NewTabButtonClick -= OnAddTab;
                TabControl.CloseButtonClick -= OnRemoveTab;

                _utils.SelectionService.SelectionChanged -= OnSelectionChanged;
            }

            base.Dispose(disposing);
        }

        protected override bool GetHitTest(Point point)
        {
            if (_tabControlSelected)
            {
                //return TabControl.TabStrip.Bounds.Contains(TabControl.PointToClient(point));
                return true;
            }

            return false;
        }

        private AeroTabPage AddTabPage()
        {
            return AddTabPage(false);
        }

        private AeroTabPage AddTabPage(bool dontRaiseEvents)
        {
            return _utils.ExecuteWithTransaction(Resources.AddTabPage + ' ' + TabControl.Site.Name, delegate
            {
                var page = _utils.CreateComponent<AeroTabPage>();
                var pagesDescriptor = _utils.GetProperty("TabPages");

                if (dontRaiseEvents)
                {
                    TabControl.TabPages.Add(page);

                    _utils.SetPropertyValue("SelectedTab", page);
                    _utils.SetPropertyValue(page, "Text", page.Name);
                }
                else
                {
                    RaiseComponentChanging(pagesDescriptor);

                    TabControl.TabPages.Add(page);

                    _utils.SetPropertyValueWithNotification("SelectedTab", page);
                    _utils.SetPropertyValueWithNotification(page, "Text", page.Name);

                    RaiseComponentChanged(pagesDescriptor, null, null);
                }

                return page;
            });
        }

        private void RemoveTabPage(AeroTabPage page)
        {
            _utils.ExecuteWithTransaction(Resources.RemoveTabPage, delegate
            {
                var pagesDescriptor = _utils.GetProperty("TabPages");

                RaiseComponentChanging(pagesDescriptor);
                TabControl.TabPages.Remove(page);
                RaiseComponentChanged(pagesDescriptor, null, null);

                _utils.DesignerHost.DestroyComponent(page);
            });
        }

        private void OnAddTab(object sender, EventArgs e)
        {
            AddTabPage();
        }

        private void OnRemoveTab(object sender, EventArgs e)
        {
            RemoveTabPage(TabControl.SelectedTab);
        }

        private void OnRemoveTab(object sender, AeroTabPageEventArgs e)
        {
            RemoveTabPage(e.Page);
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            _tabControlSelected = false;

            foreach (var item in _utils.SelectionService.GetSelectedComponents())
            {
                if (item == TabControl)
                {
                    _tabControlSelected = true;
                }

                var page = GetTabPageOfComponent(item);

                if ((page != null) && (page.Parent == TabControl))
                {
                    _tabControlSelected = false;
                    _utils.SetPropertyValueWithNotification("SelectedTab", page);

                    break;
                }
            }
        }

        private static AeroTabPage GetTabPageOfComponent(object component)
        {
            Control ctl = (component as Control);

            while (ctl != null)
            {
                var page = (ctl as AeroTabPage);

                if (page != null)
                {
                    return page;
                }

                ctl = ctl.Parent;
            }
            
            return null;
        }

        #region IDesignerUtilsClient Members

        IComponent IDesignerUtilsClient.Component
        {
            get { return Component; }
        }

        object IDesignerUtilsClient.GetService(Type type)
        {
            return GetService(type);
        }

        #endregion
    }
}
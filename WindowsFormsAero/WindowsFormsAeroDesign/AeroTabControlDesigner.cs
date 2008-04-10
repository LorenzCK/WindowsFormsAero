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
    internal sealed class AeroTabControlDesigner : ParentControlDesigner
    {
        private DesignerVerbCollection _verbs;
        private ISelectionService _selection;

        private bool _tabControlSelected;

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
                        new DesignerVerb(Resources.AddTabPage, OnAdd)
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
            SelectionService.SelectionChanged += OnSelectionChanged;
        }

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            AddTabPage();
            AddTabPage();

            base.InitializeNewComponent(defaultValues);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SelectionService.SelectionChanged -= OnSelectionChanged;
            }

            base.Dispose(disposing);
        }

        protected override bool GetHitTest(Point point)
        {
            if (_tabControlSelected)
            {
                return TabControl.TabStrip.Bounds.Contains(TabControl.PointToClient(point));
            }

            return false;
        }

        private AeroTabPage AddTabPage()
        {
            var host = GetService<IDesignerHost>();

            if (host != null)
            {
                return AddTabPage(host);
            }

            return null;
        }

        private AeroTabPage AddTabPage(IDesignerHost host)
        {
            return AddTabPage(host, true);
        }

        private AeroTabPage AddTabPage(IDesignerHost host, bool raiseEvents)
        {
            var controls = GetProperty(TabControl, "Controls");
            var page = CreateComponent<AeroTabPage>(host);

            if (raiseEvents)
            {
                RaiseComponentChanging(controls);
            }

            TabControl.Controls.Add(page);

            SetProperty("SelectedTab", page);
            SetProperty(page, "Text", page.Name);

            if (raiseEvents)
            {
                RaiseComponentChanged(controls, null, null);
            }

            return page;
        }

        private void OnAdd(object sender, EventArgs e)
        {
            var host = GetService<IDesignerHost>();

            if (host != null)
            {
                using (var tx = CreateDesignerTransaction(host, Resources.AddTabPage + ' ' + Component.Site.Name))
                {
                    if (tx != null)
                    {
                        try
                        {
                            AddTabPage(host, true);
                        }
                        finally
                        {
                            tx.Commit();
                        }
                    }
                }
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            _tabControlSelected = false;

            foreach (var item in SelectionService.GetSelectedComponents())
            {
                if (item == TabControl)
                {
                    _tabControlSelected = true;
                }

                var page = GetTabPageOfComponent(item);

                if ((page != null) && (page.Parent == TabControl))
                {
                    _tabControlSelected = false;
                    TabControl.SelectedTab = page;

                    break;
                }
            }
        }

        private ISelectionService SelectionService
        {
            get
            {
                if (_selection == null)
                {
                    _selection = GetService<ISelectionService>();

                    if (_selection == null)
                    {
                        throw new InvalidOperationException("GetService ISelectionService failed");
                    }
                }

                return _selection;
            }
        }

        private TService GetService<TService>()
        {
            return (TService)GetService(typeof(TService));
        }

        private void SetProperty(string name, object value)
        {
            SetProperty(TabControl, name, value);
        }

        private static void SetProperty(object target, string name, object value)
        {
            PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(target)[name];
            if (propDescriptor != null)
            {
                propDescriptor.SetValue(target, value);
            }
        }

        private static T GetPropertyValue<T>(object target, string name)
        {
            var descriptor = GetProperty(target, name);

            if (descriptor != null)
            {
                var value = descriptor.GetValue(target);
                
                if (value is T)
                {
                    return (T)(value);
                }
            }

            return default(T);
        }

        private static PropertyDescriptor GetProperty(object target, string name)
        {
            return TypeDescriptor.GetProperties(target)[name];
        }

        private static DesignerTransaction CreateDesignerTransaction(IDesignerHost host, String description)
        {
            try
            {
                return host.CreateTransaction(description);
            }
            catch (CheckoutException exc)
            {
                if (exc != CheckoutException.Canceled)
                {
                    throw;
                }
            }

            return null;
        }

        private static TComponent CreateComponent<TComponent>(IDesignerHost host) where TComponent : IComponent
        {
            return (TComponent)host.CreateComponent(typeof(TComponent));
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
    }
}

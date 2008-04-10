using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace WindowsFormsAero {
    class TabPageSwitcherDesigner : ParentControlDesigner {

        private DesignerVerbCollection verbs;  // a collection of actions to perform (appear as links in propgrid, on designer action panel)
        private ISelectionService selectionService;  // service which lets you know when the selection changes in the designer.

        /// <summary>
        /// The TabPageSwitcher we're designing - strongly typed wrapper around Component property.
        /// </summary>
        public TabPageSwitcher ControlSwitcher  {
            get { return Component as TabPageSwitcher; }
        }

        /// <summary>
        /// Fetches the selection service from the service provider - from this we can tell what's selected and when selection changes
        /// </summary>
        internal ISelectionService SelectionService {
            get {
                if (selectionService == null) {
                    selectionService = (ISelectionService)GetService(typeof(ISelectionService));
                    Debug.Assert(selectionService != null, "Failed to get Selection Service!");
                }
                return selectionService;
            }
        }

        /// <summary>
        /// List of "verbs" or actions to be used in the designer.  These typically appear on the Context Menu,
        /// links on the property grid, and as links on the designer action panel.
        /// </summary>
        public override System.ComponentModel.Design.DesignerVerbCollection Verbs {
            get {
                if (verbs == null) {

                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb(Resources.Strings.AddTab, new EventHandler(this.OnAdd)));         
                }

                return verbs;
            }
        }

        /// <summary>
        /// when the designer disposes, we need to be careful about
        /// unhooking from service events we've subscribed to.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing) {
                SelectionService.SelectionChanged -= new EventHandler(SelectionService_SelectionChanged);
            }
        }

        /// <summary>
        /// This is called when the designer is first loaded.  
        /// Usually a good time to hook up to events.  If you want to 
        /// set property defaults, InitializeNewComponent is what you
        /// want to override
        /// </summary>
        /// <param name="component"></param>
        public override void Initialize(IComponent component) {
            base.Initialize(component);
            SelectionService.SelectionChanged += new EventHandler(SelectionService_SelectionChanged);
        }

        public override bool CanParent(Control control) {
            return control is TabStripPage;            
        }
        /// <summary>
        /// Method implementation for our "Add TabStripPage verb".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eevent"></param>
        private void OnAdd(object sender, EventArgs eevent) {

            // fetch our designer host
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null) {

                // Create a transaction so we're friendly to undo/redo and serialization
                DesignerTransaction t = null;
                try {
                    try {
                        t = host.CreateTransaction(Resources.Strings.AddTab + Component.Site.Name);
                    }
                    catch (CheckoutException ex) {
                        if (ex == CheckoutException.Canceled) {
                            return;
                        }
                        throw ex;
                    }

                    // Add a TabStripPage to the controls collection of the TabPageSwitcher
                    
                    // Notify the TabPageSwitcher that it's control collection is changing.                    
                    MemberDescriptor member = TypeDescriptor.GetProperties(ControlSwitcher)["Controls"];
                    TabStripPage page = host.CreateComponent(typeof(TabStripPage)) as TabStripPage;
                    RaiseComponentChanging(member);
                   
                    // add the page to the controls collection.
                    ControlSwitcher.Controls.Add(page);

                    // set the SelectedTabStripPage to the current page so that it opens correctly
                    SetProperty("SelectedTabStripPage", page);                    
                    
                    // Raise event that we're done changing the controls property.
                    RaiseComponentChanged(member, null, null);

                    // if we have an associated TabStrip,
                    // add a matching Tab to the TabStrip.
                    if (ControlSwitcher.TabStrip != null) {

                        // add a tab to the toolstrip designer
                        MemberDescriptor itemsProp = TypeDescriptor.GetProperties(ControlSwitcher.TabStrip)["Items"];

                        TabStripButton tab = host.CreateComponent(typeof(TabStripButton)) as TabStripButton;
                        RaiseComponentChanging(itemsProp);

                        ControlSwitcher.TabStrip.Items.Add(tab);
                        RaiseComponentChanged(itemsProp, null, null);
                        
                        SetProperty(tab, "DisplayStyle", ToolStripItemDisplayStyle.ImageAndText);
                        SetProperty(tab, "Text", tab.Name);
                        SetProperty(tab, "TabStripPage", page);
                        SetProperty(ControlSwitcher.TabStrip, "SelectedTab", tab);
                    }
                    
                }
                finally {
                    if (t != null)
                        t.Commit();
                }
            }
         
        }

      
      
        void SelectionService_SelectionChanged(object sender, EventArgs e) {
            IList selectedComponents = (IList)SelectionService.GetSelectedComponents();
            if (selectedComponents.Count == 1) {
                TabStripButton tab = selectedComponents[0] as TabStripButton;
                if (tab != null) {
                    SetProperty("SelectedTabStripPage", tab.TabStripPage);
                    SetProperty(tab, "Checked", true);
                }
            }
        }
        private void SetProperty(object target, string propname, object value) {
            PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(target)[propname];
            if (propDescriptor != null) {
                propDescriptor.SetValue(target, value);
            }
        }
 
        private void SetProperty(string propname, object value) {
            SetProperty(ControlSwitcher, propname, value);
        }
    }
}

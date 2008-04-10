using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WindowsFormsAero
{
    [Serializable]
    internal class TabStripToolboxItem : ToolboxItem
    {
        public TabStripToolboxItem() 
        {
        }

        public TabStripToolboxItem(Type toolType)
            : base(toolType)
        {
        }

        private TabStripToolboxItem(SerializationInfo info, StreamingContext context)
        {
            Deserialize(info, context);
        }


        /// <summary>
        /// this method is called when dragging a TabStrip off the toolbox.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        protected override IComponent[] CreateComponentsCore(IDesignerHost host)
        {
            IComponent[] components = base.CreateComponentsCore(host);

            Control parentControl = null;
            ControlDesigner parentControlDesigner = null;
            TabStrip tabStrip = null;
            IComponentChangeService changeSvc = (IComponentChangeService)host.GetService(typeof(IComponentChangeService));


            // fish out the parent we're adding the TabStrip to.
            if (components.Length > 0 && components[0] is TabStrip)
            {
                tabStrip = components[0] as TabStrip;

                ITreeDesigner tabStripDesigner = host.GetDesigner(tabStrip) as ITreeDesigner;
                parentControlDesigner = tabStripDesigner.Parent as ControlDesigner;
                if (parentControlDesigner != null)
                {
                    parentControl = parentControlDesigner.Control;
                }
            }

            // Create a ControlSwitcher on the same parent.

            if (host != null)
            {
                TabPageSwitcher controlSwitcher = null;

                DesignerTransaction t = null;
                try
                {
                    try
                    {
                        t = host.CreateTransaction("add tabswitcher");
                    }
                    catch (CheckoutException ex)
                    {
                        if (ex == CheckoutException.Canceled)
                        {
                            return components;
                        }
                        throw ex;
                    }

                    // create a TabPageSwitcher and add it to the same parent as the TabStrip
                    MemberDescriptor controlsMember = TypeDescriptor.GetProperties(parentControlDesigner)["Controls"];
                    controlSwitcher = host.CreateComponent(typeof(TabPageSwitcher)) as TabPageSwitcher;

                    if (changeSvc != null)
                    {
                        changeSvc.OnComponentChanging(parentControl, controlsMember);
                        changeSvc.OnComponentChanged(parentControl, controlsMember, null, null);
                    }

                    // specify default values for our TabStrip
                    Dictionary<string, object> propertyValues = new Dictionary<string, object>();
                    propertyValues["Location"] = new Point(tabStrip.Left, tabStrip.Bottom + 3);
                    propertyValues["TabStrip"] = tabStrip;

                    // set the property values
                    SetProperties(controlSwitcher, propertyValues, host);

                }
                finally
                {
                    if (t != null)
                        t.Commit();
                }

                // add the TabPageSwitcher to the list of components that we've created
                if (controlSwitcher != null)
                {
                    IComponent[] newComponents = new IComponent[components.Length + 1];
                    Array.Copy(components, newComponents, components.Length);
                    newComponents[newComponents.Length - 1] = controlSwitcher;
                    return newComponents;
                }

            }

            return components;
        }


        /// <summary>
        ///  handy helper method for setting multiple properties. 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="properties"></param>
        /// <param name="host"></param>
        private void SetProperties(Component component, Dictionary<string, object> properties, IDesignerHost host)
        {
            IComponentChangeService changeSvc = (IComponentChangeService)host.GetService(typeof(IComponentChangeService));

            foreach (string propname in properties.Keys)
            {
                PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(component)[propname];

                if (changeSvc != null)
                {
                    changeSvc.OnComponentChanging(component, propDescriptor);
                }
                if (propDescriptor != null)
                {
                    propDescriptor.SetValue(component, properties[propname]);
                }
                if (changeSvc != null)
                {
                    changeSvc.OnComponentChanged(component, propDescriptor, null, null);
                }
            }


        }
    }
}

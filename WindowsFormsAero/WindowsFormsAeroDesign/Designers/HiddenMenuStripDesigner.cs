using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace WindowsFormsAero.Design
{
    public sealed class HiddenMenuStripDesigner : ComponentDesigner, IDesignerUtilsClient
    {
        private DesignerUtils _utils;

        public HiddenMenuStripDesigner()
        {
            _utils = new DesignerUtils(this);
        }

        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            var form = _utils.DesignerHost.RootComponent as Form;

            if (form != null)
            {
                var mainMenuStrip = form.MainMenuStrip;

                if (mainMenuStrip != null)
                {
                    var prop = TypeDescriptor.GetProperties(mainMenuStrip)["HideWhenInactive"];

                    if (prop != null)
                    {
                        prop.SetValue(mainMenuStrip, true);
                    }
                }
            }
        }

        IComponent IDesignerUtilsClient.Component
        {
            get { return Component; }
        }

        object IDesignerUtilsClient.GetService(Type type)
        {
            return GetService(type);
        }
    }
}

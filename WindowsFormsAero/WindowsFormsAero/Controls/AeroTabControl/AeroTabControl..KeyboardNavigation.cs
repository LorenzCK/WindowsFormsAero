using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    partial class AeroTabControl
    {
        public sealed class KeyboardNavigationSettingsConverter : ExpandableObjectConverter
        {
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(String) || base.CanConvertTo(context, destinationType);
            }
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(String))
                {
                    return string.Empty;
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        [TypeConverter(typeof(KeyboardNavigationSettingsConverter))]
        public sealed class KeyboardNavigationSettings
        {
            private const bool DefaultEnableCtrlTab = true;
            private const bool DefaultEnableCtrlNumbers = true;

            private const Keys DefaultNewTabShortcut = Keys.Control | Keys.T;
            private const Keys DefaultCloseTabShortcut = Keys.Control | Keys.W;

            internal KeyboardNavigationSettings()
            {
                EnableCtrlTab = DefaultEnableCtrlTab;
                EnableCtrlNumbers = DefaultEnableCtrlNumbers;

                NewTabShortcutKeys = DefaultNewTabShortcut;
                CloseTabShortutKeys = DefaultCloseTabShortcut;
            }

            [Browsable(true)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [DefaultValue(DefaultEnableCtrlNumbers)]
            public bool EnableCtrlNumbers
            {
                get;
                set;
            }

            [Browsable(true)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [DefaultValue(DefaultEnableCtrlTab)]
            public bool EnableCtrlTab
            {
                get;
                set;
            }

            [Browsable(true)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [DefaultValue(DefaultNewTabShortcut)]
            public Keys NewTabShortcutKeys
            {
                get;
                set;
            }

            [Browsable(true)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [DefaultValue(DefaultCloseTabShortcut)]
            public Keys CloseTabShortutKeys
            {
                get;
                set;
            }
        }
    }
}
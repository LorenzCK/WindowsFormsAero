using System;
using System.ComponentModel;
using System.Globalization;

namespace WindowsFormsAero
{
    public sealed class AeroTabControlButtonConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(String) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if (destinationType == typeof(String))
            {
                return string.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}

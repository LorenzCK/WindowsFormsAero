using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace WindowsFormsAero.Design
{
    public sealed class AeroTabControlSelectedPageConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(String);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(String))
            {
                return ((AeroTabPage)(value)).Name;
            }

            throw GetConvertToException(value, destinationType);
        }
    }
}

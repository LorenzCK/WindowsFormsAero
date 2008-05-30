using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [TypeConverter(typeof(AeroTabControlButtonConverter))]
    public abstract class AeroTabControlButton
    {
        private readonly TabStrip _tabStrip;

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        internal AeroTabControlButton(TabStrip tabStrip)
        {
            _tabStrip = tabStrip;

            ResetText();
            ResetShortcutKeys();
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual Keys ShortcutKeys
        {
            get;
            set;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public abstract string Text
        {
            get;
            set;
        }

        [Browsable(true)]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public abstract bool Visible
        {
            get;
            set;
        }

        protected TabStrip TabStrip
        {
            get { return _tabStrip; }
        }

        protected void ResetShortcutKeys()
        {
            ShortcutKeys = DefaultShortcutKeys;
        }

        protected void ResetText()
        {
            Text = DefaultText;
        }

        protected bool ShouldSerializeShortcutKeys()
        {
            return ShortcutKeys != DefaultShortcutKeys;
        }

        protected bool ShouldSerializeText()
        {
            return Text != DefaultText;
        }

        protected virtual Keys DefaultShortcutKeys
        {
            get { return Keys.None; }
        }

        protected virtual string DefaultText
        {
            get { return null; }
        }
    }
}

using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace WindowsFormsAero
{
    public sealed class AeroTabControlCloseButton : AeroTabControlButton
    {
        internal AeroTabControlCloseButton(TabStrip tabStrip) 
            : base(tabStrip)
        {
        }

        public override string Text
        {
            get { return TabStrip.CloseButtonText; }
            set { TabStrip.CloseButtonText = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool Visible
        {
            get { return Visibility != CloseButtonVisibility.Never; }
            set 
            {
                if (value)
                {
                    if (Visibility == CloseButtonVisibility.Never)
                    {
                        Visibility = CloseButtonVisibility.ExceptSingleTab;
                    }
                }
                else
                {
                    Visibility = CloseButtonVisibility.Never;
                }
            }
        }

        [Browsable(true)]
        [DefaultValue(CloseButtonVisibility.ExceptSingleTab)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public CloseButtonVisibility Visibility
        {
            get { return TabStrip.CloseButtonVisibility; }
            set { TabStrip.CloseButtonVisibility = value; }
        }

        protected override Keys DefaultShortcutKeys
        {
            get { return Keys.Control | Keys.W; }
        }

        protected override string DefaultText
        {
            get { return TabStrip.DefaultCloseButtonText; }
        }
    }
}

using System;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsAero
{
    public class AeroForm : Form
    {
        private Font _defaultFont;

        public AeroForm()
        {
            _defaultFont = SystemFonts.MenuFont;
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                if (base.Font == _defaultFont)
                {
                    _defaultFont.Dispose();
                    _defaultFont = null;
                }

                base.Font = value;
            }
        }

        public override void ResetFont()
        {
            Font = _defaultFont;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_defaultFont != null)
                {
                    _defaultFont.Dispose();
                }
            }

            _defaultFont = null;

            base.Dispose(disposing);
        }

        private bool ShouldSerializeFont()
        {
            return Font != _defaultFont;
        }
    }
}

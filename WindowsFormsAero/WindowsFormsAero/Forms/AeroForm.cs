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
            ResetFont();
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                if (base.Font != value)
                {
                    if (base.Font == _defaultFont)
                    {
                        _defaultFont.Dispose();
                        _defaultFont = null;
                    }

                    base.Font = value;
                }
            }
        }

        public override void ResetFont()
        {
            if (_defaultFont == null)
            {
                _defaultFont = SystemFonts.MenuFont;
            }

            base.Font = _defaultFont;
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
            return base.Font != _defaultFont;
        }
    }
}

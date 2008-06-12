using System;
using System.Drawing;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    public sealed class RichTextFont
    {
        private readonly ITextFont _font;

        internal RichTextFont(ITextFont font)
        {
            _font = font;
        }

        public String Name
        {
            get { return _font.Name; }
            set { _font.Name = value; }
        }

        public Single Size
        {
            get { return _font.Size; }
            set { _font.Size = value; }
        }

        public Color ForeColor
        {
            get { return ColorTranslator.FromWin32(_font.ForeColor); }
            set { _font.ForeColor = ColorTranslator.ToWin32(value); }
        }

        public Color BackColor
        {
            get { return ColorTranslator.FromWin32(_font.BackColor); }
            set { _font.BackColor = ColorTranslator.ToWin32(value); }
        }

        public Boolean Protected
        {
            get { return _font.Protected == TomConstants.True; }
            set { _font.Protected = TomConstants.FromBool(value); }
        }

        public Boolean Bold
        {
            get { return _font.Bold == TomConstants.True; }
            set { _font.Bold = TomConstants.FromBool(value); }
        }

        public Boolean Italic
        {
            get { return _font.Italic == TomConstants.True; }
            set { _font.Italic = TomConstants.FromBool(value); }
        }

        public RichTextUnderline Underline
        {
            get { return (RichTextUnderline)(_font.Underline & 0x0f); }
            set { SetUnderline(value, UnderlineColor); }
        }

        public RichTextUnderlineColor UnderlineColor
        {
            get { return (RichTextUnderlineColor)(_font.Underline & 0xf0); }
            set { SetUnderline(Underline, value); }
        }

        private void SetUnderline(RichTextUnderline type, RichTextUnderlineColor color)
        {
            _font.Underline = (int)(type) | (int)(color);
        }
    }
}

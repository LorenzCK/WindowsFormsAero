using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    public sealed class RichTextSelection : RichTextRange
    {
        private readonly ITextSelection _selection;

        internal RichTextSelection(RichTextDocument document, ITextSelection selection)
            : base(document, selection)
        {
            _selection = selection;
        }

        public bool StartIsActive
        {
            get { return GetFlag(TomConstants.SelStartActive); }
            set { SetFlag(TomConstants.SelStartActive, value); }
        }

        private bool GetFlag(int flag)
        {
            return (_selection.Flags & flag) != 0;
        }

        private void SetFlag(int flag, bool value)
        {
            if (value)
            {
                _selection.Flags |= flag;
            }
            else
            {
                _selection.Flags &= ~flag;
            }
        }
    }
}

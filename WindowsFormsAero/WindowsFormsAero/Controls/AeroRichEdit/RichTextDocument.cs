using System;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    public sealed class RichTextDocument
    {
        private readonly AeroRichEdit _richEdit;
        //private readonly IRichEditOle _richEditOle;
        private readonly ITextDocument _textDocument;

        private RichTextSelection _selection;

        internal RichTextDocument(AeroRichEdit box, IRichEditOle ole)
        {
            if (box == null) throw new ArgumentNullException("box");
            if (ole == null) throw new ArgumentNullException("ole");

            _richEdit = box;
            //_richEditOle = ole;
            _textDocument = (ITextDocument)(ole);
        }

        public AeroRichEdit Control
        {
            get { return _richEdit; }
        }

        public int Length
        {
            get { return _richEdit.TextLength; }
        }

        public RichTextRange Begin
        {
            get { return GetRange(0, 0); }
        }

        public RichTextRange End
        {
            get { return GetRange(Length, Length); }
        }

        public RichTextRange Range
        {
            get { return GetRange(0, _richEdit.TextLength); }
        }

        public RichTextRange GetRange(int first, int last)
        {
            return new RichTextRange(this, _textDocument.Range(first, last));
        }

        public RichTextRange RangeFromPoint(int x, int y)
        {
            return new RichTextRange(this, _textDocument.RangeFromPoint(x, y));
        }

        public RichTextSelection Selection
        {
            get
            {
                if (_selection == null)
                {
                    _selection = new RichTextSelection(this, _textDocument.Selection);
                }

                return _selection;
            }
        }

        internal void Freeze()
        {
            _textDocument.Freeze();
        }

        internal void Unfreeze()
        {
            _textDocument.Unfreeze();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    public sealed class RichTextParagraph
    {
        //private readonly RichTextDocument _document;
        private readonly ITextPara _para;

        internal RichTextParagraph(ITextPara para)
        {
            //_document = document;
            _para = para;
        }

        public void SetIndents(float start, float left, float right)
        {
            _para.SetIndents(start, left, right);
        }
    }
}

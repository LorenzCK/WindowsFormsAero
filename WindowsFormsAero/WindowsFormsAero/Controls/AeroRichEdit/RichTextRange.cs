using System;
using System.Drawing;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    public class RichTextRange
    {
        private const int TA_LEFT = 0;
        private const int TA_RIGHT = 2;
        private const int TA_TOP = 0;
        private const int TA_BOTTOM = 8;

        private readonly RichTextDocument _document;
        private readonly ITextRange _range;

        private RichTextFont _font;
        private RichTextParagraph _para;

        internal RichTextRange(RichTextDocument document, ITextRange range)
        {
            _document = document;
            _range = range;
        }

        public int Start { get { return _range.Start; } }

        public int End { get { return _range.End; } }

        public string Text
        {
            get { return _range.Text; }
            set { _range.Text = value; }
        }

        public bool IsSingleLine
        {
            get
            {
                var start = _range.GetDuplicate();
                var end = _range.GetDuplicate();

                start.Collapse(TomConstants.Start);
                end.Collapse(TomConstants.End);

                return start.GetIndex(TomConstants.Line) != end.GetIndex(TomConstants.Line);
            }
        }

        public RichTextFont Font
        {
            get
            {
                if (_font == null)
                {
                    _font = new RichTextFont(_range.Font);
                }

                return _font;
            }
        }

        public RichTextParagraph Paragraph
        {
            get
            {
                if (_para == null)
                {
                    _para = new RichTextParagraph(_range.Para);
                }

                return _para;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                if (IsSingleLine)
                {
                    var pt1 = StartPoint;
                    var pt2 = EndPoint;

                    return new Rectangle(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);
                }
                else
                {
                    var rect = _document.Control.ClientRectangle;

                    rect.Y = StartPoint.Y;
                    rect.Height = EndPoint.Y - rect.Y;
                    rect.Width -= 1;

                    return rect;
                }
            }
        }

        public Point StartPoint
        {
            get { return GetRangePoint(TomConstants.Start | TA_TOP | TA_LEFT); }
        }

        public Point EndPoint
        {
            get { return GetRangePoint(TomConstants.End | TA_BOTTOM | TA_LEFT); }
        }

        public void SetRange(int active, int other)
        {
            _range.SetRange(active, other);
        }

        public int MoveStartWhile(int count, params char[] chars)
        {
            object o = new string(chars);
            return _range.MoveStartWhile(ref o, count);
        }

        private Point GetRangePoint(int flags)
        {
            int x, y;

            _range.GetPoint(flags, out x, out y);
            return _document.Control.PointToClient(new Point(x, y));
        }
    }
}

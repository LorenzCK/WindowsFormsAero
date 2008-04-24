using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [Serializable]
    public sealed class FrameHitTestEventArgs : EventArgs
    {
        private readonly Point _point;
        private readonly Form _form;

        private FrameHitTestResult? _result;

        public FrameHitTestEventArgs(Form form, Point point)
        {
            _form = form;
            _point = point;
        }

        public Form Form
        {
            get { return _form; }
        }

        public Point Point
        {
            get { return _point; }
        }

        public FrameHitTestResult Result
        {
            get { return _result ?? FrameHitTestResult.Nowhere; }
            set { _result = value; }
        }

        internal bool IsAssigned
        {
            get { return _result.HasValue; }
        }
    }
}

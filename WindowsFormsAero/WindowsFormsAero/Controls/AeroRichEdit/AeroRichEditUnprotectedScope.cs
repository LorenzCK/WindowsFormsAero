using System;

namespace WindowsFormsAero
{
    public struct AeroRichEditUnprotectedScope : IDisposable, IEquatable<AeroRichEditUnprotectedScope>
    {
        private AeroRichEdit _ctl;
        private Boolean _prev;

        internal AeroRichEditUnprotectedScope(AeroRichEdit ctl)
        {
            _prev = ctl._allowProtectedModifications;

            _ctl = ctl;
            _ctl._allowProtectedModifications = true;
            _ctl.TextDocument.Freeze();
        }

        public void Dispose()
        {
            if (_ctl != null)
            {
                _ctl.TextDocument.Unfreeze();
                _ctl._allowProtectedModifications = _prev;
            }
        }

        public override int GetHashCode()
        {
            if (_ctl != null)
            {
                return _ctl.GetHashCode();
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is AeroRichEditUnprotectedScope)
            {
                return Equals((AeroRichEditUnprotectedScope)(obj));
            }

            return false;
        }

        public bool Equals(AeroRichEditUnprotectedScope other)
        {
            return (_ctl == other._ctl);
        }
    }
}
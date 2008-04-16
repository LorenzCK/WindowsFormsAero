using System;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    internal abstract class MessageFilter : NativeWindow, IDisposable
    {
        private Control _owner;

        protected MessageFilter()
        {
        }

        protected MessageFilter(Control owner)
        {
            AssignControl(owner);
        }

        protected void AssignControl(Control owner)
        {
            if (_owner != owner)
            {
                if (_owner != null)
                {
                    _owner.HandleDestroyed -= OnHandleDestroyed;
                    _owner.HandleCreated -= OnHandleCreated;
                    _owner = null;

                    ReleaseHandle();
                }

                _owner = owner;

                if (_owner != null)
                {
                    _owner.HandleCreated += OnHandleCreated;
                    _owner.HandleDestroyed += OnHandleDestroyed;

                    if (_owner.IsHandleCreated)
                    {
                        OnHandleCreated(_owner, EventArgs.Empty);
                    }
                }

                OnControlChange();
            }
        }

        protected virtual void OnControlChange()
        {
        }

        public void Dispose()
        {
            AssignControl(null);
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            if (_owner != null)
            {
                AssignHandle(_owner.Handle);
            }
        }

        private void OnHandleDestroyed(object sender, EventArgs e)
        {
            ReleaseHandle();
        }
    }
}

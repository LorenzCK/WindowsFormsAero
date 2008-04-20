using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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

        public void Dispose()
        {
            AssignControl(null);
        }

        protected HandleRef GetHandleRef()
        {
            return new HandleRef(_owner, _owner.Handle);
        }

        protected void AssignControl(Control owner)
        {
            if (_owner != owner)
            {
                if (_owner != null)
                {
                    _owner.HandleDestroyed -= OnOwnerHandleDestroyed;
                    _owner.HandleCreated -= OnOwnerHandleCreated;
                    _owner = null;

                    ReleaseHandle();
                }

                _owner = owner;

                if (_owner != null)
                {
                    _owner.HandleCreated += OnOwnerHandleCreated;
                    _owner.HandleDestroyed += OnOwnerHandleDestroyed;

                    if (_owner.IsHandleCreated)
                    {
                        OnOwnerHandleCreated(_owner, EventArgs.Empty);
                    }
                }

                OnControlChange();
            }
        }

        protected virtual void OnControlChange()
        {
        }

        protected virtual void OnHandleCreated()
        {
        }

        protected virtual void OnHandleDestroyed()
        {
        }
        
        private void OnOwnerHandleCreated(object sender, EventArgs e)
        {
            if (_owner != null)
            {
                AssignHandle(_owner.Handle);
            }

            OnHandleCreated();
        }

        private void OnOwnerHandleDestroyed(object sender, EventArgs e)
        {
            ReleaseHandle();
            OnHandleDestroyed();
        }
    }
}

using System;
using System.ComponentModel;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    internal sealed partial class HookWindow
    {
        private readonly EventHandlerList Events = new EventHandlerList();
        private readonly Object SyncRoot = new Object();
        
        private readonly HookNativeWindow _window;
        private readonly HookLibrary _library;

        public event KeyEventHandler KeyDown
        {
            add 
            {
                lock (SyncRoot)
                {
                    Events.AddHandler(EventKeyDown, value);
                    UpdateKeyboardHook();
                }
            }
            remove 
            {
                lock (SyncRoot)
                {
                    Events.RemoveHandler(EventKeyDown, value);
                    UpdateKeyboardHook();
                }
            }
        }

        private HookWindow()
        {
            const int HWND_MESSAGE = -3;

            _window = new HookNativeWindow(this);
            _window.CreateHandle(new CreateParams()
            {
                Parent = new IntPtr(HWND_MESSAGE),
            });

            _library = new HookLibrary(_window.Handle);
        }

        private void Dispose()
        {
            _library.Dispose();
            _window.DestroyHandle();
        }

        private void UpdateKeyboardHook()
        {
            _library.SetHook(WindowsHookType.Keyboard, HasKeyboardEventHandlers);
        }

        private bool HasKeyboardEventHandlers
        {
            get
            {
                return Events[EventKeyDown] != null;
            }
        }

        private static readonly object EventKeyDown = new object();

        private sealed class HookNativeWindow : NativeWindow
        {
            private readonly HookWindow _owner;

            public HookNativeWindow(HookWindow owner)
            {
                _owner = owner;
            }
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case WindowMessages.WM_KEYDOWN:
                        System.Diagnostics.Debug.WriteLine("WM_KEYDOWN");
                        break;

                    case WindowMessages.WM_KEYUP:
                        System.Diagnostics.Debug.WriteLine("WM_KEYUP");
                        break;

                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
        }
    }
}

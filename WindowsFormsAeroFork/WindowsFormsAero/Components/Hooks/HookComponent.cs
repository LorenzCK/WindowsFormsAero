using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    public abstract class HookComponent : EnableableComponent
    {
        private HookWindow _window;

        internal HookWindow Window
        {
            get { return _window; }
        }

        protected override void OnStart()
        {
            if (_window == null)
            {
                _window = HookWindow.IncrementRefCount();
            }
        }

        protected override void OnStop()
        {
            if (_window != null)
            {
                HookWindow.DecrementRefCount();
            }

            _window = null;
        }
    }
}

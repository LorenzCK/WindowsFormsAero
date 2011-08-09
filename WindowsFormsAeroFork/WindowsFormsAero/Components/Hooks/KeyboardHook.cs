using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class KeyboardHook : HookComponent
    {
        public KeyboardHook()
        {
        }

        public KeyboardHook(IContainer container)
        {
            container.Add(this);
        }

        public event KeyEventHandler KeyDown
        {
            add
            {
                if (Events[EventKeyDown] == null)
                {
                    Window.KeyDown += OnKeyDown;
                }

                Events.AddHandler(EventKeyDown, value);
            }
            remove
            {
                Events.RemoveHandler(EventKeyDown, value);

                if (Events[EventKeyDown] == null)
                {
                    Window.KeyDown -= OnKeyDown;
                }
            }
        }

        protected override void OnStop()
        {
            if (Events[EventKeyDown] != null)
            {
                Window.KeyDown -= OnKeyDown;
            }

            base.OnStop();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        private static readonly object EventKeyDown = new object();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WindowsFormsAero
{
    [System.ComponentModel.DefaultProperty("Enabled")]
    [System.ComponentModel.DesignerCategory("Code")]
    public abstract class EnableableComponent : Component
    {
        private Boolean _enabled;

        [DefaultValue(false)]
        [Browsable(true)]
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;

                    if (!DesignMode && value)
                    {
                        OnStart();
                    }
                    else if (!DesignMode && !value)
                    {
                        OnStop();
                    }
                }
            }
        }

        public void Start()
        {
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();
            }
            
            base.Dispose(disposing);
        }

        protected abstract void OnStart();

        protected abstract void OnStop();
    }
}

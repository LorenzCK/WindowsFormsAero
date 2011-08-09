using System;
using System.ComponentModel;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class WindowsHook : EnableableComponent
    {
        private WindowsHookType _hookType;
        private String _functionName;
        private String _moduleName;
        private Int32 _threadId;

        private NativeModule _module;
        private IntPtr _callback;

        private SafeWindowsHookHandle _hook;

        public WindowsHook()
        {
        }

        public WindowsHook(IContainer container)
        {
            if (container != null)
            {
                container.Add(this);
            }
        }

        public WindowsHookType HookType
        {
            get { return _hookType; }
            set { _hookType = value; }
        }

        public String ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        public String FunctionName
        {
            get { return _functionName; }
            set { _functionName = value; }
        }

        public Int32 ThreadId
        {
            get { return _threadId; }
            set { _threadId = value; }
        }

        protected override void OnStart()
        {
            _module = NativeModule.Load(_moduleName);
            _callback = _module.GetProcedureAddress(_functionName);

            _hook = NativeMethods.SetWindowsHookEx(_hookType, _callback, _module, _threadId);
        }

        protected override void OnStop()
        {
            if (_hook != null)
            {
                _hook.Dispose();
                _hook = null;
            }

            if (_module != null)
            {
                _module.Dispose();
                _module = null;
            }
        }
    }
}

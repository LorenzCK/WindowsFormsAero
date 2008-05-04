using System;

namespace WindowsFormsAero
{
    partial class HookWindow
    {
        public static HookWindow IncrementRefCount()
        {
            lock (StaticSyncRoot)
            {
                if (_instance == null)
                {
                    _instance = new HookWindow();
                }

                ++_references;
            }

            return _instance;
        }

        public static void DecrementRefCount()
        {
            lock (StaticSyncRoot)
            {
                --_references;

                if (_references == 0)
                {
                    _instance.Dispose();
                    _instance = null;
                }
            }
        }

        private static HookWindow _instance;
        private static UInt32 _references;

        private static readonly object StaticSyncRoot = new object();
    }
}

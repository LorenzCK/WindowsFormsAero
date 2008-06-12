using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsAero.InteropServices
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class RichEditObject
    {
        public RichEditObject()
        {
        }

        private Int32 _cbStruct = Marshal.SizeOf(typeof(RichEditObject));

        private Int32 _charIndex;
        private Guid _clsid;
        private IntPtr _object;
        private IStorage _storage;
        private IOleClientSite _site;
        private SIZEL _sizel;
        private DVASPECT _dvaspect;
        private RichEditObjectFlags _flags;
        private UInt32 _user;

        public static RichEditObject FromControl(Int32 charIndex, IOleClientSite site,  Control control)
        {
            var clsid = Marshal.GenerateGuidForType(control.GetType());
            var lpunk = Marshal.GetIUnknownForObject(control);

            const StorageFlags flags = 
                StorageFlags.Create | StorageFlags.ReadWrite |
                StorageFlags.ShareExclusive;

            var bytes = NativeMethods.CreateILockBytesOnHGlobal(IntPtr.Zero, true);

            try
            {
                var storage = NativeMethods.StgCreateDocfileOnILockBytes(bytes, flags, 0);

                try
                {
                    return new RichEditObject()
                    {
                        _charIndex = charIndex,
                        _storage = storage,
                        _site = site,

                        _dvaspect = DVASPECT.Content,
                        _flags = RichEditObjectFlags.BelowBaseline | RichEditObjectFlags.InvertedSelect,
                        _clsid = clsid,
                        _object = lpunk,
                    };
                }
                finally
                {
                    Marshal.ReleaseComObject(storage);
                }
            }
            finally
            {
                Marshal.ReleaseComObject(bytes);
            }
        }
    }

}

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsAero.InteropServices
{
    using ComTypes = System.Runtime.InteropServices.ComTypes;

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
        private ComTypes.DVASPECT _dvaspect;
        private RichEditObjectFlags _flags;
        private UInt32 _user;

        private static Guid IIDOf<T>()
        {
            return Marshal.GenerateGuidForType(typeof(T));
        }

        public static RichEditObject FromBitmap(Int32 charIndex, IOleClientSite site, Bitmap bitmap)
        {
            const int CF_BITMAP = 2;
            var IID_IDataObject = IIDOf<ComTypes.IDataObject>();

            var dataObject = new DataObject(bitmap);
            var comDataObject = (ComTypes.IDataObject)(dataObject);
            
            var formatEtc = new ComTypes.FORMATETC()
            {
                lindex = -1,
                ptd = IntPtr.Zero,
                cfFormat = CF_BITMAP,
                tymed = ComTypes.TYMED.TYMED_GDI,
                dwAspect = ComTypes.DVASPECT.DVASPECT_CONTENT,
            };

            var storage = CreateIStorageOnHGlobal();
            var oleObject = NativeMethods.OleCreateStaticFromData(dataObject,
                                                                  ref IID_IDataObject,
                                                                  OleRender.Format,
                                                                  ref formatEtc,
                                                                  site,
                                                                  storage);

            NativeMethods.OleSetContainedObject(oleObject, true);

            return new RichEditObject()
            {
                _charIndex = charIndex,
                _storage = storage,
                _site = site,

                _dvaspect = ComTypes.DVASPECT.DVASPECT_CONTENT,
                _flags = RichEditObjectFlags.BelowBaseline | RichEditObjectFlags.InPlaceActive,
                _clsid = Marshal.GenerateGuidForType(dataObject.GetType()),
                _object = oleObject,
            };
        }

        public static RichEditObject FromControl(Int32 charIndex, IOleClientSite site, Control control)
        {
            var storage = CreateIStorageOnHGlobal();

            try
            {
                return new RichEditObject()
                {
                    _charIndex = charIndex,
                    _storage = storage,
                    _site = site,

                    _dvaspect = ComTypes.DVASPECT.DVASPECT_CONTENT,
                    _flags = RichEditObjectFlags.BelowBaseline | RichEditObjectFlags.InvertedSelect,
                    _clsid = Marshal.GenerateGuidForType(control.GetType()),
                    _object = Marshal.GetIUnknownForObject(control),
                };
            }
            finally
            {
                Marshal.ReleaseComObject(storage);
            }
        }

        private static IStorage CreateIStorageOnHGlobal()
        {
            var bytes = NativeMethods.CreateILockBytesOnHGlobal(IntPtr.Zero, true);

            try
            {
                const StorageFlags CreateRWExclusive = StorageFlags.Create |
                                                       StorageFlags.ReadWrite |
                                                       StorageFlags.ShareExclusive;

                return NativeMethods.StgCreateDocfileOnILockBytes(bytes, CreateRWExclusive, 0);
            }
            finally
            {
                Marshal.ReleaseComObject(bytes);
            }
        }
    }
}

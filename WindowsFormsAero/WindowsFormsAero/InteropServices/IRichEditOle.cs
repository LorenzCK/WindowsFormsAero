using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [Guid("00020D00-0000-0000-c000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IRichEditOle
    {
        IOleClientSite GetClientSite();

        [PreserveSig]
        int GetObjectCount();

        [PreserveSig]
        int GetLinkCount();

        void GetObject(
            [In] Int32 iob,
            [In, Out] RichEditObject lpreobject,
            [In] GetRichEditObjectInterfaces flags);

        void InsertObject(RichEditObject lpreobject);

        void ConvertObject(int iob, Guid rclsidNew, string lpstrUserTypeNew);
        void ActivateAs(Guid rclsid, Guid rclsidAs);
        void SetHostNames(string lpstrContainerApp, string lpstrContainerObj);
        void SetLinkAvailable(int iob, int fAvailable);
        void SetDvaspect(int iob, uint dvaspect);
        void HandsOffStorage(int iob);
        void SaveCompleted(int iob, IntPtr lpstg);
        void InPlaceDeactivate();
        void ContextSensitiveHelp(int fEnterMode);

        void GetClipboardData(IntPtr lpchrg, uint reco, IntPtr lplpdataobj);
        void ImportDataObject(IntPtr lpdataobj, Int16 cf, IntPtr hMetaPict);
    }
}

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [DefaultMember("Item")]
    [SuppressUnmanagedCodeSecurity]
    [Guid("8CC497C5-A1DF-11CE-8098-00AA0047BE5D")]
    internal interface ITextStoryRanges : IEnumerable
    {
        [return: MarshalAs(UnmanagedType.Interface)]
        ITextRange Item([In] int Index);

        int Count { get; }
    }
}


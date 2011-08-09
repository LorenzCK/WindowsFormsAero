using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [DefaultMember("Text")]
    [SuppressUnmanagedCodeSecurity]
    [Guid("8CC497C1-A1DF-11CE-8098-00AA0047BE5D")]
    [TypeLibType(TypeLibTypeFlags.FDispatchable | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible)]
    internal interface ITextSelection : ITextRange
    {
        int Flags { get; set; }

        int Type { get; }
        
        int MoveLeft([In] int Unit, [In] int Count, [In] int Extend);
        
        int MoveRight([In] int Unit, [In] int Count, [In] int Extend);
        
        int MoveUp([In] int Unit, [In] int Count, [In] int Extend);
        
        int MoveDown([In] int Unit, [In] int Count, [In] int Extend);
        
        int HomeKey([In] int Unit, [In] int Extend);
        
        int EndKey([In] int Unit, [In] int Extend);
        
        void TypeText([In, MarshalAs(UnmanagedType.BStr)] string bstr);
    }
}


using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [DefaultMember("Name")]
    [SuppressUnmanagedCodeSecurity]
    [Guid("8CC497C0-A1DF-11CE-8098-00AA0047BE5D")]
    [TypeLibType(TypeLibTypeFlags.FDispatchable | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible)]
    internal interface ITextDocument
    {
        string Name { get; }

        ITextSelection Selection { get; }
        
        int StoryCount { get; }
        
        ITextStoryRanges StoryRanges { get; }
        
        int Saved { get; set; }
        
        float DefaultTabStop { get; set; }

        void New();

        void Open([In, MarshalAs(UnmanagedType.Struct)] ref object pVar, [In] int Flags, [In] int CodePage);
        
        void Save([In, MarshalAs(UnmanagedType.Struct)] ref object pVar, [In] int Flags, [In] int CodePage);
        
        int Freeze();
        
        int Unfreeze();
        
        void BeginEditCollection();
        
        void EndEditCollection();
        
        int Undo([In] int Count);
        
        int Redo([In] int Count);
        
        ITextRange Range([In] int cp1, [In] int cp2);

        ITextRange RangeFromPoint([In] int x, [In] int y);
    }
}
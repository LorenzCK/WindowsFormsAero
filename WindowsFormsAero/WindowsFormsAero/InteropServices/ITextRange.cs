using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [DefaultMember("Text")]
    [SuppressUnmanagedCodeSecurity]
    [Guid("8CC497C2-A1DF-11CE-8098-00AA0047BE5D")]
    [TypeLibType(TypeLibTypeFlags.FDispatchable | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible)]
    internal interface ITextRange
    {
        string Text { get; set; }

        int Char { get; set; }
        
        ITextRange GetDuplicate();

        ITextRange FormattedText { get; set; }

        int Start { get; set; }

        int End { get; set; }

        ITextFont Font { get; set; }

        ITextPara Para { get; set; }

        int StoryLength { get; }

        int StoryType { get; }
        
        void Collapse([In] int bStart);
        
        int Expand([In] int Unit);
        
        int GetIndex([In] int Unit);
        
        void SetIndex([In] int Unit, [In] int Index, [In] int Extend);
        
        void SetRange([In] int cpActive, [In] int cpOther);
        
        int InRange([In, MarshalAs(UnmanagedType.Interface)] ITextRange pRange);
        
        int InStory([In, MarshalAs(UnmanagedType.Interface)] ITextRange pRange);
        
        int IsEqual([In, MarshalAs(UnmanagedType.Interface)] ITextRange pRange);
        
        void Select();
        
        int StartOf([In] int Unit, [In] int Extend);
        
        int EndOf([In] int Unit, [In] int Extend);
        
        int Move([In] int Unit, [In] int Count);
        
        int MoveStart([In] int Unit, [In] int Count);
        
        int MoveEnd([In] int Unit, [In] int Count);
        
        int MoveWhile([In, MarshalAs(UnmanagedType.Struct)] ref object Cset, [In] int Count);
        
        int MoveStartWhile([In, MarshalAs(UnmanagedType.Struct)] ref object Cset, [In] int Count);
        
        int MoveEndWhile([In, MarshalAs(UnmanagedType.Struct)] ref object Cset, [In] int Count);
        
        int MoveUntil([In, MarshalAs(UnmanagedType.Struct)] ref object Cset, [In] int Count);
        
        int MoveStartUntil([In, MarshalAs(UnmanagedType.Struct)] ref object Cset, [In] int Count);
        
        int MoveEndUntil([In, MarshalAs(UnmanagedType.Struct)] ref object Cset, [In] int Count);
        
        int FindText([In, MarshalAs(UnmanagedType.BStr)] string bstr, [In] int cch, [In] int Flags);
        
        int FindTextStart([In, MarshalAs(UnmanagedType.BStr)] string bstr, [In] int cch, [In] int Flags);
        
        int FindTextEnd([In, MarshalAs(UnmanagedType.BStr)] string bstr, [In] int cch, [In] int Flags);
        
        int Delete([In] int Unit, [In] int Count);
        
        void Cut([MarshalAs(UnmanagedType.Struct)] out object pVar);
        
        void Copy([MarshalAs(UnmanagedType.Struct)] out object pVar);
        
        void Paste([In, MarshalAs(UnmanagedType.Struct)] ref object pVar, [In] int Format);
        
        int CanPaste([In, MarshalAs(UnmanagedType.Struct)] ref object pVar, [In] int Format);
        
        int CanEdit();
        
        void ChangeCase([In] int Type);
        
        void GetPoint([In] int Type, out int px, out int py);
        
        void SetPoint([In] int x, [In] int y, [In] int Type, [In] int Extend);
        
        void ScrollIntoView([In] int Value);

        [return: MarshalAs(UnmanagedType.IUnknown)]
        object GetEmbeddedObject();
    }
}

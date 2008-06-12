using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [DefaultMember("Duplicate")]
    [SuppressUnmanagedCodeSecurity]
    [Guid("8CC497C4-A1DF-11CE-8098-00AA0047BE5D")]
    [TypeLibType(TypeLibTypeFlags.FDispatchable | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible)]
    internal interface ITextPara
    {
        ITextPara Duplicate { get; set; }

        int CanChange();
        
        int IsEqual([In, MarshalAs(UnmanagedType.Interface)] ITextPara pPara);
        
        void Reset([In] int Value);

        int Style { get; set; }

        int Alignment { get; set; }

        int Hyphenation { get; set; }

        float FirstLineIndent { get; }

        int KeepTogether { get; set; }

        int KeepWithNext { get; set; }
        
        float LeftIndent { get; }
        
        float LineSpacing { get; }
        
        int LineSpacingRule { get; }

        int ListAlignment { get; set; }

        int ListLevelIndex { get; set; }

        int ListStart { get; set; }

        float ListTab { get; set; }

        int ListType { get; set; }

        int NoLineNumber { get; set; }

        int PageBreakBefore { get; set; }

        float RightIndent { get; set; }
        
        void SetIndents([In] float StartIndent, [In] float LeftIndent, [In] float RightIndent);
        
        void SetLineSpacing([In] int LineSpacingRule, [In] float LineSpacing);

        float SpaceAfter { get; set; }

        float SpaceBefore { get; set; }

        int WidowControl { get; set; }

        int TabCount { get; }
        
        void AddTab([In] float tbPos, [In] int tbAlign, [In] int tbLeader);
        
        void ClearAllTabs();
        
        void DeleteTab([In] float tbPos);
        
        void GetTab([In] int iTab, out float ptbPos, out int ptbAlign, out int ptbLeader);
    }
}


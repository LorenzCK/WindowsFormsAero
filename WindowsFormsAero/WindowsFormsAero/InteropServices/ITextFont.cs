using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Security;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [DefaultMember("Duplicate")]
    [SuppressUnmanagedCodeSecurity]
    [Guid("8CC497C3-A1DF-11CE-8098-00AA0047BE5D")]
    [TypeLibType(TypeLibTypeFlags.FDispatchable | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible)]
    internal interface ITextFont
    {
        ITextFont Duplicate { [return: MarshalAs(UnmanagedType.Interface)] get; set; }

        int CanChange();
        
        int IsEqual([In, MarshalAs(UnmanagedType.Interface)] ITextFont pFont);
        
        void Reset([In] int Value);

        int Style { get; set; }

        int AllCaps { get; set; }

        int Animation { get; set; }

        int BackColor { get; set; }

        int Bold { get; set; }

        int Emboss { get; set; }

        int ForeColor { get; set; }

        int Hidden { get; set; }

        int Engrave { get; set; }

        int Italic { get; set; }

        float Kerning { get; set; }

        int LanguageID { get; set; }

        string Name { get; set; }

        int Outline { get; set; }

        float Position { get; set; }

        int Protected { get; set; }

        int Shadow { get; set; }

        float Size { get; set; }

        int SmallCaps { get; set; }

        float Spacing { get; set; }

        int StrikeThrough { get; set; }

        int Subscript { get; set; }

        int Superscript { get; set; }

        int Underline { get; set; }

        int Weight { get; set; }
    }
}

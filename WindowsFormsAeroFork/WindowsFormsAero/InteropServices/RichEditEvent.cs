using System;

namespace WindowsFormsAero.InteropServices
{
    [Flags, Serializable]
    internal enum RichEditEvent
    {
        None = 0x00000000,
        Change = 0x00000001,
        Update = 0x00000002,
        Scroll = 0x00000004,
        ScrollEvents = 0x00000008,
        DragDropDone = 0x00000010,
        ParagraphExpanded = 0x00000020,
        PageChage = 0x00000040,
        KeyEvents = 0x00010000,
        MouseEvents = 0x00020000,
        RequestResize = 0x00040000,
        SelChange = 0x00080000,
        DropFiles = 0x00100000,
        Protected = 0x00200000,
        CorrectText = 0x00400000,
        ImeChange = 0x00800000,
        LangChange = 0x01000000,
        ObjectPositions = 0x02000000,
        Link = 0x04000000,
    }
}
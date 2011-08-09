using System;
using System.Diagnostics.CodeAnalysis;

namespace WindowsFormsAero.InteropServices
{
    [Serializable]
    internal enum MarginType : int
    {
        Sizing = 3601,
        Content = 3602,
        Caption = 3603
    }

    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    internal enum MenuPart : int
    {
        MENU_MENUITEM_TMSCHEMA = 1,
        MENU_MENUDROPDOWN_TMSCHEMA = 2,
        MENU_MENUBARITEM_TMSCHEMA = 3,
        MENU_MENUBARDROPDOWN_TMSCHEMA = 4,
        MENU_CHEVRON_TMSCHEMA = 5,
        MENU_SEPARATOR_TMSCHEMA = 6,
        BarBackground = 7,
        BarItem = 8,
        PopupBackground = 9,
        PopupBorders = 10,
        PopupCheck = 11,
        PopupCheckBackground = 12,
        PopupGutter = 13,
        PopupItem = 14,
        PopupSeparator = 15,
        PopupSubmenu = 16,
        MENU_SYSTEMCLOSE = 17,
        MENU_SYSTEMMAXIMIZE = 18,
        MENU_SYSTEMMINIMIZE = 19,
        MENU_SYSTEMRESTORE = 20
    }

    internal static class MenuBarState
    {
        internal const int Active = 1;
        internal const int Inactive = 2;
    }

    internal static class MenuBarItemStates
    {
        internal const int Normal = 1;
        internal const int Hot = 2;
        internal const int Pushed = 3;
        internal const int Disabled = 4;
        internal const int DisabledHot = 5;
        internal const int DisabledPushed = 6;
    }

    internal static class MenuPopupItemStates 
    {
        internal const int Normal = 1;
        internal const int Hot = 2;
        internal const int Disabled = 3;
        internal const int DisabledHot = 4;
    }

    internal static class MenuPopupCheckState
    {
        internal const int CheckmarkNormal = 1;
        internal const int CheckmarkDisabled = 2;
        internal const int BulletNormal = 3;
        internal const int BulletDisabled = 4;
    }

    internal static class MenuPopupCheckBackgroundState
    {
        internal const int Disabled = 1;
        internal const int Normal = 2;
        internal const int Bitmap = 3;
    }

    internal static class MenuPopupSubmenuState
    {
        internal const int Normal = 1;
        internal const int Disabled = 2;
    }


}

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
    public enum MenuPart : int
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

    public static class MenuBarState
    {
        public const int Active = 1;
        public const int Inactive = 2;
    }

    public static class MenuBarItemStates
    {
        public const int Normal = 1;
        public const int Hot = 2;
        public const int Pushed = 3;
        public const int Disabled = 4;
        public const int DisabledHot = 5;
        public const int DisabledPushed = 6;
    }

    public static class MenuPopupItemStates 
    {
        public const int Normal = 1;
        public const int Hot = 2;
        public const int Disabled = 3;
        public const int DisabledHot = 4;
    }

    public static class MenuPopupCheckState
    {
        public const int CheckmarkNormal = 1;
        public const int CheckmarkDisabled = 2;
        public const int BulletNormal = 3;
        public const int BulletDisabled = 4;
    }

    public static class MenuPopupCheckBackgroundState
    {
        public const int Disabled = 1;
        public const int Normal = 2;
        public const int Bitmap = 3;
    }

    public static class MenuPopupSubmenuState
    {
        public const int Normal = 1;
        public const int Disabled = 2;
    }


}

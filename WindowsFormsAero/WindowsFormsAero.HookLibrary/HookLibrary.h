#pragma once


#pragma region include <Windows.h>
#define _WIN32_WINNT 0x0400

#define STRICT
#define WIN32_LEAN_AND_MEAN

// #define NOKERNEL          // All KERNEL defines and routines
// #define NOMSG             // typedef MSG and associated routines
// #define NOUSER            // All USER defines and routines
// #define NOVIRTUALKEYCODES // VK_
// #define NOWH              // SetWindowsHook and WH_
// #define NOWINMESSAGES     // WM_, EM_*, LB_*, CB_*

#define NOATOM            // Atom Manager routines
#define NOCLIPBOARD       // Clipboard routines
#define NOCOLOR           // Screen colors
#define NOCOMM            // COMM driver routines
#define NOCTLMGR          // Control and Dialog routines
#define NOCRYPT
#define NODEFERWINDOWPOS  // DeferWindowPos routines
#define NODESKTOP         // Desktop
#define NODRAWTEXT        // DrawText() and DT_
#define NOGDI             // All GDI defines and routines
#define NOGDICAPMASKS     // CC_, LC_*, PC_*, CP_*, TC_*, RC_
#define NOHELP            // Help engine interface.
#define NOICONS           // IDI_
#define NOIME             // Input Method Editor
#define NOKANJI           // Kanji support stuff.
#define NOKEYSTATES       // MK_
#define NOLANGUAGE        // Language
#define NOMB              // MB_ and MessageBox()
#define NOMCX             // Modem Configuration Extensions
#define NOMDI             // MDI
#define NOMEMMGR          // GMEM_, LMEM_*, GHND, LHND, associated routines
#define NOMENUS           // MF_
#define NOMETAFILE        // typedef METAFILEPICT
#define NOMINMAX          // Macros min(a,b) and max(a,b)
#define NONLS             // All NLS defines and routines
#define NOOPENFILE        // OpenFile(), OemToAnsi, AnsiToOem, and OF_
#define NOPROFILER        // Profiler interface.
#define NORASTEROPS       // Binary and Tertiary raster ops
#define NORESOURCE        // Resources
#define NOSCROLL          // SB_ and scrolling routines
#define NOSECURITY        // Security API
#define NOSERVICE         // All Service Controller routines, SERVICE_ equates, etc.
#define NOSHOWWINDOW      // SW_
#define NOSOUND           // Sound driver routines
#define NOSYSCOMMANDS     // SC_
#define NOSYSMETRICS      // SM_
#define NOSYSPARAMSINFO   // SystemParametersInfo()
#define NOTEXTMETRIC      // typedef TEXTMETRIC and associated routines
#define NOWINABLE         // Accessibility
#define NOWINDOWSTATION   // Window Station
#define NOWINOFFSETS      // GWL_, GCL_*, associated routines
#define NOWINSTYLES       // WS_, CS_*, ES_*, LBS_*, SBS_*, CBS_*
#undef  OEMRESOURCE       // OEM Resource values

#include <Windows.h>
#pragma endregion
#pragma region include <StrSafe.h>
#define STRSAFE_USE_SECURE_CRT 1
#define STRSAFE_NO_CB_FUNCTIONS
#include <StrSafe.h>
#pragma endregion

extern HMODULE g_hModule;
extern CRITICAL_SECTION g_csModule;
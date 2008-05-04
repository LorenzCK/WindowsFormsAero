#include "HookLibrary.h"
#include "MessageWindow.h"

HMODULE          g_hModule = NULL;
CRITICAL_SECTION g_csModule;

BOOL APIENTRY DllMain(HMODULE hModule, DWORD fReason, LPVOID lpReserved)
{
	UNREFERENCED_PARAMETER(lpReserved);

	if (fReason == DLL_PROCESS_ATTACH)
	{
		g_hModule = hModule;

		InitializeCriticalSection(&g_csModule);
		DisableThreadLibraryCalls(hModule);
	}
	else if (fReason == DLL_PROCESS_DETACH)
	{
		DeleteCriticalSection(&g_csModule);
	}


	return TRUE;
}


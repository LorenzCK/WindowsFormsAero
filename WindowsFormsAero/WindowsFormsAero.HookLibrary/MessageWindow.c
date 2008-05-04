#include <Intrin.h>
#pragma intrinsic(_ReadWriteBarrier)

#include "HookLibrary.h"
#include "MessageWindow.h"
#include "ParseHWND.h"

static volatile BOOL g_fInitialized = FALSE;
static          HWND g_hMsgWindow = NULL;

static LPWSTR AllocString(SIZE_T cch)
{
	return HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, sizeof(WCHAR) * cch);
}

static LPWSTR ReAllocString(LPWSTR lpsz, SIZE_T cch)
{
	return HeapReAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, lpsz, sizeof(WCHAR) * cch);
}

static BOOL FreeString(LPCWSTR lpsz)
{
	return HeapFree(GetProcessHeap(), 0, (LPVOID)(lpsz));
}
static LPCWSTR GetModuleName(SIZE_T* cchModuleName)
{
	LPWSTR szModuleName = AllocString(*cchModuleName);
	
	while ((szModuleName != NULL) &&
		   (GetModuleFileNameW(g_hModule, szModuleName, *cchModuleName) == *cchModuleName) &&
		   (GetLastError() == ERROR_INSUFFICIENT_BUFFER))
	{
		*cchModuleName *= 2;
		szModuleName = ReAllocString(szModuleName, *cchModuleName);
	}

	return szModuleName;
}

static LPCWCH FindLast(LPCWSTR psz, SIZE_T cchMax, WCHAR ch)
{
	if (psz)
	{
		size_t cch = 0;

		if (SUCCEEDED(StringCchLengthW(psz, cchMax, &cch)) && (cch > 0))
		{
			LPCWCH pch = psz + cch;

			while ((pch != psz) && (*pch != ch))
			{
				--pch;
			}

			if (*pch == ch)
			{
				return pch;
			}
		}
	}

	return NULL;
}
static HWND FindMsgWindow()
{
	HWND    Result = NULL;
	
	SIZE_T  cchModuleName = MAX_PATH;
	LPCWSTR szModuleName = GetModuleName(&cchModuleName);

	if (szModuleName)
	{
		LPCWCH pchDash = FindLast(szModuleName, cchModuleName, L'-');

		if (pchDash)
		{
			Result = ParseHWND(pchDash);
		}

		FreeString(szModuleName);
	}

	return Result;
}


HWND MsgWindow()
{
	if (!g_fInitialized)
	{
		EnterCriticalSection(&g_csModule);

		if (!g_fInitialized)
		{
			g_hMsgWindow = FindMsgWindow();
			_ReadWriteBarrier();

			g_fInitialized = TRUE;
		}

		LeaveCriticalSection(&g_csModule);
	}

	return g_hMsgWindow;
}
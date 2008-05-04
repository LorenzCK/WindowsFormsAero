#include "HookLibrary.h"
#include "MessageWindow.h"

__declspec(dllexport) LRESULT CALLBACK KeyboardProc(INT32 nCode, WPARAM wParam, LPARAM lParam)
{
	HWND hWnd = MsgWindow();

	if (hWnd && (nCode == HC_ACTION))
	{
		UINT Msg = WM_KEYDOWN;

		if (lParam & KF_UP)
		{
			Msg = WM_KEYUP;
		}
		
		PostMessage(hWnd, Msg, wParam, lParam);
	}

	return CallNextHookEx(NULL, nCode, wParam, lParam);
}
#include "HookLibrary.h"
#include "ParseHWND.h"

static BOOL IsInRange(WCHAR ch, WCHAR low, WCHAR high)
{
	return ((ch >= low) && (ch <= high));
}

static UINT GetNibble(WCHAR ch)
{
	if (IsInRange(ch, L'0', L'9'))
	{
		return ch - '0';
	}

	if (IsInRange(ch, L'a', L'f'))
	{
		return 0xa + (ch - 'a');
	}

	if (IsInRange(ch, L'A', L'F'))
	{
		return 0xA + (ch - 'A');
	}

	return (UINT)(-1);
}

static BOOL IsNibbleChar(LPCWCH pch)
{
	return pch && *pch && 
		((IsInRange(*pch, L'0', L'9')) ||
		 (IsInRange(*pch, L'a', L'f')) ||
		 (IsInRange(*pch, L'A', L'F')));
}


HWND ParseHWND(LPCWCH ch)
{
	if ((*ch) && (*ch == L'-'))
	{
		++ch;
	}

	if ((*ch) && (*(ch + 1)))
	{
		if (*ch == L'0')
		{
			++ch;

			if ((*ch == L'x') || (*ch == L'X'))
			{
				++ch;
			}
		}
	}

	if (*ch)
	{
		INT_PTR Result = 0;
		BYTE    BitsRemaining = 8 * sizeof(INT_PTR);

		while (IsNibbleChar(ch) && (BitsRemaining > 0))
		{
			Result          = (Result << 4) | GetNibble(*ch);
			BitsRemaining  -= 4;

			++ch;
		}

		return (HWND)(Result);
	}

	return NULL;
}
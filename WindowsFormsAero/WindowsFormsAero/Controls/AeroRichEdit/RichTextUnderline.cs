using System;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    [Serializable]
    public enum RichTextUnderline
    {
        None = TomConstants.None,
        Single = TomConstants.Single,
        Double = TomConstants.Double,
        Dotted = TomConstants.Dotted,
        Wave = TomConstants.Wave,
        Words = TomConstants.Words,
    }
}

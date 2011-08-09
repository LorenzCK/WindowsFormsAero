using System;
using System.Diagnostics.CodeAnalysis;

namespace WindowsFormsAero
{
    [Serializable]
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum AeroProgressBarStatus
    {
        Normal = 1,
        Failed = 2,
        Paused = 3,
    }
}

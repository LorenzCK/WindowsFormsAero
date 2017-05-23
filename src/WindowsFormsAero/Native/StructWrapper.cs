/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.Native {

    /// <summary>
    /// Class wrapping a managed struct that is duplicated onto the unmanaged
    /// heap and correctly destroyed when disposed or finalized.
    /// </summary>
    internal class StructWrapper<T> : IDisposable
        where T : struct {

        public IntPtr Ptr { get; private set; }

        public StructWrapper(T s) {
            Ptr = Marshal.AllocHGlobal(Marshal.SizeOf(s));
            Marshal.StructureToPtr(s, Ptr, false);
        }

        ~StructWrapper() {
            if (Ptr != IntPtr.Zero) {
                Marshal.DestroyStructure(Ptr, typeof(T));
                Marshal.FreeHGlobal(Ptr);
                Ptr = IntPtr.Zero;
            }
        }

        public void Dispose() {
            Marshal.DestroyStructure(Ptr, typeof(T));
            Marshal.FreeHGlobal(Ptr);
            Ptr = IntPtr.Zero;

            GC.SuppressFinalize(this);
        }

        public static implicit operator IntPtr(StructWrapper<T> w) {
            return w.Ptr;
        }

    }

}

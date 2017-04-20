/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Runtime.Serialization;

namespace WindowsFormsAero.Dwm {

    [Serializable]
    class DwmCompositionException : Exception {

        public DwmCompositionException(string m)
            : base(m) {
        }

        public DwmCompositionException(string m, Exception innerException)
            : base(m, innerException) {
        }

        public DwmCompositionException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }

    }

}

/*****************************************************
 *            Vista Controls for .NET 2.0
 * 
 * http://www.codeplex.com/vistacontrols
 * 
 * @author: Lorenz Cuno Klopfenstein
 * Licensed under Microsoft Community License (Ms-CL)
 * 
 *****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace VistaControls.DWM
{
    [Serializable]
    class DWMCompositionException : Exception
    {
        public DWMCompositionException(string m)
            : base(m) {
        }

        public DWMCompositionException(string m, Exception innerException)
            : base(m, innerException) {
        }

        public DWMCompositionException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}

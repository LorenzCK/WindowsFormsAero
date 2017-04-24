using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAero.Native {

    internal enum SystemErrorCode : int {
        Success = 0,
        ErrorInvalidFunction = 1,
        ErrorInsufficientBuffer = 122,
        AppModelErrorNoPackage = 15700,
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public enum ErrorType
    {
        Failure,
        Validation,
        NotFound,
        Conflict,
        Problem,
        Unauthorized,
        Forbidden
    }
}

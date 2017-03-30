using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static Exception RequiredPropertyValidationError(string propertyName)
        {
            throw new ArgumentException("Value cannot be null", propertyName);
        }    

        internal static Exception MaxLengthPropertyValidationError(string propertyName, int actualLength, uint allowedLength)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has length {1} but the maximum allowed length is {2}", 
                propertyName, actualLength, allowedLength));
        }

        internal static Exception MinLengthPropertyValidationError(string propertyName, int actualLength, uint allowedLength)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has length {1} but the minimum allowed length is {2}",
                propertyName, actualLength, allowedLength));
        }
    }
}

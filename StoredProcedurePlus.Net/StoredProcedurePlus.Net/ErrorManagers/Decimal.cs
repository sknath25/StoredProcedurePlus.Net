using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static Exception MaxValuePropertyValidationError(string propertyName, decimal actualValue, decimal allowedValue)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has value {1} but the maximum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }

        internal static Exception MinValuePropertyValidationError(string propertyName, decimal actualValue, decimal allowedValue)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has value {1} but the minimum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }
    }
}

using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
       internal static Exception ListPropertiesAreReadOnlyError(string parametername)
        {
            throw new ArgumentException(
               string.Format("The value of {0} is read only", parametername));
        }
    }
}

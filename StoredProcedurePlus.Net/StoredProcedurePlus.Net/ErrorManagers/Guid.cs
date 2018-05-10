using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static void CannotSetNullToNotNullableGuidProperty(string propertyName)
        {
            throw new InvalidOperationException("Cannot set null value into non nullable Guid property : " + propertyName);
        }
    }
}

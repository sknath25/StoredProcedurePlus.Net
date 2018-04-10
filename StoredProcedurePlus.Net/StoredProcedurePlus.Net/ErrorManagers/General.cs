using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static void CannotCreateInstanceOfTypeNull()
        {
            throw new NullReferenceException("Cannot create instance from null type");
        }
    }
}

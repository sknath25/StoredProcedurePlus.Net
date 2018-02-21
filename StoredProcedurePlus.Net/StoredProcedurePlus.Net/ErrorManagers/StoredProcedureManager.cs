using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static Exception WrongReturnTypeExpected(Type expectedType, Type actualType)
        {
            throw new ArgumentException(
                string.Format(
                    "Return expected of type {0} but configured type for this resultset found should be {1}",
                expectedType.Name, 
                actualType.Name));
        }

        internal static void PrepareDidnotCalled()
        {
            throw new InvalidOperationException("Configurations were not prepared");                          
        }
    }
}

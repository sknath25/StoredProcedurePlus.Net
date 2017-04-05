using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static Exception WrongEntityConfigurationType(Type expectedType, Type actualType)
        {
            throw new ArgumentException(
                string.Format("Entity configuration expected for type {0} but actual was {1}",
                expectedType.Name, actualType.Name));
        }
    }
}

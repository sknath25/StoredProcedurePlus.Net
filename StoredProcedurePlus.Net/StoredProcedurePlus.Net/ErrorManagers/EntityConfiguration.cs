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

        internal static Exception NestedTypeAsDatTableError(string parametername, string childtableaspropertyname)
        {
            throw new ArgumentException(
                string.Format("Parameter : {0}, which is already marked as table type input cannot contain another table type : {1} in it.",
                parametername, childtableaspropertyname));
        }
    }
}

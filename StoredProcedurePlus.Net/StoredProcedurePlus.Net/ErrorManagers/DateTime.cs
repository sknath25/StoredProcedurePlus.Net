using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static void MaxDatePropertyValidationError(string propertyName, DateTime actualValue, DateTime allowedValue)
        {
            throw new InvalidOperationException(
                 string.Format("The value of {0} has value {1} but the maximum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }

        internal static void MinDatePropertyValidationError(string propertyName, DateTime actualValue, DateTime allowedValue)
        {
            throw new InvalidOperationException(
                string.Format("The value of {0} has value {1} but the minimum allowed value is {2}",
               propertyName, actualValue, allowedValue));
        }
        internal static void DateNotAllowedError(string propertyName, DateTime value, DateTime[] allowedValuesOnly)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into datetime property : " + propertyName + " where allowed values are only " + string.Join(", ", allowedValuesOnly));
        }

        internal static void CannotSetNullToNotNullableDateTimeProperty(string propertyName)
        {
            throw new InvalidOperationException("Cannot set null value into not nullable datetime property : " + propertyName);
        }
    }
}

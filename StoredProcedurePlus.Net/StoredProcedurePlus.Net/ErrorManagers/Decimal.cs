using System;
using System.Configuration;

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
        internal static void CannotSetNullToNotNullableDecimalProperty(string propertyName)
        {
            throw new InvalidOperationException("Cannot set null value into non nullable decimal property : " + propertyName);
        }

        internal static void ValueNotAllowedError(string propertyName, decimal value, decimal[] allowedValuesOnly)
        {
            throw new InvalidOperationException("Cannot set given value " + value  +" into decimal property : " + propertyName + " where allowed values are only " + string.Join(", ", allowedValuesOnly));
        }
    }
}

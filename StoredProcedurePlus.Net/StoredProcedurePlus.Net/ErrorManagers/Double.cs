using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static Exception MaxValuePropertyValidationError(string propertyName, double actualValue, double allowedValue)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has value {1} but the maximum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }

        internal static Exception MinValuePropertyValidationError(string propertyName, double actualValue, double allowedValue)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has value {1} but the minimum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }

        internal static void ValueNotAllowedError(string propertyName, double value, double[] allowedValuesOnly)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into double property : " + propertyName + " where allowed values are only " + string.Join(", ", allowedValuesOnly));
        }


        internal static void CannotSetNullToNotNullableDoubleProperty(string propertyName)
        {
            throw new InvalidOperationException("Cannot set null value into not nullable double property : " + propertyName);
        }
    }
}

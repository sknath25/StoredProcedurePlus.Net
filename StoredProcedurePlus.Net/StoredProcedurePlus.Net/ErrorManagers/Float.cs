using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static Exception MaxValuePropertyValidationError(string propertyName, float actualValue, float allowedValue)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has value {1} but the maximum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }

        internal static Exception MinValuePropertyValidationError(string propertyName, float actualValue, float allowedValue)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has value {1} but the minimum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }

        internal static void ValueNotAllowedError(string propertyName, float value, float[] allowedValuesOnly)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into float property : " + propertyName + " where allowed values are only " + string.Join(", ", allowedValuesOnly));
        }

        internal static void CannotSetNullToNotNullableFloatProperty(string propertyName)
        {
            throw new InvalidOperationException("Cannot set null value into not nullable float property : " + propertyName);
        }
    }
}

﻿using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        #region MaxValuePropertyValidationError
        internal static Exception MaxValuePropertyValidationError(string propertyName, int actualValue, int allowedValue)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has value {1} but the maximum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }
        #endregion

        #region MinValuePropertyValidationError
        internal static Exception MinValuePropertyValidationError(string propertyName, int actualValue, int allowedValue)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has value {1} but the minimum allowed value is {2}",
                propertyName, actualValue, allowedValue));
        }
        #endregion

        #region CannotSetNullToNotNullableIntProperty
        internal static void CannotSetNullToNotNullableIntProperty(string propertyName)
        {
            throw new InvalidOperationException("Cannot set null value into non nullable integer property : " + propertyName);
        }
        #endregion

        #region ValueNotAllowedError
        internal static void ValueNotAllowedError(string propertyName, int value, int[] allowedValuesOnly)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into property : " + propertyName + " where allowed values are only " + string.Join(", ", allowedValuesOnly));
        }
        #endregion
    }
}

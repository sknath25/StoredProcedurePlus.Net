using System;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static Exception RequiredPropertyValidationError(string propertyName)
        {
            throw new ArgumentException("Value cannot be null", propertyName);
        }    

        internal static Exception MaxLengthPropertyValidationError(string propertyName, int actualLength, uint allowedLength)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has length {1} but the maximum allowed length is {2}", 
                propertyName, actualLength, allowedLength));
        }

        internal static Exception MinLengthPropertyValidationError(string propertyName, int actualLength, uint allowedLength)
        {
            throw new ArgumentException(
                string.Format("The value of {0} has length {1} but the minimum allowed length is {2}",
                propertyName, actualLength, allowedLength));
        }

        internal static void ValueNotAllowedError(string propertyName, string value, string[] allowedValuesOnly)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into property : " + propertyName + " where allowed values are only " + string.Join(", ", allowedValuesOnly));
        }

        internal static void PatternMathcingFailed(string propertyName, string value, string stringPattern)
        {
            throw new InvalidOperationException("Cannot set given value " + value + " into property : " + propertyName + " where allowed pattern is only " + stringPattern);
        }

        internal static void PatternMathcingTimeOut(string propertyName, string value, string stringPattern, double paternTimeOut)
        {
            throw new InvalidOperationException(
                "Cannot set given value " + value 
                + " into property : "
                + propertyName
                + " Pattern matching timed out for given timeout " 
                + paternTimeOut 
                + "ms against the pattern : " 
                + stringPattern);
        }

        internal static void PatternMatchingError(string propertyName, string value, string stringPattern, Exception ex)
        {
            throw new InvalidOperationException(
                "Cannot set given value " + value
                + " into property : "
                + propertyName
                + " Pattern matching error "
                + ex.Message
                + ", against the pattern : "
                + stringPattern);
        }

        internal static void PatternMatchingError(string propertyName, string value, string patternName)
        {
            throw new InvalidOperationException(
                "Cannot set given value " + value
                + " into property : "
                + propertyName
                + ", against the pattern : "
                + patternName);
        }

        internal static void DbToPropertyCastErrorForString(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to string property " + propertyName);
        }

        internal static void DbToPropertyCastErrorForBool(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to boolean property " + propertyName);
        }

        internal static void DbToPropertyCastErrorForShort(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to short property " + propertyName);
        }

        internal static void DbToPropertyCastErrorForInt(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to integer property " + propertyName);
        }

        internal static void DbToPropertyCastErrorForLong(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to long integer property " + propertyName);
        }

        internal static void DbToPropertyCastErrorForFloat(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to float property " + propertyName);
        }

        internal static void DbToPropertyCastErrorForDouble(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to double property " + propertyName);
        }

        internal static void DbToPropertyCastErrorForDecimal(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to decimal property " + propertyName);
        }

        internal static void DbToPropertyCastErrorForDate(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to date property " + propertyName);
        }

        internal static void CastErrorToVarBinaryForProperty(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to var binary property " + propertyName);
        }

        internal static void CastErrorToGuidForProperty(string propertyName)
        {
            throw new InvalidOperationException("Conversipn failed while setting database value to GUID property " + propertyName);
        }
    }
}

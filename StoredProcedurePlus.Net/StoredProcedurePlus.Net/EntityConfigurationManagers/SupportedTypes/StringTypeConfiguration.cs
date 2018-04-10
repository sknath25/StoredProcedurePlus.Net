using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes
{
    public class StringTypeConfiguration<TContainerType> : PrimitiveTypeConfiguration<TContainerType,string> where TContainerType : class
    {
        public StringTypeConfiguration(Expression<Func<TContainerType, string>> memberSelector):base(memberSelector)
        {

        }

        internal override SqlDbType GetDbType
        {
            get
            {
                if (IsNVarChar)
                {
                    return SqlDbType.NVarChar;
                }
                return SqlDbType.VarChar;
            }
        }

        protected override string Validate(string value)
        {
            if (IsRequired && value==null) Error.RequiredPropertyValidationError(PropertyName);

            if (value != null)
            {
                if (StringPattern != null && StringPattern.Length > 0)
                {
                    try
                    {
                        bool IsMatch = Regex.IsMatch(
                            value,
                            StringPattern,
                            Pattern_Option,
                            TimeSpan.FromMilliseconds(PaternTimeOut)
                        );

                        if (!IsMatch)
                        {
                            Error.PatternMathcingFailed(PropertyName, value, StringPattern);
                        }
                    }
                    catch (RegexMatchTimeoutException)
                    {
                        Error.PatternMathcingTimeOut(PropertyName, value, StringPattern, PaternTimeOut);
                    }
                    catch (Exception ex)
                    {
                        if (DefinedPattern == PreDefinedPatterns.None)
                        {
                            Error.PatternMatchingError(PropertyName, value, StringPattern, ex);
                        }
                        else
                        {
                            Error.PatternMatchingError(PropertyName, value, DefinedPattern.ToString());
                        }
                    }
                }

                int Length = value.Length;

                if (AllowedMaxLength.HasValue && Length > AllowedMaxLength) Error.MaxLengthPropertyValidationError(PropertyName, Length, AllowedMaxLength.Value);
                if (AllowedMinLength.HasValue && Length < AllowedMinLength) Error.MinLengthPropertyValidationError(PropertyName, Length, AllowedMinLength.Value);

                if (AllowedValuesOnly != null && AllowedValuesOnly.Length > 0)
                {
                    if (!Array.Exists<string>(AllowedValuesOnly, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value, AllowedValuesOnly);
                }

                if (AllowedValuesExcept != null && AllowedValuesExcept.Length > 0)
                {
                    if (Array.Exists<string>(AllowedValuesExcept, v => v.Equals(value)))
                        Error.ValueNotAllowedError(PropertyName, value, AllowedValuesExcept);
                }
                
                if(IsTrim)
                {
                    value = value.Trim();
                }
                else
                {
                    if(IsLTrim)
                    {
                        value = value.TrimStart();
                    }

                    if (IsRTrim)
                    {
                        value = value.TrimEnd();
                    }
                }
            }

            return base.Validate(value);
        }

        public StringTypeConfiguration<TContainerType> Out()
        {
            IsOut = true;
            return this;
        }

        public StringTypeConfiguration<TContainerType> HasParameterName(string name)
        {
            ParameterName = name;
            return this;
        }

        bool IsNVarChar = false;
        public StringTypeConfiguration<TContainerType> AsNVarChar()
        {
            IsNVarChar = true;
            return this;
        }

        internal bool IsRequired { get; private set; }
        public StringTypeConfiguration<TContainerType> Required()
        {
            IsRequired = true;
            return this;
        }

        uint? AllowedMaxLength = null;
        public StringTypeConfiguration<TContainerType> MaxLength(uint value)
        {
            AllowedMaxLength = value;
            Size1 = value;
            return this;
        }

        uint? AllowedMinLength = null;
        public StringTypeConfiguration<TContainerType> MinLength(uint value)
        {
            AllowedMinLength = value;
            return this;
        }

        string[] AllowedValuesOnly = null;
        public StringTypeConfiguration<TContainerType> AllowedOnly(string[] values)
        {
            AllowedValuesOnly = values;
            return this;
        }

        string[] AllowedValuesExcept = null;
        public StringTypeConfiguration<TContainerType> AllowedExcept(string[] values)
        {
            AllowedValuesExcept = values;
            return this;
        }

        internal bool IsTrim = false;
        public StringTypeConfiguration<TContainerType> Trim()
        {
            IsTrim = true;
            return this;
        }

        internal bool IsLTrim = false;
        public StringTypeConfiguration<TContainerType> LTrim()
        {
            IsLTrim = true;
            return this;
        }

        internal bool IsRTrim = false;
        public StringTypeConfiguration<TContainerType> RTrim()
        {
            IsRTrim = true;
            return this;
        }

        #region Pattern validation

        string StringPattern = null;
        public StringTypeConfiguration<TContainerType> Pattern(string regX)
        {
            StringPattern = regX;
            return this;
        }

        double PaternTimeOut = 200; //Default
        public StringTypeConfiguration<TContainerType> PatternMatchingTimeout(double timeout)
        {
            PaternTimeOut = timeout;
            return this;
        }

        RegexOptions Pattern_Option = RegexOptions.IgnoreCase; //Default
        public StringTypeConfiguration<TContainerType> PatternOption(RegexOptions option)
        {
            Pattern_Option = option;
            return this;
        }

        enum PreDefinedPatterns
        {
            None,
            Email,
            Ftp,
            Website
        }

        PreDefinedPatterns DefinedPattern = PreDefinedPatterns.None;

        public StringTypeConfiguration<TContainerType> Email()
        {
            DefinedPattern = PreDefinedPatterns.Email;
            StringPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            return this;
        }

        public StringTypeConfiguration<TContainerType> Website()
        {
            DefinedPattern = PreDefinedPatterns.Website;
            StringPattern = @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            return this;
        }

        public StringTypeConfiguration<TContainerType> Ftpsite()
        {
            DefinedPattern = PreDefinedPatterns.Ftp;
            StringPattern = @"(ftp):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            return this;
        }
        #endregion
    }
}

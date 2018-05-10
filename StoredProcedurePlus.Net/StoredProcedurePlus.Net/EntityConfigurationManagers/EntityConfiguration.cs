using StoredProcedurePlus.Net.EntityConfigurationManagers;
using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using StoredProcedurePlus.Net.EntityConfigurationManagers.SupportedTypes;
using StoredProcedurePlus.Net.EntityManagers;
using StoredProcedurePlus.Net.ErrorManagers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class ParameterInputEntityConfiguration<TContainerType> : EntityConfiguration<TContainerType> where TContainerType : class
    {
    }

    public class TvpParameterInputEntityConfiguration<TContainerType> : EntityConfiguration<TContainerType> where TContainerType : class
    {
        internal override void Initialize()
        {
            base.InitializePropertyConfigurations(true);
        }
    }

    internal interface IHasDefaultConstructor
    {
        object CreateNewDefaultInstance();
    }

    public class OutputEntityConfiguration<TContainerType> : EntityConfiguration<TContainerType>, IHasDefaultConstructor where TContainerType : class, new()
    {
        object IHasDefaultConstructor.CreateNewDefaultInstance()
        {
            return new TContainerType();
        }
    }

    public class EntityConfiguration<TContainerType> : NonPrimitiveEntityConfiguration where TContainerType : class
    {
        #region Private

        private class OrdinalProxy : EntityOrdinalConfiguration
        {
            internal OrdinalProxy(List<PropertyConfiguration> parameters, IDataEntityAdapter record) : base(parameters, record) { }
        }

        private class DbParameterEntityAdapterProxy : DbParameterEntityAdapter
        {
            internal DbParameterEntityAdapterProxy(List<PropertyConfiguration> configurations) : base(configurations)
            {

            }
        }

        private class DbDataEntityAdapterProxy : DbDataEntityAdapter
        {
            internal DbDataEntityAdapterProxy(IDataReader record) : base(record)
            {

            }
        }

        private readonly List<PropertyConfiguration> Configurations = new List<PropertyConfiguration>();

        private static LambdaExpression BuildExpression(Type sourceType, PropertyInfo propertyInfo)
        {
            var parameter = Expression.Parameter(sourceType, propertyInfo.Name);
            var property = Expression.Property(parameter, propertyInfo);
            var funcType = typeof(Func<,>).MakeGenericType(sourceType, propertyInfo.PropertyType);
            var lambda = Expression.Lambda(funcType, property, parameter);
            return lambda;
        }

        private void AddMapping(PropertyConfiguration configuration)
        {
            Configurations.Remove(configuration);//IT will only check agaist property name. 
            Configurations.Add(configuration);
        }

        #endregion


        #region Private Static : Prime Types
        static readonly Type type_bool = typeof(bool);
        static readonly Type type_nbool = typeof(bool?);
        static readonly Type type_short = typeof(short);
        static readonly Type type_nshort = typeof(short?);
        static readonly Type type_int = typeof(int);
        static readonly Type type_nint = typeof(int?);
        static readonly Type type_long = typeof(long);
        static readonly Type type_nlong = typeof(long?);
        static readonly Type type_float = typeof(float);
        static readonly Type type_nfloat = typeof(float?);
        static readonly Type type_double = typeof(double);
        static readonly Type type_ndouble = typeof(double?);
        static readonly Type type_decimal = typeof(decimal);
        static readonly Type type_ndecimal = typeof(decimal?);
        static readonly Type type_datetime = typeof(DateTime);
        static readonly Type type_ndatetime = typeof(DateTime?);
        static readonly Type type_bytearray = typeof(byte[]);
        static readonly Type type_string = typeof(string);
        static readonly Type type_guid = typeof(Guid);
        static readonly Type type_nguid = typeof(Guid?);

        #endregion

        protected override void InitializePropertyConfigurations(bool IsIncludeUnmappedProperties)
        {
            SourceType = typeof(TContainerType);

            PropertyInfo[] Properties = SourceType.GetProperties();

            if (IsIncludeUnmappedProperties)
            {
                for (int i = 0; i < Properties.Length; i++)
                {
                    if (!Configurations.Exists(v => v.PropertyName == Properties[i].Name))
                    {
                        if (Properties[i].PropertyType == type_bool)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, bool>>)l);
                        }
                        if (Properties[i].PropertyType == type_nbool)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, bool?>>)l);
                        }
                        if (Properties[i].PropertyType == type_short)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, short>>)l);
                        }
                        if (Properties[i].PropertyType == type_nshort)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, short?>>)l);
                        }
                        if (Properties[i].PropertyType == type_int)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, int>>)l);
                        }
                        if (Properties[i].PropertyType == type_nint)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, int?>>)l);
                        }
                        if (Properties[i].PropertyType == type_long)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, long>>)l);
                        }
                        if (Properties[i].PropertyType == type_nlong)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, long?>>)l);
                        }
                        if (Properties[i].PropertyType == type_float)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, float>>)l);
                        }
                        if (Properties[i].PropertyType == type_nfloat)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, float?>>)l);
                        }
                        if (Properties[i].PropertyType == type_double)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, double>>)l);
                        }
                        if (Properties[i].PropertyType == type_ndouble)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, double?>>)l);
                        }
                        if (Properties[i].PropertyType == type_decimal)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, decimal>>)l);
                        }
                        if (Properties[i].PropertyType == type_ndecimal)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, decimal?>>)l);
                        }
                        if (Properties[i].PropertyType == type_datetime)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, DateTime>>)l);
                        }
                        if (Properties[i].PropertyType == type_ndatetime)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, DateTime?>>)l);
                        }
                        if (Properties[i].PropertyType == type_string)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, string>>)l);
                        }
                        if (Properties[i].PropertyType == type_bytearray)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, byte[]>>)l);
                        }
                        if (Properties[i].PropertyType == type_guid)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, Guid>>)l);
                        }
                        if (Properties[i].PropertyType == type_nguid)
                        {
                            LambdaExpression l = BuildExpression(SourceType, Properties[i]);
                            Maps((Expression<Func<TContainerType, Guid?>>)l);
                        }
                    }
                }
            }

            if (Properties.Length == Configurations.Count)
            {
                for (int i = 0; i < Properties.Length; i++)
                {
                    for (int s = 0; s < Configurations.Count; s++)
                    {
                        if (Configurations[s].PropertyName == Properties[i].Name)
                        {
                            PropertyConfiguration buff = Configurations[i];
                            Configurations[i] = Configurations[s];
                            Configurations[s] = buff;
                        }
                    }
                }
            }

            foreach (var c in Configurations)
            {
                if (c is ListObjectTypeConfiguration<TContainerType> tc && tc.ChildEntityConfiguration != null)
                {
                    tc.ChildEntityConfiguration.Initialize();
                }
            }
        }

        #region Internal

        internal override IDataEntityAdapter GetAsDbParameters()
        {
            return new DbParameterEntityAdapterProxy(Configurations);
        }

        internal EntityConfiguration() { }

        override internal void Prepare(IDataEntityAdapter record)
        {
            OrdinalProvider = new OrdinalProxy(Configurations, record);
        }

        override internal DbDataEntityAdapter GetNewDataAdapter(IDataReader record)
        {
            return new DbDataEntityAdapterProxy(record);
        }

        EntityOrdinalConfiguration OrdinalProvider = null;

        override internal void Set(IDataEntityAdapter fromEntity, object toInstance)
        {
            if (OrdinalProvider == null) Error.PrepareDidnotCalled();

            TContainerType Instance = (TContainerType)toInstance;

            int Ordinal;

            for (int i = 0; i < Configurations.Count; i++)
            {
                PropertyConfiguration configuration = Configurations[i];

                Ordinal = OrdinalProvider[configuration.PropertyName];

                if (configuration.DataType == type_bool)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableBooleanProperty(configuration.PropertyName);
                    }
                    else
                    {
                        BoolTypeConfiguration<TContainerType> Configuration = configuration as BoolTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetBool(Ordinal);
                    }
                }
                else if (configuration.DataType == type_nbool)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        BoolTypeNullableConfiguration<TContainerType> Configuration = configuration as BoolTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        BoolTypeNullableConfiguration<TContainerType> Configuration = configuration as BoolTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetBool(Ordinal);
                    }
                }
                else if (configuration.DataType == type_short)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableShortProperty(configuration.PropertyName);
                    }
                    else
                    {
                        ShortTypeConfiguration<TContainerType> Configuration = configuration as ShortTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetShort(Ordinal);
                    }
                }
                else if (configuration.DataType == type_nshort)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        ShortTypeNullableConfiguration<TContainerType> Configuration = configuration as ShortTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        ShortTypeNullableConfiguration<TContainerType> Configuration = configuration as ShortTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetShort(Ordinal);
                    }
                }
                else if (configuration.DataType == type_int)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableIntProperty(configuration.PropertyName);
                    }
                    else
                    {
                        IntegerTypeConfiguration<TContainerType> Configuration = configuration as IntegerTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetInt(Ordinal);
                    }
                }
                else if (configuration.DataType == type_nint)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        IntegerTypeNullableConfiguration<TContainerType> Configuration = configuration as IntegerTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        IntegerTypeNullableConfiguration<TContainerType> Configuration = configuration as IntegerTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetInt(Ordinal);
                    }
                }
                else if (configuration.DataType == type_long)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableLongProperty(configuration.PropertyName);
                    }
                    else
                    {
                        LongTypeConfiguration<TContainerType> Configuration = configuration as LongTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetLong(Ordinal);
                    }
                }
                else if (configuration.DataType == type_nlong)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        LongTypeNullableConfiguration<TContainerType> Configuration = configuration as LongTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        LongTypeNullableConfiguration<TContainerType> Configuration = configuration as LongTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetLong(Ordinal);
                    }
                }
                else if (configuration.DataType == type_float)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableFloatProperty(configuration.PropertyName);
                    }
                    else
                    {
                        FloatTypeConfiguration<TContainerType> Configuration = configuration as FloatTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetFloat(Ordinal);
                    }
                }
                else if (configuration.DataType == type_nfloat)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        FloatTypeNullableConfiguration<TContainerType> Configuration = configuration as FloatTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        FloatTypeNullableConfiguration<TContainerType> Configuration = configuration as FloatTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetFloat(Ordinal);
                    }
                }
                else if (configuration.DataType == type_double)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDoubleProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DoubleTypeConfiguration<TContainerType> Configuration = configuration as DoubleTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetDouble(Ordinal);
                    }
                }
                else if (configuration.DataType == type_ndouble)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        DoubleTypeNullableConfiguration<TContainerType> Configuration = configuration as DoubleTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        DoubleTypeNullableConfiguration<TContainerType> Configuration = configuration as DoubleTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetDouble(Ordinal);
                    }
                }
                else if (configuration.DataType == type_decimal)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDecimalProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DecimalTypeConfiguration<TContainerType> Configuration = configuration as DecimalTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetDecimal(Ordinal);
                    }
                }
                else if (configuration.DataType == type_ndecimal)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        DecimalTypeNullableConfiguration<TContainerType> Configuration = configuration as DecimalTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        DecimalTypeNullableConfiguration<TContainerType> Configuration = configuration as DecimalTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetDecimal(Ordinal);
                    }
                }
                else if (configuration.DataType == type_datetime)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableDateTimeProperty(configuration.PropertyName);
                    }
                    else
                    {
                        DateTimeTypeConfiguration<TContainerType> Configuration = configuration as DateTimeTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetDate(Ordinal);
                    }
                }
                else if (configuration.DataType == type_ndatetime)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        DateTimeTypeNullableConfiguration<TContainerType> Configuration = configuration as DateTimeTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        DateTimeTypeNullableConfiguration<TContainerType> Configuration = configuration as DateTimeTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetDate(Ordinal);
                    }
                }
                else if (configuration.DataType == type_string)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        StringTypeConfiguration<TContainerType> Configuration = configuration as StringTypeConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        StringTypeConfiguration<TContainerType> Configuration = configuration as StringTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetString(Ordinal);
                    }
                }
                else if (configuration.DataType == type_bytearray)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        VarBinaryTypeConfiguration<TContainerType> Configuration = configuration as VarBinaryTypeConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        VarBinaryTypeConfiguration<TContainerType> Configuration = configuration as VarBinaryTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetBinary(Ordinal);
                    }
                }
                else if (configuration.DataType == type_guid)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        Error.CannotSetNullToNotNullableGuidProperty(configuration.PropertyName);
                    }
                    else
                    {
                        UniqueIdentifierTypeConfiguration<TContainerType> Configuration = configuration as UniqueIdentifierTypeConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetGuid(Ordinal);
                    }
                }
                else if (configuration.DataType == type_nguid)
                {
                    if (fromEntity.IsDBNull(Ordinal))
                    {
                        UniqueIdentifierTypeNullableConfiguration<TContainerType> Configuration = configuration as UniqueIdentifierTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = null;
                    }
                    else
                    {
                        UniqueIdentifierTypeNullableConfiguration<TContainerType> Configuration = configuration as UniqueIdentifierTypeNullableConfiguration<TContainerType>;
                        Configuration[Instance] = fromEntity.GetGuid(Ordinal);
                    }
                }
            }
        }

        override internal void Get(object fromInstance, IDataEntityAdapter toEntity)
        {
            if (OrdinalProvider == null) Error.PrepareDidnotCalled();

            TContainerType Instance = (TContainerType)fromInstance;

            for (int i = 0; i < Configurations.Count; i++)
            {
                PropertyConfiguration configuration = Configurations[i];

                int Ordinal = OrdinalProvider[configuration.PropertyName];
                if (configuration.DataType == type_bool)
                {
                    BoolTypeConfiguration<TContainerType> Configuration = configuration as BoolTypeConfiguration<TContainerType>;
                    toEntity.SetBool(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_nbool)
                {
                    BoolTypeNullableConfiguration<TContainerType> Configuration = configuration as BoolTypeNullableConfiguration<TContainerType>;
                    toEntity.SetBool(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_short)
                {
                    ShortTypeConfiguration<TContainerType> Configuration = configuration as ShortTypeConfiguration<TContainerType>;
                    toEntity.SetShort(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_nshort)
                {
                    ShortTypeNullableConfiguration<TContainerType> Configuration = configuration as ShortTypeNullableConfiguration<TContainerType>;
                    toEntity.SetShort(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_int)
                {
                    IntegerTypeConfiguration<TContainerType> Configuration = configuration as IntegerTypeConfiguration<TContainerType>;
                    toEntity.SetInt(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_nint)
                {
                    IntegerTypeNullableConfiguration<TContainerType> Configuration = configuration as IntegerTypeNullableConfiguration<TContainerType>;
                    toEntity.SetInt(Ordinal, Configuration[Instance]);
                }
                if (configuration.DataType == type_long)
                {
                    LongTypeConfiguration<TContainerType> Configuration = configuration as LongTypeConfiguration<TContainerType>;
                    toEntity.SetLong(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_nlong)
                {
                    LongTypeNullableConfiguration<TContainerType> Configuration = configuration as LongTypeNullableConfiguration<TContainerType>;
                    toEntity.SetLong(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_float)
                {
                    FloatTypeConfiguration<TContainerType> Configuration = configuration as FloatTypeConfiguration<TContainerType>;
                    toEntity.SetFloat(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_nfloat)
                {
                    FloatTypeNullableConfiguration<TContainerType> Configuration = configuration as FloatTypeNullableConfiguration<TContainerType>;
                    toEntity.SetFloat(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_double)
                {
                    DoubleTypeConfiguration<TContainerType> Configuration = configuration as DoubleTypeConfiguration<TContainerType>;
                    toEntity.SetDouble(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_ndouble)
                {
                    DoubleTypeNullableConfiguration<TContainerType> Configuration = configuration as DoubleTypeNullableConfiguration<TContainerType>;
                    toEntity.SetDouble(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_decimal)
                {
                    DecimalTypeConfiguration<TContainerType> Configuration = configuration as DecimalTypeConfiguration<TContainerType>;
                    toEntity.SetDecimal(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_ndecimal)
                {
                    DecimalTypeNullableConfiguration<TContainerType> Configuration = configuration as DecimalTypeNullableConfiguration<TContainerType>;
                    toEntity.SetDecimal(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_datetime)
                {
                    DateTimeTypeConfiguration<TContainerType> Configuration = configuration as DateTimeTypeConfiguration<TContainerType>;
                    toEntity.SetDateTime(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_ndatetime)
                {
                    DateTimeTypeNullableConfiguration<TContainerType> Configuration = configuration as DateTimeTypeNullableConfiguration<TContainerType>;
                    toEntity.SetDateTime(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_string)
                {
                    StringTypeConfiguration<TContainerType> Configuration = configuration as StringTypeConfiguration<TContainerType>;
                    toEntity.SetString(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_bytearray)
                {
                    VarBinaryTypeConfiguration<TContainerType> Configuration = configuration as VarBinaryTypeConfiguration<TContainerType>;
                    toEntity.SetBinary(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_guid)
                {
                    UniqueIdentifierTypeConfiguration<TContainerType> Configuration = configuration as UniqueIdentifierTypeConfiguration<TContainerType>;
                    toEntity.SetGuid(Ordinal, Configuration[Instance]);
                }
                else if (configuration.DataType == type_nguid)
                {
                    UniqueIdentifierTypeNullableConfiguration<TContainerType> Configuration = configuration as UniqueIdentifierTypeNullableConfiguration<TContainerType>;
                    toEntity.SetGuid(Ordinal, Configuration[Instance]);
                }
                else if (configuration.IsEnumerable)
                {
                    ListObjectTypeConfiguration<TContainerType> Configuration = configuration as ListObjectTypeConfiguration<TContainerType>;
                    IList ListObbject = (IList)Configuration[Instance];
                    if (ListObbject == null)
                    {
                        toEntity.SetTable(Ordinal, null, Configuration.TableTypeName);
                    }
                    else
                    {
                        DbParameterEntityAdapter adapter = (DbParameterEntityAdapter)Configuration.ChildEntityConfiguration.GetAsDbParameters();
                        DataTable ListAsDataTable = new DataTable(Configuration.PropertyName);
                        for (int listcounter = 0; listcounter < ListObbject.Count; listcounter++)
                        {
                            if (adapter.FieldCount > 0)
                            {
                                if (listcounter == 0)
                                {
                                    Configuration.ChildEntityConfiguration.Prepare(adapter);
                                    Configuration.ChildEntityConfiguration.Get(ListObbject[listcounter], adapter);

                                    for (int fieldcounter = 0; fieldcounter < adapter.FieldCount; fieldcounter++)
                                    {
                                        var dtype = adapter.GetSourceType(fieldcounter);

                                        if (dtype == typeof(DataTable))
                                        {
                                            throw Error.NestedTypeAsDatTableError(
                                                adapter[fieldcounter].ParameterName,
                                                ((DataTable)adapter[fieldcounter].Value).TableName);
                                        }

                                        if (dtype == type_nbool)
                                            dtype = type_bool;

                                        if (dtype == type_nshort)
                                            dtype = type_short;

                                        if (dtype == type_nint)
                                            dtype = type_int;

                                        if (dtype == type_nlong)
                                            dtype = type_long;

                                        if (dtype == type_nfloat)
                                            dtype = type_float;

                                        if (dtype == type_ndouble)
                                            dtype = type_double;

                                        if (dtype == type_ndecimal)
                                            dtype = type_decimal;

                                        if (dtype == type_ndatetime)
                                            dtype = type_datetime;

                                        if (dtype == type_nguid)
                                            dtype = type_guid;

                                        DataColumn col = new DataColumn(adapter[fieldcounter].ParameterName, dtype);
                                        ListAsDataTable.Columns.Add(col);
                                    }
                                }
                                else
                                {
                                    Configuration.ChildEntityConfiguration.Get(ListObbject[listcounter], adapter);
                                }

                                DataRow r = ListAsDataTable.NewRow();

                                for (int fieldcounter = 0; fieldcounter < adapter.FieldCount; fieldcounter++)
                                {
                                    var name = adapter[fieldcounter].ParameterName;

                                    if (adapter.GetSourceType(fieldcounter) == type_bytearray)
                                    {
                                        r[adapter[fieldcounter].ParameterName] = (byte[])adapter[fieldcounter].Value;
                                    }
                                    else
                                    {
                                        r[adapter[fieldcounter].ParameterName] = adapter[fieldcounter].Value;
                                    }
                                }

                                ListAsDataTable.Rows.Add(r);
                            }
                        }
                        toEntity.SetTable(Ordinal, ListAsDataTable, Configuration.TableTypeName);
                    }
                }
            }
        }

        #endregion

        #region Public
        public BoolTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, bool>> memberSelector)
        {
            BoolTypeConfiguration<TContainerType> Configuration = new BoolTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public BoolTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, bool?>> memberSelector)
        {
            BoolTypeNullableConfiguration<TContainerType> Configuration = new BoolTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public ShortTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, short>> memberSelector)
        {
            ShortTypeConfiguration<TContainerType> Configuration = new ShortTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public ShortTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, short?>> memberSelector)
        {
            ShortTypeNullableConfiguration<TContainerType> Configuration = new ShortTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public IntegerTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, int>> memberSelector)
        {
            IntegerTypeConfiguration<TContainerType> Configuration = new IntegerTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public IntegerTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, int?>> memberSelector)
        {
            IntegerTypeNullableConfiguration<TContainerType> Configuration = new IntegerTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public LongTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, long>> memberSelector)
        {
            LongTypeConfiguration<TContainerType> Configuration = new LongTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public LongTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, long?>> memberSelector)
        {
            LongTypeNullableConfiguration<TContainerType> Configuration = new LongTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public FloatTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, float>> memberSelector)
        {
            FloatTypeConfiguration<TContainerType> Configuration = new FloatTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public FloatTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, float?>> memberSelector)
        {
            FloatTypeNullableConfiguration<TContainerType> Configuration = new FloatTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DoubleTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, double>> memberSelector)
        {
            DoubleTypeConfiguration<TContainerType> Configuration = new DoubleTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DoubleTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, double?>> memberSelector)
        {
            DoubleTypeNullableConfiguration<TContainerType> Configuration = new DoubleTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DecimalTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, decimal>> memberSelector)
        {
            DecimalTypeConfiguration<TContainerType> Configuration = new DecimalTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DecimalTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, decimal?>> memberSelector)
        {
            DecimalTypeNullableConfiguration<TContainerType> Configuration = new DecimalTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DateTimeTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, DateTime>> memberSelector)
        {
            DateTimeTypeConfiguration<TContainerType> Configuration = new DateTimeTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public DateTimeTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, DateTime?>> memberSelector)
        {
            DateTimeTypeNullableConfiguration<TContainerType> Configuration = new DateTimeTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public StringTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, string>> memberSelector)
        {
            StringTypeConfiguration<TContainerType> Configuration = new StringTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public VarBinaryTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, byte[]>> memberSelector)
        {
            VarBinaryTypeConfiguration<TContainerType> Configuration = new VarBinaryTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public UniqueIdentifierTypeConfiguration<TContainerType> Maps(Expression<Func<TContainerType, Guid>> memberSelector)
        {
            UniqueIdentifierTypeConfiguration<TContainerType> Configuration = new UniqueIdentifierTypeConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }
        public UniqueIdentifierTypeNullableConfiguration<TContainerType> Maps(Expression<Func<TContainerType, Guid?>> memberSelector)
        {
            UniqueIdentifierTypeNullableConfiguration<TContainerType> Configuration = new UniqueIdentifierTypeNullableConfiguration<TContainerType>(memberSelector);
            AddMapping(Configuration);
            return Configuration;
        }

        public TvpParameterInputEntityConfiguration<TPropertyType> MapAsTable<TPropertyType>(Expression<Func<TContainerType, List<TPropertyType>>> memberSelector) where TPropertyType : class
        {
            ListObjectTypeConfiguration<TContainerType> Configuration = new ListObjectTypeConfiguration<TContainerType>();
            AddMapping(Configuration);
            TvpParameterInputEntityConfiguration<TPropertyType> c = Configuration.AsTable(memberSelector);
            return c;
        }

        public TvpParameterInputEntityConfiguration<TPropertyType> MapAsTable<TPropertyType>(Expression<Func<TContainerType, List<TPropertyType>>> memberSelector, string typename) where TPropertyType : class
        {
            ListObjectTypeConfiguration<TContainerType> Configuration = new ListObjectTypeConfiguration<TContainerType>();
            AddMapping(Configuration);
            TvpParameterInputEntityConfiguration<TPropertyType> c = Configuration.AsTable(memberSelector, typename);
            return c;
        }

        public TvpParameterInputEntityConfiguration<TPropertyType> MapAsTable<TPropertyType>(Expression<Func<TContainerType, List<TPropertyType>>> memberSelector, string typename, string parametername) where TPropertyType : class
        {
            ListObjectTypeConfiguration<TContainerType> Configuration = new ListObjectTypeConfiguration<TContainerType>();
            AddMapping(Configuration);
            TvpParameterInputEntityConfiguration<TPropertyType> c = Configuration.AsTable(memberSelector, typename, parametername);
            return c;
        }

        #endregion
    }
}
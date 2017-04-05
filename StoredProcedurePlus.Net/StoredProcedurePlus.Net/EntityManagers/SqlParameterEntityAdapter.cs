using StoredProcedurePlus.Net.EntityConfigurationManagers.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StoredProcedurePlus.Net.EntityManagers
{
    internal class SqlParameterEntityAdapter : IDataEntityAdapter
    {
        const string SQLPARAMETERPREFIX = "@";

        readonly Dictionary<int, Tuple<string, SqlParameter>> Parameters;

        protected SqlParameterEntityAdapter(List<PropertyConfiguration> values)
        {
            if (values != null)
            {
                Parameters = new Dictionary<int, Tuple<string, SqlParameter>>();

                for (int i = 0; i < values.Count; i++)
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = string.Concat(SQLPARAMETERPREFIX, values[i].ParameterName);
                    parameter.SqlDbType = values[i].GetSqlDbType();
                    parameter.Direction = values[i].IsOut ? System.Data.ParameterDirection.Output : System.Data.ParameterDirection.Input;
                    Parameters.Add(i, new Tuple<string, SqlParameter>(values[i].PropertyName, parameter));
                }
            }
        }

        internal SqlParameter this[int ordinal]
        {
            get
            {
                return Parameters[ordinal].Item2;
            }
        }




        public int FieldCount => Parameters.Count;

        public string GetName(int ordinal)
        {
            return Parameters[ordinal].Item1;
        }

        public int GetOrdinal(string name)
        {
            for(int i = 0; i< Parameters.Count; i++)
            {
                if (Parameters[i].Item1 == name) return i;
            }

            throw new IndexOutOfRangeException();
        }

        public bool IsDBNull(int ordinal)
        {
            if(Parameters[ordinal].Item2.SqlValue == DBNull.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public decimal GetDecimal(int ordinal)
        {
            return decimal.Parse(Parameters[ordinal].Item2.SqlValue.ToString());
        }

        public double GetDouble(int ordinal)
        {
            return double.Parse(Parameters[ordinal].Item2.SqlValue.ToString());
        }

        public int GetInt(int ordinal)
        {
            return int.Parse(Parameters[ordinal].Item2.SqlValue.ToString());
        }

        public long GetLong(int ordinal)
        {
            return long.Parse(Parameters[ordinal].Item2.SqlValue.ToString());
        }

        public short GetShort(int ordinal)
        {
            return short.Parse(Parameters[ordinal].Item2.SqlValue.ToString());
        }

        public string GetString(int ordinal)
        {
            return Parameters[ordinal].Item2.SqlValue.ToString();
        }

        public void SetDecimal(int ordinal, decimal value)
        {
            Parameters[ordinal].Item2.SqlValue = value;
        }

        public void SetDouble(int ordinal, double value)
        {
            Parameters[ordinal].Item2.SqlValue = value;
        }

        public void SetInt(int ordinal, int value)
        {
            Parameters[ordinal].Item2.SqlValue = value;
        }

        public void SetLong(int ordinal, long value)
        {
            Parameters[ordinal].Item2.SqlValue = value;
        }

        public void SetShort(int ordinal, short value)
        {
            Parameters[ordinal].Item2.SqlValue = value;
        }

        public void SetString(int ordinal, string value)
        {
            if (value == null)
            {
                Parameters[ordinal].Item2.SqlValue = DBNull.Value;
            }
            else
            {
                Parameters[ordinal].Item2.SqlValue = value;
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.EntityManagers
{
    internal class DbDataEntityAdapter : IDataEntityAdapter
    {
        readonly IDataRecord Data;

        protected DbDataEntityAdapter(IDataRecord data)
        {
            Data = data;
        }

        public int FieldCount => Data.FieldCount;

        public decimal GetDecimal(int ordinal)
        {
            return Data.GetDecimal(ordinal);
        }

        public double GetDouble(int ordinal)
        {
            return Data.GetDouble(ordinal);
        }

        public int GetInt(int ordinal)
        {
            return Data.GetInt32(ordinal);
        }

        public long GetLong(int ordinal)
        {
            return Data.GetInt64(ordinal);
        }

        public string GetName(int ordinal)
        {
            return Data.GetString(ordinal);
        }

        public short GetShort(int ordinal)
        {
            return Data.GetInt16(ordinal);
        }

        public string GetString(int ordinal)
        {
            return Data.GetString(ordinal);
        }

        public void SetDecimal(int ordinal, decimal value)
        {
            throw new NotImplementedException();
        }

        public void SetDouble(int ordinal, double value)
        {
            throw new NotImplementedException();
        }

        public void SetInt(int ordinal, int value)
        {
            throw new NotImplementedException();
        }

        public void SetLong(int ordinal, long value)
        {
            throw new NotImplementedException();
        }

        public void SetShort(int ordinal, short value)
        {
            throw new NotImplementedException();
        }

        public void SetString(int ordinal, string value)
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace StoredProcedurePlus.Net.EntityManagers
{
    interface IDataEntityAdapter
    {
        int FieldCount { get; }
        string GetName(int ordinal);
        int GetOrdinal(string name);

        bool IsDBNull(int ordinal);
        string GetString(int ordinal);
        short GetShort(int ordinal);
        int GetInt(int ordinal);
        long GetLong(int ordinal);
        decimal GetDecimal(int ordinal);
        double GetDouble(int ordinal);
        DateTime GetDate(int ordinal);


        void SetString(int ordinal, string value);
        void SetShort(int ordinal, short value);
        void SetInt(int ordinal, int value);
        void SetLong(int ordinal, long value);
        void SetDecimal(int ordinal, decimal value);
        void SetDouble(int ordinal, double value);
        void SetDateTime(int ordinal, DateTime dateTime);
    }
}

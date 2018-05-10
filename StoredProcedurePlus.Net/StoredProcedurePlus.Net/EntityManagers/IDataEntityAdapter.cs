using System;
using System.Data;

namespace StoredProcedurePlus.Net.EntityManagers
{
    public interface IDataEntityAdapter
    {
        int FieldCount { get; }
        Type GetSourceType(int ordinal);
        string GetName(int ordinal);
        int GetOrdinal(string name);

        bool IsDBNull(int ordinal);
        bool GetBool(int ordinal);        
        short GetShort(int ordinal);
        int GetInt(int ordinal);
        long GetLong(int ordinal);
        float GetFloat(int ordinal);
        double GetDouble(int ordinal);
        decimal GetDecimal(int ordinal);
        DateTime GetDate(int ordinal);
        string GetString(int ordinal);
        byte[] GetBinary(int ordinal);
        Guid GetGuid(int ordinal);
        DataTable GetTable(int ordinal);

        void SetBool(int ordinal, bool value);
        void SetBool(int ordinal, bool? value);       
        void SetShort(int ordinal, short value);
        void SetShort(int ordinal, short? value);
        void SetInt(int ordinal, int value);
        void SetInt(int ordinal, int? value);
        void SetLong(int ordinal, long value);
        void SetLong(int ordinal, long? value);
        void SetFloat(int ordinal, float value);
        void SetFloat(int ordinal, float? value);
        void SetDouble(int ordinal, double value);
        void SetDouble(int ordinal, double? value);
        void SetDecimal(int ordinal, decimal value);
        void SetDecimal(int ordinal, decimal? value);
        void SetDateTime(int ordinal, DateTime value);
        void SetDateTime(int ordinal, DateTime? value);
        void SetString(int ordinal, string value);
        void SetBinary(int ordinal, byte[] value);
        void SetGuid(int ordinal, Guid value);
        void SetGuid(int ordinal, Guid? value);
        void SetTable(int ordinal, DataTable value, string declaredTableTypeName);               
    }
}

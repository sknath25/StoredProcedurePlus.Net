using System;
using System.Data;

namespace StoredProcedurePlus.Net.EntityManagers
{
    public class PropertyAsParameterAttribute : Attribute
    {
        public readonly string Name;
        public readonly uint Size = 0;
        public readonly ParameterDirection Direction = ParameterDirection.Input;

        public PropertyAsParameterAttribute(string name)
        {
            Name = name;
        }

        public PropertyAsParameterAttribute(string name, uint size) : this(name)
        {
            Size = size;
        }

        public PropertyAsParameterAttribute(string name, uint size, ParameterDirection direction) : this(name, size)
        {
            Direction = direction;
        }

        public PropertyAsParameterAttribute(string name, ParameterDirection direction) : this(name)
        {
            Direction = direction;
        }
    }
}
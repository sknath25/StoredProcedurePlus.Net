using StoredProcedurePlus.Net.EntityManagers;
using System;
using System.Data;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public abstract class NonPrimitiveEntityConfiguration
    {
        protected NonPrimitiveEntityConfiguration()
        {
            IncludeUnmappedProperties = true;
        }

        internal virtual Type SourceType { get; set; }

        public bool IncludeUnmappedProperties { get; set; }

        internal virtual void Initialize()
        {
            if (!IncludeUnmappedProperties) return;

            InitializePropertyConfigurations();
        }

        protected abstract void InitializePropertyConfigurations();

        abstract internal object GetNewEntity();

        abstract internal void Prepare(IDataEntityAdapter record);

        abstract internal DbDataEntityAdapter GetNewDataAdapter(IDataReader record);

        abstract internal void Set(IDataEntityAdapter fromEntity, object toInstance);

        abstract internal void Get(object fromInstance, IDataEntityAdapter toEntity);
    }
}
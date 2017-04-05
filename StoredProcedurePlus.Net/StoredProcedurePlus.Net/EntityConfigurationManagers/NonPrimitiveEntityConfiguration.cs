using System;

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
    }
}
using StoredProcedurePlus.Net.ConnectionManagers;
using System.Collections.Generic;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public sealed class ProcedureConfiguration<TContainerType> where TContainerType : class
    {
        public string ConnectionStringName { get; set; }
        public string ConnectionString { get; set; }
        public string ProcedureName { get; set; }
        public bool Mock { get; set; }

        internal readonly ConnectionFactory Connection;

        internal List<NonPrimitiveEntityConfiguration> OutputSets;

        private ParameterInputEntityConfiguration<TContainerType> _Input;
        public ParameterInputEntityConfiguration<TContainerType> Input
        {
            get
            {
                return _Input;
            }
        }

        public OutputEntityConfiguration<T> CanReturnCollectionOf<T>() where T : class, new ()
        {
            OutputEntityConfiguration<T> ReturnEntity = new OutputEntityConfiguration<T>();
            OutputSets.Add(ReturnEntity);
            return ReturnEntity;
        }

        public ProcedureConfiguration()
        {
            ConnectionStringName = "DbString"; // Default
            _Input = new ParameterInputEntityConfiguration<TContainerType>();
            OutputSets = new List<NonPrimitiveEntityConfiguration>();
            Connection = new ConnectionFactory();
        }

        internal void Initialize()
        {
            _Input.Initialize();

            for(int i = 0; i < OutputSets.Count; i++ )
            {
                OutputSets[i].Initialize();
            }
        }
    }
}
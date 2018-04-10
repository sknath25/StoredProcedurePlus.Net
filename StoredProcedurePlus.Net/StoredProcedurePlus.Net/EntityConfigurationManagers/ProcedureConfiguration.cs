using StoredProcedurePlus.Net.ConnectionManagers;
using System.Collections.Generic;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public sealed class ProcedureConfiguration<S> where S : class
    {
        public string ConnectionStringName { get; set; }
        public string ConnectionString { get; set; }
        public string ProcedureName { get; set; }
        public bool Mock { get; set; }

        internal readonly ConnectionFactory Connection;

        internal List<NonPrimitiveEntityConfiguration> OutputSets;

        public ParameterInputEntityConfiguration<S> Input;

        public OutputEntityConfiguration<T> CanReturnCollectionOf<T>() where T : class, new ()
        {
            OutputEntityConfiguration<T> ReturnEntity = new OutputEntityConfiguration<T>();
            OutputSets.Add(ReturnEntity);
            return ReturnEntity;
        }

        public ProcedureConfiguration()
        {
            ConnectionStringName = "DbString"; // Default
            Input = new ParameterInputEntityConfiguration<S>();
            OutputSets = new List<NonPrimitiveEntityConfiguration>();
            Connection = new ConnectionFactory();
        }

        internal void Initialize()
        {
            Input.Initialize();

            for(int i = 0; i < OutputSets.Count; i++ )
            {
                OutputSets[i].Initialize();
            }
        }
    }
}
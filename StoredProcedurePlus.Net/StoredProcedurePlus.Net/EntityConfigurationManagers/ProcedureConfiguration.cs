using StoredProcedurePlus.Net.ConnectionManagers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        internal List<TableTypeConfiguration<S>> TablesInputs;

        public OutputEntityConfiguration<T> CanReturnCollectionOf<T>() where T : class, new ()
        {
            OutputEntityConfiguration<T> ReturnEntity = new OutputEntityConfiguration<T>();
            OutputSets.Add(ReturnEntity);
            return ReturnEntity;
        }

        public ParameterInputEntityConfiguration<T> InputTable<T>(Expression<Func<S, IList<T>>> memberSelector) where T : class
        {
            TableTypeConfiguration<S> c = new TableTypeConfiguration<S>();
            var x = c.SetEntityConfiguration(memberSelector);
            TablesInputs.Add(c);
            return x;
        }

        public ProcedureConfiguration()
        {
            ConnectionStringName = "DbString"; // Default
            Input = new ParameterInputEntityConfiguration<S>();
            OutputSets = new List<NonPrimitiveEntityConfiguration>();
            Connection = new ConnectionFactory();
            TablesInputs = new List<TableTypeConfiguration<S>>();
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
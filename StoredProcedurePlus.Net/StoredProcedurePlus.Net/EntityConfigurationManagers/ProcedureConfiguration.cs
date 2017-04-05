using StoredProcedurePlus.Net.ConnectionManagers;
using System;
using System.Collections.Generic;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public sealed class ProcedureConfiguration<S> where S : class
    {
        public string ConnectionStringName { get; set; }

        public string ConnectionString { get; set; }

        public string ProcedureName { get; set; }
      
        internal readonly ConnectionFactory Connection;

        public EntityConfiguration<S> Input;

        internal List<NonPrimitiveEntityConfiguration> OutputSets;

        //internal List<Type> PrimitiveTypes;
        
        public EntityConfiguration<T> CanReturn<T>() where T : class
        {
            EntityConfiguration<T> ReturnEntity = new EntityConfiguration<T>();
            OutputSets.Add(ReturnEntity);
            return ReturnEntity;
        }

        //public void CanReturn<int>()
        //{
        //    //EntityConfiguration<T> ReturnEntity = new EntityConfiguration<T>();
        //    //OutputSets.Add(ReturnEntity);
        //    //return ReturnEntity;


        //}

        public ProcedureConfiguration()
        {
            ConnectionStringName = "DbString"; // Default
            Input = new EntityConfiguration<S>();
            OutputSets = new List<NonPrimitiveEntityConfiguration>();
            Connection = new ConnectionFactory();
        }

        internal void Initialize()
        {
            Input.Initialize();

            foreach(NonPrimitiveEntityConfiguration config in OutputSets)
            {
                config.Initialize();
            }
        }
    }
}
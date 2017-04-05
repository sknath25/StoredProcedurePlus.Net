using CodeFirstStoredProcs;
using EFDrivenSPAccess.DatabaseContext.Models;
using StoredProcedurePlus.Net.UnitTestEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDrivenSPAccess.DatabaseContext
{
    public sealed class PerformanceDbContext_NonVirtual : DbContext
    {
        static PerformanceDbContext_NonVirtual()
        {
            Database.SetInitializer<PerformanceDbContext_NonVirtual>(null);
        }

        public PerformanceDbContext_NonVirtual()
            : base("Name=DbString")
        {
            this.InitializeStoredProcs();

            ResourceSummaryDetails
                .HasName("InsertResourceSummaryDetails")
                .HasOwner("dbo").ReturnsTypes(typeof(ResourceSummaryNonVirtual));

        }

        public StoredProc<ResourceSummary> ResourceSummaryDetails { get; set; }
    }

    public sealed class PerformanceDbContext_Virtual : DbContext
    {
        static PerformanceDbContext_Virtual()
        {
            Database.SetInitializer<PerformanceDbContext_NonVirtual>(null);
        }

        public PerformanceDbContext_Virtual()
            : base("Name=DbString")
        {
            this.InitializeStoredProcs();

            ResourceSummaryDetails
                .HasName("InsertResourceSummaryDetails")
                .HasOwner("dbo").ReturnsTypes(typeof(ResourceSummaryVirtual));

        }

        public StoredProc<ResourceSummary> ResourceSummaryDetails { get; set; }
    }
}

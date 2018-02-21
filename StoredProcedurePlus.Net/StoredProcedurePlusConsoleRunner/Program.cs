using EFDrivenSPAccess.DatabaseContext;
using EFDrivenSPAccess.DatabaseContext.Models;
using StoredProcedurePlus.Net;
using StoredProcedurePlus.Net.UnitTestEntities;
using StoredProcedurePlus.Net.UnitTestEntities.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlusConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo Info;

            do
            {
                Console.WriteLine("Chose the program type (1/2) : ");

                Info = Console.ReadKey();

                switch (Info.Key)
                {
                    case ConsoleKey.NumPad1:
                        RunRetrivalTest1();
                        break;
                    default:
                        RunRetrivalTest2();
                        break;
                }

                Console.WriteLine("Want to perform again ? (Y/N) : ");

                Info = Console.ReadKey();

            } while (Info.Key == ConsoleKey.Y);

            Console.WriteLine("Good bye.");
        }


        static void RunRetrivalTest1()
        {
            ResourceSummary Input = new ResourceSummary();

            IEnumerable<ResourceSummary> Result = null;

            ConsoleKeyInfo KeyInfo;

            do
            {
                try
                {
                    using (ConnectionScope scope = new ConnectionScope())
                    {
                        SpResourceSummaryDetails Sp = new SpResourceSummaryDetails();

                        Sp.Execute(Input, scope);

                        Result = Sp.GetResult<ResourceSummary>();
                    }

                    foreach (ResourceSummary each in Result)
                    {
                        Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", each.PersonId, each.PersonName, each.EmailAddress, each.CTC));
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception : {0}", ex.Message);
                }

                Console.WriteLine("Would you like to perform it again ? (Y/N)");

                KeyInfo = Console.ReadKey();
            }

            while (KeyInfo.Key == ConsoleKey.Y);
        }

        static void RunRetrivalTest2()
        {
            ResourceSummary Input = new ResourceSummary();

            IEnumerable<object> Result = null;

            ConsoleKeyInfo KeyInfo;

            do
            {
                try
                {
                    PerformanceDbContext_NonVirtual DbContext = new PerformanceDbContext_NonVirtual();

                    var x = DbContext.ResourceSummaryDetails.CallStoredProc(Input);

                    Result = x[0].ToArray();

                    foreach (ResourceSummaryNonVirtual each in Result)
                    {
                        Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", each.PersonId, each.PersonName, each.EmailAddress, each.CTC));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception : {0}", ex.Message);
                }

                Console.WriteLine("Would you like to perform it again ? (Y/N)");

                KeyInfo = Console.ReadKey();
            }

            while (KeyInfo.Key == ConsoleKey.Y);
        }
    }
}

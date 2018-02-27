using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.ErrorManagers
{
    internal static partial class Error
    {
        internal static void NoConnectionStringError()
        {
            throw new InvalidOperationException("No connection string provided!. Please check the configurations");
        }

        internal static void NoConnectionNameError()
        {
            throw new InvalidOperationException("No connection string name provided!. Please check the configurations");
        }

        internal static void ConnectionNameMissingError(string connectionStringName)
        {
            throw new InvalidOperationException(
                string.Format("No connection string found in config file having the name {0}", connectionStringName));
        }

        internal static void ConnectionNameMissingError(string connectionStringName, ConfigurationErrorsException ex)
        {
            throw new InvalidOperationException(
               string.Format("Error getting value from connection string name {0}, in file : {1}. Error message {2}", connectionStringName, ex.Filename, ex.Message));
        }

    }
}

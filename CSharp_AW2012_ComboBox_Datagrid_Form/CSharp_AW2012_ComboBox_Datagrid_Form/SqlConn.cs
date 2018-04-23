using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

// // ^^^^  MUST HAVE using System.Data; using System.Data.SqlClient; when doing this

namespace CSharp_AW2012_ComboBox_Datagrid_Form
{
    class SqlConn
    {
        // Declare a constant to hold the connection @"Server=;Database=;Trusted_Connection= (Windows Authentication Y or N)"
        const string varConDefault = @"Server=PL11\MTCDEVDB;Database=AdventureWorks2012;Trusted_Connection=True;User ID=AdvWorks2012;Password=Password123";
        // Declare SQL connection
        SqlConnection varSqlconnect;

        private bool DBConnect(string varConnectionString = "")
        {
            bool returnValue;

            // If no connection string was specified, use the default
            if (varConnectionString.Length == 0)
            {
                varConnectionString = varConDefault;
            }

            try
            {
                // Open the connection to SQL server
                varSqlconnect = new SqlConnection(varConnectionString);
                varSqlconnect.Open();
                returnValue = true;
            }
            catch (Exception Ex)
            {
                returnValue = false;
                throw Ex;
            }

            return returnValue;
        }

        public DataTable OrdersByName(int CustomerID)
        {

            DataTable dtOrdersByName = new DataTable();

            try
            {
                if (DBConnect())
                {
                    SqlCommand Source = new SqlCommand("OrdersByName", varSqlconnect);
                    Source.CommandType = CommandType.StoredProcedure;


                    // Define Adapter, "dbo.ActiveContactList" is stored procedure
                    SqlDataAdapter sqlDA = new SqlDataAdapter(Source);
                    // Fill DataTable with storedProcedure results
                    sqlDA.Fill(dtOrdersByName);
                }
                else
                {
                    throw new Exception("Could not find Stored Procedure");
                }
            
            }
            catch (Exception)
            {

                throw;
            }

            return dtOrdersByName;
        }

        public DataTable CustomerList()
        {
            DataTable dtCustomerList = new DataTable();

            try
            {
                if (DBConnect())
                {
                    SqlCommand Source = new SqlCommand("CustomerList", varSqlconnect);
                    Source.CommandType = CommandType.StoredProcedure;
                    // Define Adapter, "dbo.ActiveContactList" is stored procedure
                    SqlDataAdapter sqlDA = new SqlDataAdapter(Source);
                    // Fill DataTable with storedProcedure results
                    sqlDA.Fill(dtCustomerList);
                }
                else
                {
                    throw new Exception("Could not find Stored Procedure");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return dtCustomerList;
        }
    }
}

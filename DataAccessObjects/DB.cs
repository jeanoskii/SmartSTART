using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Configuration;

namespace CAPRES.DataAccessObjects
{
    public class DB
    {
        DataTable dt = new DataTable();

        /// <summary>
        /// Gets connection string from configuration file.
        /// </summary>
        /// <returns>
        /// Connection string
        /// </returns>
        private string GetConnectionString()
        {
            try
            {
                string connString = null;
                if (WebConfigurationManager.OpenWebConfiguration("/CAPRES").
                    ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    connString = WebConfigurationManager.OpenWebConfiguration("/CAPRES").
                    ConnectionStrings.ConnectionStrings["CAPRESConnectionString"].ConnectionString;
                    if (connString.Length == 0)
                    {
                        Debug.WriteLine("Connection string is empty.");
                    }
                }
                else
                {
                    Debug.WriteLine("No connection string in configuration file.");
                }
                return connString;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Get Connection String Exception Type: " + e.GetType() +
                    "\nMessage: " + e.Message +
                    "\nStack Trace: " + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Executes SELECT statements.
        /// </summary>
        /// <param name="sqlStatement">
        /// SQL statement to be executed.
        /// </param>
        /// <returns>
        /// DataTable consisting of results from query.
        /// </returns>
        public DataTable GetData(string sqlStatement)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
                sqlConnection.Open();
                SqlCommand com = new SqlCommand(sqlStatement, sqlConnection);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                sqlConnection.Close();
                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Get Data Exception Type: " + e.GetType() +
                    "\nMessage: " + e.Message +
                    "\nStack Trace: " + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Executes INSERT, UPDATE, and DELETE statements.
        /// </summary>
        /// <param name="transactionName">
        /// Name to be used for transaction.
        /// </param>
        /// <param name="sqlStatement">
        /// SQL statement to be executed.
        /// </param>
        public void Execute(string transactionName, string sqlStatement)
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(transactionName);
            try
            {
                SqlCommand com = new SqlCommand(sqlStatement, sqlConnection, sqlTransaction);
                com.ExecuteNonQuery();
                sqlTransaction.Commit();
                Debug.WriteLine(transactionName + " transaction commit success.");
            }
            catch (Exception e1)
            {
                Debug.WriteLine(transactionName + " Commit Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                try
                {
                    sqlTransaction.Rollback();
                    Debug.WriteLine(transactionName + " transaction rollback success.");
                }
                catch (Exception e2)
                {
                    Debug.WriteLine(transactionName + " Rollback Exception Type: " + e2.GetType() +
                        "\nMessage: " + e2.Message +
                        "\nStack Trace: " + e2.StackTrace);
                }
            }
            finally
            {
                sqlTransaction.Dispose();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Executes INSERT, UPDATE, and DELETE statements.
        /// </summary>
        /// <param name="transactionName">
        /// Name to be used for transaction.
        /// </param>
        /// <param name="sqlStatement">
        /// SQL statement to be executed.
        /// </param>
        /// <param name="sqlParameter">
        /// SQL parameters to be used.
        /// </param>
        public void Execute(string transactionName, string sqlStatement, SqlParameter[] sqlParameter)
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(transactionName);
            try
            {
                foreach (SqlParameter parameter in sqlParameter)
                {
                    if (parameter.Value == null || parameter.Value == "" || parameter.Value == "0")
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                SqlCommand com = new SqlCommand(sqlStatement, sqlConnection, sqlTransaction);
                com.Parameters.AddRange(sqlParameter);
                com.ExecuteNonQuery();
                sqlTransaction.Commit();
                Debug.WriteLine(transactionName + " transaction commit success.");
            }
            catch (Exception e1)
            {
                Debug.WriteLine(transactionName + " Commit Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                try
                {
                    sqlTransaction.Rollback();
                    Debug.WriteLine(transactionName + " transaction rollback success.");
                }
                catch (Exception e2)
                {
                    Debug.WriteLine(transactionName + " Rollback Exception Type: " + e2.GetType() +
                        "\nMessage: " + e2.Message +
                        "\nStack Trace: " + e2.StackTrace);
                }
            }
            finally
            {
                sqlTransaction.Dispose();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Executes INSERT, UPDATE, and DELETE statements with OUTPUT
        /// </summary>
        /// <param name="transactionName">
        /// Name to be used for transaction.
        /// </param>
        /// <param name="sqlStatement">
        /// SQL statement to be executed.
        /// </param>
        /// <returns>
        /// OUTPUT result.
        /// </returns>
        public DataTable ExecuteWithOutput(string transactionName, string sqlStatement)
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(transactionName);
            try
            {
                SqlCommand com = new SqlCommand(sqlStatement, sqlConnection, sqlTransaction);
                //com.ExecuteNonQuery();
                sqlTransaction.Commit();
                Debug.WriteLine(transactionName + " transaction commit success.");
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                return dt;
            }
            catch (Exception e1)
            {
                Debug.WriteLine(transactionName + " Commit Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                try
                {
                    sqlTransaction.Rollback();
                    Debug.WriteLine(transactionName + " transaction rollback success.");

                }
                catch (Exception e2)
                {
                    Debug.WriteLine(transactionName + " Rollback Exception Type: " + e2.GetType() +
                        "\nMessage: " + e2.Message +
                        "\nStack Trace: " + e2.StackTrace);
                }
                return null;
            }
            finally
            {
                sqlTransaction.Dispose();
                sqlConnection.Close();
            }
        }
    }
}
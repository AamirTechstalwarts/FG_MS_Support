using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGM_MS_Support.DattaAccess.DAL
{
    public class DBUtility
    {
        public static SqlConnection sqlCon = null;
        public static SqlDataAdapter sqlDa = null;
        public static SqlCommand sqlCom = null;

        public static DataSet ExecuteProcedureReturnDataset(string connString , string procName, params SqlParameter[] parameters)
        {
            string result = "";

            DataSet ds = new DataSet();
            using (var sqlConnection = new SqlConnection(connString))
            {
                using (var command  = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if(parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    sqlConnection.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                    sqlConnection.Close();
                }
            }
            return ds;
        }



        public static DataSet ExecuteProcedureReturnDataset(string connString, string procName)
        {
            DataSet ds = new DataSet();
            sqlCon = new SqlConnection(connString);
            sqlCom = new SqlCommand(procName, sqlCon);
            try
            {
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.CommandTimeout = 0;
                sqlDa = new SqlDataAdapter(sqlCom);
                sqlCon.Open();
                sqlDa.Fill(ds);
            }
            catch (Exception ex)
            {
                sqlCom.Dispose();
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlDa.Dispose();
                    sqlCon.Close();
                }
            }
            finally
            {
                sqlCom.Dispose();
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlDa.Dispose();
                    sqlCon.Close();
                }
            }
            return ds;
        }


    }
}

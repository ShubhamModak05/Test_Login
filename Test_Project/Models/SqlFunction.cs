using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Test_deqode.Models
{
    public class SqlFunction
    {
        public static SqlConnection Test_Deqode = new SqlConnection(ConfigurationManager.ConnectionStrings["Test_Deqode"].ToString());
        public static SqlCommand command = new SqlCommand();
        public static List<SqlParameter> Parameters = new List<SqlParameter>();


        public static void OpenConnection()
        {
            try
            {
                if (Test_Deqode.State != ConnectionState.Open)
                {
                    Test_Deqode.Open();
                    command.Connection = Test_Deqode;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (Test_Deqode.State != ConnectionState.Closed)
                {
                    Test_Deqode.Dispose();
                    Test_Deqode.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        //For Direct Query
        public static DataTable GetDataTable(string Query)
        {
            DataTable dt = new DataTable();
            OpenConnection();
            command.CommandText = Query;
            dt.Load(command.ExecuteReader());
            return dt;
        }

        public static int ExecuteNonQuery(string Query)
        {
            try
            {
                OpenConnection();
                command.CommandText = Query;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (command.Connection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
                throw ex;
            }
        }

    }
}
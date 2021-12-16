using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulKitapligiADONET_DAL
{
    public class MyPocketDAL
    {
        // //static string connString = @"Data Source=mssql27.natro.com;Initial Catalog=DB150511020833;Persist Security Info=True;User ID=USR150511020833;Password=PSS!G0Y4D8%";
        //Connection string eğer Windows Authentication değil server authentication ise yukarıdaki gibi bir connection string'iniz olacak. 
        public string ServerName { get; set; }

        public string DatabaseName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }

        private string _sqlConnectionString;
        public string SQLConnectionString
        {
            get
            {
                return _sqlConnectionString;
            }

        }
        private SqlConnection mySQLConnection = new SqlConnection();

        private SqlCommand mySqlCommand = new SqlCommand();

        private SqlDataAdapter mySqlAdapter = new SqlDataAdapter();

        public MyPocketDAL(string serverName, string dbname, string userid, string password)
        {
            //MyPocketDaL class'ından bir nesne türetilirken sql server name ve parola alınacak. Alınan değerler bu class'ın sahip olduğu SQLConnectionString property'sine atanacak.
            //Böylece bu class yapacağı tüm sql işlemlerinde connection stringi buradan alacak
            //Bir kere yazdık bu class'ın birçok metodunda kullabiliriz.
            this.ServerName = serverName;
            this.DatabaseName = dbname;
            this.UserID = userid;
            this.Password = password;

            if (string.IsNullOrEmpty(this.ServerName) || string.IsNullOrEmpty(this.DatabaseName))
            {
                throw new Exception("SQLConnection string için Server name ve database name gereklidir!");
            }
            else if (string.IsNullOrEmpty(this.UserID) && string.IsNullOrEmpty(this.Password))
            {
                //windows authentication
                this._sqlConnectionString = @"Server=" + this.ServerName + ";Database=" + this.DatabaseName + ";Trusted_Connection=True";

            }
            else
            {
                //server authentication
                this._sqlConnectionString = @"Data Source=" + this.ServerName + ";Initial Catalog=" + this.DatabaseName + ";Persist Security Info=True;User ID=" + this.UserID + ";Password=" + this.Password;
            }

        }

        public DataTable GetTheData(string tableName, string fieldName, string condition)
        {
            //returnValue ---> retVal
            DataTable retVal = new DataTable();
            string queryString = "";
            if (string.IsNullOrEmpty(condition))
            {
                queryString = "Select " + fieldName +  " from " + tableName;
            }
            else
            {
                queryString = "Select " + fieldName + " from " + tableName + " where " + condition;
            }

            using (this.mySQLConnection)
            {
                this.mySqlCommand = new SqlCommand(queryString, this.mySQLConnection);
                OpenTheConnection();
                this.mySqlAdapter = new SqlDataAdapter(this.mySqlCommand);
                this.mySqlAdapter.Fill(retVal);
            }

            return retVal;
        }

        public DataTable GetTheData (string query)
        {
            DataTable retVal = new DataTable();
            using (this.mySQLConnection)
            {
                using (this.mySQLConnection)
                {
                    this.mySqlCommand = new SqlCommand(query, this.mySQLConnection);
                    OpenTheConnection();
                    this.mySqlAdapter = new SqlDataAdapter(this.mySqlCommand);
                    this.mySqlAdapter.Fill(retVal);
                }
                return retVal;
            }
        }

        public void OpenTheConnection()
        {
            try
            {
                if (this.mySQLConnection.State!=ConnectionState.Open)
                {
                    this.mySQLConnection.ConnectionString = this.SQLConnectionString;
                    this.mySQLConnection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseTheConnection()
        {
            try
            {
                if (this.mySQLConnection.State!=ConnectionState.Closed)
                {
                    this.mySQLConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetTheDataByExecuteScalar(string query)
        {
            object retVal=null;
            using (this.mySQLConnection)
            {
                this.mySqlCommand = new SqlCommand(query, this.mySQLConnection);
                OpenTheConnection();
               retVal = this.mySqlCommand.ExecuteScalar();
            }

            return retVal;
        }

    }
}

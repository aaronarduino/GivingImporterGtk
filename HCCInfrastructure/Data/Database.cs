using System;
using Microsoft.Data.Sqlite;

namespace HCCInfrastructure.Data
{
    class Database
    {
        private readonly string FileName;
        private readonly Action<string> WriteToScreen;
        private SqliteConnection SqliteConn;

        public Database(string fileName, Action<string> writeToScreen)
        {
            FileName = fileName;
            WriteToScreen = writeToScreen;
        }

        public SqliteConnection CreateConnection()
        {
            SqliteConn = new SqliteConnection("Data Source=" + FileName);
            try
            {
                SqliteConn.Open();
            }
            catch (Exception ex)
            {
                WriteToScreen("[ERROR]: Could not connect to database. " + ex.Message);
            }

            return SqliteConn;
        }

        public void CreateTable()
        {
            var cmd = SqliteConn.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS BatchFileData (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, status TEXT, file_exists BOOLEAN, file_name TEXT, file_path TEXT, date_added DATETIME, last_poll_time DATETIME)";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteToScreen("[ERROR]: Could not create database table. " + ex.Message);
            }
        }
    }
}

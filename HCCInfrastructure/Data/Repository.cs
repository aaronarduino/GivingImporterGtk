using System;
using System.Collections.Generic;
using System.Linq;
using HCCInfrastructure.Helpers;
using HCCInfrastructure.Models;
using Microsoft.Data.Sqlite;

namespace HCCInfrastructure.Data
{
    public class Repository
    {
        private readonly Action<string> WriteToScreen;
        private Database Database;
        private SqliteConnection SqliteConn;

        public Repository(string fileLocation, Action<string> writeToScreen)
        {
            WriteToScreen = writeToScreen;
            Database = new Database(fileLocation, writeToScreen);
            SqliteConn = Database.CreateConnection();
        }

        public void AddBatchFileToQueue(BatchFileInfoModel batchFile)
        {
            EnsureDatabaseExists();

            var cmd = SqliteConn.CreateCommand();
            cmd.CommandText = "INSERT INTO BatchFileData (status, file_exists, file_name, file_path, date_added, last_poll_time) VALUES ($status, $file_exists, $file_name, $file_path, $date_added, $last_poll_time)";

            cmd.Parameters.AddWithValue("$status", batchFile.Status);
            cmd.Parameters.AddWithValue("$file_exists", batchFile.Exists);
            cmd.Parameters.AddWithValue("$file_name", batchFile.FileName);
            cmd.Parameters.AddWithValue("$file_path", batchFile.FilePath);
            cmd.Parameters.AddWithValue("$date_added", batchFile.DateAdded);
            cmd.Parameters.AddWithValue("$last_poll_time", batchFile.LastPollTime);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteToScreen("[ERROR]: Could not insert data into database. " + ex.Message);
            }

        }

        public List<BatchFileInfoModel> GetBatchFileQueue()
        {
            List<BatchFileInfoModel> batchFileQueue = new List<BatchFileInfoModel>();

            var cmd = SqliteConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM BatchFileData";

            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var batchFile = new BatchFileInfoModel()
                        {
                            ID = reader.GetInt32(0),
                            Status = reader.GetTextReader(1).ReadToEnd(),
                            Exists = reader.GetBoolean(2),
                            FileName = reader.GetTextReader(3).ReadToEnd(),
                            FilePath = reader.GetTextReader(4).ReadToEnd(),
                            DateAdded = reader.GetDateTime(5),
                            LastPollTime = reader.GetDateTime(6),
                        };

                        batchFileQueue.Add(batchFile);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToScreen("[ERROR]: Could not read some data from database. " + ex.Message);
            }

            return batchFileQueue;
        }

        public BatchFileInfoModel GetBatchFileByID(int id)
        {
            List<BatchFileInfoModel> result = new List<BatchFileInfoModel>();

            var cmd = SqliteConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM BatchFileData WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var batchFile = new BatchFileInfoModel()
                        {
                            ID = reader.GetInt32(0),
                            Status = reader.GetTextReader(1).ReadToEnd(),
                            Exists = reader.GetBoolean(2),
                            FileName = reader.GetTextReader(3).ReadToEnd(),
                            FilePath = reader.GetTextReader(4).ReadToEnd(),
                            DateAdded = reader.GetDateTime(5),
                            LastPollTime = reader.GetDateTime(6),
                        };

                        result.Add(batchFile);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToScreen("[ERROR]: Could not read some data from database. " + ex.Message);
            }

            return result.FirstOrDefault();
        }

        public void SetBatchFileStatus(int id, EnumHelper.BatchFileStatus status)
        {
            var cmd = SqliteConn.CreateCommand();
            cmd.CommandText = "UPDATE BatchFileData SET status = $status WHERE id = $id";

            cmd.Parameters.AddWithValue("$status", status.ToString());
            cmd.Parameters.AddWithValue("$id", id);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteToScreen("[ERROR]: Could not change status in database. " + ex.Message);
            }
        }

        public void SetBatchFileExists(BatchFileInfoModel fileInfoModel, bool exists)
        {
            var cmd = SqliteConn.CreateCommand();
            cmd.CommandText = "UPDATE BatchFileData SET status = $status, file_exists = $exists WHERE id = $id";

            // If file in database is "deleted" don't change status
            if(fileInfoModel.Status == EnumHelper.BatchFileStatus.Hidden.ToString())
            {
                cmd.CommandText = "UPDATE BatchFileData SET file_exists = $exists WHERE id = $id";
            }
            else
            {
                cmd.CommandText = "UPDATE BatchFileData SET status = $status, file_exists = $exists WHERE id = $id";
                cmd.Parameters.AddWithValue("$status", EnumHelper.BatchFileStatus.NotFound.ToString());
            }

            cmd.Parameters.AddWithValue("$exists", exists);
            cmd.Parameters.AddWithValue("$id", fileInfoModel.ID);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteToScreen("[ERROR]: Could not change status in database. " + ex.Message);
            }
        }

        private void EnsureDatabaseExists()
        {
            Database.CreateTable();
        }
    }
}

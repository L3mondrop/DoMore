using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using DoMore;
using static DoMore.Objects;

namespace DoMore
{



    public static class SqliteHandler
    {
        static string dbname = "Filename = data.db";
        public static void InitDatabase()
        {

            using (SqliteConnection db =
            new SqliteConnection(dbname))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS DataTable (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "GUID BLOB, Text NVARCHAR(2048) NULL, " + "DateTimeStarted BLOB, DateTimeStopped BLOB)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }


        public static string AddData(string text)
        {
            using (SqliteConnection db =
            new SqliteConnection(dbname))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                string gui = Guid.NewGuid().ToString();
                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO DataTable VALUES (NULL,@GUID, @Text, @DateTimeStarted, @DateTimeStopped);";
                insertCommand.Parameters.AddWithValue("@GUID", gui);
                insertCommand.Parameters.AddWithValue("@Text", text);
                insertCommand.Parameters.AddWithValue("@DateTimeStarted", DateTime.UtcNow);
                insertCommand.Parameters.AddWithValue("@DateTimeStopped", DateTime.UtcNow);

                insertCommand.ExecuteReader();

                db.Close();
                
                List<Objects.WorkItem> list = GetData();
                foreach (Objects.WorkItem a in list)
                {
                    System.Diagnostics.Debug.WriteLine(a.StartTime);
                }

                return gui;
            }
        }

        public static void UpdateData(string gui)
        {
            using (SqliteConnection db =
                new SqliteConnection(dbname))
            {
                db.Open();

                SqliteCommand updateCommand = new SqliteCommand
                    ("Update DataTable SET DateTimeStopped = '"+DateTime.UtcNow+"' where GUID = '"+gui+"'", db);

                SqliteDataReader query = updateCommand.ExecuteReader();

                db.Close();
            }

        }

        public static List<Objects.WorkItem> GetData()
        {
            List<Objects.WorkItem> entries = new List<Objects.WorkItem>();

            using (SqliteConnection db =
                new SqliteConnection(dbname))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from DataTable", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                
                while (query.Read())
                {
                    Objects.WorkItem item = new Objects.WorkItem();
                    item.Id = query.GetInt32(0);
                    item.GUID = query.GetString(1);
                    item.Text = query.GetString(2);
                    item.StartTime = query.GetString(3);
                    item.EndTime = query.GetString(4);
                    entries.Add(item);
                }

                db.Close();
            }

            return entries;
        }


    }
}

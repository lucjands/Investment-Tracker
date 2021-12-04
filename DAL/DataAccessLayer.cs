using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class DataAccessLayer
	{
		private static string _DBDataSource = Properties.Resources.DBLocation;
		public static string DBDataSource
		{
			get 
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(appDataPath, _DBDataSource);
                return path; 
            }
		}

        private static string _ConnectionString;
        public static string ConnectionString
        {
            get
            {
                if (_ConnectionString != null)
                    return _ConnectionString;

                SQLiteConnectionStringBuilder connStringBuilder = new SQLiteConnectionStringBuilder();
                connStringBuilder.DataSource = DBDataSource;
                _ConnectionString = connStringBuilder.ConnectionString;

                return _ConnectionString;
            }
        }

        public DataAccessLayer()
        {
            CreateDBFileIfNotPresent();
        }

        private void CreateDBFileIfNotPresent()
        {
            try
            {
                using (var con = new SQLiteConnection(ConnectionString))
                {
                    con.Open();
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("unable to open database file"))
                {
                    string DBDataSourceDir = Path.GetDirectoryName(DBDataSource);
                    if (!Directory.Exists(DBDataSourceDir))
                        Directory.CreateDirectory(DBDataSourceDir);

                    SQLiteConnection.CreateFile(DBDataSource);
                }
                else
                {
                    throw new Exception("There was an SQLException that has notting to do with the DB file not being present.", ex);
                }
            }
        }


        public int ExecuteWrite(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;

            if (string.IsNullOrEmpty(query.Trim()))
                return numberOfRowsAffected = 0;

            //setup the connection to the database
            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();

                //open a new command
                using (var cmd = new SQLiteCommand(query, con))
                {
                    //set the arguments given in the query if any
                    if (args != null)
                    {
                        foreach (var pair in args)
                        {
                            cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                        }
                    }

                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }

                return numberOfRowsAffected;
            }
        }

        public DataTable ExecuteRead(string query, Dictionary<string, object> args)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;

            using (var con = new SQLiteConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(query, con))
                {
                    foreach (KeyValuePair<string, object> entry in args)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }

                    var da = new SQLiteDataAdapter(cmd);

                    var dt = new DataTable();
                    da.Fill(dt);

                    da.Dispose();
                    return dt;
                }
            }
        }



    }
}

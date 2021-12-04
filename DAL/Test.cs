using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Test
    {
        public void CreateDBFile()
        {
            SQLiteConnection.CreateFile("Data/database.sqlite");
        }

        public int createTable()
        {
            const string query = "CREATE TABLE User(Id INTEGER PRIMARY KEY AUTOINCREMENT, FirstName TEXT NOT NULL, LastName TEXT NOT NULL); ";

            return DataAccessLayer.ExecuteWrite(query, null);
        }

        public int AddUser(User user)
        {
            const string query = "INSERT INTO User(FirstName, LastName) VALUES(@firstName, @lastName)";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@firstName", user.FirstName},
                {"@lastName", user.Lastname}
            };

            return DataAccessLayer.ExecuteWrite(query, args);
        }

        public User GetUserById(int id)
        {
            var query = "SELECT * FROM User WHERE Id = @id";

            var args = new Dictionary<string, object>
            {
                {"@id", id}
            };

            DataTable dt = DataAccessLayer.ExecuteRead(query, args);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            var user = new User
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                FirstName = Convert.ToString(dt.Rows[0]["FirstName"]),
                Lastname = Convert.ToString(dt.Rows[0]["LastName"])
            };

            return user;
        }



    }
}

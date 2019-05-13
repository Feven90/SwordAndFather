using Dapper;
using SwordAndFather.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SwordAndFather.Data
{
    public class UserRepository
    {
        const string ConnectionString = "Server=localhost;Database=SwordAndFather;Trusted_Connection=True;";

        public User AddUser(string username, string password)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newUser = db.QueryFirstOrDefault<User>(@"
                    Insert into users (username,password)
                    Output inserted.*
                    Values(@username,@password)",
                    new { username, password });

                if (newUser != null)
                {
                    return newUser;
                }
            }

            throw new Exception("No user created");
        }

        public void DeleteUser(int userId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var parameter = new { Id = userId };

                var deleteQuery = "Delete From Users where Id = @id";

                var rowsAffected = db.Execute(deleteQuery, parameter);

                if (rowsAffected != 1)
                {
                    throw new Exception("Didn't do right");
                }
            }
        }

        public User UpdateUser(User userToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sql = @"Update Users
                            Set username = @username,
                                password = @password
                            Where id = @id";

                var rowsAffected = db.Execute(sql, userToUpdate);

                if (rowsAffected == 1)
                    return userToUpdate;
            }

            throw new Exception("Could not update user");
        }

        public IEnumerable<User> GetAll() // if a method is IEnumerable, it can not be removed or add stuff to it
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var users = db.Query<User>("select username, password, id from users").ToList();
                var targets = db.Query<Target>("select * from Targets").ToList();

                foreach (var user in users)
                {
                    user.Targets = targets.Where(x => x.UserId == user.Id).ToList();
                }

                return users; // null propagation makes the null value carry out 
            }
        }
    }
}















//using System.Collections.Generic;
//using SwordAndFather.Models;
//using System.Data.SqlClient;
//using Dapper;
//using System;

//namespace SwordAndFather.Data
//{
//    public class UserRepository
//    {
//        static List<User> _users = new List<User>();
//        const string ConnectionString = "Server=localhost;Database=SwordAndFather;Trusted_Connection=True;";

//        public User AddUser(string username, string password)
//        {
//            using (var connection = new SqlConnection(ConnectionString));
//            {
//                var newUser = connection.QueryFirstOrDefault<User>(@"
//                    Insert into Users (username, password)
//                    Output inserted.*        
//                    values (@username,@password)",
//                    new { Username = username, password = password }); // or new { username, password }); // Output inserted.*  (this is select statment, if this was not there it will give null and does not give back what it read)      


//                if (newUser != null)
//                {
//                    return newUser;
//                }

//            }
//            //connection.Open();


//            //    var InsertUserCommand = connection.CreateCommand();
//            //    InsertUserCommand.CommandText = $@"Insert into Users (username, password)
//            //                                Output inserted.*
//            //                             values (@username,@password)";  // string interpultation will cause sql injection 
//            //InsertUserCommand.parameters.AddWithValue("username", username);  // for sql injection use parameters'
//            //InsertUserCommand.parameters.AddWithValue("password", username);


//            //var numberOfRowsAffected = InsertUserCommand.ExecuteNonQuery(); // numberofRowsAffected is going to be integer, this will return number of rows affected(we use ExecuteNonQuery because we don't care what is affected)
//            //    var reader = InsertUserCommand.ExecuteReader();
//            //    if (reader.Read())       // Read() gives back the row
//            //    {
//            //        var insetedUsername = reader["username"].ToString();
//            //        var insertedPassword = reader["password"].ToString();
//            //        var insertedId = (int)reader["Id"];

//            //        var newUser = new User(insetedUsername, insertedPassword)(Id = insertedId);
//            //        return newUser;
//            //    }

//            ////finally
//            ////{
//            ////    connection.Close();  // finally will make closing connection to be guarantied 
//            ////}
//            throw new Exception("No user created");
//        }

//        public void DeleteUser(int id)
//        {
//            using (var db = new SqlConnection(connectionString))
//            {
//                var rowsAffected = db.Execute("Delete From Users Where id = @id", new { id });
//                if (rowsAffected != 1)
//                {
//                    throw new Exception("Didn't do right");
//                }
//            }
//        }

//        public User UpdatedUser(User userToUpdate)
//        {
//            using (var db = new SqlConnection(conncetionString));
//            {
//                var rowsAffected = db.Execute(@"update Users  +
//                            Set username = @username,
//                                password = @password
//                            Where id = @id", userToUpdate);
//                if(rowsAffected == 1)
//                return userToUpdate;

//                // we use the anonymous to set the parameters 
//            }
//            throw new Exception("Could not update user");
//        }


//        public IEnumerable<User> GetAll()
//        {
//            var users = new List<User>();

//            var connection = new SqlConnection("Server=localhost;Database=SwordAndFather;Trusted_Connection=True;"); // creating a connection to sql server 
//            connection.Open();
//            var users = connection.Query<User>("select username,password,id from users");

//            //var getAllUsersCommand = connection.CreateCommand();   // command - assosiating it with the connection we created 
//            //getAllUsersCommand.CommandText = @"select username,password,id  
//            //                                   from users";

//            //var reader = getAllUsersCommand.ExecuteReader();      //ExcuteScal top left column and row 

//            //while (reader.Read())
//            //{
//            //    var id = (int)reader["Id"];
//            //    var username = reader["username"].ToString();
//            //    var password = reader["password"].ToString();
//            //    var user = new User(username, password) { Id = id };

//            //    users.Add(user);
//            //}

//            //connection.Close();

//            return users;
//        }  

//    }
//}
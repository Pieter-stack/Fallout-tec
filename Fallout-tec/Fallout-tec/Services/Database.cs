using Fallout_tec.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Fallout_tec.Services
{
    public class Database
    {

        //configure to connect to our local host
        private static string serverConfiguration = @"server=localhost;userid=root;password=;database=fallouttec;";

        //test our db connection by returnbing version of our db
        public static string GetVersion()
        {

            using var con = new MySqlConnection(serverConfiguration);
            con.Open();
            return con.ServerVersion;
        }

        //get inventory data

        public static List<Inventory> GetInventory()
        {
            //create and open db con
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //setup our query
            string sql = "SELECT * FROM inventory WHERE Location_id = '1' ";
            using var cmd = new MySqlCommand(sql, con); // perform this new command which is sql and do it in the con established

            //creates an instance of our command result that can be read in c#
            using MySqlDataReader reader = cmd.ExecuteReader();

            //init our return list
            var results = new List<Inventory>();

            while (reader.Read())
            {
                var InvItems = new Inventory(reader.GetInt32(2))
                {
                    ItemName = reader.GetString(1), // index of our column order
                    ItemType = reader.GetString(4),
                    ItemPicture = reader.GetString(3),
                    Location = reader.GetString(5),

                };
                results.Add(InvItems);
            }
            //return TagHelperServicesExtensions final results after adding
            return results;
        }

		//update the count of our blocks
		public static void UpdateInvItemsQuantity(string itemname, int newCount, int itemlocation)
        {
            //establish conection
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //sql query
            string sql = "UPDATE `inventory` SET `ItemQuantity`= @itemcount WHERE `ItemName`= @itemname AND Location_id = @itemlocation ";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@itemname", itemname);
            cmd.Parameters.AddWithValue("@itemcount", newCount);
            cmd.Parameters.AddWithValue("@itemlocation", itemlocation);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public static void SearchUsersbackpack(int backpack)
        {
            //establish conection
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //sql query
            string sql = "SELECT * FROM inventory WHERE Location_id = '1' ";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@backpack", backpack);


            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
        public static void SearchUsersbase( int homebase)
        {
            //establish conection
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //sql query
            string sql = "SELECT * FROM inventory WHERE Location_id = '2' ";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@homebase", homebase);


            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
        public static void SearchUserstent(int survivaltent)
        {
            //establish conection
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //sql query
            string sql = "SELECT * FROM inventory WHERE Location_id = '3' ";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@survivaltent", survivaltent);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }






        //register new user

        public static List<Users> GetUsers()
        {
            //create and open db con
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //setup our query
            string sql = "SELECT * FROM users";
            using var cmd = new MySqlCommand(sql, con); // perform this new command which is sql and do it in the con established

            //creates an instance of our command result that can be read in c#
            using MySqlDataReader reader = cmd.ExecuteReader();

            //init our return list
            var results = new List<Users>();

            while (reader.Read())
            {
                var Rusers = new Users()
                {
                     Username = reader.GetString(1), // index of our column order
                    Password = reader.GetString(2),
                    Email = reader.GetString(3),
                    ProfilePic = reader.GetString(4),

                };
                results.Add(Rusers);
            }
            //return TagHelperServicesExtensions final results after adding
            return results;
        }





        //register new user
        public static void RegisterNewUser(string username, string email, string password, string profilepic)
        {


            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: salt,
           prf: KeyDerivationPrf.HMACSHA256,
           iterationCount: 100000,
           numBytesRequested: 256 / 8));
            Console.WriteLine($"Hashed: {hashed}");
            //https://stackoverflow.com/questions/64407091/check-encrypted-password-on-login-asp-net-core

            //establish conection
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();



            //sql query
            string sql = "INSERT INTO `users`(`Username`, `Password`, `Email`, `ProfilePic`, `Caps`) VALUES (@username,@password,@email,@profilepic,1000)";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);


            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", hashed);
            cmd.Parameters.AddWithValue("@profilepic", profilepic);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        

    }
}

using Fallout_tec.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

namespace Fallout_tec.Services
{

    public class Database
    {

        //configure to connect to our local host
        private static string serverConfiguration = @"server=localhost;userid=root;password=;database=fallouttec;";
        private static string adminEmail;
        private static string adminPassword;
        private static string adminCode;

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
                    ID = reader.GetString(0),
                    ItemName = reader.GetString(1), // index of our column order
                    ItemType = reader.GetString(4),
                    ItemPicture = reader.GetString(3),
                    Location = reader.GetString(5),

                };
                results.Add(InvItems);
            }
            con.Close();
            //return TagHelperServicesExtensions final results after adding
            return results;
        }








        public static List<Inventory> GetCraftingSearch(int location)
        {
            //establish conection

            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //sql query
            string sql = "SELECT * FROM inventory WHERE Location_id = @location ";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);


            cmd.Parameters.AddWithValue("@location", location);

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


            con.Close();    
            //return TagHelperServicesExtensions final results after adding
            return results;

        }



        public static List<Inventory> GetInputInvSearch(string search, int location)
        {
            //establish conection

            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //sql query
            string sql = "SELECT * FROM inventory WHERE ItemName LIKE '" + search + "%' AND Location_id = @location  ";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);


            cmd.Parameters.AddWithValue("@search", search);
            cmd.Parameters.AddWithValue("@location", location);

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


            con.Close();
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
            string sql = "UPDATE `inventory` SET `ItemQuantity`= @itemcount WHERE `ItemName`= @itemname AND Location_id = @itemlocation";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@itemname", itemname);
            cmd.Parameters.AddWithValue("@itemcount", newCount);
            cmd.Parameters.AddWithValue("@itemlocation", itemlocation);

            cmd.Prepare();
            cmd.ExecuteNonQuery();



            string sql2 = "UPDATE `crafting` SET `CraftQuantity`= @itemcount WHERE `CraftName`= @itemname AND LocationId = @itemlocation";
            Console.WriteLine(sql);
            using var cmd2 = new MySqlCommand(sql2, con);

            cmd2.Parameters.AddWithValue("@itemname", itemname);
            cmd2.Parameters.AddWithValue("@itemcount", newCount);
            cmd2.Parameters.AddWithValue("@itemlocation", itemlocation);
            cmd2.Prepare();
            cmd2.ExecuteNonQuery();



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
            con.Close();
            //return TagHelperServicesExtensions final results after adding
            return results;
        }



        public static bool VerifyCode(string verify)
        {
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            string sql = "SELECT Verify FROM admin WHERE Verify = @verify ";
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@verify", verify);
            using MySqlDataReader reader = cmd.ExecuteReader();

            var databaseverify = "";

            while (reader.Read())
            {
                databaseverify = reader.GetString(0);
            }
            con.Close();





            Console.WriteLine("Input verify code --------------" + verify);
            Console.WriteLine("Database verify code --------------" + databaseverify);




            if (databaseverify == verify)
            {

                Console.WriteLine("Correct");
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect");
                return false;
            }

        }


        public static void SendEmail()
        {
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            string sql = "SELECT SENDemail FROM admin";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader reader = cmd.ExecuteReader();

            var databaseemail = "";

            while (reader.Read())
            {
                databaseemail = reader.GetString(0);
            }
            con.Close();

            Console.WriteLine("Database verify code --------------" + databaseemail);
            adminEmail = databaseemail;


        }


        public static void SendCode()
        {
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            string sql = "SELECT Verify FROM admin";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader reader = cmd.ExecuteReader();

            var databaseverify = "";

            while (reader.Read())
            {
                databaseverify = reader.GetString(0);
            }
            con.Close();

            Console.WriteLine("Database verify code --------------" + databaseverify);
            adminCode = databaseverify;


        }





        public static void SendPassword()
        {
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            string sql = "SELECT SENDpassword FROM admin";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader reader = cmd.ExecuteReader();

            var databasepassword = "";

            while (reader.Read())
            {
                databasepassword = reader.GetString(0);
            }
            con.Close();


            Console.WriteLine("Database verify code --------------" + databasepassword);
            adminPassword = databasepassword;


        }


        //register new user
        public static bool RegisterNewUser(string username, string email, string password)
        {

            SendEmail();
            SendPassword();
            SendCode();

            if (CheckReg(email) == false)
            {

                //establish conection
                using var con = new MySqlConnection(serverConfiguration);
                con.Open();



                //sql query
                string sql = "INSERT INTO `users`(`Username`, `Password`, `Email`, `Caps`) VALUES (@username,@password,@email,1000)";
                Console.WriteLine(sql);
                using var cmd = new MySqlCommand(sql, con);


                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                cmd.Prepare();
                cmd.ExecuteNonQuery();



                string getEmail = adminEmail;
                Console.WriteLine("method 2 string --------------" + getEmail);
                string getPassword = adminPassword;
                Console.WriteLine("method 2 string --------------" + getPassword);
                string getCode = adminCode;
                Console.WriteLine("method 2 string --------------" + getCode);

                MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(getEmail);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Fallout-Tec authentication code";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = "Hello your fallout tec access code is:" + getCode;
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(getEmail, getPassword);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);


                return true;


            }
            else
            {
                Console.WriteLine("Email Taken");
                return false;
            }

        }



        public static bool CheckReg(string email)
        {
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            string sql = "SELECT Email FROM users WHERE Email = @email ";
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@email", email);
            using MySqlDataReader reader = cmd.ExecuteReader();

            var databaseemail = "";

            while (reader.Read())
            {
                databaseemail = reader.GetString(0);
            }
            con.Close();





            Console.WriteLine("Input verify code --------------" + email);
            Console.WriteLine("Database verify code --------------" + databaseemail);





            if (databaseemail == email)
            {

                Console.WriteLine("Correct");
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect");
                return false;
            }

        }





        //Crafting 

        public static List<Recipe> GetAllRecipes()
        {
            //create and open db con
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();
            //setup our query
            string sql = "SELECT * FROM crafting WHERE CraftingStation = 'ArmourWorkbench' AND LocationId = '1'";
            using var cmd = new MySqlCommand(sql, con); // perform this new command which is sql and do it in the con established

            //creates an instance of our command result that can be read in c#
            using MySqlDataReader reader = cmd.ExecuteReader();

            //init our return list
            var results = new List<Recipe>();
            //go through our readable data do thbis for each entry
            while (reader.Read())
            {
                var recipe = new Recipe(reader.GetInt32(3))
                {
                    CraftName = reader.GetString(1), // index of our column order
                    CraftImage = reader.GetString(2),
                    CraftType = reader.GetString(4),
                    CraftStation = reader.GetString(5),
                    LocationId = reader.GetString(6),

                };


                var ingredientsName = new List<string>();
                var ingredientsCount = new List<int>();
                var ingredientsImage = new List<string>();

                ingredientsName.Add(reader.GetString(7)); //ingredient1
                ingredientsCount.Add(reader.GetInt32(8)); //ingredient1Count
                ingredientsImage.Add(reader.GetString(9)); //ingredient1Image


                ingredientsName.Add(reader.GetString(10)); //ingredient2
                ingredientsCount.Add(reader.GetInt32(11)); //ingredient2Count
                ingredientsImage.Add(reader.GetString(12)); //ingredient2Image


                ingredientsName.Add(reader.GetString(13)); //ingredient3
                ingredientsCount.Add(reader.GetInt32(14)); //ingredient3Count
                ingredientsImage.Add(reader.GetString(15)); //ingredient3Image


                ingredientsName.Add(reader.GetString(16)); //ingredient4
                ingredientsCount.Add(reader.GetInt32(17)); //ingredient4Count
                ingredientsImage.Add(reader.GetString(18)); //ingredient4Image



                ingredientsName.Add(reader.GetString(19)); //ingredient5
                ingredientsCount.Add(reader.GetInt32(20)); //ingredient5Count
                ingredientsImage.Add(reader.GetString(21)); //ingredient5Image


 
                ingredientsName.Add(reader.GetString(22)); //ingredient6
                ingredientsCount.Add(reader.GetInt32(23)); //ingredient6Count
                ingredientsImage.Add(reader.GetString(24)); //ingredient6Image


                ingredientsName.Add(reader.GetString(25)); //ingredient7
                ingredientsCount.Add(reader.GetInt32(26)); //ingredient7Count
                ingredientsImage.Add(reader.GetString(27)); //ingredient7Image

                recipe.IngredientsName = ingredientsName;
                recipe.IngredientsCount = ingredientsCount;
                recipe.IngredientsImage = ingredientsImage;


                results.Add(recipe);
               
            }
            con.Close();
            //return TagHelperServicesExtensions final results after adding
            return results;
        }



        public static bool CraftRecipe(string name, int newcount, int location, List<string> ingredientsName, List<string> ingredientsCount,string verify)
        {
          

            if (VerifyCode(verify))
            {

                //TODO: Remove the ingredients
                UpdateItemsCountAfterCraft(ingredientsName, ingredientsCount, location);

                using var con = new MySqlConnection(serverConfiguration);
                con.Open();

                //sql query
                string sql = "UPDATE `inventory` SET `ItemQuantity`= @count WHERE `ItemName`= @name  AND Location_id = @location";
                Console.WriteLine(sql);
                using var cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@count", newcount);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@location", location);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                //sql query
                string sql2 = "UPDATE `crafting` SET `CraftQuantity`= @count WHERE `CraftName`= @name  AND LocationId = @location";
                Console.WriteLine(sql);
                using var cmd2 = new MySqlCommand(sql2, con);

                cmd2.Parameters.AddWithValue("@count", newcount);
                cmd2.Parameters.AddWithValue("@name", name);
                cmd2.Parameters.AddWithValue("@location", location);

                cmd2.Prepare();
                cmd2.ExecuteNonQuery();
                return true;

            }
            else
            {
                Console.WriteLine("failure");
                return false;
            }





        }




        public static void UpdateItemsCountAfterCraft(List<string> ingredientsName, List<string> ingredientsCount, int location)
        {
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            foreach (var ingredient in ingredientsName)
            {

                if (ingredient != "")
                {

                    //skryf function
                    //for(var i = 0l i < name.length; i++{
                    // name[i] same wees as count[i]
                    //}

                    int currentcount = GetCountOfItems(ingredient, location);
                    //sql query
                    string sql = "UPDATE `inventory` SET `ItemQuantity` = @count WHERE `ItemName` = @name AND Location_id = @location";
                    using var cmd = new MySqlCommand(sql, con);


                    cmd.Parameters.AddWithValue("@name", ingredient);
                    cmd.Parameters.AddWithValue("@count", currentcount - 1);
                    cmd.Parameters.AddWithValue("@location", location);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static int GetCountOfItems(string name, int location)
        {
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            string sql = "SELECT `ItemQuantity` FROM inventory WHERE ItemName = @name AND Location_id = @location";
            using var cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@location", location);

            using MySqlDataReader reader = cmd.ExecuteReader();

            int count = 0;

            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }
            con.Close();
            return count;
        }

  





    public static List<Recipe> GetCraftingPageSearch(int location, string station)
        {
            //establish conection

            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //sql query
            string sql = "SELECT * FROM crafting WHERE LocationId = @location AND CraftingStation = @station";
            Console.WriteLine(sql);
            using var cmd = new MySqlCommand(sql, con);


            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@station", station);

            using MySqlDataReader reader = cmd.ExecuteReader();

            //init our return list
            var results = new List<Recipe>();
            //go through our readable data do thbis for each entry
            while (reader.Read())
            {
                var recipe = new Recipe(reader.GetInt32(3))
                {
                    CraftName = reader.GetString(1), // index of our column order
                    CraftImage = reader.GetString(2),
                    CraftType = reader.GetString(4),
                    CraftStation = reader.GetString(5),
                    LocationId = reader.GetString(6),

                };


                var ingredientsName = new List<string>();
                var ingredientsCount = new List<int>();
                var ingredientsImage = new List<string>();

                ingredientsName.Add(reader.GetString(7)); //ingredient1
                ingredientsCount.Add(reader.GetInt32(8)); //ingredient1Count
                ingredientsImage.Add(reader.GetString(9)); //ingredient1Image


                ingredientsName.Add(reader.GetString(10)); //ingredient2
                ingredientsCount.Add(reader.GetInt32(11)); //ingredient2Count
                ingredientsImage.Add(reader.GetString(12)); //ingredient2Image


                ingredientsName.Add(reader.GetString(13)); //ingredient3
                ingredientsCount.Add(reader.GetInt32(14)); //ingredient3Count
                ingredientsImage.Add(reader.GetString(15)); //ingredient3Image


                ingredientsName.Add(reader.GetString(16)); //ingredient4
                ingredientsCount.Add(reader.GetInt32(17)); //ingredient4Count
                ingredientsImage.Add(reader.GetString(18)); //ingredient4Image



                ingredientsName.Add(reader.GetString(19)); //ingredient5
                ingredientsCount.Add(reader.GetInt32(20)); //ingredient5Count
                ingredientsImage.Add(reader.GetString(21)); //ingredient5Image



                ingredientsName.Add(reader.GetString(22)); //ingredient6
                ingredientsCount.Add(reader.GetInt32(23)); //ingredient6Count
                ingredientsImage.Add(reader.GetString(24)); //ingredient6Image


                ingredientsName.Add(reader.GetString(25)); //ingredient7
                ingredientsCount.Add(reader.GetInt32(26)); //ingredient7Count
                ingredientsImage.Add(reader.GetString(27)); //ingredient7Image

                recipe.IngredientsName = ingredientsName;
                recipe.IngredientsCount = ingredientsCount;
                recipe.IngredientsImage = ingredientsImage;


                results.Add(recipe);

            }
            con.Close();
            //return TagHelperServicesExtensions final results after adding
            return results;

        }



        public static List<Inventory> GetInventoryJunk(int location)
        {
            //create and open db con
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //setup our query
            string sql = "SELECT * FROM inventory WHERE ItemQuantity > 0 AND ItemType = 'Junk' AND  Location_id = @location ";
            using var cmd = new MySqlCommand(sql, con); // perform this new command which is sql and do it in the con established
            cmd.Parameters.AddWithValue("@location", location);
            //creates an instance of our command result that can be read in c#
            using MySqlDataReader reader = cmd.ExecuteReader();

            //init our return list
            var results = new List<Inventory>();

            while (reader.Read())
            {
                var InvItems = new Inventory(reader.GetInt32(2))
                {
                    ID = reader.GetString(0),
                    ItemName = reader.GetString(1), // index of our column order
                    ItemType = reader.GetString(4),
                    ItemPicture = reader.GetString(3),
                    Location = reader.GetString(5),

                };
                results.Add(InvItems);
            }
            con.Close();
            //return TagHelperServicesExtensions final results after adding
            return results;
        }


        public static List<Inventory> GetInventoryExtras(int location)
        {
            //create and open db con
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //setup our query
            string sql = "SELECT * FROM inventory WHERE ItemQuantity > 0 AND ItemType = 'Exras' AND  Location_id = @location ";
            using var cmd = new MySqlCommand(sql, con); // perform this new command which is sql and do it in the con established
            cmd.Parameters.AddWithValue("@location", location);
            //creates an instance of our command result that can be read in c#
            using MySqlDataReader reader = cmd.ExecuteReader();

            //init our return list
            var results = new List<Inventory>();

            while (reader.Read())
            {
                var InvItems = new Inventory(reader.GetInt32(2))
                {
                    ID = reader.GetString(0),
                    ItemName = reader.GetString(1), // index of our column order
                    ItemType = reader.GetString(4),
                    ItemPicture = reader.GetString(3),
                    Location = reader.GetString(5),

                };
                results.Add(InvItems);
            }
            con.Close();
            //return TagHelperServicesExtensions final results after adding
            return results;
        }


        public static List<Inventory> GetInventoryCraftable(int location)
        {
            //create and open db con
            using var con = new MySqlConnection(serverConfiguration);
            con.Open();

            //setup our query
            string sql = "SELECT * FROM inventory WHERE ItemQuantity > 0 AND ItemType = 'Craftable' AND  Location_id = @location ";
            using var cmd = new MySqlCommand(sql, con); // perform this new command which is sql and do it in the con established
            cmd.Parameters.AddWithValue("@location", location);
            //creates an instance of our command result that can be read in c#
            using MySqlDataReader reader = cmd.ExecuteReader();

            //init our return list
            var results = new List<Inventory>();

            while (reader.Read())
            {
                var InvItems = new Inventory(reader.GetInt32(2))
                {
                    ID = reader.GetString(0),
                    ItemName = reader.GetString(1), // index of our column order
                    ItemType = reader.GetString(4),
                    ItemPicture = reader.GetString(3),
                    Location = reader.GetString(5),

                };
                results.Add(InvItems);
            }
            con.Close();
            //return TagHelperServicesExtensions final results after adding
            return results;
        }






    }
}







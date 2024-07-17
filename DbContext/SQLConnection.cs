using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Drivers_License_Test_Practice.UI;

namespace Drivers_License_Test_Practice.DbContext
{
    internal class SQLConnection
    {

        public static string  GetConnectionString()
        {
            // conection sting edw thes chat gtp

            string dataSource = ".";
            string dataBase = "Driver_License_DB";

            string ConnString = @"Data Source=" + dataSource + ";initial catalog=" + dataBase + ";integrated security=true;" + "TrustServerCertificate=True;";
            return ConnString;
        }

        // Εισοδος χρηστη
            
        public static bool ValidationLoginUser(string username, string pass)
        {
            Console.WriteLine("Λήψη σύνδεσης");
            Utility.PrintDotAnimation(); 

            using (SqlConnection connection = new SqlConnection(GetConnectionString())) 
            {
                try 
                { 
                    connection.Open();
                    Console.WriteLine("\nΣύνδεση Ανοιχτή");
                    Utility.PrintDotAnimation();

                    //string query = "SELECT FROM Users WHERE UserName = @username AND Password = @password";
                    string query = "SELECT COUNT(*) FROM Users WHERE UserName = @username AND Password = @password";

                    using (SqlCommand command = new SqlCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue ("@Password", pass);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                       
                    }
                } 
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message); 
                    return false;
                }
                
                
                
            }
        }

        //Εγγραφη νεου χρηστη

        public static string CreatNewUser(string username, string password) 
        {
            Console.WriteLine("Λήψη σύνδεσης");
            Utility.PrintDotAnimation();

            using (SqlConnection connection = new SqlConnection(GetConnectionString())) 
            {
                try 
                {
                    connection.Open();
                    Console.WriteLine("\nΣύνδεση Ανοιχτή");
                    Utility.PrintDotAnimation();
                    //SQL QUERY 
                    string query = "INSERT INTO Users(UserName, Password) VALUES(@username, @password)";

                    //Δημιουργία του SqlCommand αντικειμένου με το query και τη σύνδεση 
                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {

                        // Προσθήκη παραμέτρων στο query
                        command.Parameters.AddWithValue("@username",username);
                        command.Parameters.AddWithValue("@password",password);


                        // Εκτέλεση του query
                        int result = command.ExecuteNonQuery();

                        // Έλεγχος αν το query εκτελέστηκε επιτυχώς
                        if (result > 0)
                        {
                            return $"\n\nΟ χρήστης {username} δημιουργήθηκε με επιτυχία.";

                        }
                        else {
                            return "\n\nΗ δημιουργία χρήστη απέτυχε.";
                        }   
                    }
                }
                catch (Exception ex){

                    return "Error " + ex.Message ;
                     
                }
            }
                
        }



    }

        


    
}

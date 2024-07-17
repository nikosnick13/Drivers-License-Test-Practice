using Drivers_License_Test_Practice.DbContext;
using Drivers_License_Test_Practice.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;

namespace Drivers_License_Test_Practice.AnsuerAndQoyestion
{
    internal class QuestionAnswer
    {
 
        private string conecionString;
        public QuestionAnswer( )
        {
            conecionString = SQLConnection.GetConnectionString();
        }



        public void CreatTable() 
        {
            using (SqlConnection connection = new SqlConnection(conecionString)) 
            {
                connection.Open();
                string createTableQuery = @"

                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Questions' AND xtype='U')
                    CREATE TABLE Questions (
                        id INT PRIMARY KEY IDENTITY(1,1),
                        question NVARCHAR(255) NOT NULL,
                        option1 NVARCHAR(100) NOT NULL,
                        option2 NVARCHAR(100) NOT NULL,
                        option3 NVARCHAR(100) NOT NULL,
                        option4 NVARCHAR(100) NOT NULL,
                        correct_option INT NOT NULL,
                        category NVARCHAR(50)
                        );";
                    
             
                using (SqlCommand command = new SqlCommand(createTableQuery, connection)) 
                {
                    command.ExecuteNonQuery();
                }

            }

        }


        public  void AddQuestion(string question, string option1, string option2, string option3, string option4, int correct_option, string category  /*int userAnswer*/)
        {
            using (SqlConnection connection = new SqlConnection(conecionString)) 
            {
                connection.Open();
                string insertQuery = @" 
                INSERT INTO Questions (question,option1,option2,option3,option4,correct_option,category) 
                VALUES(@question,@option1,@option2,@option3,@option4,@correctOption,@category)";

                using (SqlCommand command = new SqlCommand(insertQuery,connection)) 
                {
                    command.Parameters.AddWithValue("@question",question);
                    command.Parameters.AddWithValue("@option1", option1);
                    command.Parameters.AddWithValue("@option2", option2);
                    command.Parameters.AddWithValue("@option3", option3);
                    command.Parameters.AddWithValue("@option4", option4);
                    command.Parameters.AddWithValue("@correctOption", correct_option); // Πρέπει να είναι ακριβώς όπως στο SQL ερώτημα
                    command.Parameters.AddWithValue("@category", category);
                     
                    try {
                         command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("General Error: " + ex.Message);
                    }
                }
            }
        }
        public bool isQuestionExists(string question)
        {
            using (SqlConnection connection = new SqlConnection(conecionString))
            {
                connection.Open();
                string checkQuery = "SELECT COUNT(*) FROM Questions WHERE question = @question";
                using (SqlCommand command = new SqlCommand(checkQuery, connection))
                {
                    command.Parameters.AddWithValue("@question", question);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public void PrintQuestionsAddCollectAnswers()
        {

            Utility utility = new Utility();
            List<Tuple<int, int, int>> userAnswers = new List<Tuple<int, int, int>>();

            using (SqlConnection connection = new SqlConnection(conecionString)) 
            {
                connection.Open ();

                string printQuery = "SELECT * FROM Questions";

                using(SqlCommand command = new SqlCommand(printQuery, connection)) 
                {

                    try 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())

                            while (reader.Read())
                            {
                                WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                WriteLine($"ID: {reader["id"]}");
                                WriteLine($"Ερώτηση: {reader["question"]}");
                                WriteLine($"Επιλογή 1: {reader["option1"]}");
                                WriteLine($"Επιλογή 2: {reader["option2"]}");
                                WriteLine($"Επιλογή 3: {reader["option3"]}");
                                WriteLine($"Επιλογή 4: {reader["option4"]}");
                              

                                int correctOption = (int)reader["correct_option"];
                                int userAnswer = utility.playerAnswer();

                                userAnswers.Add(new Tuple<int, int, int>((int)reader["id"], userAnswer, correctOption));
                                Console.WriteLine();

                            }
                    }catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("General Error: " + ex.Message);
                    }

                }

            }
            //Ελεγχος
            int correctCount = 0;

            WriteLine("|================================= Απαντήσεις ========================================|\n");
            foreach (var answer in userAnswers){

              
                Console.WriteLine($"Ερώτηση ID: {answer.Item1}, Απάντηση Χρήστη: {answer.Item2}, Σωστή Απάντηση: {answer.Item3}");
                if (answer.Item2 == answer.Item3)
                {
                    correctCount++;
                }
            
            }

          
            WriteLine($"\nΣυνολικές σωστές απαντήσεις: {correctCount} από {userAnswers.Count}");    
            WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            Utility.PlayAgain();


        }





    }
}

using Drivers_License_Test_Practice.AnsuerAndQoyestion;
using Drivers_License_Test_Practice.DbContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Drivers_License_Test_Practice.UI
{
    internal class Utility
    {


        private string conecionString;
        public Utility()
        {
            conecionString = SQLConnection.GetConnectionString();
        }


         

        public  int playerAnswer() 
        {
            int answer; 
            while (true)
            {
                Console.WriteLine("Παρακαλώ δώστε την απάντηση (1-4):");

                if (int.TryParse(Console.ReadLine(), out answer) && answer >= 1 && answer <= 4) 
                {
                    //Αν και οι δύο συνθήκες είναι αληθείς,
                    //τότε η είσοδος είναι έγκυρη και βγαίνει από τον βρόχο while.
                    //Αν κάποια από τις συνθήκες είναι ψευδής, εμφανίζεται μήνυμα σφάλματος και
                    //ζητείται από τον χρήστη να προσπαθήσει ξανά.
                    break;
                }
                Console.WriteLine("Μη έγκυρη είσοδος. Δοκιμάστε ξανά.");
            }
            return answer;

        }


        public static void Consoleframe() {
            if (OperatingSystem.IsWindows())
                Console.SetWindowSize(118, 40);
        }

        public static void PressEnter() 
        {
            Console.WriteLine("\n \n \nΠάτα Enter για συνέχεια.....");
            Console.ReadLine();
        }

        public static void OutMassege() 
        {
            WriteLine("\r\nΣας ευχαριστούμε που επιλέξατε την εφαρμογή μας.\r\n");
        }


        public static string HidePassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(intercept: true);

                // Χειρισμός του πλήκτρου Backspace
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        // Διαγραφή του τελευταίου χαρακτήρα στη συμβολοσειρά και στην κονσόλα
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b"); // Διαγραφή του αστερίσκου από την κονσόλα
                    }
                }
                // Χειρισμός του πλήκτρου Enter
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(); // Νέα γραμμή μετά την εισαγωγή του κωδικού
                    break; // Τερματισμός της λούπας
                }
                // Χειρισμός άλλων πλήκτρων
                else if (!char.IsControl(keyInfo.KeyChar) && !char.IsWhiteSpace(keyInfo.KeyChar))
                {
                    // Πρόσθεση του χαρακτήρα στη συμβολοσειρά του κωδικού
                    password += keyInfo.KeyChar;
                    // Εμφάνιση αστερίσκου στην κονσόλα
                    Console.Write("*");
                }
            } while (true);

            Console.WriteLine();  
            return password;
        }


        public static void PlayAgain() 
        {
            QuestionAnswer newQuestionAnswer = new QuestionAnswer();
            string answer;
            Console.WriteLine("Θες να ξανα προσπαθήσης? (yes/no)");
            answer = Console.ReadLine().ToLower().Trim();
         
            if (answer == "yes") 
            {
                Clear();
                Console.WriteLine("\n\r\n\r|==================================================== Ερωτήσεις =====================================================|\n\r\n\r");
                UI_Login.GreatAndAddQuestions();

                newQuestionAnswer.PrintQuestionsAddCollectAnswers();
                Utility.PressEnter();
            }else if (answer == "no") 
            {
                Clear();
                AppScreen.Wellcome();
                AppScreen.DisplayAppMenu();
            }else
            {
                Console.WriteLine("Η απαντηση σου δεν είναι σωστη\n");
                PlayAgain();
            }
        }
        public static void PrintDotAnimation(int timer = 10) 
        {
            for (int i = 0; i < timer; i++) {
                Write(".");
                Thread.Sleep(200);
            }
        }

    }
}

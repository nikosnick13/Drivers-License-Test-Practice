using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace Drivers_License_Test_Practice.UI
{
    internal class AppScreen
    {

        public static void DisplayAppMenu() 
        {
            bool isRunning = false;
            int User;
            do 
            {              
               

                WriteLine("1-----Σύνδεση");
                WriteLine("2-----Δημιουργήστε νέο λογαριασμό");
                WriteLine("3-----Εξοδος");

                WriteLine("\nΠατήστε μια απο τις επιλογές (1, 2 ή 3) και πατήστε Enter:\n");

               

                if (int.TryParse(ReadLine(), out User) && User >= 1 && User <= 3)
                {
                    switch (User)
                    {
                        case 1:
                            UI_Login.DassboardLogin();
                            break;
                        case 2:
                            UI_Login.DashboardCreatNewUser();
                            break;
                        case 3:
                            isRunning = true;
                            Utility.OutMassege();
                            break;
                        default:
                            isRunning = true;
                            break;
                    }
                }
                else {
                    Console.WriteLine("\nΠαρακαλώ ξανά δώστε μια σωστή απάντηση\n");
                    DisplayAppMenu();

                    Utility.PressEnter();
                }
              
            }while (isRunning);
            


        }
        public static void  Wellcome() 
        {
            Console.Clear();

            Console.Title = "Εφαρμογή Εξάσκησης Δοκιμών Άδειας οδήγησης";
            
            Console.ForegroundColor= ConsoleColor.Yellow;


            // width and hight in console 
            //ελεγχος για το λιτουργικό συστημα ειναι windows 
            //Αλλιος θα εβγαζε warring στο  Console.SetWindowSize(118, 40);

            Utility.Consoleframe();
                                                                                                                                       

            Console.WriteLine("\n \n \n|======================= Γεια σας Καλώς ορίσατε στην Εφαρμογή Εξάσκησης Δοκιμών Άδειας οδήγησης =======================|\n\n\n");

            Console.WriteLine("Η καλύτερη και πληρέστερη εφαρμογή για το δίπλωμα αυτοκινήτου.\r\n\r\nΠεριλαμβάνει όλα τα τεστ (30 τεστ) με τα σήματα και τις μηχανολογικές ερωτήσεις του ΥΜΕ.");
            Console.WriteLine("\r\n\nΠαρέχει την δυνατότητα αποθήκευσης των δύσκολων ερωτήσεων \r\nκαθώς επίσης και την αυτόματη αποθήκευση των λανθασμένων απαντήσεων για γρήγορη εκμάθηση.");

            Utility.PressEnter();
        }

       



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyTraderClt_Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            int startChoice = 0;
            while (startChoice > 2 || startChoice < 1)
            {
                Console.Clear();
                startChoice = ShowWelcome();                   
            } 
                switch (startChoice)
                {
                    case 1: // LuckyTrader starten
                        Console.WriteLine("Starte LuckyTrader .....\nlade Login .....");
                        ShowLogin();
                        break;
                    case 2: // Application beenden
                        Console.WriteLine("Danke für das Interesse - auf Wiedersehen.");
                        break;
                    default:
                        Console.WriteLine("FALSCHE EINGABE");
                        break;
                }
            Console.ReadLine();
        }

        private static void ShowLogin()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 61));
            Console.WriteLine("#####\t\tWillkommen zu Lucky-Trader\t\t#####");
            Console.WriteLine(new string('=', 61));
            Console.WriteLine("\n1)\tBei \"Lucky-Trader\" einloggen.");
            Console.WriteLine("2)\tLucky-Trader beenden");
            Console.WriteLine("\nBitte treffe eine Wahl: ");

            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.D1)
                DoLoginScreen();
            else if (key == ConsoleKey.D2)
                return;
        }

        private static void DoLoginScreen()
        {
            Console.WriteLine("LOGIN");
            Console.ReadLine();
        }

        private static int ShowWelcome()
        {
            ShowStartMenu();
            if (Console.ReadKey().Key == ConsoleKey.D1)
            {
                return 1;
            }
            else if (Console.ReadKey().Key == ConsoleKey.D2)
            {
                return 2;
            }
            else
                return 0;
        }

        private static void ShowStartMenu()
        {
            Console.WriteLine(new string('=', 61));
            Console.WriteLine("#####\t\tWillkommen zu Lucky-Trader\t\t#####");
            Console.WriteLine(new string('=', 61));
            Console.WriteLine("\n1)\tBeginnt das Spiel \"LuckyTrader\"");
            Console.WriteLine("2)\tBeendet die Eingabeaufforderung");
            Console.WriteLine("\nBitte treffe eine Wahl: ");
        }
    }
}

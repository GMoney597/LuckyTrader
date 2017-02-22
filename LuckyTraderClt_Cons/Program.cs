using LuckyTraderClt_Cons.LTSvc;
using LuckyTraderLib;
using System;

namespace LuckyTraderClt_Cons
{
    class Program
    {
        static LuckyTraderClient ltClient = new LuckyTraderClient();
        static internal int logID = 0;
        static PlayerClass player;

        static void Main(string[] args)
        {
            StartSequence();
        }

        private static void EndMain()
        {
            Console.WriteLine("\nProgramm wird beendet");
            Console.ReadLine();
            if (logID != 0)
                ltClient.DoLogout(logID);
        }

        private static void StartSequence()
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
                    Console.WriteLine("\nStarte LuckyTrader .....");
                    try
                    {
                        if (ltClient.GetServerState())
                        {
                            Console.WriteLine("\nlade Login .....");
                            ShowLogReg();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Server nicht verfügbar");
                    }                 
                    break;
                case 2: // Application beenden
                    Console.WriteLine("\nDanke für das Interesse - auf Wiedersehen.");
                    break;
                default:
                    Console.WriteLine("\nFALSCHE EINGABE");
                    break;
            }
        }

        private static void ShowLogReg()
        {
            Header();
            Console.WriteLine("1)\tBei \"Lucky-Trader\" einloggen.");
            Console.WriteLine("2)\tBei Lucky-Trader registrieren.");
            Console.WriteLine("3)\tLucky-Trader beenden");
            Console.WriteLine("\nBitte treffe eine Wahl: ");

            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
                DoLoginScreen();
            else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
                DoRegisterScreen();
            else if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3)
                EndMain();
            else
                ShowLogReg();
        }

        private static void DoRegisterScreen()
        {
            int registerSuccess = -1;

            Header();
            Console.WriteLine("Zur Registrierung sind einige Angaben notwendig:");
            Console.WriteLine();
            Console.Write("Bitte gib einen Benutzernamen ein: ");
            string user = Console.ReadLine();
            Console.Write("Bitte gib ein Passwort ein: ");
            string pass = Console.ReadLine();
            Console.Write("Bitte gib Deine Mail-Adresse ein: ");
            string mail = Console.ReadLine();
            Console.Write("Bitte gib Deinen Vornamen ein: ");
            string firstName = Console.ReadLine();
            Console.Write("Bitte gib Deinen Nachnamen ein: ");
            string lastName = Console.ReadLine();
            Console.Write("Bitte gib nur noch dein Geburtsdatum ein (dd-mm-yyyy): ");
            string[] birth = new string[3];
            birth = (Console.ReadLine().Split('-'));
            DateTime birthdate = new DateTime();
            try
            {
                birthdate = new DateTime(Convert.ToInt16(birth[2]), Convert.ToInt16(birth[1]), Convert.ToInt16(birth[1]));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kann Datum nicht erkennen ---> Registrierung wird wiederholt");
                DoRegisterScreen();
            }

            Console.WriteLine();
            registerSuccess = ltClient.DoRegister(user, pass, mail, firstName, lastName, birthdate);

            HandleRegisterSuccess(registerSuccess);
        }

        private static void HandleRegisterSuccess(int registerSuccess)
        {
            switch (registerSuccess)
            {
                case 0:
                    Console.ReadLine();
                    CheckContinue("Login", true);
                    break;
                case 1:
                    Console.WriteLine("Mindestens ein Feld ist ohne WERT oder Datum ist falsch");
                    Console.ReadLine();
                    CheckContinue("Register", false);
                    break;         
                case 2:
                    Console.WriteLine("Mindestens ein Feld ist leer oder enthält nur Leerzeichen");
                    Console.ReadLine();
                    CheckContinue("Register", false);
                    break;         
                case 3:
                    Console.WriteLine("Bitte einen Usernamen mit mehr als 5 Zeichen angeben");
                    Console.ReadLine();
                    CheckContinue("Register", false);
                    break;
                case 4:
                    Console.WriteLine("Passwort ist nicht mindestens 8 Zeichen lang");
                    Console.ReadLine();
                    CheckContinue("Register", false);
                    break;
                case 5:
                    Console.WriteLine("Keine gültige Mail-Adresse angegeben");
                    Console.ReadLine();
                    CheckContinue("Register", false);
                    break;
                case 6:
                    Console.WriteLine("Ungültiges Datum gewählt");
                    Console.ReadLine();
                    CheckContinue("Register", false);
                    break;
                case 7:
                    Console.WriteLine("Interner Fehler - bitte melden");
                    Console.ReadLine();
                    CheckContinue("Register", false);
                    break;
                case 9:
                    Console.WriteLine("User bereits vorhanden - bitte einloggen");
                    Console.ReadLine();
                    CheckContinue("Login", true);
                    break;
                default:
                    Console.WriteLine("Nicht identifizierter Fehler aufgetreten");
                    Console.ReadLine();
                    break;
            }
        }

        private static void CheckContinue(string next, bool accept)
        {
            Header();
            if (next == "Login" && accept)
            {
                Console.WriteLine("Registrierung erfolgreich");
                Console.WriteLine("Weiter zum Login?");
                Console.Write("(J)a oder (N)ein ---> Nein, beendet das Programm");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.J)
                {
                    DoLoginScreen();
                }
                else if (key == ConsoleKey.N)
                {
                    EndMain();
                }
                else
                    EndMain();
            }
            else if (next == "Login" && !accept)
            {
                Console.WriteLine("Login wurde wegen Fehler abgebrochen");
                Console.WriteLine("Login wiederholen?");
                Console.Write("(J)a oder (N)ein ---> Nein, beendet das Programm");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.J)
                {
                    DoLoginScreen();
                }
                else if (key == ConsoleKey.N)
                {
                    EndMain();
                }
            }
            else if (next == "Register" && false)
            {
                Console.WriteLine("Fehler bei der Registrierung festgestellt");
                Console.WriteLine("Registrierung wiederholen?");
                Console.Write("(J)a oder (N)ein ---> Nein, beendet das Programm");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.J)
                {
                    DoRegisterScreen();
                }
                else if (key == ConsoleKey.N)
                {
                    EndMain();
                }
            }
            else
                EndMain();
        }

        private static void DoLoginScreen()
        {
            bool loginSuccess = false;
            player = new PlayerClass();
            string user = "";
            string pass = "";
            bool dataCorrect = false;

            Header();
            LoginHeader();

            Console.WriteLine("Bitte gib Deine Logindaten ein.");
            Console.Write("Username: ");
            user = Console.ReadLine();
            Console.Write("Password: ");
            pass = Console.ReadLine();
            dataCorrect = CheckEnteredData(user, pass);
            if (dataCorrect)
            {
                player = ltClient.DoLogin(user, pass);
                user = "";
                pass = "";
                loginSuccess = player.loginSuccess;
                logID = player.loginID;
            }
            else
                CheckContinue("Login", true);

            if (loginSuccess)
            {
                Console.WriteLine("Login erfolgreich - LogID: " + logID);
                Console.WriteLine("Weiter mit <ENTER>");
                Console.ReadLine();
                PlayerMenu();
            }
            else
                Console.WriteLine("Fehler bei der Anmeldung");
        }

        private static bool CheckEnteredData(string user, string pass)
        {
            if (user == "" || pass == "")
            {
                Console.WriteLine("Es müssen Daten eingegeben werden");
                return false;
            }
            return true;
        }

        private static void LoginHeader()
        {
            Console.WriteLine(new string('-', 61));
            Console.WriteLine(new string('-', 5) + "\t\t\t   LOGIN   \t\t\t" + new string('-', 5));
            Console.WriteLine(new string('-', 61));
            Console.WriteLine();
        }

        private static void PlayerMenu()
        {
            Header();
            Console.ReadLine();
            MenuHeader();
            ConsoleKey choice = new ConsoleKey();
            choice = Console.ReadKey().Key;

            if (choice == ConsoleKey.K)
            {
                // Kontostand anzeigen
                player.assets = ltClient.UpdatePlayerAssets(player.userName);
            }
            else if (choice == ConsoleKey.B)
            {
                // Börsentabelle anzeigen
            }
            else if (choice == ConsoleKey.A)
            {
                // Aktien handeln
            }
        }

        private static void MenuHeader()
        {
            Console.WriteLine(new string('=', 61));
            Console.WriteLine("#####\t\tDein Hauptmenü\t\t#####");
            Console.WriteLine(new string('=', 61));
            Console.WriteLine();
            Console.WriteLine("K) Kontostand anzeigen");
            Console.WriteLine("B) Börsentabelle anzeigen");
            Console.WriteLine("A) Aktien handeln");
            Console.WriteLine("x) Jede andere Eingabe beendet das Programm");
            Console.Write("\nBitte wählen: ");
        }

        private static int ShowWelcome()
        {
            ShowStartMenu();

            ConsoleKey consKey = Console.ReadKey().Key;

            if (consKey == ConsoleKey.S)
            {
                return 1;
            }
            else if (consKey == ConsoleKey.E)
            {
                return 2;
            }
            else return 0;
        }

        private static void ShowStartMenu()
        {
            Header();
            Console.WriteLine("S)\tBeginnt das Spiel \"LuckyTrader\"");
            Console.WriteLine("E)\tBeendet die Eingabeaufforderung");
            Console.WriteLine("\nBitte treffe eine Wahl: ");
        }

        private static void Header()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 61));
            Console.WriteLine("#####\t\tWillkommen zu Lucky-Trader\t\t#####");
            Console.WriteLine(new string('=', 61));
            Console.WriteLine();
        }
    }
}

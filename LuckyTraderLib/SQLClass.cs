using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace LuckyTraderLib
{
    internal class SQLClass
    {
        SqlConnection sqlCon = null;
        SqlCommand sqlCom = null;
        SqlDataAdapter sqlDA = null;
        ErrorMessage err = new ErrorMessage();
        LoginMessage log = new LoginMessage();
        DataTable dt = null;

        public SQLClass()
        {
            if (Dns.GetHostName() == "Gamer")
            {
                // Verbindung für den Gamer-PC steht
                sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename =D:\Projects\Datenbanken\LuckyTrader.mdf;
                    Integrated Security=True;
                    Connect Timeout=30");
            }
            else if (Dns.GetHostName() == "DT-Sarnoch1")
            {
                // das ist die ursprüngliche Verbindung am Dienst-PC
                sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; 
                    AttachDbFilename=C:\Users\Sarnoch T\Documents\Visual Studio 2015\Projects\Databases\LuckyTrader.mdf;
                    Integrated Security = True; 
                    Connect Timeout = 30");
            }
            else if (Dns.GetHostName() == "Developer")
            {
                //sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; 
                //    AttachDbFilename=C:\Users\Sarnoch T\Documents\Visual Studio 2015\Projects\Databases\LuckyTrader.mdf;
                //    Integrated Security = True; 
                //    Connect Timeout = 30");
            }
        }

        public bool DoConnectionTest()
        {
            try
            {
                sqlCon.Open();
                sqlCon.Close();
                return true;
            }
            catch (Exception ex)
            {
                err.WriteErrorLog("0008", "Keine Verbindung zur Datenbank möglich.");
                return false;
            }
        }

        /// <summary>
        /// Try to register User with given parameters
        /// </summary>
        /// <param name="UN">Username</param>
        /// <param name="PW">Password</param>
        /// <param name="Mail">Mail-Address</param>
        /// <param name="FN">First Name</param>
        /// <param name="LN">Last Name</param>
        /// <param name="Birth">Birthdate</param>
        /// <returns>Returns true if register process is successful and false if not</returns>
        internal int SQLReg(string UN, string PW, string Mail, string FN, string LN, DateTime Birth)
        {
            #region ErrorHandling
            if (UN == null || PW == null || Mail == null || FN == null || LN == null || Birth == DateTime.Today)
            {
                err.WriteErrorLog("0001", "Mindestens ein Feld ist ohne WERT oder Datum ist falsch");
                return 1;
            }
            else if (UN == "" || PW == "" || Mail == "" || FN == "" || LN == "")
            {
                err.WriteErrorLog("0002", "Mindestens ein Feld ist leer oder enthält nur Leerzeichen");
                return 2;
            }
            else if (UN.Length < 5)
            {
                err.WriteErrorLog("0003", "Bitte einen Usernamen mit mehr als 5 Zeichen angeben");
                return 3;
            }
            else if (PW.Length < 8)
            {
                err.WriteErrorLog("0005", "Passwort ist nicht mindestens 8 Zeichen lang");
                return 5;
            }
            else if (Mail.IndexOf('@') < 0)
            {
                err.WriteErrorLog("0006", "Keine gültige Mail-Adresse angegeben");
                return 6;
            }
            else if (Birth == new DateTime(1970, 01, 01))
            {
                err.WriteErrorLog("0007", "Ungültiges Datum gewählt");
                return 7;
            }
            else if (UserAlreadyExists(UN, Mail))
            {
                err.WriteErrorLog("0009", "User bereits vorhanden - bitte einloggen");
                return 9;
            }
            #endregion

            #region Attempt to register
            try
            {
                sqlCom = new SqlCommand("INSERT INTO table_Registries(Reg_User, Reg_Pass, Reg_Mail, Reg_FirstName, Reg_LastName, Reg_Birth)" +
                    "VALUES (@UN, @PW, @Mail, @FN, @LN, @Birth)", sqlCon);
                sqlCom.Parameters.AddWithValue("@UN", UN);
                sqlCom.Parameters.AddWithValue("@PW", PW);
                sqlCom.Parameters.AddWithValue("@Mail", Mail);
                sqlCom.Parameters.AddWithValue("@FN", FN);
                sqlCom.Parameters.AddWithValue("@LN", LN);
                sqlCom.Parameters.AddWithValue("@Birth", Birth);
                sqlCon.Open();
                sqlCom.ExecuteNonQuery();
                sqlCon.Close();

                log.WriteLogin("0000", "Registrierung von " + UN + " erfolgreich");
                return 0;
            }
            catch (Exception ex)
            {
                err.WriteErrorLog("9009", "Unbehandelter Fehler - Registrierung ist fehlgeschlagen.");
                return 9009;
            }
            #endregion
        }

        private bool UserAlreadyExists(string uN, string mail)
        {
            //int count = 0;
            sqlCom = new SqlCommand("SELECT Reg_ID FROM table_Registries WHERE Reg_User = @UN", sqlCon);
            sqlCom.Parameters.AddWithValue("@UN", uN);
            sqlCon.Open();
            sqlDA = new SqlDataAdapter(sqlCom);
            sqlDA.Fill(dt = new DataTable());
            sqlCon.Close();

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal int SQLLog()
        {
            throw new NotImplementedException();
        }
    }
}
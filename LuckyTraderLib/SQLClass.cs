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

        internal int SQLLogIn(string uN, string pW)
        {
            sqlCom = new SqlCommand("SELECT Reg_ID, Reg_User, Reg_Pass FROM table_Registries", sqlCon);
            sqlCon.Open();
            sqlDA = new SqlDataAdapter(sqlCom);
            sqlDA.Fill(dt = new DataTable());
            sqlCon.Close();

            foreach (DataRow row in dt.Rows)
            {
                if (row.ItemArray[1].ToString() == uN)
                {
                    if (row.ItemArray[2].ToString() == pW)
                    {
                        int playerLogID = DoWriteTableLogin((int)row.ItemArray[0], DateTime.Now);
                        log.WriteLogin("0010", "User-Login erfolgreich");
                        return playerLogID;
                    }
                    else
                    {
                        err.WriteErrorLog("0012", "Passwort nicht korrekt");
                        return 12;
                    }
                }
                else
                {
                    err.WriteErrorLog("0011", "Username nicht gefunden");
                    return 11;
                }
            }
            return 9008;
        }

        internal DataTable GetStock()
        {
            sqlCom = new SqlCommand("SELECT Share_Title, Share_Amount, Share_Buy, Share_Sell FROM table_Stock", sqlCon);
            sqlDA = new SqlDataAdapter(sqlCom);
            sqlCon.Open();
            sqlDA.Fill(dt = new DataTable());
            sqlCon.Close();

            return dt;
        }

        internal void SQLLogOut(int login_ID)
        {
            //UPDATE NACHSCHLAGEN
            sqlCom = new SqlCommand("UPDATE table_Logins  SET Log_DateTime_Off = @logout_now WHERE Log_ID = @login_ID", sqlCon);
            sqlCom.Parameters.AddWithValue("@logout_now", DateTime.Now);
            sqlCom.Parameters.AddWithValue("@login_ID", login_ID);
            sqlCon.Open();
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();
        }

        internal void DoAutoUpdateStock()
        {
            sqlCom = new SqlCommand("SELECT * FROM table_Stock", sqlCon);
            sqlDA = new SqlDataAdapter(sqlCom);
            sqlCon.Open();
            sqlCom.ExecuteNonQuery();
            sqlDA.Fill(dt = new DataTable());
            sqlCon.Close();

            foreach (DataRow row in dt.Rows)
            {
                Random updateBuy = new Random((int)DateTime.Now.Ticks);
                int rndBuySeq = updateBuy.Next(1,500);
                if ((rndBuySeq % 2) == 0)
                {
                    UpdatePrice_Auto(row, true);
                }
                else
                {
                    UpdatePrice_Auto(row, false);
                }
            }
        }

        private void UpdatePrice_Auto(DataRow stock, bool raise)
        {
            string shareTitle = stock.ItemArray[1].ToString();
            double shareBuy = Convert.ToDouble(stock.ItemArray[3]);
            double shareSell = Convert.ToDouble(stock.ItemArray[4]);
            int highCounter = Convert.ToInt16(stock.ItemArray[5]);
            int lowCounter = Convert.ToInt16(stock.ItemArray[6]);
            Random decValue = new Random();
            double percValue = decValue.NextDouble() / 100;

            if (raise && highCounter < 5)
            {
                shareBuy += shareBuy * percValue;
                lowCounter--;
                highCounter++;
            }
            else if (raise && lowCounter < 5)
            {
                shareBuy -= shareBuy * percValue;
                highCounter--;
                lowCounter++;  
            }
            else if (!raise && lowCounter < 5)
            {
                shareBuy -= shareBuy * percValue;
                highCounter--;
                lowCounter++;
            }
            else if (!raise && highCounter < 5)
            {
                shareBuy += shareBuy * percValue;
                lowCounter--;
                highCounter++;
            }
            else
            {
                highCounter -= 2;
                lowCounter -= 2;
            }

            if (highCounter < 0)
                highCounter = 0;
            if (lowCounter < 0)
                lowCounter = 0;

            shareSell = shareBuy * 0.95;

            DoStockTableUpdate(shareTitle, shareBuy, shareSell, highCounter, lowCounter);
        }

        private void DoStockTableUpdate(string shareTitle, double shareBuy, double shareSell, int highCounter, int lowCounter)
        {
            sqlCom = new SqlCommand("UPDATE table_Stock SET Share_Buy = @shareBuy, Share_Sell = @shareSell, Share_Higher = @highCounter, Share_Lower = @lowCounter " + 
                "WHERE Share_Title = @shareTitle", sqlCon);
            sqlCom.Parameters.AddWithValue("@shareBuy", shareBuy);
            sqlCom.Parameters.AddWithValue("@shareSell", shareSell);
            sqlCom.Parameters.AddWithValue("@highCounter", highCounter);
            sqlCom.Parameters.AddWithValue("@lowCounter", lowCounter);
            sqlCom.Parameters.AddWithValue("@shareTitle", shareTitle);
            sqlCon.Open();
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();
        }

        private int DoWriteTableLogin(int uID, DateTime now)
        {
            sqlCom = new SqlCommand("INSERT INTO table_Logins (Log_User_ID, Log_DateTime_On) VALUES (@uID, @now)", sqlCon);
            sqlCom.Parameters.AddWithValue("@uID", uID);
            sqlCom.Parameters.AddWithValue("@now", now);
            sqlCon.Open();
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();
            sqlCom = new SqlCommand("SELECT Log_ID FROM table_Logins WHERE Log_User_ID = @uID AND Log_DateTime_On = @now", sqlCon);
            sqlCom.Parameters.AddWithValue("@uID", uID);
            sqlCom.Parameters.AddWithValue("@now", now);
            sqlCon.Open();
            sqlDA = new SqlDataAdapter(sqlCom);
            sqlDA.Fill(dt = new DataTable());
            sqlCon.Close();
            return Convert.ToInt16(dt.Rows[0].ItemArray[0]);
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

                PlayerClass pCl = new PlayerClass(UN, FN, LN, Birth);

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
    }
}
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
        SqlDataReader sqlDR = null;
        ErrorMessage err = new ErrorMessage();
        LoginMessage log = new LoginMessage();
        DataTable dt = null;
        ExceptionMessage exM = new ExceptionMessage();

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

        #region Allgemeine Wiederkehrende Abfragen / Funktionen  
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
                exM.DoWriteExceptionLog(ex);
                err.WriteErrorLog("0008", "Keine Verbindung zur Datenbank möglich.");
                return false;
            }
        }
        private int GetUserID(string uN)
        {
            int UserID = -1;
            sqlCom = new SqlCommand("SELECT Reg_ID FROM table_Registries WHERE Reg_User = @UN", sqlCon);
            sqlCom.Parameters.AddWithValue("@UN", uN);
            sqlCon.Open();
            sqlDR = sqlCom.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    UserID = sqlDR.GetInt32(0);
                }
            }
            sqlDR.Close();
            sqlCon.Close();
            return UserID;
        }                      
        #endregion

        #region Registrierung neuer Spieler
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
                DoInsertTableUser(GetUserID(UN), "", 1000m, 0m);

                log.WriteLogin("0000", "Registrierung von " + UN + " erfolgreich");
                return 0;
            }
            catch (Exception ex)
            {
                exM.DoWriteExceptionLog(ex);
                err.WriteErrorLog("9009", "Unbehandelter Fehler - Registrierung ist fehlgeschlagen.");
                return 9009;
            }
            #endregion
        }
        #endregion

        #region Log-Verhalten der Spieler
        internal int SQLLogIn(string uN, string pW)
        {
            sqlCom = new SqlCommand("SELECT Reg_User, Reg_Pass FROM table_Registries", sqlCon);
            sqlCon.Open();
            sqlDA = new SqlDataAdapter(sqlCom);
            sqlDA.Fill(dt = new DataTable());
            sqlCon.Close();

            foreach (DataRow row in dt.Rows)
            {
                if (row.ItemArray[0].ToString() == uN)
                {
                    if (row.ItemArray[1].ToString() == pW)
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
        #endregion

        #region Aktien-Funktionen wiederkehrend
        internal DataTable GetStock()
        {
            sqlCom = new SqlCommand("SELECT Share_Title, Share_Amount, Share_Buy, Share_Sell FROM table_Stock", sqlCon);
            sqlDA = new SqlDataAdapter(sqlCom);
            sqlCon.Open();
            sqlDA.Fill(dt = new DataTable());
            sqlCon.Close();

            return dt;
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
                int rndBuySeq = updateBuy.Next(1, 500);
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
        internal bool CheckPrice(string shareTitle, decimal sharePrice, string tradeType)
        {
            decimal shaBuy = 0m;
            decimal shaSell = 0m;

            sqlCom = new SqlCommand("SELECT Share_Buy, Share_Sell FROM table_Stock WHERE Share_Title = @shaTitle", sqlCon);
            sqlCom.Parameters.AddWithValue("@shaTitle", shareTitle);
            sqlCon.Open();
            sqlDR = sqlCom.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    shaBuy = sqlDR.GetDecimal(0);
                    shaSell = sqlDR.GetDecimal(1);
                }
            }
            sqlDR.Close();
            sqlCon.Close();

            if (tradeType == "buy" && sharePrice == shaBuy)
                return true;
            else if (tradeType == "sell" && sharePrice == shaSell)
                return true;
            else
                return false;
        }
        private void UpdatePrice_Trade(int sID, decimal sharePrice, int shareAmount, string tradeType)
        {
            double shP = Convert.ToDouble(sharePrice);
            int actAmount = 0;

            Random updSeq = new Random();
            for (int i = 0; i < 10; i++)
            {
                double updPerc = updSeq.NextDouble() / 100;
                if (tradeType == "buy")
                    shP += shP * updPerc;
                else
                    shP -= shP * updPerc;
            }

            decimal shareBuy = Convert.ToDecimal(shP);
            decimal shareSell = Convert.ToDecimal(shP * 0.95);

            sqlCom = new SqlCommand("SELECT Share_Amount FROM table_Stock WHERE Share_ID = @sID", sqlCon);
            sqlCom.Parameters.AddWithValue("@sID", sID);
            sqlCon.Open();
            sqlDR = sqlCom.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    actAmount = sqlDR.GetInt32(0);
                }
            }
            sqlDR.Close();

            if (tradeType == "buy")
                actAmount -= shareAmount;
            else
                actAmount += shareAmount;

            sqlCom = new SqlCommand("UPDATE table_Stock SET Share_Amount = @shaAmount, Share_Buy = @shaBuy, Share_Sell = @shaSell, Share_Higher = 0, Share_Lower = 9 " +
                "WHERE Share_ID = @sID", sqlCon);
            sqlCom.Parameters.AddWithValue("@shaAmount", actAmount);
            sqlCom.Parameters.AddWithValue("@shaBuy", shareBuy);
            sqlCom.Parameters.AddWithValue("@shaSell", shareSell);
            sqlCom.Parameters.AddWithValue("@sID", sID);
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();
        }
        private int GetShareID(string shareTitle)
        {
            int shareID = -1;
            sqlCom = new SqlCommand("SELECT Share_ID FROM table_Stock WHERE Share_Title = @shTitle", sqlCon);
            sqlCom.Parameters.AddWithValue("@shTitle", shareTitle);
            sqlCon.Open();
            sqlDR = sqlCom.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    shareID = sqlDR.GetInt32(0);
                }
            }
            sqlDR.Close();
            sqlCon.Close();
            return shareID;
        }

        #endregion

        #region Aktualisierungen wiederkehrend zum Spieler
        internal decimal DoAssetsUpdate(string uN)
        {
            int uID = GetUserID(uN);

            sqlCom = new SqlCommand("SELECT * FROM table_Player_Shares WHERE Reg_User_ID = @uID", sqlCon);
            sqlCom.Parameters.AddWithValue("@uID", uID);
            sqlDA = new SqlDataAdapter(sqlCom);
            sqlCon.Open();
            sqlDA.Fill(dt = new DataTable());
            sqlCon.Close();

            decimal assetsSum = 0m;
            decimal shareSell = 0m;

            foreach (DataRow row in dt.Rows)
            {
                shareSell = DoPlayerShareSellUpdate(Convert.ToInt16(row.ItemArray[2]));
                assetsSum += Convert.ToInt16(row.ItemArray[3]) * shareSell;
            }

            sqlCom = new SqlCommand("UPDATE table_Players SET Play_Assets = @assets WHERE Play_User_ID = @uID", sqlCon);
            sqlCom.Parameters.AddWithValue("@assets", assetsSum);
            sqlCom.Parameters.AddWithValue("@uID", uID);
            sqlCon.Open();
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();

            return assetsSum;
        }
        internal bool DoResyncUserData(string uN, string loc, decimal cash, decimal assets)
        {
            int UserID = GetUserID(uN);
            sqlCom = new SqlCommand("SELECT Play_Cash, Play_Assets FROM table_Players WHERE Play_User_ID = @UserID", sqlCon);
            sqlCom.Parameters.AddWithValue("@UserID", UserID);
            sqlCon.Open();
            sqlDR = sqlCom.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    if (sqlDR.GetDecimal(0) == cash && sqlDR.GetDecimal(1) == assets)
                        return true;
                }
            }

            sqlDR.Close();
            sqlCon.Close();
            return false;
        }
        private void DoPlayerUpdate(int uID, decimal tradeSum, string tradeType)
        {
            decimal cash = 0m;
            decimal assets = 0m;

            sqlCom = new SqlCommand("SELECT * FROM table_Players WHERE Play_User_ID = @uID", sqlCon);
            sqlCom.Parameters.AddWithValue("@uID", uID);
            sqlCon.Open();
            sqlDR = sqlCom.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    cash = sqlDR.GetDecimal(3);
                    assets = sqlDR.GetDecimal(4);
                }
            }
            sqlDR.Close();

            if (tradeType == "buy")
            {
                cash -= tradeSum;
                assets += tradeSum;
            }
            else
            {
                cash += tradeSum;
                assets -= tradeSum;
            }

            sqlCom = new SqlCommand("UPDATE table_Players SET Play_Cash = @newCash, Play_Assets = @newAssets WHERE Play_User_ID = @uID", sqlCon);
            sqlCom.Parameters.AddWithValue("@newCash", cash);
            sqlCom.Parameters.AddWithValue("@newAssets", assets);
            sqlCom.Parameters.AddWithValue("@uID", uID);
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();
        }

        #endregion

        #region Aktien-Funktionen Spieler
        internal decimal DoStockTrade(string uN, string shareTitle, decimal sharePrice, int shareAmount, string tradeType, int sharePosition)
        {
            try
            {
                int uID = GetUserID(uN);
                int sID = GetShareID(shareTitle);
                int sqlResult = 0;
                int plashaID = sharePosition;

                sqlCom = new SqlCommand("INSERT INTO table_Share_Trades (Sha_Tra_User_ID, Sha_Tra_Share_ID, Share_Price, Share_Trade_Type, Share_ChangeAmount, Share_Trade_Date) " +
                    "VALUES (@uID, @sID, @shPrice, @shType, @shAmount, @shDate)", sqlCon);
                sqlCom.Parameters.AddWithValue("@uID", uID);
                sqlCom.Parameters.AddWithValue("@sID", sID);
                sqlCom.Parameters.AddWithValue("@shPrice", sharePrice);
                sqlCom.Parameters.AddWithValue("@shType", tradeType);
                sqlCom.Parameters.AddWithValue("@shAmount", shareAmount);
                sqlCom.Parameters.AddWithValue("@shDate", DateTime.Now);
                sqlCon.Open();
                sqlResult = sqlCom.ExecuteNonQuery();
                sqlCon.Close();

                if (sqlResult > 0)
                {
                    decimal tradeSum = shareAmount * sharePrice;
                    UpdatePrice_Trade(sID, sharePrice, shareAmount, tradeType);

                    if (tradeType == "buy")
                        InsertPlaShaTable(uID, sID, shareAmount, sharePrice);
                    else
                        UpdatePlaShaTable(plashaID, shareAmount);

                    DoPlayerUpdate(GetUserID(uN), tradeSum, tradeType);
                    return tradeSum;
                }
                return 0m;
            }
            catch (Exception ex)
            {
                exM.DoWriteExceptionLog(ex);
                return 0m;
            }

        }
        private void UpdatePlaShaTable(int plashaID, int shareAmount)
        {
            #region Hole aktuellen Aktienbestand
            int shaAmount = 0;
            sqlCom = new SqlCommand("SELECT Pla_Sha_Amount FROM table_Player_Shares WHERE Pla_Sha_ID = @plashaID", sqlCon);
            sqlCom.Parameters.AddWithValue("@plashaID", plashaID);
            sqlCon.Open();
            sqlDR = sqlCom.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    shaAmount = sqlDR.GetInt32(0);
                }
            }
            sqlDR.Close();
            sqlCon.Close();
            #endregion

            shaAmount -= shareAmount;

            if (shaAmount == 0)
            {
                #region Lösche diesen Aktienbestand vollständig
                sqlCom = new SqlCommand("DELETE FROM table_Player_Shares WHERE Pla_Sha_ID = @plashaID", sqlCon);
                sqlCom.Parameters.AddWithValue("@plashaID", plashaID);
                sqlCon.Open();
                sqlCom.ExecuteNonQuery();
                sqlCon.Close();
                #endregion
            }
            else
            {
                #region Korrigiere neuen Aktienbestand
                sqlCom = new SqlCommand("UPDATE table_Player_Shares SET Pla_Sha_Amount = @shaAmount WHERE Pla_Sha_ID = @psID", sqlCon);
                sqlCom.Parameters.AddWithValue("@psID", plashaID);
                sqlCom.Parameters.AddWithValue("@shaAmount", shaAmount);
                sqlCon.Open();
                sqlCom.ExecuteNonQuery();
                sqlCon.Close();
                #endregion
            }
        }
        private void InsertPlaShaTable(int uID, int sID, int shareAmount, decimal sharePrice)
        {
            sqlCom = new SqlCommand("INSERT INTO table_Player_Shares (Reg_User_ID, Stock_Sha_ID, Pla_Sha_Amount, Pla_Sha_Buy, Pla_Sha_Sell) " +
                " VALUES (@uID, @sID, @shaAmount, @shaBuy, @shaSell)", sqlCon);
            sqlCom.Parameters.AddWithValue("@uID", uID);
            sqlCom.Parameters.AddWithValue("@sID", sID);
            sqlCom.Parameters.AddWithValue("@shaAmount", shareAmount);
            sqlCom.Parameters.AddWithValue("@shaBuy", sharePrice);
            sqlCom.Parameters.AddWithValue("@shaSell", Convert.ToDecimal((double)sharePrice * 0.95));
            sqlCon.Open();
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();
        }
        private decimal DoPlayerShareSellUpdate(int sID)
        {
            decimal shareSell = 0m;

            sqlCom = new SqlCommand("SELECT Share_Sell FROM table_Stock WHERE Share_ID = @sID", sqlCon);
            sqlCom.Parameters.AddWithValue("@sID", sID);
            sqlCon.Open();
            sqlDR = sqlCom.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    shareSell = sqlDR.GetDecimal(0);
                }
            }
            sqlDR.Close();

            sqlCom = new SqlCommand("UPDATE table_Player_Shares SET Pla_Sha_Sell = @shareSell WHERE Stock_Sha_ID = @sID", sqlCon);
            sqlCom.Parameters.AddWithValue("@shareSell", shareSell);
            sqlCom.Parameters.AddWithValue("@sID", sID);
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();

            return shareSell;
        }
        private void DoInsertTableUser(int uID, string uLoc, decimal uCash, decimal uAssets)
        {
            sqlCom = new SqlCommand("INSERT INTO table_Players (Play_User_ID, Play_Location, Play_Cash, Play_Assets) " +
                "VALUES (@uID, @uLoc,  quCash, @uAssets)", sqlCon);
            sqlCom.Parameters.AddWithValue("@uID", uID);
            sqlCom.Parameters.AddWithValue("@uLoc", uLoc);
            sqlCom.Parameters.AddWithValue("@uCash", uCash);
            sqlCom.Parameters.AddWithValue("@uAssets", uAssets);
            sqlCon.Open();
            sqlCom.ExecuteNonQuery();
            sqlCon.Close();
        }    
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LuckyTraderLib
{
    #region Demo-Code
    //// HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "Service1" sowohl im Code als auch in der Konfigurationsdatei ändern.
    //public class Service1 : IService1
    //{
    //    public string GetData(int value)
    //    {
    //        return string.Format("You entered: {0}", value);
    //    }

    //    public CompositeType GetDataUsingDataContract(CompositeType composite)
    //    {
    //        if (composite == null)
    //        {
    //            throw new ArgumentNullException("composite");
    //        }
    //        if (composite.BoolValue)
    //        {
    //            composite.StringValue += "Suffix";
    //        }
    //        return composite;
    //    }
    //}
    #endregion

    public class LuckyTraderService : ILuckyTrader
    {
        int register = 9999;
        Stock stock;
        PlayerClass uLog;
        List<Stock> stockList = new List<Stock>();
        DataTable dt = new DataTable();
        ExceptionMessage exM = new ExceptionMessage();

        // Abgeschlossen
        public PlayerClass DoLogin(string UN, string PW)
        {
            uLog = new PlayerClass();
            uLog.loginSuccess = false;
            uLog.loginID = 0;

            try
            {
                SQLClass sqlLog = new SQLClass();
                if (sqlLog.DoConnectionTest())
                {
                    uLog.loginID = sqlLog.SQLLogIn(UN, PW);
                    uLog.loginSuccess = true;
                    SQLClass sqlPlayer = new SQLClass();
                    uLog = sqlPlayer.SQLPlayerData(UN);
                    return uLog;
                }
                return uLog;
            }
            catch (Exception ex)
            {
                exM.DoWriteExceptionLog(ex);
                return uLog;
            }
        }

        // Abgeschlossen
        public void DoLogout(int LogID)
        {
            try
            {
                SQLClass sqlLog = new SQLClass();
                if (sqlLog.DoConnectionTest())
                {
                    sqlLog.SQLLogOut(LogID);
                }
            }
            catch (Exception ex)
            {
                exM.DoWriteExceptionLog(ex);
            }
        }

        // Abgeschlossen
        public int DoRegister(string UN, string PW, string Mail, string FN, string LN, DateTime Birth)
        {
            try
            {
                SQLClass sqlReg = new SQLClass();
                if (sqlReg.DoConnectionTest())
                {
                    register = sqlReg.SQLReg(UN, PW, Mail, FN, LN, Birth);
                    return register;
                }
                else
                    return 8;
            }
            catch (Exception ex)
            {
                exM.DoWriteExceptionLog(ex);
                return register;
            }
        }

        // Abgeschlossen
        public List<Stock> CheckStock()
        {
            SQLClass sqlStock = new SQLClass();
            dt = sqlStock.GetStock();

            foreach (DataRow row in dt.Rows)
            {
                stock = new Stock();
                stock.shareTitle = row.ItemArray[0].ToString();
                stock.shareAmount = Convert.ToInt16(row.ItemArray[1]);
                stock.shareBuy = Convert.ToDecimal(row.ItemArray[2]);
                stock.shareSell = Convert.ToDecimal(row.ItemArray[3]);
                stockList.Add(stock);
            }

            return stockList;
        }

        // Test-Funktion - bei Freigabe wieder deaktivieren und die andere Funktion einsetzen
        // Abgeschlossen
        public void AutoUpdateStock()
        {
            SQLClass sqlUpdateStock = new SQLClass();
            sqlUpdateStock.DoAutoUpdateStock();
        }

        // Abgeschlossen
        public bool ResyncUserData(string UN, string Loc, decimal Cash, decimal Assets)
        {
            bool syncResult = false;
            SQLClass sqlSync = new SQLClass();
            syncResult = sqlSync.DoResyncUserData(UN, Loc, Cash, Assets);
            return syncResult;
        }

        // Abgeschlossen
        public string DoStockTrade(string UN, string ShareTitle, decimal SharePrice, int ShareAmount, string TradeType, int SharePosition)
        {
            decimal tradeSum = 0m;
            bool equalPrice = false;

            SQLClass sqlTradeStock = new SQLClass();
            equalPrice = CheckSharePrice(ShareTitle, SharePrice, TradeType);
            if (equalPrice)
            {
                tradeSum = sqlTradeStock.DoStockTrade(UN, ShareTitle, SharePrice, ShareAmount, TradeType, SharePosition);
                return "Handel wurde durchgeführt";
            }
            else
            {
                CheckStock();
                return "Handel konnte nicht durchgeführt werden, die Preise wurden aktualisiert";
            }            
        }

        // Abgeschlossen
        private bool CheckSharePrice(string shareTitle, decimal sharePrice, string tradeType)
        {
            bool equal = false;
            SQLClass sqlShare = new SQLClass();
            equal = sqlShare.CheckPrice(shareTitle, sharePrice, tradeType);
            if (equal)
                return true;
            else
                return false;
        }

        // Abgeschlossen
        public decimal UpdatePlayerAssets(string UN)
        {
            decimal assets = 0m;
            SQLClass sqlUserAssets = new SQLClass();
            assets = sqlUserAssets.DoAssetsUpdate(UN);
            return assets;
        }

        // Abgeschlossen
        public bool GetServerState()
        {
            return true;
        }

    }
}

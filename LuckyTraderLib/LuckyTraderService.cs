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
        int login = 9998;
        Stock stock;
        List<Stock> stockList = new List<Stock>();
        DataTable dt = new DataTable();

        // Abgeschlossen
        public int DoLogin(string UN, string PW)
        {
            try
            {
                SQLClass sqlLog = new SQLClass();
                if (sqlLog.DoConnectionTest())
                {
                    login = sqlLog.SQLLogIn(UN, PW);
                    return login;
                }
                else
                    return 8;
            }
            catch (Exception ex)
            {
                return login;
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
            {}
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
                return register;
            }
        }

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

            //public void AutoUpdateStock()
            //{
            //    Stopwatch timer = new Stopwatch();
            //    timer.Start();
            //    if (timer.Elapsed == TimeSpan.FromMinutes(5))
            //    {
            //        SQLClass sqlUpdateStock = new SQLClass();
            //        sqlUpdateStock.DoAutoUpdateStock();
            //        timer.Restart();
            //    }
            //}


        }
    }

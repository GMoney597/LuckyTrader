using System;
using System.Collections.Generic;
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
        int login = 9999;

        public bool DoLogin(string UN, string PW)
        {
            try
            {
                SQLClass sqlLog = new SQLClass();
                //err = sqlLog.SQLLog();
                return true;
            }
            catch (Exception ex)
            {
                //string errorString = err.ErrorString;
                return false;
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
                return register;
            }
        }
    }
}

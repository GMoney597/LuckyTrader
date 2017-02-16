using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LuckyTraderLib
{
    #region DEMO-Code
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "IService1" sowohl im Code als auch in der Konfigurationsdatei ändern.
    //[ServiceContract]
    //public interface IService1
    //{
    //    [OperationContract]
    //    string GetData(int value);

    //    [OperationContract]
    //    CompositeType GetDataUsingDataContract(CompositeType composite);

    //    // TODO: Hier Dienstvorgänge hinzufügen
    //}

    //// Verwenden Sie einen Datenvertrag, wie im folgenden Beispiel dargestellt, um Dienstvorgängen zusammengesetzte Typen hinzuzufügen.
    //// Sie können im Projekt XSD-Dateien hinzufügen. Sie können nach dem Erstellen des Projekts dort definierte Datentypen über den Namespace "LuckyTraderLib.ContractType" direkt verwenden.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
    #endregion

    [ServiceContract]
    public interface ILuckyTrader
    {
        [OperationContract]
        int DoRegister(string UN, string PW, string Mail, string FN, string LN, DateTime Birth);

        [OperationContract]
        int DoLogin(string UN, string PW);

        [OperationContract]
        void DoLogout(int LogID);

        [OperationContract]
        List<Stock> CheckStock();

        [OperationContract]  //nur zu Testzwecken freizugeben
        void AutoUpdateStock();

        [OperationContract]
        bool ResyncUserData(string UN, string Loc, decimal Cash, decimal Assets);

        [OperationContract]
        decimal UpdatePlayerAssets(string UN);

        [OperationContract]
        string DoStockTrade(string UN, string ShareTitle, decimal SharePrice, int ShareAmount, string TradeType, int SharePosition);
    }

    [DataContract]
    public class Stock
    {
        [DataMember]
        public string shareTitle;

        [DataMember]
        public int shareAmount;

        [DataMember]
        public decimal shareBuy;

        [DataMember]
        public decimal shareSell;
    }   
}

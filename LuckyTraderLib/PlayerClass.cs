using System;

namespace LuckyTraderLib
{
    public class PlayerClass
    {
        public int Login_ID { get; internal set; }
        public string Password { get; internal set; }
        public string Username { get; internal set; }

        private DateTime birthdate;
        private string firstName;
        private string lastName;
        private string userName;
        private string location;
        private decimal cash;
        private decimal assets;

        public PlayerClass(string uN, string fN, string lN, DateTime birth)
        {
            userName = uN;
            firstName = fN;
            lastName = lN;
            birthdate = birth;
            location = "";
            cash = 1000m;
            assets = 0m;
        }

    }
}
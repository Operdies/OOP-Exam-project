//20154304_Alexander_Nørskov_Larsen



using System;
using System.Collections.Generic;


namespace Eksamensopgave2016.Core
{
    public abstract class Transaction : IComparable<Transaction>
    {
        public static Dictionary<int, Transaction> TransactionDictionary = new Dictionary<int, Transaction>();
        private static int NextID = 1;
        public int TransactionID { get; private set; }
        public User User { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }

        public virtual void Execute()
        {
            TransactionID = NextID++;
            TransactionDictionary.Add(TransactionID, this);
        }


        protected Transaction(User user, decimal amount)
        {
            Amount = amount;
            User = user;
            Date = DateTime.Now;
        }

        public int CompareTo(Transaction other)
        {
            //Sorts by descending transaction ID
            return other.TransactionID.CompareTo(this.TransactionID);
        }

        public override string ToString()
        {
            return (
                $"Transaction ID      : {TransactionID}\n" +
                $"User                : {User}\n" +
                $"Amount              : {Amount/100}\n"+
                $"Time of transaction : {Date}"
                );
        }
    }
}

//20154304_Alexander_Nørskov_Larsen

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016.Core
{
    public abstract class Transaction : IComparable<Transaction>
    {
        private static int NextID = 1;
        public int TransactionID { get; }
        public User User { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }
        public abstract void Execute();


        protected Transaction(User user, decimal amount)
        {
            Amount = amount;
            User = user;
            TransactionID = NextID++;
            Date = DateTime.Now;
            
        }

        public int CompareTo(Transaction other)
        {
            //Sorts by descending transaction ID
            return other.TransactionID.CompareTo(this.TransactionID);
        }

        public override string ToString()
        {
            return
                $"Transaction ID : {TransactionID}\nUser : {User}\nAmount : {Amount}\nTime of transaction:{Date}";
        }
    }
}

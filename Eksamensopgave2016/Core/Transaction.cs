using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016.Core
{
    abstract class Transaction
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

        public override string ToString()
        {
            return
                $"Transaction ID : {TransactionID}\nUser : {User}\nAmount : {Amount}\nTime of transaction:{Date}";
        }
    }
}

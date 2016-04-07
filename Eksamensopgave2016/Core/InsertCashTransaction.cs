//20154304_Alexander_Nørskov_Larsen

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016.Core
{
    public class InsertCashTransaction : Transaction
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            //user.balance += amount
            return "Cash is being inserted into user account:\n\n" + base.ToString();
        }

        public InsertCashTransaction(User user, decimal amount) : base(user, amount)
        {
            
        }
    }
}

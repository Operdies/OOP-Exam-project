//20154304_Alexander_Nørskov_Larsen



using System;

namespace Eksamensopgave2016.Core
{
    public class InsertCashTransaction : Transaction
    {
        public override void Execute()
        {
            User.AddCredits(Amount);
            base.Execute();
        }

        public override string ToString()
        {
            return "Cash is being inserted into user account:\n\n" + base.ToString();
        }

        public InsertCashTransaction(User user, decimal amount) : base(user, amount)
        {
            
        }
    }
}

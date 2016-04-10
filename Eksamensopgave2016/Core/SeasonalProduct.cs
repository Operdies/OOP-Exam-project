//20154304_Alexander_Nørskov_Larsen



using System;

namespace Eksamensopgave2016.Core
{
    class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate { get; private set; }
        public DateTime SeasonEndDate { get; private set; }

        public SeasonalProduct(string productName, int productID, decimal price, 
            bool active, bool canBeBoughtOnCredit, DateTime seasonStartDate, DateTime seasonEndDate) 
            : base(productName, productID, price, active, canBeBoughtOnCredit)
        {
            
            SeasonStartDate = seasonStartDate;
            SeasonEndDate = seasonEndDate;
        }
    }
}

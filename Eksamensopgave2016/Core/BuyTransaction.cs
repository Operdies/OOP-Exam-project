//20154304_Alexander_Nørskov_Larsen

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016.Core
{
    public class BuyTransaction : Transaction
    {
        public Product Item { get; }
        public override string ToString()
        {
            return $"User purchased product: {Item}\n\n" + base.ToString();
        }

        public override void Execute()
        {
            //User.balance -= amount
            CanPurchase();
            throw new NotImplementedException();
        }

        private void CanPurchase()
        {
            if (User.BalanceDecimal >= Amount) return;
            if (Item.CanBeBoughtOnCredit == false)
                throw new InsufficientCreditsException($"User: {User.Username} has insufficient funds for product {Item.ProductName}\n" + 
                    $"{User.Username} has {User.BalanceDecimal:C} available, but the item is {Item.PriceDecimal:C}.\n" +
                    "Transfer aborted");
        }

        public BuyTransaction(User user, Product item) : base(user, item.PriceDecimal)
        {
            Item = item;
        }
    }
}

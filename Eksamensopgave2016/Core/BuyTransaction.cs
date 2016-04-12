//20154304_Alexander_Nørskov_Larsen


using System;
using System.ComponentModel.Design;

namespace Eksamensopgave2016.Core
{
    public class BuyTransaction : Transaction
    {
        public Product Item { get; }
        public override string ToString()
        {
            return $"User {User} purchased product: {Item} \n" + base.ToString();
        }

        public override void Execute()
        {
            CanPurchase();
            User.AddCredits(Amount * -1);
            base.Execute();
        }

        private void CanPurchase()
        {
            if (!Item.Active)
            {
                throw new ArgumentException("Chosen item is inactive. You may only buy items that are on the list");
            }
            if (User.BalanceDecimal >= Amount) return;
            if (!Item.CanBeBoughtOnCredit)
                throw new InsufficientCreditsException($"User: {User.Username} has insufficient funds for product {Item.ProductName}\n" + 
                    $"{User.Username} has {User.BalanceDecimal/100:C} available, but the item is {Item.PriceDecimal/100:C}.\n" +
                    "Transfer aborted");
        }

        public BuyTransaction(User user, Product item) : base(user, item.PriceDecimal)
        {
            Item = item;
        }
    }
}

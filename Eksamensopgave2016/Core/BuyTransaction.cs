//20154304_Alexander_Nørskov_Larsen





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
            CanPurchase();
            User.AddCredits(Amount * -1);
            base.Execute();
        }

        private void CanPurchase()
        {
            if (User.BalanceDecimal >= Amount) return;
            if (!Item.CanBeBoughtOnCredit)
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

//20154304_Alexander_Nørskov_Larsen

using System;
using System.Collections.Generic;
using Eksamensopgave2016.Core;

namespace Eksamensopgave2016.Interface
{
    public interface IStregsystem
    {
        
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, int amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int productID);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUser(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        //event UserBalanceNotification UserBalanceWarning;
    }
}
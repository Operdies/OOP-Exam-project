//20154304_Alexander_Nørskov_Larsen



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Eksamensopgave2016.Interface;


namespace Eksamensopgave2016.Core
{
    class Stregsystem : IStregsystem
    {
        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                return Product.ProductDictionary.Values.Where(product => product.Active).AsEnumerable();
                //return
                //    (from product in Product.ProductDictionary where product.Value.Active select product.Value).AsEnumerable();
            }
        }
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            return new InsertCashTransaction(user, amount);
        }

        public BuyTransaction BuyProduct(User user, Product item)
        {
            return new BuyTransaction(user, item);
            //BuyTransaction purchase = new BuyTransaction(user, item);
            //ExecuteTransaction(purchase);
            
            //return purchase;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
        }
        public Product GetProductByID(int productID)
        {
            return Product.ProductDictionary.Values.First(item => item.ProductID == productID);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            List<Transaction> UserTransactions =
                Transaction.TransactionDictionary.Values
                .Where(transaction => transaction.User.Equals(user)).ToList();

            UserTransactions.Sort();
            return UserTransactions.GetRange(0, count).AsEnumerable();

            //List<Transaction> RecentTransactions = new List<Transaction>();

            //for (int index = 0; index < count; index++)
            //{
            //    RecentTransactions.Add(UserTransactions[index]);
            //}
            //return RecentTransactions.AsEnumerable();
        }

        public User GetUser(Func<User, bool> predicate)
        {
            //GetUser(x => x.UserID == 5);
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            username = username.ToLower();
            return User.UserDictionary.Values.First(user => user.Username.ToLower() == username);
        }
    }
}
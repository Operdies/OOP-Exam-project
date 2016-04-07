//20154304_Alexander_Nørskov_Larsen

using System;
using System.Collections.Generic;
using System.Linq;
using Eksamensopgave2016.Interface;

namespace Eksamensopgave2016.Core
{
    class Stregsystem : IStregsystem
    {

        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                //return ProductList.Where(product => product.Active).ToList();
                throw new NotImplementedException();
            }
        }
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            return new InsertCashTransaction(user, amount);
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            return new BuyTransaction(user, product);
        }

        public Product GetProductByID(int productID)
        {
            //return ProductList.Find(product => product.ProductID == ID);
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            List<Transaction> bigList = new List<Transaction>();
            List<Transaction> filterList = new List<Transaction>();
            int limit = 0;

            for (int index = bigList.Count - 1; index >= 0 && count > limit; index--)
            {
                if (!bigList[index].User.Equals(user)) continue;
                filterList.Add(bigList[index]);
                limit++;
            }
            if (filterList.Count == 0)
                throw new ArgumentNullException($"No transaction history for user {user.Username}");
            return filterList;
        }

        public User GetUser(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            //return UserList.Find(user => user.Username == username);
            throw new NotImplementedException();
        }

        //public event UserBalanceNotification UserBalanceWarning;

        //public void BuyProduct(User user, Product item)
        //{
        //    //Transaction purchase = new BuyTransaction(user, item);
        //    //purchase.Execute();
        //    //TransactionList.Add(purchase);
        //}

        //public void AddCreditsToAccount(User user, decimal amount)
        //{
        //Transaction insertion = new InsertCashTransaction(user, amount);
        //insertion.Execute();
        //TransactionList.Add(insertion);
        //}

        //public void ExecuteTransaction(Transaction transaction)
        //{
        //    transaction.Execute();
        //}

        //public Product GetProductById(int ID)
        //{
        //    //return ProductList.Find(product => product.ProductID == ID);
        //    throw new NotImplementedException();
        //}

        //public List<User> GetUsers(Func<User, bool> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public User GetUserByUsername(string username)
        //{
        //    //return UserList.Find(user => user.Username == username);
        //    throw new NotImplementedException();
        //}

        //public event UserBalanceNotification UserBalanceWarning;

        //public List<Transaction> GetTransaction(User user, int count)
        //{
        //    //return TransactionList.Where(transaction => transaction.User.Equals(user)).ToList();
        //    throw new NotImplementedException();
        //}

    }
}
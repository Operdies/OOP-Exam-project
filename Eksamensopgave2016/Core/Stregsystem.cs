//20154304_Alexander_Nørskov_Larsen



using System;
using System.Collections.Generic;
using Eksamensopgave2016.Interface;


namespace Eksamensopgave2016.Core
{
    class Stregsystem : IStregsystem
    {
        public List<Product> ProductList { get; set; } 
        public List<User> UserList { get; set; } 
        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                //return ProductList.Where(item => item.Active).ToList();
                throw new NotImplementedException();
            }
        }
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            return new InsertCashTransaction(user, amount);
        }

        public BuyTransaction BuyProduct(User user, Product item)
        {
            BuyTransaction purchase = new BuyTransaction(user, item);
            ExecuteTransaction(purchase);
            
            return purchase;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            //TransactionList.Add(transaction);
        }
        public Product GetProductByID(int productID)
        {
            //return ProductList.Find(item => item.ProductID == ID);
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
    }
}
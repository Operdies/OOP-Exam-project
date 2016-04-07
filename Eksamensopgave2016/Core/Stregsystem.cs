using System;
using System.Collections.Generic;
using System.Linq;
using Eksamensopgave2016.Core;

namespace Eksamensopgave2016
{
    class Stregsystem : IStregsystem
    {
        public void BuyProduct(User user, Product item)
        {
            
        }

        public void AddCreditsToAccount(User user, decimal amount)
        {
            
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            
        }

        public Product GetProductById(int ID)
        {
            //return ProductList.Find(product => product.ProductID == ID);
            throw new NotImplementedException();
        }

        public List<User> GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            //return UserList.Find(user => user.Username == username);
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransaction(User user, int count)
        {
            //return TransactionList.Where(transaction => transaction.User.Equals(user)).ToList();
            throw new NotImplementedException();
        }

        public List<Product> ActiveProducts()
        {
            //return ProductList.Where(product => product.Active).ToList();
            throw new NotImplementedException();
        }
    }
}
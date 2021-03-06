﻿//20154304_Alexander_Nørskov_Larsen



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Eksamensopgave2016.Interface;
using Eksamensopgave2016.Core;


namespace Eksamensopgave2016.Core
{
    class Stregsystem : IStregsystem
    {
        public event User.UserBalanceNotifications UserBalanceWarning;
        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                return Product.ProductDictionary.Values.Where(product => product.Active).AsEnumerable();
            }
        }

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            var transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(transaction);
            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product item)
        {
            var transaction = new BuyTransaction(user, item);
            ExecuteTransaction(transaction);
            return transaction;
        }

        private void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
        }
        public Product GetProductByID(int productID)
        {
            Product item;
            if (!Product.ProductDictionary.TryGetValue(productID, out item))
                throw new ArgumentException($"No match for product ID {productID}");
            return item;
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            List<Transaction> UserTransactions =
                Transaction.TransactionDictionary.Values
                .Where(transaction => transaction.User.Equals(user)).ToList();

            UserTransactions.Sort();
            int list = UserTransactions.Count;
            count = count < list ? count : list;
            return UserTransactions.GetRange(0, count).AsEnumerable();
        }

        public User GetUser(Func<User, bool> predicate)
        {
            return User.UserDictionary.Values.First(predicate);
        }

        public User GetUserByUsername(string username)
        {
            username = username.ToLower();
            User user;
            if (!User.UserDictionary.TryGetValue(username, out user))
                throw new ArgumentException($"No match found for username {username}");
            if (user.BalanceDecimal < 5000)
            {
                UserBalanceWarning(user, user.BalanceDecimal/100);
            }
            return user;
        }
    }
}
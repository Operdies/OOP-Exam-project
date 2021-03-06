//20154304_Alexander_N�rskov_Larsen



using System;
using System.Collections.Generic;
using System.Diagnostics;
using Eksamensopgave2016.Core;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Eksamensopgave2016.Controller;


namespace Eksamensopgave2016.Interface
{
    public class StregsystemCLI : IStregsystemUI
    {
        private bool _running = true;
        private readonly IStregsystem stregsystem;
        private readonly StregsystemController controller;
        public StregsystemCLI(IStregsystem _stregsystem)
        {
            stregsystem = _stregsystem;
            controller = new StregsystemController(this, stregsystem);
            stregsystem.UserBalanceWarning += BalanceWarning;
        }

        public delegate void CommandHandler(string command);

        public event CommandHandler StregSystemEvent;

        public void BalanceWarning(User user, decimal balance)
        {
            Console.WriteLine($"Warning: Balance for user {user.Username} is low. Current balance: {balance/100:C}");
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"No user with the username {username} was found.");
        }

        public void DisplayProductNotFound(string product)
        {            
            Console.WriteLine($"No product was found for {product}");
        }
        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"Username: {user.Username}");
            Console.WriteLine($"Full name: {user.FirstName} {user.LastName}");
            Console.WriteLine($"User balance: {user.BalanceDecimal/100:C}");
            IEnumerable<Transaction> recentTransactions = stregsystem.GetTransactions(user, 10);

            foreach (Transaction recentTransaction in recentTransactions)
            {
                Console.WriteLine(recentTransaction);
            }
            if (user.BalanceDecimal < 50)
                BalanceWarning(user, user.BalanceDecimal);
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Command {command} has too many arguments.");
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"The command {adminCommand} is not a valid admin command.");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction + "\n" +
                "Product purchase succeeded.");
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine(transaction + "\n" +
                $"Product purchase succeeded {count} times");
        }

        public void Close()
        {
            _running = false;
            Transaction.TransactionLogger.Close();
            Console.WriteLine("Exiting. Goodbye");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            decimal discrepancy = (product.PriceDecimal - user.BalanceDecimal)/100;
            Console.WriteLine($"{user.Username} has insufficient credits to purchase {product.ProductName}\n"+
                $"You are {discrepancy:C} short.");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("An error has occurred.");
            Console.WriteLine(errorString);
            Console.ReadKey();
        }

        

        public void Start()
        {
            InitializeMenu();
            while (_running)
            {
                DrawMenu();
                HandleUserInput();
            }
            
        }

        private void HandleUserInput()
        {
            Console.WriteLine();
            Console.WriteLine("Quickbuy: Type in your username followed by a space and then the product ID");
            Console.WriteLine("To multibuy, insert a non-negative number between your username and product ID");
            StregSystemEvent(Console.ReadLine());
            //controller.ParseCommand(command);
        }

        private void InitializeMenu()
        {
            string path = GetMenuPath();
            StreamReader menuReader = new StreamReader(path);
            ReadMenu(menuReader);
        }

        private void ReadMenu(StreamReader menuReader)
        {
            menuReader.ReadLine(); //Skips first line
            while (!menuReader.EndOfStream)
            {
                string line = menuReader.ReadLine();
                GenerateProduct(line);
            }
            menuReader.Close();
        }

        private void GenerateProduct(string line)
        {
            string[] itemParameters = line.Split(';');
            int i = 0;
            int ID = int.Parse(itemParameters[i++]);
            string productName = PrettifyName(itemParameters[i++]);
            decimal price = decimal.Parse(itemParameters[i++]);
            bool active = int.Parse(itemParameters[i++]) == 1;

            new Product(productName, ID, price, active);

        }

        private string PrettifyName(string oldName)
        {
            string newName = oldName.Where(ch => ch != '"')
                .Aggregate("", (ch, current) => ch + current);
            if (newName.Contains(">"))
                RemoveHTML(ref newName);
            return newName.Trim();
        }
        private void RemoveHTML(ref string oldName)
        {
            string newName = "";
            string[] segments = oldName.Split('>');
            foreach (string segment in segments)
            {
                newName += segment.TakeWhile(ch => ch != '<')
                    .Aggregate("", (ch, current) => ch + current);
            }
            oldName = newName;
        }
        private string GetMenuPath()
        {
            string path = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.FullName;
            return path + @"\products.csv";
        }

        private void DrawMenu()
        {
            foreach (Product item in stregsystem.ActiveProducts)
            {
                Console.WriteLine(item);
            }
        }
    }
}
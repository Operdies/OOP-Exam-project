//20154304_Alexander_Nørskov_Larsen

using System;
using System.Collections.Generic;
using Eksamensopgave2016.Core;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Eksamensopgave2016.Interface
{
    
    class StregsystemCLI : IStregsystemUI
    {
        private bool _running = true;
        public Stregsystem stregsystem { get; private set; }
        public StregsystemCLI(Stregsystem _stregsystem)
        {
            stregsystem = _stregsystem;
            
        }

        public void DisplayUserNotFound(string username)
        {
            throw new NotImplementedException();
        }

        public void DisplayProductNotFound(string product)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public void DisplayTooManyArgumentsError(string command)
        {
               
            throw new NotImplementedException();
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            _running = false;
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
            throw new NotImplementedException();
        }

        //public event StregsystemEvent CommandEntered;

        public void Start()
        {
            InitializeMenu();
            while (_running)
            {

                DrawMenu();
                HandleUserInput();
            }
            throw new NotImplementedException();
        }

        private void HandleUserInput()
        {
            Console.WriteLine();
            Console.WriteLine("Quickbuy: Type in your username followed by a space and then the product ID");
            string command = Console.ReadLine();
            string[] parameters = command.Split(' ');
        }

        private void InitializeMenu()
        {
            string path = GetMenuPath();
            StreamReader menuReader = new StreamReader(path);
            ReadMenu(menuReader);
        }

        private void ReadMenu(StreamReader menuReader)
        {
            menuReader.ReadLine();//Skips first line
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
            return path + "products.csv";
        }

        private void DrawMenu()
        {
            //for (int index = 0; index < ProductList.Count; index++)
            //{
            //    Console.WriteLine("loL");
            //}
        }
    }
}
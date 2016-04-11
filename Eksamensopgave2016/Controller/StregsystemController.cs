//20154304_Alexander_Nørskov_Larsen



using System;
using System.Linq;
using Eksamensopgave2016.Interface;

namespace Eksamensopgave2016.Controller
{
    class StregsystemController
    {
        private readonly IStregsystemUI UserInterface;
        private readonly IStregsystem Stregsystem;

        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            UserInterface = ui;
            Stregsystem = stregsystem;
        }

        private void wait()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public void ParseCommand(string command)
        {
            string[] commandParameters =
                command.Split(' ').Where(str => string.IsNullOrWhiteSpace(str) == false).ToArray();
            if (commandParameters.Length < 1)
            {
                ErrorMessage("No arguments given");
                return;
            }
            if (commandParameters.Length > 3)
            {
                UserInterface.DisplayTooManyArgumentsError(command);
                wait();
                return;
            }
            if (commandParameters[0].StartsWith(":"))
                AdminCommand(commandParameters);

            else ParseUserCommand(commandParameters);
            
            //Splits the string by spaces and removes white space. An expanded string.Trim()

        }
        private void AdminCommand(string[] commandParameters)
        {
            string command = commandParameters[0].Substring(1);
            //quit / q              close();
            //activate / deactivate productid
            //crediton / creditoff  productid
            //addcredits            username, amount

        }

        private void ParseUserCommand(string[] commandParameters)
        {
            string username = commandParameters[0];
            int productID;
            int count;
            Core.User user = Stregsystem.GetUserByUsername(username);
        }

        private void ErrorMessage(string errorString)
        {
            UserInterface.DisplayGeneralError(errorString);
            wait();
        }
        private void MakePurchase(string username, int productID, int count)
        {
            Core.User user = Stregsystem.GetUserByUsername(username);
            Core.Product product = Stregsystem.GetProductByID(productID);
            Core.BuyTransaction transaction = Stregsystem.BuyProduct(user, product);
            UserInterface.DisplayUserBuysProduct(transaction);
        }

        private void SetProductActiveBool(int productID, bool state)
        {
            Stregsystem.GetProductByID(productID).Active = state;
        }

        private void SetProductCanBePurchasedOnCreditBool(int productID, bool state)
        {
            Stregsystem.GetProductByID(productID).CanBeBoughtOnCredit = state;
        }

        private void AddCreditsToUserAccount(string username, int amount)
        {
            Stregsystem.GetUserByUsername(username).BalanceDecimal += amount;
        }

        private void ExitProgram()
        {
            UserInterface.Close();
        }

    }
}

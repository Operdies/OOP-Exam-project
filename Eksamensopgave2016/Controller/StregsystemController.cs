//20154304_Alexander_Nørskov_Larsen


using System;
using System.Collections.Generic;
using System.Linq;
using Eksamensopgave2016.Interface;
using Eksamensopgave2016.Core;

namespace Eksamensopgave2016.Controller
{
    class StregsystemController
    {
        private readonly Dictionary<string, Delegate> AdminCommands = new Dictionary<string, Delegate>();
        private readonly IStregsystemUI UserInterface;
        private readonly IStregsystem Stregsystem;

        private void SetAdminCommands()
        {
            AdminCommands.Add(":q", new Action(ExitProgram));
            AdminCommands.Add(":quit", AdminCommands[":q"]);
            AdminCommands.Add(":deactivate", new Action<int>(DeactivateProduct));
            AdminCommands.Add(":activate", new Action<int>(ActivateProduct));
            AdminCommands.Add(":crediton", new Action<int>(CreditOn));
            AdminCommands.Add(":creditoff", new Action<int>(CreditOff));
            AdminCommands.Add(":addcredits", new Action<string, int>(AddCreditsToUserAccount));
        }

        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            UserInterface = ui;
            Stregsystem = stregsystem;
            SetAdminCommands();
        }

        public void ParseCommand(string command)
        {
            string[] commandParameters =
                command.Split(' ').Where(str => string.IsNullOrWhiteSpace(str) == false).ToArray();
            //Splits the string by spaces and removes white space. An expanded string.Trim()

            if (commandParameters.Length > 3)
            {
                UserInterface.DisplayTooManyArgumentsError(command);
                ErrorMessage("");
                return;
            }
            if (commandParameters.Length < 1)
            {
                ErrorMessage("Command must contain at least one argument.");
                return;
            }
            if (commandParameters[0].StartsWith(":"))
                AdminCommand(commandParameters);
            else ParseUserCommand(commandParameters);

            Console.ReadKey();
        }

        private void AdminCommand(string[] commandParameters)
        {
            string command = commandParameters[0];
            string user = commandParameters.Length > 1 ? commandParameters[1] : "";
            int ID = commandParameters.Length > 1 ? int.Parse(commandParameters.Last()) : 0;
            switch (commandParameters.Length)
            {
                case 1:
                    AdminCommands[command].DynamicInvoke();
                    break;
                case 2:
                    AdminCommands[command].DynamicInvoke(ID);
                    break;
                case 3:
                    AdminCommands[command].DynamicInvoke(user, ID);
                    break;
                default:
                    UserInterface.DisplayAdminCommandNotFoundMessage(command);
                    break;
            }            
        }

        private void ParseUserCommand(string[] commandParameters)
        {
            string username = commandParameters[0];
            User user = Stregsystem.GetUserByUsername(username);

            if (user == null)
            {
                UserInterface.DisplayUserNotFound(username);
                ErrorMessage("");
                return;
            }

            int count = 0;
            Product item = null;

            if (commandParameters.Length > 1)
            {
                int productID = 0;
                if (!int.TryParse(commandParameters.Last(), out productID))
                {
                    ErrorMessage("Product ID parameter was not a number!");
                    return;
                }
                if (!int.TryParse(commandParameters[1], out count))
                {
                    ErrorMessage("Multibuy parameter was not a number!");
                    return;
                }
                item = Stregsystem.GetProductByID(productID);

                if (item == null)
                {
                    UserInterface.DisplayProductNotFound("Product ID: " + productID);
                    ErrorMessage("");
                    return;
                }

                if (!item.Active)
                {
                    ErrorMessage("Selected item is inactive.");
                    return;
                }

            }

            


            {
                try
                {
                    switch (commandParameters.Length)
                    {
                        case 1:
                            UserInterface.DisplayUserInfo(user);
                            break;
                        case 2:
                            QuickBuy(user, item);
                            break;
                        case 3:
                            MultiQuickBuy(user, item, count);
                            break;
                    }
                }
                catch (InsufficientCreditsException e)
                {
                    UserInterface.DisplayInsufficientCash(user, item);
                    ErrorMessage(e.Message);
                }
            }
        }

        private void QuickBuy(User user, Product item)
        {
            BuyTransaction transaction = MakePurchase(user, item);
            UserInterface.DisplayUserBuysProduct(transaction);
        }

        private void MultiQuickBuy(User user, Product item, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                MakePurchase(user, item);
            }
            BuyTransaction transaction = MakePurchase(user, item);
            UserInterface.DisplayUserBuysProduct(count, transaction);
        }

        private BuyTransaction MakePurchase(User user, Product item)
        {
            return Stregsystem.BuyProduct(user, item);
        }

        private void ErrorMessage(string errorString)
        {
            UserInterface.DisplayGeneralError(errorString);
            Console.ReadKey();
        }

        #region Admin Commands

        private void ActivateProduct(int productID)
        {
            Stregsystem.GetProductByID(productID).Active = true;
        }

        private void DeactivateProduct(int productID)
        {
            Stregsystem.GetProductByID(productID).Active = false;
        }

        private void CreditOn(int productID)
        {
            Stregsystem.GetProductByID(productID).CanBeBoughtOnCredit = true;
        }

        private void CreditOff(int productID)
        {
            Stregsystem.GetProductByID(productID).CanBeBoughtOnCredit = false;
        }

        private void AddCreditsToUserAccount(string username, int amount)
        {
            Stregsystem.GetUserByUsername(username).AddCredits(amount);
        }

        private void ExitProgram()
        {
            UserInterface.Close();
        }

        #endregion
    }
}
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
            UserInterface.StregSystemEvent += ParseCommand;
        }

        public void ParseCommand(string command)
        {
            try
            {
                string[] commandParameters =
                    command.Split(' ').Where(str => string.IsNullOrWhiteSpace(str) == false).ToArray();
                //Splits the string by spaces and removes white space. An expanded string.Trim()

                if (commandParameters.Length > 3)
                {
                    throw new ArgumentException("Command must contain at most three arguments.\n" +
                                                $"Your command was: {command}");

                }
                if (commandParameters.Length < 1)
                {
                    throw new ArgumentException("Command must contain at least one argument");
                }
                if (commandParameters[0].StartsWith(":"))
                    ParseAdminCommmand(commandParameters);
                else ParseUserCommand(commandParameters);

                Console.ReadKey();
            }

            catch (ArgumentNullException exception)
            {
                ErrorMessage(exception.Message);
            }
            catch (ArgumentException exception)
            {
                ErrorMessage(exception.Message);
            }
            catch (InsufficientCreditsException exception)
            {
                ErrorMessage(exception.Message);
            }
            catch (Exception exception)
            {
                ErrorMessage(exception.Message);
            }

        }

        private void ParseAdminCommmand(string[] commandParameters)
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

            int count = 0;

            Product item = null;

            if (commandParameters.Length > 1)
            {
                int productID;
                if (!int.TryParse(commandParameters.Last(), out productID))
                {
                    throw new ArgumentException("Last parameter was not a number!");
                }
                if (!int.TryParse(commandParameters[1], out count))
                {
                    throw new ArgumentException("Second parameter was not a number!");
                }
                if (count < 1)
                {
                    throw new ArgumentException("Multibuy parameter must be a positive integer");
                }
                item = Stregsystem.GetProductByID(productID);
            }

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


        private void QuickBuy(User user, Product item)
        {
            BuyTransaction transaction = MakePurchase(user, item);
            UserInterface.DisplayUserBuysProduct(transaction);
        }

        private void MultiQuickBuy(User user, Product item, int count)
        {
            if (user.BalanceDecimal < count * item.PriceDecimal && item.CanBeBoughtOnCredit == false)
                throw new InsufficientCreditsException($"User: {user.Username} has insufficient funds for product {item.ProductName}\n" +
                    $"{user.Username} has {user.BalanceDecimal / 100:C} available, but the item is {count * item.PriceDecimal / 100:C}.\n" +
                    "Transfer aborted");
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
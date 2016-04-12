//20154304_Alexander_Nørskov_Larsen

using Eksamensopgave2016.Core;

namespace Eksamensopgave2016.Interface
{
    public interface IStregsystemUI
    {
        
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string product);
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void Close();
        void DisplayInsufficientCash(User user, Product product);
        void DisplayGeneralError(string errorString);
        event StregsystemCLI.CommandHandler StregSystemEvent;
        
        void Start();
    }
}
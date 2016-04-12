//20154304_Alexander_Nørskov_Larsen



using Eksamensopgave2016.Core;
using Eksamensopgave2016.Interface;

namespace Eksamensopgave2016
{
    class Program
    {
        static void Main(string[] args)
        {
            User newuser = new User("lel", "lel", "lel@lel.lel", "lmao");

            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            ui.Start();

        }
    }
}
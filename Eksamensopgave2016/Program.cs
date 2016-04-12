//20154304_Alexander_Nørskov_Larsen



using Eksamensopgave2016.Core;
using Eksamensopgave2016.Interface;

namespace Eksamensopgave2016
{
    class Program
    {
        static void Main(string[] args)
        {
            new User("Alexander", "Larsen", "anla15@student.aau.dk", "anla_15");
            //lel@lel.lel

            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            ui.Start();

        }
    }
}
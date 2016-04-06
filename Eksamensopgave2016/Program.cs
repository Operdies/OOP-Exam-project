//20154304_Alexander_Nørøskov_Larsen


using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);

            ui.Start();
            */
            string email = "lolol@xD.com";
            string local = String.Empty;
            local = email.TakeWhile(ch => ch != '@').Aggregate(local, (current, ch) => current + ch);
            Console.WriteLine(local);
            string domain = email.Substring(local.Length + 1);
            Console.WriteLine(domain);
            if (domain.Count(ch => ch == '.') != 1)
                Console.WriteLine("Domain has too many dots, doofus");


                Console.ReadKey();
        }
    }
}

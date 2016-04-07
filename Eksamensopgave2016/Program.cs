//20154304_Alexander_Nørskov_Larsen


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eksamensopgave2016.Controller;
using Eksamensopgave2016.Core;
using Eksamensopgave2016.Interface;
using System.IO;
using System.Runtime.InteropServices;


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



            List<Product> productList = new List<Product>();
            string filePath = @"C:\Users\Alex\Desktop\Eksamen\OOP-Exam-project\Eksamensopgave2016\products.csv";
            StreamReader reader = new StreamReader(filePath);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                int i = 0;
                string[] line = reader.ReadLine()?.Split(';');
                int ID = int.Parse(line[i++]);
                int j = i++;
                string name = line[j].Where(ch => ch != '\"').Aggregate("", (current, ch) => current + ch);
                if (name.Contains(">"))
                    name = RemoveHTML(name);
                decimal price = decimal.Parse(line[i++]);
                bool active = line[i] == "1";
                productList.Add(new Product(name, ID, price, active, true));
            }
            foreach (Product product in productList)
            {
                Console.WriteLine(product.ToString());
            }




            Console.ReadKey();
        }

        private static string RemoveHTML(string name)
        {
            string newName = "";
            int count = name.Count(ch => ch == '>');
            for (int i = 0; i < count; i++)
            {
                newName += name.Split('>')[i].TakeWhile(ch => ch != '<').Aggregate("", (current, ch) => current + ch);
            }

            return newName;
        }
    }
}

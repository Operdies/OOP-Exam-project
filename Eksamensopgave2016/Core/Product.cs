//20154304_Alexander_Nørskov_Larsen

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016.Core
{
    public class Product
    {
        public int ProductID { get; }

        private string _productName;
        public string ProductName {
            get { return _productName; }
            private set
            {
                InputValidation.ValidateProductName(value);
                _productName = value;
            }
        } 
        public decimal PriceDecimal { get; private set; }
        public bool Active { get; private set; }
        public bool CanBeBoughtOnCredit { get; private set; }

        public override string ToString()
        {   
            return $"{ProductID} {ProductName} {PriceDecimal/100:C}";
        }

        public Product(string productName, int productID, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            ProductName = productName;
            ProductID = productID;
            PriceDecimal = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
        }
    }

}

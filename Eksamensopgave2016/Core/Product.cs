//20154304_Alexander_Nørskov_Larsen


using System.Collections.Generic;
using System.IO;


namespace Eksamensopgave2016.Core
{
    public class Product
    {
        public static Dictionary<int, Product> ProductDictionary = new Dictionary<int, Product>();
        public int ProductID { get; }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            private set
            {
                InputValidation.ValidateProductName(value);
                _productName = value;
            }
        }

        public decimal PriceDecimal { get; set; }
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }

        public override string ToString()
        {
            return $"{ProductID} {ProductName} {PriceDecimal/100:C}";
        }

        public Product(string productName, int productID, decimal price, bool active, bool canBeBoughtOnCredit = false)
        {
            ProductName = productName;
            ProductID = productID;
            PriceDecimal = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;

            ProductDictionary.Add(productID, this);
        }
    }
}
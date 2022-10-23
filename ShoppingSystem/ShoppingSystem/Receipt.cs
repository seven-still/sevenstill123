using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingSystem
{
	public class Receipt
	{
        private string customerName;
        private List<Product> products;

        public Receipt(string customerName)
        {
            this.CustomerName = customerName;
            this.products = new List<Product>();
        }
        public string CustomerName
        {
            get { return customerName; }
            private set { 
                if(value.Length < 2  || value.Length > 40)
                {
                    throw new ArgumentException("Customer Name should be between 2 and 40 characters!");
                }
                customerName = value; }
        }

        public void AddProduct(Product product)
        {
            this.products.Add(product);
        }

        public double Total { get => this.products.Sum(p => p.Price); }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Receipt of {CustomerName}");
            sb.AppendLine($"Total Price: {this.Total:f2}");
            sb.Append("Products:");
            foreach (var prod in this.products)
            {
                sb.AppendLine();
                sb.Append(prod.ToString());
            }
            return sb.ToString();
        }
    }

}

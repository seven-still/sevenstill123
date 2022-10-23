using ShoppingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Controller
{
    private List<Product> products;
    private List<Receipt> receipts;
    public Controller()
    {
        this.products = new List<Product>();
        this.receipts = new List<Receipt>();
    }

    public string ProcessProductCommand(List<string> args)
    {
        Product product = new PhysicalProduct(args[0], double.Parse(args[1]), double.Parse(args[1]));
        this.products.Add(product);
        return $"The current customer has bought {product.Name}.";
    }

    public string ProcessServiceCommand(List<string> args)
    {
        Product service = new ServiceProduct(args[0], double.Parse(args[1]), double.Parse(args[1]));
        this.products.Add(service);
        return $"The current customer has applied for {service.Name} service";
    }

    public string ProcessCheckoutCommand(List<string> args)
    {
        Receipt receipt = new Receipt(args[0]); 
        foreach (var product in this.products)
        {
            receipt.AddProduct(product);
        }
        this.receipts.Add(receipt);
        this.products.Clear();
        return $"Customer checked out for a total of ${receipt.Total:f2}.";
    }

    public string ProcessInfoCommand(List<string> args)
    {
        StringBuilder sb = new StringBuilder();
        if (args[0].Equals("Customer"))
        {            
            sb.AppendLine("Current customer has:");
            sb.AppendLine($"Products: {this.products.Count}");
            sb.AppendLine($"Total Bill: ${this.products.Sum(p => p.Price):f2}");
        }
        else if (args[0].Equals("Shop"))
        {
            if (this.receipts.Count > 0)
            {
                sb.AppendLine("Receipts:");
                foreach (var rec in this.receipts)
                {
                    sb.AppendLine(rec.ToString());
                }
            }

            sb.AppendLine("Receipts: No receipts");
        }
        else
        {
            var currentCustomer = this.receipts.Where(r => r.CustomerName.Equals(args[0])).FirstOrDefault();
            sb.AppendLine(currentCustomer.ToString());
        }

        return sb.ToString();
    }

    public string ProcessEndCommand()
    {
        return $"Total customers today: {this.receipts.Count}";
    }

}

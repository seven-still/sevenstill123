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
        this.products = new List<Product>();//колекционира всички продукти от командите Product И Service.
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
        Receipt receipt = new Receipt(args[0]); //създава нова фактура с името на клиента

        foreach (var product in this.products)
        {
            receipt.AddProduct(product);//всички продукти регистрирани до момента в системата (this.products) се добавят в новосъздадената фактура
        }
        this.receipts.Add(receipt);
        this.products.Clear();//всички продукти се премахват от системата и се пазят само във фактурата. Всяка нова команда Product или Service се считат за нов клиент
        return $"Customer checked out for a total of ${receipt.Total:f2}.";
    }

    public string ProcessInfoCommand(List<string> args)
    {
        StringBuilder sb = new StringBuilder();
        if (args[0].Equals("Customer"))//ако командата е Info последвана от Customer връщаме информация за продуктите, които си е поръчал текущие клиент до момента(тези, които сме получили при командите Product Или Service)
        {            
            sb.AppendLine("Current customer has:");
            sb.AppendLine($"Products: {this.products.Count}");
            sb.AppendLine($"Total Bill: ${this.products.Sum(p => p.Price):f2}");
        }
        else if (args[0].Equals("Shop"))//ако командата е Info последвана от Shop се връща информация за фактурите до момента, ако няма добавени фактури в receipts отпечатваме Receipts: No receipts
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
        else //този else ако получим Info Pesho (примерно) той вече си е купил продуктите (съществува такава фактура поръчката е завършена). Той си купува продуктите когато кажем Checkout И се изпълни метода ProcessCheckoutCommand -> където създаваме вече фактурата и добавяме всички налични продукти от products
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

using System;

namespace ShoppingSystem
{
    public class ServiceProduct : Product
    {
        private double time;
        public ServiceProduct(string name, double price, double time) : base(name, price)
        {
            this.Time = time;
        }

        public double Time
        {
            get { return time; }
            private set {
                if (value <= 0)
                {
                    throw new ArgumentException("Time should be greater than 0!");
                }
                time = value; }
        }
        public override string ToString()
        {
            string result = $"Name: {Name}{Environment.NewLine}Price: {Price}{Environment.NewLine}Weight: {Time}";
            return result.ToString();
        }
    }
}

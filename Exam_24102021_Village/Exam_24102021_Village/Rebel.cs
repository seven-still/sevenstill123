using System;
namespace Village
{
    public class Rebel : Person
    {
        private int harm;


        public Rebel(string name, int age, int harm) : base(name, age)
        {
            if (base.Age > 50)
            {
                throw new ArgumentException("Age should be less or equal to 50!");
            }
            this.Harm = harm;
        }
        public int Harm
        {
            get { return harm; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Harm should be greater than 0!");
                }
                harm = value;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}Harm: {this.Harm}";
        }
    }

}
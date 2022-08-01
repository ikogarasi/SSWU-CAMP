using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.model
{
    public abstract class Product
    {
        public Product(string productName, double productPrice, double productWeight)
        {
           Name = productName;
           Price = productPrice;
           Weight = productWeight;
        }

        public virtual void ChangePrice(double percents)
        {
            Price += Price / 100 * percents;
        }

        public override string ToString()
        {
            return ("Name - " + Name + "\nPrice - " + Price + "\nWeight - " + Weight);
        }

        public string Name { get; protected set; }
        public double Price { get; protected set; }
        public double Weight { get; protected set; }

        public override int GetHashCode()
        {
            return (Name, Price, Weight).GetHashCode();
        }

        ~Product() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11.models
{
    public class DiaryProducts : Product
    {
        public DiaryProducts(string name, double price, double weight, int days)
            : base(name, price, weight)
        {
            ExpirationDate = days;
        }

        public override string ToString()
        {
            return base.ToString() + ("\nDays to expiration - " + ExpirationDate);
        }

        public override void ChangePrice(double percents)
        {
            double summaryPercents = percents;

            if (ExpirationDate <= 7)
                summaryPercents -= 10;

            Price += Price / 100 * summaryPercents;
        }

        public int ExpirationDate { get; set; }

        public override int GetHashCode()
        {
            return ExpirationDate.GetHashCode() ^ base.GetHashCode();
        }

        ~DiaryProducts() { }
    }
}

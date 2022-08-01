using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.model
{
    public class DiaryProducts : Product
    {
        public DiaryProducts(string name, double price, double weight, DateTime expiration)
            : base(name, price, weight)
        {
            ExpirationDate = expiration;
        }

        public override string ToString()
        {
            return base.ToString() + ("\nExpiration date - " + ExpirationDate);
        }

        public override void ChangePrice(double percents)
        {
            double summaryPercents = percents;

            if ((int)Math.Floor(ExpirationDate.Subtract(DateTime.Now).TotalDays) <= 2)
                summaryPercents -= 10;

            Price += Price / 100 * summaryPercents;
        }

        public DateTime ExpirationDate { get; private set; }

        public override int GetHashCode()
        {
            return ExpirationDate.GetHashCode() ^ base.GetHashCode();
        }

        ~DiaryProducts() { }
    }
}

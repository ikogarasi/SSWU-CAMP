using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.model
{
    public class Meat : Product
    {
        public enum MeatSort { FIRST, SECOND };
        public enum MeatType { MUTTON, VEAL, PORK, CHICKEN };

        public Meat(string name, double price, double weight, MeatSort sort, MeatType type)
            : base (name, price, weight)
        {
            Sort = sort;
            Type = type;
        }

        public override string ToString()
        {
            return base.ToString() + ("\nSort - " + this.Sort + "\nType - " + this.Type);
        }

        public override void ChangePrice(double percents)
        {
            double summaryPercents = percents;

            switch (Sort)
            {
                case MeatSort.FIRST:
                    summaryPercents += 10;
                    break;
                case MeatSort.SECOND:
                    summaryPercents += 7.5;
                    break;
            }

            switch (Type)
            {
                case MeatType.MUTTON:
                    summaryPercents += 10;
                    break;
                case MeatType.VEAL:
                    summaryPercents += 7.5;
                    break;
                case MeatType.PORK:
                    summaryPercents += 5;
                    break;
                case MeatType.CHICKEN:
                    summaryPercents += 2.5;
                    break;
            }

            Price += Price / 100 * summaryPercents;
        }

        public MeatSort Sort { get; set; }

        public MeatType Type { get; set; }

        public override int GetHashCode()
        {
            return (Sort, Type).GetHashCode() ^ base.GetHashCode();
        }

        ~Meat() { }
    }
}

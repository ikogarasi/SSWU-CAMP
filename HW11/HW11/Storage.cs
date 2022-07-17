using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW11.models;

namespace HW11
{
    public class Storage<T> where T : Product
    {
        private List<T> _list;
        public ErrorLogs errorLogs = new ErrorLogs(@"../../../logs/ErrorLogs.txt");
        public T this[int index]
        { 
            get 
            {
                if (index < 0 || index >= _list.Count)
                    throw new ArgumentOutOfRangeException("index");
                return _list[index]; 
            } 
            set
            {
                if (index < 0 || index >= _list.Count)
                    throw new ArgumentOutOfRangeException("index");
                _list[index] = value;
            }
        }
        
        public Storage() => _list = new List<T>(); 
        
        public Storage(List<T> list) => _list = list;

        public Storage(params T[] products)
        {
            _list = new List<T>();
            foreach(var item in products)
            {
                _list.Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        public void AddProduct(T product) => _list.Add(product);
        
        public void AddProduct(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            int lineCount = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    ++lineCount;
                    string[] productInfo = sr.ReadLine().Split(" ");
                    try
                    {
                        switch (productInfo[0].ToUpper())
                        {
                            case "MEAT":
                                {
                                    string productName = productInfo[1].Replace(productInfo[1][0], char.ToUpper(productInfo[1][0]));
                                    Meat.MeatType type = (Meat.MeatType)Enum.Parse(typeof(Meat.MeatType), productInfo[2].ToUpper());
                                    Meat.MeatSort sort = (Meat.MeatSort)Enum.Parse(typeof(Meat.MeatSort), productInfo[3].ToUpper());
                                    double weight = Convert.ToDouble(productInfo[4]);
                                    double price = Convert.ToDouble(productInfo[5]);

                                    AddProduct((T)Convert.ChangeType(new Meat(productName, price, weight, sort, type), typeof(T)));
                                    break;
                                }
                            case "DIARY_PRODUCT":
                                {
                                    string productName = productInfo[1].Replace(productInfo[1][0], char.ToUpper(productInfo[1][0]));
                                    double weight = Convert.ToDouble(productInfo[2]);
                                    double price = Convert.ToDouble(productInfo[3]);
                                    int daysToExpire = Convert.ToInt32(productInfo[3]);

                                    AddProduct((T)Convert.ChangeType(new DiaryProducts(productName, price, weight, daysToExpire), typeof(T)));
                                    break;
                                }
                            default:
                                throw new ArgumentException($" Item Type {productInfo[0]} is not correct item type");
                        }
                    }
                    catch(Exception ex)
                    {
                        errorLogs[DateTime.Now] = ($" (line {lineCount}) " + ex.Message);
                    }
                }
            }
        }
        
        /*public Storage<Meat> GetMeats()
        {
            List<Meat> temp = new List<Meat>();
            foreach(var item in _list)
            {
                if (item is Meat)
                {
                    temp.Add((Meat)Convert.ChangeType(item,typeof(Meat)));
                }
            }

            return new Storage<Meat>(temp);
        }

        public Storage<DiaryProducts> GetDiaryProducts()
        {
            List<DiaryProducts> temp = new List<DiaryProducts>();
            foreach (var item in _list)
            {
                if (item is Meat)
                {
                    temp.Add((DiaryProducts)Convert.ChangeType(item, typeof(DiaryProducts)));
                }
            }

            return new Storage<DiaryProducts>(temp);
        }*/

        public void ChangePrice(double percents)
        {
            foreach(var item in _list)
            {
                item.ChangePrice(percents);
            }
        }

        public void Remove(T product)
        {
            if (Contains(product))
                _list.Remove(product);
            else
                throw new ArgumentOutOfRangeException("product");
        }

        public override string ToString()
        {
            string temp = string.Empty;
            int index = 0;
            foreach(var item in _list)
            {
                temp += $"Product #{++index}\n{item.ToString()}";
            }

            return temp;
        }

        private bool Contains(T product)
        {
            foreach (var item in _list)
            {
                if (product.GetHashCode() == item.GetHashCode())
                {
                    return true;
                }
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HW12.model;

namespace HW12
{
    public class Storage
    {
        private List<Product> _list;
        private string sUtilizeProductPath = @"../../../UtilizedProducts.txt";
        public ErrorLogs errorLogs = new ErrorLogs(@"../../../logs/ErrorLogs.txt");
        public Product this[int index]
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
        
        public Storage() => _list = new List<Product>(); 
        
        public Storage(List<Product> list) => _list = list;

        public Storage(params Product[] products)
        {
            _list = new List<Product>();
            foreach(var item in products)
            {
                _list.Add(item);
            }
        }

        public IEnumerator<Product> GetEnumerator() => _list.GetEnumerator();

        public void AddProduct(Product product)
        {
            if (product is DiaryProducts)
            {
                if (!isProductExpired(product))
                    _list.Add(product);
                else
                    UtilizeProduct(product);
            }
            else
                _list.Add(product);

        }
        
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

                                    AddProduct(new Meat(productName, price, weight, sort, type));
                                    break;
                                }
                            case "DIARY_PRODUCT":
                                {
                                    string productName = productInfo[1].Replace(productInfo[1][0], char.ToUpper(productInfo[1][0]));
                                    double weight = Convert.ToDouble(productInfo[2]);
                                    double price = Convert.ToDouble(productInfo[3]);
                                    DateTime expirationDate = DateTime.ParseExact(productInfo[4], "d.M.yyyy", CultureInfo.CreateSpecificCulture("uk-UA"));

                                    DiaryProducts product = new DiaryProducts(productName, price, weight, expirationDate);
                                    if (!isProductExpired(product))
                                        AddProduct(product);
                                    else
                                        UtilizeProduct(product);
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
        
        public void ChangePrice(double percents)
        {
            foreach(var item in _list)
            {
                item.ChangePrice(percents);
            }
        }

        public void Remove(string productName)
        {
            var productWithName = (Product)_list.Where(i => i.Name.Equals(productName));
            if (productWithName != null)
                _list.Remove(productWithName);
            throw new ArgumentOutOfRangeException("product");
        }

        
        public bool GetProducts<T>(string? propertyName, T value, ref List<Product> productList)
        {
            bool isEmpty = true;
            foreach (var item in _list)
            {
                PropertyInfo prop = item.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null)
                {
                    if (prop.PropertyType == typeof(double))
                    {
                        if (Math.Abs((double)prop.GetValue(item) - (double)Convert.ChangeType(value, typeof(double))) <= 1e-7)
                        {
                            productList.Add(item);
                            isEmpty = false;
                        }
                    }
                    else if (prop.PropertyType.IsEnum)
                    {
                        object enumValue;
                        if (Enum.TryParse(prop.PropertyType, value.ToString(), true, out enumValue));
                            if (prop.GetValue(item).Equals(enumValue))
                        {
                            productList.Add(item);
                            isEmpty = false;
                        }
                    }
                    else if (prop.GetValue(item).Equals(value))
                    {
                        productList.Add(item);
                        isEmpty = false;
                    }
                }
            }

            return !isEmpty;
        }

        private void UtilizeProduct(Product product)
        {
            using (StreamWriter sw = File.AppendText(sUtilizeProductPath))
            {
                sw.WriteLine(product.ToString());
            }
        }

        private bool isProductExpired<T>(T product)
            where T : Product
        {
            PropertyInfo? prop = product.GetType().GetProperty("ExpirationDate");
            if (prop != null)
            {
                DateTime expDate = (DateTime)prop.GetValue(product);
                if (DateTime.Compare(expDate, DateTime.Now) <= 0)
                {
                    return true;
                }
                return false;
            }
            else
                throw new Exception();
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
    }
}

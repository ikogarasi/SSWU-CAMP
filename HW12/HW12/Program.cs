using HW12.model;
using HW12;
using System.Globalization;

Storage _storage = new Storage();

foreach (var item in Enum.GetValues(typeof(Commands)))
{
    Console.WriteLine(item);
}

while (true)
{
    try
    {
        Console.ResetColor();
        Console.Write("Enter command >> ");
        var operation = Console.ReadLine();
        if (operation?.ToUpper() == Commands.ADD_PRODUCT_FROM_FILE.ToString())
        {
            string path = string.Empty;
            do
            {
                Console.Write("Enter path >> ");
                path = Console.ReadLine();
                if (!File.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong path, file doesn\'t exist");
                    Console.ResetColor();
                }
            }
            while (!File.Exists(path));
            _storage.AddProduct(path);
        }
        else if (operation?.ToUpper() == Commands.ADD_PRODUCT.ToString())
        {
            Console.Write("Enter the type of product (meat/diary_product)\n>>");
            var typeOfProduct = (ProductType)Enum.Parse(typeof(ProductType), Console.ReadLine(), true);
            Console.Write("Enter the name of product\n>> ");
            var name = Console.ReadLine();

            Console.Write("Enter the weight of product (kg)\n>> ");
            double weight = Convert.ToDouble(Console.ReadLine());
            if (weight <= 0)
            {
                throw new ArgumentException("Error: invalid weight argument");
            }

            Console.Write("Enter the price of product\n>> ");
            double price = Convert.ToDouble(Console.ReadLine());
            if (price <= 0)
            {
                throw new ArgumentException("Error: invalid price argument");
            }

            if (typeOfProduct == ProductType.MEAT)
            {
                Console.WriteLine("Select the type of meat");
                Console.WriteLine("(mutton/veal/pork/chicken)");
                Console.Write(">> ");
                var _type = Console.ReadLine();
                Meat.MeatType type = (Meat.MeatType)Enum.Parse(typeof(Meat.MeatType), _type, true);

                Console.WriteLine("Select the sort of meat");
                Console.WriteLine("FIRST, SECOND");
                Console.Write(">> ");
                var _sort = Console.ReadLine();
                Meat.MeatSort sort = (Meat.MeatSort)Enum.Parse(typeof(Meat.MeatSort), _sort, true);

                _storage.AddProduct(new Meat(name, price, weight, sort, type));
            }
            else
            {
                Console.Write("Enter expiration date (day.month.year)\n>>");
                DateTime expirationDate = DateTime.ParseExact(Console.ReadLine(), "d.M.yyyy", CultureInfo.CreateSpecificCulture("uk-UA"));
                _storage.AddProduct(new DiaryProducts(name, price, weight, expirationDate));
            }
        }
        else if (operation?.ToUpper() == Commands.GET_ALL_PRODUCTS.ToString())
        {
            Console.WriteLine(_storage);
        }
        else if (operation?.ToUpper() == Commands.GET_SPECIFIC_PRODUCTS.ToString())
        {
            Console.Write("Enter a search parametr (parametr - value)\n>>");
            string[] prop = Console.ReadLine().Replace(" ", string.Empty).Split("-");
            List<Product> products = new List<Product>();
            if (_storage.GetProducts(prop[0], prop[1], ref products))
            {
                foreach(Product product in products)
                    Console.WriteLine(product);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are not products with such values");
                Console.ResetColor();
            }
        }
        else if (operation?.ToUpper() == Commands.AT.ToString())
        {
            Console.Write("Enter the index of product (starts from 1) >> ");
            int i = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(_storage[i - 1]);
        }
        else if (operation?.ToUpper() == Commands.EXIT.ToString())
        {
            return;
        }
        else
        {
            throw new InvalidOperationException("Error: Invalid operation");
        }
    }
    catch(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.ResetColor();
    }
}

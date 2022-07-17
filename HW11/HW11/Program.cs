using HW11.models;
using HW11;

Storage<Meat> _storage = new Storage<Meat>();

foreach (var item in Enum.GetValues(typeof(Commands)))
{
    Console.WriteLine(item);
}

while (true)
{
    try
    {
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
                    Console.WriteLine("Wrong path, file doesn\'t exist");
                }
            }
            while (!File.Exists(path));
            _storage.AddProduct(path);
        }
        else if (operation?.ToUpper() == Commands.ADD_PRODUCT.ToString())
        {
            Console.Write("Enter the name of product \n>> ");
            var name = Console.ReadLine();

            Console.Write("Enter the weight of product (kg) \n>> ");
            double weight = Convert.ToDouble(Console.ReadLine());
            if (weight <= 0)
            {
                throw new ArgumentException("Error: invalid weight argument");
            }

            Console.Write("Enter the price of product \n>> ");
            double price = Convert.ToDouble(Console.ReadLine());
            if (price <= 0)
            {
                throw new ArgumentException("Error: invalid price argument");
            }

            Console.WriteLine("Select the type of meat");
            Console.WriteLine("MUTTON, VEAL, PORK, CHICKEN");
            Console.Write(">> ");
            var _type = Console.ReadLine();
            Meat.MeatType type = (Meat.MeatType)Enum.Parse(typeof(Meat.MeatType), _type?.ToUpper() ?? string.Empty, true);

            Console.WriteLine("Select the sort of meat");
            Console.WriteLine("FIRST, SECOND");
            Console.Write(">> ");
            var _sort = Console.ReadLine();
            Meat.MeatSort sort = (Meat.MeatSort)Enum.Parse(typeof(Meat.MeatSort), _sort?.ToUpper() ?? string.Empty, true);

            _storage.AddProduct(new Meat(name, weight, price, sort, type));
        }
        else if (operation?.ToUpper() == Commands.GET_PRODUCTS.ToString())
        {
            Console.WriteLine(_storage);
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
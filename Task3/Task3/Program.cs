﻿namespace Task3
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the length of vector");
            int size;
            do
            {
                Console.Write(">> ");
                size = Convert.ToInt32(Console.ReadLine());
                try
                {
                    if (size <= 0)
                        throw new ArgumentOutOfRangeException(nameof(size) + " must be > 0");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (size <= 0);

            Vector vector = new Vector(size);

            Console.WriteLine();
            foreach (Commands command in (Commands[])Enum.GetValues(typeof(Commands)))
            {
                Console.WriteLine("- " + command);
            }

            Console.WriteLine();

            while (true)
            {
                Console.Write(">> ");
                var operation = Console.ReadLine();
                try
                {
                    if (operation?.ToUpper() == Commands.PRINT_VECTOR.ToString())
                    {
                        vector.PrintVector();
                    }
                    if (operation?.ToUpper() == Commands.RANDOM_INITIALIZATION.ToString())
                    {
                        Console.Write("Enter first number >> ");
                        int a = Convert.ToInt32(Console.ReadLine()); 
                        Console.Write("Enter second number >> ");
                        int b = Convert.ToInt32(Console.ReadLine());
                        vector.InitRand(a, b);
                        vector.PrintVector();
                    }
                    if (operation?.ToUpper() == Commands.SHUFFLE_INITIALIZATION.ToString())
                    {
                        vector.InitShuffle();
                        vector.PrintVector();
                    }
                    if (operation?.ToUpper() == Commands.CALCULATE_FREQUENCE.ToString())
                    {
                        Pair<int, int>[] pairs = vector.CalculateFrequence();
                        foreach (var pair in pairs)
                        {
                            Console.WriteLine(pair);
                        }
                    }
                    if (operation?.ToUpper() == Commands.PALINDROME_CHECK.ToString())
                    {
                        string isPalindrome = vector.CheckPalindrome() ? " is Palindrome" : "is not Palindrome";
                        Console.WriteLine("Vector" + isPalindrome);
                    }
                    if (operation?.ToUpper() == Commands.VECTOR_REVERSE.ToString())
                    {
                        vector.ReverseVector();
                        vector.PrintVector();
                    }
                    if (operation?.ToUpper() == Commands.FIND_LONGEST_COMMON_SUBSEQUENCE.ToString())
                    { 
                    
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        enum Commands
        {
            PRINT_VECTOR,
            RANDOM_INITIALIZATION,
            SHUFFLE_INITIALIZATION,
            CALCULATE_FREQUENCE,
            PALINDROME_CHECK,
            VECTOR_REVERSE,
            FIND_LONGEST_COMMON_SUBSEQUENCE,
            QUICK_SORT
        }
    }
}
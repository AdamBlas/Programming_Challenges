using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i=1;i<101;i++)
            {
                if (i % 3 == 0)
                    Console.Write("Fizz");
                if (i % 5 == 0)
                    Console.Write("Buzz");
                if (i % 3 != 0 && i % 5 != 0)
                    Console.Write(i);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}

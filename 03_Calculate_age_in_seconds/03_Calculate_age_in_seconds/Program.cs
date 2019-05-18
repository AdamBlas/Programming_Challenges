using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Calculate_age_in_seconds
{
    class Program
    {
        static void Main(string[] args)
        {
            int year, month, day;
            Console.Write("Year: ");
            year = int.Parse(Console.ReadLine());
            Console.Write("Month: ");
            month = int.Parse(Console.ReadLine());
            Console.Write("Day: ");
            day = int.Parse(Console.ReadLine());

            DateTime now = DateTime.Now;          
            DateTime date = new DateTime(year, month, day);

            Console.WriteLine("Difference in seconds: " + Math.Round((now - date).TotalSeconds));
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Name_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(); 
            string[] syllables = { "en", "le", "lo", "don", "te", "per", "lni", "kan", "mit",
                "man", "ka", "sa", "ki", "shi", "mo", "hi", "yo", "wa", "ri", "ise", "ni", "bo", "me", "zu" };

            while(true)
            {
                string name = "";
                int lenght = random.Next(3, 8);
                
                while (name.Length < lenght)
                {
                    int index = random.Next(syllables.Length);
                    name += syllables[index];
                }
                name = name.First().ToString().ToUpper() + name.Substring(1);

                Console.WriteLine(name);
                Console.ReadKey();
            }

        }
    }
}

using System;
using System.Collections.Generic;

namespace _01_HiLo_and_Head_Tails
{
    enum Type
    {
        Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King
    }
    enum Color
    {
        Hearts, Diamonds, Clubs, Spades
    }
    class Card
    {
        public Type type;
        public Color color;

        public Card(Type type, Color color)
        {
            this.type = type;
            this.color = color;
        }
        public override string ToString()
        {
            return type.ToString() + " of " + color.ToString();
        }
    }

    public class Program
    {
        private static Random random = new Random();

        private static void HiLo()
        {
            int score = 0;
            List<Card> deck = new List<Card>();
            for (int i=0;i<52; i++)
            {
                deck.Add(new Card(Type.Ace + i / 4, Color.Hearts + i % 4));
            }
            deck = Shuffle(deck);
            
            for (int i = 0; i<51; i++)
            {
                Console.Clear();
                Console.WriteLine("Score: " + score);
                Console.WriteLine(deck[i].ToString());
                Console.WriteLine("Higher or lower? Use arrows");
                ConsoleKeyInfo wybor;
                do
                {
                    wybor = Console.ReadKey();
                }
                while (wybor.Key != ConsoleKey.UpArrow && wybor.Key != ConsoleKey.DownArrow);
                if (deck[i + 1].type >= deck[i].type)
                    if (wybor.Key == ConsoleKey.UpArrow)
                        score++;
                    else
                        score--;
                else if (deck[i + 1].type <= deck[i].type)
                    if (wybor.Key == ConsoleKey.UpArrow)
                        score--;
                    else
                        score++;
            }
            Console.Clear();
            Console.WriteLine("Final Score: " + score);
            Console.WriteLine("Press anything to continue...");
            Console.ReadKey();
        }
        private static List<Card> Shuffle(List<Card> oldDeck)
        {
            List<Card> shuffledDeck = new List<Card>();
            while(oldDeck.Count > 0)
            {
                int index = random.Next(oldDeck.Count);
                shuffledDeck.Add(oldDeck[index]);
                oldDeck.Remove(oldDeck[index]);
            }
            return shuffledDeck;
        }
        private static void HeadTails()
        {
            int score = 0;
            for (int i = 0; i<10; i++)
            {
                int outcome = random.Next(1, 3);
                Console.Clear();
                Console.WriteLine("SCORE: " + score);
                Console.WriteLine("1 - Head");
                Console.WriteLine("2 - Tails");
                int wybor = 0;
                do
                {
                    wybor = Console.Read();
                } while (wybor != 49 && wybor != 50);
                if (wybor - 48 == outcome)
                    score++;
                else score--;
            }
            Console.Clear();
            Console.WriteLine("Final Score: " + score);
            Console.WriteLine("Press anything to continue...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("1 - Higher/Lower");
                Console.WriteLine("2 - Head/Tails");
                Console.Write("OPTION: ");

                int wybor = Console.Read();
                if (wybor == 49)
                    HiLo();
                else if (wybor == 50)
                    HeadTails();
            }
        }     
    } 
}

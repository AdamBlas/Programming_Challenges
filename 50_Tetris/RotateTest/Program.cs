using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotateTest
{
    public struct Point
    {
        public int x;
        public int y;

    }

    class Program
    {
        public static void Display(_50_Tetris.FieldStatus[,] grid)
        {
            for (int i=0;i<grid.GetLength(0);i++)
            {
                for (int j=0;j<grid.GetLength(1);j++)
                {
                    if (grid[i, j] == _50_Tetris.FieldStatus.Free)
                        Console.Write("F");
                    else
                        Console.Write("T");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int size = 3;
            Point[,] array = new Point[size, size];
            Point[,] newArray = new Point[size, size];
            for (int i=0;i< size; i++)
            {
                for (int j=0;j< size; j++)
                {
                    array[i, j].x = i;
                    array[i, j].y = j;
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(array[i, j].x + ", " + array[i, j].y + "\t");
                }
                Console.WriteLine();
            }
            foreach (Point p in array)
            {
                Point tmp = new Point();
                Point tmp2 = new Point();
                tmp.x = 1 - p.x;
                tmp.y = 1 - p.y;

                tmp2.x = 1 - tmp.y;
                tmp2.y = 1 + tmp.x;
                newArray[tmp2.x, tmp2.y] = p;
            }
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(newArray[i, j].x + ", " + newArray[i, j].y + "\t");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}

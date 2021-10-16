using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_B.Classes
{
    public static class TrisCollection  // Публичный статический класс, который хранит список треугольников, и на основании этой коллекции выводит треугольники определённых типов
    {
        private static List<Triangle> _collection = new List<Triangle>()        // Инициализация списка треугольников
        {
            new Triangle(new Point(1, 1), new Point(1, 7), new Point(5, 1)),
            new Triangle(new Point(3, 2), new Point(5, 7), new Point(7, 2)),
            new Triangle(new Point(-2, 0), new Point(4, 0), new Point(1.0, 3.0 * Math.Sqrt(3.0))),
            new Triangle(new Point(2, 7), new Point(4, 3), new Point(8, 2)),
            new Triangle(new Point(0, 3), new Point(-2, -3), new Point(-6, 1)),
            new Triangle(new Point(-3, -2), new Point(0, -1), new Point(-2, 5))
        };

        public static void OutputIsosceles()    // Вывод на консоль всех равнобедренных треугольников
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nIsosceles triangles\n");
            foreach(var tris in _collection)
            {
                if(tris.IsIsosceles())
                {
                    Console.WriteLine(tris);
                }
            }
            Console.ResetColor();
        }

        public static void OutputEquilateral()  // Вывод на консоль всех равносторонних треугольников
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nEquilateral triangles\n");
            foreach (var tris in _collection)
            {
                if (tris.IsEquilateral())
                {
                    Console.WriteLine(tris);
                }
            }
            Console.ResetColor();
        }

        public static void OutputRectangular()  // Вывод на консоль всех прямоугольных треугольников
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nRectangular triangles\n");
            foreach (var tris in _collection)
            {
                if (tris.IsRectangular())
                {
                    Console.WriteLine(tris);
                }
            }
            Console.ResetColor();
        }

        public static void OutputObtuse(double area = 1.0)  // Вывод на консоль всех тупоугольных треугольников, с площадью больше заданной
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nObtuse triangles\n");
            foreach (var tris in _collection)
            {
                if (tris.IsObtuse(area))
                {
                    Console.WriteLine(tris);
                }
            }
            Console.ResetColor();
        }
    }
}

// Курс по основам C# от компании Plarium
// Боярский Алексей

//  Треугольники
//  В сущностях (типах) хранятся координаты вершин треугольников на плоскости.
//  Вывести все равнобедренные треугольники.
//  Вывести все равносторонние треугольники.
//  Вывести все прямоугольные треугольники.
//  Вывести все тупоугольные треугольники с площадью больше заданной.

using System;
using Task_B.Classes;

namespace Task_B
{
    class EntryPoint
    {
        static void Main()
        {
            TrisCollection.OutputIsosceles();
            TrisCollection.OutputEquilateral();
            TrisCollection.OutputRectangular();
            Console.WriteLine("Введите площадь:\n");
            TrisCollection.OutputObtuse(Console.Read());
        }
    }
}

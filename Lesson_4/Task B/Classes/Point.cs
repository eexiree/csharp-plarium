using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_B.Classes
{
    public struct Point // Структура для хранения данных о точке в двумерном пространстве
    {
        public double x 
        {
            get;
            private set;
        }
        public double y
        {
            get;
            private set;
        }
        
        public Point(double x, double y)    // Конструктор
        {
            this.x = x;
            this.y = y;
        }

        public static double Distance(Point a, Point b) // Статический метод класса для вычисления растояния между двумя точками
        {
            return Math.Sqrt(Math.Pow(b.x - a.x, 2.0) + Math.Pow(b.y - a.y, 2.0));
        }

        public override string ToString()   // Переопределенный метод преобразования типа в строку
        {
            return $"X: {x}\tY: {y}";
        }
    }

}

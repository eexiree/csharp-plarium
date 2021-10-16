using System;

//                  B
//                  /\
//                 /  \
//                /    \
// EdgeC ---->   /      \  <---- EdgeA
//              /        \
//             /          \
//            /            \
//          A ============== C
//
//                  A
//                  |
//                  |
//                  |
//               Edge B

namespace Task_B.Classes
{
    class Triangle
    {
        public (Point vertexA, Point vertexB, Point vertexC) Vertices   // Свойство типа кортежа с тремя полями типа точек  
        {
            get;
            private set;
        }
        public (double A, double B, double C) Edges // Свойство типа кортежа с тремя полями типа double, которое отвечается за длинны между вершинами треугольника 
        {
            get;
            private set;
        }
        public double Area  // Свойство, которое возвращает площадь треугольника
        {
            get
            {
                double p = (Edges.A + Edges.B + Edges.C) / 2.0;
                return Math.Sqrt(p * (p - Edges.A) * (p - Edges.B) * (p - Edges.C));
            }
        }

        public Triangle(Point vertexA, Point vertexB, Point vertexC)    // Конструктор
        {
            Vertices = (vertexA, vertexB, vertexC);
            Edges = (Point.Distance(vertexA, vertexB),
                      Point.Distance(vertexA, vertexC),
                      Point.Distance(vertexB, vertexC));
        }

        public bool IsIsosceles() => Edges.A == Edges.B ||  // Метод, который определяет, является ли треугольник равнобедренным 
                                     Edges.A == Edges.C ||
                                     Edges.B == Edges.C;

        public bool IsEquilateral() => Edges.A == Edges.B &&    // Метод, который определяет, является ли треугольник равносторонним
                                       Edges.A == Edges.C &&
                                       Edges.B == Edges.C;

        public bool IsRectangular() // Метод, который определяет, является ли треугольник прямоугольным
        {
            double A2 = Math.Pow(Edges.A, 2.0),
                   B2 = Math.Pow(Edges.B, 2.0),
                   C2 = Math.Pow(Edges.C, 2.0);
            return A2 == B2 + C2 || B2 == A2 + C2 || C2 == A2 + B2; 
        }

        public bool IsObtuse(double area)   // Метод, который определяет, является ли треугольник тупоугольным и его площадь превышает заданую
        {
            double A2 = Math.Pow(Edges.A, 2.0),
                   B2 = Math.Pow(Edges.B, 2.0),
                   C2 = Math.Pow(Edges.C, 2.0);
            return (A2 > B2 + C2 || B2 > A2 + C2 || C2 > A2 + B2) && Area > area;
        }

        public override string ToString()   // Переопределенный метод преобразования типа в строку
        {
            return $"\nTriangle with coords:\n\nVertex A: {Vertices.vertexA}\nVertex B: {Vertices.vertexB}\nVertex C: {Vertices.vertexC}";
        }
    }
}

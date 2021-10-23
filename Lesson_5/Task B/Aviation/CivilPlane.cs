using System;

namespace Task_B.Aviation
{
    // Абстрактный класс, который описывает гражданский тип самолетов
    public abstract class CivilPlane : Plane // Наследуется от абстрактного класса самолета
    {
        // Конструктор, который передает параметры в родительский класс Plane
        protected CivilPlane(string name, int capacity, int carryingCapacity, int flightRange, int fuelConsumption) :
                  base(name, capacity, carryingCapacity, flightRange, fuelConsumption)
        {
            // Тут можно было бы определить свойства, присущи гражданским самолетам
        }

        // Переопределенный метод взлета интерфейса IFlying
        public override void TakeOff()
        {
            Console.WriteLine($"Supervisor {rng.Next() % 20}, requesting permission to take off. Taking off in 3..2..1");
        }

        // Переопределенный метод посадки интерфейса IFlying
        public override void Land()
        {
            Console.WriteLine($"Supervisor {rng.Next() % 20}, requesting permission to land.");
        }

    }
}

using System;

namespace Task_B.Aviation
{
    // Абстрактный класс, который описывает военный тип самолетов
    public abstract class MilitaryPlane : Plane
    {
        protected MilitaryPlane(string name, int capacity, int carryingCapacity, int flightRange, int fuelConsumption) :
                  base(name, capacity, carryingCapacity, flightRange, fuelConsumption)
        {
            // Тут можно было бы определить свойства, присущи военным самолетам
        }



        // Переопределенный метод взлета интерфейса IFlying
        public override void TakeOff()
        {
            Console.WriteLine($"Base {rng.Next() % 1000}, we're taking off in 3..2..1");
        }

        // Переопределенный метод посадки интерфейса IFlying
        public override void Land()
        {
            Console.WriteLine($"Base {rng.Next() % 1000}, we're landing on {rng.Next() % 10}th landing strip");
        }
    }
}

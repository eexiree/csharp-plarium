using System;

namespace Task_B.Aviation
{
    public abstract class Plane : IFlying // Абстрактный класс, который описывает общие характеристики самолёта и реализует интерфейс IFlying
    {
        #region Fields

        protected Random rng = new Random((int)DateTime.Now.Ticks); 

        // Свойство имени самолета
        public string Name
        {
            get;
            protected init;
        }

        // Свойство ID самолета
        public int ID
        {
            get => GetHashCode();
        }

        // Свойство вместимости самолета
        public int Capacity
        {
            get;
            protected init;
        }

        // Свойство грузоподъемности самолета
        public int CarryingCapacity
        {
            get;
            protected init;
        }

        // Свойство дальности полёта самолета
        public int FlightRange
        {
            get;
            protected init;
        }

        // Свойство потребления топлива самолетом
        public int FuelConsumption
        {
            get;
            protected init;
        }

        #endregion

        // Конструктор
        protected Plane(string name, int capacity, int carryingCapacity, int flightRange, int fuelConsumption)
        {
            Name = name;
            Capacity = capacity;
            CarryingCapacity = carryingCapacity;
            FlightRange = flightRange;
            FuelConsumption = fuelConsumption;
        }

        // Переопределенный метод преобразования типа в тип строки


        public override string ToString()
        {
            return $"Aircraft {ID}" +
                   $"\n{"Name".PadRight(25, '.')}{Name}" +
                   $"\n{"Capacity".PadRight(25, '.')}{Capacity} people" +
                   $"\n{"Carrying capacity".PadRight(25, '.')}{CarryingCapacity} tons" +
                   $"\n{"Flight range".PadRight(25, '.')}{FlightRange} miles" +
                   $"\n{"Fuel consumption".PadRight(25, '.')}{FuelConsumption} liters per 100 miles\n";
        }

        // Абстрактные методы интерфейса IFlying, которые необходимо реализовать в классах наследниках
        public abstract void TakeOff();
        public abstract void Land();
    }
}

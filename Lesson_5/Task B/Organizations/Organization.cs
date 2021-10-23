using System;
using System.Collections.Generic;
using Task_B.Aviation;

namespace Task_B.Organizations
{
    // Абстрактный класс, который описывает организацию воздушного флота (Может быть военная и гражданская)
    public abstract class Organization
    {
        #region Fields

        // Название организации
        public string Name
        {
            get;
            protected init;
        }

        // Тип организации
        public OrgType Type
        {
            get;
            protected init;
        }

        // Флот организации
        protected Plane[] fleet;

        #endregion

        #region Constructor
        // Конструктор
        protected Organization(string name)
        {
            Name = name;
        }

        #endregion

        #region Methods

        // Сеттер для поля флота организации
        public void CreateFleet(Plane[] fleet)
        {
            this.fleet = fleet;
        }

        // Вывод информации на консоль организации
        public void OutputData()
        {
            Console.WriteLine(this);
            foreach (var plane in fleet)
            {
                Console.WriteLine("\n\n" + plane + "\n\n");
            }
        }

        // Метод сортировки флота организации по дальности полета каждого самолета
        public void SortByFlightRange()
        {
            Array.Sort(fleet, (Plane p1, Plane p2) =>   // Используем лямбда-выражение типа компаратора для сравнения дальности полета самолетов
            {
                if (p1.FlightRange > p2.FlightRange)
                    return -1;
                else if (p1.FlightRange < p2.FlightRange)
                    return 1;
                else return 0;
            });
        }

        
    
        // Метод для рассчета общей вместимости самолетов компании
        public int TotalCapacity()
        {
            int total = 0;
            foreach(var plane in fleet)
                total += plane.Capacity;
            return total;
        }

        // Метод для рассчета общей грузоподъемности самолетов компании
        public double TotalCarryingCapacity()
        {
            int total = 0;
            foreach (var plane in fleet)
                total += plane.CarryingCapacity;
            return total;
        }

        // Метод который выводит на консоль самолеты компании, которые потребляют определённое кол-во топлива 
        public void FindPlaneByFuelIntake(int min, int max)
        {
            List<Plane> suitable = new List<Plane>();
            foreach(var plane in fleet)
            {
                if (plane.FuelConsumption >= min && plane.FuelConsumption <= max)
                    Console.WriteLine(plane);
            }
        }

        // Переопределнный метод преобразования типа в строку
        public override string ToString()
        {
            return $"Organization\n{"Name:".PadRight(25, '.')}{Name}\n{"Organization type:".PadRight(25, '.')}{Type}";
        }

        #endregion
    }

    // Перечисление типа организации
    public enum OrgType
    {
        Civil, Military
    }

}

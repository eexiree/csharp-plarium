using System;
using Task_B.Aviation;
using Task_B.Organizations;
using Task_B.Aviation.Civil;
using Task_B.Aviation.Military;

namespace Task_B.Creator
{
    public static class PlaneCreator // Статический класс для создания самолета исходя из типа организации, со случайными характеристиками
    {

        #region Fields

        public static Random rng = new Random((int)DateTime.Now.Ticks);

        private static ValueRange capacityRange             = new ValueRange(20, 100);
        private static ValueRange carryingCapacityRange     = new ValueRange(100, 1000);
        private static ValueRange flightRange               = new ValueRange(10000, 25000);
        private static ValueRange fuelConsumptionRange      = new ValueRange(40, 240);

        private static string[] _militaryPlanesNames = { "Air Hunter", "YY-11", "Falcon I", "Tiny Jack", "Shark R-17", "Lolly Pop", "KW-31Q" }; // Доступные имена для военных самолетов
        private static string[] _civilPlanesNames = { "Airbus", "Boeing-365", "AN-225", "Boeing-221", "MD-11", "Airbus-Beluga", "MD-11F" }; // Доступные имена для гражданских самолетов

        #endregion

        #region Methods

        // Метод создания флота для организации
        public static Plane[] GetAircraftFleet(OrgType type, int amountOfPlanes)
        {
            Plane[] fleet = new Plane[amountOfPlanes];
            for(int i = 0; i < amountOfPlanes; i++)
            {
                fleet[i] = CreatePlane(type);
            }
            return fleet;
        }

        // Метод создания самолета
        private static Plane CreatePlane(OrgType type)
        {
            string name             = GetPlaneName(type);
            int capacity            = capacityRange.GetRandom();
            int carryingCapacity    = carryingCapacityRange.GetRandom();
            int fRange              = flightRange.GetRandom();
            int fuelConsumption     = fuelConsumptionRange.GetRandom();

            switch (type)
            {
                case OrgType.Civil:
                    if (rng.Next(0, 2) == 0)
                        return new CargoPlane(name, capacity, carryingCapacity, fRange, fuelConsumption);
                    else return new PassagerPlane(name, capacity, carryingCapacity, fRange, fuelConsumption);
                case OrgType.Military:
                    if (rng.Next(0, 2) == 0)
                        return new Bomber(name, capacity, carryingCapacity, fRange, fuelConsumption);
                    else return new Fighter(name, capacity, carryingCapacity, fRange, fuelConsumption);
                default: return null;
            }

        }

        // Метод для получения имени самолета
        private static string GetPlaneName(OrgType type)
        {
            return new string(type == OrgType.Civil ? _civilPlanesNames[rng.Next() % _civilPlanesNames.Length] : _militaryPlanesNames[rng.Next() % _militaryPlanesNames.Length]);
        }

        #endregion

        // Структура для хранения диапазона значений 
        public struct ValueRange
        {

            public readonly int min;
            public readonly int max;
            public ValueRange(int min, int max)
            {
                this.min = min;
                this.max = max;
            }

            // Метод для получения случайного числа из заданого диапазона
            public int GetRandom()
            {
                return rng.Next(min, max);
            }
        }
    }
}

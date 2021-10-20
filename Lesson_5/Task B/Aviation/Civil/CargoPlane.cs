namespace Task_B.Aviation.Civil
{
    // Класс транспортного самолета
    public class CargoPlane : CivilPlane
    {
        // Поскольку транспортный самолет обладает отличными от других гражданских самолетов характеристиками, их можно было бы добавить, т.е. рассширить родительский класс CivilPlane

        public CargoPlane(string name, int capacity, int carryingCapacity, int flightRange, int fuelConsumption) :
               base(name, capacity, carryingCapacity, flightRange, fuelConsumption)
        {
 
        }


    }
}

namespace Task_B.Aviation.Civil
{
    // Класс пассажирского самолета
    public class PassagerPlane : CivilPlane
    {
        // Поскольку пассажирский самолет обладает отличными от других гражданских самолетов характеристиками, их можно было бы добавить, т.е. рассширить родительский класс CivilPlane
        public PassagerPlane(string name, int capacity, int carryingCapacity, int flightRange, int fuelConsumption) :
               base(name, capacity, carryingCapacity, flightRange, fuelConsumption)
        {
            
        }

    }
}

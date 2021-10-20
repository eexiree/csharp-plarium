namespace Task_B.Aviation.Military
{
    // Класс военного самолета - бомбардировщика
    public class Bomber : MilitaryPlane
    {
        // Даному классу бомбардировщика можно было бы добавить новые поля, например: количество бомб, наличие блока воздушной дозаправки, район патрулированияи т.д и т.п.
        public Bomber(string name, int capacity, int carryingCapacity, int flightRange, int fuelConsumtion) : 
               base(name, capacity, carryingCapacity, flightRange, fuelConsumtion)
        {
            
        }
    }
}

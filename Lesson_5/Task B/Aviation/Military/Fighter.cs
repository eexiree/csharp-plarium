using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_B.Aviation;

namespace Task_B.Aviation.Military
{
    // Класс военного самолета - истрибителя
    class Fighter : MilitaryPlane
    {
        // Истрибителю можно было бы добавить новые поля и методы, например: количество орудий, объем патронтажа, может ли осуществляться вертикальный взлет и т.д и т.п.
        public Fighter(string name, int capacity, int carryingCapacity, int flightRange, int fuelConsumtion) :
               base(name, capacity, carryingCapacity, flightRange, fuelConsumtion)
        {

        }
    }
}

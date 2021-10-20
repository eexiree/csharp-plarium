using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_B.Aviation
{
    // По заданию можно было не описывать данный интерфейс. Описан в учебных целях
    public interface IFlying // Интерфейс, который описывает методы посадки и взлета
    {
        void TakeOff(); // Метод взлета 

        void Land();    // Метод посадки
    }
}

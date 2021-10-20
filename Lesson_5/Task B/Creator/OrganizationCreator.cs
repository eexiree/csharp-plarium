using System;
using System.Collections.Generic;
using Task_B.Organizations;
using Task_B.Organizations.Civil;
using Task_B.Organizations.Military;

namespace Task_B.Creator
{
    public static class OrganizationCreator // Статический класс для создания определенного типа организации
    {
        // Генератор псевдослучайных чисел 
        private static Random rng = new Random((int)DateTime.Now.Ticks);

        // Метод создания организации по заданому типу
        public static Organization GetOrganization(OrgType type, int amountOfPlanesInFleet)
        {
            Organization org = type == OrgType.Civil ? new Airline(GetUniqueName(type)) : new AirForce(GetUniqueName(type));
            org.CreateFleet(PlaneCreator.GetAircraftFleet(type, amountOfPlanesInFleet));
            return org;
        }

        // Метод, который возвращает уникальное имя из существующего списка заданных имен для организаций, исходя из типа организации
        private static string GetUniqueName(OrgType type)
        {
            List<string> lst = type == OrgType.Civil ? _civilOrgNames : _militaryOrgNames; // Получаем список доступных имен по типу организации
            int randomIndex = rng.Next() % lst.Count;   // Определяем случайный индекс имени из списка возможных

            if(_militaryOrgsCounter >= _militaryOrgNames.Count || _civilOrgsCounter >= _civilOrgNames.Count) // Проверяем общее количество созданных организаций. Если превысили - создаем исключение.
                throw new ArgumentOutOfRangeException("The allowed number of organizations to be created has been exhausted");

            while(_takenNames.Contains(lst[randomIndex]))   // Находим свободное имя для организации. Не практичный алгоритм для реализации при крупной выборке объектов
            {
                randomIndex = rng.Next() % lst.Count;
            }

            if (type == OrgType.Civil)  // Увеличиваем счетчики созданных организаций
                _civilOrgsCounter++;
            else _militaryOrgsCounter++;

            _takenNames.Add(lst[randomIndex]); // В список занятых имен добавляем полученное имя 
            return new string(lst[randomIndex]);
        }

        private static int _militaryOrgsCounter = 0, _civilOrgsCounter = 0; // Счетчики созданных организаций

        private static List<string> _takenNames = new List<string>();   // Список занятых имен

        private static readonly List<string> _militaryOrgNames = new List<string>()
        { 
            "US Air Forces", "Air Horizont", "China Air Force", "Luftwaffe", "RAF", "UAR" 
        }; // Список доступных имен для военных организаций

        private static readonly List<string> _civilOrgNames = new List<string>()
        { 
            "Dubai Airlines", "American Airlines", "Bering Air", "IAU", "Comair", "Compass", "Fudziyama", "Opaska", "Deli Airlines" 
        };  // Список доступных имен для гражданских организаций
    }
}

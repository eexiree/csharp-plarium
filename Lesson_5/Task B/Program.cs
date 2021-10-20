using System;
using Task_B.Organizations;
using Task_B.Creator;

namespace Task_B
{
    class EntryPoint
    {
        static void Main()
        {
            #region Input Block
            // Блок создания организации по типу (Военная и гражданская)

            Console.WriteLine("Enter the type of the airline organization\n\n1. Civil\n2. Military\n");
            string input = Console.ReadLine();
            int type, amount;
            if(!int.TryParse(input, out type) || type != 1 && type != 2)
            {
                Console.WriteLine("Invalid inpud data");
                return;
            }
            Console.WriteLine("\nEnter the amount of the planes in organization\n");
            input = Console.ReadLine();
            if (!int.TryParse(input, out amount))
            {
                Console.WriteLine("Invalid inpud data");
                return;
            }

            // С помощью метода статического класса создаем экземпляр организации
            Organization org = OrganizationCreator.GetOrganization((OrgType)(type - 1), amount);
            Console.Clear();
            org.OutputData();   // Выводим флот организации

            Console.WriteLine("Press any key to show sorted fleet by flight range...");
            Console.ReadKey();
            Console.Clear();

            #endregion

            #region Sorted by Flight Range
            // Блок вывода флота организации в отсортированном порядке по свойству дальности полёта


            org.SortByFlightRange();
            Console.WriteLine("Sorted by Flight Range:\n");
            org.OutputData();

            Console.WriteLine("Press any key to show total Capacity and Carrying Capacity of the fleet...");
            Console.ReadKey();
            Console.Clear();

            #endregion

            #region Total data about planes
            // Блок вывода общей вместимости и грузоподъемности флота организации

            Console.WriteLine($"{"Total Capacity:".PadRight(30, '.')}{org.TotalCapacity()}\n" +
                              $"\n{"Total Carrying Capacity:".PadRight(30, '.')}{org.TotalCarryingCapacity()}\n");

            Console.WriteLine("Press any key to find planes with specific Fuel Consumption");
            Console.ReadKey();
            Console.Clear();

            #endregion

            #region Find the planes with specific Fuel Consumption
            // Блок вывода самолетов организации, которые имеют рассход топлива в заданом пользователем диапазоне

            Console.WriteLine("Enter the range of fuel consumption:\n");

            int min, max;
            Console.Write("Min:\t");
            input = Console.ReadLine();
            if (!int.TryParse(input, out min))
            {
                Console.WriteLine("Invalid inpud data");
                return;
            }
            Console.Write("Max:\t");
            input = Console.ReadLine();
            if (!int.TryParse(input, out max))
            {
                Console.WriteLine("Invalid inpud data");
                return;
            }
            org.FindPlaneByFuelIntake(min, max);

            #endregion
        }
    }
}

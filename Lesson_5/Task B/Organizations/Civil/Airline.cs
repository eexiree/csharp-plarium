namespace Task_B.Organizations.Civil
{
    // Класс гражданских авиалиний 
    public class Airline : Organization // Наследуется от класса организации
    {
        // Конструктор
        public Airline(string name) : base(name)
        {
            Type = OrgType.Civil;
        }
    }
}
